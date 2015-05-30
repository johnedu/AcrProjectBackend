(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.empresas.infoTributaria';

    /*****************************************************************
     * 
     * CONTROLADOR INFORMACIÓN TRIBUTARIA
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.empresas', 'abp.services.app.parametros', 'abp.services.app.zonificacion',
        function ($scope, $modal, empresasService, parametrosService, zonificacionService) {
            var vm = this;

            //----------------------------------------------------------------------------------------------------------//
            //--------------------------------------    Información Tributaria    --------------------------------------//
            //----------------------------------------------------------------------------------------------------------//

            ////Inicializando modelos
            vm.listaInfoTributarias = [];
            vm.editandoInfoTributaria = false;
            vm.eliminadoInfoTributaria = false;
            vm.administrandoOpcionesLocalidades = false;
            vm.infoTributariaId = '';
            vm.nombreInfoTributariaEditar = '';
            vm.nombreInfoTributariaOpciones = '';

            vm.infoTributaria = {
                id: '',
                nombre: '',
                tipoValorId: '',
                estadoId: true
            };

            /************************************************************************
             * Controles para editar o eliminar las informaciones tributarias
             ************************************************************************/
            vm.controles = {
                visible: [],
                mostrar: function ($index) {
                    vm.controles.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controles.visible[$index] = false;
                    vm.eliminadoInfoTributaria = false;
                }
            };

            /********************************************************
             * CONTROL PARA MOSTRAR OPCIÓN DE PARÁMETRO SELECCIONADA
             *******************************************************/
            vm.opcionParametro = [true, false];

            vm.opcionOpciones = function () {
                vm.opcionParametro = [true, false];
            }

            vm.opcionLocalidades = function () {
                vm.opcionParametro = [false, true];
            }

            ///********************************************************************
            // * Función encargada de consultar el listado de información tributaria en la base de datos
            // ********************************************************************/
            cargarInfoTributarias = function () {
                empresasService.getInfoTributarias().success(function (data) {
                    vm.listaInfoTributarias = bow.tablas.paginar(data.infoTributarias, 5);
                });
            };
            cargarInfoTributarias();

            /********************************************************************
             * Funciones para la directiva de eliminar una información tributaria
             ********************************************************************/
            vm.puedeEliminarInfoTributaria = function (infoTributariaId, funcionRetornarPuedeEliminar) {
                empresasService.puedeEliminarInfoTributaria({ Id: infoTributariaId }).success(function (data) {
                    vm.eliminadoInfoTributaria = data.puedeEliminar;
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('empresas_infoTributaria_mensaje_nosePuede_eliminar', 'Bow'), abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
            }

            vm.eliminarInfoTributaria = function (infoTributariaId) {
                empresasService.deleteInfoTributaria({ id: infoTributariaId })
                   .success(function (data) {
                       abp.notify.success(abp.localization.localize('empresas_infoTributaria_mensaje_eliminado', 'Bow'), abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
                       vm.eliminadoInfoTributaria = false;
                       cargarInfoTributarias();
                   }).error(function (error) {
                       if (error.validationErrors != null) {
                           abp.notify.error(error.validationErrors[0].message, abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
                       }
                       else {
                           abp.notify.error(error.message, abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
                       }
                   });
            }

            vm.cancelarEliminar = function () {
                vm.eliminadoInfoTributaria = false;
            };

            /************************************************************************
             * Llamado para editar una Información Tributaria
             ************************************************************************/
            vm.editarInfoTributaria = function (infoTributariaId) {
                empresasService.getInfoTributaria({ id: infoTributariaId }).success(function (data) {
                    vm.infoTributaria = data;
                    vm.infoTributariaId = infoTributariaId;
                    vm.nombreInfoTributariaEditar = data.nombre;
                    vm.editandoInfoTributaria = true;
                });
            }

            /************************************************************************
             * Función para guardar o actualizar una información tributaria según la bandera
             ************************************************************************/
            vm.saveOrUpdateInfoTributaria = function () {
                if (vm.infoTributaria.estadoId)
                    vm.infoTributaria.estadoId = "1";
                else
                    vm.infoTributaria.estadoId = "0";
                if (vm.editandoInfoTributaria) {
                    updateInfoTributaria();
                }
                else {
                    saveInfoTributaria();
                }
            }

            /************************************************************************
             * Función para guardar una información tributaria
             ************************************************************************/
            function saveInfoTributaria() {
                empresasService.saveInfoTributaria(vm.infoTributaria)
                    .success(function () {
                        if (vm.infoTributaria.id == vm.infoTributariaId) {
                            vm.cargarOpcionesInfoTributaria(vm.infoTributariaId, vm.infoTributaria.nombre);
                        }
                        abp.notify.success(abp.localization.localize('empresas_infoTributaria_mensaje_insertado', 'Bow') + ' ' + vm.infoTributaria.nombre, abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
                        vm.cancelarUpdateInfoTributaria();
                        cargarInfoTributarias();
                    }).error(function (error) {
                        abp.notify.error(error.message, abp.localization.localize('personas_tiposDocumento_error', 'Bow'));
                    });
            };

            /************************************************************************
             * Función para actualizar una información tributaria
             ************************************************************************/
            function updateInfoTributaria() {
                empresasService.updateInfoTributaria(vm.infoTributaria)
                    .success(function () {
                        if (vm.infoTributaria.id == vm.infoTributariaId) {
                            vm.cargarOpcionesInfoTributaria(vm.infoTributariaId, vm.infoTributaria.nombre);
                        }
                        abp.notify.success(abp.localization.localize('empresas_infoTributaria_mensaje_modificado', 'Bow') + ' ' + vm.infoTributaria.nombre, abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
                        vm.cancelarUpdateInfoTributaria();
                        cargarInfoTributarias();
                    }).error(function (error) {
                        abp.notify.error(error.message, abp.localization.localize('personas_tiposDocumento_error', 'Bow'));
                    });
            };

            /************************************************************************
             * Función para limpiar los datos del formulario de ingreso de información tributaria
             ************************************************************************/
            function limpiarFormulario() {
                vm.infoTributaria.id = '';
                vm.infoTributaria.nombre = '';
                vm.infoTributaria.tipoValorId = '';
                vm.infoTributaria.estadoId = true;
            }

            /************************************************************************
             * Función para cancelar la actualización de información tributaria
             ************************************************************************/
            vm.cancelarUpdateInfoTributaria = function () {
                limpiarFormulario();
                vm.nombreInfoTributariaEditar = '';
                vm.editandoInfoTributaria = false;
                $scope.frmInfoTributaria.$setPristine();
            }

            /********************************************************************
             * Funcion para cargar las opciones de la información tributaria seleccionada
             ********************************************************************/
            vm.cargarOpcionesInfoTributaria = function (infoTributariaId, infoTributariaNombre) {
                vm.infoTributariaId = infoTributariaId;
                vm.nombreInfoTributariaOpciones = infoTributariaNombre;
                empresasService.getInfoTributariaOpciones({ infoTributariaId: infoTributariaId })
                    .success(function (data) {
                        vm.listaOpcionesInfoTributaria = bow.tablas.paginar(data.infoTributariaOpciones, 5);
                        vm.administrandoOpcionesLocalidades = true;
                        cargarLocalidades();
                        vm.cargarLocalidadesLista(0, "", "I");
                    });
            }


            //----------------------------------------------------------------------------------------------------------//
            //--------------------------------    Opciones de Información Tributaria    --------------------------------//
            //----------------------------------------------------------------------------------------------------------//


            ////Inicializando modelos
            vm.listaOpcionesInfoTributaria = [];
            vm.verFormularioOpciones = false;
            vm.editandoOpcionInfoTributaria = false;
            vm.eliminandoOpcionInfoTributaria = false;

            vm.opcionInfoTributaria = {
                id: '',
                nombre: '',
                infoTributariaId: ''
            };

            /************************************************************************
             * Controles para editar o eliminar las informaciones tributarias
             ************************************************************************/
            vm.controlesOpciones = {
                visible: [],
                mostrar: function ($index) {
                    vm.controlesOpciones.visible[$index] = true;
                },
                ocultar: function ($index) {
                    vm.controlesOpciones.visible[$index] = false;
                    vm.eliminandoOpcionInfoTributaria = false;
                }
            };

            ///********************************************************************
            // * FUNCION PRIVADA DEL CONTROLADOR PARA CARGAR LISTA DE TIPOS DE VALOR DE INFORMACIÓN TRIBUTARIA PARA EL DROPDOWN
            // ********************************************************************/
            function cargarTiposValor() {
                parametrosService.getTiposInfoTributaria().success(function (data) {
                    vm.tiposValores = data.tipos;
                });
            }
            cargarTiposValor();

            /********************************************************************
             * Funciones para la directiva de eliminar opciones de información tributaria
             ********************************************************************/
            vm.puedeEliminarOpcionInfoTributaria = function (opcionInfoTributariaId, funcionRetornarPuedeEliminar) {
                empresasService.puedeEliminarInfoTributariaOpcion({ Id: opcionInfoTributariaId }).success(function (data) {
                    vm.eliminandoOpcionInfoTributaria = data.puedeEliminar;
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            vm.noPuedeOpcionEliminar = function () {
                abp.notify.error(abp.localization.localize('empresas_infoTributaria_opciones_notificacion_nosePuedeEliminar', 'Bow'), abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
            }

            vm.eliminarOpcionInfoTributaria = function (opcionInfoTributariaId, opcionInfoTributariaNombre) {
                empresasService.deleteInfoTributariaOpcion({ id: opcionInfoTributariaId })
                   .success(function (data) {
                       abp.notify.success(abp.localization.localize('empresas_infoTributaria_opciones_notificacionEliminado', 'Bow') + ' ' + opcionInfoTributariaNombre, abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
                       vm.eliminandoOpcionInfoTributaria = false;
                       vm.cargarOpcionesInfoTributaria(vm.infoTributariaId, vm.nombreInfoTributariaOpciones);
                   }).error(function (error) {
                       if (error.validationErrors != null) {
                           abp.notify.error(error.validationErrors[0].message, abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
                       }
                       else {
                           abp.notify.error(error.message, abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
                       }
                   });
            }

            vm.cancelarOpcionEliminar = function () {
                vm.eliminandoOpcionInfoTributaria = false;
            };

            /************************************************************************
             * Llamado para mostrar el formulario para agregar una nueva opción de información tributaria
             ************************************************************************/
            vm.nuevaOpcionInfoTributaria = function () {
                limpiarFormularioOpciones();
                vm.verFormularioOpciones = true;
            };

            /************************************************************************
             * Llamado para editar una Opción de Información Tributaria
             ************************************************************************/
            vm.editarOpcionInfoTributaria = function (opcionInfoTributariaId) {
                empresasService.getInfoTributariaOpcion({ id: opcionInfoTributariaId }).success(function (data) {
                    vm.opcionInfoTributaria = data;
                    vm.opcionInfoTributaria.infoTributariaId = vm.infoTributariaId;
                    vm.verFormularioOpciones = true;
                    vm.editandoOpcionInfoTributaria = true;
                    cargarLocalidades();
                    vm.cargarLocalidadesLista(0, "", "I");
                });
            }

            /************************************************************************
             * Función para guardar o actualizar una opción de información tributaria según la bandera
             ************************************************************************/
            vm.saveOrUpdateOpcionInfoTributaria = function () {
                if (vm.editandoOpcionInfoTributaria) {
                    updateOpcionInfoTributaria();
                }
                else {
                    saveOpcionInfoTributaria();
                }
            }

            /************************************************************************
             * Función para guardar una opción información tributaria
             ************************************************************************/
            function saveOpcionInfoTributaria() {
                vm.opcionInfoTributaria.infoTributariaId = vm.infoTributariaId;
                empresasService.saveInfoTributariaOpcion(vm.opcionInfoTributaria)
                    .success(function () {
                        abp.notify.success(abp.localization.localize('empresas_infoTributaria_opciones_notificacionInsertado', 'Bow') + ' ' + vm.opcionInfoTributaria.nombre, abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
                        vm.cancelarUpdateOpcionesInfoTributaria(false);
                        vm.cargarOpcionesInfoTributaria(vm.infoTributariaId, vm.nombreInfoTributariaOpciones);
                    }).error(function (error) {
                        abp.notify.error(error.message, abp.localization.localize('personas_tiposDocumento_error', 'Bow'));
                    });
            };

            /************************************************************************
             * Función para actualizar una opcion de información tributaria
             ************************************************************************/
            function updateOpcionInfoTributaria() {
                empresasService.updateInfoTributariaOpcion(vm.opcionInfoTributaria)
                    .success(function () {
                        abp.notify.success(abp.localization.localize('empresas_infoTributaria_opciones_notificacionModificado', 'Bow') + ' ' + vm.opcionInfoTributaria.nombre, abp.localization.localize('empresas_infoTributaria_mensaje_informacion', 'Bow'));
                        vm.cancelarUpdateOpcionesInfoTributaria(false);
                        vm.cargarOpcionesInfoTributaria(vm.infoTributariaId, vm.nombreInfoTributariaOpciones);
                    }).error(function (error) {
                        abp.notify.error(error.message, abp.localization.localize('personas_tiposDocumento_error', 'Bow'));
                    });
            };

            /************************************************************************
             * Función para limpiar los datos del formulario de ingreso de opciones de información tributaria
             ************************************************************************/
            function limpiarFormularioOpciones() {
                vm.opcionInfoTributaria = {
                    id: '',
                    nombre: '',
                    infoTributariaId: ''
                };
            }

            /************************************************************************
             * Función para cancelar la actualización opciones de información tributaria
             ************************************************************************/
            vm.cancelarUpdateOpcionesInfoTributaria = function (soloLimpiar) {
                limpiarFormularioOpciones();
                vm.formOpcionInfoTributaria.$setPristine();
                if (!soloLimpiar) {
                    vm.editandoOpcionInfoTributaria = false;
                    vm.verFormularioOpciones = false;
                }
            }


            //-------------------------------------------------------------------------------------------------------------//
            //--------------------------------    Localidades de Información Tributaria    --------------------------------//
            //-------------------------------------------------------------------------------------------------------------//


            ////Inicializando modelos

            vm.localidades = [];
            vm.localidadesAsignarOrEliminar = [];
            vm.listaLocalidadesTabla = [];
            vm.listaLocalidadesAsignarTabla = [];
            vm.mostrarLocalidadesLista = true;
            vm.mostrarFormulario = "Asignar";
            vm.confirmacionAsignar = false;
            vm.confirmacionEliminar = false;
            vm.opcionBusqueda = "P";

            function cargarLocalidades() {
                empresasService.getAllLocalidadesByInfoTributaria({ infoTributariaId: vm.infoTributariaId }).success(function (data) {
                    var uniquePaises = {};
                    var uniqueDeptos = {};
                    var distinctPaises = [];
                    var distinctDeptos = [];
                    var listLocalidades = [];
                    for (var i in data.infoTributariaLocalidades) {
                        if (typeof (uniquePaises[data.infoTributariaLocalidades[i].pais]) == "undefined") {
                            distinctPaises.push({ id: data.infoTributariaLocalidades[i].paisId, nombre: data.infoTributariaLocalidades[i].pais, tipoEntrada: "P" });
                        }
                        uniquePaises[data.infoTributariaLocalidades[i].pais] = 0;
                        if (typeof (uniqueDeptos[data.infoTributariaLocalidades[i].departamento]) == "undefined") {
                            distinctDeptos.push({ id: data.infoTributariaLocalidades[i].departamentoId, nombre: data.infoTributariaLocalidades[i].departamento + " (" + data.infoTributariaLocalidades[i].pais + ")", tipoEntrada: "D" });
                        }
                        uniqueDeptos[data.infoTributariaLocalidades[i].departamento] = 0;
                        listLocalidades.push({ id: data.infoTributariaLocalidades[i].localidadId, nombre: data.infoTributariaLocalidades[i].localidad + " (" + data.infoTributariaLocalidades[i].departamento + ", " + data.infoTributariaLocalidades[i].pais + ")", tipoEntrada: "L" });
                    }

                    vm.localidades = distinctPaises.concat(distinctDeptos).concat(listLocalidades);
                });
            };
            

            ///********************************************************************
            // * Función encargada de consultar las localidades según el filtro desde la base de datos
            // ********************************************************************/
            vm.cargarLocalidadesLista = function (Id, Nombre, TipoEntrada) {
                if (TipoEntrada == "I") {
                    empresasService.getAllLocalidadesByInfoTributaria({ infoTributariaId: vm.infoTributariaId }).success(function (data) {
                        var listLocalidades = [];
                        for (var i in data.infoTributariaLocalidades) {
                            listLocalidades.push({ id: data.infoTributariaLocalidades[i].localidadId, nombre: data.infoTributariaLocalidades[i].localidad + " (" + data.infoTributariaLocalidades[i].departamento + ", " + data.infoTributariaLocalidades[i].pais + ")", tipoEntrada: "L" });
                        }
                        vm.listaLocalidadesTabla = bow.tablas.paginar(listLocalidades, 5);
                    });
                }
                else if (TipoEntrada == "P") {
                    empresasService.getAllLocalidadesByInfoTributariaAndPais({ infoTributariaId: vm.infoTributariaId, PaisId: Id }).success(function (data) {
                        vm.listaLocalidadesTabla = bow.tablas.paginar(data.infoTributariaLocalidades, 5);
                    });
                }
                else if (TipoEntrada == "D") {
                    empresasService.getAllLocalidadesByInfoTributariaAndDepartamento({ infoTributariaId: vm.infoTributariaId, DepartamentoId: Id }).success(function (data) {
                        vm.listaLocalidadesTabla = bow.tablas.paginar(data.infoTributariaLocalidades, 5);
                    });
                }
                else {
                    vm.listaLocalidadesTabla = bow.tablas.paginar([{ id: Id, nombre: Nombre }], 5);
                }
            };

            vm.filterByLocalidadAndDepartamentoAndPais = function (localidades, typedValue) {
                return localidades.filter(function (localidad) {
                    var nombre = localidad.nombre.toLowerCase();
                    var busqueda = typedValue.toLowerCase();

                    matches_nombre = nombre.indexOf(busqueda) != -1;

                    return matches_nombre;
                });
            }

            vm.filterByLocalidadAndDepartamentoAndPaisAsignarOrEliminar = function (localidadesAsignarOrEliminar, typedValue) {
                return localidadesAsignarOrEliminar.filter(function (localidad) {

                    var nombre = localidad.nombre.toLowerCase();
                    var busqueda = typedValue.toLowerCase();

                    matches_nombre = nombre.indexOf(busqueda) != -1;

                    return matches_nombre;
                });
            }

            vm.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }

            ///********************************************************************
            // * Función encargada de consultar la lista de paises, departamentos o 
            //   localidades para asignarlos a la info tributaria
            // ********************************************************************/
            function cargarLocalidadesAsignar(TipoEntrada) {
                if (TipoEntrada == "P") {
                    zonificacionService.getPaises().success(function (data) {
                        vm.localidadesAsignarOrEliminar = data.paises;
                    });
                }
                else if (TipoEntrada == "D") {
                    zonificacionService.getAllDepartamentos().success(function (data) {
                        vm.localidadesAsignarOrEliminar = data.departamentos;
                    });
                }
                else {
                    empresasService.getAllLocalidadesByNotInfoTributaria({ infoTributariaId: vm.infoTributariaId }).success(function (data) {
                        vm.localidadesAsignarOrEliminar = data.infoTributariaLocalidades;
                    });
                }
            };

            ///********************************************************************
            // * Función encargada de consultar la lista de paises, departamentos o 
            //   localidades para eliminarlos de la info tributaria
            // ********************************************************************/
            function cargarLocalidadesEliminar(TipoEntrada) {
                if (TipoEntrada == "P") {
                    empresasService.getAllPaisesByInfoTributaria({ infoTributariaId: vm.infoTributariaId }).success(function (data) {
                        vm.localidadesAsignarOrEliminar = data.infoTributariaPaises;
                    });
                }
                else if (TipoEntrada == "D") {
                    empresasService.getAllDepartamentosByInfoTributaria({ infoTributariaId: vm.infoTributariaId }).success(function (data) {
                        vm.localidadesAsignarOrEliminar = data.infoTributariaDepartamentos;
                    });
                }
                else {
                    empresasService.getAllLocalidadesByInfoTributaria({ infoTributariaId: vm.infoTributariaId }).success(function (data) {
                        var listLocalidades = [];
                        for (var i in data.infoTributariaLocalidades) {
                            listLocalidades.push({ id: data.infoTributariaLocalidades[i].localidadId, nombre: data.infoTributariaLocalidades[i].localidad + " (" + data.infoTributariaLocalidades[i].departamento + ", " + data.infoTributariaLocalidades[i].pais + ")" });
                        }
                        vm.localidadesAsignarOrEliminar = listLocalidades;
                    });
                }
            };

            ///********************************************************************
            // * Función encargada de mostrar el formulario de asignar localidad
            // ********************************************************************/
            vm.mostrarFormularioAsignarLocalidad = function () {
                vm.mostrarLocalidadesLista = false;
                vm.confirmacionAsignar = false;
                vm.mostrarFormulario = "Asignar";
                vm.opcionBusqueda = "P";
                //  Limpiamos el formulario para que no se muestren las clases 'has-error'
                vm.formAsignarOrEliminarLocalidad.$setPristine();
                vm.selectedAsignarOrEliminar = "";
                cargarLocalidadesAsignar(vm.opcionBusqueda);
            };

            ///********************************************************************
            // * Función encargada de mostrar el formulario de eliminar localidad
            // ********************************************************************/
            vm.mostrarFormularioEliminarLocalidad = function () {
                vm.mostrarLocalidadesLista = false;
                vm.confirmacionEliminar = false;
                vm.mostrarFormulario = "Eliminar";
                vm.opcionBusqueda = "P";
                //  Limpiamos el formulario para que no se muestren las clases 'has-error'
                vm.formAsignarOrEliminarLocalidad.$setPristine();
                vm.selectedAsignarOrEliminar = "";
                cargarLocalidadesEliminar(vm.opcionBusqueda);
            };

            ///********************************************************************
            // * Función encargada de asignar la localidad si seleccionó una sola localidad
            //   o mensaje de confirmación si seleccionó pais o departamento
            // ********************************************************************/
            vm.asignarLocalidad = function () {
                if (vm.opcionBusqueda == "L") {
                    empresasService.saveInfoTributariaLocalidad({ infoTributariaId: vm.infoTributariaId, localidadId: vm.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionLocalidadAsignada', 'Bow') + " " + vm.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_localidades_notificacionInformacion', 'Bow'));
                        vm.confirmacionAsignar = false;
                        vm.cancelFormulario();
                    }).error(function (error) {
                        vm.mensajeError = error.message;
                    });
                }
                else {
                    vm.confirmacionAsignar = true;
                }
            };

            ///********************************************************************
            // * Función encargada de asignar localidades de un pais o departamento seleccionado
            // ********************************************************************/
            vm.mostrarConfirmacionAsignar = function () {
                if (vm.opcionBusqueda == "P") {
                    empresasService.saveInfoTributariaLocalidadByPais({ infoTributariaId: vm.infoTributariaId, paisId: vm.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionPaisAsignado', 'Bow') + " " + vm.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_opciones_mensaje_informacion', 'Bow'));
                        vm.confirmacionAsignar = false;
                        vm.cancelFormulario();
                    }).error(function (error) {
                        vm.mensajeError = error.message;
                    });
                }
                else if (vm.opcionBusqueda == "D") {
                    empresasService.saveInfoTributariaLocalidadByDepartamento({ infoTributariaId: vm.infoTributariaId, departamentoId: vm.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionDepartamentoAsignado', 'Bow') + " " + vm.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_opciones_mensaje_informacion', 'Bow'));
                        vm.confirmacionAsignar = false;
                        vm.cancelFormulario();
                    }).error(function (error) {
                        vm.mensajeError = error.message;
                    });
                }
            };

            ///********************************************************************
            // * Función encargada de eliminar la localidad si seleccionó una sola localidad
            //   o mensaje de confirmación si seleccionó pais o departamento
            // ********************************************************************/
            vm.eliminarLocalidad = function () {
                if (vm.opcionBusqueda == "L") {
                    empresasService.deleteInfoTributariaLocalidad({ infoTributariaId: vm.infoTributariaId, localidadId: vm.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionLocalidadEliminada', 'Bow') + " " + vm.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_opciones_mensaje_informacion', 'Bow'));
                        vm.confirmacionEliminar = false;
                        vm.cancelFormulario();
                    }).error(function (error) {
                        vm.mensajeError = error.message;
                    });
                }
                else {
                    vm.confirmacionEliminar = true;
                }

            };

            ///********************************************************************
            // * Función encargada de eliminar localidades de un pais o departamento seleccionado
            // ********************************************************************/
            vm.mostrarConfirmacionEliminar = function () {
                if (vm.opcionBusqueda == "P") {
                    empresasService.deleteInfoTributariaLocalidadByPais({ infoTributariaId: vm.infoTributariaId, paisId: vm.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionPaisEliminado', 'Bow') + " " + vm.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_opciones_mensaje_informacion', 'Bow'));
                        vm.confirmacionEliminar = false;
                        vm.cancelFormulario();
                    }).error(function (error) {
                        vm.mensajeError = error.message;
                    });
                }
                else if (vm.opcionBusqueda == "D") {
                    empresasService.deleteInfoTributariaLocalidadByDepartamento({ infoTributariaId: vm.infoTributariaId, departamentoId: vm.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionDepartamentoEliminado', 'Bow') + " " + vm.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_opciones_mensaje_informacion', 'Bow'));
                        vm.confirmacionEliminar = false;
                        vm.cancelFormulario();
                    }).error(function (error) {
                        vm.mensajeError = error.message;
                    });
                }
            };

            ///********************************************************************
            // * Función encargada ocultar el div de confirmación de asignar localidades
            // ********************************************************************/
            vm.asignarCancel = function () {
                vm.confirmacionAsignar = false;
            };

            ///********************************************************************
            // * Función encargada ocultar el div de confirmación de eliminar localidades
            // ********************************************************************/
            vm.eliminarCancel = function () {
                vm.confirmacionEliminar = false;
            };

            ///********************************************************************
            // * Función encargada mostrar el listado de localidades de la información tributaria
            // ********************************************************************/
            vm.cancelFormulario = function () {
                //  Limpiamos el formulario para que no se muestren las clases 'has-error'
                vm.formLocalidad.$setPristine();
                vm.selected = "";
                vm.mostrarLocalidadesLista = true;
                cargarLocalidades();
                vm.cargarLocalidadesLista(0, "", "I");
            };

            ///********************************************************************
            // * Función encargada de cambiar la lista para filtrar la asignación o eliminación de localidades
            // ********************************************************************/
            vm.cambiarListaFiltro = function () {
                if (vm.mostrarFormulario == "Asignar") {
                    cargarLocalidadesAsignar(vm.opcionBusqueda);
                }
                else {
                    cargarLocalidadesEliminar(vm.opcionBusqueda);
                }
                vm.formAsignarOrEliminarLocalidad.$setPristine();
                vm.selectedAsignarOrEliminar = "";
            };

            ///********************************************************************
            // * Función encargada de cargar todas las localidades si se borra la selección del typeahead
            // 
            vm.validarTypeaheadVacio = function (id) {
                if (id == undefined) {
                    //  Cargamos la tabla inicial con todas las localidades ya que se borró la selección del typeahead
                    vm.cargarLocalidadesLista(0, "", "I");
                }
            };

            ///********************************************************************
            // * Función encargada de cargar las localidades del pais o departamento a asignar o eliminar
            // ********************************************************************/
            $scope.onSelect = function ($item, $model, $label) {
                if (vm.mostrarFormulario == "Asignar") {
                    if (vm.opcionBusqueda == "P") {
                        empresasService.getAllLocalidadesByNotInfoTributariaPais({ infoTributariaId: vm.infoTributariaId, paisId: $model.id })
                        .success(function (data) {
                            vm.listaLocalidadesAsignarTabla = bow.tablas.paginar(data.infoTributariaLocalidades, 5);
                        }).error(function (error) {
                            vm.mensajeError = error.message;
                        });
                    }
                    else if (vm.opcionBusqueda == "D") {
                        empresasService.getAllLocalidadesByNotInfoTributariaDepartamento({ infoTributariaId: vm.infoTributariaId, DepartamentoId: $model.id })
                        .success(function (data) {
                            vm.listaLocalidadesAsignarTabla = bow.tablas.paginar(data.infoTributariaLocalidades, 5);
                        }).error(function (error) {
                            vm.mensajeError = error.message;
                        });
                    }
                }
                else {
                    if (vm.opcionBusqueda == "P") {
                        empresasService.getAllLocalidadesByInfoTributariaAndPais({ infoTributariaId: vm.infoTributariaId, paisId: $model.id })
                        .success(function (data) {
                            vm.listaLocalidadesAsignarTabla = bow.tablas.paginar(data.infoTributariaLocalidades, 5);
                        }).error(function (error) {
                            vm.mensajeError = error.message;
                        });
                    }
                    else if (vm.opcionBusqueda == "D") {
                        empresasService.getAllLocalidadesByInfoTributariaAndDepartamento({ infoTributariaId: vm.infoTributariaId, departamentoId: $model.id })
                        .success(function (data) {
                            vm.listaLocalidadesAsignarTabla = bow.tablas.paginar(data.infoTributariaLocalidades, 5);
                        }).error(function (error) {
                            vm.mensajeError = error.message;
                        });
                    }
                }
                if (vm.opcionBusqueda == "L") {
                    vm.listaLocalidadesAsignarTabla = bow.tablas.paginar([{ id: $model.id, nombre: $model.nombre }], 5);
                }
            };

        }]);
})();