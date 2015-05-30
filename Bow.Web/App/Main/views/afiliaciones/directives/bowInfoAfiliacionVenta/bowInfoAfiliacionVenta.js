(function () {
    angular.module('app')
        .directive("bowInfoAfiliacionVenta", function () {
            return {
                restrict: 'E',
                scope: {
                    notificarSeleccion: "&bowValidarSeleccion",
                    modoConsulta: "=modoConsulta",
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.consultarInformacionVenta = function () {
                        scope.consultarInformacionVenta();
                    }
                    scope.internalControl.asignarInformacionVenta = function (modelo) {
                        scope.asignarInformacionVenta(modelo);
                    }
                },
                controller: ['$scope', 'abp.services.app.afiliaciones',
                    function ($scope, afiliacionesService) {
                    $scope.controlOperacionesEmpleado = {};
                    $scope.controlOperacionesSucursal = {};
                    $scope.controlOperacionesLocalidad = {};
                    $scope.empleado = '';
                    $scope.sucursal = '';
                    $scope.localidad = '';

                    $scope.consultarInformacionVenta = function () {
                        if (!$scope.empleado) {
                            throw new CampoRequeridoException('directivasException_campoEmpleado_requerido');
                        }
                        else if (!$scope.sucursal) {
                            throw new CampoRequeridoException('directivasException_campoSucursal_requerido');
                        }
                        else if (!$scope.localidad) {
                            throw new CampoRequeridoException('directivasException_campoLocalidad_requerido');
                        }
                        else {
                            var modelo = {
                                empleado: $scope.empleado,
                                sucursal: $scope.sucursal,
                                localidad: $scope.localidad
                            }
                            $scope.notificarSeleccion({ modelo: modelo });
                        }
                    };

                    $scope.asignarInformacionVenta = function (modelo) {
                        $scope.controlOperacionesEmpleado.asignarEmpleado(modelo.empleadoId);
                        $scope.controlOperacionesSucursal.asignarSucursal(modelo.sucursalId);
                        $scope.controlOperacionesLocalidad.asignarLocalidad(modelo.localidadId);
                    };

                }],
                templateUrl: '/App/Main/views/afiliaciones/directives/bowInfoAfiliacionVenta/bowInfoAfiliacionVenta.cshtml'
            };
        })
})();