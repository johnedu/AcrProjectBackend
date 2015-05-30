(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.personas.preferencia.preferencias';

    /*****************************************************************
     * 
     * CONTROLADOR PREFERENCIAS 
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', 'abp.services.app.personas',
        function ($scope, personasService) {
            var vm = this;
            vm.nuevaPreferencia = "";
            vm.nuevaOPcion = "";

            vm.editandoPreferencia = "";

            //Controles para eliminar o eliminar un Sufijo
            vm.controlesPreferencia = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesPreferencia.visible[$index] = true;
                    vm.eliminandoPreferencia = false;
                },
                ocultar: function ($index) {
                    vm.controlesPreferencia.visible[$index] = false;
                }
            };

            //Controles para eliminar o eliminar un Sufijo
            vm.seleccionPreferencia = {
                marcado: [],
                seleccionar: function ($index) {
                    for (var i = 0; i < vm.seleccionPreferencia.marcado.length; ++i) {
                        vm.seleccionPreferencia.marcado[i] = false;
                    }
                    vm.seleccionPreferencia.marcado[$index] = true;
                }
            };

            //Controles para eliminar o eliminar un Sufijo
            vm.controlesOpciones = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesOpciones.visible[$index] = true;
                    vm.eliminandoOpcion = false;
                },
                ocultar: function ($index) {
                    vm.controlesOpciones.visible[$index] = false;
                }
            };

            /******************************************************
             * FUNCIONES
             ******************************************************/
            function limpiarFormularioPreferencia () {
                vm.nuevaPreferencia.nombre = '';
                vm.nuevaPreferencia.estado = false;
                $scope.frmPreferencia.$setPristine();
            }

            //Función encargada de consultar las preferencias en la base de datos
            function cargarPreferencias() {
                personasService.getPreferenciasWithEstadoBool().success(function (data) {
                    vm.preferencias = bow.tablas.paginar(data.preferencias, 10);
                    console.log("Preferencias", data);
                });
            }
            cargarPreferencias();

            function limpiarFormularioOpcion () {
                vm.nuevaOpcion = {
                    nombre: ''
                };
                $scope.frmOpcion.$setPristine();
            }

            function cargarOpciones() {
                personasService.getOpcionesPreferenciaByPreferencia({ id: vm.preferenciaSeleccion }).success(function (data) {
                    vm.opcionesPreferencia = bow.tablas.paginar(data.opcionesPreferencia, 10);
                });
            }

            /*****************************************************
             * FUNCIONAMIENTO DEL FORMULARIO
             *****************************************************/
            vm.cancelarIngresoPreferencia = function () {
                vm.ingresandoPreferencia = false;
                vm.editandoPreferencia = false;
                limpiarFormularioPreferencia();
            };

            
            /*******************************************************
             * GUARDAR PREFERENCIA
             ******************************************************/

            vm.saveOrUpdatePreferencia = function () {
                if (vm.editandoPreferencia) {
                    updatePreferencia();
                }
                else {
                    registrarPreferencia();
                }
            };

            function registrarPreferencia() {
                personasService.savePreferencia({ nombre: vm.nuevaPreferencia.nombre })
                .success(function () {
                    cargarPreferencias();
                    abp.notify.success(abp.localization.localize('personas_preferencia_NotificacionNuevaPreferencia', 'Bow') + vm.nuevaPreferencia.nombre,
                        abp.localization.localize('personas_preferencia_informacion', 'Bow'));
                    limpiarFormularioPreferencia();
                }).error(function (error) {
                    abp.notify.error(error.message);
                });
            }

            /**********************************************************
             * EDITANDO PREFERENCIA
             **********************************************************/
            vm.editarPreferencia = function (preferencia) {
                limpiarFormularioPreferencia();
                vm.editandoPreferencia = true;
                vm.nuevaPreferencia = {
                    id: preferencia.id,
                    nombre: preferencia.nombre,
                    estado: preferencia.estado
                };                
                vm.nombrePreferencia = preferencia.nombre;
            };

            function updatePreferencia() {
                console.log("Resultado Update:", vm.nuevaPreferencia);
                personasService.updatePreferenciaWithEstadoBool({ id: vm.nuevaPreferencia.id, nombre: vm.nuevaPreferencia.nombre, estado: vm.nuevaPreferencia.estado })
                        .success(function () {
                            cargarPreferencias();
                            vm.editandoPreferencia = false;
                            abp.notify.success(abp.localization.localize('personas_preferencia_NotificacionEditarPreferencia', 'Bow') + vm.nuevaPreferencia.nombre,
                            abp.localization.localize('personas_preferencia_informacion', 'Bow'));
                            limpiarFormularioPreferencia();
                        }).error(function (error) {
                            abp.notify.error(error.message);
                        });
            }

            /************************************************************
             * ELIMINANDO PREFERENCIA
             ************************************************************/
            vm.puedeEliminarPreferencia = function (preferencia, funcionRetornarPuedeEliminar) {
                personasService.puedeEliminarPreferencia({ Id: preferencia.id }).success(function (data) {
                    if (data.puedeEliminar) {
                        vm.eliminandoPreferencia = true;
                    }
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            };

            vm.noPuedeEliminarPreferencia = function () {
                abp.notify.error(abp.localization.localize('personas_preferencia_NonotificacionEliminadoPreferencia', 'Bow'),
                        abp.localization.localize('zonificacion_localidad_informacion', 'Bow'));
            };

            vm.eliminarPreferenciaOk = function (preferencia) {
                personasService.deletePreferencia({ id: preferencia.id })
                .success(function () {
                    cargarPreferencias();
                    vm.eliminandoPreferencia = false;
                    abp.notify.success(abp.localization.localize('personas_preferencia_NotificacionEliminarPreferencia', 'Bow') + preferencia.nombre,
                                 abp.localization.localize('personas_preferencia_informacion', 'Bow'));
                }).error(function (error) {
                    abp.notify.error(error.message);
                });
            };

            vm.eliminarPreferenciaCancel = function () {
                vm.eliminandoPreferencia = false;
            };

            /***********************************************************
             * CONSULTAR OPCIONES DE PREFERENCIA
             **********************************************************/

            vm.consultarOpcionesPreferencia = function (preferencia, posicion) {

                vm.seleccionPreferencia.seleccionar((vm.preferencias.filasPorPagina * (vm.preferencias.paginaActual - 1)) + posicion);
                vm.preferenciaSeleccionNombre = preferencia.nombre;
                vm.preferenciaSeleccion = preferencia.id;
                personasService.getOpcionesPreferenciaByPreferencia({ id: preferencia.id }).success(function (data) {
                    vm.opcionesPreferencia = bow.tablas.paginar(data.opcionesPreferencia, 10);
                });
            };

            /***********************************************************
             * REGISTRAR OPCIÓN
             ***********************************************************/
            vm.saveOrUpdateOpcion = function () {
                if (vm.editandoOpcion) {
                    updateOpcion();
                }
                else {
                    registrarOpcion();
                }
            };

            function registrarOpcion() {
                personasService.saveOpcionPreferencia({ nombre: vm.nuevaOpcion.nombre, preferenciaId: vm.preferenciaSeleccion })
                .success(function () {
                    cargarOpciones();
                    abp.notify.info(abp.localization.localize('personas_preferencia_notificacionNuevaOpcion', 'Bow'), abp.localization.localize('personas_preferencia_informacion', 'Bow'));
                    limpiarFormularioOpcion();
                }).error(function (error) {
                    abp.notify.error(error.message);
                });
            }

            vm.cancelarIngresoOpcion = function () {
                vm.ingresandoOpcion = false;
                vm.editandoOpcion = false;
                limpiarFormularioOpcion();
            };

            /***********************************************************
             * EDITANDO OPCIÓN
             ***********************************************************/
            vm.editarOpcion = function (opcion) {
                limpiarFormularioOpcion();
                vm.editandoOpcion = true;
                vm.nuevaOpcion = {
                    id: opcion.id,
                    nombre: opcion.nombre,
                };
            };

            function updateOpcion() {
                personasService.updateOpcionPreferencia({ Id: vm.nuevaOpcion.id, nombre: vm.nuevaOpcion.nombre, preferenciaId: vm.preferenciaSeleccion })
                        .success(function () {
                            cargarOpciones();
                            vm.editandoOpcion = false;
                            abp.notify.info(abp.localization.localize('personas_preferencia_notificacionUpdateOpcion', 'Bow'), abp.localization.localize('personas_preferencia_informacion', 'Bow'));
                            limpiarFormularioOpcion();
                        }).error(function (error) {
                            abp.notify.error(error.message);
                        });
            }

            /*************************************************************
             * ELIMINANDO OPCIÓN
             ************************************************************/
            vm.puedeEliminarOpcion = function (opcion, funcionRetornarPuedeEliminar) {
                personasService.puedeEliminarOpcionPreferencia({ Id: opcion.id }).success(function (data) {
                    if (data.puedeEliminar) {
                        vm.eliminandoOpcion = true;
                    }
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            };

            vm.noPuedeEliminarOpcion = function () {
                abp.notify.error(abp.localization.localize('personas_preferencia_NonotificacionEliminadoOpcion', 'Bow'),
                        abp.localization.localize('personas_preferencia_informacion', 'Bow'));
            };

            vm.eliminarOpcionOk = function (opcion) {
                personasService.deleteOpcionPreferencia({ id: opcion.id })
                .success(function () {
                    cargarOpciones();
                    vm.eliminandoOpcion = false;
                    abp.notify.info(abp.localization.localize('personas_preferencia_notificacionEliminadoOpcion', 'Bow'), abp.localization.localize('personas_preferencia_informacion', 'Bow'));
                }).error(function (error) {
                    abp.notify.error(error.message);
                });
            };

            vm.eliminarOpcionCancel = function () {
                vm.eliminandoOpcion = false;
            };

        }]);
})();