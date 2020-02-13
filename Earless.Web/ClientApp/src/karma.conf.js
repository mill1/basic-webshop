// Karma configuration file, see link for more information 
// https://karma-runner.github.io/1.0/config/configuration-file.html 

process.env.CHROME_BIN = require('puppeteer').executablePath(); //needed for browser automation and chrome headless: https://www.sitepen.com/blog/2017/10/04/browser-automation-with-puppeteer/ 

module.exports = function (config) {
    config.set({
        //Setup a custom chrome 
        customLaunchers: {
            ChromeHeadless: {
                base: 'Chrome',
                flags: [
                    '--headless',
                    '--disable-gpu',
                    // Without a remote debugging port, Google Chrome exits immediately. 
                    '--remote-debugging-port=9222',
                ],
            }
        },
        basePath: '',
        frameworks: ['jasmine', '@angular-devkit/build-angular'],
        plugins: [
            require('karma-jasmine'),
            require('karma-chrome-launcher'),
            require('karma-jasmine-html-reporter'),
            require('karma-junit-reporter'), // add karma-unit reporter 
            require('karma-coverage-istanbul-reporter'),
            require('@angular-devkit/build-angular/plugins/karma')
        ],
        client: {
            clearContext: false // leave Jasmine Spec Runner output visible in browser 
        },
        coverageIstanbulReporter: {
            dir: require('path').join(__dirname, 'coverage'), reports: ['html', 'lcovonly'],
            fixWebpackSourcePaths: true
        },
        angularCli: {
            environment: 'dev'
        },
        reporters: ['progress', 'kjhtml', 'junit'],
        junitReporter: {
            outputDir: '../../', // results will be saved as $outputDir/$browserName.xml 
            outputFile: 'test.xml', // if included, results will be saved as $outputDir/$browserName/$outputFile 
            useBrowserName: false, // add browser name to report and classes names 
        },
        port: 9876,
        colors: true,
        logLevel: config.LOG_INFO,
        autoWatch: true,
        browsers: ['Chrome', 'ChromeHeadless'],
        singleRun: false
    });
};
