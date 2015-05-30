using System.Web.Optimization;

namespace Bow.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            #region Bundles para la Aplicación Angular (Páginas privadas despues de haberse logueado)

            //Bootstrap Styles
            bundles.Add(new StyleBundle("~/bundles/BootstrapStyles").Include(
                "~/Content/bootstrap.css"));

            //Application Styles
            bundles.Add(new StyleBundle("~/bundles/ApplicationStyles").Include(
                "~/Content/styles.css"));

            //ABP Styles -> Se estan agregando los de abp que no están en NAUT Se debe verificar si esto se puede eliminar.
            bundles.Add(new StyleBundle("~/bundles/ABPStyles").Include(
                "~/Content/toastr.min.css",
                "~/Content/flags/famfamfam-flags.css",
                "~/Content/font-awesome.min.css",
                "~/Scripts/vendor/feather/webfont/feather-webfont/feather.css",
                "~/App/Main/styles/bow.css"));

            //Scripts de NAUT
            bundles.Add(new ScriptBundle("~/bundles/ApplicationScripts")
                .Include("~/Scripts/naut/app.module.js")
                .IncludeDirectory("~/Scripts/naut/modules", "*.js", true)
            );


            //Vendor Scripts de Naut
            bundles.Add(new ScriptBundle("~/bundles/VendorScripts").Include(
              "~/Scripts/vendor/jquery/dist/jquery.js",
              "~/Scripts/vendor/angular/angular.js",
              "~/Scripts/vendor/angular-animate/angular-animate.js",
              "~/Scripts/vendor/angular-bootstrap/ui-bootstrap-tpls.js",
              "~/Scripts/vendor/angular-cookies/angular-cookies.js",
              "~/Scripts/vendor/angular-dynamic-locale/dist/tmhDynamicLocale.js",
              "~/Scripts/vendor/angular-loading-bar/build/loading-bar.js",
              "~/Scripts/vendor/angular-resource/angular-resource.js",
              "~/Scripts/vendor/angular-route/angular-route.js",
              "~/Scripts/vendor/angular-sanitize/angular-sanitize.js",
              "~/Scripts/vendor/angular-touch/angular-touch.js",
              "~/Scripts/vendor/angular-translate/angular-translate.js",
              "~/Scripts/vendor/angular-translate-loader-static-files/angular-translate-loader-static-files.js",
              "~/Scripts/vendor/angular-translate-loader-url/angular-translate-loader-url.js",
              "~/Scripts/vendor/angular-translate-storage-cookie/angular-translate-storage-cookie.js",
              "~/Scripts/vendor/angular-translate-storage-local/angular-translate-storage-local.js",
              "~/Scripts/vendor/angular-ui-router/release/angular-ui-router.js",
              "~/Scripts/vendor/angular-ui-utils/ui-utils.js",
              "~/Scripts/vendor/modernizr/modernizr.js",
              "~/Scripts/vendor/ngstorage/ngStorage.js",
              "~/Scripts/vendor/oclazyload/dist/ocLazyLoad.js",
              "~/Scripts/vendor/flot/jquery.flot.js",
              "~/Scripts/vendor/flot/jquery.flot.canvas.js",
              "~/Scripts/vendor/flot/*.js",
              "~/Scripts/vendor/flot-spline/js/*.js",
              "~/Scripts/vendor/flot.tooltip/js/*.js",
              "~/Scripts/vendor/slimscroll/*.js",
              "~/Scripts/vendor/sparklines/jquery.sparkline.min.js",
              "~/Scripts/vendor/ika.jvectormap/*.js"));

            //VENDOR RESOURCES

            //~/Bundles/App/vendor/css
            //Se adiciona bow-theme como tema de estilos para bow. El tema se desarrolla compilando las fuentes de bootstrap
            //y sobrescribiendo los elementos que se requiera cambiar.
            //Para asegurar que siempre se tomen los estilos nuevos, se debe dejar de utilizar las librerías core de bootstrap
            // y los temas que boilerplate trae por defecto.
            //bundles.Add(
            //    new StyleBundle("~/bundles/App/vendor/css")
            //        .Include(
            //            "~/Content/themes/base/all.css",
            //            // "~/Content/bootstrap.min.css",                         
            //           //"~/Content/bootstrap-theme.min.css",
            //           // "~/Content/bootstrap-cosmo.min.css",
            //           "~/Content/bow-theme.css",
            //            "~/Content/toastr.min.css",
            //            "~/Content/flags/famfamfam-flags.css",
            //            "~/Content/font-awesome.min.css"
            //        )
            //    );

            //~/Bundles/App/vendor/js
            bundles.Add(new ScriptBundle("~/bundles/AbpVendorScripts").Include(
                        "~/Abp/Framework/scripts/utils/ie10fix.js",
                        "~/Scripts/json2.min.js",
                        "~/Scripts/jquery-ui.min-1.11.1.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/jquery.blockUI.min.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",
                        "~/Scripts/ng-file-upload-shim.min.js",
                        "~/Scripts/ng-file-upload.min.js",
                        "~/Scripts/angular-ui/ui-bootstrap.min.js",

                        "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/AbpScripts").Include(
                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/angularjs/abp.ng.js"));

            ////~/Bundles/App/vendor/js
            //bundles.Add(
            //    new ScriptBundle("~/Bundles/App/vendor/js")
            //        .Include(
            //            "~/Abp/Framework/scripts/utils/ie10fix.js",
            //            "~/Scripts/json2.min.js",

            //            "~/Scripts/modernizr-2.8.3.js",

            //            "~/Scripts/jquery-2.1.1.min.js",
            //            "~/Scripts/jquery-ui.min-1.11.1.js",

            //            "~/Scripts/bootstrap.min.js",

            //            "~/Scripts/moment-with-locales.min.js",
            //            "~/Scripts/jquery.blockUI.min.js",
            //            "~/Scripts/toastr.min.js",
            //            "~/Scripts/others/spinjs/spin.js",
            //            "~/Scripts/others/spinjs/jquery.spin.js",

            //            "~/Scripts/angular.min.js",
            //            "~/Scripts/angular-animate.min.js",
            //            "~/Scripts/angular-sanitize.min.js",
            //            "~/Scripts/angular-ui-router.min.js",
            //            "~/Scripts/angular-ui/ui-bootstrap.min.js",
            //            "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
            //            "~/Scripts/angular-ui/ui-utils.min.js",

            //            "~/Abp/Framework/scripts/abp.js",
            //            "~/Abp/Framework/scripts/libs/abp.jquery.js",
            //            "~/Abp/Framework/scripts/libs/abp.toastr.js",
            //            "~/Abp/Framework/scripts/libs/abp.blockUI.js",
            //            "~/Abp/Framework/scripts/libs/abp.spin.js",
            //            "~/Abp/Framework/scripts/libs/angularjs/abp.ng.js"
            //        )
            //    );

            //APPLICATION RESOURCES

            //~/Bundles/App/Main/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/Main/js")
                    .IncludeDirectory("~/App/Main", "*.js", true)
                );

            #endregion

            #region Bundles para la Aplicación MVC (Páginas publicas que pueden accederse sin haberse logueado)

            bundles.Add(
                new StyleBundle("~/bundles/public/css")
                    .Include("~/Content/bootstrap.css",
                             "~/Content/styles.css",
                             "~/Content/toastr.min.css",
                             "~/Content/flags/famfamfam-flags.css",
                             "~/Content/font-awesome.min.css",
                             "~/Scripts/vendor/feather/webfont/feather-webfont/feather.css"));


            bundles.Add(
                new ScriptBundle("~/bundles/public/js/top")
                    .Include("~/Abp/Framework/scripts/utils/ie10fix.js",
                             "~/Scripts/vendor/modernizr/modernizr.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/public/js/bottom")
                    .Include("~/Scripts/json2.min.js",
                             "~/Scripts/vendor/jquery/dist/jquery.js",
                             "~/Scripts/jquery-ui.min-1.11.1.js",
                             "~/Scripts/bootstrap.min.js",
                             "~/Scripts/moment-with-locales.min.js",
                             "~/Scripts/jquery.blockUI.min.js",
                             "~/Scripts/toastr.min.js",
                             "~/Scripts/others/spinjs/spin.js",
                             "~/Scripts/others/spinjs/jquery.spin.js",
                             "~/Abp/Framework/scripts/abp.js",
                             "~/Abp/Framework/scripts/libs/abp.jquery.js",
                             "~/Abp/Framework/scripts/libs/abp.toastr.js",
                             "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                             "~/Abp/Framework/scripts/libs/abp.spin.js"
                             ));

            #endregion


        }
    }
}