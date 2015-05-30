(function () {
    var controllerId = 'app.views.seguridad.prueba';

    angular.module('app').controller(controllerId, ['$scope', 'abp.services.app.seguridad',
        function ($scope, seguridadService) {

            var vm = this;

            vm.insertTenant = function () {
                seguridadService.crearTenant().success(function () {
                    abp.notify.success("Creo Tenant: ");
                }).error(function (error) {
                    console.log(error);
                });
            }

        }]);
})();