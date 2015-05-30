(function () {
    angular.module('app')
        .directive("bowBuscarpersonaGrupoinformal", function () {
            return {
                restrict: 'E',
                scope: {
                    personaSeleccionada: "&bowSeleccionopersona"
                },
                controller: ['$scope', 'abp.services.app.personas', function ($scope, personasService) {

                    $scope.buscarPor = {
                        nombre: '',
                        apellido1: '',
                        apellido2: '',
                        documento: '',
                        telefono: '',
                        zipCode: '',
                        correoElectronico: ''
                    };

                    $scope.seleccionoPersona = function (personaSeleccion) {
                        $scope.personaSeleccionada({ personaSeleccion: personaSeleccion });
                        $scope.limpiarBuscador();
                    }

                    $scope.consultarPersonas = function () {

                        //Se verifica cuantos input lleno para poder hacer el filtro, deben ser mayor >= 2
                        var cantidad = 0;
                        for (var key in $scope.buscarPor) {
                            if ($scope.buscarPor[key] != "") {
                                cantidad = cantidad + 1;
                            }
                        }

                        if (cantidad >= 2) {
                            personasService.getBuscadorPersonaGrupoInformal($scope.buscarPor).success(function (data) {
                                if (data.personas != "") {
                                    $scope.resultadoBusqueda = bow.tablas.paginar(data.personas, 7);
                                } else {
                                    $scope.resultadoBusqueda = "";
                                    abp.notify.warn(abp.localization.localize('directiva_bowBuscadorpersona_noSeEncontraronDatos', 'Bow'), abp.localization.localize('directiva_bowResultadobusquedaGrupoinformal_informacion', 'Bow'));
                                }

                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });
                        } else {
                            abp.notify.warn(abp.localization.localize('directiva_bowResultadobusquedaGrupoinformal_cantidadFiltros', 'Bow'), abp.localization.localize('directiva_bowResultadobusquedaGrupoinformal_informacion', 'Bow'));
                        }
                    }

                    $scope.limpiarBuscador = function () {
                        $scope.buscarPor = "";
                        $scope.resultadoBusqueda = "";
                    }


                }],
                templateUrl: '/App/Main/directives/bowBuscarpersonaGrupoinformal/bowBuscarpersonaGrupoinformal.cshtml'
            };
        })
})();