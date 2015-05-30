(function () {
    angular.module('app')
        .directive("bowPersona", function () {
            return {
                restrict: 'E',
                scope: {
                    notificarRegistroPersona: '&bowRegistroPersona',
                    personaEstado: '=ngModel',
                    fechaNacimientoRequerido: '=fechaNacimientoRequired',
                    mostrarAuditoria: '=mostrarAuditoria'
                },

                controller: ['$filter', '$scope', 'abp.services.app.personas', 'abp.services.app.zonificacion', 'abp.services.app.parametros',
                    function ($filter, $scope, personasService, zonificacionService, parametrosService) {

                        $scope.editandoMedio = false;
                        $scope.eliminandoMedio = false;

                        //Controles para editar o eliminar un medio de contacto web
                        $scope.controlesMedio = {
                            visible: [],
                            mostrar: function ($index) {
                                $scope.controlesMedio.visible[$index] = true;
                                $scope.eliminandoMedio = false;
                            },
                            ocultar: function ($index) {
                                $scope.controlesMedio.visible[$index] = false;
                                $scope.eliminandoMedio = false;
                            }
                        };

                        //Creamos una variable almacenar los valores que vienen en personaEstado (personaId, estado)
                        var personaEstado = $scope.personaEstado;

                        $scope.soloLecturaConsultar = false;

                        //Se inicializa la variable que almacena la lista de telefonos de la persona
                        $scope.telefonosPersona = [];

                        //Se inicializa la variable que almacena la lista de direcciones de la persona
                        $scope.direccionesPersona = [];

                        //Variable para almacenar los medios de contacto eliminados si es cargado desde BD
                        $scope.eliminadosMedioContacto = [];

                        //Variable para almacenar las opciones preferencias a guardar
                        $scope.savePreferenciasPersona = [];

                        //Variables inicializar matriz para eliminar de la lista
                        $scope.mensajeEliminar = [];

                        //Variable para manipular los controles que no se almacenan en nuevaPersona (controles que manipulan datos solo en el front-end)
                        $scope.controlIndependiente = {};

                        //Variable para validar si el medio contacto es nuevo o es para editar
                        $scope.medioContactoNuevo = true;

                        //Variable para colocar controles en solo lectura
                        $scope.soloLecturaPersona = false;

                        //variable para visuzalizar el div que con la edad calcular la fecha
                        $scope.mostrarEdadfecha = true;

                        //Variable para deshabilitar los tabs
                        $scope.tabsDisable = true;

                        $scope.paises = [];

                        $scope.profesiones = [];

                        $scope.controles = {
                            visible: [],
                            mostrar: function ($index) {
                                $scope.controles.visible[$index] = true;
                            },
                            ocultar: function ($index) {
                                $scope.controles.visible[$index] = false;
                            }
                        };

                        //Lamado al servicio para obtener el rango de la fecha de expedicion del documento
                        personasService.fechaExpedicionDocumento().success(function (data) {
                            var fechaMaximaExpedicion = new Date(data.fechaMaximaExp);
                            var fechaMinimaExpedicion = new Date(data.fechaMinimaExp);
                            $scope.configuracionFechaExpedicion = bow.fechas.configurarDatePicker(fechaMinimaExpedicion, fechaMaximaExpedicion);
                        }).error(function (error) {
                            abp.notify.error(error.message);
                        });

                        //Lamado al servicio para obtener el rango de la fecha de nacimiento
                        personasService.fechaNacimiento().success(function (data) {
                            var fechaMaximaNacimiento = new Date(data.fechaMaximaNac);
                            var fechaMinimaNacimiento = new Date(data.fechaMinimaNac);
                            $scope.configuracionFechaNacimiento = bow.fechas.configurarDatePicker(fechaMinimaNacimiento, fechaMaximaNacimiento);
                        }).error(function (error) {
                            abp.notify.error(error.message);
                        });

                        //Lamado al servicio para obtener el rango de la fecha de fallecimiento
                        $scope.fechaFallecimiento = function (personaId) {
                            personasService.fechaFallecimiento({ personaId: personaId }).success(function (data) {
                                var fechaMaximaFallecimiento = new Date(data.FechaMaximaFall);
                                var fechaMinimaFallecimiento = new Date(data.fechaMinimaFall);
                                $scope.configuracionFechaFallecimiento = bow.fechas.configurarDatePicker(fechaMinimaFallecimiento, fechaMaximaFallecimiento);
                            }).error(function (error) {
                                abp.notify.error(error.message);
                            });
                        };

                        //Funcion para cargar los profesiones en el typeahead profesiones
                        function cargarProfesiones() {

                            parametrosService.getTiposByParametroProfesionesWithIdNoIdentificado().success(function (data) {
                                $scope.profesiones = data.tipos;

                                if (personaEstado.personaId == 0 && personaEstado.estado == 'N') {
                                    profesionNoIdentificada = {
                                        id: data.idNoIdentificado,
                                        nombre: data.nombre,
                                        descripcion: data.descripcion
                                    }

                                    $scope.controlIndependiente.profesionId = profesionNoIdentificada;
                                }
                            });
                        };
                        cargarProfesiones();

                        //Funcion para cargar los estados civiles en el dropdown
                        function cargarEstadoCivil() {
                            parametrosService.getTiposByParametroEstadoCivilWithIdNoIdentificado().success(function (data) {
                                $scope.estadoCivil = data.tipos;

                                if (personaEstado.personaId == 0 && personaEstado.estado == 'N') {
                                    $scope.nuevaPersona.tipoEstadoCivilId = data.idNoIdentificado;
                                }
                            });
                        };
                        cargarEstadoCivil();

                        //Funcion para cargar los tipos de documentos segun el pais seleccionado
                        $scope.cargarTiposDocumento = function (pais) {

                            $scope.paisSeleccionado = pais.id;

                            personasService.getTiposDocumentoPersona({ paisId: pais.id })
                                .success(function (data) {
                                    $scope.tiposDocumento = data.tiposDocumento;
                                }).error(function (error) {
                                    $scope.tiposDocumento = "";
                                    abp.notify.error(error.message);
                                });
                        };

                        //Funcion para calcular la edad segun la fecha de nacimiento indicada
                        $scope.calcularEdad = function (fechanacimiento) {
                            console.log("Calculo la edad: ", fechanacimiento);
                            if (fechanacimiento === "" || fechanacimiento === undefined || fechanacimiento === null) {
                                $scope.edadcalculada = "";
                            } else {
                                var birthday = new Date(fechanacimiento);
                                $scope.edadcalculada = ~~((Date.now() - birthday) / (31557600000));
                                $scope.edadcalculada += "";
                            }
                        };

                        //Funcion para calcular la fecha de nacimiento segun la edad indicada
                        $scope.calcularFecha = function (edad) {
                            if (!edad) {
                                $scope.fechacalculada = "";
                            } else {
                                var anioactual = new Date().getFullYear();
                                var anionacimiento = anioactual - edad;
                                $scope.fechacalculada = '01/01/' + anionacimiento;
                            }
                        };

                        //Matriz de los campos para enviar en el guardado
                        $scope.nuevaPersona = {
                            id: '',
                            tieneDocumento: false,
                            tipoDocumentoId: '',
                            numeroDocumento: '',
                            fechaExpDocumento: null,
                            nombre: '',
                            apellido1: '',
                            apellido2: '',
                            tieneFechaNacimiento: false,
                            fechaNacimiento: '',
                            genero: '',
                            correoElectronico: '',
                            contactarCorreo: '',
                            contactarSms: '',
                            contactarTelefono: '',
                            fechaIngreso: '',
                            tipoProfesionId: '',
                            tipoEstadoCivilId: '',
                            discapacitada: false,
                            fechaFallecimiento: '',
                            paisId: '',
                            fechaUltActualizacion: '',
                            fechaNacimientoRequerido: '',
                            usuario: ''
                        };

                        //Funcion que se ejecuta al dar click en el chechkbox de tiene Documento            
                        $scope.mostrarCamposDocumento = function () {
                            if ($scope.nuevaPersona.tieneDocumento) {
                                $scope.mostrarDocumento = true;
                            }
                            else {
                                $scope.nuevaPersona.tipoDocumentoId = "";
                                $scope.nuevaPersona.numeroDocumento = "";
                                $scope.mostrarDocumento = false;
                            }
                        };

                        //Funcion para mostrar y ocultar div segun seleccion del checkbox (tiene fecha de nacimiento)
                        $scope.mostrarFechaNacimiento = function () {
                            if ($scope.nuevaPersona.tieneFechaNacimiento) {
                                $scope.fechaedad = true;
                                $scope.mostrarEdadfecha = false;
                                $scope.controlIndependiente.edad = "";
                                $scope.fechacalculada = "";
                            } else {
                                $scope.nuevaPersona.fechaNacimiento = "";
                                $scope.edadcalculada = "";
                                $scope.fechaedad = false;
                                $scope.mostrarEdadfecha = true;
                            }
                        };

                        //Funcion para enviar los datos al servidor y guardar
                        $scope.savePersona = function () {

                            $scope.nuevaPersona.fechaNacimientoRequerido = $scope.fechaNacimientoRequerido;

                            if ($scope.paisSeleccionado != undefined) {
                                $scope.nuevaPersona.paisId = $scope.paisSeleccionado;
                            }

                            //Se verifica si indico la profesion (profesionId)
                            if ($scope.controlIndependiente.profesionId != undefined) {
                                $scope.nuevaPersona.tipoProfesionId = $scope.controlIndependiente.profesionId.id;
                            }

                            //Validamos cual de la fecha de nacimiento enviamos la indicada o la calculada
                            if (($scope.nuevaPersona.tieneFechaNacimiento == false || $scope.nuevaPersona.tieneFechaNacimiento == "") && ($scope.fechacalculada != "")) {
                                $scope.nuevaPersona.fechaNacimiento = $scope.fechacalculada;
                            }

                            personasService.savePersona($scope.nuevaPersona)
                                .success(function (data) {

                                    var telefonosConUbicacion = true;

                                    //Paso a la función de la directiva la persona que almeceno en la bd
                                    $scope.notificarRegistroPersona({ persona: data });

                                    //se valida si la persona es nueva 
                                    if ($scope.nuevaPersona.id != 0) {
                                        var telefonosConUbicacion = savePersonaTelefono();
                                        var direccionesConUbicacion = savePersonaDireccion();
                                        savePersonaContactoWeb();
                                        savePersonaPreferencias();
                                    }

                                    //Almaceno el id con que se guardo o actualizo en la bd
                                    $scope.nuevaPersona.id = data.id;

                                    if (telefonosConUbicacion === true && direccionesConUbicacion === true) {
                                        abp.notify.success(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_notificacionInsertadoPersona', 'Bow'),
                                        abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));
                                    }

                                    $scope.soloLecturaPersona = true;
                                    $scope.tabsDisable = false;
                                    //$scope.mensajeError = "";

                                    //Paso para activar el tab Telefonos en el tab Contacto
                                    //$('#myTab a:first').tab('show')

                                }).error(function (error) {
                                    if (error.validationErrors != null) {
                                        abp.notify.error(error.validationErrors[0].message);
                                    } else {
                                        abp.notify.error(error.message);
                                    }
                                });
                        };

                        //Funcion para guardar los telefonos de la persona
                        function savePersonaTelefono() {

                            var telefonoTieneUbicacion = true;

                            //Ciclo para verificar si algún número de teléfono no tiene el tipo de ubicación
                            if ($scope.telefonosPersona != null) {
                                for (var i = 0; i < $scope.telefonosPersona.length; i++) {
                                    if (!$scope.telefonosPersona[i].tipoUbicacionId) {
                                        telefonoTieneUbicacion = false;
                                        break;
                                    }
                                }
                            }

                            //Enviar un mensaje al usuario si hay algun número de teléfono sin tipo de ubicación
                            if (telefonoTieneUbicacion === false) {
                                abp.notify.warn(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_telefonoSinTipoUbicacion', 'Bow') + $scope.telefonosPersona[i].telefonoNumero,
                                abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));
                            } else {
                                personasService.savePersonaTelefono({ personaId: $scope.nuevaPersona.id, personaTelefonos: $scope.telefonosPersona })
                               .success(function () {
                                   $scope.cargarTelefonosPersona();
                               }).error(function (error) {
                                   abp.notify.error(error.message);
                               });
                            }

                            return telefonoTieneUbicacion;

                        };

                        //Funcion para guardar las direcciones de la persona
                        function savePersonaDireccion() {

                            var direccionTieneUbicacion = true;

                            //Ciclo para verificar si alguna dirección no tiene el tipo de ubicación
                            if ($scope.direccionesPersona != null) {
                                for (var i = 0; i < $scope.direccionesPersona.length; i++) {
                                    if (!$scope.direccionesPersona[i].tipoUbicacionId) {
                                        direccionTieneUbicacion = false;
                                        break;
                                    }
                                }
                            }

                            //Enviar un mensaje al usuario si hay alguna dirección sin tipo de ubicación
                            if (direccionTieneUbicacion === false) {
                                abp.notify.warn(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_direccionSinTipoUbicacion', 'Bow') + $scope.direccionesPersona[i].direccionCompleta,
                                abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));
                            } else {

                                personasService.savePersonaDireccion({ personaId: $scope.nuevaPersona.id, personaDirecciones: $scope.direccionesPersona })
                                    .success(function () {
                                        $scope.cargarDireccionesPersona();
                                    }).error(function (error) {
                                        abp.notify.error(error.message);
                                    });
                            }


                            return direccionTieneUbicacion;

                        };

                        //Funcion para guardar las preferencias de la persona
                        function savePersonaPreferencias() {
                            personasService.savePersonaPreferencia({ preferenciasPersona: $scope.savePreferenciasPersona })
                                .success(function () {
                                    cargarPreferencias();
                                }).error(function (error) {
                                    abp.notify.error(error.message);
                                });
                        };

                        //Funcion para guardar los contactos web de la persona
                        function savePersonaContactoWeb() {
                            $scope.saveMediosContactoWeb = [];
                            $scope.saveMediosContactoWeb = $scope.mediosContactoPersona;

                            if ($scope.eliminadosMedioContacto.length != 0) {
                                //Se unen las dos listas de eliminados si son de BD con los que se crearon nuevos
                                $scope.saveMediosContactoWeb = $scope.mediosContactoPersona.concat($scope.eliminadosMedioContacto);
                            }

                            if ($scope.saveMediosContactoWeb != null) {
                                personasService.savePersonaContactoWeb({ personaContactoWeb: $scope.saveMediosContactoWeb })
                                    .success(function () {
                                        $scope.cargarMediosContactoPersona();

                                    }).error(function (error) {
                                        abp.notify.error(error.message);
                                    });
                            }
                        };

                        //Funcion para cargar el formulario de agregar una nueva persona
                        $scope.cargarNuevaPersona = function () {
                            //Se vuelven a cargar los controles para ubicarlos en la posicion No Identificado
                            cargarProfesiones();
                            cargarEstadoCivil();

                            $scope.mostrarDocumentoPersonaExiste = false;
                            $scope.mostrarConsultaPersona = false;
                            $scope.mostrarMasinformacion = true;
                            $scope.soloLecturaPersona = true;

                            $scope.mostrarEdadfecha = true;
                            $scope.tabsDisable = true;
                            //$scope.mensajeError = "";
                        };

                        //Funcion para consultar la persona y cargar el div inferior correspondiente para actualizar persona, agregar nueva persona o para mostrar mensaje de que ya existe (boton Iniciar registro)
                        $scope.consultarPersona = function () {

                            if ($scope.paisSeleccionado != undefined) {
                                $scope.nuevaPersona.paisId = $scope.paisSeleccionado;
                            }

                            personasService.getPersonas($scope.nuevaPersona)
                            .success(function (data) {

                                $scope.tabsDisable = true;

                                if (data.personas != null) {

                                    if (data.puedeSeleccionarPersona == true) {

                                        $scope.mostrarDocumentoPersonaExiste = false;
                                        $scope.mostrarConsultaPersona = true;

                                    } else if (data.puedeSeleccionarPersona == false) {

                                        $scope.mostrarConsultaPersona = false;
                                        $scope.mostrarDocumentoPersonaExiste = true;

                                    }

                                    $scope.resultadoPersona = data.personas;
                                    $scope.mostrarMasinformacion = false;

                                } else {
                                    $scope.cargarNuevaPersona();
                                }

                                $scope.limpiarFormulario();

                            }).error(function (error) {
                                if (error.validationErrors != null) {
                                    abp.notify.error(error.validationErrors[0].message);
                                } else {
                                    abp.notify.error(error.message);
                                }
                            })
                        };

                        //Funcion para consultar la persona y cargarla para editar sus datos, es la funcion del button (esta es la persona)
                        $scope.cargarPersona = function (personaId) {

                            personasService.getPersonaEditar({ id: personaId })
                            .success(function (data) {

                                if (data != null) {

                                    $scope.tabsDisable = false;
                                    $scope.nuevaPersona.id = data.id;

                                    $scope.nuevaPersona.nombre = data.nombre;
                                    $scope.nuevaPersona.apellido1 = data.apellido1;
                                    $scope.nuevaPersona.apellido2 = data.apellido2;

                                    $scope.nuevaPersona.tieneDocumento = data.tieneDocumento;
                                    $scope.mostrarDocumento = data.tieneDocumento;

                                    $scope.nuevaPersona.tipoDocumentoId = data.tipoDocumentoId;
                                    $scope.nuevaPersona.numeroDocumento = data.numeroDocumento;

                                    if (data.fechaExpDocumento != null) {
                                        if (data.fechaExpDocumento != "") {
                                            $scope.nuevaPersona.fechaExpDocumento = data.fechaExpDocumento.substring(0, 10);
                                        }
                                    }

                                    $scope.fechaNacimientoOculta = data.tieneFechaNacimiento;
                                    $scope.nuevaPersona.tieneFechaNacimiento = data.tieneFechaNacimiento;

                                    if (data.tieneFechaNacimiento == true) {
                                        $scope.nuevaPersona.fechaNacimiento = data.fechaNacimiento.substring(0, 10);
                                        $scope.edadcalculada = data.edad;

                                        $scope.fechaedad = true;
                                        $scope.mostrarEdadfecha = false;
                                    }
                                    else if (data.tieneFechaNacimiento == false && data.fechaNacimiento != null) {
                                        $scope.fechacalculada = data.fechaNacimiento.substring(0, 10);
                                        $scope.controlIndependiente.edad = data.edad;

                                        $scope.fechaedad = false;
                                        $scope.mostrarEdadfecha = true;
                                    } else {
                                        $scope.mostrarEdadfecha = true;
                                    }

                                    $scope.nuevaPersona.genero = data.genero

                                    $scope.nuevaPersona.discapacitada = data.discapacitada;

                                    if (data.fechaFallecimiento != null) {
                                        $scope.nuevaPersona.fechaFallecimiento = data.fechaFallecimiento.substring(0, 10);
                                    }

                                    //Proceso para ubicar el typeahead de profesion de la persona
                                    profesionPersona = {
                                        id: data.tipoProfesionId,
                                        nombre: data.tipoProfesionNombre
                                    }

                                    $scope.controlIndependiente.profesionId = profesionPersona;

                                    $scope.nuevaPersona.tipoEstadoCivilId = data.tipoEstadoCivilId;

                                    $scope.nuevaPersona.contactarTelefono = data.contactarTelefono;
                                    $scope.nuevaPersona.contactarCorreo = data.contactarCorreo;
                                    $scope.nuevaPersona.contactarSms = data.contactarSms;

                                    $scope.nuevaPersona.correoElectronico = data.correoElectronico;

                                    $scope.cargarTelefonosPersona();
                                    $scope.cargarDireccionesPersona();
                                    $scope.cargarMediosContacto();
                                    $scope.cargarMediosContactoPersona();

                                    cargarPreferencias();

                                    $scope.mostrarMasinformacion = true;
                                    $scope.mostrarConsultaPersona = false;

                                    //Procesos para cargar los datos de la persona si viene como parametro
                                    if ((personaEstado.personaId != 0 && personaEstado.estado == 'E') || (personaEstado.personaId != 0 && personaEstado.estado == 'C')) {

                                        //Proceso para ubicar el typeahead de nacionalidad (paisId) de la persona
                                        nacionalidadPersona = {
                                            id: data.paisId,
                                            nombre: data.paisNombre,
                                            indicativo: data.indicativo
                                        }
                                        //$scope.controlIndependiente.paisId = nacionalidadPersona;
                                        $scope.selectedPais = nacionalidadPersona;

                                        //Se carga los tipos de documento de la nacionalidad de la persona indicada
                                        $scope.cargarTiposDocumento(nacionalidadPersona);

                                        if (personaEstado.personaId != 0 && personaEstado.estado == 'C') {
                                            $scope.soloLecturaConsultar = true;
                                            $scope.soloLecturaPersona = true;
                                        } else {
                                            $scope.soloLecturaPersona = false;
                                            $scope.soloLecturaPersona = false;
                                        }

                                    } else {
                                        $scope.soloLecturaPersona = true;
                                        $scope.soloLecturaConsultar = false;
                                    }

                                    //Se llama la funcion para calcular el rango de la fecha de fallecimiento segun fecha nacimiento
                                    $scope.fechaFallecimiento(personaId);

                                    $('#myTab a:first').tab('show')

                                } else {
                                    $scope.mostrarMasinformacion = false;
                                    $scope.mostrarConsultaPersona = false;
                                }

                            }).error(function (error) {
                                abp.notify.error(error.message);
                            })
                        };

                        //Funcion para validar si se paso id de la persona por el parametro desde el link para cargar la edición de la persona
                        if ((personaEstado.personaId != 0 && personaEstado.estado == 'E') || (personaEstado.personaId != 0 && personaEstado.estado == 'C')) {
                            $scope.cargarPersona(personaEstado.personaId);
                        };

                        //Funcion cargar datos del Tab Contacto, con los telefonos de la persona
                        $scope.cargarTelefonosPersona = function () {
                            personasService.getTelefonosPersona({ personaId: $scope.nuevaPersona.id })
                            .success(function (data) {
                                if (data != null) {
                                    $scope.telefonosPersona = data.telefonosPersona;

                                    $scope.personaId = $scope.nuevaPersona.id;

                                    //Variable para capturar la cantidad de telefonos activos para mostrar en el tab
                                    $scope.cantidadTelefonos = 0;

                                    for (var i = 0; i < $scope.telefonosPersona.length; i++) {
                                        if ($scope.telefonosPersona[i].nombreEstado === true) {
                                            $scope.cantidadTelefonos = $scope.cantidadTelefonos + 1;
                                        }
                                    }
                                }
                            })
                        };

                        function cargarTipoUbicacion() {
                            parametrosService.getTiposByParametroUbicacion()
                            .success(function (data) {
                                $scope.tipos = data.tipos;
                            })
                        };
                        cargarTipoUbicacion();


                        $scope.limpiarFormulario = function () {
                            $scope.nuevaPersona.fechaExpDocumento = null;
                            $scope.nuevaPersona.tieneFechaNacimiento = false;
                            $scope.nuevaPersona.fechaNacimiento = null;
                            $scope.nuevaPersona.genero = "Masculino";

                            $scope.nuevaPersona.discapacitada = false;
                            $scope.nuevaPersona.fechaFallecimiento = null;
                            $scope.nuevaPersona.paisId = "";

                            $scope.nuevaPersona.contactarTelefono = false;
                            $scope.nuevaPersona.contactarCorreo = false;

                            $scope.controlIndependiente.edad = "";
                            $scope.edadcalculada = "";
                            $scope.fechacalculada = "";

                            //$scope.mensajeError = "";
                            $scope.fechaedad = false;
                        };

                        $scope.cancelModal = function () {
                            $modalInstance.close('cancel');
                        }

                        ///*************************** Direcciones Persona *********************************

                        ////Funcion cargar datos del Tab Contacto, con las direcciones de la persona
                        $scope.cargarDireccionesPersona = function () {
                            personasService.getDireccionesPersona({ personaId: $scope.nuevaPersona.id })
                            .success(function (data) {
                                if (data != null) {
                                    $scope.direccionesPersona = data.direccionesPersona;

                                    $scope.cantidadDirecciones = 0;

                                    for (var i = 0; i < $scope.direccionesPersona.length; i++) {
                                        if ($scope.direccionesPersona[i].nombreEstado === true) {
                                            $scope.cantidadDirecciones = $scope.cantidadDirecciones + 1;
                                        }
                                    }
                                }
                            })
                        }


                        // **************************** Medios de Contacto (Pill Web) ********************

                        //Funcion para cargar el dropdown de medios de contacto para asignar
                        $scope.cargarMediosContacto = function () {
                            personasService.getContactosWebFilterByPersona({ personaId: $scope.nuevaPersona.id })
                                .success(function (data) {
                                    $scope.tiposMedioContacto = data.contactosWeb;
                                    $scope.controlIndependiente.medioContacto = $scope.tiposMedioContacto[0];
                                }).error(function (error) {
                                    abp.notify.error(error.message);
                                });
                        }

                        //Funcion para cargar los medios de contacto de la persona indicada
                        $scope.cargarMediosContactoPersona = function () {
                            personasService.getPersonaContactoWeb({ personaId: $scope.nuevaPersona.id })
                                .success(function (data) {
                                    $scope.mediosContactoPersona = data.mediosContactoPersona;
                                }).error(function (error) {
                                    abp.notify.error(error.message);
                                });
                        }


                        $scope.eliminarMedioOk = function (mediocontacto) {
                            //Variable para ir almacenando los medios de contacto web eliminados si vienen desde la BD
                            var medioEliminado =
                                {
                                    id: mediocontacto.id,
                                    nombre: mediocontacto.medioContactoNombre
                                }

                            //Se asigna el tipo eliminado al dropdown de tipos de medio contacto
                            $scope.tiposMedioContacto.push(medioEliminado);

                            var eliminadoMedio =
                            {
                                id: mediocontacto.id,
                                personaId: mediocontacto.personaId,
                                identificador: mediocontacto.identificador,
                                medioContactoNombre: mediocontacto.medioContactoNombre,
                                tipoId: mediocontacto.tipoId,
                                tipoCambio: 'E'
                            }

                            if (mediocontacto.id != null) {
                                $scope.eliminadosMedioContacto.push(eliminadoMedio);
                            }

                            //Se elimina el medio de contacto asignado del dropdown
                            for (var i = 0; i < $scope.mediosContactoPersona.length; i++) {
                                if ($scope.mediosContactoPersona[i].id == eliminadoMedio.id) {
                                    $scope.mediosContactoPersona.splice(i, 1);
                                }
                            }

                            $scope.controlIndependiente.medioContacto = $scope.tiposMedioContacto[0];
                        };

                        $scope.eliminarMedioCancel = function () {
                            $scope.eliminandoMedio = false;
                        };

                        $scope.puedeEliminarMedio = function (medioId, funcionRetornarPuedeEliminar) {
                            $scope.eliminandoMedio = true;
                            funcionRetornarPuedeEliminar(true);
                        }

                        $scope.noPuedeEliminar = function () {
                            //Mensaje de no puede eliminar
                        }


                        //Funcion para editar un medio de contacto
                        $scope.editarMedioContactoWeb = function (medioPersona, $index) {

                            $scope.medioEditando = medioPersona;
                            $scope.indiceEditar = $index;
                            $scope.editandoMedio = true;

                            var medioEditado =
                               {
                                   id: medioPersona.tipoId,
                                   nombre: medioPersona.medioContactoNombre
                               }

                            $scope.nombreMedioEditar = medioPersona.medioContactoNombre;

                            $scope.tiposMedioContacto.push(medioEditado);

                            //Ubico la lista en la posición que se va a editar
                            $scope.mediosContactoPersona[$index];

                            $scope.controlIndependiente.identificador = medioPersona.identificador;
                            $scope.controlIndependiente.medioContacto = medioEditado;
                            $scope.medioContactoNuevo = false;

                        }

                        //Función para almacenar temporalmente el medio de contacto indicado; los valores de input y select se deben pasar por parametros en la funcion para que funcione
                        $scope.asignarMedioContacto = function () {

                            //Se elimina el medio de contacto asignado del dropdown
                            for (var i = 0; i < $scope.tiposMedioContacto.length; i++) {
                                if ($scope.tiposMedioContacto[i].id == $scope.controlIndependiente.medioContacto.id) {
                                    $scope.tiposMedioContacto.splice(i, 1);
                                }
                            }

                            if ($scope.medioContactoNuevo == true) {
                                var nuevoMedioContacto =
                                      {
                                          personaId: $scope.nuevaPersona.id,
                                          identificador: $scope.controlIndependiente.identificador,
                                          medioContactoNombre: $scope.controlIndependiente.medioContacto.nombre,
                                          tipoId: $scope.controlIndependiente.medioContacto.id,
                                          tipoCambio: 'N'
                                      };

                                $scope.mediosContactoPersona.push(nuevoMedioContacto);

                                alert(JSON.stringify($scope.mediosContactoPersona));

                                abp.notify.info(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_notificacionAsignadoPersonaContactoWeb', 'Bow'),
                                abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));

                            } else {
                                $scope.mediosContactoPersona[$scope.indiceEditar].tipoId = $scope.controlIndependiente.medioContacto.id;
                                $scope.mediosContactoPersona[$scope.indiceEditar].medioContactoNombre = $scope.controlIndependiente.medioContacto.nombre;
                                $scope.mediosContactoPersona[$scope.indiceEditar].identificador = $scope.controlIndependiente.identificador

                                if ($scope.mediosContactoPersona[$scope.indiceEditar].id != null) {
                                    $scope.mediosContactoPersona[$scope.indiceEditar].tipoCambio = 'M';
                                }
                            }

                            $scope.controlIndependiente.identificador = "";
                            $scope.controlIndependiente.medioContacto = $scope.tiposMedioContacto[0];

                            $scope.medioContactoNuevo = true;
                            $scope.editandoMedio = false;
                        }


                        $scope.cancelarUpdateMedio = function () {
                            if ($scope.editandoMedio) {
                                $scope.editandoMedio = false;

                                //Se elimina el medio de contacto asignado del dropdown
                                for (var i = 0; i < $scope.tiposMedioContacto.length; i++) {
                                    if ($scope.tiposMedioContacto[i].id == $scope.medioEditando.id) {
                                        $scope.tiposMedioContacto.splice(i, 1);
                                    }
                                }
                            }

                            $scope.medioContactoNuevo = true;
                            $scope.controlIndependiente.identificador = "";
                            $scope.controlIndependiente.medioContacto = $scope.tiposMedioContacto[0];
                        }

                        //************************  Tab Informacion Personal ***************************
                        function cargarPreferencias() {
                            personasService.getPreferenciaPersona()
                               .success(function (data) {
                                   $scope.preferencias = data.preferencias;

                                   cargarOpcionPreferenciaPersona();

                               }).error(function (error) {
                                   abp.notify.error(error.message);
                               });
                        }

                        function cargarOpcionPreferenciaPersona() {
                            personasService.getOpcionPreferenciaPersona({ personaId: $scope.nuevaPersona.id })
                               .success(function (data) {
                                   $scope.opcionPreferenciaPersona = data.opcionPreferenciaPersona;

                                   //Procedimiento para asignar en la lista de preferencias cargadas inicialmente con la preferencia de la persona indicada
                                   if ($scope.opcionPreferenciaPersona != null) {
                                       for (var i = 0; i < $scope.preferencias.length; i++) {
                                           for (var j = 0; j < $scope.opcionPreferenciaPersona.length; j++) {
                                               if ($scope.preferencias[i].id == $scope.opcionPreferenciaPersona[j].preferenciaId) {
                                                   $scope.preferencias[i].tipoCambio = "B";
                                                   $scope.preferencias[i].opcionPreferenciaId = $scope.opcionPreferenciaPersona[j].opcionPreferenciaId;
                                                   $scope.preferencias[i].personaPreferenciaId = $scope.opcionPreferenciaPersona[j].id;
                                               }
                                           }
                                       }
                                   }
                               }).error(function (error) {
                                   abp.notify.error(error.message);
                               });
                        }

                        $scope.changeOpcionPreferencia = function ($index) {
                            if ($scope.preferencias[$index].tipoCambio == "B" || $scope.preferencias[$index].tipoCambio == "M") {
                                $scope.preferencias[$index].tipoCambio = "M";
                            } else {
                                $scope.preferencias[$index].tipoCambio = "N";
                            }
                            $scope.preferencias[$index].personaId = $scope.nuevaPersona.id;

                            //Almaceno el registro que se modifico o agrego para enviar al servicio
                            var nuevaPreferenciaPersona = {
                                id: $scope.preferencias[$index].personaPreferenciaId,
                                personaId: $scope.preferencias[$index].personaId,
                                tipoCambio: $scope.preferencias[$index].tipoCambio,
                                opcionPreferenciaId: $scope.preferencias[$index].opcionPreferenciaId
                            };

                            $scope.savePreferenciasPersona.push(nuevaPreferenciaPersona);

                        }

                        // *********************** Tab Auditoria *******************
                        $scope.cargarAuditoria = function () {
                            personasService.getAuditoriaPersona({ personaId: $scope.nuevaPersona.id })
                             .success(function (data) {
                                 //$scope.auditoriaPersona = data.auditoriaPersona;
                                 $scope.auditoriaPersona = bow.tablas.paginar(data.auditoriaPersona, 3);

                             }).error(function (error) {
                                 abp.notify.error(error.message);
                             });
                        }

                    }],
                templateUrl: '/App/Main/directives/bowPersona/bowPersona.cshtml'
            };
        })
})();

