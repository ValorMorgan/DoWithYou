module.exports = {
  header: 'text/plain'
, check(res){ return !includes('.html')(res.name) && !includes('.css')(res.name) && is.str(res.body) }
}

const includes = require('utilise/includes')
    , is = require('utilise/is')