(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.zonificacion.pais';

    /*****************************************************************
     * 
     * CONTROLADOR PAIS
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', 'abp.services.app.zonificacion',
        function ($scope, zonificacionService) {
            var vm = this;

            //Inicializando modelos
            vm.editandoPais = false;
            vm.eliminandoPais = false;
            vm.nombrePaisEditar = undefined;
            vm.paises = [];
            vm.nuevoPais = {
                nombre: '',
                indicativo: ''
            };

            /********************************************************************
             * FUNCIONES
             ********************************************************************/
            function cargarPaises() {
                zonificacionService.getPaises().success(function (data) {
                    vm.paises = bow.tablas.paginar(data.paises, 5);
                });
            }
            cargarPaises();

            function limpiarFormulario() {
                vm.nuevoPais = {
                    nombre: '',
                    indicativo: ''
                };
                $scope.frmPais.$setPristine();
            };

            //Controles para editar o eliminar un país
            vm.controles = {
                visible: [],
                mostrar: function ($index) {
                    vm.controles.visible[$index] = true;
                    vm.eliminandoPais = false;
                },
                ocultar: function ($index) {
                    vm.controles.visible[$index] = false;
                    vm.eliminandoPais = false;
                }
            };
            

            /*********************************************
             * REGISTRAR PAIS
             *********************************************/
            function savePais() {
                zonificacionService.savePais(vm.nuevoPais)
                .success(function () {
                    cargarPaises();
                    abp.notify.success(abp.localization.localize('zonificacion_pais_notificacionInsertadoPais', 'Bow') + ' ' + vm.nuevoPais.nombre,
                    abp.localization.localize('zonificacion_pais_informacion', 'Bow'));
                    limpiarFormulario();
                })
                .error(function (error) {
                    abp.notify.error(error.message);
                });
            };

            vm.saveOrUpdatePais = function () {
                if (vm.editandoPais) {
                    updatePais();
                }
                else {
                    savePais();
                }
            }


            /********************************************
             * EDITAR PAIS
             *******************************************/
            function updatePais() {
                zonificacionService.updatePais(vm.nuevoPais)
                    .success(function () {
                        cargarPaises();
                        vm.editandoPais = false;
                        abp.notify.success(abp.localization.localize('zonificacion_pais_notificacionActualizadoPais', 'Bow') + ' ' + vm.nuevoPais.nombre,
                        abp.localization.localize('zonificacion_pais_informacion', 'Bow'));
                        limpiarFormulario();

                    }).error(function (error) {
                        abp.notify.error(error.message);
                    });
            };

            vm.editarPais = function (pais) {
                vm.editandoPais = true;
                vm.nuevoPais = {
                    id: pais.id,
                    nombre: pais.nombre,
                    indicativo: pais.indicativo
                };
                vm.nombrePaisEditar = pais.nombre;
            };

            vm.cancelarUpdatePais = function () {
                if (vm.editandoPais) {
                    vm.editandoPais = false;
                }
                limpiarFormulario();
            }

            /********************************************
             * ELIMINAR PAIS
             ********************************************/
            vm.eliminarPaisOk = function (pais) {
                zonificacionService.deletePais({ id: pais.id })
                   .success(function (data) {
                       abp.notify.info(abp.localization.localize('zonificacion_pais_notificacionEliminadoPais', 'Bow') + ' ' + pais.nombre,
                           abp.localization.localize('zonificacion_pais_informacion', 'Bow'));

                       cargarPaises();
                       vm.eliminandoPais = false;
                   });
            };

            vm.eliminarPaisCancel = function () {
                vm.eliminandoPais = false;
            };

            vm.puedeEliminarPais = function (paisId, funcionRetornarPuedeEliminar) {
                zonificacionService.puedeEliminarPais({ id: paisId }).success(function (data) {
                    if (data.puedeEliminar) {
                        vm.eliminandoPais = true;
                    }
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('zonificacion_pais_notificacionNoEliminarPais', 'Bow'),
                            abp.localization.localize('zonificacion_pais', 'Bow'));
            }

        }]);
})();

