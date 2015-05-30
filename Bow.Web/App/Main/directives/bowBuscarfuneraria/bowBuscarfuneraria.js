(function () {
    angular.module('app')
        .directive("bowBuscarfuneraria", function () {
            return {
                restrict: 'E',
                scope: {
                    titulolabel: "@titulolabel",
                    tituloplaceholder: "@tituloplaceholder",
                    selected: "=ngModel",
                    notificarSeleccion: "&bowSeleccionofuneraria",
                    requerido: "@ngRequired"
                },
                controller: ['$scope', 'abp.services.app.afiliaciones', function ($scope, afiliacionesService) {

                    //$scope.selected = undefined;

                    $scope.funerarias = [];

                    function cargarFunerarias() {
                        afiliacionesService.getAllFunerarias().success(function (data) {
                            $scope.funerarias = data.funerarias;
                        });
                    };

                    $scope.onSelect = function ($item, $model, $label) {
                        $scope.notificarSeleccion({ funeraria: $model });
                    };

                    cargarFunerarias();

                }],
                templateUrl: '/App/Main/directives/bowBuscarfuneraria/bowBuscarfuneraria.cshtml'
            };
        })
})();