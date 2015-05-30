(function () {
    angular.module('app')
        .directive("bowResultadobusqueda", function () {
            return {
                restrict: 'E',
                scope: {
                    persona: '=ngModel',
                    personaSeleccionada: '&bowSeleccionopersona'
                },
                controller: ['$scope', function ($scope) {

                    $scope.selectedPersona = function (persona) {
                        $scope.personaSeleccionada({ personaSeleccion: persona });
                        //alert(JSON.stringify(persona));
                    }

                }],
                templateUrl: '/App/Main/directives/bowBuscarpersona/bowResultadobusqueda/bowResultadoBusqueda.cshtml'
            };
        })
})();