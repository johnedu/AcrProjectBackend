(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.afiliaciones.beneficio';

    /*****************************************************************
     * 
     * CONTROLADOR BENEFICIO
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.parametros', 'abp.services.app.afiliaciones',
        function ($scope, $modal, parametrosService, afiliacionesService) {

            var vm = this;

            vm.categoriaSeleccionada = 0;

            vm.nuevaCategoria = {
                id: '',
                nombre: '',
                esNuevo: ''
            };

            vm.nuevoBeneficio = {
                tipoId: '',
                nombre: '',
                descripcion: '',
                esNuevo: ''
            };

            vm.eliminadoCategoria = false;
            vm.eliminadoBeneficio = false;

            //Controles para editar o eliminar categorias
            vm.controlesCategorias = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesCategorias.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesCategorias.visible[$index] = false;
                    vm.eliminadoCategoria = false;
                }
            };

            //Controles para editar o eliminar beneficios
            vm.controlesBeneficios = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesBeneficios.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesBeneficios.visible[$index] = false;
                    vm.eliminadoBeneficio = false;
                }
            };

            //Funcion temporal que verifica si se puede eliminar un registro (funcion para directiva)
            vm.puedeEliminar = function (id, funcionRetornarPuedeEliminar) {
                afiliacionesService.getBeneficiosByCategoria({ tipoCategoriaId: id }).success(function (data) {
                    if (!data.beneficios.length) {
                        vm.eliminadoCategoria = true;
                        funcionRetornarPuedeEliminar(true);
                    }
                    else {
                        vm.eliminadoCategoria = false;
                        funcionRetornarPuedeEliminar(false);
                    }
                });
            };

            //Funcion temporal que se ejecuta cuando no se se puede eliminar un registro
            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('', 'Bow') + 'No se puede eliminar la categoría porque tiene beneficios asociados', abp.localization.localize('afiliaciones_beneficio_error', 'Bow'));
            };

            //Funcion para eliminar una Categoria
            vm.deleteCategoria = function (categoriaId) {
                parametrosService.deleteCategoria({ id: categoriaId }).success(function () {
                    vm.mostraBeneficios = false;
                    vm.eliminadoCategoria = false;
                    cargarCategorias();
                })
                   .error(function (error) {
                       abp.notify.error(error.message, abp.localization.localize('afiliaciones_beneficio_error', 'Bow'));
                   });
            }

            vm.cancelarEliminar = function () {
                vm.eliminadoCategoria = false;
            };

            //Funcion encargada de consultar las categorias en la base de datos
            function cargarCategorias() {
                parametrosService.getCategorias().success(function (data) {
                    vm.categorias = data.categorias;
                });
            };

            cargarCategorias();

            //Funcion para almacenar una categoria
            vm.saveCategoria = function () {
                //if (vm.nuevaCategoria.esNuevo == true) {
                //    vm.nuevaCategoria.esNuevo = true;
                //} else if (vm.nuevaCategoria.esNuevo == false) {
                //    vm.nuevaCategoria.esNuevo = false;
                //}

                parametrosService.saveCategoria(vm.nuevaCategoria).success(function () {
                    vm.mostrarNuevaCategoria = false;

                    if (vm.nuevaCategoria.esNuevo == true) {
                        abp.notify.success(abp.localization.localize('afiliaciones_beneficio_notificacionInsertadoCategoria', 'Bow') + ' ' + vm.nuevaCategoria.nombre,
                        abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                    } else if (vm.nuevaCategoria.esNuevo == false) {
                        abp.notify.success(abp.localization.localize('afiliaciones_beneficio_notificacionActualizadoCategoria', 'Bow') + ' ' + vm.nuevaCategoria.nombre,
                        abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                    }
                    cargarCategorias();
                })
                   .error(function (error) {
                       abp.notify.error(error.message, abp.localization.localize('afiliaciones_beneficio_error', 'Bow'));
                   });
            };

            vm.editarCategoria = function (categoriaId) {
                vm.nuevaCategoria.esNuevo = false;
                vm.mostrarNuevaCategoria = true;

                parametrosService.getTipo({ id: categoriaId }).success(function (data) {
                    vm.nuevaCategoria.id = data.id;
                    vm.nuevaCategoria.nombre = data.nombre;
                })
            }

            //Funcion para cargar el formulario de la nueva categoria
            vm.formularioNuevaCategoria = function () {
                $scope.formNuevaCategoria.$setPristine();
                vm.nuevaCategoria.nombre = "";
                vm.nuevaCategoria.esNuevo = true;
                vm.mostrarNuevaCategoria = !vm.mostrarNuevaCategoria;
            };


            //************************* Beneficios **************************

            //Funcion para eliminar una Categoria
            vm.deleteBeneficio = function (beneficioId) {
                afiliacionesService.deleteBeneficio({ id: beneficioId }).success(function () {
                    vm.cargarBeneficios(vm.categoriaSeleccionada);
                    vm.eliminadoBeneficio = false;
                })
                   .error(function (error) {
                       abp.notify.error(error.message, abp.localization.localize('afiliaciones_beneficio_error', 'Bow'));
                   });
            }

            //Funcion temporal que verifica si se puede eliminar un registro (funcion para directiva)
            vm.puedeEliminarBeneficio = function (id, funcionRetornarPuedeEliminar) {
                vm.eliminadoBeneficio = true;
                funcionRetornarPuedeEliminar(true);
            };

            //Funcion temporal que se ejecuta cuando no se se puede eliminar un registro
            vm.noPuedeEliminarBeneficio = function () {

            };

            vm.cancelarEliminarBeneficio = function () {
                vm.eliminadoBeneficio = false;
            };

            //Funcion encargada de consultar los beneficios segun la categoria en la base de datos
            vm.cargarBeneficios = function (categoriaId) {
                vm.categoriaSeleccionada = categoriaId;
                vm.mostraBeneficios = true;
                afiliacionesService.getBeneficiosByCategoria({ tipoCategoriaId: categoriaId }).success(function (data) {
                    vm.tipoCategoria = data.tipoCategoria;
                    vm.beneficios = data.beneficios;
                });
            };

            //Funcion para alamacenar un nuevo beneficio
            vm.saveBeneficio = function () {
                if (vm.nuevoBeneficio.esNuevo == true) {
                    vm.nuevoBeneficio.esNuevo = true;
                } else if (vm.nuevoBeneficio.esNuevo == false) {
                    vm.nuevoBeneficio.esNuevo = false;
                }
                vm.nuevoBeneficio.tipoId = vm.categoriaSeleccionada;

                afiliacionesService.saveBeneficio(vm.nuevoBeneficio).success(function () {
                    vm.mostrarNuevoBeneficio = !vm.mostrarNuevoBeneficio;

                    if (vm.nuevoBeneficio.esNuevo == true) {
                        abp.notify.success(abp.localization.localize('afiliaciones_beneficio_notificacionInsertadoBeneficio', 'Bow') + ' ' + vm.nuevoBeneficio.nombre,
                        abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                    } else if (vm.nuevoBeneficio.esNuevo == false) {
                        abp.notify.success(abp.localization.localize('afiliaciones_beneficio_notificacionActualizadoBeneficio', 'Bow') + ' ' + vm.nuevoBeneficio.nombre,
                        abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                    }
                    vm.cargarBeneficios(vm.categoriaSeleccionada);
                })
                  .error(function (error) {
                      abp.notify.error(error.message, abp.localization.localize('afiliaciones_beneficio_error', 'Bow'));
                  });
            };

            vm.editarBeneficio = function (beneficioId) {
                vm.nuevoBeneficio.esNuevo = false;
                vm.mostrarNuevoBeneficio = true;

                afiliacionesService.getBeneficio({ id: beneficioId }).success(function (data) {
                    vm.nuevoBeneficio.id = data.id;
                    vm.nuevoBeneficio.nombre = data.nombre;
                    vm.nuevoBeneficio.descripcion = data.descripcion;
                })
            }

            //Funcion para cargar el formulario del nuevo beneficio
            vm.formularioNuevoBeneficio = function () {
                $scope.formNuevoBeneficio.$setPristine();

                vm.nuevoBeneficio.nombre = "";
                vm.nuevoBeneficio.descripcion = "";
                vm.nuevoBeneficio.esNuevo = true;
                vm.mostrarNuevoBeneficio = !vm.mostrarNuevoBeneficio;
            };

        }]);
})();