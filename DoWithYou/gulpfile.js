/// <binding BeforeBuild='clean' AfterBuild='copy-assets, min' Clean='clean' />
'use strict';

// Includes
var gulp = require('gulp'),
    rimraf = require('rimraf'),
    concat = require('gulp-concat'),
    cssmin = require('gulp-cssmin'),
    uglify = require('gulp-uglify'),
    sass = require('gulp-sass'),
    _ = require('lodash');

var roots = {
    web: './wwwroot/',
    node: './node_modules/'
};

var branches = {
    lib: roots.web + 'lib/',
    js: roots.web + 'js/',
    css: roots.web + 'css/',
    scss: roots.web + 'css/' + 'scss/'
};

var extensions = {
    js: '*.js',
    jsMin: '*.min.js',
    css: '*.css',
    cssMin: '*.min.css',
    scss: '*.scss',
    map: '*.map'
};

var paths = {
    js: branches.js + '**/' + extensions.js,
    minJs: branches.js + '**/' + extensions.jsMin,
    css: branches.css + '**/' + extensions.css,
    scss: branches.scss + '**/' + extensions.scss,
    minCss: branches.css + '**/' + extensions.cssMin,
    sassCssDest: roots.web + 'css',
    concatJsDest: branches.js + 'site.min.js',
    concatCssDest: branches.css + 'site.min.css'
};

gulp.task('clean:lib:dist',
    function(cb) {
        var path = branches.lib + '**/dist/**/*.*';
        console.log('rimraf: ' + path);
        rimraf(path, cb);
    });

gulp.task('clean:lib:bundles',
    function(cb) {
        var path = branches.lib + '**/bundles/**/*.*';
        console.log('rimraf: ' + path);
        rimraf(path, cb);
    });

gulp.task('clean:lib:lib',
    function(cb) {
        var path = branches.lib + '**/lib/**/*.*';
        console.log('rimraf: ' + path);
        rimraf(path, cb);
    });

gulp.task('clean:lib',
    ['clean:lib:dist', 'clean:lib:bundles', 'clean:lib:lib'],
    function(cb) {
        console.log('rimraf: ' + branches.lib);
        rimraf(branches.lib, cb);
    });

gulp.task('copy-assets',
    function () {
        var assets = {
            js: [
                roots.node + '**/' + extensions.js,
                roots.node + '**/' + extensions.jsMin
            ],
            css: [
                roots.node + '**/' + extensions.css,
                roots.node + '**/' + extensions.cssMin
            ],
            map: [
                roots.node + '**/' + extensions.map
            ]
        };

        var excludes = [
            '!' + roots.node + '**/' + 'node_modules/**/*.*',
            '!' + roots.node + '**/' + 'src/**/*.*',
            '!' + roots.node + '**/' + 'example/**/*.*',
            '!' + roots.node + '**/' + 'examples/**/*.*',
            '!' + roots.node + '**/' + 'test/**/*.*',
            '!' + roots.node + '**/' + 'locale/**/*.*'
        ];

        _(assets).forEach(function (asset, type) {
            var final = asset.concat(excludes);
            console.log('Pipe: ' + final);
            gulp.src(final)
                .pipe(gulp.dest(branches.lib));
        });
    });

gulp.task('clean:js',
    function (cb) {
        console.log('rimraf: ' + paths.concatJsDest);
        rimraf(paths.concatJsDest, cb);
    });

gulp.task('clean:css',
    function (cb) {
        console.log('rimraf: ' + paths.concatCssDest);
        rimraf(paths.concatCssDest, cb);
    });

gulp.task('clean', ['clean:js', 'clean:css', 'clean:lib']);

gulp.task('sass',
    function () {
        console.log('Sass: ' + paths.scss + ' -> ' + paths.sassCssDest);
        return gulp.src(paths.scss)
            .pipe(sass().on('error', sass.logError))
            .pipe(gulp.dest(paths.sassCssDest));
    });

gulp.task('min:js',
    function () {
        console.log('Minify: ' + paths.js);
        return gulp.src([paths.js, '!' + paths.minJs], { base: '.' })
            .pipe(concat(paths.concatJsDest))
            .pipe(uglify())
            .pipe(gulp.dest('.'));
    });

gulp.task('min:css', ['sass'],
    function () {
        console.log('Minify: ' + paths.css);
        return gulp.src([paths.css, '!' + paths.minCss])
            .pipe(concat(paths.concatCssDest))
            .pipe(cssmin())
            .pipe(gulp.dest('.'));
    });

gulp.task('min', ['min:js', 'min:css']);