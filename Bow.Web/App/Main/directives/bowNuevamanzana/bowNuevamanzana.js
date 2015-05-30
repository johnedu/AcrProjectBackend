(function () {
    angular.module('app')
        .directive("bowNuevamanzana", function () {
            return {
                restrict: 'E',
                scope: {
                    controlOperaciones: '=',
                    notificarIngreso: '&bowRegistroManzana',
                    notificarCierreFormulario: '&bowCierreFormulario'
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.asignarBarrio = function (barrioId) {
                        scope.asignarBarrio(barrioId);
                    }
                },
                controller: ['$scope', 'abp.services.app.zonificacion', function ($scope, zonificacionService) {
                    
                    $scope.oculto = {};
                    $scope.nomenclatura = true;
                    $scope.manzanaSaveConNomenclatura = {
                        id: '',
                        torieLocalidad1: '',
                        torieLocalidad2: '',
                        sufijo1: '',
                        sufijo2: '',
                        orientacion1: '',
                        orientacion2: '',
                        barrioId: '',
                        avenida1Id: '',
                        avenida2Id: ''
                    };

                    $scope.manzanaSaveSinNomenclatura = {
                        nombre: '',
                        barrioId: ''
                    };

                    $scope.barrioId = '';

                    /***************************************************
                     * FUNCIONES
                     ***************************************************/
                    ////Cargar Tories de la localidad seleccionada para el dropdow 1 via principal
                    function cargarToriesLocalidad1() {
                        console.log("localidadId", $scope.barrioSeleccionado.localidadId);
                        zonificacionService.getToriesLocalidadByLocalidad({ localidadId: $scope.barrioSeleccionado.localidadId })
                            .success(function (data) {
                                $scope.toriesLocalidad1 = data.toriesLocalidad;
                            });
                    }

                    ////Cargar Tories de la localidad seleccionada para el dropdow 2 via secundaria
                    function cargarToriesLocalidad2(torieId) {
                        zonificacionService.getToriesLocalidadDisponiblesWithTipoOrientacionByTorieLocalidad({ torieLocalidadId: torieId, localidadId: $scope.barrioSeleccionado.localidadId })
                            .success(function (data) {
                                $scope.toriesLocalidad2 = data.toriesLocalidad;
                            });
                    }

                    ////Cargar sufijos de la localidad seleccionada para el dropdow 1 sufijo
                    function cargarSufijosLocalidad() {
                        zonificacionService.getSufijosLocalidadByLocalidad({ localidadId: $scope.barrioSeleccionado.localidadId })
                            .success(function (data) {
                                $scope.sufijosLocalidad1 = data.sufijosLocalidad;
                            });
                    }

                    //Funcion para cargar las avenidas de la localidad en los dropdown avenidas
                    function cargarAvenidasLocalidad() {
                        zonificacionService.getAvenidasByLocalidad({ id: $scope.barrioSeleccionado.localidadId })
                           .success(function (data) {
                               $scope.avenidasLocalidad = data.avenidas;
                           });
                    }

                    

                    /*************************************************************
                     * ASIGNACIÓN DEL BARRIO
                     ************************************************************/
                    $scope.asignarBarrio = function (barrioId) {
                        zonificacionService.getBarrio({ id: barrioId }).success(function (data) {
                            $scope.barrioSeleccionado = data;
                            cargarToriesLocalidad1();
                            cargarSufijosLocalidad();
                            $scope.barrioId = barrioId;
                        });
                    };

                    /**************************************************************
                     * FUNCIONAMIENTO DEL FORMULARIO
                     *************************************************************/
                    //Funcion que se ejecuta al seleccionar un objeto del dropdown via principal
                    $scope.viaPrincipalChange = function () {
                        if ($scope.manzanaSaveConNomenclatura.torieLocalidad1 != null) {
                            zonificacionService.esTipoOrientacionAvenida({ id: $scope.manzanaSaveConNomenclatura.torieLocalidad1 })
                                .success(function (data) {
                                    $scope.mostrarAvenidaPrincipal = data.esAvenida;
                                    if (data.esAvenida == true) {

                                        $scope.manzanaSaveConNomenclatura.orientacion1 = "";
                                        $scope.manzanaSaveConNomenclatura.sufijo1 = "";
                                        //Si selecciono avenida me carga el dropdown con las avenidas de la localidad
                                        cargarAvenidasLocalidad();
                                    }
                                    else {
                                        $scope.manzanaSaveConNomenclatura.avenida1Id = "";
                                        $scope.manzanaSaveConNomenclatura.avenida2Id = "";
                                    }
                                    cargarToriesLocalidad2($scope.manzanaSaveConNomenclatura.torieLocalidad1);
                                });
                        }
                    }

                    function limpiarFormulario() {

                        if (!$scope.nomenclatura) {
                            $scope.manzanaSaveSinNomenclatura = {
                                nombre: '',
                                barrioId: ''
                            };
                            $scope.oculto.frmSinNomenclaturaOculto.$setPristine();
                        }
                        else {
                            $scope.manzanaSaveConNomenclatura = {
                                id: '',
                                torieLocalidad1: '',
                                torieLocalidad2: '',
                                sufijo1: '',
                                sufijo2: '',
                                orientacion1: '',
                                orientacion2: '',
                                barrioId: '',
                                avenida1Id: '',
                                avenida2Id: ''
                            };
                            $scope.oculto.frmConNomenclaturaOculto.$setPristine();
                        }
                    }

                    $scope.cancelarIngresoManzana = function () {
                        $scope.notificarCierreFormulario();
                    }

                    /*************************************************************
                     * REGISTRANDO MANZANA EN EL SISTEMA
                     ************************************************************/
                    $scope.saveManzana = function () {
                        if ($scope.nomenclatura) {
                            guardarManzanaConNomenclatura();
                        }
                        else {
                            guardarManzanaSinNomenclatura();
                        }
                    }

                    function guardarManzanaSinNomenclatura() {
                        $scope.manzanaSaveSinNomenclatura.barrioId = $scope.barrioId;
                        console.log("Manzana sin Nomenclatura", $scope.manzanaSaveSinNomenclatura);
                        zonificacionService.saveManzanaSinNomenclatura($scope.manzanaSaveSinNomenclatura)
                        .success(function () {
                            //$scope.habilitarFormularioManzana(false);
                            abp.notify.info(abp.localization.localize('zonificacion_manzana_notificacionAsignadoSinNomenclatura', 'Bow'), abp.localization.localize('zonificacion_manzana_informacion', 'Bow'));
                            $scope.notificarIngreso({ manzana: $scope.saveManzanaSinNomenclatura });
                            limpiarFormulario();
                        }).error(function (error) {
                            $scope.mensajeError = error.message;
                        });
                    }

                    function guardarManzanaConNomenclatura() {
                        $scope.manzanaSaveConNomenclatura.barrioId = $scope.barrioId;
                        var orientacion1 = null;
                        var orientacion2 = null;

                        //Se valida si se selecciono avenida o se ingreso un número para asignarlo a orientacion1 
                        if ($scope.manzanaSaveConNomenclatura.avenida1Id != "") {
                            orientacion1 = $scope.manzanaSaveConNomenclatura.avenida1Id;
                        } else {
                            orientacion1 = $scope.manzanaSaveConNomenclatura.orientacion1;
                        }

                        //Se valida si se selecciono avenida o se ingreso un número para asignarlo a orientacion2
                        if ($scope.manzanaSaveConNomenclatura.avenida2Id != "") {
                            orientacion2 = $scope.manzanaSaveConNomenclatura.avenida2Id;
                        } else {
                            orientacion2 = $scope.manzanaSaveConNomenclatura.orientacion2;
                        }

                        zonificacionService.saveManzanaConNomenclatura({
                            barrioId: $scope.manzanaSaveConNomenclatura.barrioId,
                            orientacion1: orientacion1,
                            orientacion2: orientacion2,
                            torieLocalidad1Id: $scope.manzanaSaveConNomenclatura.torieLocalidad1,
                            torieLocalidad2Id: $scope.manzanaSaveConNomenclatura.torieLocalidad2,
                            sufijoLocalidad1Id: $scope.manzanaSaveConNomenclatura.sufijo1,
                            sufijoLocalidad2Id: $scope.manzanaSaveConNomenclatura.sufijo2,
                        })

                        .success(function () {
                            //cargarManzanasBarrio();
                            //$scope.habilitarFormularioManzana(false);
                            //$scope.mensajeError = "";
                            //$scope.limpiarFormulario();
                            abp.notify.info(abp.localization.localize('zonificacion_manzana_notificacionAsignadoConNomenclatura', 'Bow'), abp.localization.localize('zonificacion_manzana_informacion', 'Bow'));
                            limpiarFormulario();
                        })
                        .error(function (error) {
                            $scope.mensajeError = error.message;
                        });
                    }

                }],
                templateUrl: '/App/Main/directives/bowNuevamanzana/bowNuevamanzana.cshtml'
            };
        })
})();