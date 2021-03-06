var expect = require('chai').expect
  , core = require('rijs.core')
  , noop = require('utilise/noop')
  , fn = require('./')

describe('Function Type', function() {

  it('should create fn resource', function(){  
    var ripple = fn(core())
    ripple('foo', String)
    expect(ripple('foo')).to.eql(String)
    expect(ripple.resources.foo.headers).to.be.eql({ 
      'content-type': 'application/javascript'
    , 'transpile': { limit: 25 }
    })
  })

  it('should not create fn resource', function(){  
    var ripple = fn(core())
    ripple('baz', [])
    expect(ripple.resources['baz']).to.not.be.ok
  })

})