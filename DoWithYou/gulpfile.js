/// <binding BeforeBuild='clean' AfterBuild='min' Clean='clean, min' />
'use strict';

var gulp = require('gulp'),
    rimraf = require('rimraf'),
    concat = require('gulp-concat'),
    cssmin = require('gulp-cssmin'),
    uglify = require('gulp-uglify'),
    _ = require('lodash');

var webroot = './wwwroot/',
    libRoot = webroot + 'lib/',
    jsRoot = webroot + 'js/',
    cssRoot = webroot + 'css/',
    jsExt = '*.js',
    jsMinExt = '*.min.js',
    cssExt = '*.css',
    cssMinExt = '*.min.css',
    mapExt = '*.map';

var paths = {
    js: jsRoot + '**/' + jsExt,
    minJs: jsRoot + '**/' + jsMinExt,
    css: cssRoot + '**/' + cssExt,
    minCss: cssRoot + '**/' + cssMinExt,
    concatJsDest: jsRoot + 'site.min.js',
    concatCssDest: cssRoot + 'site.min.css'
};

gulp.task('copy-assets',
    function () {
        var assetRoot = './node_modules/';

        // All assets
        var assets = {
            js: [
                assetRoot + '**/dist/' + jsExt,
                assetRoot + '**/dist/js/' + jsExt,
                assetRoot + '**/bundles/' + jsExt,
                assetRoot + '**/lib/' + jsExt
            ],
            css: [
                assetRoot + '**/dist/' + cssExt,
                assetRoot + '**/dist/css/' + cssExt,
                assetRoot + '**/bundles/' + cssExt,
                assetRoot + '**/lib/' + cssExt
            ],
            map: [
                assetRoot + '**/dist/' + mapExt,
                assetRoot + '**/dist/js/' + mapExt,
                assetRoot + '**/dist/css/' + mapExt,
                assetRoot + '**/bundles/' + mapExt,
                assetRoot + '**/lib/' + mapExt
            ]
        };

        _(assets).forEach(function (asset, type) {
            gulp.src(asset)
                .pipe(gulp.dest(libRoot));
        });
    });

gulp.task('clean:js',
    function(cb) {
        rimraf(paths.concatJsDest, cb);
    });

gulp.task('clean:css',
    function(cb) {
        rimraf(paths.concatCssDest, cb);
    });

gulp.task('clean', ['clean:js', 'clean:css']);

gulp.task('min:js',
    function() {
        return gulp.src([paths.js, '!' + paths.minJs], { base: '.' })
            .pipe(concat(paths.concatJsDest))
            .pipe(uglify())
            .pipe(gulp.dest('.'));
    });

gulp.task('min:css',
    function() {
        return gulp.src([paths.css, '!' + paths.minCss])
            .pipe(concat(paths.concatCssDest))
            .pipe(cssmin())
            .pipe(gulp.dest('.'));
    });

gulp.task('min', ['min:js', 'min:css']);