//(function () {
//    angular.module('app')
//        .directive("bowPersona", function () {
//            return {
//                restrict: 'E',
//                scope: {
//                    notificarRegistroPersona: '&bowRegistroPersona',
//                    personaEstado: '=ngModel',
//                    fechaNacimientoRequerido: '=fechaNacimientoRequired',
//                    mostrarAuditoria: '=mostrarAuditoria'
//                },

//                controller: ['$filter', '$scope', 'abp.services.app.personas', 'abp.services.app.zonificacion', 'abp.services.app.parametros',
//                    function ($filter, $scope, personasService, zonificacionService, parametrosService) {

//                        //Creamos una variable almacenar los valores que vienen en personaEstado (personaId, estado)
//                        var personaEstado = $scope.personaEstado;

//                        $scope.soloLecturaConsultar = false;

//                        //Se inicializa la variable que almacena la lista de telefonos de la persona
//                        $scope.telefonosPersona = [];

//                        //Se inicializa la variable que almacena la lista de direcciones de la persona
//                        $scope.direccionesPersona = [];

//                        //Variable para almacenar los medios de contacto eliminados si es cargado desde BD
//                        $scope.eliminadosMedioContacto = [];

//                        //Variable para almacenar las opciones preferencias a guardar
//                        $scope.savePreferenciasPersona = [];

