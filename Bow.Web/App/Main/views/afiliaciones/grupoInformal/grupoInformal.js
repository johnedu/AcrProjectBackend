(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.afiliaciones.grupoInformal';

    /*****************************************************************
     * 
     * CONTROLADOR GRUPO INFORMAL
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.afiliaciones', 'abp.services.app.personas',
        function ($scope, $modal, afiliacionesService, personasService) {
            var vm = this;

            ////Inicializando modelos

            $scope.controlBuscarGrupoInformal = {};

            vm.formularioGrupoInformalVisible = false;
            vm.accionFormulario = 'Guardar';
            vm.documentoValido = false;
            vm.modoConsulta = false;

            //  Objeto para agregar o actualizar la empresa
            vm.grupoInformal = {
                id: '',
                nombre: '',
                sucursal: '',
                encargado: {
                    personaId: '',
                    documento: '',
                    personaNombre: '',
                    telefonos: [],
                    direcciones: []
                },
                encargadoExentoPago: 'false',
                porcentajeDescuento: 0,
                empleadoId: '',
                listaEmpleados: [],
                listaEmpleadosTabla: []
            };

            /****************************************************************************
             * Controles para editar o eliminar un empleado encargado del grupo informal
             ****************************************************************************/
            vm.controlesEmpleado = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesEmpleado.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesEmpleado.visible[$index] = false;
                }
            };

            /************************************************************************
             * Llamado para validar el documento de identidad de la persona
             ************************************************************************/
            vm.validarDocumentoEncargado = function () {
                personasService.getPersona({ NumeroDocumento: vm.grupoInformal.encargado.documento })
                    .success(function (data) {
                        if (data != null) {
                            abp.notify.success(abp.localization.localize('afiliaciones_grupoInformal_encargado_persona_encontrada', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));

                            vm.grupoInformal.encargado.personaId = data.id;
                            vm.grupoInformal.encargado.documento = data.numeroDocumento;
                            vm.grupoInformal.encargado.personaNombre = data.nombreCompleto;

                            personasService.getTelefonosPersona({ PersonaId: vm.grupoInformal.encargado.personaId })
                                .success(function (data) {
                                    vm.grupoInformal.encargado.telefonos = data.telefonosPersona;
                                }).error(function (error) {
                                    $scope.mensajeError = error.message;
                                });

                            personasService.getDireccionesPersona({ PersonaId: vm.grupoInformal.encargado.personaId })
                                .success(function (data) {
                                    vm.grupoInformal.encargado.direcciones = data.direccionesPersona;
                                }).error(function (error) {
                                    $scope.mensajeError = error.message;
                                });

                            vm.documentoValido = true;
                            $scope.mensajeError = "";
                        }
                        else {
                            abp.notify.error(abp.localization.localize('afiliaciones_grupoInformal_encargado_persona_noEncontrada', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                        }
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            };

            /************************************************************************
             * Llamado para ocultar el formulario para editar información de la organización
             ************************************************************************/
            vm.cambiarDocumentoEncargado = function () {
                vm.documentoValido = false;
                vm.grupoInformal.encargado.personaNombre = '';
            };

            /************************************************************************
             * Llamado para limpiar el formulario del grupo informal
             ************************************************************************/
            vm.limpiarFormularioGrupo = function () {
                //  Limpiamos el objeto
                vm.grupoInformal = {
                    id: '',
                    nombre: '',
                    sucursal: '',
                    encargado: {
                        personaId: '',
                        documento: '',
                        personaNombre: '',
                        telefonos: [],
                        direcciones: []
                    },
                    encargadoExentoPago: 'false',
                    porcentajeDescuento: 0,
                    empleadoId: '',
                    listaEmpleados: [],
                    listaEmpleadosTabla: []
                };
                $scope.frmRegistrarGrupoInformal.$setPristine();
                vm.accionFormulario = 'Guardar';
                vm.modoConsulta = false;
                vm.documentoValido = false;
                vm.formularioGrupoInformalVisible = false;
            };

            /********************************************************************
             * Funciones para la directiva de eliminar plan exequial
             ********************************************************************/
            vm.seleccionoSucursal = function (sucursal) {
                //console.log(sucursal);
            }

            /********************************************************************
             * Funciones para la directiva de eliminar plan exequial
             ********************************************************************/
            vm.seleccionoEmpleado = function (empleado) {
                var objeto = getObjectByEmpleadoId(empleado.id, vm.grupoInformal.listaEmpleados);
                if (objeto == undefined) {
                    vm.grupoInformal.listaEmpleados.push({ empleadoId: empleado.id, codigo: empleado.codigo, nombreCompleto: empleado.nombreCompleto, localidad: empleado.localidad + ', ' + empleado.departamento, tipo: 'N' });
                    vm.grupoInformal.listaEmpleadosTabla = bow.tablas.paginar(vm.grupoInformal.listaEmpleados, 10);
                }
            }

            /********************************************************************
             * Funciones para la directiva de eliminar plan exequial
             ********************************************************************/
            vm.verHistorialEmpleado = function () {
                console.log(empleado);
            }

            /************************************************************************
             * Función para obtener el elemento de una lista según el id
             ************************************************************************/
            function getObjectByEmpleadoId(id, arrayList) {
                return arrayList.filter(function (obj) {
                    if (obj.empleadoId == id) {
                        return obj
                    }
                })[0]
            }

            /********************************************************************
             * Funciones para la directiva de eliminar empleado
             ********************************************************************/

            vm.puedeEliminarEmpleado = function (empleadoId, funcionRetornarPuedeEliminar) {
                funcionRetornarPuedeEliminar(true);
            }

            vm.noPuedeEliminar = function () {
                console.log('No puede eliminar');
            };

            vm.eliminarEmpleadoOk = function (empleadoId, tipo) {
                //  Validamos si el telefono eliminado fue consultado desde el backend para posteriormente eliminarlo
                if (tipo == "N") {
                    //  Filtramos el array para eliminar el objecto
                    vm.grupoInformal.listaEmpleados = vm.grupoInformal.listaEmpleados.filter(function (el) {
                        return el.empleadoId !== empleadoId;
                    });
                    vm.grupoInformal.listaEmpleadosTabla = bow.tablas.paginar(vm.grupoInformal.listaEmpleados, 10);
                }
            };

            /********************************************************************
             * Función para guardar el plan exequial
             ********************************************************************/
            function guardarGrupoInformal() {
                afiliacionesService.saveGrupoInformal({
                    nombre: vm.grupoInformal.nombre,
                    porcentajeDescuento: vm.grupoInformal.porcentajeDescuento,
                    encargadoExento: vm.grupoInformal.encargadoExentoPago,
                    personaId: vm.grupoInformal.encargado.personaId,
                    sucursalId: vm.grupoInformal.sucursal.id
                })
                    .success(function (data) {
                        afiliacionesService.saveGrupoInformalEmpleado({
                            grupoInformalId: data.id,
                            empleados: vm.grupoInformal.listaEmpleados
                        })
                            .success(function (data) {
                                abp.notify.success(abp.localization.localize('afiliaciones_grupoInformal_almacenado_correctamente', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                                $scope.mensajeError = "";
                                $scope.controlBuscarGrupoInformal.recargarListadoGruposInformales();
                                vm.formularioGrupoInformalVisible = false;
                                vm.limpiarFormularioGrupo();
                            }).error(function (error) {
                            
                            });

                    }).error(function (error) {
                        if (error.validationErrors != null) {
                            $scope.mensajeError = error.validationErrors[0].message;
                        }
                        else {
                            $scope.mensajeError = error.message;
                        }
                    });
            }

            /************************************************************************
             * Llamado guardar o modificar el grupo informal
             ************************************************************************/
            vm.guardarGrupoInformal = function () {
                if (vm.accionFormulario == 'Guardar') {
                    guardarGrupoInformal();
                }
            };

            /********************************************************************
             * Funciones para consultar la información del grupo informal
             ********************************************************************/
            vm.seleccionoGrupo = function (grupoInformal) {
                vm.formularioGrupoInformalVisible = true;
                vm.accionFormulario = 'Modificar';
                vm.modoConsulta = true;
                afiliacionesService.getGrupoInformal({ id: grupoInformal.id })
                    .success(function (data) {
                        vm.grupoInformal = {
                            id: data.id,
                            nombre: data.nombre,
                            sucursal: { 
                                id: data.sucursalId,
                                nombre: data.sucursalNombre,
                                nombreEmpresa: data.sucursalNombreEmpresa,
                                nombreOrganizacion: data.sucursalNombreOrganizacion
                            },
                            encargado: {
                                personaId: data.personaId,
                                documento: data.documento,
                                personaNombre: data.personaNombre,
                                telefonos: [],
                                direcciones: []
                            },
                            encargadoExentoPago: String(data.encargadoExento),
                            porcentajeDescuento: data.porcentajeDescuento,
                            fechaIngreso: data.fechaIngreso,
                            fechaCancelacion: data.fechaCancelacion,
                            empleadoId: '',
                            listaEmpleados: [],
                            listaEmpleadosTabla: []
                        };

                        personasService.getTelefonosPersona({ PersonaId: data.personaId })
                                .success(function (data) {
                                    vm.grupoInformal.encargado.telefonos = data.telefonosPersona;
                                }).error(function (error) {
                                    $scope.mensajeError = error.message;
                                });

                        personasService.getDireccionesPersona({ PersonaId: data.personaId })
                            .success(function (data) {
                                vm.grupoInformal.encargado.direcciones = data.direccionesPersona;
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        afiliacionesService.getAllEmpleadosByGrupoInformal({ grupoInformalId: data.id })
                            .success(function (data) {
                                vm.grupoInformal.listaEmpleados = data.empleados;
                                vm.grupoInformal.listaEmpleadosTabla = bow.tablas.paginar(vm.grupoInformal.listaEmpleados, 10);
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        vm.documentoValido = true;

                    }).error(function (error) {

                    });
            }

            /********************************************************************
             * Función para mostrar el formulario
             ********************************************************************/
            vm.mostrarFormulario = function (mostrar) {
                if (mostrar) {
                    vm.limpiarFormularioGrupo();
                }
                vm.formularioGrupoInformalVisible = mostrar;
            }

        }]);
})();

