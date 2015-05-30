(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.empleados.empleado';

    /*****************************************************************
     * 
     * CONTROLADOR EMPLEADOS
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.empleados', 'abp.services.app.personas', 'abp.services.app.parametros',
        function ($scope, $modal, empleadosService, personasService, parametrosService) {
            var vm = this;

            vm.nuevoEmpleado = {
                codigo: '',
                personaId: '',
                sucursalId: '',
                estadoId: ''
            };

            vm.persona = [];

            vm.cargarEstados = function () {
                parametrosService.getEstadosEmpleado()
                   .success(function (data) {
                       vm.estados = data.estados;
                       vm.nuevoEmpleado.estadoId = data.estados[0].id;
                   });
            };

            vm.cargarEstados();

            vm.saveEmpleado = function () {

                if (vm.persona != null) {
                    vm.nuevoEmpleado.personaId = vm.persona.id;
                }

                empleadosService.saveEmpleado(vm.nuevoEmpleado)
                  .success(function () {
                      abp.notify.success(abp.localization.localize('empleados_nuevoempleado_notificacionGuardadoEmpleado' + ' ' + vm.persona.nombreCompleto, 'Bow'), abp.localization.localize('empleados_nuevoempleado_informacion', 'Bow'));
                      limpiarformulario();
                  })
                  .error(function (error) {
                      abp.notify.error(error.message);
                  });
            };

            vm.consultarPersona = function () {
                personasService.getPersona({ numeroDocumento: vm.cedula })
                  .success(function (data) {
                      vm.persona = data;
                  })
                  .error(function (error) {
                      abp.notify.error(error.message);
                  });
            };

            vm.cancelarEmpleado = function () {
                limpiarformulario();
            };

            var limpiarformulario = function () {
                vm.nuevoEmpleado = {
                    codigo: '',
                    personaId: '',
                    sucursalId: '',
                    estadoId: vm.estados[0].id
                };

                vm.persona = [];
                vm.cedula = "";
            }


        }]);
})();



//(function () {
//    //Nombre del Controlador
//    var controllerId = 'app.views.empleados.empleado';

//    /*****************************************************************
//     * 
//     * CONTROLADOR EMPLEADOS
//     * 
//     *****************************************************************/
//    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.empleados', 'abp.services.app.personas', 'abp.services.app.parametros',
//        function ($scope, $modal, empleadosService, personasService, parametrosService) {
//            var vm = this;

//            vm.nuevoEmpleado = {
//                codigo: '',
//                personaId: '',
//                sucursalId: '',
//                estadoId: ''
//            };

//            vm.cargarEstados = function () {
//                parametrosService.getEstadosEmpleado()
//                   .success(function (data) {
//                       vm.estados = data.estados;
//                       vm.nuevoEmpleado.estadoId = data.estados[0].id;
//                   });
//            };

//            vm.cargarEstados();

//            vm.saveEmpleado = function () {
//                if (vm.persona != null) {
//                    vm.nuevoEmpleado.personaId = vm.persona.id;
//                }
//                empleadosService.saveEmpleado(vm.nuevoEmpleado)
//                  .success(function () {
//                      abp.notify.success(abp.localization.localize('empleados_nuevoempleado_notificacionGuardadoEmpleado' + ' ' + vm.persona.nombreCompleto, 'Bow'), abp.localization.localize('empleados_nuevoempleado_informacion', 'Bow'));

//                      limpiarformulario();
//                  })
//                  .error(function (error) {
//                      $scope.mensajeError = error.message;
//                  });
//            };

//            vm.consultarPersona = function () {
//                personasService.getPersona({ numeroDocumento: vm.cedula })
//                  .success(function (data) {
//                      vm.persona = data;
//                      vm.resultadoPersona = true;
//                  });
//            };

//            vm.cancelEmpleado = function () {
//                limpiarformulario();
//            };

//            var limpiarformulario = function () {
//                $scope.mostrarEmpleado = !$scope.mostrarEmpleado;
//                vm.nuevoEmpleado.codigo = "";
//                vm.nuevoEmpleado.personaId = "";
//                vm.persona = "";
//                vm.cedula = "";
//                $scope.mensajeError = "";
//                vm.nuevoEmpleado.estadoId = vm.estados[0].id;
//            }


//        }]);
//})();

