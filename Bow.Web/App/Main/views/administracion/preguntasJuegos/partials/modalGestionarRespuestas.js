(function () {
    angular.module('app').controller('modalGestionarRespuestasController', ['$scope', '$modalInstance', 'preguntaId', 'preguntaTexto', 'juego', 'abp.services.app.administracion',
        function ($scope, $modalInstance, preguntaId, preguntaTexto, juego, administracionService) {

            ////Inicializando modelos

            $scope.mensajeEliminar = [];
            $scope.respuestasPregunta = [];
            $scope.preguntaTexto = preguntaTexto;
            $scope.mostrarFormulario = false;
            $scope.accionFormulario = "Agregar";
            $scope.juego = juego;
            $scope.btn_nuevo_disabled = false;

            $scope.respuesta = {
                texto: '',
                comodin50_50: false,
                respuestaVerdadera: false,
                preguntaId: preguntaId
            };

            /********************************************************************
             * Funcion para cargar las opciones de la información tributaria seleccionada
             ********************************************************************/
            function cargarRespuestas() {
                administracionService.getAllRespuestasByPregunta({ preguntaId: preguntaId })
                    .success(function (data) {
                        $scope.respuestasPregunta = bow.tablas.paginar(data.respuestas, 5);
                        $scope.btn_nuevo_disabled = false;
                        if ($scope.juego.toLowerCase() == 'falso o verdadero') {
                            if (data.respuestas.length > 1) {
                                $scope.btn_nuevo_disabled = true;
                            }
                        } else if ($scope.juego.toLowerCase() == 'millonario') {
                            if (data.respuestas.length > 3) {
                                $scope.btn_nuevo_disabled = true;
                            }
                        } else if ($scope.juego.toLowerCase() == 'pasapalabra') {
                            if (data.respuestas.length > 0) {
                                $scope.btn_nuevo_disabled = true;
                            }
                        }
                    });
            }
            cargarRespuestas();

            /********************************************************************
             * Funcion para crear una nueva opción de información tributaria
             ********************************************************************/
            function crearRespuesta() {
                administracionService.saveRespuesta($scope.respuesta)
                    .success(function () {
                        abp.notify.info(abp.localization.localize('', 'Bow') + 'La respuesta se ha creado correctamente.', abp.localization.localize('' + 'Información', 'Bow'));
                        cargarRespuestas();
                        $scope.mostrarFormulario = false;
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            /********************************************************************
             * Funcion para editar una opción de información tributaria
             ********************************************************************/
            function editarRespuesta() {
                administracionService.updateRespuesta($scope.respuesta)
                    .success(function () {
                        abp.notify.info(abp.localization.localize('', 'Bow') + 'La respuesta se ha modificado correctamente.', abp.localization.localize('' + 'Información', 'Bow'));
                        cargarRespuestas();
                        $scope.mostrarFormulario = false;
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            /********************************************************************
             * Funciones para eliminar una opción de Información Tributaria
             ********************************************************************/
            $scope.eliminarRespuesta = function ($index) {
                $scope.mensajeEliminar[$index] = true;
            };

            $scope.eliminarRespuestaOk = function (respuestaId, $index) {
                administracionService.deleteRespuesta({ id: respuestaId })
                   .success(function (data) {
                       $scope.mensajeEliminar[$index] = false;
                       abp.notify.info(abp.localization.localize('', 'Bow') + 'La respuesta se ha eliminado correctamente.', abp.localization.localize('' + 'Información', 'Bow'));
                       cargarRespuestas();
                   }).error(function (error) {
                       $scope.mensajeError = error.message;
                   });
            }

            $scope.eliminarCancel = function ($index) {
                $scope.mensajeEliminar[$index] = false;
            }

            $scope.mostrarFormularioNuevaRespuesta = function () {
                $scope.respuesta = {
                    texto: '',
                    comodin50_50: false,
                    respuestaVerdadera: false,
                    preguntaId: preguntaId
                };
                $scope.mensajeError = null;
                $scope.mostrarFormulario = true;
                $scope.accionFormulario = "Agregar";
                //  Limpiamos el formulario para que no se muestren las clases 'has-error'
                $scope.formNuevaRespuesta.$setPristine();
            }

            $scope.mostrarFormularioEditarRespuesta = function (respuestaId) {
                $scope.mensajeError = null;
                administracionService.getRespuesta({ id: respuestaId }).success(function (data) {
                    $scope.respuesta = data;
                });
                $scope.mostrarFormulario = true;
                $scope.accionFormulario = "Editar";
            }

            $scope.cancelFormulario = function () {
                $scope.mostrarFormulario = false;
            }

            $scope.guardarModificarRespuesta = function () {
                $scope.mensajeError = null;
                if ($scope.accionFormulario == "Agregar") {
                    crearRespuesta();
                }
                else {
                    editarRespuesta();
                }
            }

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }

        }]);
})();