(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.zonificacion.zona';

    /*****************************************************************
     * 
     * CONTROLADOR ZONA
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$location', '$modal', '$stateParams', 'abp.services.app.zonificacion', 'abp.services.app.parametros', 'abp.services.app.personas',
        function ($scope, $location, $modal, $stateParams, zonificacionService, parametrosService, personasService) {
            var vm = this;
            vm.localidadId = $stateParams.localidadId;

            //Inicializando modelos
            vm.editandoZona = false;
            vm.editandoEmpleado = false;
            vm.eliminandoZona = false;
            vm.eliminandoBarrio = false;
            vm.eliminandoEmpleado = false;
            vm.mostrarFechaRetiro = false;
            vm.requeridaFechaRetiro = false;

            vm.controlOperacionesEmpleado = {};

            vm.zonaEditar = "";
            vm.zonas = [];
            vm.inputTipoZona = {
                id: ''
            };

            vm.nuevaZona = {
                nombre: '',
                descripcion: '',
                tipo: '',
                localidadId: vm.localidadId
            };

            //Controles para editar o eliminar una zona
            vm.controles = {
                visible: [],
                mostrar: function ($index) {
                    vm.controles.visible[$index] = true;
                    vm.eliminandoZona = false;
                },
                ocultar: function ($index) {
                    vm.controles.visible[$index] = false;
                }
            };

            //Funcion para cargar los tipos de zona existentes.
            vm.cargarTipos = function () {
                parametrosService.getTiposZona()
                    .success(function (data) {
                        vm.tipos = data.tipos;
                    })
            }

            vm.cargarTipos();

            vm.cancelarUpdateZona = function () {
                limpiarFormulario();
                vm.editandoZona = false;
            }

            vm.saveOrUpdateZona = function () {
                if (vm.editandoZona) {
                    updateZona();
                }
                else {
                    saveZona();
                }
            }

            //Registrar zona
            function saveZona() {
                zonificacionService.saveZona(vm.nuevaZona)
                .success(function () {
                    cargarZonas();
                    abp.notify.success(abp.localization.localize('zonificacion_zona_notificacionInsertadoZona', 'Bow') + ' ' + vm.nuevaZona.nombre,
                        abp.localization.localize('zonificacion_zona_informacion', 'Bow')
                        );
                    limpiarFormulario();
                })
                .error(function (error) {
                    abp.notify.error(error.message);
                });
            };

            function updateZona() {
                zonificacionService.updateZona(vm.nuevaZona)
                    .success(function () {
                        cargarZonas();
                        vm.editandoZona = false;
                        abp.notify.success(abp.localization.localize('zonificacion_zona_notificacionActualizadoZona', 'Bow') + ' ' + vm.nuevaZona.nombre,
                        abp.localization.localize('zonificacion_zona_informacion', 'Bow'));
                        limpiarFormulario();

                    }).error(function (error) {
                        abp.notify.error(error.message);
                    });
            };

            vm.editarZona = function (zona) {
                vm.editandoZona = true;
                vm.nuevaZona = {
                    id: zona.id,
                    nombre: zona.nombre,
                    descripcion: zona.descripcion,
                    localidadId: vm.localidadId,
                    tipoId: zona.tipoId
                };
                vm.nombreZonaEditar = zona.nombre;
            };

            function limpiarFormulario() {
                vm.nuevaZona = {
                    nombre: '',
                    descripcion: '',
                    tipo: '',
                    localidadId: vm.localidadId
                };
                $scope.frmZona.$setPristine();
            }

            //Consultando la información de la zona para mostrarla en pantalla
            zonificacionService.getLocalidadWithDepartamentoAndPais({ id: $stateParams.localidadId }).success(function (data) {
                vm.zona = data;
            });

            //Funcion encargada de consultar las zonas de la zona
            function cargarZonas() {
                zonificacionService.getZonasByLocalidad({ id: $stateParams.localidadId }).success(function (data) {
                    vm.zonas = data.zonas;
                });
            };

            cargarZonas();

            //Funcion para cargar los tipos de zona existentes.
            function cargarTiposZona() {
                parametrosService.getTiposZona()
                    .success(function (data) {
                        vm.tiposzona = data.tipos;
                    })
            }

            cargarTiposZona();

            vm.consultarTipoChange = function () {
                zonificacionService.getZonasByLocalidadAndTipoZona({ id: $stateParams.localidadId, tipoid: vm.inputTipoZona.id })
                    .success(function (data) {
                        vm.zonas = data.zonas;
                    })
            }

            vm.puedeEliminarZona = function (zonaId, funcionRetornarPuedeEliminar) {
                zonificacionService.puedeEliminarZona({ id: zonaId }).success(function (data) {
                    if (data.puedeEliminar) {
                        vm.eliminandoZona = true;
                    }
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('zonificacion_zona_NonotificacionEliminadoZona', 'Bow'),
                abp.localization.localize('zonificacion_zona_informacion', 'Bow'));
            }

            vm.eliminarZonaOk = function (zona) {
                zonificacionService.deleteZona({ id: zona.id })
                   .success(function (data) {
                       abp.notify.info(abp.localization.localize('zonificacion_zona_notificacionEliminadoZona', 'Bow') + ' ' + zona.nombre,
                       abp.localization.localize('zonificacion_zona_informacion', 'Bow'));

                       cargarZonas();
                       vm.eliminandoZona = false;
                   });
            };

            vm.eliminarZonaCancel = function () {
                vm.eliminandoZona = false;
            };

            //************************** Gestionar Barrios **************************

            vm.gestionarBarriosZona = function (zona) {
                vm.zonaSeleccionada = zona;
                vm.zonaNombre = zona.nombre;
                vm.mostrarFormularios = true;

                vm.mostrarBarriosEmpleadosZona = !vm.mostrarBarriosEmpleadosZona;
                vm.cargarBarriosZonaDisponibles();
                vm.cargarBarriosZona();
            };

            //Funcion para cargar los barrios disponibles
            vm.cargarBarriosZonaDisponibles = function (zona) {
                zonificacionService.getBarriosByZonaDisponibles({ zonaId: vm.zonaSeleccionada.id })
                    .success(function (data) {
                        vm.barriosDisponibles = data.barriosDisponibles;
                    })
            }

            //Funcion para cargar los barrios de la zona asignados
            vm.cargarBarriosZona = function () {
                zonificacionService.getBarriosByZona({ zonaId: vm.zonaSeleccionada.id })
                    .success(function (data) {
                        vm.barriosZona = bow.tablas.paginar(data.barrios, 5);
                    });
            }

            //Funcion para asignar un barrio disponible a una zona
            vm.asignarBarrioZona = function () {
                if (vm.barrioId != null) {
                    zonificacionService.asignarBarrioZona({ zonaId: vm.zonaSeleccionada.id, barrioId: vm.barrioId })
                        .success(function () {
                            abp.notify.info(abp.localization.localize('zonificacion_zona_notificacionAsignadoBarrio', 'Bow'), abp.localization.localize('zonificacion_zona_informacion', 'Bow'));
                            cargarZonas();
                            vm.cargarBarriosZona();
                            vm.cargarBarriosZonaDisponibles();
                            vm.barrioId = "";
                        }).error(function (error) {
                            $scope.mensajeError = error.message;
                        });
                }
            }

            //Controles para editar o eliminar un barrio de una zona
            vm.controlesBarrios = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesBarrios.visible[$index] = true;
                    vm.eliminandoBarrio = false;
                },
                ocultar: function ($index) {
                    vm.controlesBarrios.visible[$index] = false;
                }
            };

            vm.puedeEliminarBarrio = function (barrioId, funcionRetornarPuedeEliminar) {
                vm.eliminandoBarrio = true;
                funcionRetornarPuedeEliminar(true);
            }

            vm.eliminarBarrioOk = function (barrio) {
                zonificacionService.deleteZonaBarrio({ id: barrio.id })
                   .success(function (data) {
                       abp.notify.info(abp.localization.localize('zonificacion_zona_notificacionEliminadoBarrio', 'Bow'),
                       abp.localization.localize('zonificacion_zona_informacion', 'Bow'));
                       vm.cargarBarriosZona();
                       vm.cargarBarriosZonaDisponibles();
                   });
            };

            vm.eliminarBarrioCancel = function () {
                vm.eliminandoBarrio = false;
            };

            //vm.regresarBarriosZona = function () {
            //    vm.mostrarBarriosEmpleadosZona = !vm.mostrarBarriosEmpleadosZona
            //    cargarZonas();
            //}


            //************************** Gestionar Empleados **************************

            vm.gestionarEmpleadosZona = function (zona) {

                vm.nuevoEmpleado = {
                    id: '',
                    zonaId: zona.id,
                    empleadoId: '',
                    tipoId: '',
                    fechaAsignacion: ''
                }

                vm.zonaSeleccionada = zona;
                vm.zonaNombre = zona.nombre;
                vm.mostrarFormularios = false;

                vm.mostrarBarriosEmpleadosZona = !vm.mostrarBarriosEmpleadosZona;
                vm.cargarEmpleadosActivosZona();
                vm.cargarEmpleadosInactivosZona();
                vm.cargarRoles();
            };

            //Funcion para asignar un empleado a una zona
            vm.asignarEmpleadoZona = function () {

                vm.nuevoEmpleado.empleadoId = vm.empleado.id;

                if (!vm.nuevoEmpleado.id) {
                    zonificacionService.saveZonaEmpleado(vm.nuevoEmpleado)
                       .success(function () {
                           vm.cargarEmpleadosActivosZona();
                           cargarZonas();
                           limpiarFormularioEmpleado();
                           abp.notify.info(abp.localization.localize('empleados_zona_notificacionAgregadoEmpleadoZona', 'Bow'),
                           abp.localization.localize('empleados_zona_informacion', 'Bow'));
                       }).error(function (error) {
                           abp.notify.warn(error.message, abp.localization.localize('empleados_zona_informacion', 'Bow'));
                       });
                }
                else {
                    zonificacionService.updateZonaEmpleado(vm.nuevoEmpleado)
                       .success(function () {
                           vm.cargarEmpleadosActivosZona();
                           vm.cargarEmpleadosInactivosZona();
                           cargarZonas();
                           limpiarFormularioEmpleado();
                           abp.notify.info(abp.localization.localize('empleados_zona_notificacionActualizadoEmpleadoZona', 'Bow'),
                           abp.localization.localize('empleados_zona_informacion', 'Bow'));
                       }).error(function (error) {
                           abp.notify.warn(error.message, abp.localization.localize('empleados_zona_informacion', 'Bow'));
                       });
                }
            }

            vm.editarEmpleado = function (empleado) {
                zonificacionService.getZonaEmpleado({ id: empleado.id })
                  .success(function (data) {

                      vm.editandoEmpleado = true;
                      vm.mostrarFechaRetiro = true;
                      vm.requeridaFechaRetiro = true;

                      vm.controlOperacionesEmpleado.asignarEmpleado(data.empleadoId);

                      vm.nuevoEmpleado = {
                          id: data.id,
                          zonaId: data.zonaId,
                          empleadoId: data.empleadoId,
                          tipoId: data.tipoId,
                          fechaAsignacion: data.fechaAsignacion.substring(0, 10)
                      };
                      vm.nombreEmpleadoEditar = data.nombreCompleto;
                      var fechaRetiroMaxima = new Date(data.fechaRetiroMaxima);
                      var fechaRetiroMinima = new Date(data.fechaRetiroMinima);

                      vm.configuracionFechaRetiro = bow.fechas.configurarDatePicker(fechaRetiroMinima, fechaRetiroMaxima);

                      vm.nuevoEmpleado.fechaRetiro = ""; //data.fechaRetiroMaxima;

                      vm.disableEmpleado = true;
                      vm.disableFechaAsignacion = true;
                      vm.mostrarFechaRetiro = true;

                      vm.mostrarRegistrarEmpleado = !$scope.mostrarRegistrarEmpleado;

                  }).error(function (error) {
                      $scope.mensajeError = error.message;
                  });
            };

            vm.cancelarUpdateEmpleado = function () {
                limpiarFormularioEmpleado();
                vm.editandoEmpleado = false;
            }

            function limpiarFormularioEmpleado() {
                vm.mostrarFechaRetiro = false;
                vm.disableFechaAsignacion = true;
                vm.editandoEmpleado = false;
                vm.disableEmpleado = false;

                vm.requeridaFechaRetiro = false;

                vm.empleado = '';
                vm.nuevoEmpleado = {
                    zonaId: '',
                    empleadoId: '',
                    tipoId: '',
                    fechaAsignacion: ''
                };
                $scope.frmEmpleado.$setPristine();
            }

            //funcion para obtener el rango de la fecha de asignación en el datapicker
            vm.fechaAsignacion = function (empleado) {

                personasService.fechaAsignacion({ personaId: empleado.personaId })
                    .success(function (data) {
                        var fechaAsignacionMaxima = new Date(data.fechaMaximaAsignacion);
                        var fechaAsignacionMinima = new Date(data.fechaMinimaAsignacion);
                        vm.configuracionFechaAsignacion = bow.fechas.configurarDatePicker(fechaAsignacionMinima, fechaAsignacionMaxima);
                        vm.nuevoEmpleado.fechaAsignacion = data.fechaMaximaAsignacion;

                        vm.disableFechaAsignacion = false;
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });

            };

            vm.cargarRoles = function () {
                parametrosService.getTiposRol().success(function (data) {
                    vm.roles = data.roles;
                });
            };

            //Funcion para cargar los empleados de la zona asignados
            vm.cargarEmpleadosActivosZona = function () {
                zonificacionService.getEmpleadosActivosByZona({ zonaId: vm.zonaSeleccionada.id })
                    .success(function (data) {
                        vm.empleadosActivos = bow.tablas.paginar(data.empleadosActivos, 5);
                    });
            };

            vm.cargarEmpleadosInactivosZona = function () {
                zonificacionService.getEmpleadosInactivosByZona({ zonaId: vm.zonaSeleccionada.id })
                    .success(function (data) {
                        vm.empleadosInactivos = bow.tablas.paginar(data.empleadosInactivos, 5);
                    });
            };

            //Controles para editar o eliminar un barrio de una zona
            vm.controlesEmpleados = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesEmpleados.visible[$index] = true;
                    vm.eliminandoEmpleado = false;
                },
                ocultar: function ($index) {
                    vm.controlesEmpleados.visible[$index] = false;
                }
            };

            vm.regresarZona = function () {
                vm.mostrarBarriosEmpleadosZona = !vm.mostrarBarriosEmpleadosZona
                vm.editandoZona = false;
                vm.editandoEmpleado = false;

                limpiarFormulario();
                limpiarFormularioEmpleado();

            };

          

        }]);
})();