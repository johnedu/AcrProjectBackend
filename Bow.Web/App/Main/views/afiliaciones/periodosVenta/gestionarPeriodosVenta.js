(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.afiliaciones.periodosVenta';

    /*****************************************************************
     * 
     * CONTROLADOR GESTIONAR PERIODOS VENTA
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.afiliaciones',
        function ($scope, $modal, afiliacionesService) {
            var vm = this;

            vm.periodoVerificado = {};

            vm.nuevoPeriodo = {
                prefijo: '',
                periodoInicial: '',
                intervalo: '',
                tiempo: 'Días',
                fechaFin: ''
            };

            /********************************************************************
            * Funcion para abrir el formulario para registrar nuevo periodo
            ********************************************************************/
            vm.formularioNuevoPeriodo = function () {
                vm.mostrarRegistrarPeriodo = !vm.mostrarRegistrarPeriodo;
                limpiarFormulario();
            };

            /********************************************************************
            * Función encargada de guardar los periodos de ventas verificados
            ********************************************************************/
            vm.savePeriodosVentas = function () {
                if (JSON.stringify(vm.periodoVerificado) === JSON.stringify(vm.nuevoPeriodo)) {
                    afiliacionesService.savePeriodosVentas(vm.nuevoPeriodo).success(function () {
                        cargarPeriodosRegistrados();
                        vm.mostrarRegistrarPeriodo = !vm.mostrarRegistrarPeriodo
                        abp.notify.success(abp.localization.localize('afiliaciones_periodosventas_periodoVentaGuardado', 'Bow'),
                        abp.localization.localize('afiliaciones_periodosventas_informacion', 'Bow'));
                        limpiarFormulario();
                    });
                } else {
                    abp.notify.error(abp.localization.localize('afiliaciones_periodosventas_periodoVerificadoCambio', 'Bow'),
                    abp.localization.localize('afiliaciones_periodosventas_informacion', 'Bow'));
                }
            }

            /********************************************************************
            * Función encargada de verificar los periodos indicados en el formulario
            ********************************************************************/
            vm.verificarPeriodos = function () {
                vm.periodoVerificado = JSON.parse(JSON.stringify(vm.nuevoPeriodo));

                afiliacionesService.getVerificarPeriodosVentas(vm.nuevoPeriodo).success(function (data) {
                    vm.periodosCalculados = bow.tablas.paginar(data.periodosCalculados, 10);

                }).error(function (error) {
                    abp.notify.error(error.message);
                });
            }

            /********************************************************************
            * Función encargada de consultar los periodos registrados en la base de datos
            ********************************************************************/
            function cargarPeriodosRegistrados() {
                afiliacionesService.getPeriodosVentasRegistrados().success(function (data) {

                    vm.periodosRegistrados = bow.tablas.paginar(data.periodosRegistrados, 10);

                    var fechaInicioMaxima = new Date(data.fechaInicioMaxima);
                    var fechaInicioMinima = new Date(data.fechaInicioMinima);

                    vm.configuracionFechaInicio = bow.fechas.configurarDatePicker(fechaInicioMinima, fechaInicioMaxima);

                    //Cargo la fecha de Inicio mas un día donde finalizo el periodo anterior
                    if (data.fechaInicioProxima != null) {
                        vm.nuevoPeriodo.fechaInicio = data.fechaInicioProxima.substring(0, 10);
                    }

                    //Se cargar el rango del datapicker de la fecha fin segun la fecha seleccionada del datpicker fecha inicio
                    vm.cargarFechaFin(fechaInicioMinima);
                });
            }

            cargarPeriodosRegistrados();

            /********************************************************************
            * Función encargada de eliminar los periodos de venta seleccionados
            ********************************************************************/
            vm.eliminarSeleccionados = function () {

                var periodosEliminados = [];

                if (vm.periodosRegistrados != null) {
                    for (var i = 0; i < vm.periodosRegistrados.filasMostrar.length; i++) {

                        if (vm.periodosRegistrados.filasMostrar[i].marcar === true) {
                            periodosEliminados.push(vm.periodosRegistrados.filasMostrar[i]);
                        }
                    }
                    if (periodosEliminados != "") {
                        afiliacionesService.deletePeriodosVentas({ periodosEliminar: periodosEliminados })
                            .success(function (data) {
                                if (data == false) {
                                    abp.notify.error(abp.localization.localize('afiliaciones_periodosventas_periodosVentasNoEliminar', 'Bow'),
                                    abp.localization.localize('afiliaciones_periodosventas_informacion', 'Bow'));
                                } else {
                                    abp.notify.success(abp.localization.localize('afiliaciones_periodosventas_periodosVentasEliminados', 'Bow'),
                                    abp.localization.localize('afiliaciones_periodosventas_informacion', 'Bow'));
                                    cargarPeriodosRegistrados();
                                }
                            }).error(function (error) {
                                abp.notify.error(error.message);
                            });
                    }
                }
            }

            vm.marcarTodo = function (marcartodo) {

                if (vm.periodosRegistrados != null) {
                    for (var i = 0; i < vm.periodosRegistrados.filasMostrar.length; i++) {
                        if (marcartodo === true) {
                            vm.periodosRegistrados.filasMostrar[i].marcar = true;
                        } else {
                            vm.periodosRegistrados.filasMostrar[i].marcar = false;
                        }
                    }
                }
            }

            vm.cargarFechaFin = function (fechaMinima) {
                if (fechaMinima != "") {
                    if (fechaMinima != undefined) {
                        afiliacionesService.fechaFinPeriodoVenta({ fechaInicioMinima: fechaMinima }).success(function (data) {
                            var fechaFinMaxima = new Date(data.fechaFinMaxima);
                            var fechaFinMinima = new Date(data.fechaFinMinima);
                            vm.configuracionFechaFin = bow.fechas.configurarDatePicker(fechaFinMinima, fechaFinMaxima);
                        }).error(function (error) {
                            abp.notify.error(error.message);
                        });
                    }
                }
            }


            function limpiarFormulario() {
                vm.periodosCalculados = "";

                vm.nuevoPeriodo = {
                    prefijo: '',
                    periodoInicial: '',
                    fechaInicio: vm.nuevoPeriodo.fechaInicio,
                    intervalo: '',
                    tiempo: 'Días',
                    fechaFin: ''
                };

                $scope.formNuevoPeriodo.$setPristine();
            };


        }]);
})();

