(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.personas.tiposDocumento';

    /*****************************************************************
     * 
     * CONTROLADOR TIPOS DE DOCUMENTO
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.personas', 
        function ($scope, $modal, personasService) {
            var vm = this;

            ////Inicializando modelos
            vm.editandoDocumento = false;
            vm.eliminadoDocumento = false;
            vm.tiposDocumento = [];

            vm.tipoDocumento = {
                id: '',
                nombre: '',
                nombreDocumentoEditar: '',
                longitudMinima: '',
                longitudMaxima: '',
                conjuntoCaracteres: 'A',
                edadMinima: null,
                edadMaxima: null,
                default: '',
                aplicaEmpresa: '',
                aplicaPersona: '',
                paisId: '',
                paisSelected: ''
            };

            /************************************************************************
             * Controles para editar o eliminar los tipos de documentos
             ************************************************************************/
            vm.controles = {
                visible: [],
                mostrar: function ($index) {
                    vm.controles.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controles.visible[$index] = false;
                    vm.eliminadoDocumento = false;
                }
            };

            /********************************************************************
             * Funciones para cargar todos los tipos de documento
             ********************************************************************/
            function cargarTiposDocumento() {
                personasService.getAllTiposDocumentoWithPais().success(function (datos) {
                    vm.tiposDocumento = datos.tiposDocumento;
                });
            }
            cargarTiposDocumento();

            /************************************************************************
             * Llamado para editar un Tipo de Documento
             ************************************************************************/
            vm.editarTipoDocumento = function (tipoDocumentoId) {
                personasService.getTipoDocumento({ id: tipoDocumentoId }).success(function (data) {
                    if (data.default == "1")
                        data.default = true;
                    else
                        data.default = false;
                    if (data.aplicaEmpresa == "1")
                        data.aplicaEmpresa = true;
                    else
                        data.aplicaEmpresa = false;
                    if (data.aplicaPersona == "1")
                        data.aplicaPersona = true;
                    else
                        data.aplicaPersona = false;
                    vm.tipoDocumento = data;
                    vm.tipoDocumento.nombreDocumentoEditar = data.nombre;
                    vm.tipoDocumento.paisSelected = {
                        nombre: data.paisNombre,
                        indicativo: data.paisIndicativo,
                        id: data.paisId,
                    }
                    vm.editandoDocumento = true;
                });
            }

            /********************************************************************
             * Funciones para la directiva de eliminar plan exequial
             ********************************************************************/
            vm.puedeEliminarTipoDocumento = function (tipoDocumentoId, funcionRetornarPuedeEliminar) {
                personasService.puedeEliminarTipoDocumento({ Id: tipoDocumentoId }).success(function (data) {
                    vm.eliminadoDocumento = data.puedeEliminar;
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('personas_tiposDocumento_notificacionEliminado_PersonasAsociadas', 'Bow'), abp.localization.localize('personas_tiposDocumento_noPuedeEliminar', 'Bow'));
            }

            vm.eliminarTipoDocumento = function (tipoDocumentoId) {
                personasService.deleteTipoDocumento({ id: tipoDocumentoId }).success(function () {
                    abp.notify.success(abp.localization.localize('personas_tiposDocumento_notificacionEliminado', 'Bow'), abp.localization.localize('personas_tiposDocumento_informacion', 'Bow'));
                    vm.eliminadoDocumento = false;
                    cargarTiposDocumento();
                }).error(function (error) {
                    if (error.validationErrors != null) {
                        abp.notify.error(error.validationErrors[0].message, abp.localization.localize('personas_tiposDocumento_error', 'Bow'));
                    }
                    else {
                        abp.notify.error(error.message, abp.localization.localize('personas_tiposDocumento_error', 'Bow'));
                    }
                });
            }

            vm.cancelarEliminar = function (tipoDocumentoId) {
                vm.eliminadoDocumento = false;
            };

            /************************************************************************
             * Función para cancelar la actualización de un tipo de documento
             ************************************************************************/
            vm.cancelarUpdateDocumento = function () {
                limpiarFormulario();
                vm.editandoDocumento = false;
                $scope.frmTipoDocumento.$setPristine();
            }

            /************************************************************************
             * Función para limpiar los datos del formulario de ingreso de un tipo de documento
             ************************************************************************/
            function limpiarFormulario () {
                vm.tipoDocumento = {
                    id: '',
                    nombre: '',
                    nombreDocumentoEditar: '',
                    longitudMinima: '',
                    longitudMaxima: '',
                    conjuntoCaracteres: 'A',
                    edadMinima: null,
                    edadMaxima: null,
                    default: '',
                    aplicaEmpresa: '',
                    aplicaPersona: '',
                    paisId: '',
                    paisSelected: ''
                };
            }

            /************************************************************************
             * Función para guardar o actualizar un tipo de documento según la bandera
             ************************************************************************/
            vm.saveOrUpdateTipoDocumento = function () {
                var validacionesCorrectas = true;
                if (parseInt(vm.tipoDocumento.longitudMinima) > parseInt(vm.tipoDocumento.longitudMaxima)) {
                    validacionesCorrectas = false;
                    abp.notify.error(abp.localization.localize('personas_tiposDocumento_notificacionLongitud', 'Bow'), abp.localization.localize('personas_tiposDocumento_error', 'Bow'));
                }
                if (vm.tipoDocumento.edadMinima != "" && vm.tipoDocumento.edadMaxima != "")
                    if (parseInt(vm.tipoDocumento.longitudMinima) > parseInt(vm.tipoDocumento.longitudMaxima)) {
                        validacionesCorrectas = false;
                        abp.notify.error(abp.localization.localize('personas_tiposDocumento_notificacionEdad', 'Bow'), abp.localization.localize('personas_tiposDocumento_error', 'Bow'));
                    }
                if (validacionesCorrectas) {
                    vm.tipoDocumento.paisId = vm.tipoDocumento.paisSelected.id;
                    if (vm.editandoDocumento) {
                        updateTipoDocumento();
                    }
                    else {
                        saveTipoDocumento();
                    }
                }
            }
            
            /************************************************************************
             * Función para guardar un tipo de documento
             ************************************************************************/
            function saveTipoDocumento() {
                personasService.saveTipoDocumento(vm.tipoDocumento)
                    .success(function () {
                        abp.notify.success(abp.localization.localize('personas_tiposDocumento_notificacionModificado', 'Bow') + ' ' + vm.tipoDocumento.nombre, abp.localization.localize('personas_tiposDocumento_informacion', 'Bow'));
                        vm.cancelarUpdateDocumento();
                        cargarTiposDocumento();
                    }).error(function (error) {
                        abp.notify.error(error.message, abp.localization.localize('personas_tiposDocumento_error', 'Bow'));
                    });
            };

            /************************************************************************
             * Función para actualizar un tipo de documento
             ************************************************************************/
            function updateTipoDocumento() {
                personasService.updateTipoDocumento(vm.tipoDocumento)
                    .success(function () {
                        abp.notify.success(abp.localization.localize('personas_tiposDocumento_notificacionInsertado', 'Bow') + ' ' + vm.tipoDocumento.nombre, abp.localization.localize('personas_tiposDocumento_informacion', 'Bow'));
                        vm.cancelarUpdateDocumento();
                        cargarTiposDocumento();
                    }).error(function (error) {
                        abp.notify.error(error.message, abp.localization.localize('personas_tiposDocumento_error', 'Bow'));
                    });
            };

        }]);
})();