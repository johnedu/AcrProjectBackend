(function () {
    angular.module('app').controller('modalNuevoPreguntasFrecuentesController', ['$scope', '$modalInstance', 'abp.services.app.administracion',
        function ($scope, $modalInstance, administracionService) {

            $scope.preguntaFrecuente = {
                pregunta: '',
                respuesta: '',
                estadoActiva: 'true'
            };

            $scope.okModal = function () {

                administracionService.savePreguntaFrecuente($scope.preguntaFrecuente)
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