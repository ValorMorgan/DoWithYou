var features = (function () {
'use strict';

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

var commonjsGlobal = typeof window !== 'undefined' ? window : typeof global !== 'undefined' ? global : typeof self !== 'undefined' ? self : {};

var client = typeof window != 'undefined';

var owner = client ? /* istanbul ignore next */ window : commonjsGlobal;

var log$1 = function log(ns){
  return function(d){
    if (!owner.console || !console.log.apply) { return d; }
    is_1.arr(arguments[2]) && (arguments[2] = arguments[2].length);
    var args = to.arr(arguments)
      , prefix = '[log][' + (new Date()).toISOString() + ']' + ns;

    args.unshift(prefix.grey ? prefix.grey : prefix);
    return console.log.apply(console, args), d
  }
};

var includes = function includes(pattern){
  return function(d){
    return d && d.indexOf && ~d.indexOf(pattern)
  }
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

var header = function header(header$1, value) {
  var getter = arguments.length == 1;
  return function(d){ 
    return !d || !d.headers ? null
         : getter ? key(header$1)(d.headers)
                  : key(header$1)(d.headers) == value
  }
};

var append = function append(v) {
  return function(d){
    return d+v
  }
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

var datum = function datum(node){
  return node.__data__
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

var not = function not(fn){
  return function(){
    return !fn.apply(this, arguments)
  }
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

// -------------------------------------------
// Extend Components with Features
// -------------------------------------------
var index = function features(ripple){
  if (!client) { return }
  log('creating');
  ripple.render = render(ripple)(ripple.render);
  return ripple
};

var render = function (ripple) { return function (next) { return function (el) {
  var features = str(attr(el, 'is'))
          .split(' ')
          .map(from_1(ripple.resources))
          .filter(header('content-type', 'application/javascript'))
      , css = str(attr('css')(el)).split(' ');

  features
    .filter(by('headers.needs', includes('[css]')))
    .map(key('name'))
    .map(append('.css'))
    .filter(not(is_1.in(css)))
    .map(function (d) { return attr('css', (str(attr('css')(el)) + ' ' + d).trim())(el); });

  var node = next(el);

  return !node || !node.state ? undefined
       : (features
          .map(key('body'))
          .map(function (d) { return d.call(node.shadowRoot || node, node.shadowRoot || node, node.state); })
          , node)
}; }; };

var log = log$1('[ri/features]');

return index;

}());
