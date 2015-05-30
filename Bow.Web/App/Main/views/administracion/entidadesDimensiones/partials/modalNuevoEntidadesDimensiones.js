(function () {
    angular.module('app').controller('modalNuevoEntidadesDimensionesController', ['$scope', '$modalInstance', 'abp.services.app.administracion',
        function ($scope, $modalInstance, administracionService) {

            $scope.entidad = {
                nombre: '',
                descripcion: '',
                dimensionId: '',
                estadoActiva: true
            };

            function cargarDimensiones() {
                administracionService.getAllDimensiones().success(function (data) {
                    $scope.listaDimensiones = data.dimensiones;
                });
            }
            cargarDimensiones();

            $scope.okModal = function () {

                administracionService.saveEntidad($scope.entidad)
                    .success(function () {
                        $modalInstance.close($scope.entidad.nombre);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();