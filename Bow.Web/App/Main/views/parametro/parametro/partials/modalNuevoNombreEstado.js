(function () {
    angular.module('app').controller('modalNuevoNombreEstadoController', ['$scope', '$modalInstance', 'abp.services.app.parametros',
        function ($scope, $modalInstance, parametrosService) {

            $scope.nuevoNombreEstado = {
                nombre: '',
                abreviacion: ''
            };

            $scope.okModal = function () {
                parametrosService.saveNombreEstado($scope.nuevoNombreEstado)
                    .success(function () {
                        $modalInstance.close($scope.nuevoNombreEstado.nombre);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            $scope.cerrarModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();