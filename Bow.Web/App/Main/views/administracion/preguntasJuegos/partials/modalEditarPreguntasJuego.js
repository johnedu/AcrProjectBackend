(function () {
    angular.module('app').controller('modalEditarPreguntasJuegoController', ['$scope', '$modalInstance', 'preguntaEditar', 'preguntaTexto', 'abp.services.app.administracion',
        function ($scope, $modalInstance, preguntaEditar, preguntaTexto, administracionService) {

            $scope.preguntaTexto = preguntaTexto;

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

            administracionService.getPregunta({ id: preguntaEditar })
                .success(function (data) {
                    $scope.pregunta = data;
                });

            $scope.okModal = function () {
                administracionService.updatePregunta($scope.pregunta)
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