//                        //Variables inicializar matriz para eliminar de la lista
//                        $scope.mensajeEliminar = [];

//                        //Variable para manipular los controles que no se almacenan en nuevaPersona (controles que manipulan datos solo en el front-end)
//                        $scope.controlIndependiente = {};

//                        //Variable para validar si el medio contacto es nuevo o es para editar
//                        $scope.medioContactoNuevo = true;

//                        //Variable para colocar controles en solo lectura
//                        $scope.soloLecturaPersona = false;

//                        //variable para visuzalizar el div que con la edad calcular la fecha
//                        $scope.mostrarEdadfecha = true;

//                        //Variable para deshabilitar los tabs
//                        $scope.tabsDisable = true;

//                        $scope.paises = [];

//                        $scope.profesiones = [];

//                        //Lamado al servicio para obtener el rango de la fecha de expedicion del documento
//                        personasService.fechaExpedicionDocumento().success(function (data) {
//                            var fechaMaximaExpedicion = new Date(data.fechaMaximaExp);
//                            var fechaMinimaExpedicion = new Date(data.fechaMinimaExp);
//                            $scope.configuracionFechaExpedicion = bow.fechas.configurarDatePicker(fechaMinimaExpedicion, fechaMaximaExpedicion);
//                        }).error(function (error) {
//                            $scope.mensajeError = error.message;
//                        });

