(function () {
    angular.module('app')
        .directive("bowBuscarlocalidad", function () {
            return {
                restrict: 'E',
                scope: {
                    titulo: "@titulo",
                    mostrarindicativo: "@mostrarindicativo",
                    selected: "=ngModel",
                    disabled: "=ngDisabled",
                    notificarSeleccion: "&bowSeleccionolocalidad",
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.asignarLocalidad = function (localidadId) {
                        scope.asignarLocalidad(localidadId);
                    }
                },
                controller: ['$scope', 'abp.services.app.zonificacion', function ($scope, zonificacionService) {

                    if ($scope.mostrarindicativo === "true") {
                        $scope.exprMostrar = "localidad as localidad.localidad + ' - ' + localidad.departamento + ' (' + localidad.departamentoIndicativo + '), ' + localidad.pais + ' (' + localidad.paisIndicativo + ')' for localidad in localidades | filter: $viewValue";
                    }
                    else {
                        $scope.exprMostrar = "localidad as localidad.localidad + ' - ' + localidad.departamento + ', ' + localidad.pais for localidad in localidades | filter: $viewValue";
                    }

                    $scope.localidades = [];

                    function cargarLocalidades() {
                        zonificacionService.getAllLocalidades().success(function (data) {
                            $scope.localidades = data.localidades;
                        });
                    };

                    $scope.asignarLocalidad = function (localidadId) {
                        zonificacionService.getLocalidadByIdWithDepartamentoAndPais({ id: localidadId }).success(function (data) {
                            $scope.selected = data;
                        });
                        
                    };

                    $scope.onSelect = function ($item, $model, $label) {
                        $scope.notificarSeleccion({ localidad: $model });
                    };

                    cargarLocalidades();

                }],
                templateUrl: '/App/Main/directives/bowBuscarLocalidad/bowBuscarLocalidad.cshtml'
            };
        })
})();