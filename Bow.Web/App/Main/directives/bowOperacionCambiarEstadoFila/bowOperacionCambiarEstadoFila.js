(function () {
    angular.module('app').directive("bowOperacionCambiarEstadoFila", function () {
        return {
            restrict: 'E',
            scope: {
                mensajeConfirmacionActivo: '@',
                mensajeConfirmacionInactivo: '@',
                mensajeOk: '@',
                mensajeCancel: '@',
                mensajeTooltipActivo: '@',
                mensajeTooltipInactivo: '@',
                posicionTooltip: '@',
                estadoActualActivo: '@',
                notificarConfirmacionOk: '&confirmacionOk',
                notificarConfirmacion: '&cambiandoEstado',
            },
            controller: ['$scope', function ($scope) {
                $scope.mostrarOperacion = true;

                if (!$scope.posicionTooltip) {
                    $scope.posicionTooltip = 'left';
                }

                if (!$scope.mensajeOk) {
                    $scope.mensajeOk = abp.localization.localize('directiva_confirmacion_eliminacion_si', 'Bow');
                }

                if (!$scope.mensajeCancel) {
                    $scope.mensajeCancel = abp.localization.localize('directiva_confirmacion_eliminacion_no', 'Bow');
                }

                if (!$scope.estadoActualActivo) {
                    $scope.estadoActualActivo = true;
                }

                $scope.mostrarConfirmacion = function () {
                    $scope.notificarConfirmacion({ ocultar: true });
                    $scope.mostrarOperacion = false;
                };

                $scope.confirmacionOk = function () {
                    $scope.mostrarOperacion = true;
                    $scope.notificarConfirmacionOk();
                };

                $scope.confirmacionCancel = function () {
                    $scope.notificarConfirmacion({ ocultar: false });
                    $scope.mostrarOperacion = true;
                }

            }],
            link: function (scope, elem, attrs) {
                var elementoPadre = $(elem).parents('tr');
                elementoPadre.on('mouseleave', function () {
                    scope.mostrarOperacion = true;
                })
            },
            templateUrl: '/App/Main/directives/bowOperacionCambiarEstadoFila/bowOperacionCambiarEstadoFila.cshtml'
        }
    });
})();