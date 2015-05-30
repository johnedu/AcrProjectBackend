/**=========================================================
 * Module: VendorAssetsConstant.js
 =========================================================*/

(function() {
    'use strict';

    angular
        .module('naut')
        .constant('VENDOR_ASSETS', {
            // jQuery based and standalone scripts
            scripts: {
              'animate':            ['/Scripts/vendor/animate.css/animate.min.css'],
              'icons':              ['/Scripts/vendor/font-awesome/css/font-awesome.min.css',
                                     '/Scripts/vendor/weather-icons/css/weather-icons.min.css',
                                     '/Scripts/vendor/feather/webfont/feather-webfont/feather.css']
            },
            // Angular modules scripts (name is module name to be injected)
            modules: [
              {name: 'toaster',           files: ['/Scripts/vendor/angularjs-toaster/toaster.js',
                                                  '/Scripts/vendor/angularjs-toaster/toaster.css']
              }
            ]

        });

})();

