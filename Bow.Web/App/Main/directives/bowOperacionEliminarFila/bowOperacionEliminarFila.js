(function () {
    angular.module('app').directive("bowOperacionEliminarFila", function () {
        return {
            restrict: 'E',
            scope: {
                mensajeConfirmacion: '@',
                tooltipOk: '@',
                tooltipCancel: '@',
                mensajeTooltip: '@',
                preguntarPuedeEliminar: '&operacionPuedeEliminar',
                notificarNoPuedeEliminar: '&noPuedeEliminar',
                notificarConfirmacionOk: '&confirmacionOk',
                notificarConfirmacionCancel: '&confirmacionCancel'
            },
            controller: ['$scope', function ($scope) {

                $scope.mostrarOperacion = true;

                function capturarValorPuedeEliminar(resultado) {
                    $scope.mostrarOperacion = !resultado;

                    if (!resultado) {
                        $scope.notificarNoPuedeEliminar();
                    }
                };

                if (!$scope.tooltipOk) {
                    $scope.tooltipOk = abp.localization.localize('directiva_confirmacion_eliminacion_tooltip_ok', 'Bow');
                }

                if (!$scope.tooltipCancel) {
                    $scope.tooltipCancel = abp.localization.localize('directiva_confirmacion_eliminacion_tooltip_cancel', 'Bow');
                }

                $scope.mostrarConfirmacion = function () {
                    $scope.preguntarPuedeEliminar({ funcionRetornarPuedeEliminar: capturarValorPuedeEliminar });
                };

                $scope.confirmacionOk = function () {
                    $scope.mostrarOperacion = true;
                    $scope.notificarConfirmacionOk();
                };

                $scope.confirmacionCancel = function () {
                    $scope.mostrarOperacion = true;
                    $scope.notificarConfirmacionCancel();
                }

            }],
            link: function(scope, elem, attrs) {
                var elementoPadre = $(elem).parents('tr');
                elementoPadre.on('mouseleave', function () {
                    scope.mostrarOperacion = true;
                })
            },
            templateUrl: '/App/Main/directives/bowOperacionEliminarFila/bowOperacionEliminarFila.cshtml'
        }
    });
})();