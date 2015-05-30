(function () {
    angular.module('app').directive('bowModalpersona', ['$modal', function ($modal) {
        return {
            restrict: 'A',
            scope: {
                titulo: '@titulo',
                reportarPersona: '&bowPersonaregistrada',
                personaEstado: '=ngModel',
                fechaNacimientoRequerido: '=fechaNacimientoRequired'
            },
            link: function (scope, elem, attrs) {
                elem.on('click', function () {
                    var modalInstance = $modal.open({
                        templateUrl: '/App/Main/directives/bowModalpersona/bowModalpersona.cshtml',
                        controller: 'modalRegistrarPersonaController',
                        size: 'lg',
                        resolve: {
                            personaEstado: function () {
                                return scope.personaEstado;
                            },
                            fechaNacimientoRequerido: function () {
                                return scope.fechaNacimientoRequerido;
                            },
                            titulo: function () {
                                return scope.titulo;
                            }
                        }
                    });

                    modalInstance.result.then(function (data) {
                        scope.reportarPersona({ persona: data });
                    }, function () {
                        console.log("Error devolviendo persona");
                    });
                })
            }
        }
    }]);

    angular.module('app')
        .controller('modalRegistrarPersonaController', ['$scope', '$modalInstance', 'personaEstado', 'fechaNacimientoRequerido', 'titulo', function ($scope, $modalInstance, personaEstado, fechaNacimientoRequerido, titulo) {

            //Se asigna al ng-model de la directiva de persona
            $scope.personaConsultar = personaEstado;

            //Se asigna el valor true o false de la fechaRequerida a la directiva de persona
            $scope.fechaNacimientoRequerida = fechaNacimientoRequerido;

            //Se asigna el titulo a la ventana modal
            $scope.titulo = titulo;

            //Cuando se registra la persona se cierra la ventana retornando el dato
            $scope.personaRegistrada = function (persona) {
                $modalInstance.close(persona);
            };

            $scope.cancelModal = function () {
                $modalInstance.close('cancel');
            }
        }])
})();