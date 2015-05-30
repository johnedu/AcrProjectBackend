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
                .state('develop', {
                    url: '/develop',
                    templateUrl: '/App/Main/views/home/develop.cshtml',
                    menu: 'Desarrollo'
                })
                .state('zonificacionPais', {
                    url: '/administracion/zonificacion/:paisId',
                    templateUrl: '/App/Main/views/zonificacion/departamento/departamento.cshtml',
                    menu: 'menu_administracion_zonificacion'
                })
               .state('zonificacionLocalidad', {
                   url: '/administracion/zonificacion/localidad/:departamentoId',
                   templateUrl: '/App/Main/views/zonificacion/localidad/localidad.cshtml',
                   menu: 'menu_administracion_zonificacion'
               })
                 .state('parametro', {
                     url: '/administracion/parametro',
                     templateUrl: '/App/Main/views/parametro/parametro/parametro.cshtml',
                     menu: 'menu_administracion_administracionParametros'
                 })
                .state('tiposestados', {
                    url: '/administracion/parametro/tiposEstados/:parametroId',
                    templateUrl: '/App/Main/views/parametro/tiposEstados/tiposestados.cshtml',
                    menu: 'menu_administracion_administracionParametros'
                })
                .state('parametrosLocalidad', {
                    url: '/administracion/zonificacion/parametroslocalidad/:localidadId',
                    templateUrl: '/App/Main/views/zonificacion/localidad/parametrosLocalidad.cshtml',
                    menu: 'menu_administracion_zonificacion'
                })
                .state('zonasLocalidad', {
                    url: '/administracion/zonificacion/zona/:localidadId',
                    templateUrl: '/App/Main/views/zonificacion/zona/zona.cshtml',
                    menu: 'menu_administracion_zonificacion'
                })
                .state('tiposDocumento', {
                    url: '/administracion/tipos-documento',
                    templateUrl: '/App/Main/views/personas/tiposDocumento/tiposDocumento.cshtml',
                    menu: 'menu_administracion_tiposDocumento' //Matches to name of 'Home' menu in BowNavigationProvider
                })
                .state('pais', {
                    url: '/administracion/zonificacion',
                    templateUrl: '/App/Main/views/zonificacion/pais/pais.cshtml',
                    menu: 'menu_administracion_zonificacion' //Matches to name of 'Home' menu in BowNavigationProvider
                })
                .state('about', {
                    url: '/about',
                    templateUrl: '/App/Main/views/about/about.cshtml',
                    menu: 'About' //Matches to name of 'About' menu in BowNavigationProvider
                })
                 .state('preferencias', {
                     url: '/administracion/personas/preferencia',
                     templateUrl: '/App/Main/views/personas/preferencia/preferencias.cshtml',
                     menu: 'menu_administracion_preferencias'
                 })
                .state('actividadesEconomicas', {
                    url: '/administracion/empresas/actividadEconomica',
                    templateUrl: '/App/Main/views/empresas/actividadEconomica/actividadEconomica.cshtml',
                    menu: 'menu_administracion_actividadesEconomicas'
                })
                .state('infoTributaria', {
                    url: '/administracion/empresas/infoTributaria',
                    templateUrl: '/App/Main/views/empresas/infoTributaria/infoTributaria.cshtml',
                    menu: 'menu_administracion_info_tributaria'
                })
                .state('empresas', {
                    url: '/administracion/empresas/empresa',
                    templateUrl: '/App/Main/views/empresas/empresa/empresa.cshtml',
                    menu: 'menu_administracion_empresas'
                })

                .state('empleados', {
                    url: '/administracion/empleados/empleado',
                    templateUrl: '/App/Main/views/empleados/empleado/empleado.cshtml',
                    menu: 'menu_administracion_personal'
                })

                .state('planExequial', {
                    url: '/afiliaciones/plan-exequial',
                    templateUrl: '/App/Main/views/afiliaciones/planExequial/planExequial.cshtml',
                    menu: 'menu_afiliaciones_planExequial'
                })
                .state('beneficios', {
                    url: '/afiliaciones/beneficio',
                    templateUrl: '/App/Main/views/afiliaciones/beneficio/beneficio.cshtml',
                    menu: 'menu_comercial_beneficio'
                })
                .state('detallePlanExequial', {
                    url: '/afiliaciones/plan-exequial/:planId',
                    templateUrl: '/App/Main/views/afiliaciones/planExequial/detallePlanExequial.cshtml',
                    menu: 'menu_afiliaciones_detallePlanExequial'
                })
                .state('gestionarPeriodosVenta', {
                    url: '/afiliaciones/periodosVenta',
                    templateUrl: '/App/Main/views/afiliaciones/periodosVenta/gestionarPeriodosVenta.cshtml',
                    menu: 'menu_afiliacion_gestionarPeriodosVenta'
                })
                .state('clienteProspecto', {
                    url: '/afiliaciones/clienteProspecto',
                    templateUrl: '/App/Main/views/afiliaciones/clienteProspecto/clienteProspecto.cshtml',
                    menu: 'menu_afiliacion_gestionarClienteProspecto'
                })
                .state('grupoInformal', {
                    url: '/afiliaciones/grupoInformal',
                    templateUrl: '/App/Main/views/afiliaciones/grupoInformal/grupoInformal.cshtml',
                    menu: 'menu_afiliacion_grupoInformal'
                })
                .state('gestionarParentescos', {
                    url: '/afiliaciones/parentesco',
                    templateUrl: '/App/Main/views/afiliaciones/parentesco/parentesco.cshtml',
                    menu: 'menu_afiliacion_gestionarParentescos'
                })
                .state('infoAfiliacionVenta', {
                    url: '/afiliaciones/infoAfiliacionVenta',
                    templateUrl: '/App/Main/views/afiliaciones/infoAfiliacionVenta/infoAfiliacionVenta.cshtml',
                    menu: 'menu_afiliacion_infoAfiliacionVenta'
                })
                .state('seguridad', {
                    url: '/seguridad/prueba',
                    templateUrl: '/App/Main/views/seguridad/Prueba/pruebaSeguridad.cshtml',
                    menu: 'menu_seguridad'
                })

            /*$stateProvider.when('departamentos', {
                url: '/zonificacion/pais/:paisId'
            });*/


        }
    ]);
})();

