(function () {
    //Nombre del controlador   
    var controllerId = 'app.views.administracion.entidadesDimensiones';

    /*****************************************************************
    * 
    * CONTROLADOR DE ENTIDADES DE LAS DIMENSIONES
    * 
    *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.administracion',
       function ($scope, $modal, administracionService) {
           var vm = this;

           //Inicializando Modelos

           vm.listaDimensiones = [];
           vm.listaEntidades = [];

           vm.selectedDimension = '';

           vm.entidad = {
               id: '',
               nombre: '',
               descripcion: ''
           };

           //   Función encargada de consultar las entidades de una dimensión seleccionada
           vm.cargarEntidades = function () {
               administracionService.getAllEntidadesByDimension({ dimensionId: vm.selectedDimension }).success(function (data) {
                   vm.listaEntidades = data.entidades;
               });
           }

           function cargarDimensiones() {
               administracionService.getAllDimensiones().success(function (data) {
                   vm.listaDimensiones = data.dimensiones;
               });
           }
           cargarDimensiones();

           /************************************************************************
            * Llamado para abrir Modal para Nueva Entidad
            ************************************************************************/
           vm.abrirModalNueva= function () {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/entidadesDimensiones/partials/modalNuevoEntidadesDimensiones.cshtml',
                   controller: 'modalNuevoEntidadesDimensionesController',
                   size: 'md'
               });

               modalInstance.result.then(function () {
                   vm.cargarEntidades();
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se guardó correctamente la entidad', abp.localization.localize('', 'Bow') + 'Información');
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al guardar la entidad'
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Editar una Entidad
           ************************************************************************/
           vm.abrirModalEditar = function (entidadId, entidadNombre) {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/entidadesDimensiones/partials/modalEditarEntidadesDimensiones.cshtml',
                   controller: 'modalEditarEntidadesDimensionesController',
                   size: 'md',
                   resolve: {
                       entidadEditar: function () {
                           return entidadId;
                       },
                       entidadNombre: function () {
                           return entidadNombre;
                       }
                   }
               });

               modalInstance.result.then(function () {
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se actualizó correctamente la entidad', abp.localization.localize('', 'Bow') + 'Información');
                   vm.cargarEntidades();
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la entidad';
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Eliminar una Entidad
           ************************************************************************/
           vm.abrirModalEliminar = function (entidadId) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/administracion/entidadesDimensiones/partials/modalEliminarEntidadesDimensiones.cshtml',
                    controller: 'modalEliminarEntidadesDimensionesController',
                    size: 'md',
                    resolve: {
                        entidadEliminar: function () {
                            return entidadId;
                        }
                    }
                });

                modalInstance.result.then(function () {
                    vm.cargarEntidades();
                    abp.notify.success(abp.localization.localize('', 'Bow') + 'Se eliminó correctamente la pregunta', abp.localization.localize('', 'Bow') + 'Información');
                }, function () {
                    vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la pregunta';
                });
           }

           /************************************************************************
           * Llamado para abrir Modal para cambiar el estado de una Entidad
           ************************************************************************/
           vm.cambiarEstado = function (entidad) {
               if (entidad.estadoActiva) {
                   entidad.estadoActiva = false;
               }
               else {
                   entidad.estadoActiva = true;
               }
               
               administracionService.updateEntidad(entidad)
                   .success(function () {
                       abp.notify.success(abp.localization.localize('', 'Bow') + 'Se actualizó correctamente el estado de la pregunta', abp.localization.localize('', 'Bow') + 'Información');
                   }).error(function (error) {
                       $scope.mensajeError = error.message;
                   });
           };

       }]);
})();