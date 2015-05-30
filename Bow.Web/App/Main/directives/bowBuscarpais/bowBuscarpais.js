(function () {
    angular.module('app')
        .directive("bowBuscarPais", function () {
            return {
                restrict: 'E',
                scope: {
                    titulo: "@titulo",
                    errorRequerido: "@",
                    errorInvalido: "@",
                    deshabilitado: "=ngDisabled",
                    requerido: "@",
                    notificarSeleccion: "&seleccionoPais",
                    selected: "=ngModel"
                },
                replace: true,
                controller: ['$scope', 'abp.services.app.zonificacion', function ($scope, zonificacionService) {

                    //alert($scope.selected);

                    ///***********************************************************************
                    // * Si no se especifica ng-disbled se deja habilitado el campo
                    // ***********************************************************************/
                    //if (!$scope.deshabilitado) {
                    //    $scope.deshabilitado = false;
                    //}
                    
                    /**********************************************************************************
                     * Si no se definen los textos del control se colocan los textos por defecto
                     *********************************************************************************/
                    if (!$scope.requerido) {
                        $scope.requerido = true;
                    }

                    if (!$scope.titulo)
                        $scope.titulo = abp.localization.localize('directiva_buscar_pais_titulo', 'Bow');

                    if (!$scope.errorRequerido) {
                        $scope.errorRequerido = abp.localization.localize('directiva_buscar_pais_requerido', 'Bow');
                    }

                    if (!$scope.errorInvalido) {
                        $scope.errorInvalido = abp.localization.localize('directiva_buscar_pais_invalido', 'Bow');
                    }

                    /*********************************************************
                     * Consultando los paises registrados en el sistema
                     *********************************************************/
                    $scope.paises = [];

                    function cargarpaises() {
                        zonificacionService.getPaises().success(function (data) {
                            $scope.paises = data.paises;
                        });
                    }
                    cargarpaises();

                    /***********************************************************
                     * Notificando el pais seleccionado
                     ***********************************************************/

                    $scope.onSelect = function ($item, $model, $label) {
                        $scope.selected = $model;
                        $scope.notificarSeleccion({ pais: $model });
                    };

                }],
                templateUrl: '/App/Main/directives/bowBuscarPais/bowBuscarPais.cshtml'
            };
        })
})();