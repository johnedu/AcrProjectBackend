(function () {
    angular.module('app').controller('modalLocalidadesConvenioRecaudoController', ['$scope', '$modalInstance', 'convenioRecaudoId', 'convenioRecaudoNombre', 'abp.services.app.afiliaciones', 'abp.services.app.zonificacion',
        function ($scope, $modalInstance, convenioRecaudoId, convenioRecaudoNombre, afiliacionesService, zonificacionService) {

            ////Inicializando modelos

            $scope.localidades = [];
            $scope.localidadesAsignarOrEliminar = [];
            $scope.listaLocalidadesTabla = [];
            $scope.listaLocalidadesAsignarTabla = [];
            $scope.mostrarLocalidadesLista = true;
            $scope.mostrarFormulario = "Asignar";
            $scope.confirmacionAsignar = false;
            $scope.confirmacionEliminar = false;
            $scope.opcionBusqueda = "P";
            $scope.nombreConvenioRecaudo = convenioRecaudoNombre;

            function cargarLocalidades() {
                afiliacionesService.getAllLocalidadesByConvenio({ recaudoMasivoId: convenioRecaudoId }).success(function (data) {
                    var uniquePaises = {};
                    var uniqueDeptos = {};
                    var distinctPaises = [];
                    var distinctDeptos = [];
                    var listLocalidades = [];
                    for (var i in data.recaudoMasivoLocalidades) {
                        if (typeof (uniquePaises[data.recaudoMasivoLocalidades[i].pais]) == "undefined") {
                            distinctPaises.push({ id: data.recaudoMasivoLocalidades[i].paisId, nombre: data.recaudoMasivoLocalidades[i].pais, tipoEntrada: "P" });
                        }
                        uniquePaises[data.recaudoMasivoLocalidades[i].pais] = 0;
                        if (typeof (uniqueDeptos[data.recaudoMasivoLocalidades[i].departamento]) == "undefined") {
                            distinctDeptos.push({ id: data.recaudoMasivoLocalidades[i].departamentoId, nombre: data.recaudoMasivoLocalidades[i].departamento + " (" + data.recaudoMasivoLocalidades[i].pais + ")", tipoEntrada: "D" });
                        }
                        uniqueDeptos[data.recaudoMasivoLocalidades[i].departamento] = 0;
                        listLocalidades.push({ id: data.recaudoMasivoLocalidades[i].localidadId, nombre: data.recaudoMasivoLocalidades[i].localidad + " (" + data.recaudoMasivoLocalidades[i].departamento + ", " + data.recaudoMasivoLocalidades[i].pais + ")", tipoEntrada: "L" });
                    }

                    $scope.localidades = distinctPaises.concat(distinctDeptos).concat(listLocalidades);
                });
            };
            cargarLocalidades();

            ///********************************************************************
            // * Función encargada de consultar las localidades según el filtro desde la base de datos
            // ********************************************************************/
            $scope.cargarLocalidadesLista = function (Id, Nombre, TipoEntrada) {
                if (TipoEntrada == "I") {
                    afiliacionesService.getAllLocalidadesByConvenio({ recaudoMasivoId: convenioRecaudoId }).success(function (data) {
                        var listLocalidades = [];
                        for (var i in data.recaudoMasivoLocalidades) {
                            listLocalidades.push({ id: data.recaudoMasivoLocalidades[i].localidadId, nombre: data.recaudoMasivoLocalidades[i].localidad + " (" + data.recaudoMasivoLocalidades[i].departamento + ", " + data.recaudoMasivoLocalidades[i].pais + ")", tipoEntrada: "L" });
                        }
                        $scope.listaLocalidadesTabla = bow.tablas.paginar(listLocalidades, 5);
                    });
                }
                else if (TipoEntrada == "P") {
                    afiliacionesService.getAllLocalidadesByConvenioAndPais({ RecaudoMasivoId: convenioRecaudoId, PaisId: Id }).success(function (data) {
                        $scope.listaLocalidadesTabla = bow.tablas.paginar(data.recaudoMasivoLocalidades, 5);
                    });
                }
                else if (TipoEntrada == "D") {
                    afiliacionesService.getAllLocalidadesByConvenioAndDepartamento({ RecaudoMasivoId: convenioRecaudoId, DepartamentoId: Id }).success(function (data) {
                        $scope.listaLocalidadesTabla = bow.tablas.paginar(data.recaudoMasivoLocalidades, 5);
                    });
                }
                else {
                    $scope.listaLocalidadesTabla = bow.tablas.paginar([{ id: Id, nombre: Nombre }], 5);
                }
            };
            //  Cargamos la tabla inicial con todas las localidades
            $scope.cargarLocalidadesLista(0, "", "I");

            $scope.filterByLocalidadAndDepartamentoAndPais = function (localidades, typedValue) {
                return localidades.filter(function (localidad) {
                    var nombre = localidad.nombre.toLowerCase();
                    var busqueda = typedValue.toLowerCase();

                    matches_nombre = nombre.indexOf(busqueda) != -1;

                    return matches_nombre;
                });
            }

            $scope.filterByLocalidadAndDepartamentoAndPaisAsignarOrEliminar = function (localidadesAsignarOrEliminar, typedValue) {
                return localidadesAsignarOrEliminar.filter(function (localidad) {

                    var nombre = localidad.nombre.toLowerCase();
                    var busqueda = typedValue.toLowerCase();

                    matches_nombre = nombre.indexOf(busqueda) != -1;

                    return matches_nombre;
                });
            }

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }

            ///********************************************************************
            // * Función encargada de consultar la lista de paises, departamentos o 
            //   localidades para asignarlos a un recaudo masivo
            // ********************************************************************/
            function cargarLocalidadesAsignar(TipoEntrada) {
                if (TipoEntrada == "P") {
                    zonificacionService.getPaises().success(function (data) {
                        $scope.localidadesAsignarOrEliminar = data.paises;
                    });
                }
                else if (TipoEntrada == "D") {
                    zonificacionService.getAllDepartamentos().success(function (data) {
                        $scope.localidadesAsignarOrEliminar = data.departamentos;
                    });
                }
                else {
                    afiliacionesService.getAllLocalidadesByNotConvenio({ recaudoMasivoId: convenioRecaudoId }).success(function (data) {
                        $scope.localidadesAsignarOrEliminar = data.recaudoMasivoLocalidades;
                    });
                }
            };

            ///********************************************************************
            // * Función encargada de consultar la lista de paises, departamentos o 
            //   localidades para eliminarlos del recaudo masivo
            // ********************************************************************/
            function cargarLocalidadesEliminar(TipoEntrada) {
                if (TipoEntrada == "P") {
                    afiliacionesService.getAllPaisesByConvenio({ recaudoMasivoId: convenioRecaudoId }).success(function (data) {
                        $scope.localidadesAsignarOrEliminar = data.recaudoMasivoPaises;
                    });
                }
                else if (TipoEntrada == "D") {
                    afiliacionesService.getAllDepartamentosByConvenio({ recaudoMasivoId: convenioRecaudoId }).success(function (data) {
                        $scope.localidadesAsignarOrEliminar = data.recaudoMasivoDepartamentos;
                    });
                }
                else {
                    afiliacionesService.getAllLocalidadesByConvenio({ recaudoMasivoId: convenioRecaudoId }).success(function (data) {
                        var listLocalidades = [];
                        for (var i in data.recaudoMasivoLocalidades) {
                            listLocalidades.push({ id: data.recaudoMasivoLocalidades[i].localidadId, nombre: data.recaudoMasivoLocalidades[i].localidad + " (" + data.recaudoMasivoLocalidades[i].departamento + ", " + data.recaudoMasivoLocalidades[i].pais + ")" });
                        }
                        $scope.localidadesAsignarOrEliminar = listLocalidades;
                    });
                }
            };

            ///********************************************************************
            // * Función encargada de mostrar el formulario de asignar localidad
            // ********************************************************************/
            $scope.mostrarFormularioAsignarLocalidad = function () {
                $scope.mostrarLocalidadesLista = false;
                $scope.confirmacionAsignar = false;
                $scope.mostrarFormulario = "Asignar";
                $scope.opcionBusqueda = "P";
                //  Limpiamos el formulario para que no se muestren las clases 'has-error'
                $scope.formAsignarOrEliminarLocalidad.$setPristine();
                $scope.selectedAsignarOrEliminar = "";
                cargarLocalidadesAsignar($scope.opcionBusqueda);
            };

            ///********************************************************************
            // * Función encargada de mostrar el formulario de eliminar localidad
            // ********************************************************************/
            $scope.mostrarFormularioEliminarLocalidad = function () {
                $scope.mostrarLocalidadesLista = false;
                $scope.confirmacionEliminar = false;
                $scope.mostrarFormulario = "Eliminar";
                $scope.opcionBusqueda = "P";
                //  Limpiamos el formulario para que no se muestren las clases 'has-error'
                $scope.formAsignarOrEliminarLocalidad.$setPristine();
                $scope.selectedAsignarOrEliminar = "";
                cargarLocalidadesEliminar($scope.opcionBusqueda);
            };

            ///********************************************************************
            // * Función encargada de asignar la localidad si seleccionó una sola localidad
            //   o mensaje de confirmación si seleccionó pais o departamento
            // ********************************************************************/
            $scope.asignarLocalidad = function () {
                if ($scope.opcionBusqueda == "L") {
                    afiliacionesService.saveConvenioLocalidad({ recaudoMasivoId: convenioRecaudoId, localidadId: $scope.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionLocalidadAsignada', 'Bow') + " " + $scope.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_localidades_notificacionInformacion', 'Bow'));
                        $scope.confirmacionAsignar = false;
                        $scope.cancelFormulario();
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
                }
                else {
                    $scope.confirmacionAsignar = true;
                }
            };

            ///********************************************************************
            // * Función encargada de asignar localidades de un pais o departamento seleccionado
            // ********************************************************************/
            $scope.mostrarConfirmacionAsignar = function () {
                if ($scope.opcionBusqueda == "P") {
                    afiliacionesService.saveConveniosLocalidadByPais({ recaudoMasivoId: convenioRecaudoId, paisId: $scope.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionPaisAsignado', 'Bow') + " " + $scope.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_opciones_mensaje_informacion', 'Bow'));
                        $scope.confirmacionAsignar = false;
                        $scope.cancelFormulario();
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
                }
                else if ($scope.opcionBusqueda == "D") {
                    afiliacionesService.saveConveniosLocalidadByDepartamento({ recaudoMasivoId: convenioRecaudoId, departamentoId: $scope.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionDepartamentoAsignado', 'Bow') + " " + $scope.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_opciones_mensaje_informacion', 'Bow'));
                        $scope.confirmacionAsignar = false;
                        $scope.cancelFormulario();
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
                }
            };

            ///********************************************************************
            // * Función encargada de eliminar la localidad si seleccionó una sola localidad
            //   o mensaje de confirmación si seleccionó pais o departamento
            // ********************************************************************/
            $scope.eliminarLocalidad = function () {
                $scope.confirmacionEliminar = true;
            };

            ///********************************************************************
            // * Función encargada de eliminar localidades de un pais o departamento seleccionado
            // ********************************************************************/
            $scope.mostrarConfirmacionEliminar = function () {
                if ($scope.opcionBusqueda == "L") {
                    afiliacionesService.deleteConvenioLocalidad({ recaudoMasivoId: convenioRecaudoId, localidadId: $scope.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionLocalidadEliminada', 'Bow') + " " + $scope.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_opciones_mensaje_informacion', 'Bow'));
                        $scope.confirmacionEliminar = false;
                        $scope.cancelFormulario();
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
                }
                else if ($scope.opcionBusqueda == "P") {
                    afiliacionesService.deleteConveniosLocalidadByPais({ recaudoMasivoId: convenioRecaudoId, paisId: $scope.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionPaisEliminado', 'Bow') + " " + $scope.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_opciones_mensaje_informacion', 'Bow'));
                        $scope.confirmacionEliminar = false;
                        $scope.cancelFormulario();
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
                }
                else if ($scope.opcionBusqueda == "D") {
                    afiliacionesService.deleteConveniosLocalidadByDepartamento({ recaudoMasivoId: convenioRecaudoId, departamentoId: $scope.selectedAsignarOrEliminar.id })
                    .success(function () {
                        abp.notify.info(abp.localization.localize('empresas_infoTributaria_localidades_notificacionDepartamentoEliminado', 'Bow') + " " + $scope.selectedAsignarOrEliminar.nombre, abp.localization.localize('empresas_infoTributaria_opciones_mensaje_informacion', 'Bow'));
                        $scope.confirmacionEliminar = false;
                        $scope.cancelFormulario();
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
                }
            };

            ///********************************************************************
            // * Función encargada ocultar el div de confirmación de asignar localidades
            // ********************************************************************/
            $scope.asignarCancel = function () {
                $scope.confirmacionAsignar = false;
            };

            ///********************************************************************
            // * Función encargada ocultar el div de confirmación de eliminar localidades
            // ********************************************************************/
            $scope.eliminarCancel = function () {
                $scope.confirmacionEliminar = false;
            };

            ///********************************************************************
            // * Función encargada mostrar el listado de localidades de la información tributaria
            // ********************************************************************/
            $scope.cancelFormulario = function () {
                //  Limpiamos el formulario para que no se muestren las clases 'has-error'
                $scope.formLocalidad.$setPristine();
                $scope.selected = "";
                $scope.mostrarLocalidadesLista = true;
                cargarLocalidades();
                $scope.cargarLocalidadesLista(0, "", "I");
            };

            ///********************************************************************
            // * Función encargada de cambiar la lista para filtrar la asignación o eliminación de localidades
            // ********************************************************************/
            $scope.cambiarListaFiltro = function () {
                if ($scope.mostrarFormulario == "Asignar") {
                    cargarLocalidadesAsignar($scope.opcionBusqueda);
                }
                else {
                    cargarLocalidadesEliminar($scope.opcionBusqueda);
                }
                $scope.formAsignarOrEliminarLocalidad.$setPristine();
                $scope.selectedAsignarOrEliminar = "";
            };

            ///********************************************************************
            // * Función encargada de cargar todas las localidades si se borra la selección del typeahead
            // 
            $scope.validarTypeaheadVacio = function (id) {
                if (id == undefined) {
                    //  Cargamos la tabla inicial con todas las localidades ya que se borró la selección del typeahead
                    $scope.cargarLocalidadesLista(0, "", "I");
                }
            };

            ///********************************************************************
            // * Función encargada de cargar las localidades del pais o departamento a asignar o eliminar
            // ********************************************************************/
            $scope.cargarLocalidadesListaAsignarEliminar = function (Id, Nombre) {
                if ($scope.mostrarFormulario == "Asignar") {
                    if ($scope.opcionBusqueda == "P") {
                        afiliacionesService.getAllLocalidadesByNotConvenioAndPais({ recaudoMasivoId: convenioRecaudoId, paisId: Id })
                        .success(function (data) {
                            $scope.listaLocalidadesAsignarTabla = bow.tablas.paginar(data.recaudoMasivoLocalidades, 5);
                        }).error(function (error) {
                            $scope.mensajeError = error.message;
                        });
                    }
                    else if ($scope.opcionBusqueda == "D") {
                        afiliacionesService.getAllLocalidadesByNotConvenioAndDepartamento({ recaudoMasivoId: convenioRecaudoId, DepartamentoId: Id })
                        .success(function (data) {
                            $scope.listaLocalidadesAsignarTabla = bow.tablas.paginar(data.recaudoMasivoLocalidades, 5);
                        }).error(function (error) {
                            $scope.mensajeError = error.message;
                        });
                    }
                }
                else {
                    if ($scope.opcionBusqueda == "P") {
                        afiliacionesService.getAllLocalidadesByConvenioAndPais({ recaudoMasivoId: convenioRecaudoId, paisId: Id })
                        .success(function (data) {
                            $scope.listaLocalidadesAsignarTabla = bow.tablas.paginar(data.recaudoMasivoLocalidades, 5);
                        }).error(function (error) {
                            $scope.mensajeError = error.message;
                        });
                    }
                    else if ($scope.opcionBusqueda == "D") {
                        afiliacionesService.getAllLocalidadesByConvenioAndDepartamento({ recaudoMasivoId: convenioRecaudoId, departamentoId: Id })
                        .success(function (data) {
                            $scope.listaLocalidadesAsignarTabla = bow.tablas.paginar(data.recaudoMasivoLocalidades, 5);
                        }).error(function (error) {
                            $scope.mensajeError = error.message;
                        });
                    }
                }
                if ($scope.opcionBusqueda == "L") {
                    $scope.listaLocalidadesAsignarTabla = bow.tablas.paginar([{ id: Id, nombre: Nombre }], 5);
                }
            };
        }]);
})();