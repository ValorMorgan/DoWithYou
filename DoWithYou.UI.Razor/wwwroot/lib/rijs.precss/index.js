// -------------------------------------------
// Pre-applies Scoped CSS [css=name]
// -------------------------------------------
module.exports = function precss(ripple){
  if (!client) return;
  log('creating')  
  ripple.render = render(ripple)(ripple.render)
  return ripple
}

const render = ripple => next => host => {
  var css = str(attr(host, 'css')).split(' ').filter(Boolean)
    , root = host.shadowRoot || host
    , head = document.head
    , shadow = head.createShadowRoot && host.shadowRoot
    , styles

  // this host does not have a css dep, continue with rest of rendering pipeline
  if (!css.length) return next(host)
  
  // this host has a css dep, but it is not loaded yet - stop rendering this host
  if (css.some(not(is.in(ripple.resources)))) return;

  css
    // reuse or create style tag
    .map(d => ({ 
      res: ripple.resources[d] 
    , el: raw(`style[resource="${d}"]`, shadow ? root : head) || el(`style[resource=${d}]`) 
    }))
    // check if hash of styles changed
    .filter((d, i) => d.el.hash != d.res.headers.hash)
    .map((d, i) => {
      d.el.hash = d.res.headers.hash
      d.el.innerHTML = shadow ? d.res.body : transform(d.res.body, d.res.name)
      return d.el
    })
    .filter(not(by('parentNode')))
    .map(d => shadow ? root.insertBefore(d, root.firstChild) : head.appendChild(d))

  // continue with rest of the rendering pipeline
  return next(host)
}

const transform = (styles, name) => scope(styles, '[css~="' + name + '"]')

const log = require('utilise/log')('[ri/precss]')
    , client = require('utilise/client')
    , attr = require('utilise/attr')
    , raw = require('utilise/raw')
    , str = require('utilise/str')
    , not = require('utilise/not')
    , by = require('utilise/by')
    , is = require('utilise/is')
    , el = require('utilise/el')
    , scope = require('cssscope')