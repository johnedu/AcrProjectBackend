(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.afiliaciones.parentesco';

    /*****************************************************************
     * 
     * CONTROLADOR PARENTESCOS
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', 'abp.services.app.afiliaciones',
        function ($scope, afiliacionesService) {
            var vm = this;

            vm.edadLimiteRequerida = false;

            vm.editandoParentesco = false;
            vm.eliminandoParentesco = false;

            vm.nuevoParentesco = {
                id: '',
                nombre: '',
                posicion: '',
                genero: 'M',
                repeticiones: '',
                limite: undefined,
                edadDiferencia: undefined,
                coincidirApellidos: ''
            };

            vm.parentescos = [];

            vm.posiciones = [];

            vm.cantidadParentescos = 0;

            vm.cargarParentescos = function () {
                afiliacionesService.getAllParentescos()
                   .success(function (data) {
                       vm.parentescos = bow.tablas.paginar(data.parentescos, 10);// data.parentescos

                       vm.cantidadParentescos = data.parentescos.length;
                       vm.cargarPosiciones();
                   });
            };

            vm.cargarParentescos();

            //Funcion para calcular los valores para el dropdown posicion dependiendo de la cantidad de parentescos registrados
            vm.cargarPosiciones = function () {

                vm.posiciones = [];

                for (var i = 0; i <= vm.cantidadParentescos; i++) {
                    vm.posiciones.push({ pos: i + 1 });
                }

                vm.posicion = vm.posiciones[vm.cantidadParentescos];
            };

            //Controles para editar o eliminar parentescos
            vm.controlesParentescos = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesParentescos.visible[$index] = true;
                    vm.eliminandoParentesco = false;
                },
                ocultar: function ($index) {
                    vm.controlesParentescos.visible[$index] = false;
                    vm.eliminandoParentesco = false;
                }
            };

            vm.editarParentesco = function (parentescoId) {
                vm.editandoParentesco = true;

                afiliacionesService.getParentesco({ id: parentescoId })
                .success(function (data) {

                    if (!vm.nuevoParentesco.id) {
                        vm.posiciones.pop();
                    }

                    for (var i = 0; i < vm.posiciones.length; i++) {
                        if (vm.posiciones[i].pos == data.posicion) {
                            vm.posicion = vm.posiciones[i];
                        }
                    }

                    vm.nuevoParentesco = {
                        id: data.id,
                        nombre: data.nombre,
                        posicion: data.posicion,
                        genero: String(data.genero),
                        repeticiones: data.repeticiones,
                        limite: data.limite,
                        edadDiferencia: data.edadDiferencia,
                        coincidirApellidos: data.coincidirApellidos
                    };

                    vm.nombreParentescoEditar = data.nombre;

                    if (data.limite == null) {
                        vm.nuevoParentesco.limite = undefined;
                        vm.mostrarRestriccion = false;
                        vm.restriccionEdad = false;
                    } else {
                        vm.mostrarRestriccion = true;
                        vm.restriccionEdad = true;
                    }

                })
            };

            vm.puedeEliminarParentesco = function (parentescoId, funcionRetornarPuedeEliminar) {
                afiliacionesService.puedeEliminarParentesco({ id: parentescoId })
                    .success(function (data) {
                        if (data.puedeEliminar) {
                            vm.eliminandoParentesco = true;
                        }
                        funcionRetornarPuedeEliminar(data.puedeEliminar);
                    });
            }

            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('afiliaciones_gestionarparentescos_notificacionNoEliminarParentesco', 'Bow'),
                abp.localization.localize('afiliaciones_gestionarparentescos_informacion', 'Bow'));
            }

            vm.eliminarParentescoOk = function (parentescoId, posicion) {
                afiliacionesService.deleteParentesco({ id: parentescoId, posicion: posicion })
                   .success(function (data) {
                       abp.notify.info(abp.localization.localize('afiliaciones_gestionarparentescos_notificacionEliminadoParentesco', 'Bow'),
                       abp.localization.localize('afiliaciones_gestionarparentescos_informacion', 'Bow'));

                       vm.cargarParentescos();
                       vm.eliminandoParentesco = false;
                   });
            };

            vm.eliminarParentescoCancel = function () {
                vm.eliminandoParentesco = false;
            };

            vm.cancelarUpdateParentesco = function () {
                if (vm.editandoParentesco) {
                    vm.editandoParentesco = false;
                }
                vm.limpiarFormulario();
            };

            vm.limpiarFormulario = function () {
                vm.nuevoParentesco = {
                    id: '',
                    nombre: '',
                    posicion: '',
                    genero: 'M',
                    repeticiones: '',
                    limite: undefined,
                    edadDiferencia: undefined,
                    coincidirApellidos: ''
                };

                vm.restriccionEdad = false;
                vm.mostrarRestriccion = false;
                vm.edadLimiteRequerida = false;

                $scope.frmParentesco.$setPristine();

            };

            vm.saveOrUpdateParentesco = function () {
                if (vm.editandoParentesco) {
                    updateParentesco();
                }
                else {
                    saveParentesco();
                }
            }

            function saveParentesco() {
                vm.nuevoParentesco.posicion = vm.posicion.pos;

                afiliacionesService.saveParentesco(vm.nuevoParentesco)
                .success(function () {
                    vm.cargarParentescos();
                    abp.notify.success(abp.localization.localize('afiliaciones_gestionarparentescos_notificacionGuardadoParentesco' + ' ' + vm.nuevoParentesco.nombre, 'Bow'),
                    abp.localization.localize('afiliaciones_gestionarparentescos_informacion', 'Bow'));
                    vm.limpiarFormulario();
                })
                .error(function (error) {
                    abp.notify.error(error.message);
                });
            };

            function updateParentesco() {
                vm.nuevoParentesco.posicion = vm.posicion.pos;

                afiliacionesService.updateParentesco(vm.nuevoParentesco)
                    .success(function () {
                        vm.cargarParentescos();
                        vm.editandoParentesco = false;
                        abp.notify.success(abp.localization.localize('afiliaciones_gestionarparentescos_notificacionActualizadoParentesco' + ' ' + vm.nuevoParentesco.nombre, 'Bow'),
                        abp.localization.localize('afiliaciones_gestionarparentescos_informacion', 'Bow'));
                        vm.limpiarFormulario();

                    }).error(function (error) {
                        abp.notify.error(error.message);
                    });
            };


            vm.validarRestriccion = function () {
                vm.mostrarRestriccion = !vm.mostrarRestriccion;

                if (vm.restriccionEdad) {
                    vm.edadLimiteRequerida = true;
                } else {
                    vm.edadLimiteRequerida = false;
                }
            };

        }]);
})();
