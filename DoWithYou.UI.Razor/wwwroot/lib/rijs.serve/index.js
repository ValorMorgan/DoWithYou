// -------------------------------------------
// Serves the client library /ripple.js
// -------------------------------------------
module.exports = function serve(ripple, { server, serve = __dirname, client = 'ripple' } = {}){
  log('creating')
  const { http = server } = ripple.server || {}
  if (!http) return ripple
  const app  = expressify(http)
      , path = local(serve, client)
      , compress = compression()

  app.use(`/${client}.js`, compress, send(path('js')))
  app.use(`/${client}.min.js`, compress, send(path('min.js')))
  app.use(`/${client}.pure.js`, compress, send(path('pure.js')))
  app.use(`/${client}.pure.min.js`, compress, send(path('pure.min.js')))
  return ripple
}

const expressify = server => server.express
  || key('_events.request')(server) 
  || server.on('request', express())._events.request

const local = (path, client) => ext => resolve(path, `./dist/${client}.${ext}`)

const compression = require('compression')
    , send = require('utilise/send')
    , key = require('utilise/key')
    , { resolve } = require('path')
    , express = require('express')
    , log = require('utilise/log')('[ri/serve]')