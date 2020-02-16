/// <binding BeforeBuild='compile-css' />
// https://medium.com/@georgemr/tailwind-css-in-blazor-asp-net-core-app-with-grunt-24a627cd9f3a

module.exports = function (grunt) {
    grunt.initConfig({
        // get the configuration info from package.json
        pkg: grunt.file.readJSON('package.json'),

        // create a clean task to remove production resource files under wwwroot
        clean: ["wwwroot/css/*"],

        // Watch CSS files and run compile-css on change
        watch: {
            postcss: {
                files: 'src/css/**/*.css',
                tasks: ['compile-css'],
                options: {
                    interrupt: true
                }
            }
        },
        
        // PostCSS - Tailwindcss and Autoprefixer
        postcss: {
            options: {
                map: true, // inline sourcemaps
                processors: [
                    require('tailwindcss')(),
                    require('autoprefixer') // add vendor prefixes
                ] 
            },
            dist: {
                expand: true,
                cwd: 'src/css/',
                src: ['*.css'],
                dest: 'wwwroot/css/',
                ext: '.css'
            }
        }
    });

    grunt.registerTask('compile-css', ['clean', 'postcss']);

    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('watch-css', ['watch:postcss']);

    // Load Grunt Plugins
    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks("grunt-postcss");
};