(function () {
    angular.module('app')
        .directive("bowBuscarGrupoInformal", function () {
            return {
                restrict: 'E',
                scope: {
                    selected: "=ngModel",
                    notificarSeleccion: "&bowSeleccionoGrupo",
                    requerido: "@ngRequired",
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.recargarListadoGruposInformales = function () {
                        scope.recargarListadoGruposInformales();
                    };
                    scope.internalControl.asignarGrupoSeleccionado = function (grupoInformalId) {
                        scope.asignarGrupoSeleccionado(grupoInformalId);
                    };
                    scope.internalControl.obtenerGrupoInformalSeleccionado = function () {
                        scope.obtenerGrupoInformalSeleccionado();
                    };
                },
                controller: ['$scope', 'abp.services.app.empresas', 'abp.services.app.afiliaciones', 'abp.services.app.personas', function ($scope, empresasService, afiliacionesService, personasService) {

                    $scope.controlBuscarNombreGrupoInformal = {};

                    $scope.formaBusqueda = 'nombre';
                    $scope.listadoGruposInformales = [];

                    $scope.limpiarFormulario = function () {
                        $scope.nombreGrupoInformal = '';
                        $scope.cedulaContacto = '';
                        $scope.listadoGruposInformales = [];
                    };

                    $scope.seleccionoGrupoInformal = function (grupoInformal) {
                        $scope.notificarSeleccion({ grupoInformal: grupoInformal });
                    };

                    $scope.seleccionoPersona = function (personaSeleccion) {
                        $scope.cedulaContacto = personaSeleccion.numeroDocumento;
                        afiliacionesService.getAllGrupoInformalByContacto({ personaId: personaSeleccion.id }).success(function (data) {
                            $scope.listadoGruposInformales = data.gruposInformales;
                        });
                    };

                    $scope.seleccionoDocumentoPersona = function () {
                        personasService.getPersona({ numeroDocumento: $scope.cedulaContacto }).success(function (data) {
                            if (data == null) {
                                abp.notify.error(abp.localization.localize('directiva_bowBuscarGrupoInformal_persona_noEncontrada', 'Bow'), abp.localization.localize('directiva_bowBuscarGrupoInformal_informacion', 'Bow'));
                            }
                            else {
                                afiliacionesService.getAllGrupoInformalByContacto({ personaId: data.id }).success(function (data) {
                                    $scope.listadoGruposInformales = data.gruposInformales;
                                });
                            }
                        }).error(function (error) {
                            $scope.mensajeError = error.message;
                        });;
                    };

                    $scope.recargarListadoGruposInformales = function () {
                        $scope.controlBuscarNombreGrupoInformal.recargarListadoGruposInformales();
                    };

                    $scope.obtenerGrupoInformalSeleccionado = function () {
                        $scope.controlBuscarNombreGrupoInformal.obtenerGrupoSeleccionado();
                    };

                    $scope.asignarGrupoSeleccionado = function (grupoInformalId) {
                        $scope.controlBuscarNombreGrupoInformal.asignarGrupoSeleccionado(grupoInformalId);
                    };

                }],
                templateUrl: '/App/Main/directives/bowBuscarGrupoInformal/bowBuscarGrupoInformal.cshtml'
            };
        })
})();