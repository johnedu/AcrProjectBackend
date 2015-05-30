(function () {
    angular.module('app')
        .directive("bowBuscarapellidopersona", function () {
            return {
                restrict: 'E',
                scope: {
                    titulolabel: "@titulolabel",
                    tituloplaceholder: "@tituloplaceholder",
                    selected: "=ngModel"
                },
                controller: ['$scope', 'factoryNombresApellidos', function ($scope, factoryNombresApellidos) {

                    $scope.mostrarTitulo = false;

                    if ($scope.titulolabel) {
                        $scope.mostrarTitulo = true;
                    }

                    $scope.selected = undefined;

                    $scope.apellidos = [];
                    $scope.apellidos = factoryNombresApellidos.listaApellidos();

                }],
                templateUrl: '/App/Main/directives/bowBuscarapellidopersona/bowBuscarApellidoPersona.cshtml'
            };
        })

})();