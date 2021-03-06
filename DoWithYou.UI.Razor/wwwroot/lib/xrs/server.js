// this is a reactive server, it takes in a single stream and returns a stream
module.exports = (opts = {}, deps = {}) => {
  const { certs, port, processor } = is.fn(opts) ? { processor: opts } : opts
      , { express = require('express')()
        , http = certs
          ? require('https').createServer(certs, express)
          : require('http').createServer(express)
        , ws = new (require('uws').Server)({ server: http })
        } = deps
      , server = emitterify({ express, http, ws }) 

  ws.sockets = []
  ws.on('connection', connected(processor, server, opts.connected))
  http.listen(port, d => {
    log('listening', server.port = http.address().port)
    server.emit('listening', server)
  })

  return server
}

const connected = (processor, server, next) => socket => {
  socket.remoteAddress = socket._socket.remoteAddress
  socket.remotePort = socket._socket.remotePort
  socket.handshake = socket.upgradeReq
  socket.subscriptions = {}
  next(socket)
  socket.on('message', message(socket, processor))
  socket.on('close', disconnected(server, socket))
  socket.on('error', err)
  server.ws.sockets.push(socket)
  server.emit('connected', socket)
  log('connected'.green, `${socket.remoteAddress} | ${socket.remotePort}`.grey) 
}

const disconnected = (server, socket) => () => {
  log('disconnected'.yellow, `${socket.remoteAddress} | ${socket.remotePort}`.grey) 
  server.ws.sockets.splice(server.ws.sockets.indexOf(socket), 1)
  for (id in socket.subscriptions)
    close(socket, id)
  server.emit('disconnected', socket)
}

const close = ({ subscriptions }, id) => { 
  if (!(id in subscriptions)) return 'nuack'
  if (subscriptions[id].stream) {
    subscriptions[id].stream.source.emit('stop')
    subscriptions[id].stream.parent.off(subscriptions[id].stream)
  }
  delete subscriptions[id]
  return 'uack'
}

const message = (socket, processor) => buffer => {
  if (buffer instanceof ArrayBuffer) 
    return socket.upload.emit('progress', buffer)

  const { id, data, type } = parse(buffer)

  if (id in socket.subscriptions)
    return socket.subscriptions[id].emit('data', data)

  const res = data => socket.send(stringify({ id, data }))
      , error = e => req.res({ 
          exec: msg => console.error('(server error)', msg)
        , value: err(e.message, '\n', e.stack)
        })
      , req = socket.subscriptions[id] = emitterify({ id, error, socket, data, res })

  // TODO: For some reason, this handler is sometimes not invoked 
  // without a log statement here
  log('recv', data.type || '', data.name || '')

  try {
    unwrap(req)(
      type == 'UNSUBSCRIBE' ? close(socket, data)
    : type == 'BINARY'      ? binary(req).then(processor)
                            : processor(req, req.res)
    ) 
  } catch (e) { req.error(e) }

  req.emit('data', data)
}

const unwrap = req => reply => 
  !is.def(reply)     ? false
:  is.stream(reply)  ? (req.stream = reply.map(req.res)).source.emit('start')
:  is.promise(reply) ? reply
                        .then(unwrap(req))
                        .catch(req.error)
                     : (req.res(reply), close(req.socket, req.id))

const binary = req =>
  (req.socket.upload = extend(req)({ chunks: [], size: 0 }))
    .on('progress')
    .map(chunk => {
      req.chunks.push(new Buffer(chunk.slice()))
      req.size += chunk.byteLength
      req.res({ exec: (o, v) => o.emit('progress', v), value: req.size })
    })
    .filter(d => req.size == req.data.size)
    .map(d => {
      req.binary = stream(req.chunks)
      return req
    })

const { emitterify, is, parse, noop, extend } = require('utilise/pure')
    , { stringify } = require('cryonic')
    , log = require('utilise/log')('[xrs]')
    , err = require('utilise/err')('[xrs]')
    , deb = require('utilise/deb')('[xrs]')
    , stream = chunks => new require('stream').Readable({
        read(){
          this.push(chunks.length 
            ? new Buffer(new Uint8Array(chunks.shift())) 
            : null
          )
        }
      })