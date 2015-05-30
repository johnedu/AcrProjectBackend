(function () {
    angular.module('app')
        .directive("bowAfiliacionTipoPlanExequial", function () {
            return {
                restrict: 'E',
                scope: {
                    notificarSeleccion: "&bowSeleccionoModelo",
                    modoConsulta: "=modoConsulta",
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.asignarTipoPlanExequial = function (tipoPlanExequial) {
                        scope.asignarTipoPlanExequial(tipoPlanExequial);
                    };
                    scope.internalControl.obtenerTipoPlanExequial = function () {
                        scope.obtenerTipoPlanExequial();
                    };
                },
                controller: ['$scope', 'abp.services.app.afiliaciones',
                    function ($scope, afiliacionesService) {
                        $scope.controlBuscarNombreGrupoInformal = {};

                        $scope.tipoPlan = 'Empresarial';
                        $scope.selectedEmpresa = '';
                        $scope.selectedGrupoInformal = '';

                        $scope.asignarTipoPlanExequial = function (tipoPlanExequial) {
                            $scope.tipoPlan = tipoPlanExequial.tipoPlan;
                            if (tipoPlanExequial.empresaId) {
                                $scope.selectedEmpresa
                            }
                            if (tipoPlanExequial.grupoId) {
                                $scope.controlBuscarNombreGrupoInformal.asignarGrupoSeleccionado(tipoPlanExequial.grupoId);
                            }
                        };

                        $scope.seleccionoGrupoInformal = function (grupoInformal) {
                            console.log(grupoInformal);
                        };

                        $scope.obtenerTipoPlanExequial = function () {
                            var tipoPlanExequial = {
                                tipoPlan: $scope.tipoPlan,
                                empresaId: '',
                                grupoId: ''
                            };
                            if ($scope.tipoPlan == 'Empresarial') {
                                if (!$scope.selectedEmpresa) {
                                    throw new CampoRequeridoException('directivasException_campoEmpresa_requerido');
                                }
                                tipoPlanExequial.empresaId = $scope.selectedEmpresa;
                            }
                            else if ($scope.tipoPlan == 'Grupo') {
                                if (!$scope.selectedGrupoInformal) {
                                    throw new CampoRequeridoException('directivasException_campoGrupo_requerido');
                                }
                                tipoPlanExequial.grupoId = $scope.selectedGrupoInformal.id;
                            }
                            $scope.notificarSeleccion({ tipoPlanExequial: tipoPlanExequial });
                        };

                    }],
                templateUrl: '/App/Main/views/afiliaciones/directives/bowAfiliacionTipoPlanExequial/bowAfiliacionTipoPlanExequial.cshtml'
            };
        })
})();