//                        //Lamado al servicio para obtener el rango de la fecha de nacimiento
//                        personasService.fechaNacimiento().success(function (data) {
//                            var fechaMaximaNacimiento = new Date(data.fechaMaximaNac);
//                            var fechaMinimaNacimiento = new Date(data.fechaMinimaNac);
//                            $scope.configuracionFechaNacimiento = bow.fechas.configurarDatePicker(fechaMinimaNacimiento, fechaMaximaNacimiento);
//                        }).error(function (error) {
//                            $scope.mensajeError = error.message;
//                        });

//                        //Lamado al servicio para obtener el rango de la fecha de fallecimiento
//                        $scope.fechaFallecimiento = function (personaId) {
//                            personasService.fechaFallecimiento({ personaId: personaId }).success(function (data) {
//                                var fechaMaximaFallecimiento = new Date(data.FechaMaximaFall);
//                                var fechaMinimaFallecimiento = new Date(data.fechaMinimaFall);
//                                $scope.configuracionFechaFallecimiento = bow.fechas.configurarDatePicker(fechaMinimaFallecimiento, fechaMaximaFallecimiento);
//                            }).error(function (error) {
//                                $scope.mensajeError = error.message;
//                            });
//                        };

//                        //Funcion para cargar los profesiones en el typeahead profesiones
//                        function cargarProfesiones() {

