(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.zonificacion.zonificacion';

    //Creando el controlador
    angular.module('app').controller(controllerId, ['$scope', '$location', '$stateParams', 'abp.services.app.zonificacion', function ($scope, $location, $stateParams, zonificacionService) {
        var vm = this;
        vm.pais = "";
        vm.departamentos = [];
        vm.inputDepto = {
            nombre: '',
            indicativo: '',
            paisId: ''
        };

        //Función encargada de consultar los departamentos en la base de datos
        cargarDepartamentos = function () {
            zonificacionService.getDepartamentos().success(function (data) {
                vm.departamentos = data.departamentos;
            });
        }

        cargarDepartamentos();

        zonificacionService.getPais({ id: $stateParams.paisId }).success(function (data) {
            vm.pais = data;
        });

        //Llamado al servicio que permite registrar un nuevo departamento en el sistema
        vm.guardarDepartamento = function () {
            vm.inputDepto.paisId = vm.pais.id;
            abp.ui.setBusy(null, {
                promise: zonificacionService.saveDepartamento(vm.inputDepto)
                .success(function () {
                    abp.notify.info("Se ha creado el Departamento " + vm.inputDepto.nombre);
                    $location.path('/zonificacion/pais/' + vm.pais.id);
                })
            })
        }

       

        /************************************************************************
        * Llamado para abrir Modal para Nuevo Pais
        ************************************************************************/
        vm.abrirModalNuevoDepto = function () {
            var modalInstance = $modal.open({
                templateUrl: '/App/Main/views/zonificacion/pais/partials/modalNuevoDepartamento.cshtml',
                controller: 'modalNuevoDepartamentoController',
                size: 'md'
            });

            modalInstance.result.then(function () {
                cargarDepartamentos();
                $location.path('/administracion/zonificacion');
                abp.notify.success("Se ha Registrado el Departamento ", "Información");
            }, function () { vm.resultado = "No devolvio" + paisId });
        }
    }]);
})();