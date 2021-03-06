var expect = require('chai').expect
  , time = require('utilise/time')
  , keys = require('utilise/keys')
  , str = require('utilise/str')
  , core = require('rijs.core')
  , data = require('rijs.data')
  , fn = require('rijs.fn')
  , offline = require('./')
  
describe('Offline', function(){

  beforeEach(function() {
    delete localStorage.ripple
  })

  afterEach(function() {
    delete localStorage.ripple
  })

  it('should cache to localStorage', function(done){  
    
    var ripple1 = offline(data(fn(core())))
    ripple1
      .resource('foo', [1,2,3])
      .resource('bar', function(){ "baz" })

    expect(localStorage.ripple).to.not.be.ok
    
    time(1050, function(){
      expect(localStorage.ripple.replace(/[ |;]/g, '')).to.eql(
        '[{"name":"foo","body":[1,2,3],"headers":{"content-type":"application/data"}},{"name":"bar","headers":{"content-type":"application/javascript"},"body":"function(){\\"baz\\"}"}]')
      done()
    })
  })

  it('should load from localStorage', function(done){  
    var ripple1 = offline(data(fn(core())))

    ripple1
      .resource('foo', [1,2,3])
      .resource('bar', function(){ "baz" })

    time(1050, function(){
      var ripple2 = offline(data(fn(core())))
      expect(keys(ripple2.resources)).to.eql(['foo', 'bar'])

      expect(ripple2.resources.foo.name).to.be.eql('foo')
      expect(ripple2.resources.foo.body).to.be.eql([1,2,3])
      expect(ripple2.resources.foo.headers['content-type']).to.be.eql('application/data')

      expect(ripple2.resources.bar.name).to.be.eql('bar')
      expect(ripple2.resources.bar.body).to.be.a('function')
      expect(str(ripple2.resources.bar.body).replace(/ /g, '')).to.be.eql(str(function (){ "baz" }).replace(/ /g, ''))
      expect(ripple2.resources.bar.headers['content-type']).to.be.eql('application/javascript')
      
      done()
    })
  })

})