//                            parametrosService.getTiposByParametroProfesionesWithIdNoIdentificado().success(function (data) {
//                                $scope.profesiones = data.tipos;

//                                if (personaEstado.personaId == 0 && personaEstado.estado == 'N') {
//                                    profesionNoIdentificada = {
//                                        id: data.idNoIdentificado,
//                                        nombre: data.nombre,
//                                        descripcion: data.descripcion
//                                    }

//                                    $scope.controlIndependiente.profesionId = profesionNoIdentificada;
//                                }
//                            });
//                        };
//                        cargarProfesiones();

//                        //Funcion para cargar los estados civiles en el dropdown
//                        function cargarEstadoCivil() {
//                            parametrosService.getTiposByParametroEstadoCivilWithIdNoIdentificado().success(function (data) {
//                                $scope.estadoCivil = data.tipos;

//                                if (personaEstado.personaId == 0 && personaEstado.estado == 'N') {
//                                    $scope.nuevaPersona.tipoEstadoCivilId = data.idNoIdentificado;
//                                }
//                            });
//                        };
//                        cargarEstadoCivil();

//                        //Funcion para cargar los tipos de documentos segun el pais seleccionado
//                        $scope.cargarTiposDocumento = function (pais) {

//                            $scope.paisSeleccionado = pais.id;

//                            personasService.getTiposDocumentoPersona({ paisId: pais.id })
//                                .success(function (data) {
//                                    $scope.tiposDocumento = data.tiposDocumento;
//                                }).error(function (error) {
//                                    $scope.tiposDocumento = "";
//                                    $scope.mensajeError = error.message;
//                                });
//                        };

//                        //Funcion para calcular la edad segun la fecha de nacimiento indicada
//                        $scope.calcularEdad = function (fechanacimiento) {
//                            console.log("Calculo la edad: ", fechanacimiento);
//                            if (fechanacimiento === "" || fechanacimiento === undefined || fechanacimiento === null) {
//                                $scope.edadcalculada = "";
//                            } else {
//                                var birthday = new Date(fechanacimiento);
//                                $scope.edadcalculada = ~~((Date.now() - birthday) / (31557600000));
//                                $scope.edadcalculada += "";
//                            }
//                        };

//                        //Funcion para calcular la fecha de nacimiento segun la edad indicada
//                        $scope.calcularFecha = function (edad) {
//                            if (!edad) {
//                                $scope.fechacalculada = "";
//                            } else {
//                                var anioactual = new Date().getFullYear();
//                                var anionacimiento = anioactual - edad;
//                                $scope.fechacalculada = '01/01/' + anionacimiento;
//                            }
//                        };

//                        //Matriz de los campos para enviar en el guardado
//                        $scope.nuevaPersona = {
//                            id: '',
//                            tieneDocumento: false,
//                            tipoDocumentoId: '',
//                            numeroDocumento: '',
//                            fechaExpDocumento: null,
//                            nombre: '',
//                            apellido1: '',
//                            apellido2: '',
//                            tieneFechaNacimiento: false,
//                            fechaNacimiento: '',
//                            genero: '',
//                            correoElectronico: '',
//                            contactarCorreo: '',
//                            contactarSms: '',
//                            contactarTelefono: '',
//                            fechaIngreso: '',
//                            tipoProfesionId: '',
//                            tipoEstadoCivilId: '',
//                            discapacitada: false,
//                            fechaFallecimiento: '',
//                            paisId: '',
//                            fechaUltActualizacion: '',
//                            fechaNacimientoRequerido: '',
//                            usuario: ''
//                        };

//                        //Funcion que se ejecuta al dar click en el chechkbox de tiene Documento            
//                        $scope.mostrarCamposDocumento = function () {
//                            if ($scope.nuevaPersona.tieneDocumento) {
//                                $scope.mostrarDocumento = true;
//                            }
//                            else {
//                                $scope.nuevaPersona.tipoDocumentoId = "";
//                                $scope.nuevaPersona.numeroDocumento = "";
//                                $scope.mostrarDocumento = false;
//                            }
//                        };

//                        //Funcion para mostrar y ocultar div segun seleccion del checkbox (tiene fecha de nacimiento)
//                        $scope.mostrarFechaNacimiento = function () {
//                            //console.log("Fecha nacimiento oculta: ", $scope.nuevaPersona.tieneFechaNacimiento);
//                            if ($scope.nuevaPersona.tieneFechaNacimiento) {
//                                $scope.fechaedad = true;
//                                $scope.mostrarEdadfecha = false;
//                                $scope.controlIndependiente.edad = "";
//                                $scope.fechacalculada = "";
//                            } else {
//                                $scope.nuevaPersona.fechaNacimiento = "";
//                                $scope.edadcalculada = "";
//                                $scope.fechaedad = false;
//                                $scope.mostrarEdadfecha = true;
//                            }
//                        };

