(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.zonificacion.localidad';

    /*****************************************************************
     * 
     * CONTROLADOR LOCALIDAD
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', '$stateParams', 'abp.services.app.zonificacion',
        function ($scope, $modal, $stateParams, zonificacionService) {
            var vm = this;
            vm.departamentoId = $stateParams.departamentoId;

            //Inicializando modelos
            vm.editandoLocalidad = false;
            vm.eliminandoLocalidad = false;

            vm.localidadEditar = "";
            vm.localidades = [];

            vm.nuevaLocalidad = {
                nombre: '',
                habitantes: '',
                departamentoId: vm.departamentoId
            };

            //Controles para editar o eliminar una localidad
            vm.controles = {
                visible: [],
                mostrar: function ($index) {
                    vm.controles.visible[$index] = true;
                    vm.eliminandoLocalidad = false;
                },
                ocultar: function ($index) {
                    vm.controles.visible[$index] = false;
                }
            };

            vm.saveOrUpdateLocalidad = function () {
                if (vm.editandoLocalidad) {
                    updateLocalidad();
                }
                else {
                    saveLocalidad();
                }
            }

            vm.cancelarUpdateLocalidad = function () {
                limpiarFormulario();
                vm.editandoLocalidad = false;
            }

            vm.editarLocalidad = function (localidad) {
                vm.editandoLocalidad = true;
                vm.nuevaLocalidad = {
                    id: localidad.id,
                    nombre: localidad.nombre,
                    habitantes: localidad.habitantes,
                    departamentoId: vm.departamentoId
                };
                vm.nombreLocalidadEditar = localidad.nombre;
            };

            //Registrar localidad
            function saveLocalidad() {
                zonificacionService.saveLocalidad(vm.nuevaLocalidad)
                .success(function () {
                    cargarLocalidades();
                    abp.notify.success(abp.localization.localize('zonificacion_localidad_notificacionInsertadoLocalidad', 'Bow') + ' ' + vm.nuevaLocalidad.nombre,
                        abp.localization.localize('zonificacion_localidad_informacion', 'Bow')
                        );
                    limpiarFormulario();
                })
                .error(function (error) {
                    abp.notify.error(error.message);
                });
            };

            function updateLocalidad() {
                zonificacionService.updateLocalidad(vm.nuevaLocalidad)
                    .success(function () {
                        cargarLocalidades();
                        vm.editandoLocalidad = false;
                        abp.notify.success(abp.localization.localize('zonificacion_pais_notificacionActualizadoLocalidad', 'Bow') + ' ' + vm.nuevaLocalidad.nombre,
                        abp.localization.localize('zonificacion_localidad_informacion', 'Bow'));
                        limpiarFormulario();

                    }).error(function (error) {
                        abp.notify.error(error.message);
                    });
            };

            vm.puedeEliminarLocalidad = function (localidadId, funcionRetornarPuedeEliminar) {
                zonificacionService.puedeEliminarLocalidad({ id: localidadId }).success(function (data) {
                    if (data.puedeEliminar) {
                        vm.eliminandoLocalidad = true;
                    }
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('zonificacion_localidad_NonotificacionEliminadoLocalidad', 'Bow'),
                abp.localization.localize('zonificacion_localidad_informacion', 'Bow'));
            }

            vm.eliminarLocalidadOk = function (localidad) {
                zonificacionService.deleteLocalidad({ id: localidad.id })
                   .success(function (data) {
                       abp.notify.info(abp.localization.localize('zonificacion_localidad_notificacionEliminadoLocalidad', 'Bow') + ' ' + localidad.nombre,
                       abp.localization.localize('zonificacion_localidad_informacion', 'Bow'));

                       cargarLocalidades();
                       vm.eliminandoLocalidad = false;
                   });
            };

            vm.eliminarLocalidadCancel = function () {
                vm.eliminandoLocalidad = false;
            };

            function limpiarFormulario() {
                vm.nuevaLocalidad = {
                    nombre: '',
                    habitantes: '',
                    departamentoId: vm.departamentoId
                };
                $scope.frmLocalidad.$setPristine();
            }

            //Consultando la información de la localidad para mostrarla en pantalla
            zonificacionService.getDepartamentoWithPais({ id: $stateParams.departamentoId }).success(function (data) {
                vm.localidad = data;
            });

            //Carga inicial de localidades para mostrar en pantalla
            cargarLocalidades();

            //Función encargada de consultar las localidades en la base de datos
            function cargarLocalidades() {
                zonificacionService.getLocalidades({ id: $stateParams.departamentoId }).success(function (data) {
                    vm.localidades = bow.tablas.paginar(data.localidades, 10);
                });
            }

            ///************************************************************************
            // * Llamado para abrir Modal para Nueva Localidad
            // ************************************************************************/
            //vm.abrirModalNuevaLocalidad = function (departamentoId) {
            //    var modalInstance = $modal.open({
            //        templateUrl: '/App/Main/views/zonificacion/localidad/partials/modalNuevaLocalidad.cshtml',
            //        controller: 'modalNuevaLocalidadController',
            //        size: 'md',
            //        resolve: {
            //            dptoId: function () {
            //                return departamentoId;
            //            }
            //        }

            //    });

            //    modalInstance.result.then(function () {
            //        cargarLocalidades();
            //        abp.notify.success("Se ha Registrado la localidad ", "Información");
            //    }, function () { vm.resultado = "No devolvio" });
            //}

            ///************************************************************************
            // * Llamado para abrir Modal para editar Localidad
            // ************************************************************************/
            //vm.abrirModalEditar = function (localidadId) {
            //    var modalInstance = $modal.open({
            //        templateUrl: '/App/Main/views/zonificacion/localidad/partials/modalEditarLocalidad.cshtml',
            //        controller: 'modalEditarLocalidadController',
            //        size: 'md',
            //        resolve: {
            //            localidadEditar: function () {
            //                return localidadId;
            //            }
            //        }
            //    });

            //    modalInstance.result.then(function () {
            //        cargarLocalidades();
            //        abp.notify.success("Se ha Actualizado la localidad ", "Información");
            //    }, function () { vm.resultado = "No devolvio" + localidadId });
            //}

            ///********************************************************************
            // * Llamado para abrir Modal para eliminar Localidad
            // ********************************************************************/
            //vm.abrirModalEliminar = function (localidadId) {

            //    zonificacionService.puedeEliminarLocalidad({ Id: localidadId }).success(function (data) {

            //        var puedeEliminar = data.puedeEliminar;

            //        if (puedeEliminar == true) {

            //            var modalInstance = $modal.open({
            //                templateUrl: '/App/Main/views/zonificacion/localidad/partials/modalEliminarLocalidad.cshtml',
            //                controller: 'modalEliminarLocalidadController',
            //                size: 'md',
            //                resolve: {
            //                    localidadEliminar: function () {
            //                        return localidadId;
            //                    }
            //                }
            //            });

            //            modalInstance.result.then(function () {
            //                cargarLocalidades();
            //                abp.notify.success("Se ha Eliminado la localidad ", "Información");
            //            }, function () { vm.resultado = "No devolvio" + localidadId });
            //        }
            //        else {
            //            abp.notify.error(abp.localization.localize('zonificacion_localidad_NonotificacionEliminadoLocalidad', 'Bow'),
            //            abp.localization.localize('zonificacion_localidad_informacion', 'Bow')
            //         );
            //        }
            //    });
            //}


        }]);
})();

