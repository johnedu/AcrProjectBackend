(function () {
    angular.module('app').directive('bowModalnuevotelefono', ['$modal', function ($modal) {
        return {
            restrict: 'A',
            scope: {
                reportarNuevoTelefono: '&bowNuevotelefonoregistrado'
            },
            link: function (scope, elem, attrs) {
                elem.on('click', function () {
                    var modalInstance = $modal.open({
                        templateUrl: '/App/Main/directives/bowModalnuevotelefono/bowModalNuevoTelefono.cshtml',
                        controller: 'modalRegistrarTelefonoController',
                        size: 'md'
                    });

                    modalInstance.result.then(function (data) {
                        scope.reportarNuevoTelefono({ telefono: data });
                    }, function () {
                        console.log("Error devolviendo telefono");
                    });
                })
            }
        }
    }]);

    angular.module('app')
        .controller('modalRegistrarTelefonoController', ['$scope', '$modalInstance', function ($scope, $modalInstance) {

            //Cuando se registra el teléfono se cierra la ventana retornando el dato
            $scope.registroTelefono = function (telefono) {
                $modalInstance.close(telefono);
            };

            $scope.cancelModal = function () {
                $modalInstance.close('cancel');
            }
        }])
})();