//                        //Funcion para enviar los datos al servidor y guardar
//                        $scope.savePersona = function () {

//                            $scope.nuevaPersona.fechaNacimientoRequerido = $scope.fechaNacimientoRequerido;

//                            if ($scope.paisSeleccionado != undefined) {
//                                $scope.nuevaPersona.paisId = $scope.paisSeleccionado;
//                            }

//                            //Se verifica si indico la profesion (profesionId)
//                            if ($scope.controlIndependiente.profesionId != undefined) {
//                                $scope.nuevaPersona.tipoProfesionId = $scope.controlIndependiente.profesionId.id;
//                            }

//                            //Validamos cual de la fecha de nacimiento enviamos la indicada o la calculada
//                            if (($scope.nuevaPersona.tieneFechaNacimiento == false || $scope.nuevaPersona.tieneFechaNacimiento == "") && ($scope.fechacalculada != "")) {
//                                $scope.nuevaPersona.fechaNacimiento = $scope.fechacalculada;
//                            }

//                            personasService.savePersona($scope.nuevaPersona)
//                                .success(function (data) {

//                                    var telefonosConUbicacion = true;

//                                    //Paso a la función de la directiva la persona que almeceno en la bd
//                                    $scope.notificarRegistroPersona({ persona: data });

//                                    //se valida si la persona es nueva 
//                                    if ($scope.nuevaPersona.id != 0) {
//                                        var telefonosConUbicacion = savePersonaTelefono();
//                                        var direccionesConUbicacion = savePersonaDireccion();
//                                        savePersonaContactoWeb();
//                                        savePersonaPreferencias();
//                                    }

//                                    //Almaceno el id con que se guardo o actualizo en la bd
//                                    $scope.nuevaPersona.id = data.id;

//                                    if (telefonosConUbicacion === true && direccionesConUbicacion === true) {
//                                        abp.notify.success(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_notificacionInsertadoPersona', 'Bow'),
//                                        abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));
//                                    }

//                                    $scope.soloLecturaPersona = true;
//                                    $scope.tabsDisable = false;
//                                    $scope.mensajeError = "";

//                                }).error(function (error) {
//                                    if (error.validationErrors != null) {
//                                        $scope.mensajeError = error.validationErrors[0].message;
//                                    } else {
//                                        $scope.mensajeError = error.message;
//                                    }
//                                });
//                        };

//                        //Funcion para guardar los telefonos de la persona
//                        function savePersonaTelefono() {

//                            var telefonoTieneUbicacion = true;

//                            //Ciclo para verificar si algún número de teléfono no tiene el tipo de ubicación
//                            if ($scope.telefonosPersona != null) {
//                                for (var i = 0; i < $scope.telefonosPersona.length; i++) {
//                                    if ($scope.telefonosPersona[i].tipoUbicacionId === undefined) {
//                                        telefonoTieneUbicacion = false;
//                                        break;
//                                    }
//                                }
//                            }

//                            //Enviar un mensaje al usuario si hay algun número de teléfono sin tipo de ubicación
//                            if (telefonoTieneUbicacion === false) {
//                                abp.notify.warn(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_telefonoSinTipoUbicacion', 'Bow') + $scope.telefonosPersona[i].telefonoNumero,
//                                abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));
//                            } else {
//                                personasService.savePersonaTelefono({ personaId: $scope.nuevaPersona.id, personaTelefonos: $scope.telefonosPersona })
//                               .success(function () {
//                                   $scope.cargarTelefonosPersona();
//                               }).error(function (error) {
//                                   $scope.mensajeError = error.message;
//                               });
//                            }

//                            return telefonoTieneUbicacion;

//                        };

//                        //Funcion para guardar las direcciones de la persona
//                        function savePersonaDireccion() {

//                            var direccionTieneUbicacion = true;

//                            //Ciclo para verificar si alguna dirección no tiene el tipo de ubicación
//                            if ($scope.direccionesPersona != null) {
//                                for (var i = 0; i < $scope.direccionesPersona.length; i++) {

//                                    if ($scope.direccionesPersona[i].tipoUbicacionId === undefined) {
//                                        direccionTieneUbicacion = false;
//                                        break;
//                                    }
//                                }
//                            }

//                            //Enviar un mensaje al usuario si hay alguna dirección sin tipo de ubicación
//                            if (direccionTieneUbicacion === false) {
//                                abp.notify.warn(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_direccionSinTipoUbicacion', 'Bow') + $scope.direccionesPersona[i].direccionCompleta,
//                                abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));
//                            } else {

//                                personasService.savePersonaDireccion({ personaId: $scope.nuevaPersona.id, personaDirecciones: $scope.direccionesPersona })
//                                    .success(function () {
//                                        $scope.cargarDireccionesPersona();
//                                    }).error(function (error) {
//                                        $scope.mensajeError = error.message;
//                                    });
//                            }


//                            return direccionTieneUbicacion;

//                        };

//                        //Funcion para guardar las preferencias de la persona
//                        function savePersonaPreferencias() {
//                            personasService.savePersonaPreferencia({ preferenciasPersona: $scope.savePreferenciasPersona })
//                                .success(function () {
//                                    cargarPreferencias();
//                                }).error(function (error) {
//                                    $scope.mensajeError = error.message;
//                                });
//                        };

//                        //Funcion para guardar los contactos web de la persona
//                        function savePersonaContactoWeb() {
//                            $scope.saveMediosContactoWeb = [];
//                            $scope.saveMediosContactoWeb = $scope.mediosContactoPersona;

//                            if ($scope.eliminadosMedioContacto.length != 0) {
//                                //Se unen las dos listas de eliminados si son de BD con los que se crearon nuevos
//                                $scope.saveMediosContactoWeb = $scope.mediosContactoPersona.concat($scope.eliminadosMedioContacto);
//                            }

//                            if ($scope.saveMediosContactoWeb != null) {
//                                personasService.savePersonaContactoWeb({ personaContactoWeb: $scope.saveMediosContactoWeb })
//                                    .success(function () {
//                                        $scope.cargarMediosContactoPersona();

//                                    }).error(function (error) {
//                                        $scope.mensajeError = error.message;
//                                    });
//                            }
//                        };

//                        //Funcion para cargar el formulario de agregar una nueva persona
//                        $scope.cargarNuevaPersona = function () {
//                            //Se vuelven a cargar los controles para ubicarlos en la posicion No Identificado
//                            cargarProfesiones();
//                            cargarEstadoCivil();

//                            $scope.mostrarDocumentoPersonaExiste = false;
//                            $scope.mostrarConsultaPersona = false;
//                            $scope.mostrarMasinformacion = true;
//                            $scope.soloLecturaPersona = true;

//                            $scope.mostrarEdadfecha = true;
//                            $scope.tabsDisable = true;
//                            $scope.mensajeError = "";
//                        };

//                        //Funcion para consultar la persona y cargar el div inferior correspondiente para actualizar persona, agregar nueva persona o para mostrar mensaje de que ya existe (boton Iniciar registro)
//                        $scope.consultarPersona = function () {

//                            if ($scope.paisSeleccionado != undefined) {
//                                $scope.nuevaPersona.paisId = $scope.paisSeleccionado;
//                            }

//                            personasService.getPersonas($scope.nuevaPersona)
//                            .success(function (data) {

//                                $scope.tabsDisable = true;

//                                if (data.personas != null) {

//                                    if (data.puedeSeleccionarPersona == true) {

//                                        $scope.mostrarDocumentoPersonaExiste = false;
//                                        $scope.mostrarConsultaPersona = true;

//                                    } else if (data.puedeSeleccionarPersona == false) {

//                                        $scope.mostrarConsultaPersona = false;
//                                        $scope.mostrarDocumentoPersonaExiste = true;

//                                    }

//                                    $scope.resultadoPersona = data.personas;
//                                    $scope.mostrarMasinformacion = false;

//                                } else {
//                                    $scope.cargarNuevaPersona();
//                                }

//                                $scope.limpiarFormulario();

//                            }).error(function (error) {
//                                if (error.validationErrors != null) {
//                                    $scope.mensajeError = error.validationErrors[0].message;
//                                } else {
//                                    $scope.mensajeError = error.message;
//                                }
//                            })
//                        };

//                        //Funcion para consultar la persona y cargarla para editar sus datos, es la funcion del button (esta es la persona)
//                        $scope.cargarPersona = function (personaId) {

