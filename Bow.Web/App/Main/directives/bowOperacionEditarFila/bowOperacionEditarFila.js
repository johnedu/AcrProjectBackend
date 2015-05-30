(function () {
    angular.module('app').directive("bowOperacionEditarFila", function () {
        return {
            restrict: 'E',
            scope: {
                notificarOperacionEditar: '&operacionEditar',
                mensajeTooltip: '@'
            },
            controller: ['$scope', function ($scope) {

                if (!$scope.posicionTooltip) {
                    $scope.posicionTooltip = 'left';
                }

                $scope.operacionEditar = function () {
                    $scope.notificarOperacionEditar();
                };
            }],
            templateUrl: '/App/Main/directives/bowOperacionEditarFila/bowOperacionEditarFila.cshtml'
        }
    });
})();