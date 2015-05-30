(function () {
    angular.module('app')
        .directive("bowPlanclienteprospecto", function () {
            return {
                restrict: 'E',
                scope: {
                    mostrarNucleoSeleccionado: '=bowMostrarnucleoseleccionado',
                    grupoFamiliarIdSeleccionado: '=bowGrupofamiliaridseleccionado',
                    adicionales: '=bowBeneficiosadicionales',
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.cargarNucleosPlan = function (planExequial, grupoFamiliar, planes) {
                        scope.cargarNucleosPlan(planExequial, grupoFamiliar, planes);
                    };
                },
                controller: ['$scope', 'abp.services.app.afiliaciones', function ($scope, afiliacionesService) {

                    $scope.propios = [];
                    $scope.adicionales = [];
                    $scope.planes = [];
                    $scope.grupos = [];
                    $scope.gruposFamiliares = [];

                    //Funcion que se carga desde el formulario principal (clientprospecto), para cargar los planes posibles para el cliente prospecto
                    //y seleccionar el plan si es una gestion prospecto seleccionada
                    $scope.cargarNucleosPlan = function (planExequial, grupoFamiliar, planes) {

                        $scope.mostrarNucleoSeleccionado = false;
                        $scope.gruposFamiliares = [];
                        $scope.planes = [];

                        $scope.planes = planes.planes;
                        $scope.grupos = planes.gruposFamiliares;

                        if (planExequial.id != "") {
                            //Se obtiene los grupos familiares segun el planExequialId que selecciono
                            if (planes.gruposFamiliares.length != 0) {

                                for (i = 0; i < planes.gruposFamiliares.length; i++) {
                                    if (planes.gruposFamiliares[i].planExequialId == planExequial.id) {

                                        $scope.grupo = {
                                            id: planes.gruposFamiliares[i].id,
                                            nombre: planes.gruposFamiliares[i].nombre
                                        };

                                        //Se agregan los gruposFamiliares que pertenecen al planExequial seleccionado y se cargan en el dropdown de nucleos
                                        $scope.gruposFamiliares.push($scope.grupo);
                                    }
                                }
                                $scope.planSelected = planExequial.id;
                                $scope.grupoSelected = grupoFamiliar.id;
                                $scope.changeGrupo();
                            }
                        } else {
                            $scope.planSelected = $scope.planes[0];
                        }
                    };

                    //Funcion que se ejecuta al cambiar el plan exequial (dropdown planes)
                    $scope.changePlan = function () {
                        $scope.mostrarNucleoSeleccionado = false;

                        if ($scope.planSelected != undefined) {
                            $scope.pagoDescuento = 0;
                            $scope.valorAnual = 0;
                            $scope.valorMensual = 0;
                            $scope.cantidadMeses = 0;
                            $scope.cantidadDescuento = 0;

                            $scope.gruposFamiliares = [];

                            //Se obtiene los grupos familiares segun el planExequialId que selecciono
                            if ($scope.grupos.length != 0) {

                                for (i = 0; i < $scope.grupos.length; i++) {
                                    if ($scope.grupos[i].planExequialId == $scope.planSelected) {

                                        $scope.grupo = {
                                            id: $scope.grupos[i].id,
                                            nombre: $scope.grupos[i].nombre
                                        };

                                        //Se agregan los gruposFamiliares que pertenecen al planExequial seleccionado y se cargan en el dropdown de nucleos
                                        $scope.gruposFamiliares.push($scope.grupo);
                                    }
                                }
                                $scope.grupoSelected = $scope.gruposFamiliares[0];
                            }
                        } else {
                            $scope.gruposFamiliares = [];
                        }
                    };

                    //Funcion que se ejecuta al cambiar el núcleo (dropdown núcleos)
                    $scope.changeGrupo = function () {

                        if ($scope.grupoSelected != undefined) {
                            afiliacionesService.getBeneficiosPlan({ id: $scope.grupoSelected, planExequialId: $scope.planSelected })
                                 .success(function (data) {
                                     $scope.valorAnual = data.valorPlan;

                                     $scope.valorMensual = ($scope.valorAnual / 12);
                                     $scope.cantidadMeses = 12;
                                     $scope.cantidadDescuento = 0;

                                     $scope.propios = data.propios;
                                     $scope.adicionales = data.adicionales;

                                     if (data.propios == []) {
                                         $scope.propios = "";
                                     } else if (data.adicionales == []) {
                                         $scope.adicionales = "";
                                     }

                                     $scope.mostrarNucleoSeleccionado = true;

                                     $scope.grupoFamiliarIdSeleccionado = $scope.grupoSelected;

                                 }).error(function (error) {
                                     abp.notify.error(error.message);
                                 });
                        } else {
                            $scope.mostrarNucleoSeleccionado = false;
                            $scope.grupoFamiliarIdSeleccionado = '';
                        }
                    };

                    $scope.calcularValorMensual = function () {
                        $scope.valorMensual = $scope.valorAnual / $scope.cantidadMeses;
                    };

                    $scope.calcularDescuento = function () {
                        var descuento = ($scope.valorAnual * $scope.cantidadDescuento) / 100;
                        $scope.pagoDescuento = $scope.valorAnual - descuento;
                    };

                    $scope.changeAdicional = function (adicionalId, adicionalValor, $index) {

                        if ($scope.adicionales[$index].seleccionado == true) {
                            $scope.valorAnual = $scope.valorAnual + adicionalValor;
                        } else {
                            $scope.valorAnual = $scope.valorAnual - adicionalValor;
                        }

                        $scope.calcularValorMensual();
                        $scope.calcularDescuento();
                    };

                }],
                templateUrl: '/App/Main/views/afiliaciones/directives/bowPlanclienteprospecto/bowPlanClienteProspecto.cshtml'
            };
        })
})();