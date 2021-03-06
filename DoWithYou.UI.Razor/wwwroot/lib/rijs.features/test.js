const { expect } = require('chai')
    , components = require('rijs.components')
    , precss = require('rijs.precss')
    , needs = require('rijs.needs')
    , core = require('rijs.core')
    , data = require('rijs.data')
    , css = require('rijs.css')
    , fn = require('rijs.fn')
    , features = require('./')
    , includes = require('utilise/includes')
    , time = require('utilise/time')
    , noop = require('utilise/noop')
    , raw = require('utilise/raw')

var container = document.createElement('div')
  , head = document.head
  , el1, el2, el3, clean

describe('Features', d => {

  before(() => (document.body.appendChild(container), clean = document.head.innerHTML))
  
  beforeEach(() => document.head.innerHTML = clean)

  after(() => document.body.removeChild(container))

  it('should extend component with feature', done => {
    container.innerHTML = '<foo-bar-1 is="featurable1">'

    var el = container.firstElementChild
      , ripple = features(precss(components(fn(css(data(core()))))))

    ripple('foo-bar-1', function() { this.innerHTML += 1 })
    ripple('featurable1', function() { this.innerHTML += 2 })

    time(40, d => expect(el.innerHTML).to.be.eql('12'))
    time(50, done)
  })

  it('should extend component with multiple features', done => {
    container.innerHTML = '<foo-bar-2 is="featurable2 featurable3">'

    var el = container.firstElementChild
      , ripple = features(precss(components(fn(css(data(core()))))))

    ripple('foo-bar-2', function() { this.innerHTML += 1 })
    ripple('featurable2', function() { this.innerHTML += 2 })
    ripple('featurable3', function() { this.innerHTML += 3 })

    time(40, d => expect(el.innerHTML).to.be.eql('123'))
    time(50, done)
  })

  it('should extend component with feature css', done => {
    container.innerHTML = '<foo-bar-3 is="featurable4">'

    var el = container.firstElementChild
      , ripple = features(precss(components(fn(css(data(core()))))))

    ripple('foo-bar-3', noop)
    ripple('featurable4.css', ':host {}')
    ripple('featurable4', noop, { needs: '[css]' })

    time(40, d => {
      expect(includes('is="featurable4"')(el.outerHTML)).to.be.ok
      expect(includes('css="featurable4.css"')(el.outerHTML)).to.be.ok
      expect(raw('style', head).innerHTML.trim()).to.equal('[css~="featurable4.css"] {}')
    })
    time(50, done)
  })

  it('should not apply feature if base component not run - 1', done => {
    container.innerHTML = '<foo-bar-5 is="featurable5">'

    var el = container.firstElementChild
      , ripple = features(needs(precss(components(fn(css(data(core())))))))
      , result

    ripple('foo-bar-5', noop, { needs: '[css]' })
    ripple('featurable5', d => result = true)

    time(40, d => {
      expect(includes('is="featurable5"')(el.outerHTML)).to.be.ok
      expect(includes('css="foo-bar-5.css"')(el.outerHTML)).to.be.ok
      expect(result).to.not.be.ok
      done()
    })
  })

  it('should not apply feature if base component not run - 2', done => {
    container.innerHTML = '<foo-bar-6 is="featurable6">'

    var el = container.firstElementChild
      , ripple = needs(precss(features(components(fn(css(data(core())))))))
      , result

    ripple('foo-bar-6', noop, { needs: '[css]' })
    ripple('featurable6', d => result = true)

    time(40, d => {
      expect(includes('is="featurable6"')(el.outerHTML)).to.be.ok
      expect(includes('css="foo-bar-6.css"')(el.outerHTML)).to.be.ok
      expect(result).to.not.be.ok
      done()
    })  
  })

})