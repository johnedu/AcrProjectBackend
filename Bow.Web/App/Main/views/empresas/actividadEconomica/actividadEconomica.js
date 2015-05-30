(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.empresas.actividadEconomica';

    /*****************************************************************
     * 
     * CONTROLADOR ACTIVIDAD ECONOMICA
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.empresas',
        function ($scope, $modal, empresasService) {
            var vm = this;

            //Inicializando modelos
            vm.editandoActividad = false;
            vm.eliminandoActividad = false;

            vm.actividadEconomicaEditar = "";
            vm.actividadesEconomicas = [];
            $scope.mensajeEliminar = [];

            vm.inputActividadEconomica = {
                codigo: '',
                nombre: ''
            };

            cargarActividadesEconomicas();

            ///********************************************************************
            // * Función encargada de consultar las actividades en la  base de datos
            // ********************************************************************/
            function cargarActividadesEconomicas() {
                empresasService.getActividadesEconomicas().success(function (data) {
                    vm.actividadesEconomicas = bow.tablas.paginar(data.actividadesEconomicas, 10);
                });
            };

            vm.saveOrUpdateActividad = function () {
                if (vm.editandoActividad) {
                    updateActividad();
                }
                else {
                    saveActividad();
                }
            };

            function updateActividad() {
                empresasService.updateActividadEconomica(vm.nuevaActividad)
                  .success(function () {
                      cargarActividadesEconomicas();
                      vm.editandoActividad = false;
                      abp.notify.success(abp.localization.localize('empresas_actividadeEconomica_notificacionModificadoActividad', 'Bow') + ' ' + vm.nuevaActividad.codigo,
                      abp.localization.localize('empresas_actividadeEconomica_informacion', 'Bow'));
                      limpiarFormulario();
                  }).error(function (error) {
                      $scope.mensajeError = error.message;
                  });
            };

            function saveActividad() {
                empresasService.saveActividadEconomica(vm.nuevaActividad)
                  .success(function () {
                      cargarActividadesEconomicas();
                      abp.notify.success(abp.localization.localize('empresas_actividadeEconomica_notificacionInsertadoActividad', 'Bow') + ' ' + vm.nuevaActividad.codigo,
                      abp.localization.localize('empresas_actividadeEconomica_informacion', 'Bow'));
                      limpiarFormulario();
                  }).error(function (error) {
                      $scope.mensajeError = error.message;
                  });
            };

            vm.editarActividad = function (actividad) {
                vm.editandoActividad = true;

                vm.nuevaActividad = {
                    id: actividad.id,
                    codigo: actividad.codigo,
                    nombre: actividad.nombre
                };

                vm.nombreActividadEditar = actividad.nombre;
            };

            vm.cancelarUpdateActividad = function () {
                if (vm.editandoActividad) {
                    vm.editandoActividad = false;
                }
                limpiarFormulario();
            }

            vm.eliminarActividadOk = function (actividad) {
                empresasService.deleteActividadEconomica({ id: actividad.id })
                   .success(function (data) {
                       abp.notify.info(abp.localization.localize('empresas_actividadEconomica_notificacionEliminadoActividad', 'Bow'),
                       abp.localization.localize('empresas_actividadeEconomica_informacion', 'Bow'));
                       cargarActividadesEconomicas();
                   });
            }

            vm.eliminarActividadCancel = function () {
                vm.eliminandoActividad = false;
            };

            vm.puedeEliminarActividad = function (actividadId, funcionRetornarPuedeEliminar) {
                empresasService.puedeEliminarActividadEconomica({ id: actividadId }).success(function (data) {
                    if (data.puedeEliminar) {
                        vm.eliminandoActividad = true;
                    }
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('zonificacion_pais_notificacionNoEliminarActividad', 'Bow'),
                abp.localization.localize('empresas_actividadeEconomica_informacion', 'Bow'));
            }


            function limpiarFormulario() {
                vm.nuevaActividad = {
                    codigo: '',
                    nombre: ''
                };
                $scope.frmActividad.$setPristine();
            };

            //Controles para editar o eliminar un país
            vm.controles = {
                visible: [],
                mostrar: function ($index) {
                    vm.controles.visible[$index] = true;
                    vm.eliminandoActividad = false;
                },
                ocultar: function ($index) {
                    vm.controles.visible[$index] = false;
                    vm.eliminandoActividad = false;
                }
            };

            //$scope.eliminarOpcionOk = function (actividadEconomicaId, $index) {

            //    empresasService.deleteActividadEconomica({ id: actividadEconomicaId })
            //      .success(function (data) {
            //          $scope.mensajeEliminar[$index] = false;
            //          abp.notify.info(abp.localization.localize('empresas_actividadEconomica_notificacionEliminadoActividad', 'Bow'), abp.localization.localize('empresas_actividadEconomica_informacion', 'Bow'));
            //          cargarActividadesEconomicas();
            //      });
            //};

            //$scope.eliminarOpcionCancel = function ($index) {
            //    $scope.mensajeEliminar[$index] = false;
            //};

            //$scope.cerrarModal = function () {
            //    $modalInstance.close('cancel');
            //};

            //$scope.cancelModal = function () {
            //    cargarActividadesEconomicas();
            //};


            /////************************************************************************
            //// * Llamado para abrir Modal para Nueva Actividad
            //// ************************************************************************/
            //vm.abrirModalNuevaActividadEconomica = function () {
            //    var modalInstance = $modal.open({
            //        templateUrl: '/App/Main/views/empresas/actividadEconomica/partials/modalNuevaActividadEconomica.cshtml',
            //        controller: 'modalNuevaActividadEconomicaController',
            //        size: 'md'
            //    });

            //    modalInstance.result.then(function (nombreActividad) {
            //        cargarActividadesEconomicas();
            //        abp.notify.success(abp.localization.localize('empresas_actividadeEconomica_notificacionInsertadoActividad', 'Bow') + ' ' + nombreActividad,
            //            abp.localization.localize('empresas_actividadeEconomica_informacion', 'Bow')
            //            );
            //    }, function () {
            //        console.log("Ocurrió un problema al cargar el modal del nueva actividad:  " );
            //    });
            //}

            /////************************************************************************
            //// * Llamado para abrir Modal para editar Actividad Economica
            //// ************************************************************************/
            //vm.abrirModalEditarActividadEconomica = function (actividadEconomicaId) {
            //    var modalInstance = $modal.open({
            //        templateUrl: '/App/Main/views/empresas/actividadEconomica/partials/modalEditarActividadEconomica.cshtml',
            //        controller: 'modalEditarActividadEconomicaController',
            //        size: 'md',
            //        resolve: {
            //            actividadEconomicaEditar: function () {
            //                return actividadEconomicaId;
            //            }
            //        }
            //    });

            //    modalInstance.result.then(function (nombreActividadEconomica) {
            //        cargarActividadesEconomicas();
            //        abp.notify.success(abp.localization.localize('empresas_actividadeEconomica_notificacionModificadoActividad', 'Bow') + ' ' + nombreActividadEconomica,
            //            abp.localization.localize('empresas_actividadeEconomica_informacion', 'Bow')
            //            );
            //    }, function () {
            //        console.log("Ocurrió un problema al cargar el modal de edición de la actividad: " );
            //    });
            //}

            /////*************************************************************
            /////  Eliminar actividad económica
            /////*************************************************************


            //$scope.eliminarActividad = function (actividadEconomicaId, $index) {

            //    empresasService.puedeEliminarActividadEconomica({ id: actividadEconomicaId }).success(function (data) {
            //                var puedeEliminar = data.puedeEliminar;

            //                if (puedeEliminar == true) {                              
            //                    $scope.mensajeEliminar[$index] = true;                     
            //                }
            //                else {
            //                    abp.notify.error(abp.localization.localize('empresas_actividadEconomica_notificacionNoEliminadoActividad', 'Bow'),
            //                    abp.localization.localize('empresas_actividadEconomica_informacion', 'Bow')
            //                        );
            //                }
            //            });               
            //};               



        }]);
})();