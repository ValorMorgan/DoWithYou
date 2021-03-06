// -------------------------------------------
// Exposes a convenient global instance 
// -------------------------------------------
module.exports = function css(ripple){
  log('creating')
  ripple.types['text/css'] = {
    header: 'text/css'
  , selector: res => `[css~="${res.name}"]`
  , extract: el => (attr(`css`)(el) || '').split(' ')
  , check(res){ return includes('.css')(res.name) }
  , parse(res){ 
      res.headers.hash = djb(res.body)
      return res
    }
  }

  return ripple
}

const includes = require('utilise/includes')
    , attr = require('utilise/attr')
    , log = require('utilise/log')('[ri/types/css]')

const djb = str => {
  let hash = 5381
    , i = str.length

  while (i)
    hash = (hash * 33) ^ str.charCodeAt(--i)

  return hash >>> 0
}