//                            personasService.getPersonaEditar({ id: personaId })
//                            .success(function (data) {

//                                if (data != null) {

//                                    $scope.tabsDisable = false;
//                                    $scope.nuevaPersona.id = data.id;

//                                    $scope.nuevaPersona.nombre = data.nombre;
//                                    $scope.nuevaPersona.apellido1 = data.apellido1;
//                                    $scope.nuevaPersona.apellido2 = data.apellido2;

//                                    $scope.nuevaPersona.tieneDocumento = data.tieneDocumento;
//                                    $scope.mostrarDocumento = data.tieneDocumento;

//                                    $scope.nuevaPersona.tipoDocumentoId = data.tipoDocumentoId;
//                                    $scope.nuevaPersona.numeroDocumento = data.numeroDocumento;

//                                    if (data.fechaExpDocumento != null) {
//                                        if (data.fechaExpDocumento != "") {
//                                            $scope.nuevaPersona.fechaExpDocumento = data.fechaExpDocumento.substring(0, 10);
//                                        }
//                                    }

//                                    $scope.fechaNacimientoOculta = data.tieneFechaNacimiento;
//                                    $scope.nuevaPersona.tieneFechaNacimiento = data.tieneFechaNacimiento;

//                                    if (data.tieneFechaNacimiento == true) {
//                                        $scope.nuevaPersona.fechaNacimiento = data.fechaNacimiento.substring(0, 10);
//                                        $scope.edadcalculada = data.edad;

//                                        $scope.fechaedad = true;
//                                        $scope.mostrarEdadfecha = false;
//                                    }
//                                    else if (data.tieneFechaNacimiento == false && data.fechaNacimiento != null) {
//                                        $scope.fechacalculada = data.fechaNacimiento.substring(0, 10);
//                                        $scope.controlIndependiente.edad = data.edad;

//                                        $scope.fechaedad = false;
//                                        $scope.mostrarEdadfecha = true;
//                                    } else {
//                                        $scope.mostrarEdadfecha = true;
//                                    }

//                                    if (data.genero == "M") {
//                                        $scope.nuevaPersona.genero = "Masculino"
//                                    }
//                                    else {
//                                        $scope.nuevaPersona.genero = "Femenino"
//                                    }

//                                    $scope.nuevaPersona.discapacitada = data.discapacitada;

//                                    if (data.fechaFallecimiento != null) {
//                                        $scope.nuevaPersona.fechaFallecimiento = data.fechaFallecimiento.substring(0, 10);
//                                    }

//                                    //Proceso para ubicar el typeahead de profesion de la persona
//                                    profesionPersona = {
//                                        id: data.tipoProfesionId,
//                                        nombre: data.tipoProfesionNombre
//                                    }

//                                    $scope.controlIndependiente.profesionId = profesionPersona;

//                                    $scope.nuevaPersona.tipoEstadoCivilId = data.tipoEstadoCivilId;

//                                    $scope.nuevaPersona.contactarTelefono = data.contactarTelefono;
//                                    $scope.nuevaPersona.contactarCorreo = data.contactarCorreo;
//                                    $scope.nuevaPersona.contactarSms = data.contactarSms;

//                                    $scope.nuevaPersona.correoElectronico = data.correoElectronico;

//                                    $scope.cargarTelefonosPersona();
//                                    $scope.cargarDireccionesPersona();
//                                    $scope.cargarMediosContacto();
//                                    $scope.cargarMediosContactoPersona();

//                                    cargarPreferencias();

//                                    $scope.mostrarMasinformacion = true;
//                                    $scope.mostrarConsultaPersona = false;

//                                    //Procesos para cargar los datos de la persona si viene como parametro
//                                    if ((personaEstado.personaId != 0 && personaEstado.estado == 'E') || (personaEstado.personaId != 0 && personaEstado.estado == 'C')) {

//                                        //Proceso para ubicar el typeahead de nacionalidad (paisId) de la persona
//                                        nacionalidadPersona = {
//                                            id: data.paisId,
//                                            nombre: data.paisNombre,
//                                            indicativo: data.indicativo
//                                        }
//                                        //$scope.controlIndependiente.paisId = nacionalidadPersona;
//                                        $scope.selectedPais = nacionalidadPersona;

//                                        //Se carga los tipos de documento de la nacionalidad de la persona indicada
//                                        $scope.cargarTiposDocumento(nacionalidadPersona);

//                                        if (personaEstado.personaId != 0 && personaEstado.estado == 'C') {
//                                            $scope.soloLecturaConsultar = true;
//                                            $scope.soloLecturaPersona = true;
//                                        } else {
//                                            $scope.soloLecturaPersona = false;
//                                            $scope.soloLecturaPersona = false;
//                                        }

//                                    } else {
//                                        $scope.soloLecturaPersona = true;
//                                        $scope.soloLecturaConsultar = false;
//                                    }


//                                    //Se llama la funcion para calcular el rango de la fecha de fallecimiento segun fecha nacimiento
//                                    $scope.fechaFallecimiento(personaId);



//                                } else {
//                                    $scope.mostrarMasinformacion = false;
//                                    $scope.mostrarConsultaPersona = false;
//                                }

//                            }).error(function (error) {
//                                $scope.mensajeError = error.message;
//                            })
//                        };

//                        //Funcion para validar si se paso id de la persona por el parametro desde el link para cargar la edición de la persona
//                        if ((personaEstado.personaId != 0 && personaEstado.estado == 'E') || (personaEstado.personaId != 0 && personaEstado.estado == 'C')) {
//                            $scope.cargarPersona(personaEstado.personaId);
//                        };

//                        //Funcion cargar datos del Tab Contacto, con los telefonos de la persona
//                        $scope.cargarTelefonosPersona = function () {
//                            personasService.getTelefonosPersona({ personaId: $scope.nuevaPersona.id })
//                            .success(function (data) {
//                                if (data != null) {
//                                    $scope.telefonosPersona = data.telefonosPersona;

//                                    $scope.personaId = $scope.nuevaPersona.id;

//                                    //Variable para capturar la cantidad de telefonos activos para mostrar en el tab
//                                    $scope.cantidadTelefonos = 0;

//                                    for (var i = 0; i < $scope.telefonosPersona.length; i++) {
//                                        if ($scope.telefonosPersona[i].nombreEstado === true) {
//                                            $scope.cantidadTelefonos = $scope.cantidadTelefonos + 1;
//                                        }
//                                    }
//                                }
//                            })
//                        };

//                        function cargarTipoUbicacion() {
//                            parametrosService.getTiposByParametroUbicacion()
//                            .success(function (data) {
//                                $scope.tipos = data.tipos;
//                            })
//                        };
//                        cargarTipoUbicacion();


//                        $scope.limpiarFormulario = function () {
//                            $scope.nuevaPersona.fechaExpDocumento = null;
//                            $scope.nuevaPersona.tieneFechaNacimiento = false;
//                            $scope.nuevaPersona.fechaNacimiento = null;
//                            $scope.nuevaPersona.genero = "Masculino";

//                            $scope.nuevaPersona.discapacitada = false;
//                            $scope.nuevaPersona.fechaFallecimiento = null;
//                            $scope.nuevaPersona.paisId = "";

//                            $scope.nuevaPersona.contactarTelefono = false;
//                            $scope.nuevaPersona.contactarCorreo = false;

//                            $scope.controlIndependiente.edad = "";
//                            $scope.edadcalculada = "";
//                            $scope.fechacalculada = "";

//                            $scope.mensajeError = "";
//                            $scope.fechaedad = false;
//                        };

//                        $scope.cancelModal = function () {
//                            $modalInstance.close('cancel');
//                        }

//                        ///*************************** Direcciones Persona *********************************

//                        ////Funcion cargar datos del Tab Contacto, con las direcciones de la persona
//                        $scope.cargarDireccionesPersona = function () {
//                            personasService.getDireccionesPersona({ personaId: $scope.nuevaPersona.id })
//                            .success(function (data) {
//                                if (data != null) {
//                                    $scope.direccionesPersona = data.direccionesPersona;

//                                    $scope.cantidadDirecciones = 0;

//                                    for (var i = 0; i < $scope.direccionesPersona.length; i++) {
//                                        if ($scope.direccionesPersona[i].nombreEstado === true) {
//                                            $scope.cantidadDirecciones = $scope.cantidadDirecciones + 1;
//                                        }
//                                    }
//                                }
//                            })
//                        }


//                        // **************************** Medios de Contacto (Pill Web) ********************

