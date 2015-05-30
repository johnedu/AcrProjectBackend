(function () {
    angular.module('app').controller('modalEliminarPreguntasFrecuentesController', ['$scope', '$modalInstance', 'preguntaEliminar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, preguntaEliminar, administracionService) {

            administracionService.getPreguntaFrecuente({ id: preguntaEliminar })
                .success(function (data) {
                    $scope.preguntaFrecuente = data;
                });

            $scope.okModal = function () {
                administracionService.deletePreguntaFrecuente({ id: preguntaEliminar })
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

