(function () {
    angular.module('app').controller('modalNuevoPreguntasJuegoController', ['$scope', '$modalInstance', 'abp.services.app.administracion',
        function ($scope, $modalInstance, administracionService) {

            $scope.pregunta = {
                texto: '',
                juegoId: '',
                dimensionId: '',
                nivel: '',
                pista: '',
                estadoActiva: true
            };

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

            $scope.okModal = function () {

                administracionService.savePregunta($scope.pregunta)
                    .success(function () {
                        $modalInstance.close($scope.pregunta.texto);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();