(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.zonificacion.parametrosLocalidad';

    //Creando el controlador
    angular.module('app').controller(controllerId, ['$scope', '$stateParams', '$modal', 'abp.services.app.zonificacion', function ($scope, $stateParams, $modal, zonificacionService) {
        var vm = this;
        vm.torieLocalidad = {
            localidadId: $stateParams.localidadId,
            tipoOrientacionId: ''
        };
        vm.toriesLocalidad = "";
        vm.tories = "";

        vm.sufijoLocalidad = {
            localidadId: $stateParams.localidadId,
            sufijoId: ''
        };

        vm.localidadId = $stateParams.localidadId;
        vm.localidad = "";

        vm.sufijosLocalidad = "";
        vm.sufijos = "";
        vm.avenidasLocalidad = "";
        vm.barriosLocalidad = "";
        vm.nuevaAvenida = "";
        vm.frmAvenidaOculto = "";
        vm.eliminandoAvenida = "";
        vm.editandoAvenida = false;
        vm.ingresandoBarrio = "";
        vm.controlOperacionesNuevaManzana = {};


        /********************************************************
         * CONTROL PARA MOSTRAR OPCIÓN DE PARÁMETRO SELECCIONADA
         *******************************************************/
        vm.opcionParametro = [true, false, false];

        vm.opcionTipoOrientacion = function () {
            vm.opcionParametro = [true, false, false];
        }

        vm.opcionSufijo = function () {
            vm.opcionParametro = [false, true, false];
        }

        vm.opcionAvenida = function () {
            vm.opcionParametro = [false, false, true];
        }

        //Controles para editar o eliminar un Tipo de Orientación
        vm.controlesTorie = {
            visible: [],
            mostrar: function ($index) {
                vm.controlesTorie.visible[$index] = true;
            },
            ocultar: function ($index) {
                vm.controlesTorie.visible[$index] = false;
            }
        };

        //Controles para eliminar o eliminar un Sufijo
        vm.controlesSufijo = {
            visible: [],
            mostrar: function ($index) {
                vm.controlesSufijo.visible[$index] = true;
            },
            ocultar: function ($index) {
                vm.controlesSufijo.visible[$index] = false;
            }
        };

        //Controles para eliminar o eliminar un Sufijo
        vm.controlesAvenida = {
            visible: [],
            mostrar: function ($index) {
                vm.controlesAvenida.visible[$index] = true;
                vm.eliminandoAvenida = false;
            },
            ocultar: function ($index) {
                vm.controlesAvenida.visible[$index] = false;
            }
        };

        //Controles para eliminar o eliminar un Sufijo
        vm.controlesBarrio = {
            visible: [],
            mostrar: function ($index) {
                vm.controlesBarrio.visible[$index] = true;
                vm.eliminandoBarrio = false;
            },
            ocultar: function ($index) {
                vm.controlesBarrio.visible[$index] = false;
            }
        };

        //Controles para eliminar o eliminar un Sufijo
        vm.controlesManzana = {
            visible: [],
            mostrar: function ($index) {
                vm.controlesManzana.visible[$index] = true;
            },
            ocultar: function ($index) {
                vm.controlesManzana.visible[$index] = false;
            }
        };

        //Controles para eliminar o eliminar un Sufijo
        vm.seleccionBarrio = {
            marcado: [],
            seleccionar: function ($index) {
                for (i = 0; i < vm.seleccionBarrio.marcado.length; ++i) {
                    vm.seleccionBarrio.marcado[i] = false;
                }
                vm.seleccionBarrio.marcado[$index] = true;
                console.log("Marcado", vm.seleccionBarrio.marcado);
            }
        };

        /**************************************************
         * FUNCIONES
         *************************************************/
        function limpiarFormularioAvenida() {
            vm.nuevaAvenida = {
                nombre: ''
            };
            vm.frmAvenidaOculto.$setPristine();
        }

        function limpiarFormularioBarrio() {
            vm.nuevoBarrio = {
                nombre: ''
            };
            $scope.frmBarrio.$setPristine();
        }

        function cargarToriesLocalidad(localidadId) {
            zonificacionService.getToriesLocalidadByLocalidad({ localidadId: localidadId })
                .success(function (data) {
                    vm.toriesLocalidad = data.toriesLocalidad;
                });
        }
        cargarToriesLocalidad($stateParams.localidadId);

        function cargarToriesDisponibles(localidadId) {
            zonificacionService.getTiposOrientacionSinAsignarALocalidad({ localidadId: localidadId }).success(function (data) {
                vm.tories = data.tories;
            });
        }
        cargarToriesDisponibles($stateParams.localidadId);

        function cargarSufijosLocalidad(localidadId) {
            zonificacionService.getSufijosLocalidadByLocalidad({ localidadId: localidadId })
                .success(function (data) {
                    vm.sufijosLocalidad = bow.tablas.paginar(data.sufijosLocalidad, 5);
                });
        }
        cargarSufijosLocalidad($stateParams.localidadId);

        function cargarSufijosDisponibles(localidadId) {
            zonificacionService.getSufijosSinAsignarALocalidad({ localidadId: localidadId }).success(function (data) {
                vm.sufijos = data.sufijos;
                cargarSufijosLocalidad($stateParams.localidadId);
            });
        }
        cargarSufijosDisponibles($stateParams.localidadId);

        function cargarAvenidasLocalidad(localidadId) {
            zonificacionService.getAvenidasByLocalidad({ id: localidadId })
                .success(function (data) {
                    vm.avenidasLocalidad = bow.tablas.paginar(data.avenidas, 5);
                });
        }
        cargarAvenidasLocalidad($stateParams.localidadId);

        function cargarBarriosLocalidad(localidadId) {
            zonificacionService.getBarriosByLocalidad({ id: localidadId })
                .success(function (data) {
                    vm.barriosLocalidad = bow.tablas.paginar(data.barrios, 5);
                });
        }
        cargarBarriosLocalidad($stateParams.localidadId);

        /****************************************************
         * ASIGNAR TIPO DE ORIENTACIÓN A LA LOCALIDAD 
         ***************************************************/
                
        vm.asignarTorieLocalidad = function () {
            zonificacionService.saveTipoOrientacionLocalidad(vm.torieLocalidad)
                .success(function () {
                    cargarToriesLocalidad($stateParams.localidadId);
                    cargarToriesDisponibles($stateParams.localidadId);
                    abp.notify.success(abp.localization.localize('zonificacion_parametrosLocalidad_tipoOrientacion_notificacionAsignadoTorie', 'Bow'), "Información");
                }).error(function (error) {
                    abp.notify.error(error.message);
                });
        }

        /****************************************************
         * ELIMINAR TIPO DE ORIENTACIÓN DE LA LOCALIDAD 
         ***************************************************/
        
        vm.puedeEliminarTorieLocalidad = function (torieLocalidadId, funcionRetornarPuedeEliminar) {
            zonificacionService.puedeEliminarTorieLocalidad({ Id: torieLocalidadId }).success(function (data) {
                if (data.puedeEliminar) {
                    vm.eliminandoTorieLocalidad = true;
                }
                funcionRetornarPuedeEliminar(data.puedeEliminar);
            });
        }

        vm.noPuedeEliminar = function () {
            abp.notify.error(abp.localization.localize('zonificacion_torie_notificacionNoQuitarTorie', 'Bow'),
                    abp.localization.localize('zonificacion_torie_informacion', 'Bow'));
        }

        vm.eliminarTorieLocalidadOk = function (torieLocalidad) {
            zonificacionService.deleteTorieLocalidad({ id: torieLocalidad.id })
               .success(function (data) {
                   abp.notify.success(abp.localization.localize('zonificacion_departamento_notificacionEliminadoDpto', 'Bow') + ' ' + torieLocalidad.nombre, abp.localization.localize('zonificacion_departamento_notificacionInsertadoInformacion', 'Bow'));
                   cargarToriesLocalidad($stateParams.localidadId);
                   cargarToriesDisponibles($stateParams.localidadId);
                   vm.eliminandoTorieLocalidad = false;
               });
        }

        vm.eliminarTorieLocalidadCancel = function () {
            vm.eliminandoTorieLocalidad = false;
        };

        /****************************************************
         * VENTANA MODAL DE ADMINISTRACIÓN DE SUFIJOS
         ****************************************************/
        vm.abrirModalNuevoSufijo = function () {
            var modalInstance = $modal.open({
                templateUrl: '/App/Main/views/zonificacion/localidad/partials/modalNuevoSufijo.cshtml',
                controller: 'modalNuevoSufijoController',
                keyboard: false,
                backdrop: 'static',
                size: 'md'
            });

            modalInstance.result.then(function () {
                cargarSufijosDisponibles($stateParams.localidadId);
            }, function () { vm.resultado = "No devolvio" + sufijoId });
        }

        /*****************************************************
         * ASIGNAR SUFIJO A LA LOCALIDAD
         ****************************************************/

        //Asignar un sufijo a la localidad seleccionada
        vm.asignarSufijoLocalidad = function () {
            console.log(vm.sufijoLocalidad);
            zonificacionService.saveSufijoLocalidad(vm.sufijoLocalidad)
                .success(function () {
                    cargarSufijosLocalidad($stateParams.localidadId);
                    cargarSufijosDisponibles($stateParams.localidadId);
                    abp.notify.success(abp.localization.localize('zonificacion_parametrosLocalidad_sufijo_notificacionAsignadoSufijo', 'Bow'), "Información");
                }).error(function (error) {
                    abp.notify.info(error.message);
                });
        }

        /*******************************************************
         * ELIMINAR SUFIJO LOCALIDAD
         ******************************************************/
        vm.puedeEliminarSufijoLocalidad = function (sufijoLocalidad, funcionRetornarPuedeEliminar) {
            zonificacionService.puedeEliminarSufijoLocalidad({ Id: sufijoLocalidad.id }).success(function (data) {
                funcionRetornarPuedeEliminar(data.puedeEliminar);
            });
        }

        vm.noPuedeEliminarSufijo = function () {
            abp.notify.error(abp.localization.localize('zonificacion_sufijo_notificacionNoQuitarSufijo', 'Bow'),
                       abp.localization.localize('zonificacion_sufijo_informacion', 'Bow'));
        }

        vm.eliminarSufijoLocalidadOk = function (sufijoLocalidad) {
            zonificacionService.deleteSufijoLocalidad({ id: sufijoLocalidad.id })
            .success(function (data) {
                abp.notify.success(abp.localization.localize('zonificacion_sufijo_notificacionQuitadoSufijo', 'Bow') + ' ' + sufijoLocalidad.nombreSufijo, "Información");
                cargarSufijosLocalidad($stateParams.localidadId);
                cargarSufijosDisponibles($stateParams.localidadId);
            });
        }

        /*********************************************************
         * REGISTRAR AVENIDA
         ********************************************************/
        vm.saveOrUpdateAvenida = function () {
            if (vm.editandoAvenida) {
                updateAvenida();
            }
            else {
                registrarAvenida();
            }
        }

        function registrarAvenida() {
            vm.nuevaAvenida.localidadId = $stateParams.localidadId;
            console.log("Avenida", vm.nuevaAvenida);
            zonificacionService.saveAvenida(vm.nuevaAvenida)
            .success(function () {
                cargarAvenidasLocalidad($stateParams.localidadId);
                abp.notify.success(abp.localization.localize('zonificacion_avenida_notificacionInsertadoAvenida', 'Bow') + ' ' + vm.nuevaAvenida.nombre, "Información");
                limpiarFormularioAvenida();
            }).error(function (error) {
                abp.notify.error(error.message);
            });
        }

        /*********************************************************
         * ELIMINAR AVENIDA
         *********************************************************/
        vm.puedeEliminarAvenida = function (avenida, funcionRetornarPuedeEliminar) {
            zonificacionService.puedeEliminarAvenida({ Id: avenida.id }).success(function (data) {
                if (data.puedeEliminar) {
                    vm.eliminandoAvenida = true;
                }
                funcionRetornarPuedeEliminar(data.puedeEliminar);
            });
        }

        vm.noPuedeEliminarAvenida = function () {
            abp.notify.error(abp.localization.localize('zonificacion_avenida_notificacionNoEliminadoAvenida', 'Bow'),
                   abp.localization.localize('zonificacion_avenida_informacion', 'Bow'));
        }

        vm.eliminarAvenidaOk = function (avenida) {
            zonificacionService.deleteAvenida({ id: avenida.id })
            .success(function () {
                vm.eliminandoAvenida = false;
                cargarAvenidasLocalidad($stateParams.localidadId);
                abp.notify.success(abp.localization.localize('zonificacion_avenida_notificacionEliminadoAvenida', 'Bow') + ' ' + avenida.nombre, "Información");
            }).error(function (error) {
                abp.notify.error(error.message);
            });
        }

        vm.eliminarAvenidaCancel = function () {
            vm.eliminandoAvenida = false;
        }

        /*******************************************************
         * EDITAR AVENIDA
         *******************************************************/
        vm.editarAvenida = function (avenida) {
            vm.nuevaAvenida = {
                id: avenida.id,
                nombre: avenida.nombre,
                localidadId: $stateParams.localidadId
            };
            vm.editandoAvenida = true;
        }

        function updateAvenida() {
            zonificacionService.updateAvenida(vm.nuevaAvenida)
                .success(function () {
                    cargarAvenidasLocalidad($stateParams.localidadId);
                    vm.editandoAvenida = false;
                    abp.notify.success(abp.localization.localize('zonificacion_avenida_notificacionActuailizadoAvenida', 'Bow') + ' ' + vm.nuevaAvenida.nombre, "Información");
                    limpiarFormularioAvenida();
                }).error(function (error) {
                    abp.notify.error(error.message);
                });
        };

        vm.cancelarUpdateAvenida = function () {
            if (vm.editandoAvenida) {
                vm.editandoAvenida = false;
            }
            limpiarFormularioAvenida();
        }

        /*******************************************************
         * REGISTRAR NUEVO BARRIO
         *******************************************************/
        vm.cancelarIngresoBarrio = function () {
            vm.ingresandoBarrio = false;
            vm.editandoBarrio = false;
            limpiarFormularioBarrio();
        }

        vm.saveOrUpdateBarrio = function () {
            if (vm.editandoBarrio) {
                updateBarrio();
            }
            else {
                registrarBarrio();
            }
        }

        function registrarBarrio() {
            vm.nuevoBarrio.localidadId = $stateParams.localidadId;
            zonificacionService.saveBarrio(vm.nuevoBarrio)
            .success(function () {
                cargarBarriosLocalidad($stateParams.localidadId);
                abp.notify.success(abp.localization.localize('zonificacion_barrio_notificacionInsertadoBarrio', 'Bow') + ' ' + vm.nuevoBarrio.nombre, "Información");
                limpiarFormularioBarrio();
            }).error(function (error) {
                abp.notify.error(error.message);
            });
        }


        /******************************************************
         * EDITAR BARRIO
         ******************************************************/
        vm.editarBarrio = function (barrio) {
            limpiarFormularioBarrio();
            vm.editandoBarrio = true;
            vm.nuevoBarrio = {
                id: barrio.id,
                nombre: barrio.nombre,
                localidadId: $stateParams.localidadId
            };
            vm.nombreBarrioEditar = barrio.nombre;
        }

        function updateBarrio() {
            zonificacionService.updateBarrio({ id: vm.nuevoBarrio.id, nombre: vm.nuevoBarrio.nombre })
                    .success(function () {
                        cargarBarriosLocalidad($stateParams.localidadId);
                        vm.editandoBarrio = false;
                        abp.notify.success(abp.localization.localize('zonificacion_barrio_notificacionActuailizadoBarrio', 'Bow') + ' ' + vm.nuevoBarrio.nombre, "Información");
                        limpiarFormularioBarrio();
                    }).error(function (error) {
                        abp.notify.error(error.message);
                    });
        }
        
        /********************************************************
         * ELIMINAR BARRIO
         ********************************************************/
        vm.puedeEliminarBarrio = function (barrio, funcionRetornarPuedeEliminar) {
            zonificacionService.puedeEliminarBarrio({ Id: barrio.id }).success(function (data) {
                if (data.puedeEliminar) {
                    vm.eliminandoBarrio = true;
                }
                funcionRetornarPuedeEliminar(data.puedeEliminar);
            });
        }

        vm.noPuedeEliminarBarrio = function () {
            abp.notify.error(abp.localization.localize('zonificacion_barrio_NonotificacionEliminadoBarrio', 'Bow'),
                    abp.localization.localize('zonificacion_pais_informacion', 'Bow'));
        }

        vm.eliminarBarrioOk = function (barrio) {
            zonificacionService.deleteBarrio({ id: barrio.id })
            .success(function () {
                cargarBarriosLocalidad ($stateParams.localidadId);
                vm.eliminandoBarrio = false;
                abp.notify.success(abp.localization.localize('zonificacion_barrio_notificacionEliminadoBarrio', 'Bow') + ' ' + barrio.nombre, "Información");
            }).error(function (error) {
                abp.notify.error(error.message);
            });
        }

        vm.eliminarBarrioCancel = function () {
            vm.eliminandoBarrio = false;
        }


        /********************************************************
         * CONSULTAR MANZANAS DE BARRIO
         *******************************************************/
        vm.consultarManzanasBarrio = function (barrio, posicion) {
            vm.seleccionBarrio.seleccionar((vm.barriosLocalidad.filasPorPagina * (vm.barriosLocalidad.paginaActual - 1)) + posicion);
            vm.barrioSeleccionNombre = barrio.nombre;
            vm.barrioSeleccion = barrio.id;
            vm.controlOperacionesNuevaManzana.asignarBarrio(barrio.id);
            zonificacionService.getManzanasByBarrio({ id: barrio.id })
                .success(function (data) {
                    vm.manzanas = bow.tablas.paginar(data.manzanas, 10);
                });
        }

        /******************************************************
         * REGISTRO MANZANA
         *****************************************************/
        vm.registroManzana = function (manzana) {
            zonificacionService.getManzanasByBarrio({ id: vm.barrioSeleccion })
                .success(function (data) {
                    vm.manzanas = bow.tablas.paginar(data.manzanas, 5);
                });
        }

        vm.cierreFormularioManzana = function () {
            vm.ingresandoManzana = false;
        }

        /******************************************************
         * ELIMINAR MANZANA
         *****************************************************/
        vm.puedeEliminarManzana = function (manzana, funcionRetornarPuedeEliminar) {
            zonificacionService.puedeEliminarManzana({ Id: manzana.id }).success(function (data) {
                if (data.puedeEliminar) {
                    vm.eliminandoManzana = true;
                }
                funcionRetornarPuedeEliminar(data.puedeEliminar);
            });
        }

        vm.noPuedeEliminarManzana = function () {
            abp.notify.error(abp.localization.localize('zonificacion_manzana_NonotificacionEliminadoManzana', 'Bow'),
                    abp.localization.localize('zonificacion_pais_informacion', 'Bow'));
        }

        vm.eliminarManzanaOk = function (manzana) {
            zonificacionService.deleteManzana({ id: manzana.id })
            .success(function () {
                zonificacionService.getManzanasByBarrio({ id: vm.barrioSeleccion })
                .success(function (data) {
                    vm.manzanas = bow.tablas.paginar(data.manzanas, 10);
                });
                vm.eliminandoManzana = false;
                abp.notify.info(abp.localization.localize('zonificacion_manzana_notificacionEliminadoManzana', 'Bow'), abp.localization.localize('zonificacion_manzana_informacion', 'Bow'));
            }).error(function (error) {
                abp.notify.error(error.message);
            });
        }

        vm.eliminarManzanaCancel = function () {
            vm.eliminandoManzana = false;
        }





        

        vm.avenidaLocalidad = {
            localidadId: $stateParams.localidadId,
            nombre: ''
        };


        //Consultando la información de la localidad para mostrarla en pantalla
        zonificacionService.getLocalidadWithDepartamentoAndPais({ id: $stateParams.localidadId }).success(function (data) {
            vm.localidad = data;
        });

    }]);
})();