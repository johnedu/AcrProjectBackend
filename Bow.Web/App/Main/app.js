(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',
        'ui.router',
        'ui.bootstrap',
        'ui.jq',
        'naut',
        'abp',
        'ngFileUpload'
    ]);

    //Configuración de parámetros de paginación por defecto
    app.run(function (paginationConfig) {
        paginationConfig.maxSize = 5;
        paginationConfig.rotate = false;
        paginationConfig.boundaryLinks = true;
        paginationConfig.firstText = "<<";
        paginationConfig.previousText = "<";
        paginationConfig.nextText = ">";
        paginationConfig.lastText = ">>";
    });

    //Configuración de parámetros de datepicker por defecto
    app.run(function (datepickerConfig) {
        datepickerConfig.startingDay = 1;
        datepickerConfig.showWeeks = false;
    });
    app.run(function (datepickerPopupConfig) {
        datepickerPopupConfig.showButtonBar = false;
    });

    //Configuration for Angular UI routing.
    app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

        //ruta por defecto
            $urlRouterProvider.otherwise('/');

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/App/Main/views/home/home.cshtml',
                    menu: 'Home' //Matches to name of 'Home' menu in BowNavigationProvider
                })
                .state('index', {
                    url: '/index',
                    templateUrl: '/App/Main/views/administracion/index.cshtml',
                    menu: 'index'
                })
                .state('preguntasFrecuentes', {
                    url: '/administracion/preguntasFrecuentes',
                    templateUrl: '/App/Main/views/administracion/preguntasFrecuentes/preguntasFrecuentes.cshtml',
                    menu: 'menu_administracion_preguntasFrecuentes'
                })
                .state('preguntasJuegos', {
                    url: '/administracion/preguntasJuegos',
                    templateUrl: '/App/Main/views/administracion/preguntasJuegos/preguntasJuegos.cshtml',
                    menu: 'menu_administracion_preguntasJuegos'
                })
                .state('entidadesDimensiones', {
                    url: '/administracion/entidadesDimensiones',
                    templateUrl: '/App/Main/views/administracion/entidadesDimensiones/entidadesDimensiones.cshtml',
                    menu: 'menu_administracion_entidadesDimensiones'
                })
                .state('about', {
                    url: '/about',
                    templateUrl: '/App/Main/views/about/about.cshtml',
                    menu: 'About' //Matches to name of 'About' menu in BowNavigationProvider
                })

            /*$stateProvider.when('departamentos', {
                url: '/zonificacion/pais/:paisId'
            });*/


        }
    ]);
})();

