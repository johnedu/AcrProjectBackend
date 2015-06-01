(function () {
    angular.module('app').controller('modalEditarEntidadesDimensionesController', ['$scope', '$modalInstance', 'entidadEditar', 'entidadNombre', 'abp.services.app.administracion',
        function ($scope, $modalInstance, entidadEditar, entidadNombre, administracionService) {

            $scope.entidadNombre = entidadNombre;
            $scope.entidad = '';

            function cargarJuegos() {
                administracionService.getAllJuegos().success(function (data) {
                    $scope.listaJuegos = data.juegos;
                });
            }
            cargarJuegos();

            function cargarDimensiones() {
                administracionService.getAllDimensiones().success(function (data) {
                    $scope.listaDimensiones = data.dimensiones;
                });
            }
            cargarDimensiones();

            administracionService.getEntidad({ id: entidadEditar })
                .success(function (data) {
                    $scope.entidad = data;
                });

            $scope.okModal = function () {
                administracionService.updateEntidad($scope.entidad)
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