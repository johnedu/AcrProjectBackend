(function () {
    angular.module('app').controller('modalEditarPreguntasFrecuentesController', ['$scope', '$modalInstance', 'preguntaEditar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, preguntaEditar, administracionService) {

            $scope.codigoPregunta = preguntaEditar;

            $scope.preguntaFrecuente = {
                id: '',
                pregunta: '',
                respuesta: '',
                estadoActiva: true
            };

            administracionService.getPreguntaFrecuente({ id: preguntaEditar })
                .success(function (data) {
                    $scope.preguntaFrecuente = data;
                });

            $scope.okModal = function () {
                administracionService.updatePreguntaFrecuente($scope.preguntaFrecuente)
                    .success(function () {
                        $modalInstance.close($scope.preguntaFrecuente.pregunta);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();