import commonjs from 'rollup-plugin-commonjs'
import nodeResolve from 'rollup-plugin-node-resolve'
import buble from 'rollup-plugin-buble'

export default {
  entry: 'client.js'
, dest: 'client.bundle.js'
, format: 'iife'
, moduleName: 'sync'
, plugins: [
    nodeResolve({ browser: true })
  , commonjs()
  , buble()
  ]
}