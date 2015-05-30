(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.afiliaciones.detallePlanExequial';

    /*****************************************************************
     * 
     * CONTROLADOR DETALLE DEL PLAN EXEQUIAL
     * 
     *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$modal', '$location', '$stateParams', 'abp.services.app.afiliaciones', 'abp.services.app.cartera', 'abp.services.app.parametros', 'abp.services.app.empresas',
        function ($scope, $modal, $location, $stateParams, afiliacionesService, carteraService, parametrosService, empresasService) {
            var vm = this;

            //*****************************************************************************************************************************************************//
            //***************************************************************** Grupos Familiares *****************************************************************//
            //*****************************************************************************************************************************************************//

            ////Inicializando modelos
            vm.formularioGrupoVisible = false;
            vm.formularioParentescoVisible = false;
            vm.formularioConsultaVisible = false;

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
                planExequialId: $stateParams.planId,
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

            obtenerPlanExequial();
            cargarTiposEstados();
            cargarTiposParentesco();
            cargarGruposFamiliares();

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
                afiliacionesService.getPlanExequialWithMoneda({ id: $stateParams.planId })
                    .success(function (data) {
                        vm.planExequial.id = $stateParams.planId;
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
                afiliacionesService.getAllGruposFamiliaresByPlan({ planExequialId: $stateParams.planId })
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
                        planExequialId: $stateParams.planId,
                        estadoId: ''
                    };
                    vm.data.frmRegistrarGrupoFamiliar.$setPristine();
                    vm.accionFormulario = 'Guardar';
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

            /************************************************************************
             * Llamado para guardar o modificar un grupo familiar
             ************************************************************************/
            vm.guardarGrupoFamiliar = function () {
                if (vm.accionFormulario == 'Guardar')
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
                afiliacionesService.getAllRangosParentesco({ parentescoId: vm.grupoFamiliarParentesco.parentescoId, grupoFamiliarId: vm.grupoFamiliarParentesco.grupoFamiliarId })
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

                    vm.data.frmRegistrarParentescoGrupoFamiliar.$setPristine();
                    vm.accionFormularioParentesco = 'Guardar';
                }
                else {
                    vm.consultarParentescosYRangos(vm.grupoFamiliarParentesco.grupoFamiliarId)
                    vm.formularioConsultaVisible = true;
                }
            };

            /********************************************************************
             * Función para la directiva de editar rango de parentesco
             ********************************************************************/
            vm.editarRangoParentesco = function (rangoParentescoId) {
                afiliacionesService.getRangoParentesco({ id: rangoParentescoId })
                    .success(function (data) {
                        vm.grupoParentescoRango = data;
                        vm.formularioParentescoVisible = true;
                        vm.accionFormularioParentesco = 'Modificar';
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
             * Llamado para cambiar el formulario de parentesco del grupo familiar
             ************************************************************************/
            vm.cambiarParentescoGrupoFamiliar = function () {
                vm.mostrarParentesco(vm.grupoFamiliarParentesco.grupoFamiliarId, vm.grupoFamiliar.nombre, true);

                afiliacionesService.getGrupoFamiliarParentesco({ parentescoId: vm.grupoFamiliarParentesco.parentescoId, grupoFamiliarId: vm.grupoFamiliarParentesco.grupoFamiliarId })
                    .success(function (data) {
                        vm.grupoFamiliarParentesco.validarSoloIngreso = data.validarSoloIngreso;
                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
                cargarRangosParentesco();
            };

            /************************************************************************
             * Llamado para mostrar el grupo familiar a modo consulta con los parentescos y los rangos
             ************************************************************************/
            vm.consultarParentescosYRangos = function (grupoFamiliarId) {
                afiliacionesService.getGrupoFamiliar({ id: grupoFamiliarId })
                    .success(function (data) {
                        vm.grupoFamiliar = data;
                        $scope.mensajeError = "";
                        afiliacionesService.getAllParentescosAllRangos({ planExequialId: $stateParams.planId, grupoFamiliarId: grupoFamiliarId })
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
                esAsignable: false,
                asignables: '0',
                fechaIngreso: '',
                fechaCancelacion: '',
                estadoId: ''
            };

            vm.beneficioAdicionalPlanExequial = {
                id: '',
                planExequialId: '',
                beneficioId: '',
                esAsignable: false,
                asignables: '0',
                fechaIngreso: '',
                fechaCancelacion: '',
                valor: '',
                estadoId: '',
                beneficioPlanExequialId: ''
            };

            cargarGruposBeneficios();
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
                afiliacionesService.getAllTiposBeneficioPlanExequial({ PlanExequialId: $stateParams.planId })
                    .success(function (data) {
                        console.log(data);
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
                    esAsignable: false,
                    asignables: '0',
                    fechaIngreso: '',
                    fechaCancelacion: '',
                    estadoId: ''
                };

                vm.beneficioAdicionalPlanExequial = {
                    id: '',
                    planExequialId: '',
                    beneficioId: '',
                    esAsignable: false,
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
                afiliacionesService.getAllBeneficiosPlanExequialByTipo({ planExequialId: $stateParams.planId, tipoId: categoriaId })
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
                afiliacionesService.getAllBeneficiosPlanExequial({ planExequialId: $stateParams.planId })
                    .success(function (data) {
                        if (vm.grupoBeneficioNombreSeleccionado == 'Propio') {
                            vm.listadoBeneficios = data.beneficios;
                        }
                        vm.listadoTodosLosBeneficios = data.beneficios;
                        afiliacionesService.getAllBeneficiosAdicionalesPlanExequial({ planExequialId: $stateParams.planId })
                            .success(function (data) {
                                vm.listadoTodosLosBeneficios = vm.listadoTodosLosBeneficios.concat(data.beneficios);
                            });
                    });
                if (vm.grupoBeneficioNombreSeleccionado == 'Adicional') {
                    afiliacionesService.getAllBeneficiosAdicionalesPlanExequial({ planExequialId: $stateParams.planId })
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
                vm.beneficioPlanExequial.planExequialId = $stateParams.planId;
                vm.beneficioAdicionalPlanExequial.beneficioId = vm.beneficio.beneficioId;
                vm.beneficioAdicionalPlanExequial.planExequialId = $stateParams.planId;
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

            //*****************************************************************************************************************************************************//
            //********************************************************************* Sucursales ********************************************************************//
            //*****************************************************************************************************************************************************//

            ////Inicializando modelos

            vm.listadoEmpresasSucursales = [];
            vm.listadoSucursales = [];
            var listadoSucursalesInicial = [];

            vm.empresaSeleccionada = '';
            vm.checkSelectAll = false;

            /************************************************************************
             * Controles para el checkbox de sucursales del plan exequial
             ************************************************************************/
            controlesCheckboxIniciales = [];
            vm.controlesCheckboxModificado = [];

            /********************************************************************
             * Función para cargar las empresas con las sucursales que pueden comercializar y las que no
             ********************************************************************/
            function cargarEmpresasPlanExequial() {
                empresasService.getOrganizacion({ id: 0 })
                    .success(function (data) {
                        afiliacionesService.getAllEmpresasPlanExequial({ PlanExequialId: $stateParams.planId, OrganizacionId: data.id })
                            .success(function (data2) {
                                vm.listadoEmpresasSucursales = bow.tablas.paginar(data2.empresasPlanExequial, 10);
                            });
                    });
            }
            cargarEmpresasPlanExequial();

            /********************************************************************
             * Función para cargar las empresas con las sucursales que pueden comercializar y las que no
             ********************************************************************/
            vm.cargarSucursalesPlanExequial = function (empresaOrganizacionId, empresaNombre) {
                vm.empresaSeleccionada = empresaNombre;
                afiliacionesService.getAllSucursalesPlanExequial({ PlanExequialId: $stateParams.planId, EmpresaOrganizacionId: empresaOrganizacionId })
                    .success(function (data) {
                        listadoSucursalesInicial = data.sucursalesPlanExequial;
                        vm.listadoSucursales = bow.tablas.paginar(data.sucursalesPlanExequial, 10);
                        for (var index = 0; index < data.sucursalesPlanExequial.length; index++) {
                            controlesCheckboxIniciales[index] = data.sucursalesPlanExequial[index].sucursalesAsignada;
                        }
                        vm.controlesCheckboxModificado = controlesCheckboxIniciales.slice(0);
                    });
            }

            vm.sucursalesSeleccionarTodas = function () {
                for (var index = 0; index < vm.controlesCheckboxModificado.length; index++) {
                    vm.controlesCheckboxModificado[index] = vm.checkSelectAll;
                }
            };

            vm.guardarSucursalesPlanExequial = function () {
                var listadoSucursalesModificado = {
                    listaSucursales: []
                };
                for (var index = 0; index < vm.controlesCheckboxModificado.length; index++) {
                    if (vm.controlesCheckboxModificado[index] != controlesCheckboxIniciales[index]) {
                        listadoSucursalesModificado.listaSucursales.push({ planExequialId: $stateParams.planId, sucursalId: listadoSucursalesInicial[index].sucursalId, asignadoAlPlan: vm.controlesCheckboxModificado[index] });
                    }
                }
                afiliacionesService.updateSucursalesPlanExequial(listadoSucursalesModificado)
                    .success(function (data2) {
                        abp.notify.success(abp.localization.localize('afiliaciones_detallePlanExequial_update_correcto', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                        cargarEmpresasPlanExequial();
                    });
            };


            //*****************************************************************************************************************************************************//
            //******************************************************************* Recaudo Masivo ******************************************************************//
            //*****************************************************************************************************************************************************//

            ////Inicializando modelos

            vm.listadoConveniosRecaudo = [];
            vm.listadoRecaudosMasivosAsignados = [];

            vm.recaudoMasivo = {
                id: '',
                planExequialId: '',
                recaudoMasivoId: '',
                esObligatorio: false
            };

            vm.formularioConvenioRecaudoVisible = false;
            vm.accionFormularioConvenioRecaudo = '';

            /********************************************************************
             * Función para cargar los convenios de recaudo
             ********************************************************************/
            function cargarConveniosRecaudo() {
                afiliacionesService.getAllConveniosRecaudoMasivoNoAsignados({ planExequialId: $stateParams.planId })
                    .success(function (data) {
                        vm.listadoConveniosRecaudo = data.conveniosRecaudo;
                    });
            }

            /********************************************************************
             * Función para cargar los convenios de recaudo masivo asignados
             ********************************************************************/
            function cargarConveniosRecaudoAsignados() {
                afiliacionesService.getAllPlanExequialRecaudoMasivo({ planExequialId: $stateParams.planId })
                    .success(function (data) {
                        vm.listadoRecaudosMasivosAsignados = data.planExequialRecaudos;
                    });
            }
            cargarConveniosRecaudoAsignados();

            /************************************************************************
             * Llamado para mostrar el formulario de convenios de recaudo
             ************************************************************************/
            vm.mostrarFormularioRecaudoMasivo = function (mostrar) {
                vm.formularioConvenioRecaudoVisible = mostrar
                if (mostrar) {
                    vm.accionFormularioConvenioRecaudo = 'Guardar';
                    vm.recaudoMasivo = {
                        id: '',
                        planExequialId: '',
                        recaudoMasivoId: '',
                        esObligatorio: false
                    };
                    vm.data.frmRegistrarRecaudo.$setPristine();
                    cargarConveniosRecaudo();
                }
            };

            /********************************************************************
             * Función para la directiva de editar convenio de recaudo
             ********************************************************************/
            vm.editarRecaudoMasivo = function (recaudoMasivoId) {
                afiliacionesService.getPlanExequialRecaudoMasivo({ id: recaudoMasivoId })
                    .success(function (data) {
                        vm.recaudoMasivo = data;
                        vm.accionFormularioConvenioRecaudo = 'Modificar';
                        $scope.mensajeError = "";
                        vm.formularioConvenioRecaudoVisible = true;
                        vm.listadoConveniosRecaudo.push({ nombre: data.recaudoMasivoNombre, clave: data.recaudoMasivoClave, id: data.recaudoMasivoId });
                        vm.recaudoMasivo.recaudoMasivoId = data.recaudoMasivoId;
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
            vm.guardarConvenioRecaudo = function () {
                vm.recaudoMasivo.planExequialId = $stateParams.planId;
                if (vm.accionFormularioConvenioRecaudo == 'Guardar')
                    guardarConvenioRecaudo();
                else
                    actualizarConvenioRecaudo();
            };

            /********************************************************************
             * Función para guardar el grupo familiar
             ********************************************************************/
            function guardarConvenioRecaudo() {
                afiliacionesService.savePlanExequialRecaudoMasivo(vm.recaudoMasivo)
                    .success(function (data) {
                        vm.formularioConvenioRecaudoVisible = false;
                        $scope.mensajeError = "";
                        cargarConveniosRecaudoAsignados();
                        cargarConveniosRecaudo();
                        abp.notify.success(abp.localization.localize('afiliaciones_detallePlanExequial_recaudoMasivo_save_correctamente', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
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
            function actualizarConvenioRecaudo() {
                afiliacionesService.updatePlanExequialRecaudoMasivo(vm.recaudoMasivo)
                    .success(function (data) {
                        vm.formularioConvenioRecaudoVisible = false;
                        $scope.mensajeError = "";
                        cargarConveniosRecaudoAsignados();
                        cargarConveniosRecaudo();
                        abp.notify.success(abp.localization.localize('afiliaciones_detallePlanExequial_recaudoMasivo_update_correctamente', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
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
            vm.puedeEliminarConvenioRecaudo = function (recaudoMasivoId, funcionRetornarPuedeEliminar) {
                afiliacionesService.puedeEliminarPlanExequialRecaudoMasivo({ id: recaudoMasivoId }).success(function (data) {
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminarConvenioRecaudo = function () {
                abp.notify.error(abp.localization.localize('afiliaciones_detallePlanExequial_recaudoMasivo_delete_noSePuede', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
            }

            vm.eliminarConvenioRecaudo = function (recaudoMasivoId) {
                afiliacionesService.deletePlanExequialRecaudoMasivo({ id: recaudoMasivoId }).success(function (data) {
                    abp.notify.success(abp.localization.localize('afiliaciones_detallePlanExequial_recaudoMasivo_delete_correctamente', 'Bow'), abp.localization.localize('afiliaciones_planExequial_mensajes_titulo', 'Bow'));
                    vm.mensajeError = "";
                    cargarConveniosRecaudoAsignados();
                    cargarConveniosRecaudo();
                }).error(function (error) {
                    if (error.validationErrors != null) {
                        $scope.mensajeError = error.validationErrors[0].message;
                    }
                    else {
                        $scope.mensajeError = error.message;
                    }
                });
            }
        }]);
})();