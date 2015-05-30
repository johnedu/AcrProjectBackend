(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.empresas.empresa';

    /*****************************************************************
     * 
     * CONTROLADOR EMPRESA ORGANIZACIÓN
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.personas', 'abp.services.app.empresas', 'abp.services.app.parametros', 'abp.services.app.zonificacion',
        function ($scope, $modal, personasService, empresasService, parametrosService, zonificacionService) {
            var vm = this;

            vm.modoConsulta = true;

            /****************************************************************************************************************
            ************************************************** Organización *************************************************
            *****************************************************************************************************************/

            ////Inicializando modelos
            vm.verFormularioOrganizacion = false;
            vm.NombreOrganizacion = "";
            var IdOrganizacion = 0;

            vm.listaConveniosRecaudoMasivo = [];
            vm.listaEmpresas = [];
            vm.selectedEmpresa = "";

            //  Objeto para editar la organización
            vm.organizacion = {
                id: '',
                nombre: ''
            };

            /********************************************************************
             * Función para cargar la informacíón de la oganización
             ********************************************************************/
            function cargarInformacionOrganizacion() {
                empresasService.getOrganizacion({ id: 0 })
                    .success(function (data) {
                        vm.organizacion = data;
                        IdOrganizacion = data.id;
                        vm.NombreOrganizacion = data.nombre;
                        cargarEmpresasOrganizacion();
                    });
            }
            cargarInformacionOrganizacion();

            /************************************************************************
             * Llamado para mostrar el formulario para editar información de la organización
             ************************************************************************/
            vm.mostrarFormularioEditarOrganizacion = function (mostrar) {
                vm.verFormularioOrganizacion = mostrar;
                if (mostrar) {
                    cargarInformacionOrganizacion();
                }
            };

            /********************************************************************
             * Función para editar la organización
             ********************************************************************/
            vm.editarOrganizacion = function () {
                empresasService.updateOrganizacion(vm.organizacion)
                    .success(function () {
                        abp.notify.success(abp.localization.localize('empresas_organizacion_actualizar_guardado', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                        vm.verFormularioOrganizacion = false
                        vm.NombreOrganizacion = vm.organizacion.nombre;
                        $scope.mensajeError = "";
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            };

            /********************************************************************
             * Función para cargar las empresas de la organización
             ********************************************************************/
            function cargarEmpresasOrganizacion() {
                empresasService.getAllEmpresasByOrganizacion({ OrganizacionId: IdOrganizacion })
                    .success(function (data) {
                        vm.listaEmpresas = data.empresas;
                    });
            }

            /********************************************************************
             * Función para cargar la informacíón de los convenios de recaudo
             ********************************************************************/
            function cargarInformacionConveniosRecaudo() {
                empresasService.getAllConveniosRecaudoMasivo()
                    .success(function (data) {
                        vm.listaConveniosRecaudoMasivo = data.convenios;
                    });
            }
            cargarInformacionConveniosRecaudo();

            /************************************************************************
            * Llamado para abrir Modal para Gestionar Localidades de los convenios de recaudo
            ************************************************************************/
            vm.abrirModalLocalidades = function (convenioRecaudoId, convenioRecaudoNombre) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/empresas/empresa/partials/modalLocalidadesConvenioRecaudo.cshtml',
                    controller: 'modalLocalidadesConvenioRecaudoController',
                    keyboard: false,
                    backdrop: 'static',
                    size: 'md',
                    resolve: {
                        convenioRecaudoId: function () {
                            return convenioRecaudoId;
                        },
                        convenioRecaudoNombre: function () {
                            return convenioRecaudoNombre;
                        }
                    }
                });

                modalInstance.result.then(function () {

                }, function () {
                    cargarInformacionConveniosRecaudo();
                });
            }

            /****************************************************************************************************************
            *********************************************** Información Básica **********************************************
            *****************************************************************************************************************/

            ////Inicializando modelos
            vm.FormularioNuevoEmpresa = false;
            var AccionFormulario = "Guardar";
            var IdJuridica = 0, IdLocalidad = 0;

            vm.tiposNaturalezaEmpresa = [];
            vm.selectedNaturaleza = "";
            vm.EsNaturalezaJuridica = false;

            vm.tiposDocumentoPersona = [];
            vm.selectedTipoDocumento = "";
            vm.BuscadorPersonas = false;
            vm.DocumentoValido = false;

            vm.controlOperacionesTelefono = {};
            var listaTelefonosTabla = [], listaTelefonosAgregados = [], listaTelefonosEliminados = [];

            //  Objetos para los Typeahead
            vm.actividadesEconomicas = [];
            vm.selectedActividadEconomica = "";
            vm.selectedNacionalidad = "";

            //  Objeto para agregar o actualizar la empresa
            vm.empresaOrganizacion = {
                id: '',
                empresaId: '',
                documento: '',
                razonSocial: '',
                nombreComercial: '',
                nombreInterno: '',
                direccionId: '',
                direccion: '',
                estado: 'Activo',
                personaId: null,
                listaTelefonos: [],
                listaContactosWeb: [],
                listaContactos: [],
                listaOpcionesInfoTributaria: [],
                identificador: '',
                checkActivas: true,
                contactoPersona: {
                    documentoContacto: '',
                    idPersonaContacto: '',
                    nombreContacto: '',
                    telefonosContacto: '',
                    cargoContacto: ''
                },
                infoTributaria: {
                    valor: '',
                    fechaInicio: ''
                },
                sucursal: {
                    id: '',
                    nombre: '',
                    direccionId: '',
                    direccion: '',
                    listaTelefonos: []
                }
            };

            /************************************************************************
             * Llamado para limpiar la información del formulario
             ************************************************************************/
            function limpiarFormulario() {
                cargarTiposNaturalezaEmpresa();
                cargarActividadesEconomicas();
                cargarTiposSucursales();
                cargarEstadosSucursales();

                //  Limpiamos la información del formulario
                $scope.frmRegistrarEmpresaOrganizacion.$setPristine();
                vm.empresaOrganizacion.documento = "";
                vm.empresaOrganizacion.razonSocial = "";
                vm.empresaOrganizacion.nombreComercial = "";
                vm.empresaOrganizacion.nombreInterno = "";
                vm.empresaOrganizacion.direccion = "";
                vm.empresaOrganizacion.estado = "Activo";

                //  Limpiamos las listas locales
                listaTelefonosTabla = [];
                listaTelefonosAgregados = [];
                listaTelefonosEliminados = [];
                listaContactosWebTabla = [];
                listaContactosWebAgregados = [];
                listaContactosWebEliminados = [];
                listaContactosTabla = [];
                listaContactosAgregados = [];
                listaContactosEliminados = [];
                listaOpcionesInfoTributariaTabla = [];
                listaOpcionesInfoTributariaAgregados = [];

                //  Limpiamos las tablas del formulario
                vm.empresaOrganizacion.listaTelefonos = [];
                vm.empresaOrganizacion.listaContactosWeb = [];
                vm.empresaOrganizacion.listaContactos = [];
                vm.empresaOrganizacion.listaOpcionesInfoTributaria = [];

                //  Limpiamos los combos seleccionados
                vm.selectedActividadEconomica = "";
                vm.selectedNacionalidad = "";
                vm.selectedNaturaleza = "";
                vm.selectedTipoDocumento = "";
                vm.selectedMediosContacto = "";
                vm.selectedAreaEmpresa = "";
                vm.selectedInfoTributaria = "";
                vm.selectedOpcionInfoTributaria = "";

                //  Ocultamos todos los formularios de adición
                vm.FormularioNuevoTelefono = false;
                vm.FormularioNuevoContactoWeb = false;
                vm.FormularioNuevoContacto = false;
                vm.FormularioNuevaInfoTributaria = false;
                vm.FormularioNuevaSucursal = false;
                vm.FormularioNuevoTelefonoSucursal = false;

                //  Limpiamos los indices de los mensajes de confirmación
                vm.mensajeEliminar = [];
                vm.mensajeEliminarContactoWeb = [];
                vm.mensajeEliminarContacto = [];
                vm.mensajeCancelarOpcionInfoTributaria = [];
                vm.tablaCheckInfoActualizada = [];

                vm.empresaOrganizacion.checkActivas = true;
            }

            /********************************************************************
             * Función para cargar las actividades económicas
             ********************************************************************/
            function cargarActividadesEconomicas() {
                empresasService.getActividadesEconomicas()
                    .success(function (data) {
                        vm.actividadesEconomicas = data.actividadesEconomicas;
                    });
            }

            /********************************************************************
             * Función para cargar la informacíón de los tipos de Naturaleza Empresa
             ********************************************************************/
            function cargarTiposNaturalezaEmpresa() {
                parametrosService.getTiposNaturalezaEmpresa({ id: 0 })
                    .success(function (data) {
                        vm.tiposNaturalezaEmpresa = data.tipos;
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            /********************************************************************
             * Función para cargar la informacíón del tipo de Naturaleza Jurídica
             ********************************************************************/
            function cargarTipoNaturalezaJuridica(IdTipoNaturaleza) {
                parametrosService.getTipoJuridica()
                    .success(function (data) {
                        IdJuridica = data.id;
                        if (IdTipoNaturaleza != undefined) {
                            if (IdTipoNaturaleza == IdJuridica) {
                                vm.BuscadorPersonas = false;
                                vm.EsNaturalezaJuridica = true;
                                vm.DocumentoValido = false;
                            }
                            else {
                                vm.BuscadorPersonas = true;
                                vm.EsNaturalezaJuridica = false;
                                vm.DocumentoValido = true;
                            }
                        }
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            /*********************************************************************
             * Función que se ejecuta al seleccionar una nacionalidad
             ********************************************************************/
            vm.seleccionoNacionalidad = function (pais) {
                vm.selectedNacionalidad = pais;
                vm.cargarTiposDocumentoPersona();
            }

            /********************************************************************
             * Función para cargar la informacíón de los tipos de Documento Persona
             ********************************************************************/
            vm.cargarTiposDocumentoPersona = function () {
                if (vm.selectedNacionalidad != "" && vm.selectedNaturaleza != "") {
                    personasService.getTiposDocumentoOrganizacion({ PaisId: vm.selectedNacionalidad.id, NaturalezaEmpresa: vm.selectedNaturaleza })
                        .success(function (data) {
                            vm.tiposDocumentoPersona = data.tiposDocumento;
                            if (AccionFormulario == "Guardar") {
                                personasService.getTipoDocumentoPorDefecto({ PaisId: vm.selectedNacionalidad.id })
                                    .success(function (data) {
                                        if (data != null) {
                                            vm.selectedTipoDocumento = data.id;
                                        }
                                    }).error(function (error) {
                                        $scope.mensajeError = error.message;
                                    });
                            }
                        }).error(function (error) {
                            $scope.mensajeError = error.message;
                        });
                }
            };

            /************************************************************************
             * Llamado para consultar la información de la persona con el documento de identidad
             ************************************************************************/
            vm.consultarPersona = function () {
                personasService.getPersonaWithTelefono({ Documento: vm.empresaOrganizacion.contactoPersona.documentoContacto })
                    .success(function (data) {
                        if (data.nombreCompleto != null) {
                            vm.empresaOrganizacion.contactoPersona.idPersonaContacto = data.id;
                            vm.empresaOrganizacion.contactoPersona.nombreContacto = data.nombreCompleto;

                            var stringTelefonos = [];
                            $.each(data.telefonos, function (idx, val) {
                                stringTelefonos.push(val.numero);
                            });

                            vm.empresaOrganizacion.contactoPersona.telefonosContacto = stringTelefonos.join(", ");
                            vm.PersonaContactoEncontrada = true;
                        }
                        else {
                            abp.notify.error(abp.localization.localize('empresas_empresaOrganizacion_contactos_personaNoEncontrada', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                        }
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            };

            /********************************************************************
             * Función encargada validar la naturaleza de la empresa para mostrar o no el buscador de personas
             ********************************************************************/
            vm.validarNaturalezaEmpresa = function () {
                if (vm.selectedNaturaleza == IdJuridica) {
                    vm.BuscadorPersonas = false;
                    vm.EsNaturalezaJuridica = true;
                    vm.DocumentoValido = false;
                    vm.empresaOrganizacion.personaId = null;
                }
                else {
                    vm.BuscadorPersonas = true;
                    vm.EsNaturalezaJuridica = false;
                }
                vm.cargarTiposDocumentoPersona();
            };

            /********************************************************************
             * Función encargada llamar el metodo guardar o actualizar segun la acción
             ********************************************************************/
            vm.guardarEmpresaOrganizacion = function () {
                if (AccionFormulario == "Guardar")
                    guardarEmpresaOrganizacion();
                else
                    actualizarEmpresaOrganizacion();
            };

            /********************************************************************
             * Función para guardar la empresa en la organización
             ********************************************************************/
            function guardarEmpresaOrganizacion() {
                //  Objeto para guardar la empresa
                empresaGuardar = {
                    nombre: vm.empresaOrganizacion.nombreInterno,
                    organizacionId: IdOrganizacion,
                    empresaId: 0,
                    tipoNaturalezaId: vm.selectedNaturaleza,
                    tipoDocumentoId: vm.selectedTipoDocumento,
                    documento: vm.empresaOrganizacion.documento,
                    razonSocial: vm.empresaOrganizacion.razonSocial,
                    nombreComercial: vm.empresaOrganizacion.nombreComercial,
                    personaId: vm.empresaOrganizacion.personaId,
                    actividadEconomicaId: vm.selectedActividadEconomica.id,
                    direccionId: vm.empresaOrganizacion.direccionId,
                    estado: vm.empresaOrganizacion.estado
                };

                empresasService.saveEmpresaOrganizacion(empresaGuardar)
                    .success(function (data) {
                        vm.FormularioNuevoEmpresa = false;
                        cargarEmpresasOrganizacion();
                        $scope.mensajeError = "";

                        empresasService.updateEmpresaTelefono({ empresaId: data.empresaId, telefonos: listaTelefonosAgregados })
                            .success(function () {
                                //abp.notify.info(abp.localization.localize('empresas_empresaOrganizacion_guardar_guardado', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        empresasService.updateEmpresaContactosWeb({ empresaId: data.empresaId, contactosWeb: listaContactosWebAgregados })
                            .success(function () {
                                //abp.notify.success(abp.localization.localize('empresas_empresaOrganizacion_guardar_guardado', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        empresasService.updateEmpresaContactos({ empresaId: data.empresaId, contactos: listaContactosAgregados })
                            .success(function () {
                                abp.notify.success(abp.localization.localize('empresas_empresaOrganizacion_guardar_guardado', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });

            }

            /********************************************************************
             * Función para cambiar el formulario a modo edición
             ********************************************************************/
            vm.editarEmpresaOrganizacion = function () {
                vm.modoConsulta = false;
            }

            /********************************************************************
             * Función para cargar la informacíón de una empresa asociada a la organización
             ********************************************************************/
            vm.cargarInformacionEmpresaOrganizacion = function () {
                if (vm.selectedEmpresa == undefined) {
                    vm.FormularioNuevoEmpresa = false;
                }
                else {
                    vm.modoConsulta = true;
                    empresasService.getEmpresaOrganizacion({ OrganizacionId: IdOrganizacion, EmpresaId: vm.selectedEmpresa.empresaId })
                    .success(function (data) {
                        vm.FormularioNuevoEmpresa = true;
                        IdLocalidad = data.localidadId;
                        limpiarFormulario();
                        cargarContactosWebTodos();
                        cargarTiposAreaEmpresaTodos();
                        cargarInfoTributariaTodas();
                        cargarTiposSucursales();
                        cargarEstadosSucursales();

                        vm.empresaOrganizacion.id = data.id;
                        vm.empresaOrganizacion.documento = data.documento;
                        vm.empresaOrganizacion.razonSocial = data.razonSocial;
                        vm.empresaOrganizacion.nombreComercial = data.nombreComercial;
                        vm.empresaOrganizacion.nombreInterno = data.nombre;
                        vm.empresaOrganizacion.direccionId = data.direccionId;
                        vm.empresaOrganizacion.direccion = data.direccion;
                        vm.empresaOrganizacion.personaId = data.personaId;
                        vm.empresaOrganizacion.empresaId = vm.selectedEmpresa.empresaId;
                        vm.empresaOrganizacion.estado = data.estado;

                        vm.selectedNaturaleza = data.tipoNaturalezaId;
                        vm.selectedActividadEconomica = { id: data.actividadEconomicaId, codigo: data.actividadEconomicaCodigo, nombre: data.actividadEconomicaNombre };
                        vm.selectedNacionalidad = { id: data.paisTipoDocumentoId, nombre: data.paisTipoDocumentoNombre };
                        AccionFormulario = "Modificar";
                        vm.cargarTiposDocumentoPersona();
                        vm.selectedTipoDocumento = data.tipoDocumentoId;

                        empresasService.getAllTelefonosEmpresa({ EmpresaId: vm.selectedEmpresa.empresaId })
                            .success(function (data) {
                                listaTelefonosAgregados = listaTelefonosAgregados.concat(data.telefonos);
                                listaTelefonosTabla = data.telefonos;
                                vm.empresaOrganizacion.listaTelefonos = bow.tablas.paginar(listaTelefonosTabla, 5);
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        empresasService.getContactosWebEmpresa({ EmpresaId: vm.selectedEmpresa.empresaId })
                            .success(function (data) {
                                listaContactosWebTabla = data.contactosWeb;
                                vm.empresaOrganizacion.listaContactosWeb = bow.tablas.paginar(listaContactosWebTabla, 5);
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        empresasService.getContactosEmpresa({ EmpresaId: vm.selectedEmpresa.empresaId })
                            .success(function (data) {
                                listaContactosTabla = data.contactos;
                                vm.empresaOrganizacion.listaContactos = bow.tablas.paginar(listaContactosTabla, 5);
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        empresasService.getAllOpcionesInfoTributariaEmpresa({ EmpresaId: vm.selectedEmpresa.empresaId })
                            .success(function (data) {
                                listaOpcionesInfoTributariaTabla = data.opcionesInfoTributaria;
                                vm.empresaOrganizacion.listaOpcionesInfoTributaria = bow.tablas.paginar(listaOpcionesInfoTributariaTabla, 5);
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        cargarSucursalesEmpresasOrganizacion();
                        cargarTipoNaturalezaJuridica(data.tipoNaturalezaId);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
                }
            };

            /********************************************************************
             * Función para actualizar la empresa en la organización
             ********************************************************************/
            function actualizarEmpresaOrganizacion() {
                //  Agregamos el array de telefonos eliminados para enviarlos al backend y eliminarlos de la BD
                listaTelefonosAgregados = listaTelefonosAgregados.concat(listaTelefonosEliminados);
                //  Agregamos el array de contactos web eliminados para enviarlos al backend y eliminarlos de la BD
                listaContactosWebAgregados = listaContactosWebAgregados.concat(listaContactosWebEliminados);
                //  Agregamos el array de contactos eliminados para enviarlos al backend y eliminarlos de la BD
                listaContactosAgregados = listaContactosAgregados.concat(listaContactosEliminados);
                //  Objeto para modificar la empresa
                empresaModificar = {
                    id: vm.empresaOrganizacion.id,
                    nombre: vm.empresaOrganizacion.nombreInterno,
                    organizacionId: IdOrganizacion,
                    empresaId: vm.empresaOrganizacion.empresaId,
                    tipoNaturalezaId: vm.selectedNaturaleza,
                    tipoDocumentoId: vm.selectedTipoDocumento,
                    documento: vm.empresaOrganizacion.documento,
                    razonSocial: vm.empresaOrganizacion.razonSocial,
                    nombreComercial: vm.empresaOrganizacion.nombreComercial,
                    personaId: vm.empresaOrganizacion.personaId,
                    actividadEconomicaId: vm.selectedActividadEconomica.id,
                    direccionId: vm.empresaOrganizacion.direccionId,
                    estado: vm.empresaOrganizacion.estado
                };

                empresasService.updateEmpresaOrganizacion(empresaModificar)
                    .success(function () {
                        $scope.mensajeError = "";

                        empresasService.updateEmpresaTelefono({ empresaId: vm.empresaOrganizacion.empresaId, telefonos: listaTelefonosAgregados })
                            .success(function () {
                                //abp.notify.info(abp.localization.localize('empresas_empresaOrganizacion_actualizar_guardado', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        empresasService.updateEmpresaContactosWeb({ empresaId: vm.empresaOrganizacion.empresaId, contactosWeb: listaContactosWebAgregados })
                            .success(function () {
                                //abp.notify.success(abp.localization.localize('empresas_empresaOrganizacion_guardar_guardado', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        empresasService.updateEmpresaContactos({ empresaId: vm.empresaOrganizacion.empresaId, contactos: listaContactosAgregados })
                            .success(function () {
                                //abp.notify.success(abp.localization.localize('empresas_empresaOrganizacion_guardar_guardado', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                        empresasService.updateEmpresaInfoTributaria({ empresaId: vm.empresaOrganizacion.empresaId, infoTributarias: listaOpcionesInfoTributariaAgregados })
                            .success(function () {
                                abp.notify.success(abp.localization.localize('empresas_empresaOrganizacion_guardar_guardado', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                                vm.FormularioNuevoEmpresa = false;
                                cargarEmpresasOrganizacion();
                            }).error(function (error) {
                                $scope.mensajeError = error.message;
                            });

                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }



            /************************************************************************
             * Llamado para mostrar el formulario para agregar la nueva empresa
             ************************************************************************/
            vm.mostrarFormularioEmpresa = function (mostrar) {
                vm.FormularioNuevoEmpresa = mostrar;
                if (mostrar) {
                    limpiarFormulario();
                    cargarTipoNaturalezaJuridica(undefined);
                    AccionFormulario = "Guardar";
                    vm.selectedEmpresa = "";
                    cargarContactosWebTodos();
                    cargarTiposAreaEmpresaTodos();
                    vm.modoConsulta = false;
                }
                else {
                    vm.selectedEmpresa = "";
                }
            };

            /************************************************************************
             * Llamado para abrir Modal de la Dirección
             ************************************************************************/

            vm.direccionRegistrada = function (direccion) {
                if (direccion != "cancel") {
                    vm.empresaOrganizacion.direccionId = direccion.id;
                    IdLocalidad = direccion.localidadId;
                    vm.empresaOrganizacion.direccion = direccion.direccionCompleta;
                    cargarInfoTributariaTodas();
                }
            }

            /************************************************************************
             * Llamado para validar el documento de identidad de la persona
             ************************************************************************/
            vm.validarDocumentoPersona = function () {
                personasService.validarDocumentoExiste({ NumeroDocumento: vm.empresaOrganizacion.documento, TipoDocumentoId: vm.selectedTipoDocumento })
                    .success(function (data) {
                        if (data.documentoExiste) {
                            abp.notify.success(abp.localization.localize('empresas_empresaOrganizacion_persona_existe', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                            vm.DocumentoValido = true;
                            vm.empresaOrganizacion.personaId = data.id;
                        }
                        else {
                            abp.notify.error(abp.localization.localize('empresas_empresaOrganizacion_persona_noExiste', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                            vm.DocumentoValido = false;
                        }
                        $scope.mensajeError = "";
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            };

            /************************************************************************
             * Llamado para ocultar el formulario para editar información de la organización
             ************************************************************************/
            vm.cambiarDocumentoPersona = function () {
                vm.DocumentoValido = false;
            };

            /****************************************************************************************************************
            ************************************** Información de Contacto de la Empresa ************************************
            *****************************************************************************************************************/

            ////Inicializando modelos
            vm.FormularioNuevoTelefono = false;
            
            /********************************************************************
             * Controles para eliminar teléfonos de la empresa
             ********************************************************************/
            vm.controlesTelefono = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesTelefono.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesTelefono.visible[$index] = false;
                }
            };

            /************************************************************************
             * Llamado para abrir formulario de Teléfono
             ************************************************************************/
            vm.mostrarNuevoTelefono = function (mostrar) {
                vm.FormularioNuevoTelefono = mostrar;
                vm.controlOperacionesTelefono.limpiarFormulario();
            };

            /************************************************************************
             * Directiva para capturar el valor ingresado en telefono
             ************************************************************************/
            vm.notificacionTelefonoEmpresaGuardado = function (telefono) {
                var nuevoTelefono =
                {
                    id: 0,
                    telefonoId: telefono.id,
                    telefonoNumero: telefono.telefonoCompleto,
                    nombreLocalidad: telefono.ubicacion,
                    accion: 'N'
                };

                //  Validamos si el objecto ya fue agregado al array
                if (!getObjectById(telefono.id, listaTelefonosAgregados)) {
                    listaTelefonosAgregados.push({ id: 0, empresaId: 0, telefonoId: telefono.id, accion: "N" });
                    listaTelefonosTabla.push(nuevoTelefono);
                    vm.empresaOrganizacion.listaTelefonos = bow.tablas.paginar(listaTelefonosTabla, 5);
                }
                else {
                    abp.notify.warn(abp.localization.localize('empresas_empresaOrganizacion_telefonos_yaAgregado', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                }
                vm.FormularioNuevoTelefono = false;
            };

            /************************************************************************
             * Función para obtener el elemento de una lista según el telefonoId
             ************************************************************************/
            function getObjectByTelefonoId(telefonoId, arrayList) {
                return arrayList.filter(function (obj) {
                    if (obj.telefonoId == telefonoId) {
                        return obj
                    }
                })[0]
            }

            /********************************************************************
             * Funciones para eliminar un teléfono de la lista
             ********************************************************************/
            vm.puedeEliminarTelefono = function (telefonoId, funcionRetornarPuedeEliminar) {
                funcionRetornarPuedeEliminar(true);
            }

            vm.noPuedeEliminarTelefono = function () {
                
            }

            vm.eliminarTelefonoOk = function (id, telefonoId, telefonoNumero, nombreLocalidad, accion, $index) {
                //  Validamos si el telefono eliminado fue consultado desde el backend para posteriormente eliminarlo
                if (accion == "C") {
                    //  Validamos si el objecto ya fue eliminado antes
                    if (JSON.stringify(listaTelefonosEliminados).indexOf(JSON.stringify({ id: id, empresaId: 0, telefonoId: telefonoId, accion: "E" })) == -1) {
                        listaTelefonosEliminados.push({ id: id, empresaId: 0, telefonoId: telefonoId, accion: "E" });
                    }
                }
                //  Filtramos el array para eliminar el objecto
                listaTelefonosTabla = listaTelefonosTabla.filter(function (el) {
                    return el.telefonoId !== telefonoId;
                });
                vm.empresaOrganizacion.listaTelefonos = bow.tablas.paginar(listaTelefonosTabla, 5);

                //  Filtramos tambien el array de los telefonos agregados para la BD
                listaTelefonosAgregados = listaTelefonosAgregados.filter(function (el) {
                    return el.telefonoId !== telefonoId;
                });

                vm.mensajeEliminar[$index] = false;
            };

            /****************************************************************************************************************
            ******************************************* Contactos Web de la Empresa *****************************************
            *****************************************************************************************************************/

            ////Inicializando modelos
            vm.FormularioNuevoContactoWeb = false;
            vm.eliminadoContactoWeb = false;
            vm.AccionFormularioContactosWeb = "Guardar";

            var MedioContactoWebModificar = [];
            var listaContactosWebTabla = [], listaContactosWebAgregados = [], listaContactosWebEliminados = [];

            /************************************************************************
             * Controles para editar o eliminar los contactos Web de la Empresa
             ************************************************************************/
            vm.controlesContactoWeb = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesContactoWeb.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesContactoWeb.visible[$index] = false;
                    vm.eliminadoContactoWeb = false;
                }
            };

            /********************************************************************
             * Función para cargar toda la lista de contactos web
             ********************************************************************/
            function cargarContactosWebTodos() {
                empresasService.getContactosWebFilterByEmpresa({ EmpresaId: vm.selectedEmpresa.empresaId })
                    .success(function (data) {
                        vm.mediosContacto = data.contactosWeb;
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            /************************************************************************
             * Llamado para mostrar el formulario de contactos web
             ************************************************************************/
            vm.mostrarFormularioContactoWeb = function (mostrar) {
                vm.FormularioNuevoContactoWeb = mostrar;
                if (mostrar) {
                    //  Limpiamos la información del formulario
                    $scope.frmRegistrarContactosWeb.$setPristine();
                    vm.empresaOrganizacion.identificador = "";
                    vm.selectedMediosContacto = "";
                    vm.AccionFormularioContactosWeb = "Guardar";
                }
                else {
                    //  Si cancelamos la edición removemos el item del combo
                    if (vm.AccionFormularioContactosWeb = "Modificar") {
                        vm.mediosContacto = vm.mediosContacto.filter(function (el) {
                            return el.id !== MedioContactoWebModificar.idMedioContacto;
                        });
                    }
                }
            };

            /************************************************************************
             * Llamado para almacenar el contacto web en la lista
             ************************************************************************/
            vm.guardarNuevoContactoWeb = function () {
                if (vm.AccionFormularioContactosWeb == "Modificar") {
                    //  Actualizamos el identificador que es el unico dato que se puede modificar
                    MedioContactoWebModificar.identificador = vm.empresaOrganizacion.identificador;

                    //  Validamos si el contacto web a modificar es de base datos
                    if (MedioContactoWebModificar.accion == "C" || MedioContactoWebModificar.accion == "M") {

                        MedioContactoWebModificar.accion = "M";

                        //  Validamos si el objecto ya fue modificado y agregado al array
                        if (JSON.stringify(listaContactosWebAgregados).indexOf("\"id\":" + vm.selectedMediosContacto.id + ",") == -1) {
                            //  Si ya está, eliminamos el objeto modificado anteriormente
                            listaContactosWebAgregados = listaContactosWebAgregados.filter(function (el) {
                                return el.id !== MedioContactoWebModificar.id;
                            });
                        }

                        listaContactosWebAgregados.push({ id: MedioContactoWebModificar.id, empresaId: 0, tipoRedId: MedioContactoWebModificar.idMedioContacto, identificador: MedioContactoWebModificar.identificador, accion: "M" });

                        //  Eliminamos el registro de la tabla
                        listaContactosWebTabla = listaContactosWebTabla.filter(function (el) {
                            return el.id !== MedioContactoWebModificar.id;
                        });

                        //  Agregamos a la tabla el registro modificado
                        listaContactosWebTabla.push(MedioContactoWebModificar);
                        vm.empresaOrganizacion.listaContactosWeb = bow.tablas.paginar(listaContactosWebTabla, 5);
                    }
                    else {
                        MedioContactoWebModificar.accion = "N";

                        //  Eliminamos el registro anterior del objeto de la lista
                        listaContactosWebAgregados = listaContactosWebAgregados.filter(function (el) {
                            return el.tipoRedId !== MedioContactoWebModificar.idMedioContacto;
                        });
                        listaContactosWebTabla = listaContactosWebTabla.filter(function (el) {
                            return el.idMedioContacto !== MedioContactoWebModificar.idMedioContacto;
                        });

                        //  Agregamos el registro modificado como un nuevo registro
                        listaContactosWebAgregados.push({ id: 0, empresaId: 0, tipoRedId: MedioContactoWebModificar.idMedioContacto, identificador: MedioContactoWebModificar.identificador, accion: "N" });
                        listaContactosWebTabla.push(MedioContactoWebModificar);
                        vm.empresaOrganizacion.listaContactosWeb = bow.tablas.paginar(listaContactosWebTabla, 5);
                    }

                    //  Removemos el item del combo
                    vm.mediosContacto = vm.mediosContacto.filter(function (el) {
                        return el.id !== MedioContactoWebModificar.idMedioContacto;
                    });
                }
                else {
                    var nuevoContactoWeb =
                    {
                        id: 0,
                        idMedioContacto: vm.selectedMediosContacto.id,
                        medioContacto: vm.selectedMediosContacto.nombre,
                        identificador: vm.empresaOrganizacion.identificador,
                        accion: 'N'
                    };

                    //  Validamos si el objecto ya fue agregado al array
                    if (JSON.stringify(listaContactosWebAgregados).indexOf(JSON.stringify({ id: 0, empresaId: 0, tipoRedId: vm.selectedMediosContacto.id, identificador: vm.empresaOrganizacion.identificador, accion: "N" })) == -1) {
                        listaContactosWebAgregados.push({ id: 0, empresaId: 0, tipoRedId: vm.selectedMediosContacto.id, identificador: vm.empresaOrganizacion.identificador, accion: "N" });
                        listaContactosWebTabla.push(nuevoContactoWeb);
                        vm.empresaOrganizacion.listaContactosWeb = bow.tablas.paginar(listaContactosWebTabla, 5);

                        //  Filtramos el array para eliminar el objecto del combo
                        vm.mediosContacto = vm.mediosContacto.filter(function (el) {
                            return el.id !== vm.selectedMediosContacto.id;
                        });
                    }
                }
                vm.FormularioNuevoContactoWeb = false;
            };

            /************************************************************************
             * Llamado para cargar la información del contacto web para modificarlo
             ************************************************************************/
            vm.consultarEditarContactoWeb = function (id, idMedioContacto, medioContacto, identificador, accion) {
                //  Limpiamos la información del formulario
                $scope.frmRegistrarContactosWeb.$setPristine();

                //  Guardamos temporalmente el Medio de Contacto
                vm.AccionFormularioContactosWeb = "Modificar";
                MedioContactoWebModificar = {
                    id: id,
                    idMedioContacto: idMedioContacto,
                    medioContacto: medioContacto,
                    identificador: identificador,
                    accion: accion
                };
                var contacto =
                {
                    id: idMedioContacto,
                    nombre: medioContacto,
                    descripcion: null
                };

                //  Colocamos temporalmente el objeto en la lista desplegable
                vm.mediosContacto.push(contacto);
                vm.selectedMediosContacto = contacto;
                vm.empresaOrganizacion.identificador = identificador;

                vm.FormularioNuevoContactoWeb = true;
            };

            /********************************************************************
             * Funciones para eliminar un contacto web de la lista
             ********************************************************************/
            vm.puedeEliminarContactoWeb = function (contactoWebId, funcionRetornarPuedeEliminar) {
                vm.eliminadoContactoWeb = true;
                funcionRetornarPuedeEliminar(true);
            }

            vm.noPuedeEliminarContactoWeb = function () {

            }

            vm.eliminarContactoWebOk = function (id, idMedioContacto, medioContacto, identificador, accion, $index) {
                //  Validamos si el telefono eliminado fue consultado desde el backend para posteriormente eliminarlo
                if (accion == "C" || accion == "M") {
                    //  Validamos si el objecto ya fue eliminado antes
                    if (JSON.stringify(listaContactosWebEliminados).indexOf(JSON.stringify({ id: id, empresaId: 0, tipoRedId: idMedioContacto, identificador: identificador, accion: "E" })) == -1) {
                        listaContactosWebEliminados.push({ id: id, empresaId: 0, tipoRedId: idMedioContacto, identificador: identificador, accion: "E" });
                    }
                }
                else {
                    //  Filtramos el array para eliminar el objecto tambien de la lista de contactos agregados
                    listaContactosWebAgregados = listaContactosWebAgregados.filter(function (el) {
                        return el.tipoRedId !== idMedioContacto;
                    });
                }

                //  Filtramos el array para eliminar el objecto
                listaContactosWebTabla = listaContactosWebTabla.filter(function (el) {
                    return el.idMedioContacto !== idMedioContacto;
                });
                vm.empresaOrganizacion.listaContactosWeb = bow.tablas.paginar(listaContactosWebTabla, 5);

                vm.mensajeEliminarContactoWeb[$index] = false;

                //  Volvemos a colocar el objeto en la lista desplegable
                vm.mediosContacto.push({ id: idMedioContacto, nombre: medioContacto, descripcion: '' });
                vm.eliminadoContactoWeb = false;
            };

            vm.eliminarContactoWebCancel = function () {
                vm.eliminadoContactoWeb = false;
            };

            /****************************************************************************************************************
            ********************************************* Contactos de la Empresa *******************************************
            *****************************************************************************************************************/

            ////Inicializando modelos
            vm.FormularioNuevoContacto = false;
            vm.AccionFormularioContactos = "Guardar"
            vm.PersonaContactoEncontrada = false;
            vm.eliminadoContacto = false;

            vm.mediosContacto = [];
            vm.selectedMediosContacto = "";
            vm.areasEmpresa = [];
            vm.selectedAreaEmpresa = "";

            var listaContactosTabla = [], listaContactosAgregados = [], listaContactosEliminados = [];

            /************************************************************************
             * Controles para editar o eliminar los contactos de la Empresa
             ************************************************************************/
            vm.controlesContacto = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesContacto.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesContacto.visible[$index] = false;
                    vm.eliminadoContacto = false;
                }
            };

            /************************************************************************
             * Llamado para mostrar el formulario de contactos
             ************************************************************************/
            vm.mostrarFormularioContacto = function (mostrar) {
                vm.FormularioNuevoContacto = mostrar;
                if (mostrar) {
                    //  Limpiamos la información del formulario
                    $scope.frmRegistrarContacto.$setPristine();
                    vm.empresaOrganizacion.contactoPersona.documentoContacto = "";
                    vm.empresaOrganizacion.contactoPersona.idPersonaContacto = "";
                    vm.empresaOrganizacion.contactoPersona.nombreContacto = "";
                    vm.empresaOrganizacion.contactoPersona.telefonosContacto = "";
                    vm.empresaOrganizacion.contactoPersona.cargoContacto = "";
                    vm.selectedAreaEmpresa = "";
                    vm.AccionFormularioContactos = "Guardar";
                    vm.PersonaContactoEncontrada = false;
                }
            };

            /********************************************************************
             * Función para cargar toda la lista de tipos de área de empresa
             ********************************************************************/
            function cargarTiposAreaEmpresaTodos() {
                //  Si se desea filtrar el combo con las áreas ya asignadas a la empresa => { EmpresaId: vm.selectedEmpresa.empresaId }
                empresasService.getTiposAreaFilterByEmpresa({ EmpresaId: 0 })
                    .success(function (data) {
                        vm.areasEmpresa = data.tiposAreaEmpresa;
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            };

            /************************************************************************
             * Llamado para almacenar el contacto en la lista
             ************************************************************************/
            vm.guardarNuevoContacto = function () {
                if (vm.AccionFormularioContactos == "Modificar") {
                    //  Validamos si el contacto a modificar es de base datos
                    if (ContactoModificar.accion == "C" || ContactoModificar.accion == "M") {
                        ContactoModificar.accion = "M";

                        //  Validamos si la persona ya fue modificada y agregada al array
                        if (JSON.stringify(listaContactosAgregados).indexOf("\"personaId\":" + ContactoModificar.idPersona + ",") == -1) {
                            //  Si ya está, eliminamos el objeto modificado anteriormente
                            listaContactosAgregados = listaContactosAgregados.filter(function (el) {
                                return el.personaId !== ContactoModificar.idPersona;
                            });
                        }

                        //  Eliminamos el registro de la tabla
                        listaContactosTabla = listaContactosTabla.filter(function (el) {
                            return el.id !== ContactoModificar.id;
                        });
                    }
                    else {
                        ContactoModificar.accion = "N";

                        //  Eliminamos el registro anterior del objeto de la lista
                        listaContactosAgregados = listaContactosAgregados.filter(function (el) {
                            return el.personaId !== ContactoModificar.idPersona;
                        });

                        listaContactosTabla = listaContactosTabla.filter(function (el) {
                            return el.idPersona !== ContactoModificar.idPersona;
                        });
                    }

                    //  Modificamos la información del contacto
                    ContactoModificar.documentoPersona = vm.empresaOrganizacion.contactoPersona.documentoContacto;
                    ContactoModificar.idPersona = vm.empresaOrganizacion.contactoPersona.idPersonaContacto;
                    ContactoModificar.nombrePersona = vm.empresaOrganizacion.contactoPersona.nombreContacto;
                    ContactoModificar.telefonosContacto = vm.empresaOrganizacion.contactoPersona.telefonosContacto;
                    ContactoModificar.idTipoAreaEmpresa = vm.selectedAreaEmpresa.id;
                    ContactoModificar.tipoAreaEmpresa = vm.selectedAreaEmpresa.nombre;
                    ContactoModificar.cargo = vm.empresaOrganizacion.contactoPersona.cargoContacto;

                    //  Agregamos el registro modificado como un nuevo registro
                    listaContactosAgregados.push({ id: ContactoModificar.id, empresaId: 0, personaId: ContactoModificar.idPersona, tipoAreaEmpresaId: vm.selectedAreaEmpresa.id, cargo: ContactoModificar.cargo, accion: ContactoModificar.accion });

                    //  Agregamos a la tabla el registro modificado
                    listaContactosTabla.push(ContactoModificar);
                    vm.empresaOrganizacion.listaContactos = bow.tablas.paginar(listaContactosTabla, 5);

                    vm.FormularioNuevoContacto = false;
                }
                else {
                    var nuevoContacto =
                    {
                        id: 0,
                        documentoPersona: vm.empresaOrganizacion.contactoPersona.documentoContacto,
                        idPersona: vm.empresaOrganizacion.contactoPersona.idPersonaContacto,
                        nombrePersona: vm.empresaOrganizacion.contactoPersona.nombreContacto,
                        telefonosContacto: vm.empresaOrganizacion.contactoPersona.telefonosContacto,
                        idTipoAreaEmpresa: vm.selectedAreaEmpresa.id,
                        tipoAreaEmpresa: vm.selectedAreaEmpresa.nombre,
                        cargo: vm.empresaOrganizacion.contactoPersona.cargoContacto,
                        accion: 'N'
                    };

                    //  Validamos si el objecto ya fue agregado al array
                    if (JSON.stringify(listaContactosTabla).indexOf("\"idPersona\":" + vm.empresaOrganizacion.contactoPersona.idPersonaContacto + ",") == -1) {
                        listaContactosAgregados.push({ id: 0, empresaId: 0, personaId: vm.empresaOrganizacion.contactoPersona.idPersonaContacto, tipoAreaEmpresaId: vm.selectedAreaEmpresa.id, cargo: vm.empresaOrganizacion.contactoPersona.cargoContacto, accion: "N" });
                        listaContactosTabla.push(nuevoContacto);
                        vm.empresaOrganizacion.listaContactos = bow.tablas.paginar(listaContactosTabla, 5);

                        vm.FormularioNuevoContacto = false;
                    }
                    else {
                        abp.notify.error(abp.localization.localize('empresas_empresaOrganizacion_contactos_personaYaAgregada', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                    }
                }
            };

            /************************************************************************
             * Llamado para cargar la información del contacto para modificarlo
             ************************************************************************/
            vm.consultarEditarContacto = function (id, documentoPersona, idPersona, nombrePersona, telefonosContacto, idTipoAreaEmpresa, tipoAreaEmpresa, cargo, accion) {
                //  Limpiamos la información del formulario
                $scope.frmRegistrarContacto.$setPristine();

                //  Guardamos temporalmente el Medio de Contacto
                vm.AccionFormularioContactos = "Modificar";
                ContactoModificar = {
                    id: id,
                    documentoPersona: documentoPersona,
                    idPersona: idPersona,
                    nombrePersona: nombrePersona,
                    telefonosContacto: telefonosContacto,
                    idTipoAreaEmpresa: idTipoAreaEmpresa,
                    tipoAreaEmpresa: tipoAreaEmpresa,
                    cargo: cargo,
                    accion: accion
                };

                vm.empresaOrganizacion.contactoPersona.documentoContacto = documentoPersona;
                vm.empresaOrganizacion.contactoPersona.idPersonaContacto = idPersona;
                vm.empresaOrganizacion.contactoPersona.nombreContacto = nombrePersona;
                vm.empresaOrganizacion.contactoPersona.telefonosContacto = telefonosContacto;
                vm.empresaOrganizacion.contactoPersona.cargoContacto = cargo;

                vm.selectedAreaEmpresa = getObjectById(idTipoAreaEmpresa, vm.areasEmpresa);

                vm.PersonaContactoEncontrada = true;
                vm.FormularioNuevoContacto = true;
            };

            /********************************************************************
             * Funciones para eliminar un contacto de la lista
             ********************************************************************/
            vm.puedeEliminarContacto = function (tipoDocumentoId, funcionRetornarPuedeEliminar) {
                vm.eliminadocontacto = true;
                funcionRetornarPuedeEliminar(true);
            }

            vm.noPuedeEliminarContacto = function () {
                
            }

            vm.eliminarContactoOk = function (id, idPersona, idTipoAreaEmpresa, tipoAreaEmpresa, cargo, accion, $index) {
                //  Validamos si el telefono eliminado fue consultado desde el backend para posteriormente eliminarlo
                if (accion == "C" || accion == "M") {
                    listaContactosEliminados.push({ id: id, empresaId: 0, personaId: idPersona, tipoAreaEmpresaId: idTipoAreaEmpresa, cargo: cargo, accion: "E" });
                    if (accion == "M") {
                        //  Filtramos el array para eliminar el objecto de la lista de contactos agregados
                        listaContactosAgregados = listaContactosAgregados.filter(function (el) {
                            return el.personaId !== idPersona;
                        });
                    }
                }
                else {
                    //  Filtramos el array para eliminar el objecto de la lista de contactos agregados
                    listaContactosAgregados = listaContactosAgregados.filter(function (el) {
                        return el.personaId !== idPersona;
                    });
                }

                //  Filtramos el array para eliminar el objecto
                listaContactosTabla = listaContactosTabla.filter(function (el) {
                    return el.idPersona !== idPersona;
                });
                vm.empresaOrganizacion.listaContactos = bow.tablas.paginar(listaContactosTabla, 5);

                vm.mensajeEliminarContacto[$index] = false;
                vm.eliminadocontacto = false;
            };

            vm.eliminarContactoCancel = function () {
                vm.eliminadocontacto = false;
            };

            /************************************************************************
             * Llamado para cambiar la persona seleccionada
             ************************************************************************/
            vm.cambiarConsultaPersona = function () {
                vm.PersonaContactoEncontrada = false;
                vm.empresaOrganizacion.contactoPersona.idPersonaContacto = "";
                vm.empresaOrganizacion.contactoPersona.nombreContacto = "";
                vm.empresaOrganizacion.contactoPersona.telefonosContacto = "";
            };

            /****************************************************************************************************************
            *************************************** Información Tributaria de la Empresa ************************************
            *****************************************************************************************************************/

            ////Inicializando modelos
            vm.FormularioNuevaInfoTributaria = false;

            vm.infoTributarias = [];
            vm.selectedInfoTributaria = "";
            vm.opcionesInfoTributaria = [];
            vm.selectedOpcionInfoTributaria = "";

            vm.tablaCheckInfoActualizada = [];
            var listaOpcionesInfoTributariaTabla = [], listaOpcionesInfoTributariaAgregados = [];

            /************************************************************************
             * Llamado para mostrar el formulario de opciones de información tributaria
             ************************************************************************/
            vm.mostrarFormularioInfoTributaria = function (mostrar) {
                vm.FormularioNuevaInfoTributaria = mostrar;
                if (mostrar) {
                    //  Limpiamos la información del formulario
                    $scope.frmRegistrarInfoTributaria.$setPristine();
                    vm.empresaOrganizacion.identificador = "";
                    vm.selectedInfoTributaria = "";
                    vm.selectedOpcionInfoTributaria = "";
                    vm.empresaOrganizacion.infoTributaria.valor = "";
                    vm.empresaOrganizacion.infoTributaria.fechaInicio = "";
                    vm.AccionFormularioInfoTributaria = "Guardar";
                }
            };

            /************************************************************************
             * Llamado para almacenar la opción de información tributaria en la lista
             ************************************************************************/
            vm.guardarNuevaOpcionInfoTributaria = function () {
                var fechaValida = true;
                listaOpcionesInfoTributariaTabla.forEach(function (entry) {
                    if (entry.infoTributariaId == vm.selectedInfoTributaria.id) {
                        if (new Date(vm.empresaOrganizacion.infoTributaria.fechaInicio) < new Date(entry.fechaFin)) {
                            fechaValida = false;
                        }
                    }
                });
                if (fechaValida) {
                    var nuevaOpcionInfoTributaria =
                    {
                        id: 0,
                        infoTributariaOpcionId: vm.selectedOpcionInfoTributaria.id,
                        infoTributariaId: vm.selectedInfoTributaria.id,
                        valor: vm.empresaOrganizacion.infoTributaria.valor,
                        fechaInicio: new Date(vm.empresaOrganizacion.infoTributaria.fechaInicio),
                        fechaFin: null,
                        fechaActualizacion: new Date(vm.empresaOrganizacion.infoTributaria.fechaInicio),
                        accion: 'N'
                    };
                    listaOpcionesInfoTributariaAgregados.push(nuevaOpcionInfoTributaria);

                    var nuevaOpcionInfoTributariaTabla =
                    {
                        id: 0,
                        infoTributariaId: vm.selectedInfoTributaria.id,
                        infoTributariaOpcionId: vm.selectedOpcionInfoTributaria.id,
                        infoTributaria: vm.selectedInfoTributaria.nombre,
                        infoTributariaOpcion: vm.selectedOpcionInfoTributaria.nombre,
                        tipoValor: vm.selectedInfoTributaria.tipoValor,
                        valor: vm.empresaOrganizacion.infoTributaria.valor,
                        fechaInicio: new Date(vm.empresaOrganizacion.infoTributaria.fechaInicio).yyyymmdd(),
                        fechaFin: null,
                        fechaActualizacion: new Date(vm.empresaOrganizacion.infoTributaria.fechaInicio).yyyymmdd(),
                        estadoActiva: true,
                        accion: 'N',
                        idEliminar: 'N' + vm.selectedOpcionInfoTributaria.id
                    };
                    listaOpcionesInfoTributariaTabla.push(nuevaOpcionInfoTributariaTabla);
                    vm.empresaOrganizacion.listaOpcionesInfoTributaria = bow.tablas.paginar(listaOpcionesInfoTributariaTabla, 5);

                    vm.FormularioNuevaInfoTributaria = false;

                    //  Filtramos el array para eliminar el objecto
                    vm.infoTributarias = vm.infoTributarias.filter(function (el) {
                        return el.id !== vm.selectedInfoTributaria.id;
                    });
                    vm.opcionesInfoTributaria = [];
                }
                else {
                    abp.notify.info(abp.localization.localize('empresas_empresaOrganizacion_infoTributaria_fechaInvalida', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                }

            };

            /********************************************************************
             * Función para cargar toda la lista de contactos web
             ********************************************************************/
            function cargarInfoTributariaTodas() {
                empresasService.getAllInfoTributariaByLocalidad({ LocalidadId: IdLocalidad, EmpresaId: vm.selectedEmpresa.empresaId })
                    .success(function (data) {
                        vm.infoTributarias = data.infoTributarias;
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            /********************************************************************
             * Función para cargar toda la lista de contactos web
             ********************************************************************/
            vm.cargarOpcionesInfoTributariaTodas = function (InfoTributariaId, InfoTributariaValor) {
                empresasService.getInfoTributariaOpciones({ InfoTributariaId: InfoTributariaId })
                    .success(function (data) {
                        vm.opcionesInfoTributaria = data.infoTributariaOpciones;
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
                var fechaMinima = null;
                listaOpcionesInfoTributariaTabla.forEach(function (entry) {
                    if (entry.infoTributariaId == InfoTributariaId) {
                        if (fechaMinima == null) {
                            fechaMinima = new Date(entry.fechaFin);
                        } else if (fechaMinima < new Date(entry.fechaFin)) {
                            fechaMinima = new Date(entry.fechaFin);
                        }
                    }
                });
                if (fechaMinima == null)
                    fechaMinima = new Date();
                $scope.configuracionFechaExpedicion = bow.fechas.configurarDatePicker(fechaMinima, new Date());
            };

            /********************************************************************
             * Funciones para eliminar un contacto de la lista
             ********************************************************************/
            vm.cancelarOpcionInfoTributaria = function ($index) {
                vm.mensajeCancelarOpcionInfoTributaria[$index] = true;
            };

            vm.cancelarOpcionInfoTributariaOk = function (id, infoTributariaId, infoTributariaOpcionId, infoTributariaNombre, tipoValor, valor, fechaInicio, fechaActualizacion, accion, $index) {
                //  Validamos si fue un registro Consultado de BD para agregarlo a la lista como modificado
                if (accion == "C") {
                    //  Validamos si el objeto fue modificado para eliminarlo de la lista
                    listaOpcionesInfoTributariaAgregados = listaOpcionesInfoTributariaAgregados.filter(function (el) {
                        return el.id !== id;
                    });

                    d = new Date();
                    var nuevaOpcionInfoTributaria =
                    {
                        id: id,
                        infoTributariaOpcionId: infoTributariaOpcionId,
                        infoTributariaId: infoTributariaId,
                        valor: valor,
                        fechaInicio: new Date(fechaInicio),
                        fechaFin: d.yyyymmdd(),
                        fechaActualizacion: new Date(fechaActualizacion),
                        accion: 'E'
                    };
                    listaOpcionesInfoTributariaAgregados.push(nuevaOpcionInfoTributaria);

                    listaOpcionesInfoTributariaTabla[$index].fechaFin = d.yyyymmdd();
                    listaOpcionesInfoTributariaTabla[$index].estadoActiva = false;
                    vm.empresaOrganizacion.listaOpcionesInfoTributaria = bow.tablas.paginar(listaOpcionesInfoTributariaTabla, 5);
                }
                else {
                    //  Filtramos el array para eliminar el objecto
                    listaOpcionesInfoTributariaTabla = listaOpcionesInfoTributariaTabla.filter(function (el) {
                        return el.idEliminar !== 'N' + infoTributariaOpcionId;
                    });
                    vm.empresaOrganizacion.listaOpcionesInfoTributaria = bow.tablas.paginar(listaOpcionesInfoTributariaTabla, 5);
                }

                var infoTributaria =
                    {
                        nombre: infoTributariaNombre,
                        tipoValor: tipoValor,
                        id: infoTributariaId
                    };

                //  Colocamos temporalmente el objeto en la lista desplegable
                vm.infoTributarias.push(infoTributaria);

                vm.mensajeCancelarOpcionInfoTributaria[$index] = false;
            };

            vm.cancelarOpcionInfoTributariaCancel = function ($index) {
                vm.mensajeCancelarOpcionInfoTributaria[$index] = false;
            }

            /********************************************************************
             * Llamado para agregar a la lista de opciones de info tributaria con la fecha de actualización
             ********************************************************************/
            vm.actualizarInformacionTributaria = function (id, infoTributariaId, infoTributariaOpcionId, valor, fechaInicio, $index) {
                if (vm.tablaCheckInfoActualizada[$index]) {
                    d = new Date();
                    var modificarOpcionInfoTributaria =
                    {
                        id: id,
                        infoTributariaOpcionId: infoTributariaOpcionId,
                        infoTributariaId: infoTributariaId,
                        valor: valor,
                        fechaInicio: new Date(fechaInicio),
                        fechaFin: null,
                        fechaActualizacion: d.ddmmyyyy(),
                        accion: 'M'
                    };
                    listaOpcionesInfoTributariaAgregados.push(modificarOpcionInfoTributaria);
                }
                else {
                    //  Filtramos el array para eliminar el objecto
                    listaOpcionesInfoTributariaAgregados = listaOpcionesInfoTributariaAgregados.filter(function (el) {
                        return el.id !== id;
                    });
                }
            };

            /****************************************************************************************************************
            ********************************************* Sucursales de la Empresa ******************************************
            *****************************************************************************************************************/

            ////Inicializando modelos
            vm.FormularioNuevaSucursal = false;
            vm.FormularioNuevoTelefonoSucursal = false;
            vm.PuedeEditarSucursal = false;
            vm.AccionFormularioSucursal = "Guardar";
            vm.modoConsultaSucursal = false;

            vm.controlOperacionesTelefonoSucursal = {};
            vm.tiposSucursales = [];
            vm.selectedTipoSucursal = "";
            vm.estadosSucursales = [];
            vm.selectedEstadoSucursal = "";

            var listaSucursalTelefonosTabla = [], listaSucursalTelefonosAgregados = [], listaSucursalTelefonosEliminados = [];
            
            /********************************************************************
             * Controles para eliminar la sucursal
             ********************************************************************/
            vm.controlesSucursales = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesSucursales.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesSucursales.visible[$index] = false;
                }
            };

            /********************************************************************
             * Controles para eliminar teléfonos de la sucursal
             ********************************************************************/
            vm.controlesTelefonoSucursal = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesTelefonoSucursal.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesTelefonoSucursal.visible[$index] = false;
                }
            };

            /************************************************************************
             * Llamado para mostrar el formulario de sucursales de la empresa
             ************************************************************************/
            vm.mostrarFormularioSucursal = function (mostrar) {
                vm.FormularioNuevaSucursal = mostrar;
                if (mostrar) {
                    //  Limpiamos la información del formulario
                    $scope.frmRegistrarSucursal.$setPristine();
                    vm.selectedTipoSucursal = "";
                    vm.selectedEstadoSucursal = "";
                    vm.empresaOrganizacion.sucursal.nombre = "";
                    vm.empresaOrganizacion.sucursal.direccion = "";
                    vm.empresaOrganizacion.sucursal.listaTelefonos = [];
                    vm.AccionFormularioSucursal = "Guardar";
                    vm.PuedeEditarSucursal = mostrar;

                    //  Limpiamos las listas locales
                    listaSucursalTelefonosTabla = [];
                    listaSucursalTelefonosAgregados = [];
                    listaSucursalTelefonosEliminados = [];

                    vm.FormularioNuevoTelefonoSucursal = false;
                }
            };

            /********************************************************************
             * Función encargada llamar el metodo guardar o actualizar segun la acción
             ********************************************************************/
            vm.guardarSucursalEmpresa = function () {
                if (vm.AccionFormularioSucursal == "Guardar")
                    guardarSucursalEmpresaOrganizacion();
                else
                    actualizarSucursalEmpresaOrganizacion();
            };

            /********************************************************************
             * Función para cargar el listado de sucursales de la empresa
             ********************************************************************/
            function cargarSucursalesEmpresasOrganizacion() {
                empresasService.getAllSucursalesEmpresa({ EmpresaOrganizacionId: vm.selectedEmpresa.id })
                    .success(function (data) {
                        vm.empresaOrganizacion.listaSucursales = bow.tablas.paginar(data.sucursales, 5);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            /********************************************************************
             * Función para cargar toda la lista de tipos de sucursales
             ********************************************************************/
            function cargarTiposSucursales() {
                parametrosService.getTiposSucursalEmpresa()
                    .success(function (data) {
                        vm.tiposSucursales = data.tipoSucursales;
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            /********************************************************************
             * Función para cargar toda la lista de estados de sucursales
             ********************************************************************/
            function cargarEstadosSucursales() {
                parametrosService.getEstadosSucursalEmpresa()
                    .success(function (data) {
                        vm.estadosSucursales = data.estadosSucursales;
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            /************************************************************************
             * Llamado para abrir Modal de la Dirección de la Sucursal
             ************************************************************************/
            vm.modalDireccionSucursalRegistrada = function (direccion) {
                if (direccion != "cancel") {
                    vm.empresaOrganizacion.sucursal.direccionId = direccion.id;
                    vm.empresaOrganizacion.sucursal.direccion = direccion.direccionCompleta;
                    abp.notify.success(abp.localization.localize('empresas_empresaOrganizacion_direccion_guardada', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                }
            };

            /********************************************************************
             * Función para guardar la sucursal de la empresa en la organización
             ********************************************************************/
            function guardarSucursalEmpresaOrganizacion() {
                //  Objeto para guardar la sucursal
                sucursalGuardar = {
                    empresaOrganizacionId: vm.selectedEmpresa.id,
                    nombre: vm.empresaOrganizacion.sucursal.nombre,
                    tipoId: vm.selectedTipoSucursal.id,
                    direccionId: vm.empresaOrganizacion.sucursal.direccionId,
                    estadoId: vm.selectedEstadoSucursal.id,
                };

                empresasService.saveSucursalEmpresa(sucursalGuardar)
                    .success(function (data) {
                        vm.FormularioNuevaSucursal = false;
                        $scope.mensajeErrorSucursal = "";
                        empresasService.updateSucursalEmpresaTelefono({ sucursalId: data.id, telefonos: listaSucursalTelefonosAgregados })
                            .success(function () {
                                abp.notify.success(abp.localization.localize('empresas_empresaOrganizacion_sucursal_guardada', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                                cargarSucursalesEmpresasOrganizacion();
                            }).error(function (error) {
                                $scope.mensajeErrorSucursal = error.message;
                            });

                    }).error(function (error) {
                        $scope.mensajeErrorSucursal = error.message;
                    });
            }

            /********************************************************************
            * Función para actualizar la empresa en la organización
            ********************************************************************/
            function actualizarSucursalEmpresaOrganizacion() {
                //  Agregamos el array de telefonos de la sucursal eliminados para enviarlos al backend y eliminarlos de la BD
                listaSucursalTelefonosAgregados = listaSucursalTelefonosAgregados.concat(listaSucursalTelefonosEliminados);

                sucursalGuardar = {
                    id: vm.empresaOrganizacion.sucursal.id,
                    empresaOrganizacionId: vm.selectedEmpresa.id,
                    nombre: vm.empresaOrganizacion.sucursal.nombre,
                    tipoId: vm.selectedTipoSucursal.id,
                    direccionId: vm.empresaOrganizacion.sucursal.direccionId,
                    estadoId: vm.selectedEstadoSucursal.id,
                };

                empresasService.updateSucursalEmpresa(sucursalGuardar)
                    .success(function () {
                        vm.FormularioNuevaSucursal = false;
                        $scope.mensajeErrorSucursal = "";

                        empresasService.updateSucursalEmpresaTelefono({ sucursalId: vm.empresaOrganizacion.sucursal.id, telefonos: listaSucursalTelefonosAgregados })
                            .success(function () {
                                abp.notify.success(abp.localization.localize('empresas_empresaOrganizacion_sucursal_actualizada', 'Bow'), abp.localization.localize('empresas_empresaOrganizacion_informacion', 'Bow'));
                                cargarSucursalesEmpresasOrganizacion();
                            }).error(function (error) {
                                $scope.mensajeErrorSucursal = error.message;
                            });

                    }).error(function (error) {
                        $scope.mensajeErrorSucursal = error.message;
                    });
            }

            /********************************************************************
            * Función para cargar la informacíón de una sucursal de una empresa asociada a la organización
            ********************************************************************/
            vm.cargarInformacionSucursalEmpresaOrganizacion = function (sucursalId, puedeEditar) {
                empresasService.getSucursalEmpresaOrganizacion({ EmpresaOrganizacionId: vm.selectedEmpresa.id, SucursalId: sucursalId })
                    .success(function (data) {
                        //  Limpiamos las listas locales
                        listaSucursalTelefonosTabla = [];
                        listaSucursalTelefonosAgregados = [];
                        listaSucursalTelefonosEliminados = [];

                        vm.FormularioNuevoTelefonoSucursal = false;

                        vm.PuedeEditarSucursal = puedeEditar;
                        vm.FormularioNuevaSucursal = true;

                        vm.empresaOrganizacion.sucursal.id = data.id;
                        vm.empresaOrganizacion.sucursal.nombre = data.nombre;
                        vm.empresaOrganizacion.sucursal.direccionId = data.direccionId;
                        vm.empresaOrganizacion.sucursal.direccion = data.direccion;

                        vm.selectedTipoSucursal = getObjectById(data.tipoId, vm.tiposSucursales);
                        vm.selectedEstadoSucursal = getObjectById(data.estadoId, vm.estadosSucursales);

                        listaSucursalTelefonosTabla = data.telefonos;
                        vm.empresaOrganizacion.sucursal.listaTelefonos = bow.tablas.paginar(listaSucursalTelefonosTabla, 5);

                        vm.AccionFormularioSucursal = "Modificar";
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
                if (puedeEditar) {

                }
            };
            
            /************************************************************************
             * Llamado para abrir formulario de Teléfono
             ************************************************************************/
            vm.mostrarNuevoTelefonoSucursal = function (mostrar) {
                vm.FormularioNuevoTelefonoSucursal = mostrar;
                if (mostrar) {
                    vm.controlOperacionesTelefonoSucursal.limpiarFormulario();
                };
            };

            /************************************************************************
            * Directiva para capturar el valor ingresado en telefono de la sucursal
            ************************************************************************/
            vm.notificacionTelefonoSucursalGuardado = function (telefono) {
                var nuevoTelefono =
                {
                    id: 0,
                    telefonoId: telefono.id,
                    telefonoNumero: telefono.telefonoCompleto,
                    nombreLocalidad: telefono.ubicacion,
                    accion: 'N'
                };

                //  Validamos si el objecto ya fue agregado al array
                if (JSON.stringify(listaSucursalTelefonosAgregados).indexOf(JSON.stringify({ id: 0, empresaId: 0, telefonoId: telefono.id, accion: "N" })) == -1) {
                    listaSucursalTelefonosAgregados.push({ id: 0, empresaId: 0, telefonoId: telefono.id, accion: "N" });
                    listaSucursalTelefonosTabla.push(nuevoTelefono);
                    vm.empresaOrganizacion.sucursal.listaTelefonos = bow.tablas.paginar(listaSucursalTelefonosTabla, 5);
                }
                vm.FormularioNuevoTelefonoSucursal = false;
            };

            /********************************************************************
            * Funciones para eliminar un teléfono de la lista de sucursales
            ********************************************************************/
            vm.eliminarTelefonoSucursal = function ($index) {
                vm.mensajeEliminarTelefonoSucursal[$index] = true;
            };

            vm.eliminarTelefonoSucursalOk = function (id, telefonoId, telefonoNumero, nombreLocalidad, accion, $index) {
                //  Validamos si el telefono eliminado fue consultado desde el backend para posteriormente eliminarlo
                if (accion == "C") {
                    //  Validamos si el objecto ya fue eliminado antes
                    if (JSON.stringify(listaSucursalTelefonosEliminados).indexOf(JSON.stringify({ id: id, empresaId: 0, telefonoId: telefonoId, accion: "E" })) == -1) {
                        listaSucursalTelefonosEliminados.push({ id: id, empresaId: 0, telefonoId: telefonoId, accion: "E" });
                    }
                }
                //  Filtramos el array para eliminar el objecto
                listaSucursalTelefonosTabla = listaSucursalTelefonosTabla.filter(function (el) {
                    return el.telefonoId !== telefonoId;
                });
                vm.empresaOrganizacion.sucursal.listaTelefonos = bow.tablas.paginar(listaSucursalTelefonosTabla, 5);

                //  Filtramos tambien el array de los telefonos agregados para la BD
                listaSucursalTelefonosAgregados = listaSucursalTelefonosAgregados.filter(function (el) {
                    return el.telefonoId !== telefonoId;
                });

                vm.mensajeEliminarTelefonoSucursal[$index] = false;
            };

            vm.eliminarTelefonoSucursalCancel = function ($index) {
                vm.mensajeEliminarTelefonoSucursal[$index] = false;
            };

            /************************************************************************
             * Función para obtener el elemento de una lista según el id
             ************************************************************************/
            function getObjectById(id, arrayList) {
                return arrayList.filter(function (obj) {
                    if (obj.id == id) {
                        return obj
                    }
                })[0]
            }

            /********************************************************************
             * Funciones para cambiar el formato de un tipo Date a string
             ********************************************************************/
            Date.prototype.ddmmyyyy = function () {
                var yyyy = this.getFullYear().toString();
                var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based         
                var dd = this.getDate().toString();

                return (dd[1] ? dd : "0" + dd[0]) + '/' + (mm[1] ? mm : "0" + mm[0]) + '/' + yyyy;
            };

            Date.prototype.yyyymmdd = function () {
                var yyyy = this.getFullYear().toString();
                var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based         
                var dd = this.getDate().toString();

                return yyyy + '/' + (mm[1] ? mm : "0" + mm[0]) + '/' + (dd[1] ? dd : "0" + dd[0]);
            };
        }]);
})();