//(function () {
//    //Nombre del Controlador
//    var controllerId = 'app.views.zonificacion.localidad';

//    /*****************************************************************
//     * 
//     * CONTROLADOR LOCALIDAD
//     * 
//     *****************************************************************/
//    angular.module('app').controller(controllerId, ['$scope', '$modal', '$stateParams', 'abp.services.app.zonificacion',
//        function ($scope, $modal, $stateParams, zonificacionService) {
//            var vm = this;
//            vm.departamentoId = $stateParams.departamentoId;

//            //Inicializando modelos
//            vm.localidadEditar = "";
//            vm.localidades = [];
//            vm.inputLocalidad = {
//                nombre: '',
//                habitantes: ''
//            };

//            //Consultando la información de la localidad para mostrarla en pantalla
//            zonificacionService.getDepartamentoWithPais({ id: $stateParams.departamentoId }).success(function (data) {
//                vm.localidad = data;
//            });

//            //Carga inicial de localidades para mostrar en pantalla
//            cargarLocalidades();

//            //Función encargada de consultar las localidades en la base de datos
//            function cargarLocalidades() {
//                zonificacionService.getLocalidades({ id: $stateParams.departamentoId }).success(function (data) {                 
//                    vm.localidades = bow.tablas.paginar(data.localidades, 10);
//                });
//            }

