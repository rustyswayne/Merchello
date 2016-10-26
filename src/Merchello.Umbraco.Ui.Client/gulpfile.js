var gulp = require('gulp');

var pkg = require('./package.json');

// build configuration
var debug = 'bin/Debug/';
var release = 'bin/Release/';

// source locations
var versionText = '../../build/MerchelloVersion.txt'
var allSrc = 'src/**/*.js';
var configs = 'src/config/';
var dist = './build/App_Plugins/Merchello/';
var tests = '../../tests/Merchello.Tests.Unit/';
var umbTests = '../../tests/Merchello.Tests.Umbraco/';
var concatenated = [];


// cleans the build directory
gulp.task('clean', function() {
    var del = require('del');
    return del([
       dist
    ]);

    /*
     //'dist/report.csv',
      here we use a globbing pattern to match everything inside the `mobile` folder
     'dist/mobile/ ** / *',
     we don't want to clean this file though so we negate the pattern
    '!dist/mobile/deploy.json'
    E*/
});

// copies the config files to the tests directory to ensure current
gulp.task('copy:tests', function() {
    // todo addd changed

    var copy = require('gulp-copy');
    var rename = require('gulp-rename');
    var fs = require('fs');
    var xmlpoke = require('gulp-xmlpoke');

    var paths = {
        settings: 'Configurations/MerchelloSettings/',
        extensibility: 'Configurations/ExtensibilitySettings/',
        countries: 'Configurations/CountrySettings/',
        umbraco: 'Config/'
    }

    var utdebug = umbTests + debug;
    var utrelease = umbTests + release;
    var tdebug = tests + debug;
    var trelease = tests + release;

    /// ------------------
    //  Umbraco
    /// ------------------
    gulp.src(configs + '/log4net/log4net.config')
        .pipe(gulp.dest(utdebug + paths.umbraco))
        .pipe(gulp.dest(utrelease + paths.umbraco));

    // -------------------
    // setttings
    // -------------------
    // merchelloSettings.config

    var version;

    gulp.src(configs + 'merchelloSettings.config')
        // set the version to the build version
        .pipe(xmlpoke({
                replacements: [{
                    xpath: "//merchelloConfigurationStatus",
                    value: function (node) {
                       // get the contents of the version file in the build directory
                        var data =  fs.readFileSync(versionText, "utf-8");

                        var lines = data.split('\n');
                        version = clean(lines[1]);
                        if (lines.length > 2) {
                            version += '-' + clean(lines[2]);
                        }

                        return version;

                        // remove the carage returns
                        function clean(item) {
                            return item.replace(/(\r\n|\n|\r)/gm,"");
                        }
                    }
                }]
            }))
        .pipe(gulp.dest(tdebug + paths.settings))
        .pipe(gulp.dest(trelease + paths.settings))
        .pipe(gulp.dest(utdebug + paths.umbraco))
        .pipe(gulp.dest(utrelease + paths.umbraco));



    // web.config
    gulp.src(configs + 'tests/web.settings.config')
        .pipe(rename('web.config'))
        .pipe(gulp.dest(tdebug + 'Configurations/MerchelloSettings/'))
        .pipe(gulp.dest(trelease + 'Configurations/MerchelloSettings/'));

    // -------------------
    // extensibility
    // -------------------
    gulp.src(configs + 'merchelloExtensibility.config')
        .pipe(gulp.dest(tdebug + 'Configurations/MerchelloExtensibility/'))
        .pipe(gulp.dest(trelease + 'Configurations/MerchelloExtensibility/'))
        .pipe(gulp.dest(utdebug + paths.umbraco))
        .pipe(gulp.dest(utrelease + paths.umbraco));

    // web.config
    gulp.src(configs + 'tests/web.extensibility.config')
        .pipe(rename('web.config'))
        .pipe(gulp.dest(tdebug + 'Configurations/MerchelloExtensibility/'))
        .pipe(gulp.dest(trelease + 'Configurations/MerchelloExtensibility/'));

    // -------------------
    // countries
    // -------------------
    gulp.src(configs + 'merchelloCountries.config')
        .pipe(gulp.dest(tdebug + 'Configurations/MerchelloCountries/'))
        .pipe(gulp.dest(trelease + 'Configurations/MerchelloCountries/'))
        .pipe(gulp.dest(utdebug + paths.umbraco))
        .pipe(gulp.dest(utrelease + paths.umbraco));

    // web.config
    gulp.src(configs + 'tests/web.countries.config')
        .pipe(rename('web.config'))
        .pipe(gulp.dest(tdebug + 'Configurations/MerchelloCountries/'))
        .pipe(gulp.dest(trelease + 'Configurations/MerchelloCountries/'));

    /// -------------------
    /// Umbraco
    /// -------------------
    gulp.src(configs + 'tests/web.full.config')
        .pipe(rename('web.config'))
        .pipe(gulp.dest(utdebug + 'Config/'))
        .pipe(gulp.dest(trelease + 'Config/'));
});



/********************************************************
*    UTILITY
*********************************************************/

// Code checker that finds common mistakes in scripts
gulp.task('lint', function() {
    var jshint = require('gulp-jshint');
    var jshintStylish = require('jshint-stylish');

    gulp.src(allSrc)
        .pipe(jshint())
        .pipe(jshint.reporter(jshintStylish));
});

// Logs out the total size of files in the stream and optionally the individual file-sizes
gulp.task('size', function () {
    var size = require('gulp-size');

    gulp.src(concatenated)
        .pipe(size({ showFiles: true }));
});


gulp.task('compress', function () {
    var closureCompiler = require('gulp-closure-compiler');
    var bytediff = require('gulp-bytediff');

    gulp.src(concatenated)
        .pipe(bytediff.start())
        .pipe(closureCompiler())
        .pipe(bytediff.stop())
        .pipe(gulp.dest(dist + 'compressed'));
});

// not used
gulp.task('uglify', function() {
    var uglify = require('gulp-uglify');
    var bytediff = require('gulp-bytediff');

    gulp.src(sources)
        .pipe(bytediff.start())
        .pipe(uglify())
        .pipe(bytediff.stop())
        .pipe(gulp.dest(dist + 'uglified'));
});

gulp.task('minify', function() {
    var uglify = require('gulp-uglify');
    var closureCompiler = require('gulp-closure-compiler');
    var bytediff = require('gulp-bytediff');

    gulp.src(concatenated)
        .pipe(bytediff.start())
        .pipe(closureCompiler())
        .pipe(uglify())
        .pipe(bytediff.stop())
        .pipe(gulp.dest(dist + 'minified'));
});


// javascript checker
gulp.task('jscs', function () {
    var jscs = require('gulp-jscs');
    gulp.src(allSrc)
        .pipe(jscs());
});

// adds the header
gulp.task('header', function () {
    var header = require('gulp-header');
    gulp.src(concatenated)
        .pipe(header('This is a header for ${name}!\n', { name : 'gulp test'} ))
        .pipe(gulp.dest(dist + 'header'));
});

// sloc is a simple tool to count SLOC (source lines of code)
gulp.task('sloc', function(){
    var sloc = require('gulp-sloc');

    gulp.src(allSources)
        .pipe(sloc());
});

/********************************************************
 *    USE
 *    ?> gulp test      -- not implemented
 *    ?> gulp dev
 *    ?> gulp release   -- not implemented
 *    ?> gulp
 *********************************************************/

gulp.task('dev', ['copy:tests'], function() {

});

gulp.task('default', ['clean', 'copy:tests'], function() {
    // place code for your default task here


    console.info('Default');

});


