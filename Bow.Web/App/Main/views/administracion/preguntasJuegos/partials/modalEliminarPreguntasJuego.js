(function () {
    angular.module('app').controller('modalEliminarPreguntasJuegoController', ['$scope', '$modalInstance', 'preguntaEliminar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, preguntaEliminar, administracionService) {

            administracionService.getPregunta({ id: preguntaEliminar })
                .success(function (data) {
                    $scope.pregunta = data;
                });

            $scope.okModal = function () {
                administracionService.deletePregunta({ id: preguntaEliminar })
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

