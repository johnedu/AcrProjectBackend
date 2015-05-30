(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.afiliaciones.planExequial';

    /*****************************************************************
     * 
     * CONTROLADOR PLAN EXEQUIAL
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', 'abp.services.app.afiliaciones', 'abp.services.app.cartera', 'abp.services.app.parametros', 'abp.services.app.empresas',
        function ($scope, afiliacionesService, carteraService, parametrosService, empresasService) {
            var vm = this;

            /****************************************************************************************************************
            ****************************************** Administración Plan Exequial *****************************************
            ****************************************************************************************************************/

            ////Inicializando modelos

            vm.formularioPlanVisible = false;
            vm.checkActivas = true;
            vm.eliminandoPlanExequial = false;
            vm.cambiandoEstadoPlanExequial = false;
            vm.accionFormulario = '';
            vm.planesExequiales = [];
            vm.tiposMoneda = [];
            vm.planExequialIdSelected = '';
            vm.planExequialNombreSelected = '';

            //  Objeto para agregar o actualizar la empresa
            vm.planExequial = {
                id: '',
                nombre: '',
                descripcion: '',
                planParaGrupo: '',
                planFamiliar: '',
                planEmpresarial: '',
                EstadoId: '',
                MonedaId: '',
                fechaIngreso: null,
                fechaCancelacion: null
            };

            cargarTiposMoneda();
            cargarPlanesExequiales();

            /************************************************************************
             * Controles para editar o eliminar un grupo del plan exequial
             ************************************************************************/
            vm.controlesGrupo = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesGrupo.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesGrupo.visible[$index] = false;
                }
            };

            /************************************************************************
             * Controles para editar o eliminar una empresa del plan exequial
             ************************************************************************/
            vm.controlesEmpresa = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesEmpresa.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesEmpresa.visible[$index] = false;
                }
            };

            /************************************************************************
             * Controles para editar o eliminar un plan exequial
             ************************************************************************/
            vm.controlesPlanExequial = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesPlanExequial.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesPlanExequial.visible[$index] = false;
                    vm.eliminandoPlanExequial = false;
                    vm.cambiandoEstadoPlanExequial = false;
                }
            };
            
            /********************************************************
             * CONTROL PARA MOSTRAR OPCIÓN DE PARÁMETRO SELECCIONADA
             *******************************************************/
            vm.opcionParametro = [true, false, false, false];

            vm.gruposFamiliaresTab = function () {
                vm.opcionParametro = [true, false, false, false];
            }

            vm.beneficiosTab = function () {
                vm.opcionParametro = [false, true, false, false];
            }

            vm.comoercializacionTab = function () {
                vm.opcionParametro = [false, false, true, false];
            }

            vm.recaudoMasivoTab = function () {
                vm.opcionParametro = [false, false, false, true];
            }

            /************************************************************************
             * Llamado para mostrar el formulario del plan exequial
             ************************************************************************/
            vm.mostrarFormularioPlan = function (mostrar) {
                vm.formularioPlanVisible = mostrar
                if (mostrar) {
                    //  Limpiamos el objeto
                    vm.planExequial = {
                        id: '',
                        nombre: '',
                        descripcion: '',
                        planParaGrupo: '',
                        planFamiliar: '',
                        planEmpresarial: '',
                        EstadoId: '',
                        MonedaId: '',
                        fechaIngreso: null,
                        fechaCancelacion: null
                    };
                    $scope.frmRegistrarPlanExequial.$setPristine();
                    vm.accionFormulario = 'Guardar';
                }
                    
            };

            /********************************************************************
             * Cargar el detalle de la información del plan exequial
             ********************************************************************/
            vm.cargarDetallePlanExequial = function (planExequialId, planExequialNombre) {
                vm.planExequialIdSelected = planExequialId;
                vm.planExequialNombreSelected = planExequialNombre;
                obtenerPlanExequial();
                cargarTiposEstados();
                cargarTiposParentesco();
                cargarGruposFamiliares();

                //  Parentescos
                vm.ocultarConsultaParentesco();
                //  Beneficios
                cargarGruposBeneficios();
                vm.radioGrupoBeneficio = '';
                vm.listadoBeneficiosPropios = [];
            }

            /********************************************************************
             * Función para cargar los tipos de moneda
             ********************************************************************/
            function cargarTiposMoneda() {
                carteraService.getAllMoneda()
                    .success(function (data) {
                        vm.tiposMoneda = data.monedas;
                    });
            }

            /********************************************************************
             * Función para cargar los planes exequiales
             ********************************************************************/
            function cargarPlanesExequiales() {
                afiliacionesService.getAllPlanesExequiales()
                    .success(function (data) {
                        vm.planesExequiales = data.planesExequiales;
                    });
            }

            /********************************************************************
             * Funciones para la directiva de eliminar plan exequial
             ********************************************************************/
            vm.puedeEliminarPlan = function (planId, funcionRetornarPuedeEliminar) {
                afiliacionesService.puedeEliminarPlanExequial({ id: planId }).success(function (data) {
                    vm.eliminandoPlanExequial = data.puedeEliminar;
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('afiliaciones_planExequial_tabla_eliminar_datosAsociados', 'Bow'),
                            abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
            }

            vm.eliminarPlan = function (planId) {
                afiliacionesService.deletePlanExequial({ id: planId }).success(function (data) {
                    abp.notify.success(abp.localization.localize('afiliaciones_planExequial_tabla_eliminado_correctamente', 'Bow'),
                        abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                    vm.mensajeError = "";
                    cargarPlanesExequiales();
                    vm.eliminandoPlanExequial = false;
                }).error(function (error) {
                    if (error.validationErrors != null) {
                        $scope.mensajeError = error.validationErrors[0].message;
                    }
                    else {
                        $scope.mensajeError = error.message;
                    }
                });
            }

            vm.cancelarEliminar = function () {
                vm.eliminandoPlanExequial = false;
            };

            vm.ocultarControlesEditarEliminar = function (ocultar) {
                vm.cambiandoEstadoPlanExequial = ocultar;
            };

            /********************************************************************
             * Función para la directiva de editar plan exequial
             ********************************************************************/
            vm.editarPlan = function (planId) {
                afiliacionesService.getPlanExequial({ id: planId })
                    .success(function (data) {
                        vm.planExequial = data;
                        vm.formularioPlanVisible = true;
                        vm.accionFormulario = 'Modificar';
                        $scope.mensajeError = "";
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /********************************************************************
             * Función para guardar el plan exequial
             ********************************************************************/
            function guardarPlanExequial() {
                afiliacionesService.savePlanExequial(vm.planExequial)
                    .success(function (data) {
                        vm.formularioPlanVisible = false;
                        $scope.mensajeError = "";
                        cargarPlanesExequiales();
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /********************************************************************
             * Función para modificar el plan exequial
             ********************************************************************/
            function actualizarPlanExequial() {
                afiliacionesService.updatePlanExequial(vm.planExequial)
                    .success(function (data) {
                        vm.formularioPlanVisible = false;
                        $scope.mensajeError = "";
                        cargarPlanesExequiales();
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /************************************************************************
             * Llamado para la directiva de cambiar estado del plan exequial
             ************************************************************************/
            vm.cambiarEstadoPlan = function (planId, estadoActivo) {
                afiliacionesService.cambiarEstadoPlanExequial({ id: planId, estado: !estadoActivo })
                    .success(function (data) {
                        $scope.mensajeError = "";
                        cargarPlanesExequiales();
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /************************************************************************
             * Llamado guardar o modificar el plan exequial
             ************************************************************************/
            vm.guardarPlanExequial = function () {
                if (vm.planExequial.planParaGrupo == false && vm.planExequial.planFamiliar == false && vm.planExequial.planEmpresarial == false) {
                    abp.notify.error(abp.localization.localize('afiliaciones_planExequial_form_tipo_noSelecciono_ninguno', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                }
                else {
                    if (vm.accionFormulario == 'Guardar')
                        guardarPlanExequial();
                    else
                        actualizarPlanExequial();
                }
            };
            

            /****************************************************************************************************************
            ********************************************* Detalle Plan Exequial *********************************************
            ****************************************************************************************************************/

            ////Inicializando modelos
            vm.formularioGrupoVisible = false;
            vm.formularioParentescoVisible = false;
            vm.formularioConsultaVisible = false;
            vm.eliminandoGrupoInformal = false;
            vm.formularioParentescoNuevoVisible = false;
            vm.eliminadoRangoParentesco = false;

            vm.gruposFamiliares = [];
            vm.tiposEstadoGrupoFamiliar = [];
            vm.rangosGrupoParentesco = [];
            vm.tiposParentesco = [];
            vm.consultaParentescosYRangos = [];

            vm.accionFormulario = '';
            vm.accionFormularioParentesco = '';

            vm.planExequial = {
                id: '',
                nombre: '',
                descripcion: '',
                planParaGrupo: '',
                planFamiliar: '',
                planEmpresarial: '',
                monedaSimbolo: ''
            };

            vm.grupoFamiliar = {
                id: '',
                nombre: '',
                descripcion: '',
                cantidadMaximaAfiliados: '',
                permitirAfiliadosAdicionales: '1',
                valorPlan: '',
                tieneCuotaInicial: '1',
                valorCuotaInicial: '',
                planExequialId: '',
                estadoId: ''
            };

            vm.grupoFamiliarParentesco = {
                id: '',
                parentescoId: '',
                parentesco: '',
                grupoFamiliarId: '',
                validarSoloIngreso: '1'
            };

            vm.grupoParentescoRango = {
                id: '',
                grupoFamiliarParentescoId: '',
                edadMinima: '',
                edadMaxima: '',
                periodoCarencia: '',
                unidadPeriodoCarencia: 'M',
                valorBasico: '',
                tipoValorBasico: 'V',
                valorAdicional: '',
                tipoValorAdicional: 'V'
            };

            /************************************************************************
             * Controles para editar o eliminar un grupo familiar
             ************************************************************************/
            vm.controlesGrupoFamiliar = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesGrupoFamiliar.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesGrupoFamiliar.visible[$index] = false;
                }
            };

            /************************************************************************
             * Controles para editar o eliminar un rango de un grupo familiar
             ************************************************************************/
            vm.controlesRangoGrupo = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesRangoGrupo.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesRangoGrupo.visible[$index] = false;
                    vm.eliminadoRangoParentesco = false;
                }
            };

            /********************************************************************
             * Función para cargar los tipos de estado del grupo familiar
             ********************************************************************/
            function cargarTiposEstados() {
                parametrosService.getAllEstadosGrupoFamiliar()
                    .success(function (data) {
                        vm.tiposEstadoGrupoFamiliar = data.estados;
                    });
            }

            /************************************************************************
             * Obtenemos la información del plan exequial
             ************************************************************************/
            function obtenerPlanExequial() {
                afiliacionesService.getPlanExequialWithMoneda({ id: vm.planExequialIdSelected })
                    .success(function (data) {
                        vm.planExequial.id = vm.planExequialIdSelected;
                        vm.planExequial.nombre = data.nombre;
                        vm.planExequial.descripcion = data.descripcion;
                        vm.planExequial.planParaGrupo = data.planParaGrupo;
                        vm.planExequial.planFamiliar = data.planFamiliar;
                        vm.planExequial.planEmpresarial = data.planEmpresarial;
                        vm.planExequial.monedaSimbolo = data.monedaSimbolo;
                    });
            }

            /********************************************************************
             * Función para cargar los grupos familiares
             ********************************************************************/
            function cargarGruposFamiliares() {
                afiliacionesService.getAllGruposFamiliaresByPlan({ planExequialId: vm.planExequialIdSelected })
                    .success(function (data) {
                        vm.gruposFamiliares = data.gruposFamiliares;
                    });
            }

            /************************************************************************
             * Llamado para mostrar el formulario del grupo familiar
             ************************************************************************/
            vm.mostrarFormularioGrupo = function (mostrar) {
                vm.formularioGrupoVisible = mostrar
                if (mostrar) {
                    vm.formularioParentescoVisible = false;
                    vm.formularioConsultaVisible = false;
                    //  Limpiamos el objeto
                    vm.grupoFamiliar = {
                        id: '',
                        nombre: '',
                        descripcion: '',
                        cantidadMaximaAfiliados: '',
                        permitirAfiliadosAdicionales: '1',
                        valorPlan: '',
                        tieneCuotaInicial: '1',
                        valorCuotaInicial: '',
                        planExequialId: vm.planExequialIdSelected,
                        estadoId: ''
                    };
                    vm.data.frmRegistrarGrupoFamiliar.$setPristine();
                    vm.accionFormularioGrupoFamiliar = 'Guardar';
                }

            };

            /********************************************************************
             * Función para la directiva de editar grupo familiar
             ********************************************************************/
            vm.editarGrupoFamiliar = function (grupoFamiliarId) {
                afiliacionesService.getGrupoFamiliar({ id: grupoFamiliarId })
                    .success(function (data) {
                        vm.grupoFamiliar = data;
                        vm.formularioGrupoVisible = true;
                        vm.formularioParentescoVisible = false;
                        vm.formularioConsultaVisible = false;
                        vm.accionFormularioGrupoFamiliar = 'Modificar';
                        $scope.mensajeError = "";
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /************************************************************************
             * Llamado para guardar o modificar un grupo familiar
             ************************************************************************/
            vm.guardarGrupoFamiliar = function () {
                if (vm.accionFormularioGrupoFamiliar == 'Guardar')
                    guardarGrupoFamiliar();
                else
                    actualizarGrupoFamiliar();
            };

            /********************************************************************
             * Función para guardar el grupo familiar
             ********************************************************************/
            function guardarGrupoFamiliar() {
                afiliacionesService.saveGrupoFamiliar(vm.grupoFamiliar)
                    .success(function (data) {
                        vm.formularioGrupoVisible = false;
                        $scope.mensajeError = "";
                        cargarGruposFamiliares();
                        abp.notify.success(abp.localization.localize('afiliaciones_grupoFamiliar_save_correctamente', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                        vm.mostrarParentesco(data.id, vm.grupoFamiliar.nombre, true);
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /********************************************************************
             * Función para modificar el grupo familiar
             ********************************************************************/
            function actualizarGrupoFamiliar() {
                afiliacionesService.updateGrupoFamiliar(vm.grupoFamiliar)
                    .success(function (data) {
                        vm.formularioGrupoVisible = false;
                        $scope.mensajeError = "";
                        cargarGruposFamiliares();
                        abp.notify.success(abp.localization.localize('afiliaciones_grupoFamiliar_update_correctamente', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /********************************************************************
             * Funciones para la directiva de eliminar grupo familiar
             ********************************************************************/
            vm.puedeEliminarGrupoFamiliar = function (grupoFamiliarId, funcionRetornarPuedeEliminar) {
                afiliacionesService.puedeEliminarGrupoFamiliar({ id: grupoFamiliarId }).success(function (data) {
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('afiliaciones_grupoFamiliar_delete_noSePuede', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
            }

            vm.eliminarGrupoFamiliar = function (grupoFamiliarId) {
                afiliacionesService.deleteGrupoFamiliar({ id: grupoFamiliarId }).success(function (data) {
                    abp.notify.success(abp.localization.localize('afiliaciones_grupoFamiliar_delete_correctamente', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                    vm.mensajeError = "";
                    cargarGruposFamiliares();
                }).error(function (error) {
                    if (error.validationErrors != null) {
                        $scope.mensajeError = error.validationErrors[0].message;
                    }
                    else {
                        $scope.mensajeError = error.message;
                    }
                });
            }

            /************************************************************************
             * Llamado para limpiar la caja de texto de la cuota inicial
             ************************************************************************/
            vm.limpiarCuotaInicial = function () {
                vm.grupoFamiliar.valorCuotaInicial = '';
                vm.data.frmRegistrarGrupoFamiliar.iptValorInicial.$setPristine();
            };

            /********************************************************************
             * Función para cargar los tipos de parentesco
             ********************************************************************/
            function cargarTiposParentesco() {
                afiliacionesService.getAllParentescos()
                    .success(function (data) {
                        vm.tiposParentesco = data.parentescos;
                    });
            }

            /********************************************************************
             * Función para cargar los rangos de parentesco
             ********************************************************************/
            function cargarRangosParentesco() {
                afiliacionesService.getAllRangosParentescoByGrupo({ grupoFamiliarId: vm.grupoFamiliarParentesco.grupoFamiliarId })
                    .success(function (data) {
                        vm.rangosGrupoParentesco = data.rangosParentesco;
                    });
            }

            /********************************************************************
             * Función para cargar los rangos de parentesco
             ********************************************************************/
            function cargarRangosParentescoByParentesco() {
                afiliacionesService.getAllRangosParentescoByGrupoAndParentesco({ parentescoId: vm.grupoFamiliarParentesco.parentescoId, grupoFamiliarId: vm.grupoFamiliarParentesco.grupoFamiliarId })
                    .success(function (data) {
                        vm.rangosGrupoParentesco = data.rangosParentesco;
                    });
            }

            /************************************************************************
             * Llamado para mostrar el formulario de parentescos
             ************************************************************************/
            vm.mostrarParentesco = function (grupoId, grupoNombre, mostrar) {
                vm.formularioParentescoVisible = mostrar
                if (mostrar) {
                    vm.formularioGrupoVisible = false;
                    vm.formularioConsultaVisible = false;

                    vm.grupoFamiliarParentesco.grupoFamiliarId = grupoId;
                    vm.grupoFamiliar.nombre = grupoNombre;
                    vm.formularioParentescoNuevoVisible = false;
                    cargarRangosParentesco();
                }
                else {
                    //  Limpiamos el objeto
                    vm.grupoFamiliarParentesco = {
                        id: '',
                        parentescoId: '',
                        parentesco: '',
                        grupoFamiliarId: '',
                        validarSoloIngreso: '1'
                    };
                    vm.rangosGrupoParentesco = [];
                    vm.ocultarConsultaParentesco();
                }
            };

            /********************************************************************
             * Función para la directiva de editar rango de parentesco
             ********************************************************************/
            vm.ocultarConsultaParentesco = function () {
                vm.formularioGrupoVisible = false;
                vm.formularioParentescoVisible = false
                vm.formularioConsultaVisible = false;
            };

            /********************************************************************
             * Función para la directiva de editar rango de parentesco
             ********************************************************************/
            vm.editarRangoParentesco = function (rangoParentescoId) {
                afiliacionesService.getRangoParentesco({ id: rangoParentescoId })
                    .success(function (data) {
                        vm.grupoParentescoRango = data;
                        vm.grupoFamiliarParentesco.parentescoId = data.parentescoId;
                        vm.formularioParentescoVisible = true;
                        vm.accionFormularioParentesco = 'Modificar';
                        $scope.mensajeError = "";
                        vm.formularioParentescoNuevoVisible = true;
                        cargarRangosParentescoByParentesco();
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /************************************************************************
             * Llamado para guardar o modificar un rango de parentesco
             ************************************************************************/
            vm.guardarRangoParentesco = function () {
                if (vm.accionFormularioParentesco == 'Guardar')
                    guardarRangoParentesco();
                else
                    actualizarRangoParentesco();
            };

            /********************************************************************
             * Función para guardar el rango de parentesco
             ********************************************************************/
            function guardarRangoParentesco() {
                afiliacionesService.saveOrUpdateGrupoFamiliarParentesco({ ParentescoId: vm.grupoFamiliarParentesco.parentescoId, grupoFamiliarId: vm.grupoFamiliarParentesco.grupoFamiliarId, validarSoloIngreso: vm.grupoFamiliarParentesco.validarSoloIngreso })
                    .success(function (data) {
                        vm.grupoParentescoRango.grupoFamiliarParentescoId = data.id;
                        afiliacionesService.saveRangoParentesco(vm.grupoParentescoRango)
                            .success(function (data) {
                                $scope.mensajeError = "";
                                cargarRangosParentesco();
                                vm.mostrarParentesco(vm.grupoFamiliarParentesco.grupoFamiliarId, vm.grupoFamiliar.nombre, true);
                                abp.notify.success(abp.localization.localize('afiliaciones_rangoParentesco_save_correctamente', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                            }).error(function (error) {
                                if (error.validationErrors != null) {
                                    $scope.mensajeError = error.validationErrors[0].message;
                                }
                                else {
                                    $scope.mensajeError = error.message;
                                }
                            });
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /********************************************************************
             * Función para modificar el rango de parentesco
             ********************************************************************/
            function actualizarRangoParentesco() {
                afiliacionesService.saveOrUpdateGrupoFamiliarParentesco({ ParentescoId: vm.grupoFamiliarParentesco.parentescoId, grupoFamiliarId: vm.grupoFamiliarParentesco.grupoFamiliarId, validarSoloIngreso: vm.grupoFamiliarParentesco.validarSoloIngreso })
                    .success(function (data) {
                        vm.grupoParentescoRango.grupoFamiliarParentescoId = data.id;
                        afiliacionesService.updateRangoParentesco(vm.grupoParentescoRango)
                            .success(function (data) {
                                $scope.mensajeError = "";
                                cargarRangosParentesco();
                                vm.mostrarParentesco(vm.grupoFamiliarParentesco.grupoFamiliarId, vm.grupoFamiliar.nombre, true);
                                abp.notify.success(abp.localization.localize('afiliaciones_rangoParentesco_update_correctamente', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                            }).error(function (error) {
                                if (error.validationErrors != null) {
                                    $scope.mensajeError = error.validationErrors[0].message;
                                }
                                else {
                                    $scope.mensajeError = error.message;
                                }
                            });
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /********************************************************************
             * Funciones para la directiva de eliminar rango de parentesco
             ********************************************************************/
            vm.puedeEliminarRangoParentesco = function (rangoParentescoId, funcionRetornarPuedeEliminar) {
                afiliacionesService.puedeEliminarRangoParentesco({ id: rangoParentescoId }).success(function (data) {
                    vm.eliminadoRangoParentesco = data.puedeEliminar;
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminarRangoParentesco = function () {
                abp.notify.error(abp.localization.localize('afiliaciones_rangoParentesco_delete_noSePuede', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
            }

            vm.eliminarRangoParentesco = function (rangoParentescoId) {
                afiliacionesService.deleteRangoParentesco({ id: rangoParentescoId }).success(function (data) {
                    abp.notify.success(abp.localization.localize('afiliaciones_rangoParentesco_delete_correctamente', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                    vm.mensajeError = "";
                    cargarRangosParentesco();
                    vm.eliminadoRangoParentesco = false;
                }).error(function (error) {
                    if (error.validationErrors != null) {
                        $scope.mensajeError = error.validationErrors[0].message;
                    }
                    else {
                        $scope.mensajeError = error.message;
                    }
                });
            }

            vm.cancelarEliminarRangoParentesco = function () {
                vm.eliminadoRangoParentesco = false;
            }

            /************************************************************************
             * Llamado para cambiar el formulario de parentesco del grupo familiar
             ************************************************************************/
            vm.cambiarParentescoGrupoFamiliar = function () {
                if (vm.grupoFamiliarParentesco.parentescoId) {
                    cargarRangosParentescoByParentesco();
                }
                else {
                    cargarRangosParentesco();
                }
            };

            /************************************************************************
             * Llamado para mostrar el grupo familiar a modo consulta con los parentescos y los rangos
             ************************************************************************/
            vm.consultarParentescosYRangos = function (grupoFamiliarId) {
                afiliacionesService.getGrupoFamiliar({ id: grupoFamiliarId })
                    .success(function (data) {
                        vm.grupoFamiliar = data;
                        $scope.mensajeError = "";
                        afiliacionesService.getAllParentescosAllRangos({ planExequialId: vm.planExequialIdSelected, grupoFamiliarId: grupoFamiliarId })
                            .success(function (data) {
                                vm.consultaParentescosYRangos = data;
                                vm.formularioConsultaVisible = true;
                                vm.formularioGrupoVisible = false;
                                vm.formularioParentescoVisible = false;
                            }).error(function (error) {
                                if (error.validationErrors != null) {
                                    $scope.mensajeError = error.validationErrors[0].message;
                                }
                                else {
                                    $scope.mensajeError = error.message;
                                }
                            });
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            };

            /************************************************************************
             * Llamado para mostrar el formulario de nuevo parentesco
             ************************************************************************/
            vm.verFormularioNuevoParentesco = function (mostrar) {
                vm.formularioParentescoNuevoVisible = mostrar;
                if (mostrar) {
                    //  Limpiamos el objeto
                    vm.grupoParentescoRango = {
                        id: '',
                        grupoFamiliarParentescoId: '',
                        edadMinima: '',
                        edadMaxima: '',
                        periodoCarencia: '',
                        unidadPeriodoCarencia: 'M',
                        valorBasico: '',
                        tipoValorBasico: 'V',
                        valorAdicional: '',
                        tipoValorAdicional: 'V'
                    };
                    vm.grupoFamiliarParentesco.parentescoId = '';
                    vm.data.frmRegistrarParentescoGrupoFamiliar.$setPristine();
                    vm.accionFormularioParentesco = 'Guardar';
                }
                cargarRangosParentesco();
            }

            //*****************************************************************************************************************************************************//
            //********************************************************************* Beneficios ********************************************************************//
            //*****************************************************************************************************************************************************//

            ////Inicializando modelos

            vm.limpiarCatagoriasBeneficios = {};

            vm.tiposEstadoBeneficiosPlanExequial = [];
            vm.listadoBeneficios = [];
            vm.listadoTodosLosBeneficios = [];
            vm.listadoBeneficiosPropios = [];

            vm.formularioSeccionBeneficioVisible = false;
            vm.formularioBeneficioVisible = false;
            vm.formularioNuevoBeneficioVisible = false;
            vm.confirmacionBeneficioPropioVisible = false;
            vm.mostrarMensajeCategoriaYaAsignada = '';
            vm.radioGrupoBeneficio = '';

            vm.accionFormularioBeneficio = '';

            vm.grupoBeneficioIdSeleccionado = '';
            vm.grupoBeneficioNombreSeleccionado = '';

            vm.beneficio = {
                beneficioId: '',
                beneficioNombre: '',
                beneficioDescripcion: '',
                categoriaId: '',
                categoriaNombre: ''
            };

            vm.beneficioPlanExequial = {
                id: '',
                planExequialId: '',
                beneficioId: '',
                esAsignable: 'false',
                asignables: '0',
                fechaIngreso: '',
                fechaCancelacion: '',
                estadoId: ''
            };

            vm.beneficioAdicionalPlanExequial = {
                id: '',
                planExequialId: '',
                beneficioId: '',
                esAsignable: 'false',
                asignables: '0',
                fechaIngreso: '',
                fechaCancelacion: '',
                valor: '',
                estadoId: '',
                beneficioPlanExequialId: ''
            };

            cargarTiposEstadosBeneficiosPlanExequial();

            /************************************************************************
             * Controles para editar o eliminar un beneficio
             ************************************************************************/
            vm.controlesBeneficio = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesBeneficio.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesBeneficio.visible[$index] = false;
                }
            };

            /********************************************************************
             * Función para cargar los grupos de beneficios
             ********************************************************************/
            function cargarGruposBeneficios() {
                afiliacionesService.getAllTiposBeneficioPlanExequial({ PlanExequialId: vm.planExequialIdSelected })
                    .success(function (data) {
                        vm.gruposBeneficios = data.tiposBeneficioPlanExequial;
                    });
            }

            /************************************************************************
             * Llamado para asignar el beneficio que viene desde la directiva de categoria beneficios
             ************************************************************************/
            vm.mostrarBeneficios = function (beneficio) {
                //  Asignamos el objeto beneficio seleccionado
                vm.beneficio = beneficio;

                //  Limpiamos el formulario de beneficios
                vm.beneficioPlanExequial = {
                    id: '',
                    planExequialId: '',
                    beneficioId: '',
                    esAsignable: 'false',
                    asignables: '0',
                    fechaIngreso: '',
                    fechaCancelacion: '',
                    estadoId: ''
                };

                vm.beneficioAdicionalPlanExequial = {
                    id: '',
                    planExequialId: '',
                    beneficioId: '',
                    esAsignable: 'false',
                    asignables: '0',
                    fechaIngreso: '',
                    fechaCancelacion: '',
                    valor: '',
                    estadoId: '',
                    beneficioPlanExequialId: ''
                };

                if (vm.grupoBeneficioNombreSeleccionado == 'Propio') {
                    vm.beneficioPlanExequial.valor = 0;
                }

                vm.data.frmRegistrarBeneficio.$setPristine();
                vm.accionFormularioBeneficio = 'Registrar';

                vm.mostrarFormularioNuevoBeneficio(true);
                vm.listarBeneficiosPropios(vm.beneficio.categoriaId);
            };

            /************************************************************************
             * Llamado para consultar la lista de beneficios propios para reemplazar
             ************************************************************************/
            vm.listarBeneficiosPropios = function (categoriaId) {
                afiliacionesService.getAllBeneficiosPlanExequialByTipoAndBeneficioAdicional({ planExequialId: vm.planExequialIdSelected, tipoId: categoriaId, beneficioAdicionalId: vm.beneficio.beneficioId })
                    .success(function (data) {
                        vm.listadoBeneficiosPropios = data.beneficios;
                    });
            };

            /************************************************************************
             * Llamado para mostrar la directiva de categoria beneficios
             ************************************************************************/
            vm.mostrarDirectivaBeneficios = function (mostrar) {
                vm.formularioBeneficioVisible = mostrar
                if (mostrar) {
                    vm.formularioParentescoVisible = false;
                    vm.formularioGrupoVisible = false;
                    vm.formularioConsultaVisible = false;
                    vm.formularioNuevoBeneficioVisible = false;
                    vm.mostrarMensajeCategoriaYaAsignada = '';
                    vm.limpiarCatagoriasBeneficios.limpiar();
                }
            };

            /************************************************************************
             * Llamado para mostrar el formulario de beneficios
             ************************************************************************/
            vm.mostrarFormularioNuevoBeneficio = function (mostrar) {
                vm.formularioNuevoBeneficioVisible = mostrar
            };

            /********************************************************************
             * Función para cargar los beneficios de un plan exequial
             ********************************************************************/
            function cargarBeneficiosPlanExequialByTipo() {
                vm.listadoBeneficios = [];
                vm.listadoTodosLosBeneficios = [];
                afiliacionesService.getAllBeneficiosPlanExequial({ planExequialId: vm.planExequialIdSelected })
                    .success(function (data) {
                        if (vm.grupoBeneficioNombreSeleccionado == 'Propio') {
                            vm.listadoBeneficios = data.beneficios;
                        }
                        vm.listadoTodosLosBeneficios = data.beneficios;
                        afiliacionesService.getAllBeneficiosAdicionalesPlanExequial({ planExequialId: vm.planExequialIdSelected })
                            .success(function (data) {
                                vm.listadoTodosLosBeneficios = vm.listadoTodosLosBeneficios.concat(data.beneficios);
                            });
                    });
                if (vm.grupoBeneficioNombreSeleccionado == 'Adicional') {
                    afiliacionesService.getAllBeneficiosAdicionalesPlanExequial({ planExequialId: vm.planExequialIdSelected })
                    .success(function (data) {
                        vm.listadoBeneficios = data.beneficios;
                    });
                }
            }

            /********************************************************************
             * Función para cargar los tipos de estado del grupo familiar
             ********************************************************************/
            function cargarTiposEstadosBeneficiosPlanExequial() {
                parametrosService.getAllEstadosBeneficiosPlanExequial()
                    .success(function (data) {
                        vm.tiposEstadoBeneficiosPlanExequial = data.estados;
                    });
            }

            /********************************************************************
             * Función para mostrar el formulario con el beneficio seleccionado
             ********************************************************************/
            vm.consultarBeneficiosPlanExequial = function (grupoBeneficioId, grupoBeneficioNombre) {
                vm.formularioSeccionBeneficioVisible = true;
                vm.formularioBeneficioVisible = false;
                vm.grupoBeneficioIdSeleccionado = grupoBeneficioId;
                vm.grupoBeneficioNombreSeleccionado = grupoBeneficioNombre;
                vm.limpiarCatagoriasBeneficios.limpiar();
                cargarBeneficiosPlanExequialByTipo();
            }

            /********************************************************************
             * Función para la directiva de editar beneficio del plan exequial
             ********************************************************************/
            vm.editarBeneficioPlanExequial = function (beneficioPlanExequialId) {
                if (vm.grupoBeneficioNombreSeleccionado == 'Propio') {
                    afiliacionesService.getBeneficioPlanExequial({ id: beneficioPlanExequialId })
                        .success(function (data) {
                            vm.beneficio.beneficioNombre = data.nombreBeneficio;
                            vm.beneficio.beneficioDescripcion = data.descripcionBeneficio;
                            vm.beneficio.categoriaNombre = data.categoriaBeneficioNombre;

                            vm.beneficioPlanExequial = {
                                id: data.id,
                                planExequialId: data.planExequialId,
                                beneficioId: data.beneficioId,
                                esAsignable: data.esAsignable,
                                asignables: data.asignables,
                                fechaIngreso: data.fechaIngreso,
                                fechaCancelacion: data.fechaCancelacion,
                                estadoId: data.estadoId
                            };

                            vm.accionFormularioBeneficio = 'Modificar';
                            vm.formularioNuevoBeneficioVisible = true;
                            vm.formularioBeneficioVisible = true;

                            $scope.mensajeError = "";
                        }).error(function (error) {
                            if (error.validationErrors != null) {
                                $scope.mensajeError = error.validationErrors[0].message;
                            }
                            else {
                                $scope.mensajeError = error.message;
                            }
                        });
                }
                else {
                    afiliacionesService.getBeneficioAdicionalPlanExequial({ id: beneficioPlanExequialId })
                        .success(function (data) {
                            vm.beneficio.beneficioNombre = data.nombreBeneficio;
                            vm.beneficio.beneficioDescripcion = data.descripcionBeneficio;
                            vm.beneficio.categoriaNombre = data.categoriaBeneficioNombre;
                            vm.listarBeneficiosPropios(data.categoriaBeneficioId);

                            vm.beneficioAdicionalPlanExequial = {
                                id: data.id,
                                planExequialId: data.planExequialId,
                                beneficioId: data.beneficioId,
                                esAsignable: data.esAsignable,
                                asignables: data.asignables,
                                fechaIngreso: data.fechaIngreso,
                                fechaCancelacion: data.fechaCancelacion,
                                valor: data.valor,
                                estadoId: data.estadoId,
                                beneficioPlanExequialId: data.beneficioPlanExequialId
                            };
                            vm.accionFormularioBeneficio = 'Modificar';
                            vm.formularioNuevoBeneficioVisible = true;
                            vm.formularioBeneficioVisible = true;

                            $scope.mensajeError = "";
                        }).error(function (error) {
                            if (error.validationErrors != null) {
                                $scope.mensajeError = error.validationErrors[0].message;
                            }
                            else {
                                $scope.mensajeError = error.message;
                            }
                        });
                }
            }

            /************************************************************************
             * Llamado para guardar o modificar un beneficio del plan exequial
             ************************************************************************/
            vm.guardarBeneficioPlanExequial = function () {
                if (vm.accionFormularioBeneficio == 'Registrar')
                    guardarBeneficioPlanExequial();
                else
                    actualizarBeneficioPlanExequial();
            };

            /********************************************************************
             * Función para guardar el beneficio del plan exequial
             ********************************************************************/
            function guardarBeneficioPlanExequial() {
                vm.beneficioPlanExequial.beneficioId = vm.beneficio.beneficioId;
                vm.beneficioPlanExequial.planExequialId = vm.planExequialIdSelected;
                vm.beneficioAdicionalPlanExequial.beneficioId = vm.beneficio.beneficioId;
                vm.beneficioAdicionalPlanExequial.planExequialId = vm.planExequialIdSelected;
                if (vm.grupoBeneficioNombreSeleccionado == 'Propio') {
                    afiliacionesService.saveBeneficioPlanExequial(vm.beneficioPlanExequial)
                        .success(function (data) {
                            cargarGruposBeneficios();
                            cargarBeneficiosPlanExequialByTipo();
                            $scope.mensajeError = "";
                            abp.notify.success(abp.localization.localize('afiliaciones_beneficios_guardar_mensajeCorrecto', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                            vm.formularioNuevoBeneficioVisible = false;
                            vm.formularioBeneficioVisible = false;
                        }).error(function (error) {
                            if (error.validationErrors != null) {
                                $scope.mensajeError = error.validationErrors[0].message;
                            }
                            else {
                                $scope.mensajeError = error.message;
                            }
                        });
                }
                else {
                    afiliacionesService.saveBeneficioAdicionalPlanExequial(vm.beneficioAdicionalPlanExequial)
                        .success(function (data) {
                            cargarGruposBeneficios();
                            cargarBeneficiosPlanExequialByTipo();
                            $scope.mensajeError = "";
                            abp.notify.success(abp.localization.localize('afiliaciones_beneficios_guardar_mensajeCorrecto', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                            vm.formularioNuevoBeneficioVisible = false;
                            vm.formularioBeneficioVisible = false;
                        }).error(function (error) {
                            if (error.validationErrors != null) {
                                $scope.mensajeError = error.validationErrors[0].message;
                            }
                            else {
                                $scope.mensajeError = error.message;
                            }
                        });
                }
            }

            /********************************************************************
             * Función para modificar el beneficio del plan exequial
             ********************************************************************/
            function actualizarBeneficioPlanExequial() {
                if (vm.grupoBeneficioNombreSeleccionado == 'Propio') {
                    if (!vm.beneficioPlanExequial.esAsignable) {
                        vm.beneficioPlanExequial.asignables = 0;
                    }
                    afiliacionesService.updateBeneficioPlanExequial(vm.beneficioPlanExequial)
                        .success(function (data) {
                            $scope.mensajeError = "";
                            cargarBeneficiosPlanExequialByTipo();
                            abp.notify.success(abp.localization.localize('afiliaciones_beneficios_actualizar_mensajeCorrecto', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                            vm.formularioNuevoBeneficioVisible = false;
                            vm.formularioBeneficioVisible = false;
                        }).error(function (error) {
                            if (error.validationErrors != null) {
                                $scope.mensajeError = error.validationErrors[0].message;
                            }
                            else {
                                $scope.mensajeError = error.message;
                            }
                        });
                }
                else {
                    if (!vm.beneficioAdicionalPlanExequial.esAsignable) {
                        vm.beneficioAdicionalPlanExequial.asignables = 0;
                    }
                    afiliacionesService.updateBeneficioAdicionalPlanExequial(vm.beneficioAdicionalPlanExequial)
                        .success(function (data) {
                            $scope.mensajeError = "";
                            cargarBeneficiosPlanExequialByTipo();
                            abp.notify.success(abp.localization.localize('afiliaciones_beneficios_actualizar_mensajeCorrecto', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                            vm.formularioNuevoBeneficioVisible = false;
                            vm.formularioBeneficioVisible = false;
                        }).error(function (error) {
                            if (error.validationErrors != null) {
                                $scope.mensajeError = error.validationErrors[0].message;
                            }
                            else {
                                $scope.mensajeError = error.message;
                            }
                        });
                }

            }

            /********************************************************************
             * Funciones para la directiva de eliminar beneficios del plan exequial
             ********************************************************************/
            vm.puedeEliminarBeneficio = function (beneficioPlanExequialId, funcionRetornarPuedeEliminar) {
                afiliacionesService.puedeEliminarBeneficioPlanExequial({ id: beneficioPlanExequialId }).success(function (data) {
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminarBeneficios = function () {
                abp.notify.error(abp.localization.localize('afiliaciones_beneficios_delete_noSePuede', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
            }

            vm.eliminarBeneficio = function (beneficioPlanExequialId) {
                afiliacionesService.deleteBeneficioPlanExequial({ id: beneficioPlanExequialId }).success(function (data) {
                    abp.notify.success(abp.localization.localize('afiliaciones_beneficios_delete_correctamente', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                    vm.mensajeError = "";
                    cargarGruposBeneficios();
                    cargarBeneficiosPlanExequialByTipo();
                }).error(function (error) {
                    if (error.validationErrors != null) {
                        $scope.mensajeError = error.validationErrors[0].message;
                    }
                    else {
                        $scope.mensajeError = error.message;
                    }
                });
            };

            vm.mostrarMensajeCategoria = function (mensaje) {
                if (mensaje == 'Propio') {
                    vm.mostrarMensajeCategoriaYaAsignada = 'P';
                    abp.notify.warn(abp.localization.localize('afiliaciones_beneficios_categoria_beneficioYaSeleccionado', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                } else if (mensaje == 'Adicional') {
                    vm.mostrarMensajeCategoriaYaAsignada = 'A';
                    abp.notify.warn(abp.localization.localize('afiliaciones_beneficios_categoria_beneficioYaSeleccionado_comoAdicional', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                }
            };

            vm.mostrarConfirmacionBeneficioPropio = function (mostrar) {
                if ((vm.grupoBeneficioNombreSeleccionado == 'Propio' || vm.grupoBeneficioNombreSeleccionado == 'Adicional') && vm.mostrarMensajeCategoriaYaAsignada != '') {
                    vm.confirmacionBeneficioPropioVisible = mostrar;
                }
                else {
                    vm.guardarBeneficioPlanExequial();
                }
            };

        }]);
})();