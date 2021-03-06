var components = (function () {
'use strict';

var commonjsGlobal = typeof window !== 'undefined' ? window : typeof global !== 'undefined' ? global : typeof self !== 'undefined' ? self : {};





function createCommonjsModule(fn, module) {
	return module = { exports: {} }, fn(module, module.exports), module.exports;
}

var is_1 = is;
is.fn      = isFunction;
is.str     = isString;
is.num     = isNumber;
is.obj     = isObject;
is.lit     = isLiteral;
is.bol     = isBoolean;
is.truthy  = isTruthy;
is.falsy   = isFalsy;
is.arr     = isArray;
is.null    = isNull;
is.def     = isDef;
is.in      = isIn;
is.promise = isPromise;
is.stream  = isStream;

function is(v){
  return function(d){
    return d == v
  }
}

function isFunction(d) {
  return typeof d == 'function'
}

function isBoolean(d) {
  return typeof d == 'boolean'
}

function isString(d) {
  return typeof d == 'string'
}

function isNumber(d) {
  return typeof d == 'number'
}

function isObject(d) {
  return typeof d == 'object'
}

function isLiteral(d) {
  return typeof d == 'object' 
      && !(d instanceof Array)
}

function isTruthy(d) {
  return !!d == true
}

function isFalsy(d) {
  return !!d == false
}

function isArray(d) {
  return d instanceof Array
}

function isNull(d) {
  return d === null
}

function isDef(d) {
  return typeof d !== 'undefined'
}

function isPromise(d) {
  return d instanceof Promise
}

function isStream(d) {
  return !!(d && d.next)
}

function isIn(set) {
  return function(d){
    return !set ? false  
         : set.indexOf ? ~set.indexOf(d)
         : d in set
  }
}

var keys = function keys(o) { 
  return Object.keys(is_1.obj(o) || is_1.fn(o) ? o : {})
};

var copy = function copy(from, to){ 
  return function(d){ 
    return to[d] = from[d], d
  }
};

var overwrite = function overwrite(to){ 
  return function(from){
    keys(from)
      .map(copy(from, to));
        
    return to
  }
};

var includes = function includes(pattern){
  return function(d){
    return d && d.indexOf && ~d.indexOf(pattern)
  }
};

var client = typeof window != 'undefined';

var datum = function datum(node){
  return node.__data__
};

var wrap = function wrap(d){
  return function(){
    return d
  }
};

var str = function str(d){
  return d === 0 ? '0'
       : !d ? ''
       : is_1.fn(d) ? '' + d
       : is_1.obj(d) ? JSON.stringify(d)
       : String(d)
};

var key = function key(k, v){ 
  var set = arguments.length > 1
    , keys = is_1.fn(k) ? [] : str(k).split('.')
    , root = keys.shift();

  return function deep(o, i){
    var masked = {};
    
    return !o ? undefined 
         : !is_1.num(k) && !k ? o
         : is_1.arr(k) ? (k.map(copy), masked)
         : o[k] || !keys.length ? (set ? ((o[k] = is_1.fn(v) ? v(o[k], i) : v), o)
                                       :  (is_1.fn(k) ? k(o) : o[k]))
                                : (set ? (key(keys.join('.'), v)(o[root] ? o[root] : (o[root] = {})), o)
                                       :  key(keys.join('.'))(o[root]))

    function copy(k){
      var val = key(k)(o);
      if (val != undefined) 
        { key(k, is_1.fn(val) ? wrap(val) : val)(masked); }
    }
  }
};

var from_1 = from;
from.parent = fromParent;

function from(o){
  return function(k){
    return key(k)(o)
  }
}

function fromParent(k){
  return datum(this.parentNode)[k]
}

var values = function values(o) {
  return !o ? [] : keys(o).map(from_1(o))
};

var ready = function ready(fn){
  return document.body ? fn() : document.addEventListener('DOMContentLoaded', fn.bind(this))
};

var attr = function attr(name, value) {
  var args = arguments.length;
  
  return !is_1.str(name) && args == 2 ? attr(arguments[1]).call(this, arguments[0])
       : !is_1.str(name) && args == 3 ? attr(arguments[1], arguments[2]).call(this, arguments[0])
       :  function(el){
            var ctx = this || {};
            el = ctx.nodeName || is_1.fn(ctx.node) ? ctx : el;
            el = el.node ? el.node() : el;
            el = el.host || el;

            return args > 1 && value === false ? el.removeAttribute(name)
                 : args > 1                    ? (el.setAttribute(name, value), value)
                 : el.attributes.getNamedItem(name) 
                && el.attributes.getNamedItem(name).value
          } 
};

var noop = function noop(){};

var to = { 
  arr: toArray
, obj: toObject
};

function toArray(d){
  return Array.prototype.slice.call(d, 0)
}

function toObject(d) {
  var by = 'id';

  return arguments.length == 1 
    ? (by = d, reduce)
    : reduce.apply(this, arguments)

  function reduce(p,v,i){
    if (i === 0) { p = {}; }
    p[is_1.fn(by) ? by(v, i) : v[by]] = v;
    return p
  }
}

var all = function all(selector, doc){
  var prefix = !doc && document.head.createShadowRoot ? 'html /deep/ ' : '';
  return to.arr((doc || document).querySelectorAll(prefix+selector))
};

var by = function by(k, v){
  var exists = arguments.length == 1;
  return function(o){
    var d = is_1.fn(k) ? k(o) : key(k)(o);
    
    return d && v && d.toLowerCase && v.toLowerCase ? d.toLowerCase() === v.toLowerCase()
         : exists ? Boolean(d)
         : is_1.fn(v) ? v(d)
         : d == v
  }
};

var lo = function lo(d){
  return (d || '').toLowerCase()
};

var owner = client ? /* istanbul ignore next */ window : commonjsGlobal;

var log = function log(ns){
  return function(d){
    if (!owner.console || !console.log.apply) { return d; }
    is_1.arr(arguments[2]) && (arguments[2] = arguments[2].length);
    var args = to.arr(arguments)
      , prefix = '[log][' + (new Date()).toISOString() + ']' + ns;

    args.unshift(prefix.grey ? prefix.grey : prefix);
    return console.log.apply(console, args), d
  }
};

var err = function err(ns){
  return function(d){
    if (!owner.console || !console.error.apply) { return d; }
    is_1.arr(arguments[2]) && (arguments[2] = arguments[2].length);
    var args = to.arr(arguments)
      , prefix = '[err][' + (new Date()).toISOString() + ']' + ns;

    args.unshift(prefix.red ? prefix.red : prefix);
    return console.error.apply(console, args), d
  }
};

var components = createCommonjsModule(function (module) {
// -------------------------------------------
// API: Renders specific nodes, resources or everything
// -------------------------------------------
// ripple.draw()                 - redraw all components on page
// ripple.draw(element)          - redraw specific element
// ripple.draw.call(element)     - redraw specific element
// ripple.draw.call(selection)   - redraw D3 selection
// ripple.draw('name')           - redraw elements that depend on resource
// ripple.draw({ ... })          - redraw elements that depend on resource
// MutationObserver(ripple.draw) - redraws element being observed

module.exports = function components(ripple){
  if (!client) { return ripple }
  log$$1('creating');
  
  ripple.draw = Node.prototype.draw = draw(ripple);
  ripple.render = render(ripple);
  ripple.on('change.draw', ripple.draw);
  ready(start(ripple));
  return ripple
};

// public draw api
function draw(ripple){
  return function(thing) { 
    return this && this.nodeName        ? invoke(ripple)(this)
         : this && this.node            ? invoke(ripple)(this.node())
         : !thing                       ? everything(ripple)
         : thing    instanceof mutation ? invoke(ripple)(thing.target)
         : thing[0] instanceof mutation ? invoke(ripple)(thing[0].target)
         : thing.nodeName               ? invoke(ripple)(thing)
         : thing.node                   ? invoke(ripple)(thing.node())
         : thing.name                   ? resource(ripple)(thing.name)
         : is_1.str(thing)                ? resource(ripple)(thing)
         : err$$1('could not update', thing)
  }
}

var start = function (ripple) { return function (d) { return all('*')
  .filter(by('nodeName', includes('-')))
  .map(ripple.draw); }; };

// render all components
var everything = function (ripple) {
  var selector = values(ripple.resources)
    .map(function (res) { return (ripple.types[res.headers['content-type']].selector || noop)(res); })
    .join(',');

  return all(selector || null)
    .map(invoke(ripple))
};

// render all elements that depend on the resource
var resource = function (ripple) { return function (name) { 
  var res  = ripple.resources[name]
      , type = res.headers['content-type'];

  return all((ripple.types[type].selector || noop)(res))
    .map(invoke(ripple))
}; };

// batch renders on render frames
var batch = function (ripple) { return function (el) {
  if (!el.pending) {
    el.pending = [];
    requestAnimationFrame(function (d) {
      el.changes = el.pending;
      delete el.pending;
      ripple.render(el);
    });    
  }

  if (ripple.change) 
    { el.pending.push(ripple.change[1]); }
}; };

// main function to render a particular custom element with any data it needs
var invoke = function (ripple) { return function (el) { 
  if (!includes('-')(el.nodeName)) { return }
  if (el.nodeName == '#document-fragment') { return invoke(ripple)(el.host) }
  if (el.nodeName == '#text') { return invoke(ripple)(el.parentNode) }
  if (!el.matches(isAttached)) { return }
  if (attr(el, 'inert') != null) { return }
  return batch(ripple)(el), el
}; };

var render = function (ripple) { return function (el) {
  var root = el.shadowRoot || el
    , deps = attr(el, 'data')
    , data = bodies(ripple)(deps)
    , fn   = body(ripple)(lo(el.tagName))
    , isClass = fn && fn.prototype && fn.prototype.render;

  if (!fn) { return el }
  if (deps && !data) { return el }
  if (isClass && root.class != fn) {
    Object.getOwnPropertyNames((root.class = fn).prototype)
      .map(function (method) { return root[method] = fn.prototype[method].bind(root); });

    Promise
      .resolve((root.init || noop).call(root, root, root.state = root.state || {}))
      .then(function (d) { return ripple.draw(root.initialised = root); });
    return el
  }
  if (isClass && !root.initialised) { return }

  try {
    (root.render || fn).call(root, root, defaults(el, data));
  } catch (e) {
    err$$1(e, e.stack);
  }

  return el
}; };

// helpers
var defaults = function (el, data) {
  el.state = el.state || {};
  overwrite(el.state)(data);
  overwrite(el.state)(el.__data__);
  el.__data__ = el.state;
  return el.state
};

var bodies = function (ripple) { return function (deps) {
  var o = {}
    , names = deps ? deps.split(' ') : [];

  names.map(function (d) { return o[d] = body(ripple)(d); });

  return !names.length            ? undefined
       : values(o).some(is_1.falsy) ? undefined 
       : o
}; };

var body = function (ripple) { return function (name) { return ripple.resources[name] && ripple.resources[name].body; }; };

var log$$1 = log('[ri/components]')
    , err$$1 = err('[ri/components]')
    , mutation = client && window.MutationRecord || noop
    , customs = client && !!window.document.registerElement
    , isAttached = customs
                  ? 'html *, :host-context(html) *'
                  : 'html *';
client && (window.Element.prototype.matches = window.Element.prototype.matches || window.Element.prototype.msMatchesSelector);
});

return components;

}());
