(function () {
    angular.module('app')
        .directive("bowBuscarnombrepersona", function () {
            return {
                restrict: 'E',
                scope: {
                    titulolabel: "@titulolabel",
                    tituloplaceholder: "@tituloplaceholder",
                    selected: "=ngModel"
                    //notificarSeleccion: "&bowSelecciononombre"
                },
                controller: ['$scope', 'factoryNombresApellidos', function ($scope, factoryNombresApellidos) {

                    $scope.mostrarTitulo = false;

                    if ($scope.titulolabel) {
                        $scope.mostrarTitulo = true;
                    }

                    $scope.selected = undefined;

                    $scope.nombres = [];
                    $scope.nombres = factoryNombresApellidos.listaNombres();

                    //$scope.onSelect = function ($item, $model, $label) {
                    //    $scope.notificarSeleccion({ nombre: $model });
                    //};

                }],
                templateUrl: '/App/Main/directives/bowBuscarnombrepersona/bowBuscarNombrePersona.cshtml'
            };
        })

})();