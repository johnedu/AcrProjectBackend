(function () {
    angular.module('app')
        .directive("bowResultadobusquedaGrupoinformal", function () {
            return {
                restrict: 'E',
                scope: {
                    persona: '=ngModel',
                    personaSeleccionada: '&bowSeleccionopersona'
                },
                controller: ['$scope', function ($scope) {

                    $scope.selectedPersona = function (persona) {
                        $scope.personaSeleccionada({ personaSeleccion: persona });

                    }

                }],
                templateUrl: '/App/Main/directives/bowBuscarpersonaGrupoinformal/bowResultadobusquedaGrupoinformal/bowResultadobusquedaGrupoinformal.cshtml'
            };
        })
})();