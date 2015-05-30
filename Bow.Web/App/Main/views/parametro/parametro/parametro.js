(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.parametro.parametro';

    /*****************************************************************
     * 
     * CONTROLADOR PARAMETRO
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.parametros',
        function ($scope, $modal, parametrosService) {
            var vm = this;

            //Inicializando modelos
            vm.editandoParametro = false;
            vm.eliminandoParametro = false;
            vm.ingresandoParametro = "";

            vm.editandoEstado = false;
            vm.eliminandoEstado = false;

            vm.editandoTipo = false;
            vm.eliminandoTipo = false;

            vm.parametroSeleccionado = "";

            vm.parametroEditar = "";
            vm.parametros = [];

            //Método encargado de consultar los parametros en la base de datos
            function cargarParametros() {
                parametrosService.getParametros().success(function (data) {
                    vm.parametros = bow.tablas.paginar(data.parametros, 10);
                });
            };

            cargarParametros();

            /********************************************
           * GUARDAR Y EDITAR UN PARAMETRO
           ********************************************/
            vm.saveOrUpdateParametro = function () {
                if (vm.editandoParametro) {
                    updateParametro();
                }
                else {
                    saveParametro();
                }
            }

            function saveParametro() {
                parametrosService.saveParametro(vm.nuevoParametro)
                .success(function () {
                    cargarParametros();
                    abp.notify.success(abp.localization.localize('parametro_parametro_notificacionInsertadoParametro', 'Bow') + ' ' + vm.nuevoParametro.nombre,
                    abp.localization.localize('parametro_parametro_informacion', 'Bow'));
                    limpiarFormulario();
                })
                .error(function (error) {
                    abp.notify.error(error.message);
                });
            };

            function updateParametro() {
                parametrosService.updateParametro(vm.nuevoParametro)
                    .success(function () {
                        cargarParametros();

                        vm.editandoParametro = false;
                        vm.parametroNombre = vm.nuevoParametro.nombre;
                        vm.parametroDescripcion = vm.nuevoParametro.descripcion;
                        vm.parametro = vm.nuevoParametro;

                        abp.notify.success(abp.localization.localize('parametro_parametro_notificacionActualizadoParametro', 'Bow') + ' ' + vm.nuevoParametro.nombre,
                        abp.localization.localize('parametro_parametro_informacion', 'Bow'));
                        limpiarFormulario();

                    }).error(function (error) {
                        abp.notify.error(error.message);
                    });
            };

            vm.editarParametro = function (parametro) {
                vm.editandoParametro = true;
                vm.nuevoParametro = {
                    id: parametro.id,
                    nombre: parametro.nombre,
                    descripcion: parametro.descripcion
                };
                vm.nombreParametroEditar = parametro.nombre;
            };

            vm.cancelarIngresoParametro = function () {
                vm.ingresandoParametro = false;
                vm.editandoParametro = false;
                limpiarFormulario();
            }

            //vm.cancelarUpdateParametro = function () {
            //    if (vm.editandoParametro) {
            //        vm.editandoParametro = false;
            //    }
            //    limpiarFormulario();
            //}

            function limpiarFormulario() {
                vm.nuevoParametro = {
                    nombre: '',
                    descripcion: ''
                };
                $scope.frmParametro.$setPristine();
            };

            ////Controles para editar o eliminar un parametro
            //vm.controles = {
            //    visible: [],
            //    mostrar: function ($index) {
            //        vm.controles.visible[$index] = true;
            //        vm.eliminandoParametro = false;
            //    },
            //    ocultar: function ($index) {
            //        vm.controles.visible[$index] = false;
            //        vm.eliminandoParametro = false;
            //    }
            //};


            /********************************************
            * ELIMINAR PARAMETRO
            ********************************************/
            vm.eliminarParametroOk = function (parametro) {
                parametrosService.deleteParametro({ id: parametro.id })
                   .success(function (data) {
                       abp.notify.info(abp.localization.localize('parametro_parametro_notificacionEliminadoParametro', 'Bow') + ' ' + parametro.nombre,
                       abp.localization.localize('parametro_parametro_informacion', 'Bow'));

                       cargarParametros();
                       vm.eliminandoParametro = false;
                       vm.editandoParametro = false;
                       vm.eliminandoParametro = false;

                       vm.parametroNombre = "";
                       vm.parametroDescripcion = "";
                       vm.parametro = "";
                   });
            };

            vm.eliminarParametroCancel = function () {
                vm.eliminandoParametro = false;
            };

            vm.puedeEliminarParametro = function (parametro, funcionRetornarPuedeEliminar) {
                parametrosService.puedeEliminarParametro({ id: parametro.id }).success(function (data) {
                    if (data.puedeEliminar) {
                        vm.eliminandoParametro = true;
                    }
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            };

            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('parametro_parametro_notificacionNoEliminarParametro', 'Bow'),
                abp.localization.localize('parametro_parametro_informacion', 'Bow'));
            };


            /********************************************************
             * CONTROL PARA MOSTRAR LOS TIPOS Y ESTADOS DEL PARAMETRO
             *******************************************************/
            vm.opcionParametro = [true, false];

            vm.opcionEstado = function () {
                vm.opcionParametro = [true, false];
            }

            vm.opcionTipo = function () {
                vm.opcionParametro = [false, true];
            }

            /********************************************
             * TIPOS Y ESTADOS DE PARAMETRO
             ********************************************/

            vm.consultarTiposEstados = function (parametro) {
                vm.parametroSeleccionado = parametro.id;

                vm.editandoParametro = false;
                vm.eliminandoParametro = false;

                vm.parametroNombre = parametro.nombre;
                vm.parametroDescripcion = parametro.descripcion;
                vm.parametro = parametro;

                //Carga inicial de tipos para mostrar en pantalla
                cargarTipos(parametro.id);

                //Carga inicial de estados para mostrar en pantalla
                cargarEstados(parametro.id);

            };



            //Funcion encargada de consultar los tipos en la base de datos
            function cargarTipos(parametroId) {
                parametrosService.getTipos({ id: parametroId }).success(function (data) {
                    vm.tiposParametro = bow.tablas.paginar(data.tipos, 5);
                });
            }

            //Funcion encargada de consultar el parametro seleccionado en la base de datos
            function cargarEstados(parametroId) {
                parametrosService.getEstadosWithNombreEstado({ id: parametroId }).success(function (data) {
                    vm.estadosParametro = bow.tablas.paginar(data.estados, 5);
                });
            }

            /********************************************
             * ESTADOS DE PARAMETRO
             ********************************************/

            //Mostrar controles para editar o eliminar un estado
            vm.controlesEstados = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesEstados.visible[$index] = true;
                    vm.eliminandoEstado = false;
                },
                ocultar: function ($index) {
                    vm.controlesEstados.visible[$index] = false;
                    vm.eliminandoEstado = false;
                }
            };

            //Funcion para cargar los estados en el dropdown
            function cargarNombresEstados() {
                parametrosService.getNombresEstados().success(function (data) {
                    vm.nombresEstados = data.nombresEstados;
                });
            };
            cargarNombresEstados();

            //Funcion para guardar o actualizar un nuevo estado
            vm.saveOrUpdateNombreEstado = function (form) {
                if (vm.editandoEstado) {
                    updateNombreEstado();
                }
                else {
                    saveNombreEstado();
                }
                form.$setPristine();
            }

            //Funcion para guardar un nuevo estado
            function saveNombreEstado() {
                vm.asignarEstado.parametroId = vm.parametroSeleccionado;

                parametrosService.saveAsignarEstadoParametro(vm.asignarEstado)
                    .success(function () {
                        cargarEstados(vm.parametroSeleccionado);
                        vm.editandoEstado = false;
                        abp.notify.success(abp.localization.localize('parametro_estado_notificacionAsignadoParametro', 'Bow'),
                        abp.localization.localize('parametro_parametro_informacion', 'Bow'));
                        limpiarFormularioEstado();
                    }).error(function (error) {
                        abp.notify.error(error.message);
                    });
            };

            //Funcion para actualizar un estado
            function updateNombreEstado() {
                parametrosService.updateEstadoParametro(vm.asignarEstado)
                    .success(function () {
                        cargarEstados(vm.parametroSeleccionado);
                        vm.editandoEstado = false;
                        abp.notify.success(abp.localization.localize('parametro_estado_notificacionActuailizadoEstadoParametro', 'Bow') + vm.asignarEstado.motivo,
                        abp.localization.localize('parametro_parametro_informacion', 'Bow'));
                        limpiarFormularioEstado();
                    }).error(function (error) {
                        abp.notify.error(error.message);
                    });
            };

            //Función para editar el estado seleccionado
            vm.editarEstado = function (estado) {
                vm.editandoEstado = true;
                vm.asignarEstado = {
                    id: estado.id,
                    estadoNombreId: estado.estadoNombreId,
                    motivo: estado.motivo
                };
                vm.disableEstadoNombre = true;
            };

            //Funcion para limpiar el formulario de estado
            function limpiarFormularioEstado() {
                vm.asignarEstado = {
                    estado: '',
                    motivo: ''
                };
                vm.disableEstadoNombre = false;
            };

            //Función para cancelar el guardado o actualizado del estado
            vm.cancelarUpdateNombreEstado = function (form) {
                if (vm.editandoEstado) {
                    vm.editandoEstado = false;
                }
                form.$setPristine();
                limpiarFormularioEstado();
            }

            /********************************************
            * ELIMINAR ESTADO
            ********************************************/
            vm.eliminarEstadoOk = function (estado) {
                parametrosService.deleteEstado({ id: estado.id })
                   .success(function (data) {
                       abp.notify.info(abp.localization.localize('parametro_estado_notificacionEliminadoEstado', 'Bow') + ' ' + estado.motivo,
                       abp.localization.localize('parametro_parametro_informacion', 'Bow'));
                       cargarEstados(vm.parametroSeleccionado);
                       vm.eliminandoEstado = false;
                   }).error(function (error) {
                       vm.noPuedeEliminarEstado();
                   });
            };

            vm.eliminarEstadoCancel = function () {
                vm.eliminandoEstado = false;
            };

            vm.puedeEliminarEstado = function (estadoId, funcionRetornarPuedeEliminar) {
                vm.eliminandoEstado = true;
                funcionRetornarPuedeEliminar(true);
            }

            vm.noPuedeEliminarEstado = function () {
                abp.notify.error(abp.localization.localize('zonificacion_parametro_notificacionNoEliminarEstado', 'Bow'),
                abp.localization.localize('parametro_parametro_informacion', 'Bow'));
            }

            /********************************************
            * TIPOS DE PARAMETRO
            ********************************************/
            //Mostrar controles para editar o eliminar un tipo
            vm.controlesTipos = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesTipos.visible[$index] = true;
                    vm.eliminandoTipo = false;
                },
                ocultar: function ($index) {
                    vm.controlesTipos.visible[$index] = false;
                    vm.eliminandoTipo = false;
                }
            };

            //Funcion para guardar o actualizar un nuevo tipo
            vm.saveOrUpdateTipo = function (form) {
                if (vm.editandoTipo) {
                    updateTipo();
                }
                else {
                    saveTipo();
                }
                form.$setPristine();
            }

            //Funcion para guardar un nuevo tipo
            function saveTipo() {
                vm.nuevoTipo.parametroId = vm.parametroSeleccionado;

                parametrosService.saveTipo(vm.nuevoTipo)
                    .success(function () {
                        cargarTipos(vm.parametroSeleccionado);
                        vm.editandoTipo = false;
                        abp.notify.success(abp.localization.localize('parametro_tipo_notificacionInsertadoTipo', 'Bow') + vm.nuevoTipo.nombre,
                        abp.localization.localize('parametro_parametro_informacion', 'Bow'));
                        limpiarFormularioTipo();
                    }).error(function (error) {
                        abp.notify.error(error.message);
                    });
            };

            //Funcion para actualizar un tipo
            function updateTipo() {
                parametrosService.updateTipo(vm.nuevoTipo)
                    .success(function () {
                        cargarTipos(vm.parametroSeleccionado);
                        vm.editandoTipo = false;
                        abp.notify.success(abp.localization.localize('parametro_tipo_notificacionActuailizadoTipo', 'Bow') + vm.nuevoTipo.nombre,
                        abp.localization.localize('parametro_parametro_informacion', 'Bow'));
                        limpiarFormularioTipo();
                    }).error(function (error) {
                        abp.notify.error(error.message);
                    });
            };

            //Función para editar el estado seleccionado
            vm.editarTipo = function (tipo) {
                vm.editandoTipo = true;
                vm.nuevoTipo = {
                    id: tipo.id,
                    nombre: tipo.nombre,
                    descripcion: tipo.descripcion
                };
            };

            //Funcion para limpiar el formulario de tipo
            function limpiarFormularioTipo() {
                vm.nuevoTipo = {
                    nombre: '',
                    descripcion: ''
                };
            };

            //Función para cancelar el guardado o actualizado del tipo
            vm.cancelarUpdateTipo = function (form) {
                if (vm.editandoTipo) {
                    vm.editandoTipo = false;
                }
                form.$setPristine();
                limpiarFormularioTipo();
            }

            /********************************************
            * ELIMINAR TIPO
            ********************************************/
            vm.eliminarTipoOk = function (tipo) {
                parametrosService.deleteTipo({ id: tipo.id })
                   .success(function (data) {
                       abp.notify.info(abp.localization.localize('parametro_tipo_notificacionEliminadoTipo', 'Bow') + ' ' + tipo.nombre,
                       abp.localization.localize('parametro_parametro_informacion', 'Bow'));
                       cargarTipos(vm.parametroSeleccionado);
                       vm.eliminandoTipo = false;
                   }).error(function (error) {
                       vm.noPuedeEliminarTipo();
                   });
            };

            vm.eliminarTipoCancel = function () {
                vm.eliminandoTipo = false;
            };

            vm.puedeEliminarTipo = function (tipoId, funcionRetornarPuedeEliminar) {
                vm.eliminandoTipo = true;
                funcionRetornarPuedeEliminar(true);
            }

            vm.noPuedeEliminarTipo = function () {
                abp.notify.error(abp.localization.localize('zonificacion_parametro_notificacionNoEliminarTipo', 'Bow'),
                abp.localization.localize('parametro_parametro_informacion', 'Bow'));
            }


            ///************************************************************************
            //     * Llamado para abrir Modal para Nuevo Estado
            //************************************************************************/

            vm.abrirModalNuevoNombreEstado = function (estado) {

                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/parametro/parametro/partials/modalNuevoNombreEstado.cshtml',
                    controller: 'modalNuevoNombreEstadoController',
                    keyboard: false,
                    backdrop: 'static',
                    size: 'md'
                });

                modalInstance.result.then(function (estadoNombre) {
                    abp.notify.success(abp.localization.localize('parametro_estado_notificacionInsertadoEstado', 'Bow') + ' ' + estadoNombre, "Información");
                    cargarNombresEstados();
                }, function () { vm.resultado = "No devolvio" });

            }



        }]);
})();

