// -------------------------------------------
// Adds support for data resources
// -------------------------------------------
module.exports = function data(ripple){
  log('creating')
  ripple
    .on('change.data')
    .filter(([name, change]) => header('content-type', 'application/data')(ripple.resources[name]))
    .filter(([name, change]) => change && change.key)
    .map(([name, change]) => ripple
      .resources[name]
      .body
      .emit('change', (change || null), not(is.in(['bubble']))))

  ripple.types['application/data'] = {
    header: 'application/data'
  , selector: res => `[data~="${res.name}"]`
  , extract: el => (attr(`data`)(el) || '').split(' ')
  , check: res => is.obj(res.body) ? true : false
  , parse(res){ 
      if (is.str(res.body)) res.body = fn(res.body)
      const existing = ripple.resources[res.name] || {}

      extend(res.headers)(existing.headers)
      res.body = set()(
        res.body || []
      , existing.body && existing.body.log
      , is.num(res.headers.log) ? res.headers.log : -1
      )
      overwrite(res.body.on)(listeners(existing))
      res.body.on('change.bubble', change => {
        ripple.emit('change', ripple.change = [res.name, change], not(is.in(['data'])))
        delete ripple.change
      })
      
      return res
    }
  }

  return ripple
}

const overwrite = require('utilise/overwrite')
    , header = require('utilise/header')
    , extend = require('utilise/extend')
    , attr = require('utilise/attr')
    , not = require('utilise/not')
    , key = require('utilise/key')
    , set = require('utilise/set')
    , fn = require('utilise/fn')
    , is = require('utilise/is')
    , log = require('utilise/log')('[ri/types/data]')
    , listeners = key('body.on')