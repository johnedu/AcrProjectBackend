-(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.afiliaciones.infoAfiliacionVenta';

    /*****************************************************************
     * 
     * CONTROLADOR INFO AFILIACIÓN VENTA
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope',
        function ($scope) {

            $scope.controlAfiliacionVenta = {};
            $scope.controlAfiliacionPlanExequial = {};
            $scope.controlAfiliacionTipoPlanExequial = {};
            $scope.controlAfiliacionRecaudoLocalidad = {};

            $scope.modoConsulta = false;
            $scope.modoConsultaTipoPlanExequial = false;
            $scope.modoConsultaPlanExequial = false;
            $scope.modoConsultaRecaudoLocalidad = false;

            $scope.validarSeleccion = function (modelo) {
                console.log(modelo);
            }

            $scope.consultarSeleccionado = function () {
                try {
                    $scope.controlAfiliacionVenta.consultarInformacionVenta();
                }
                catch (e) {
                    //console.log(e);
                    if (e instanceof CampoRequeridoException) {
                        abp.notify.error(abp.localization.localize(e.message, 'Bow'), abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                    }
                }
            }

            $scope.asignarModelos = function () {
                var modelo = {
                    sucursalId: 1,
                    empleadoId: 1,
                    localidadId: 1
                }
                $scope.controlAfiliacionVenta.asignarInformacionVenta(modelo);
            }

            $scope.consultarPlanExequialSeleccionado = function () {
                $scope.controlAfiliacionPlanExequial.obtenerPlanExequial();
            }

            $scope.asignarModelosPlanExequial = function () {
                try {
                    var modelo = {
                        sucursalId: 1,
                        tipoPlan: 'Empresarial',
                        empresaOrGrupoId: null,
                        recaudoMasivoId: null
                    };
                    $scope.controlAfiliacionPlanExequial.asignarPlanesExequiales(modelo);
                }
                catch (e) {
                    if (e instanceof CampoRequeridoException) {
                        abp.notify.error(abp.localization.localize(e.message, 'Bow'), abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                    }
                }
            }

            $scope.asignarPlanExequial = function () {
                var planExequialId = 1;
                $scope.controlAfiliacionPlanExequial.seleccionarPlanExequial(planExequialId);
            }
            
            $scope.asignarBeneficiosPlanExequial = function () {
                var beneficiosAdicionales = [
                    { id: 2 },
                    { id: 3 }
                ];
                $scope.controlAfiliacionPlanExequial.asignarBeneficiosPlanExequial(beneficiosAdicionales);
            }

            $scope.validarSeleccionPlanExequial = function (planExequial) {
                console.log(planExequial);
            }

            $scope.asignarTipoPlanExequial = function () {
                var tipoPlanExequial = {
                    tipoPlan: 'Grupo',
                    empresaId: null,
                    grupoId: 1
                }
                $scope.controlAfiliacionTipoPlanExequial.asignarTipoPlanExequial(tipoPlanExequial);
            }

            $scope.consultarTipoPlanExequialSeleccionado = function () {
                try {
                    $scope.controlAfiliacionTipoPlanExequial.obtenerTipoPlanExequial();
                }
                catch (e) {
                    //console.log(e);
                    if (e instanceof CampoRequeridoException) {
                        abp.notify.error(abp.localization.localize(e.message, 'Bow'), abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                    }
                }
            }

            $scope.validarSeleccionTipoPlanExequial = function (tipoPlanExequial) {
                console.log(tipoPlanExequial);
            }

            $scope.obtenerSeleccionRecaudoLocalidad = function (recaudoMasivo) {
                console.log(recaudoMasivo);
            }

            $scope.listarRecaudosLocalidad = function () {
                var localidadId = 1;
                $scope.controlAfiliacionRecaudoLocalidad.listarRecaudosMasivos(localidadId);
            }

            $scope.asignarRecaudoLocalidad = function () {
                var recaudoMasivoId = 1;
                $scope.controlAfiliacionRecaudoLocalidad.asignarRecaudoMasivo(recaudoMasivoId);
            }

            $scope.consultarRecaudoLocalidadSeleccionado = function () {
                $scope.controlAfiliacionRecaudoLocalidad.obtenerRecaudoMasivo();
            }
            
        }]);
})();