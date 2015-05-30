(function () {
    angular.module('app').directive('bowModalnuevadireccion', ['$modal', function ($modal) {
        return {
            restrict: 'A',
            scope: {
                reportarNuevaDireccion: '&bowNuevadireccionregistrada',
                localidadSeleccionada: '=',
                inputLocalidadDisabled: '=inputLocalidadDisabled'
            },
            link: function (scope, elem, attrs) {
                elem.on('click', function () {
                    var modalInstance = $modal.open({
                        templateUrl: '/App/Main/directives/bowModalnuevadireccion/bowModalnuevadireccion.cshtml',
                        controller: 'modalRegistrarDireccionController',
                        size: 'lg',
                        resolve: {
                            localidadSeleccionada: function () {
                                return scope.localidadSeleccionada;
                            },
                            inputLocalidadDisabled: function () {
                                return scope.inputLocalidadDisabled;
                            }
                        }
                    });

                    modalInstance.result.then(function (data) {
                        scope.reportarNuevaDireccion({ direccion: data });
                    }, function () {
                        console.log("Error devolviendo dirección");
                    });
                })
            }
        }
    }]);

    angular.module('app')
        .controller('modalRegistrarDireccionController', ['$scope', '$modalInstance', 'localidadSeleccionada', 'inputLocalidadDisabled',
            function ($scope, $modalInstance, localidadSeleccionada, inputLocalidadDisabled) {

                $scope.localidadSeleccionada = localidadSeleccionada;
                $scope.inputLocalidadDisabled = inputLocalidadDisabled;

                //Cuando se registra la dirección se cierra la ventana retornando el dato
                $scope.direccionRegistrada = function (direccion) {
                    $modalInstance.close(direccion);
                };

                $scope.cancelModal = function () {
                    $modalInstance.close('cancel');
                }
            }])
})();