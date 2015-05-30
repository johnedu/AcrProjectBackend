(function () {
    angular.module('app')
        .directive("bowNuevotelefono", function () {
            return {
                restrict: 'E',
                scope: {
                    notificarTelefono: '&bowGuardotelefono',
                    control: '=',
                    formularioCorto: '@'
                },
                link: function (scope, element, attrs) {

                    scope.internalControl = scope.control || {};
                    scope.internalControl.seleccionarLocalidad = function (localidad) {
                        scope.cambiarLocalidad(localidad);
                    };
                    scope.internalControl.limpiarFormulario = function () {
                        scope.limpiarFormulario();
                    }
                },
                controller: ['$scope', 'abp.services.app.zonificacion', 'abp.services.app.parametros', function ($scope, zonificacionService, parametrosService) {

                    $scope.controlOperacionesLocalidad = {};

                    function cargarTipos() {
                        parametrosService.getAllTiposTelefono().success(function (data) {
                            $scope.tipos = data.tipos;
                        });
                    };
                    cargarTipos();

                    $scope.limpiarFormulario = function () {
                        $scope.telefono = '';
                        $scope.extension = '';
                        $scope.mensajeError = '';
                        $scope.localidad = '';
                        $scope.frmRegistrarTelefono.$setPristine();
                        parametrosService.getTipoTelefonoFijo().success(function (data) {
                            $scope.radioModel = data.id;
                        });
                        $scope.extensionMostrar = true;
                    };

                    $scope.localidadDisabled = false;

                    //Funcion para mostrar y ocultar el campo Extensión
                    $scope.mostrarExtension = function (mostrar) {
                        $scope.extensionMostrar = mostrar;
                        $scope.extension = "";
                    };
                    
                    //Registrando la localidad seleccionada por la directiva bow-buscarlocalidad
                    $scope.obtenerLocalidad = function (localidad) {
                        $scope.selectedLocalidad = localidad;
                    };

                    $scope.guardarTelefono = function () {
                        zonificacionService.saveTelefono({ numero: $scope.telefono, extension: $scope.extension, tipoId: $scope.radioModel, localidadId: $scope.localidad.localidadId })
                        .success(function (data) {
                            $scope.limpiarFormulario();
                            $scope.notificarTelefono({ telefono: data });
                        }).error(function (error) {
                            $scope.mensajeError = error.message;
                        });
                    }

                    $scope.cambiarLocalidad = function (localidad) {
                        $scope.controlOperacionesLocalidad.asignarLocalidad(localidad);
                        $scope.localidadDisabled = true;
                    };
                }],    
                templateUrl: function (elem, attrs) {
                    if (attrs.formularioCorto === "true") {
                        return '/App/Main/directives/bowNuevotelefono/bowNuevotelefonoCorto.cshtml';
                    }
                    else {
                        return '/App/Main/directives/bowNuevotelefono/bowNuevotelefono.cshtml';
                    }
                }
            };
        })
})();