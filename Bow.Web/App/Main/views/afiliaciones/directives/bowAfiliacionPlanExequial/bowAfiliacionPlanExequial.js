(function () {
    angular.module('app')
        .directive("bowAfiliacionPlanExequial", function () {
            return {
                restrict: 'E',
                scope: {
                    notificarSeleccion: "&bowSeleccionoModelo",
                    modoConsulta: "=modoConsulta",
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.asignarPlanesExequiales = function (modelo) {
                        scope.asignarPlanesExequiales(modelo);
                    };
                    scope.internalControl.seleccionarPlanExequial = function (PlanExequialId) {
                        scope.seleccionarPlanExequial(PlanExequialId);
                    };
                    scope.internalControl.asignarBeneficiosPlanExequial = function (beneficiosAdicionales) {
                        scope.asignarBeneficiosPlanExequial(beneficiosAdicionales);
                    };
                    scope.internalControl.obtenerPlanExequial = function () {
                        scope.obtenerPlanExequial();
                    };
                },
                controller: ['$scope', 'abp.services.app.afiliaciones',
                    function ($scope, afiliacionesService) {
                        $scope.controlBeneficios = {};

                        $scope.listadoPlanesExequiales = '';
                        $scope.selectedPlanExequial = '';

                        $scope.asignarPlanesExequiales = function (modelo) {
                            afiliacionesService.getAllPlanesExequialesBySucursalAndTipo({
                                sucursalId: modelo.sucursalId,
                                tipoPlan: modelo.tipoPlan,
                                empresaOrGrupoId: modelo.empresaOrGrupoId,
                                recaudoMasivoId: modelo.recaudoMasivoId
                            }).success(function (data) {
                                if (!data.planesExequiales.length) {
                                    throw new CampoRequeridoException('directivasException_listadoPlanExequial_vacio');
                                }
                                $scope.listadoPlanesExequiales = data.planesExequiales;
                            }).error(function (error) {
                                throw new CampoRequeridoException('directivasException_listadoPlanExequial_vacio');
                            });
                        };

                        $scope.seleccionarPlanExequial = function (planExequialId) {
                            if ($scope.listadoPlanesExequiales) {
                                $scope.selectedPlanExequial = getObjectById(planExequialId, $scope.listadoPlanesExequiales);
                                if ($scope.selectedPlanExequial) {
                                    $scope.controlBeneficios.mostrarBeneficios($scope.selectedPlanExequial.id);
                                }
                            }
                        };

                        $scope.cambiarBeneficios = function () {
                            $scope.controlBeneficios.mostrarBeneficios($scope.selectedPlanExequial.id);
                        };

                        $scope.asignarBeneficiosPlanExequial = function (beneficiosAdicionales) {
                            $scope.controlBeneficios.asignarBeneficiosPlanExequial(beneficiosAdicionales);
                        };

                        /************************************************************************
                         * Función para obtener el elemento de una lista según el id
                         ************************************************************************/
                        function getObjectById(id, arrayList) {
                            return arrayList.filter(function (obj) {
                                if (obj.id == id) {
                                    return obj
                                }
                            })[0];
                        }

                        $scope.obtenerPlanExequial = function () {
                            $scope.controlBeneficios.obtenerBeneficiosSeleccionados();
                        };
                        
                        $scope.validarSeleccion = function (beneficios) {
                            var planExequial = {
                                planExequial: $scope.selectedPlanExequial,
                                beneficiosAdicionales: beneficios
                            }
                            $scope.notificarSeleccion({ planExequial: planExequial });
                        }

                    }],
                templateUrl: '/App/Main/views/afiliaciones/directives/bowAfiliacionPlanExequial/bowAfiliacionPlanExequial.cshtml'
            };
        })
})();