(function () {
    angular.module('app').directive('bowModalbuscarpersonaGrupoinformal', ['$modal', function ($modal) {
        return {
            restrict: 'A',
            scope: {
                titulo: '@titulo',
                personaSeleccionada: "&bowSeleccionopersona"
            },
            link: function (scope, elem, attrs) {
                elem.on('click', function () {
                    var modalInstance = $modal.open({
                        templateUrl: '/App/Main/directives/bowModalbuscarpersonaGrupoinformal/bowModalbuscarpersonaGrupoinformal.cshtml',
                        controller: 'bowModalbuscarpersonaGrupoinformalController',
                        size: 'lg',
                        resolve: {
                            titulo: function () {
                                return scope.titulo;
                            }
                        }
                    });

                    modalInstance.result.then(function (data) {
                        scope.personaSeleccionada({ personaSeleccion: data });
                    }, function () {
                        console.log("No se selecciono ninguna persona");
                    });
                })
            }
        }
    }]);

    angular.module('app')
        .controller('bowModalbuscarpersonaGrupoinformalController', ['$scope', '$modalInstance', 'titulo', function ($scope, $modalInstance, titulo) {

            $scope.titulo = titulo;

            //Cuando se registra la dirección se cierra la ventana retornando el dato
            $scope.seleccionoPersona = function (personaSeleccion) {
                $modalInstance.close(personaSeleccion);
            };

            //$scope.cancelModal = function () {
            //    $modalInstance.close('cancel');
            //}

        }])
})();