//                        //Funcion para cargar el dropdown de medios de contacto para asignar
//                        $scope.cargarMediosContacto = function () {
//                            personasService.getContactosWebFilterByPersona({ personaId: $scope.nuevaPersona.id })
//                                .success(function (data) {
//                                    $scope.tiposMedioContacto = data.contactosWeb;
//                                    $scope.controlIndependiente.medioContacto = $scope.tiposMedioContacto[0];
//                                }).error(function (error) {
//                                    $scope.mensajeError = error.message;
//                                });
//                        }

//                        //Funcion para cargar los medios de contacto de la persona indicada
//                        $scope.cargarMediosContactoPersona = function () {
//                            personasService.getPersonaContactoWeb({ personaId: $scope.nuevaPersona.id })
//                                .success(function (data) {
//                                    $scope.mediosContactoPersona = data.mediosContactoPersona;
//                                }).error(function (error) {
//                                    $scope.mensajeError = error.message;
//                                });
//                        }

//                        //Funcion para mostrar los controles cuando da click en la primera opcion de eliminar
//                        $scope.eliminarMedioContacto = function (medioId, $index) {
//                            $scope.mensajeEliminar[$index] = true;
//                        };

//                        //Funcion para ocultar los controles cancelar y eliminar y volver a la forma normal
//                        $scope.eliminarMedioContactoCancel = function ($index) {
//                            $scope.mensajeEliminar[$index] = false;
//                        }

//                        //Funcion para editar un medio de contacto
//                        $scope.editarMedioContactoWeb = function (medioPersona, $index) {

//                            $scope.indiceEditar = $index;

//                            var medioEditado =
//                               {
//                                   id: medioPersona.tipoId,
//                                   nombre: medioPersona.medioContactoNombre
//                               }

//                            $scope.tiposMedioContacto.push(medioEditado);

//                            //Ubico la lista en la posición que se va a editar
//                            $scope.mediosContactoPersona[$index];

//                            $scope.controlIndependiente.identificador = medioPersona.identificador;
//                            $scope.controlIndependiente.medioContacto = medioEditado;
//                            $scope.medioContactoNuevo = false;

//                        }

//                        //Función para almacenar temporalmente el medio de contacto indicado; los valores de input y select se deben pasar por parametros en la funcion para que funcione
//                        $scope.asignarMedioContacto = function () {
//                            //Se elimina el medio de contacto asignado del dropdown
//                            for (var i = 0; i < $scope.tiposMedioContacto.length; i++) {
//                                if ($scope.tiposMedioContacto[i].id == $scope.controlIndependiente.medioContacto.id) {
//                                    $scope.tiposMedioContacto.splice(i, 1);
//                                }
//                            }

//                            if ($scope.medioContactoNuevo == true) {
//                                var nuevoMedioContacto =
//                                      {
//                                          personaId: $scope.nuevaPersona.id,
//                                          identificador: $scope.controlIndependiente.identificador,
//                                          medioContactoNombre: $scope.controlIndependiente.medioContacto.nombre,
//                                          tipoId: $scope.controlIndependiente.medioContacto.id,
//                                          tipoCambio: 'N'
//                                      };

//                                $scope.mediosContactoPersona.push(nuevoMedioContacto);

//                                abp.notify.info(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_notificacionAsignadoPersonaContactoWeb', 'Bow'),
//                                abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));

//                            } else {
//                                $scope.mediosContactoPersona[$scope.indiceEditar].tipoId = $scope.controlIndependiente.medioContacto.id;
//                                $scope.mediosContactoPersona[$scope.indiceEditar].medioContactoNombre = $scope.controlIndependiente.medioContacto.nombre;
//                                $scope.mediosContactoPersona[$scope.indiceEditar].identificador = $scope.controlIndependiente.identificador

//                                if ($scope.mediosContactoPersona[$scope.indiceEditar].id != null) {
//                                    $scope.mediosContactoPersona[$scope.indiceEditar].tipoCambio = 'M';
//                                }
//                            }

//                            $scope.controlIndependiente.identificador = "";
//                            $scope.controlIndependiente.medioContacto = $scope.tiposMedioContacto[0];

//                            $scope.medioContactoNuevo = true;

//                        }


//                        $scope.eliminarMedioContactoOk = function (mediocontacto, $index) {

//                            $scope.mensajeEliminar[$index] = false;

//                            //Variable para ir almacenando los medios de contacto web eliminados si vienen desde la BD
//                            var medioEliminado =
//                                {
//                                    id: $scope.mediosContactoPersona[$index].tipoId,
//                                    nombre: $scope.mediosContactoPersona[$index].medioContactoNombre
//                                }

//                            //Se asigna el tipo eliminado al dropdown de tipos de medio contacto
//                            $scope.tiposMedioContacto.push(medioEliminado);

//                            var eliminadoMedio =
//                            {
//                                id: mediocontacto.id,
//                                personaId: mediocontacto.personaId,
//                                identificador: mediocontacto.identificador,
//                                medioContactoNombre: mediocontacto.medioContactoNombre,
//                                tipoId: mediocontacto.tipoId,
//                                tipoCambio: 'E'
//                            }

//                            if (mediocontacto.id != null) {
//                                $scope.eliminadosMedioContacto.push(eliminadoMedio);
//                            }

//                            //Se elimina el medio de contacto asignado de la lista
//                            $scope.mediosContactoPersona.splice($index, 1);

//                            $scope.controlIndependiente.medioContacto = $scope.tiposMedioContacto[0];

//                        }

//                        //************************  Tab Informacion Personal ***************************
//                        function cargarPreferencias() {
//                            personasService.getPreferenciaPersona()
//                               .success(function (data) {
//                                   $scope.preferencias = data.preferencias;

//                                   cargarOpcionPreferenciaPersona();

//                               }).error(function (error) {
//                                   $scope.mensajeError = error.message;
//                               });
//                        }

//                        function cargarOpcionPreferenciaPersona() {
//                            personasService.getOpcionPreferenciaPersona({ personaId: $scope.nuevaPersona.id })
//                               .success(function (data) {
//                                   $scope.opcionPreferenciaPersona = data.opcionPreferenciaPersona;

//                                   //Procedimiento para asignar en la lista de preferencias cargadas inicialmente con la preferencia de la persona indicada
//                                   if ($scope.opcionPreferenciaPersona != null) {
//                                       for (var i = 0; i < $scope.preferencias.length; i++) {
//                                           for (var j = 0; j < $scope.opcionPreferenciaPersona.length; j++) {
//                                               if ($scope.preferencias[i].id == $scope.opcionPreferenciaPersona[j].preferenciaId) {
//                                                   $scope.preferencias[i].tipoCambio = "B";
//                                                   $scope.preferencias[i].opcionPreferenciaId = $scope.opcionPreferenciaPersona[j].opcionPreferenciaId;
//                                                   $scope.preferencias[i].personaPreferenciaId = $scope.opcionPreferenciaPersona[j].id;
//                                               }
//                                           }
//                                       }
//                                   }
//                               }).error(function (error) {
//                                   $scope.mensajeError = error.message;
//                               });
//                        }

//                        $scope.changeOpcionPreferencia = function ($index) {
//                            if ($scope.preferencias[$index].tipoCambio == "B" || $scope.preferencias[$index].tipoCambio == "M") {
//                                $scope.preferencias[$index].tipoCambio = "M";
//                            } else {
//                                $scope.preferencias[$index].tipoCambio = "N";
//                            }
//                            $scope.preferencias[$index].personaId = $scope.nuevaPersona.id;

//                            //Almaceno el registro que se modifico o agrego para enviar al servicio
//                            var nuevaPreferenciaPersona = {
//                                id: $scope.preferencias[$index].personaPreferenciaId,
//                                personaId: $scope.preferencias[$index].personaId,
//                                tipoCambio: $scope.preferencias[$index].tipoCambio,
//                                opcionPreferenciaId: $scope.preferencias[$index].opcionPreferenciaId
//                            };

//                            $scope.savePreferenciasPersona.push(nuevaPreferenciaPersona);

//                        }

//                        // *********************** Tab Auditoria *******************
//                        $scope.cargarAuditoria = function () {
//                            personasService.getAuditoriaPersona({ personaId: $scope.nuevaPersona.id })
//                             .success(function (data) {
//                                 $scope.auditoriaPersona = data.auditoriaPersona;

//                             }).error(function (error) {
//                                 $scope.mensajeError = error.message;
//                             });
//                        }

//                    }],
//                templateUrl: '/App/Main/directives/bowPersona/bowPersona.cshtml'
//            };
//        })
//})();