//            /************************************************************************
//             * Llamado para abrir Modal para Nueva Localidad
//             ************************************************************************/
//            vm.abrirModalNuevaLocalidad = function (departamentoId) {
//                var modalInstance = $modal.open({
//                    templateUrl: '/App/Main/views/zonificacion/localidad/partials/modalNuevaLocalidad.cshtml',
//                    controller: 'modalNuevaLocalidadController',
//                    size: 'md',
//                    resolve: {
//                        dptoId: function () {
//                            return departamentoId;
//                        }
//                    }

//                });

//                modalInstance.result.then(function () {
//                    cargarLocalidades();
//                    abp.notify.success("Se ha Registrado la localidad ", "Información");
//                }, function () { vm.resultado = "No devolvio" });
//            }

//            /************************************************************************
//             * Llamado para abrir Modal para editar Localidad
//             ************************************************************************/
//            vm.abrirModalEditar = function (localidadId) {
//                var modalInstance = $modal.open({
//                    templateUrl: '/App/Main/views/zonificacion/localidad/partials/modalEditarLocalidad.cshtml',
//                    controller: 'modalEditarLocalidadController',
//                    size: 'md',
//                    resolve: {
//                        localidadEditar: function () {
//                            return localidadId;
//                        }
//                    }
//                });

//                modalInstance.result.then(function () {
//                    cargarLocalidades();
//                    abp.notify.success("Se ha Actualizado la localidad ", "Información");
//                }, function () { vm.resultado = "No devolvio" + localidadId });
//            }

//            /********************************************************************
//             * Llamado para abrir Modal para eliminar Localidad
//             ********************************************************************/
//            vm.abrirModalEliminar = function (localidadId) {

//                zonificacionService.puedeEliminarLocalidad({ Id: localidadId }).success(function (data) {

//                    var puedeEliminar = data.puedeEliminar;

//                    if (puedeEliminar == true) {

//                        var modalInstance = $modal.open({
//                            templateUrl: '/App/Main/views/zonificacion/localidad/partials/modalEliminarLocalidad.cshtml',
//                            controller: 'modalEliminarLocalidadController',
//                            size: 'md',
//                            resolve: {
//                                localidadEliminar: function () {
//                                    return localidadId;
//                                }
//                            }
//                        });

//                        modalInstance.result.then(function () {
//                            cargarLocalidades();
//                            abp.notify.success("Se ha Eliminado la localidad ", "Información");
//                        }, function () { vm.resultado = "No devolvio" + localidadId });
//                    }
//                    else {
//                        abp.notify.error(abp.localization.localize('zonificacion_localidad_NonotificacionEliminadoLocalidad', 'Bow'),
//                        abp.localization.localize('zonificacion_localidad_informacion', 'Bow')
//                     );
//                    }
//                });
//            }


//        }]);
//})();