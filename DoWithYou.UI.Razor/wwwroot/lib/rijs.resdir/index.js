// -------------------------------------------
// Loads resources from the /resources folder
// -------------------------------------------
module.exports = function resdir(ripple, { dir = '.', watch = isNonProd(), glob = '/resources/**/!(*test).{js,css}' } = {}){
  log('creating', { watch })
  const argv = require('minimist')(process.argv.slice(2))
      , folders = (argv.r || argv.resdirs || '')
          .split(',')
          .concat(dir)
          .filter(Boolean)
          .map(d => resolve(d))
          .map(append(glob))

  ripple.watcher = chokidar.watch(folders, { ignored: /\b_/ })
    .on('error', err)
    .on('add', register(ripple))
    .on('change', register(ripple))
    .on('ready', async () => {
      if (!watch) ripple.watcher.close()
      await Promise.all(values(ripple.resources)
        .map(loaded(ripple)))
        .catch(err)

      def(ripple, 'ready', true)
      ripple.emit('ready')
    })

  ripple.get = get(ripple)
  return ripple
}

const get = ripple => async name => 
  name in ripple.resources 
? ripple(name)
: await ripple
    .on('change')
    .map(([name]) => name)
    .filter(d => d == name)
    .filter((d, i, n) => n.source.emit('stop'))
    .map(ripple)

const register = ripple => path => {
  var last = basename(path)
    , isJS = extname(path) == '.js'
    , name = isJS ? last.replace('.js', '') : last
    , cach = delete require.cache[path]
    , body = (isJS ? require : file)(path)
    , css  = isJS && exists(path.replace('.js', '.css'))
    , res  = is.obj(body = body.default || body) ? body 
           : css ? { name, body, headers: { needs: '[css]' } } 
                 : { name, body } 

  ripple(res)

  if (ripple.ready) 
    loaded(ripple)(ripple.resources[res.name])
}

function isNonProd(){
  return lo(process.env.NODE_ENV) != 'prod' && lo(process.env.NODE_ENV) != 'production'
}

const { resolve, basename, extname } = require('path')
    , exists = require('fs').existsSync
    , chokidar = require('chokidar')
    , append = require('utilise/append')
    , values = require('utilise/values')
    , file = require('utilise/file')
    , def = require('utilise/def')
    , is = require('utilise/is')
    , lo = require('utilise/lo')
    , log = require('utilise/log')('[ri/resdir]')
    , err = require('utilise/err')('[ri/resdir]')
    , loaded = ripple => async res => {
        await (is.fn(res.headers.loaded) && res.headers.loaded(ripple, res))
        ripple.emit('loaded', res.name)
      }