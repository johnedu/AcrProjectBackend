(function () {
    angular.module('app')
        .directive("bowBuscarparentesco", function () {
            return {
                restrict: 'E',
                scope: {
                    titulolabel: "@titulolabel",
                    tituloplaceholder: "@tituloplaceholder",
                    selected: "=ngModel",
                    notificarSeleccion: "&bowSeleccionoparentesco",
                    requerido: "@ngRequired"
                },
                controller: ['$scope', 'abp.services.app.afiliaciones', function ($scope, afiliacionesService) {

                    $scope.selected = undefined;

                    $scope.parentescos = [];

                    function cargarParentescos() {
                        afiliacionesService.getAllParentescos().success(function (data) {
                            $scope.parentescos = data.parentescos;
                        });
                    };

                    $scope.onSelect = function ($item, $model, $label) {
                        $scope.notificarSeleccion({ parentesco: $model });
                    };

                    cargarParentescos();

                }],
                templateUrl: '/App/Main/directives/bowBuscarparentesco/bowBuscarParentesco.cshtml'
            };
        })
})();