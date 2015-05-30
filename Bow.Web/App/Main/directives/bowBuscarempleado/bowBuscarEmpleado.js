(function () {
    angular.module('app')
        .directive("bowBuscarempleado", function () {
            return {
                restrict: 'E',
                scope: {
                    empleadoSeleccionado: "&bowSeleccionoempleado"
                },
                controller: ['$scope', 'abp.services.app.empleados', 'abp.services.app.parametros', function ($scope, empleadosService, parametrosService) {

                    $scope.buscarPor = {
                        nombre: '',
                        apellido1: '',
                        apellido2: '',
                        documento: '',
                        correoElectronico: '',
                        sucursalId: '',
                        estadoId: ''
                    };

                    parametrosService.getEstadosEmpleado().success(function (data) {
                        $scope.estados = data.estados;                        
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });

                    $scope.seleccionoEmpleado = function (empleadoSeleccion) {
                        $scope.empleadoSeleccionado({ empleadoSeleccion: empleadoSeleccion });
                        $scope.limpiarBuscador();
                    }

                    $scope.seleccionoSucursal = function (sucursal) {
                        $scope.sucursalSeleccionada = sucursal;
                    }

                    $scope.consultarEmpleados = function () {

                        if ($scope.estado == undefined || $scope.estado == "") {
                            $scope.buscarPor.estadoId = 0;
                        } else {
                            $scope.buscarPor.estadoId = $scope.estado.id;
                        }

                        if ($scope.selectedSucursal == undefined || $scope.selectedSucursal == "") {
                            $scope.buscarPor.sucursalId = 0;
                        } else {
                            $scope.buscarPor.sucursalId = $scope.selectedSucursal.id;
                        }

                        //Se verifica cuantos input tienen valor para poder hacer el filtro, deben ser mayor >= 2
                        var cantidad = 0;
                        for (var key in $scope.buscarPor) {
                            if ($scope.buscarPor[key] != "") {
                                cantidad = cantidad + 1;
                            }
                        }

                        if (cantidad >= 2) {

                            empleadosService.getBuscadorEmpleado($scope.buscarPor).success(function (data) {
                                if (data.empleados != "") {
                                    $scope.resultadoBusqueda = bow.tablas.paginar(data.empleados, 7);
                                } else {
                                    $scope.resultadoBusqueda = "";
                                    abp.notify.warn(abp.localization.localize('directiva_bowBuscadorempleado_noSeEncontraronDatos', 'Bow'), abp.localization.localize('directiva_bowBuscadorempleado_informacion', 'Bow'));
                                }
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });
                        } else {
                            abp.notify.warn(abp.localization.localize('directiva_bowBuscadorempleado_cantidadFiltrosIncorrecto', 'Bow'), abp.localization.localize('directiva_bowBuscadorempleado_informacion', 'Bow'));
                        }
                    }

                    $scope.limpiarBuscador = function () {

                        $scope.buscarPor = "";

                        $scope.estado = 0;
                        $scope.selectedSucursal = "";
                        $scope.resultadoBusqueda = "";

                    }

                }],
                templateUrl: '/App/Main/directives/bowBuscarempleado/bowBuscarEmpleado.cshtml'
            };
        })
})();