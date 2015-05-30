(function () {
    angular.module('app')
        .directive("bowGestionProspecto", function () {
            return {
                restrict: 'E',
                scope: {
                    gestion: '=ngModel',
                    sucursalId: '=bowSucursalid'
                },
                controller: ['$scope', 'abp.services.app.afiliaciones', function ($scope, afiliacionesService) {

                    $scope.verDetalle = function (gestionId, grupoId) {

                        afiliacionesService.detalleClienteProspecto({ gestionProspectoId: gestionId, grupoFamiliarId: grupoId, sucursalId: $scope.sucursalId })
                        .success(function (data) {
                            $scope.mostrarDetalle = !$scope.mostrarDetalle;

                            $scope.detalleCliente = data;

                            if (data.planExequialEnSucursal == false) {
                                $scope.styleEnSucursal = { color: 'red' }
                            }
                        })
                        .error(function (error) {
                            abp.notify.warn(error.message, abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));
                        });
                    };

                }],
                templateUrl: '/App/Main/views/afiliaciones/directives/bowGestionProspecto/bowGestionProspecto.cshtml'
            };
        })
})();