(function () {
    angular.module('app')
        .directive("bowBuscarEmpleadoTypeahead", function () {
            return {
                restrict: 'E',
                scope: {
                    titulolabel: "@titulolabel",
                    tituloplaceholder: "@tituloplaceholder",
                    selected: "=ngModel",
                    notificarSeleccion: "&bowSeleccionoEmpleado",
                    disabled: "=ngDisabled",
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.asignarEmpleado = function (empleadoId) {
                        scope.asignarEmpleado(empleadoId);
                    }
                },
                controller: ['$scope', 'abp.services.app.empleados', function ($scope, empleadosService) {
                    $scope.exprMostrar = "empleado as empleado.codigo + ' - ' + empleado.nombreCompleto + ' (' + empleado.sucursalNombre + ')' for empleado in listaEmpleados | filter: $viewValue";

                    $scope.listaEmpleados = [];

                    function cargarEmpleados() {
                        empleadosService.getEmpleadosWithSucursal().success(function (data) {
                            $scope.listaEmpleados = data.empleados;;
                        });
                    };

                    $scope.asignarEmpleado = function (empleadoId) {
                        empleadosService.getEmpleadosById({ id: empleadoId }).success(function (data) {
                            $scope.selected = data;
                        });
                    };

                    $scope.onSelect = function ($item, $model, $label) {
                        $scope.notificarSeleccion({ empleado: $model });
                    };

                    cargarEmpleados();

                }],
                templateUrl: '/App/Main/directives/bowBuscarEmpleadoTypeahead/bowBuscarEmpleadoTypeahead.cshtml'
            };
        })
})();