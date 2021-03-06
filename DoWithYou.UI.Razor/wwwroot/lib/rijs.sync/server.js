// -------------------------------------------
// Synchronises resources between server/client
// -------------------------------------------
module.exports = function sync(
  ripple
, { server
  , certs
  , port = 5000
  } = {}
, { xrs = require('xrs/server')
  } = {}
){
  ripple.server = xrs({ 
    processor: processor(ripple)
  , connected
  , certs
  , port 
  }, { http: server })
  ripple.server.blacklist = ['loaded', 'transpile', 'from']
  ripple.caches = {}
  ripple
    .on('change')
    .map(([name]) => name)
    .filter(is.in(ripple.caches))
    .each(name => { delete ripple.caches[name] })

  return ripple
}

const processor = ripple => async (req, res) => { 
  let reply
  return req.binary                       ? req.socket.uploads[req.data.meta.index].emit('file', req)
       : req.data.type == 'PREUPLOAD'     ? upload(ripple, req, res)
       : (reply = await xres(ripple, req, res)) ? reply
       : req.data.type == 'SUBSCRIBE'     ? subscribe(ripple, req, res) 
                                          : false
}

const connected = socket => (socket.platform = parse(socket.handshake.headers['user-agent']))

const parse = useragent => {
  let { name, version, os } = platform.parse(useragent)
  name = lo(name)
  version = major(version)
  os = {
    name: lo((os.family || '').split(' ').shift())
  , version: major(os.version, os.family)
  }

  if (os.name == 'os') os.name == 'osx'
  if (name == 'chrome mobile') name = 'chrome'
  if (name == 'microsoft edge') name = 'edge'

  return { 
    uid: `${name}-${version}-${os.name}-${os.version}`
  , name
  , version
  , os
  }
}

const xres = async (ripple, req, res) => {
  if (!(req.data.name in ripple.resources))
    await ripple
      .on('change')
      .pipe(toplevelchange(req.data.name))

  const resource = ripple.resources[req.data.name]
      , { from = noop } = resource.headers

  return unpromise(from(req, res))
}

const unpromise = d => (d && d.next ? d.unpromise() : d)

const subscribe = (ripple, req) => ripple
  .on('change')
  .on('start', function(){ 
    const { body, headers } = ripple.resources[req.data.name] || {}
    this.next(req.data.value
      ? [req.data.name, { type: 'update', key: req.data.value, value: key(req.data.value)(body) }]
      : [req.data.name, { type: 'update', value: body, headers: strip(ripple.server.blacklist, headers) }]
    )
  })
  .filter(([name]) => name == req.data.name)
  .map(arr => arr[1])
  .map(transpile(ripple.caches, req.socket.platform, ripple.resources[req.data.name]))
  .filter(by('key', subset(req.data.value)))
  .map(({ key, type, value, headers }) => ({ key: str(key).replace(req.data.value, ''), type, value, headers }))
  .unpromise()

const strip = (list, o) => keys(o)
  .filter(not(is.in(list)))
  .reduce((p, k) => (p[k] = o[k], p), {})

const transpile = (caches, platform, { name, headers }, limit) => change => {
  if (!(limit = key('transpile.limit')(headers)) || change.type !== 'update') return change

  const browser = `${platform.name}-${platform.version}`
      , cache = name in caches ? caches[name] : (caches[name] = new LRUMap(limit))

  change.value = 
    change.key         ? transform(change.value, platform) 
  : cache.has(browser) ? cache.get(browser)
  : (cache.set(browser, transform(change.value, platform)), cache.get(browser))

  return change
}

const transform = (thing, { name, version }) =>
  is.fn(thing)  ? fn(buble.transform(`(${thing})`, { target: { [name]: version, fallback }}).code)
: is.lit(thing) ? keys(thing)
                    .map(key => ({ key, val: transform(thing[key], { name, version }) }))
                    .reduce((p, v) => (p[v.key] = v.val, p), {})
                : thing

const upload = async (ripple, req, res) => {
  let uploads = req.socket.uploads = req.socket.uploads || {}
    , { name, index, fields, size } = req.data
    , upload = emitterify(uploads[index] = fields)

  if (size) await upload
    .on('file')
    .map(file => (upload[file.data.meta.field][file.data.meta.i] = file.binary, file))
    .reduce(((received = 0, file) => received += file.size))
    .filter(received => received == size)
    
  delete uploads[index]

  return processor(ripple)({ 
    socket: req.socket
  , data: {
      name
    , type: 'UPLOAD'
    , value: upload
    }
  }, res)
}

const subset = (target = '') => (source = '') => str(source).startsWith(target)

const major = (v, f) => 
    v                     ? v.split('.').shift() 
  : includes('xp')(lo(f)) ? 'xp'
                          : '?'

const toplevelchange = name => o => o
  .filter(arr => !name || arr[0] === name)
  .filter(([name, change]) => !change.key && change.type == 'update')
  .map(([name]) => name)
    
const by = require('utilise/by')
    , is = require('utilise/is')
    , lo = require('utilise/lo')
    , fn = require('utilise/fn')
    , str = require('utilise/str')
    , not = require('utilise/not')
    , key = require('utilise/key')
    , keys = require('utilise/keys')
    , noop = require('utilise/noop')
    , includes = require('utilise/includes')
    , emitterify = require('utilise/emitterify')
    , { LRUMap } = require('lru_map')
    , platform = require('platform')
    , buble = require('buble')
    , fallback = { ie: 8, suppress: true }