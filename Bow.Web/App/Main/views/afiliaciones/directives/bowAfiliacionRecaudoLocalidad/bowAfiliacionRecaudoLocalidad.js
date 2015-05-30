(function () {
    angular.module('app')
        .directive("bowAfiliacionRecaudoLocalidad", function () {
            return {
                restrict: 'E',
                scope: {
                    notificarSeleccion: "&bowSeleccionoModelo",
                    modoConsulta: "=modoConsulta",
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.listarRecaudosMasivos = function (localidadId) {
                        scope.listarRecaudosMasivos(localidadId);
                    };
                    scope.internalControl.asignarRecaudoMasivo = function (recaudoMasivoId) {
                        scope.asignarRecaudoMasivo(recaudoMasivoId);
                    };
                    scope.internalControl.obtenerRecaudoMasivo = function () {
                        scope.obtenerRecaudoMasivo();
                    };
                },
                controller: ['$scope', 'abp.services.app.afiliaciones',
                    function ($scope, afiliacionesService) {
                        $scope.selectedRecaudo = '';
                        $scope.listaRecaudosMasivo = '';

                        $scope.asignarRecaudoMasivo = function (recaudoMasivoId) {
                            $scope.selectedRecaudo = getObjectById(recaudoMasivoId, $scope.listaRecaudosMasivo);
                        };

                        $scope.obtenerRecaudoMasivo = function () {
                            $scope.notificarSeleccion({ recaudoMasivo: $scope.selectedRecaudo });
                        };

                        $scope.listarRecaudosMasivos = function (localidadId) {
                            afiliacionesService.getAllRecaudosMasivosByLocalidad({ localidadId: localidadId })
                                .success(function (data) {
                                    $scope.listaRecaudosMasivo = data.recaudosMasivo;
                                });
                        };
                        $scope.listarRecaudosMasivos(1);

                        /************************************************************************
                         * Función para obtener el elemento de una lista según el id
                         ************************************************************************/
                        function getObjectById(id, arrayList) {
                            return arrayList.filter(function (obj) {
                                if (obj.id == id) {
                                    return obj
                                }
                            })[0]
                        };

                    }],
                templateUrl: '/App/Main/views/afiliaciones/directives/bowAfiliacionRecaudoLocalidad/bowAfiliacionRecaudoLocalidad.cshtml'
            };
        })
})();