(function () {
    //Nombre del controlador   
    var controllerId = 'app.views.zonificacion.departamento';

    /*****************************************************************
    * 
    * CONTROLADOR DEPARTAMENTO
    * 
    *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$stateParams', 'abp.services.app.zonificacion',
       function ($scope, $stateParams, zonificacionService) {

           //Inicializando Modelos
           var vm = this;
           vm.paisId = $stateParams.paisId;
           vm.editandoDepartamento = undefined;
           vm.nombreDepartamentoEditar = undefined;
           vm.eliminandoDepartamento = undefined;
           vm.departamentos = [];
           vm.nuevoDepartamento = {
               nombre: '',
               indicativo: '',
               paisId: vm.paisId
           };

           function cargarPais() {
               zonificacionService.getPais({ id: $stateParams.paisId }).success(function (data) {
                   vm.inputPais = data;
               });
           }
           cargarPais();

           //Funcion encargada de consultar los departamentos en la base de datos
           function cargarDepartamentos() {
               zonificacionService.getDepartamentos({ id: $stateParams.paisId }).success(function (data) {
                   vm.departamentos = bow.tablas.paginar(data.departamentos, 5);
               });
           }
           cargarDepartamentos();

           function limpiarFormulario() {
               vm.nuevoDepartamento = {
                   nombre: '',
                   indicativo: '',
                   paisId: vm.paisId
               };
               $scope.frmDepartamento.$setPristine();
           }

           /*******************************************************
            * REGISTRAR DEPARTAMENTO
            ******************************************************/

           //Registrar Departamento
           function saveDepartamento() {
               console.log(vm.nuevoDepartamento);
               zonificacionService.saveDepartamento(vm.nuevoDepartamento)
               .success(function () {
                   cargarDepartamentos();
                   abp.notify.success(abp.localization.localize('zonificacion_departamento_notificacionInsertadoDpto', 'Bow') + ' ' + vm.nuevoDepartamento.nombre, abp.localization.localize('zonificacion_departamento_notificacionInsertadoInformacion', 'Bow'));
                   limpiarFormulario();
               })
               .error(function (error) {
                   abp.notify.error(error.message);
               });
           };


           //Controles para editar o eliminar un departamento
           vm.controles = {
               visible: [],
               mostrar: function ($index) {
                   vm.controles.visible[$index] = true;
                   vm.eliminandoDepartamento = false;
               },
               ocultar: function ($index) {
                   vm.controles.visible[$index] = false;
                   vm.eliminandoDepartamento = false;
               }
           };

           /**************************************************
            * ACTUALIZAR DEPARTAMENTO
            *************************************************/

           vm.saveOrUpdateDepartamento = function () {
               if (vm.editandoDepartamento) {
                   updateDepartamento();
               }
               else {
                   saveDepartamento();
               }
           }

           vm.editarDepartamento = function (departamento) {
               vm.editandoDepartamento = true;
               vm.nuevoDepartamento = {
                   id: departamento.id,
                   nombre: departamento.nombre,
                   indicativo: departamento.indicativo,
                   paisId: departamento.paisId
               };
               vm.nombreDepartamentoEditar = departamento.nombre;
           };

           vm.cancelarUpdateDepartamento = function () {
               limpiarFormulario();
               if (vm.editandoDepartamento) {
                   vm.editandoDepartamento = false;
               }
           }

           function updateDepartamento() {
               zonificacionService.updateDepartamento({ id: vm.nuevoDepartamento.id, nombre: vm.nuevoDepartamento.nombre, indicativo: vm.nuevoDepartamento.indicativo, paisid: vm.paisId })
                   .success(function () {
                       cargarDepartamentos();
                       vm.editandoDepartamento = false;
                       abp.notify.success(abp.localization.localize('zonificacion_departamento_notificacionActuailizadoDpto', 'Bow') + ' ' + vm.nuevoDepartamento.nombre,
                       abp.localization.localize('zonificacion_departamento_notificacionInsertadoInformacion', 'Bow'));
                       limpiarFormulario();

                   }).error(function (error) {
                       abp.notify.error(error.message);
                   });
           };


           /************************************************
            * ELIMINAR DEPARTAMENTO
            ************************************************/

           vm.puedeEliminarDepartamento = function (departamentoId, funcionRetornarPuedeEliminar) {
               zonificacionService.puedeEliminarDepartamento({ id: departamentoId }).success(function (data) {
                   if (data.puedeEliminar) {
                       vm.eliminandoDepartamento = true;
                   }
                   funcionRetornarPuedeEliminar(data.puedeEliminar);
               });
           }

           vm.noPuedeEliminar = function () {
               abp.notify.error(abp.localization.localize('zonificacion_departamento_NonotificacionEliminadoDepartamento', 'Bow'),
                       abp.localization.localize('zonificacion_departamento_informacion', 'Bow'));
           }

           vm.eliminarDepartamentoOk = function (departamento) {
               zonificacionService.deleteDepartamento({ id: departamento.id })
                  .success(function (data) {
                      abp.notify.info(abp.localization.localize('zonificacion_departamento_notificacionEliminadoDpto', 'Bow') + ' ' + departamento.nombre, abp.localization.localize('zonificacion_departamento_notificacionInsertadoInformacion', 'Bow'));
                      cargarDepartamentos();
                      vm.eliminandoDepartamento = false;
                  });
           }

           vm.eliminarDepartamentoCancel = function () {
               vm.eliminandoDepartamento = false;
           };
       }]);
})();