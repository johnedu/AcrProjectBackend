(function () {
    angular.module('app')
        .directive("bowResultadoempleado", function () {
            return {
                restrict: 'E',
                scope: {
                    empleado: '=ngModel',
                    empleadoSeleccionada: '&bowSeleccionoempleado'
                },
                controller: ['$scope', function ($scope) {
                    $scope.selectedEmpleado = function (empleado) {
                        $scope.empleadoSeleccionada({ empleadoSeleccion: empleado });
                        //alert(JSON.stringify(persona));
                    }

                }],
                templateUrl: '/App/Main/directives/bowBuscarempleado/bowResultadoempleado/bowResultadoEmpleado.cshtml'
            };
        })
})();