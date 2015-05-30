(function () {
    angular.module('app').directive('bowModalbuscarempleado', ['$modal', function ($modal) {
        return {
            restrict: 'A',
            scope: {
                titulo: '@titulo',
                empleadoSeleccionado: "&bowSeleccionoempleado"
            },
            link: function (scope, elem, attrs) {
                elem.on('click', function () {
                    var modalInstance = $modal.open({
                        templateUrl: '/App/Main/directives/bowModalbuscarempleado/bowModalBuscarEmpleado.cshtml',
                        controller: 'modalConsultarEmpleadoController',
                        size: 'lg',
                        resolve: {
                            titulo: function () {
                                return scope.titulo;
                            }
                        }
                    });

                    modalInstance.result.then(function (data) {
                        scope.empleadoSeleccionado({ empleadoSeleccion: data });
                    }, function () {
                        console.log("No se selecciono ningún empleado");
                    });
                })
            }
        }
    }]);

    angular.module('app')
        .controller('modalConsultarEmpleadoController', ['$scope', '$modalInstance', 'titulo', function ($scope, $modalInstance, titulo) {

            $scope.titulo = titulo;

            //Cuando se registra la dirección se cierra la ventana retornando el dato
            $scope.seleccionoEmpleado = function (empleadoSeleccion) {
                $modalInstance.close(empleadoSeleccion);
            };

            //$scope.cancelModal = function () {
            //    $modalInstance.close('cancel');
            //}

        }])
})();