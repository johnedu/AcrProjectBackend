(function () {
    angular.module('app').controller('modalEliminarEntidadesDimensionesController', ['$scope', '$modalInstance', 'entidadEliminar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, entidadEliminar, administracionService) {
            $scope.entidad = '';

            administracionService.getEntidad({ id: entidadEliminar })
                .success(function (data) {
                    $scope.entidad = data;
                });

            $scope.okModal = function () {
                administracionService.deleteEntidad({ id: entidadEliminar })
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

