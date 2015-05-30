(function () {
    angular.module('app')
        .directive("bowAfiliacionBeneficiosPlanExequial", function () {
            return {
                restrict: 'E',
                scope: {
                    notificarSeleccion: "&bowSeleccionoModelo",
                    modoConsulta: "=modoConsulta",
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.mostrarBeneficios = function (planExequialId) {
                        scope.mostrarBeneficios(planExequialId);
                    };
                    scope.internalControl.asignarBeneficiosPlanExequial = function (beneficiosAdicionales) {
                        scope.asignarBeneficiosPlanExequial(beneficiosAdicionales);
                    };
                    scope.internalControl.obtenerBeneficiosSeleccionados = function () {
                        scope.obtenerBeneficiosSeleccionados();
                    };
                },
                controller: ['$scope', 'abp.services.app.afiliaciones',
                    function ($scope, afiliacionesService) {

                    $scope.listadoBeneficiosPropios = '';
                    $scope.listadoBeneficiosAdicionales = '';

                    $scope.mostrarBeneficios = function (planExequialId) {
                        afiliacionesService.getAllBeneficiosPlanExequial({ planExequialId: planExequialId })
                            .success(function (data) {
                                $scope.listadoBeneficiosPropios = data.beneficios;
                                afiliacionesService.getAllBeneficiosAdicionalesPlanExequial({ planExequialId: planExequialId })
                                    .success(function (data) {
                                        $scope.listadoBeneficiosAdicionales = data.beneficios;
                                    });
                            });
                    };

                    $scope.asignarBeneficiosPlanExequial = function (beneficiosAdicionales) {
                        for (item in beneficiosAdicionales) {
                            for (beneficioAdicional in $scope.listadoBeneficiosAdicionales) {
                                if ($scope.listadoBeneficiosAdicionales[beneficioAdicional].id == beneficiosAdicionales[item].id) {
                                    for (beneficioPropio in $scope.listadoBeneficiosPropios) {
                                        if ($scope.listadoBeneficiosPropios[beneficioPropio].id == $scope.listadoBeneficiosAdicionales[beneficioAdicional].beneficioPlanExequialId) {
                                            $scope.listadoBeneficiosPropios[beneficioPropio].reemplazado = true;
                                            $scope.listadoBeneficiosAdicionales[beneficioAdicional].beneficioSeleccionado = true;
                                        }
                                    }
                                }
                            }
                        }
                    };
                    
                    $scope.obtenerBeneficiosSeleccionados = function () {
                        var beneficiosAdicionalesSeleccionados = $scope.listadoBeneficiosAdicionales.filter(function (el) {
                            return el.beneficioSeleccionado;
                        });
                        $scope.notificarSeleccion({ beneficios: beneficiosAdicionalesSeleccionados });
                    };

                    $scope.cambiarBeneficios = function (beneficioPlanExequialId, checked) {
                        if (beneficioPlanExequialId) {
                            for (item in $scope.listadoBeneficiosPropios) {
                                if ($scope.listadoBeneficiosPropios[item].id == beneficioPlanExequialId) {
                                    $scope.listadoBeneficiosPropios[item].reemplazado = checked;
                                }
                            }
                        }
                    };

                }],
                templateUrl: '/App/Main/views/afiliaciones/directives/bowAfiliacionBeneficiosPlanExequial/bowAfiliacionBeneficiosPlanExequial.cshtml'
            };
        })
})();