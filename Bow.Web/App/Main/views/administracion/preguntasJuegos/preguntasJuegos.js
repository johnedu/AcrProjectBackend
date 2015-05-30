(function () {
    //Nombre del controlador   
    var controllerId = 'app.views.administracion.preguntasJuegos';

    /*****************************************************************
    * 
    * CONTROLADOR DE PREGUNTAS DE JUEGOS
    * 
    *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.administracion',
       function ($scope, $modal, administracionService) {
           var vm = this;

           //Inicializando Modelos

           vm.listaJuegos = [];
           vm.listaDimensiones = [];
           vm.listaPreguntas = [];

           vm.selectedJuego = '';
           vm.selectedDimension = '';

           vm.pregunta = {
               id: '',
               pregunta: '',
               respuesta: ''
           };

           //Funcion encargada de consultar las preguntas de un juego y una dimensión seleccionada
           vm.cargarPreguntas = function () {
               administracionService.getAllPreguntasByDimension({ juegoId: vm.selectedJuego, dimensionId: vm.selectedDimension }).success(function (data) {
                   vm.listaPreguntas = data.preguntas;
               });
           }

           function cargarJuegos() {
               administracionService.getAllJuegos().success(function (data) {
                   vm.listaJuegos = data.juegos;
               });
           }
           cargarJuegos();

           function cargarDimensiones() {
               administracionService.getAllDimensiones().success(function (data) {
                   vm.listaDimensiones = data.dimensiones;
               });
           }
           cargarDimensiones();

           /************************************************************************
            * Llamado para abrir Modal para Nueva Pregunta Frecuente
            ************************************************************************/

           vm.abrirModalNueva= function () {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/preguntasJuegos/partials/modalNuevoPreguntasJuego.cshtml',
                   controller: 'modalNuevoPreguntasJuegoController',
                   size: 'md'
               });

               modalInstance.result.then(function () {
                   vm.cargarPreguntas();
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se guardó correctamente la pregunta', abp.localization.localize('', 'Bow') + 'Información');
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al guardar la pregunta'
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Editar Pregunta Frecuente
           ************************************************************************/
           vm.abrirModalEditar = function (preguntaId, preguntaTexto) {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/preguntasJuegos/partials/modalEditarPreguntasJuego.cshtml',
                   controller: 'modalEditarPreguntasJuegoController',
                   size: 'md',
                   resolve: {
                       preguntaEditar: function () {
                           return preguntaId;
                       },
                       preguntaTexto: function () {
                           return preguntaTexto;
                       }
                   }
               });

               modalInstance.result.then(function () {
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se actualizó correctamente la pregunta', abp.localization.localize('', 'Bow') + 'Información');
                   vm.cargarPreguntas();

               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la pregunta';
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Eliminar Pregunta Frecuente
           ************************************************************************/
           vm.abrirModalEliminar = function (preguntaId) {
               administracionService.puedeEliminarPreguntaOutput({ id: preguntaId })
                   .success(function (data) {
                       if (data.puedeEliminar) {
                           var modalInstance = $modal.open({
                               templateUrl: '/App/Main/views/administracion/preguntasJuegos/partials/modalEliminarPreguntasJuego.cshtml',
                               controller: 'modalEliminarPreguntasJuegoController',
                               size: 'md',
                               resolve: {
                                   preguntaEliminar: function () {
                                       return preguntaId;
                                   }
                               }
                           });

                           modalInstance.result.then(function () {
                               vm.cargarPreguntas();
                               abp.notify.success(abp.localization.localize('', 'Bow') + 'Se eliminó correctamente la pregunta', abp.localization.localize('', 'Bow') + 'Información');
                           }, function () {
                               vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la pregunta';
                           });
                       }
                       else {
                           abp.notify.error(abp.localization.localize('', 'Bow') + 'No se puede eliminar la pregunta, porque ya tiene puntajes asociados.', abp.localization.localize('' + 'Información', 'Bow'));
                       }
                   }).error(function (error) {
                       $scope.mensajeError = error.message;
                   });
           }

           /************************************************************************
           * Llamado para abrir Modal para Eliminar Pregunta Frecuente
           ************************************************************************/
           vm.cambiarEstado = function (pregunta) {
               if (pregunta.estadoActiva) {
                   pregunta.estadoActiva = false;
               }
               else {
                   pregunta.estadoActiva = true;
               }
               
               administracionService.updatePregunta(pregunta)
                   .success(function () {
                       abp.notify.success(abp.localization.localize('', 'Bow') + 'Se actualizó correctamente el estado de la pregunta', abp.localization.localize('', 'Bow') + 'Información');
                   }).error(function (error) {
                       $scope.mensajeError = error.message;
                   });
           };

           /************************************************************************
            * Llamado para abrir Modal para Gestionar Opciones
            ************************************************************************/
           vm.abrirModalPreguntas = function (preguntaId, preguntaTexto, juego) {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/preguntasJuegos/partials/modalGestionarRespuestas.cshtml',
                   controller: 'modalGestionarRespuestasController',
                   keyboard: false,
                   backdrop: 'static',
                   size: 'lg',
                   resolve: {
                       preguntaId: function () {
                           return preguntaId;
                       },
                       preguntaTexto: function () {
                           return preguntaTexto;
                       },
                       juego: function () {
                           return juego;
                       }
                   }
               });

               modalInstance.result.then(function () {

               }, function () {
                   console.log("Ocurrió un problema al cargar el modal de administración de respuestas");
               });
           }

       }]);
})();