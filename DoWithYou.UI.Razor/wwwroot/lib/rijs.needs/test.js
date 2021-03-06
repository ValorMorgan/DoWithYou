var expect = require('chai').expect
  , noop = require('utilise/noop')
  , all = require('utilise/all')
  , time = require('utilise/time')
  , components = require('rijs.components')
  , precss = require('rijs.precss')
  , core = require('rijs.core')
  , data = require('rijs.data')
  , css = require('rijs.css')
  , fn = require('rijs.fn')
  , needs = require('./')
  , container = document.createElement('div')
  , head = document.head
  , clean, el
  
describe('Needs', function(){

  before(function(){
    document.body.appendChild(container)
    clean = document.head.innerHTML
  })
  
  beforeEach(function(){
    document.head = clean
  })

  after(function(){
    document.body.removeChild(container)
  })

  it('should add default css', function(done) {
    container.innerHTML = '<foo-bar-1>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))
      , count = 0

    ripple({
      name: 'foo-bar-1'
    , body: function() { ++count }
    , headers: { needs: '[css]' }
    })

    time(40, function() { 
      expect(attr(el, 'css')).to.be.eql('foo-bar-1.css')
      expect(count).to.be.eql(0)
      ripple('foo-bar-1.css', ':host {}')
    })

    time(80, function() { 
      expect(attr(el, 'css')).to.be.eql('foo-bar-1.css')
      expect(count).to.be.eql(1)
      done()
    })

  })

  it('should not need to add css', function(done) {
    container.innerHTML = '<foo-bar-2>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))
      , count = 0

    attr(el, 'css', 'foo-bar-2.css')

    ripple({
      name: 'foo-bar-2'
    , body: function() { ++count }
    , headers: { needs: '[css]' }
    })

    time(20, function() { 
      expect(attr(el, 'css')).to.be.eql('foo-bar-2.css')
      expect(count).to.be.eql(0)
      ripple('foo-bar-2.css', ':host {}')
    })

    time(40, function() { 
      expect(attr(el, 'css')).to.be.eql('foo-bar-2.css')
      expect(count).to.be.eql(1)
      done()
    })

  })

  it('should add specified css', function(done) {
    container.innerHTML = '<foo-bar-3>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))

    ripple({
      name: 'foo-bar-3'
    , body: noop
    , headers: { needs: '[css=foo.css]' }
    })

    time(20, function() { 
      expect(attr(el, 'css')).to.be.eql('foo.css')
      done()
    })

  })

  it('should extend css', function(done) {
    container.innerHTML = '<foo-bar-4>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))

    attr(el, 'css', 'foo.css')

    ripple({
      name: 'foo-bar-4'
    , body: noop
    , headers: { needs: '[css=bar.css]' }
    })

    time(20, function() { 
      expect(attr(el, 'css')).to.be.eql('foo.css bar.css')
      done()
    })

  })

  it('should extend css - overlap', function(done) {
    container.innerHTML = '<foo-bar-5>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))

    attr(el, 'css', 'foo.css')

    ripple({
      name: 'foo-bar-5'
    , body: noop
    , headers: { needs: '[css=foo.css bar.css]' }
    })

    time(20, function() { 
      expect(attr(el, 'css')).to.be.eql('foo.css bar.css')
      done()
    })

  })

  it('should add specified attr', function(done) {
    container.innerHTML = '<foo-bar-6>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))

    ripple({
      name: 'foo-bar-6'
    , body: noop
    , headers: { needs: '[data=foo]' }
    })

    time(20, function() { 
      expect(attr(el, 'data')).to.be.eql('foo')
      done()
    })

  })

  it('should not need to add attr', function(done) {
    container.innerHTML = '<foo-bar-7>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))
      , count = 0

    attr(el, 'data', 'foo')

    ripple({
      name: 'foo-bar-7'
    , body: function() { ++count }
    , headers: { needs: '[data=foo]' }
    })

    time(20, function() { 
      expect(attr(el, 'data')).to.be.eql('foo')
      expect(count).to.be.eql(0)
      ripple('foo', [])
    })

    time(40, function() { 
      expect(attr(el, 'data')).to.be.eql('foo')
      expect(count).to.be.eql(1)
      done()
    })

  })

  it('should extend attr', function(done) {
    container.innerHTML = '<foo-bar-8>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))

    attr(el, 'data', 'foo')

    ripple({
      name: 'foo-bar-8'
    , body: noop
    , headers: { needs: '[data=bar]' }
    })

    time(20, function() { 
      expect(attr(el, 'data')).to.be.eql('foo bar')
      done()
    })

  })

  it('should extend attr - overlap', function(done) {
    container.innerHTML = '<foo-bar-9>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))

    attr(el, 'data', 'foo')

    ripple({
      name: 'foo-bar-9'
    , body: noop
    , headers: { needs: '[data=foo bar]' }
    })

    time(20, function() { 
      expect(attr(el, 'data')).to.be.eql('foo bar')
      done()
    })

  })

  it('should work with no defined defaults', function(done) {
    container.innerHTML = '<foo-bar-10>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))
      , count = 0

    ripple('foo-bar-10', function() { ++count })

    time(20, function() { 
      expect(attr(el, 'css')).to.be.eql(null)
      expect(count).to.be.eql(1)
      done()
    })

  })

  it('should continue gracefully if component not defined', function(done) {
    container.innerHTML = '<foo-bar-11>'

    var el = container.firstElementChild
      , ripple = needs(precss(components(fn(css(data(core()))))))
  
    ripple.render(el)  
    time(20, done)
  })

})