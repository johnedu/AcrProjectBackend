(function () {
    //Nombre del Controlador
    var controllerId = 'app.views.afiliaciones.clienteProspecto';

    /*****************************************************************
     * 
     * CONTROLADOR CLIENTE PROSPECTO
     * 
     *****************************************************************/
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.parametros', 'abp.services.app.afiliaciones', 'abp.services.app.personas',
        function ($scope, $modal, parametrosService, afiliacionesService, personasService) {
            var vm = this;

            vm.activeStep = 1;
            vm.editandoBeneficiario = false;
            vm.eliminandoBeneficiario = false;
            vm.mostrarPasoDos = true;
            vm.mostrarPasoTres = true;
            vm.mostrarPasoCuatro = true;

            vm.soloLecturaBeneficiarios = true;

            vm.copiaAfiliados = [];
            vm.seCopiaAfiliados = false;

            vm.controlOperacionesPlanCliente = {};
            vm.controlOperacionesSucursal = {};
            vm.controlOperacionesLocalidad = {};
            vm.controlOperacionesTelefono = {};

            vm.gruposFamiliares = [];

            vm.planSeleccionado = {
                id: '',
                nombre: ''
            };

            vm.grupoSeleccionado = {
                id: '',
                nombre: '',
                planExequialId: ''
            };

            //Variables para inicializar los ng-show de los divs.
            vm.mostrarInformacion = true;
            vm.mostrarNoContinuar = false;
            vm.mostrarProspectos = true;

            //Variables que sincroniza el comportamiento la directiva de plan cliente prospecto
            vm.mostrarNucleoSeleccionado = false;

            //Variable para limpiar los grupos familiares de la directiva
            vm.gruposFamiliares = [];

            //Inicializamos los modelos
            vm.directivaTelefonoVisible = false;

            vm.nuevoGestionProspecto = {
                id: '',
                prospectoId: '',
                empleadoId: '',
                personaId: '',
                empresaAfiliada: '',
                observaciones: '',
                estadoNoAfiliacionId: '',
                funerariaAfiliadoId: '',
                grupoFamiliarId: '',
                sucursalId: '',
                localidadId: '',
            };

            vm.nuevaPersonaProspecto = {
                id: '',
                nombre: '',
                apellido1: '',
                apellido2: '',
                paisId: '',
                fechaNacimiento: ''
            };

            vm.nuevoBeneficiario = {
                id: '',
                gestionProspectoId: '',
                parentescoId: '',
                nombre: '',
                apellido1: '',
                apellido2: '',
                edad: '',
                ciudadResidenciaId: '',
                bebePorNacer: 'false'
            };

            vm.parentescos = [];

            vm.clienteProspecto = {
                empleado: [],
                sucursal: [],
                sucursalId: [],
                localidad: [],
                localidadId: [],
                telefono: [],
                direccion: []
            };

            vm.seleccionoEmpleado = function (empleado) {

                vm.clienteProspecto.empleado = empleado;

                vm.nuevoGestionProspecto.empleadoId = empleado.id;
                vm.nuevoGestionProspecto.sucursalId = empleado.sucursalId;
                vm.nuevoGestionProspecto.localidadId = empleado.localidadId;

                vm.clienteProspecto.sucursalId = empleado.sucursalId;
                vm.clienteProspecto.localidadId = empleado.localidadId;

                vm.ciudadResidenciaId = {
                    localidadId: empleado.localidadId,
                    localidad: empleado.localidad,
                    departamentoIndicativo: empleado.departamentoIndicativo,
                    departamentoId: empleado.departamentoId,
                    departamento: empleado.departamento,
                    paisIndicativo: empleado.paisIndicativo,
                    paisId: empleado.paisId,
                    pais: empleado.pais,
                    id: empleado.localidadId
                }

                vm.controlOperacionesSucursal.asignarSucursal(vm.clienteProspecto.sucursalId);
                vm.controlOperacionesLocalidad.asignarLocalidad(vm.clienteProspecto.localidadId);
                vm.controlOperacionesTelefono.seleccionarLocalidad(vm.clienteProspecto.localidadId);
            };

            vm.seleccionoSucursal = function (sucursal) {
                vm.controlOperacionesLocalidad.asignarLocalidad(sucursal.localidadId);
                vm.controlOperacionesTelefono.seleccionarLocalidad(sucursal.localidadId);
            };

            vm.obtenerLocalidad = function (localidad) {
                vm.controlOperacionesTelefono.seleccionarLocalidad(localidad.localidadId);
            };

            vm.mostrarDirectivaTelefono = function (mostrar) {
                vm.directivaTelefonoVisible = mostrar;
            };

            vm.notificacionTelefonoGuardado = function (telefono) {
                vm.clienteProspecto.telefono = telefono;
                vm.directivaTelefonoVisible = false;
            };

            vm.direccionRegistrada = function (direccion) {
                vm.clienteProspecto.direccion = direccion;
            };

            //Funcion para cargar los motivos de no afiliacion en el drop down
            vm.cargarMotivoNoAfiliacion = function () {
                parametrosService.getAllEstadosClienteProspecto()
                .success(function (data) {
                    vm.motivosNoAfiliacion = data.estados;
                })
                  .error(function (error) {
                      abp.notify.error(error.message);
                  });
            };

            vm.cargarMotivoNoAfiliacion();

            //funcion para guardar un prospecto
            vm.saveProspecto = function () {
                afiliacionesService.getProspecto({ telefonoId: vm.clienteProspecto.telefono.id, direccionId: vm.clienteProspecto.direccion.id })
                .success(function (data) {
                    if (data.gestionesProspecto.length == 0) {

                        afiliacionesService.saveProspecto({ telefonoId: vm.clienteProspecto.telefono.id, direccionId: vm.clienteProspecto.direccion.id })
                        .success(function (data) {
                            vm.nuevoGestionProspecto.prospectoId = data.id;
                            vm.activeStep = 2;
                            vm.mostrarProspectos = false;
                        })
                        .error(function (error) {
                            abp.notify.error(error.message);
                        });
                    } else {
                        vm.gestionesProspecto = data.gestionesProspecto;
                        vm.activeStep = 2;
                        vm.mostrarProspectos = true;
                    }
                })
                .error(function (error) {
                    abp.notify.error(error.message);
                });

            };

            //Funcion para calcular la fecha de nacimiento segun la edad indicada
            vm.calcularFecha = function (edad) {
                if (edad) {
                    var anioactual = new Date().getFullYear();
                    var anionacimiento = anioactual - edad;
                    var fechacalculada = '01/01/' + anionacimiento;
                }
                return fechacalculada;
            };

            //Funcion para guardar el cliente prospecto indicado (Persona)
            vm.saveInformacionContacto = function (formulario) {

                if (vm.edadContacto != null) {
                    vm.nuevaPersonaProspecto.fechaNacimiento = vm.calcularFecha(vm.edadContacto);
                }

                vm.nuevaPersonaProspecto.paisId = vm.clienteProspecto.localidad.paisId;

                if (vm.nuevaPersonaProspecto.nombre && vm.nuevaPersonaProspecto.apellido1) {
                    personasService.savePersonaProspecto(vm.nuevaPersonaProspecto)
                        .success(function (data) {
                            vm.nuevoGestionProspecto.personaId = data.id;
                            vm.nuevaPersonaProspecto.id = data.id;

                            vm.saveGestionProspecto(formulario);
                            vm.cargarPlanes();
                        })
                        .error(function (error) {
                            abp.notify.error(error.message);
                        });
                } else {
                    vm.saveGestionProspecto(formulario);
                }

            };

            //Función para guardar gestion contacto, el parametro de entrada (formulario) de la funcion es para identificar 
            //cual boton fue presionado (No Continuar o Asignar Beneficiarios) para mostrar el formulario.
            vm.saveGestionProspecto = function (formulario) {
                afiliacionesService.saveGestionProspecto(vm.nuevoGestionProspecto)
                 .success(function (data) {
                     vm.nuevoGestionProspecto.id = data.id;

                     //Se valida si el guardado viene desde el boton (Iniciar contacto)
                     if (vm.seCopiaAfiliados == true) {
                         vm.copiarAfiliadosProspecto();
                     }
                 })
                 .error(function (error) {
                     abp.notify.error(error.message);
                 });

                if (formulario === true) {
                    vm.activeStep = 3;
                } else {
                    //Variable para ocultar los formularios del paso 2 cuando se selecciona No Continuar
                    vm.mostrarPasoDos = false;
                    vm.mostrarNoContinuar = !vm.mostrarNoContinuar;
                }
            };

            //Funcion para capturar el valor de la funeraria seleccionada en el typeahead
            vm.seleccionoFuneraria = function (funeraria) {
                vm.nuevoGestionProspecto.funerariaAfiliadoId = funeraria.id;
            };

            //Función para almacenar la visita registrada
            vm.registrarVisita = function (form_step1) {

                form_step1.$setPristine();

                vm.nuevoGestionProspecto.grupoFamiliarId = "";

                afiliacionesService.saveGestionProspecto(vm.nuevoGestionProspecto)
                    .success(function () {

                        vm.limpiarFormulario();

                        abp.notify.success(abp.localization.localize('afiliaciones_clienteprospecto_notificacionAgregadoCliente', 'Bow'),
                        abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));
                    })
                    .error(function (error) {
                        abp.notify.error(error.message);
                    });
            };

            //funcion para limpiar el formulario
            vm.limpiarFormulario = function () {

                vm.clienteProspecto.empleado = "";
                vm.clienteProspecto.sucursal = "";
                vm.clienteProspecto.sucursalId = "";
                vm.clienteProspecto.localidad = "";
                vm.clienteProspecto.localidadId = "";
                vm.clienteProspecto.telefono = "";
                vm.clienteProspecto.direccion = "";
                vm.grupoFamiliarId = "";

                vm.edadContacto = "";
                vm.selectedFuneraria = "";

                vm.nuevaPersonaProspecto = {
                    id: '',
                    nombre: '',
                    apellido1: '',
                    apellido2: '',
                    paisId: '',
                    fechaNacimiento: ''
                };

                vm.nuevoGestionProspecto = {
                    id: '',
                    prospectoId: '',
                    empleadoId: '',
                    personaId: '',
                    empresaAfiliada: '',
                    observaciones: '',
                    estadoNoAfiliacionId: '',
                    funerariaAfiliadoId: '',
                    grupoFamiliarId: '',
                    sucursalId: '',
                    localidadId: '',
                };

                vm.planSeleccionado = {
                    id: '',
                    nombre: ''
                };

                vm.grupoSeleccionado = {
                    id: '',
                    nombre: '',
                    planExequialId: ''
                };

                vm.parentescos = [];
                vm.copiaAfiliados = [];

                vm.seCopiaAfiliados = false;
                vm.mostrarNoContinuar = false;
                vm.activeStep = 1;
                vm.mostrarPasoDos = true;
                vm.mostrarPasoTres = true;
                vm.mostrarPasoCuatro = true;
            }

            //************* Asignar Beneficiarios *************

            //Función para cancelar la funcionalidad de agregar nuevo beneficiario (botón cancelar)
            vm.cancelarNuevoBeneficiario = function () {
                vm.limpiarFormularioBeneficiarios();
            };

            //Función para guardar un nuevo beneficiario (botón registrar)
            vm.saveBeneficiario = function () {

                vm.nuevoBeneficiario.parentescoId = vm.parentescoId.id;
                vm.nuevoBeneficiario.ciudadResidenciaId = vm.ciudadResidenciaId.id;
                vm.nuevoBeneficiario.gestionProspectoId = vm.nuevoGestionProspecto.id;

                afiliacionesService.saveAfiliadoProspecto(vm.nuevoBeneficiario)
                  .success(function () {

                      vm.cargarParentescosCliente();
                      vm.limpiarFormularioBeneficiarios();

                      abp.notify.success(abp.localization.localize('afiliaciones_clienteprospecto_notificacionAgregadoBeneficiario', 'Bow'),
                      abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));
                  })
                  .error(function (error) {
                      abp.notify.warn(error.message, abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));
                  });
            };

            //Función para cargar los parentescos asignados al cliente prospecto
            vm.cargarParentescosCliente = function () {

                afiliacionesService.getAfiliadosProspecto({ id: vm.nuevoGestionProspecto.id })
                  .success(function (data) {
                      vm.parentescos = data.afiliadosProspecto;
                      vm.cargarPlanes();
                  })
                  .error(function (error) {
                      abp.notify.error(error.message);
                  });
            };

            //Función para verificar si se puede eliminar el registro indicado
            vm.puedeEliminarBeneficiario = function (parentescoAfiliado, funcionRetornarPuedeEliminar) {
                vm.eliminandoBeneficiario = true;
                funcionRetornarPuedeEliminar(true);
            };

            //Funcion para eliminar un parentesco asociado desde la tabla parentescos
            vm.eliminarBeneficiarioOk = function (parentescoAfiliado) {
                afiliacionesService.deleteAfiliadoProspecto({ id: parentescoAfiliado })
                   .success(function () {

                       vm.limpiarFormularioBeneficiarios();

                       abp.notify.info(abp.localization.localize('afiliaciones_clienteprospecto_notificacionEliminadoParentesco', 'Bow'),
                       abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));

                       vm.cargarParentescosCliente();
                       vm.eliminandoBeneficiario = false;

                   })
                   .error(function (error) {
                       abp.notify.error(error.message);
                   });
            };

            vm.eliminarBeneficiarioCancel = function () {
                vm.eliminandoBeneficiario = false;
            };

            //Funcion para cargar los planes que puede tener el cliente segun sucursal, edad y parentesco que se indicaron
            vm.cargarPlanes = function () {
                afiliacionesService.getPlanesProspecto({ sucursalId: vm.clienteProspecto.sucursalId, parentescos: vm.parentescos })
                 .success(function (data) {
                     vm.mostrarNucleoSeleccionado = false;

                     vm.controlOperacionesPlanCliente.cargarNucleosPlan(vm.planSeleccionado, vm.grupoSeleccionado, data);
                 })
                 .error(function (error) {
                     abp.notify.error(error.message);
                 });
            };

            //Funcion para editar los datos de un parentesco
            vm.editarParentesco = function (parentescoAfiliado) {
                afiliacionesService.getAfiliadoProspecto({ id: parentescoAfiliado })
                    .success(function (data) {
                        vm.soloLecturaBeneficiarios = false;

                        vm.editandoBeneficiario = true;
                        vm.nombreBeneficiarioEditar = data.nombre + ' ' + data.apellido1 + ' ' + data.apellido2;

                        vm.parentescoId = {
                            id: data.parentescoId,
                            nombre: data.parentescoNombre
                        }

                        vm.ciudadResidenciaId = {
                            id: data.ciudadResidenciaId,
                            localidadId: data.localidadId,
                            localidad: data.localidad,
                            departamentoIndicativo: data.departamentoIndicativo,
                            departamentoId: data.departamentoId,
                            departamento: data.departamento,
                            paisIndicativo: data.paisIndicativo,
                            paisId: data.paisId,
                            pais: data.pais
                        }

                        vm.nuevoBeneficiario = {
                            id: data.id,
                            edad: data.edad,
                            nombre: data.nombre,
                            apellido1: data.apellido1,
                            apellido2: data.apellido2,
                            bebePorNacer: data.bebePorNacer
                        };
                    })
            };

            //Función para limpiar el formulario de beneficiarios
            vm.limpiarFormularioBeneficiarios = function () {
                //$scope.frmBeneficiario.$setPristine();

                vm.parentescoId = "";
                vm.soloLecturaBeneficiarios = true;

                vm.nuevoBeneficiario = {
                    id: '',
                    gestionProspectoId: '',
                    parentescoId: '',
                    nombre: '',
                    apellido1: '',
                    apellido2: '',
                    edad: '',
                    ciudadResidenciaId: '',
                    bebePorNacer: 'false'
                };
            };

            //función para guardar una afiliacion
            vm.saveAfiliacion = function (form_step1) {
                //grupoFamiliarId seleccionado en la directiva de planes cliente prospecto
                if (vm.grupoFamiliarId) {

                    afiliacionesService.saveAfiliacion({ grupoFamiliarId: vm.grupoFamiliarId, gestionProspectoId: vm.nuevoGestionProspecto.id, beneficiosAdicionales: vm.adicionales })
                    .success(function () {

                        vm.stepsDone = true;

                        abp.notify.success(abp.localization.localize('afiliaciones_clienteprospecto_notificacionAgregadoAfiliacion', 'Bow'),
                        abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));

                        form_step1.$setPristine();
                        vm.limpiarFormulario();
                    })
                    .error(function (error) {
                        abp.notify.error(error.message);
                    });
                } else {
                    abp.notify.warn(abp.localization.localize('afiliaciones_clienteprospecto_notificacionSeleccionarPlan', 'Bow'),
                    abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));
                }
            };


            //******** Iniciar Contacto ***********

            //Funcion para cargar toda a información de la gestion prospecto seleccionada (Iniciar Contacto)
            vm.iniciarContacto = function (gestion) {

                afiliacionesService.getGestionProspectoIniciarContacto({ id: gestion.id, sucursalId: vm.nuevoGestionProspecto.sucursalId })
                .success(function (data) {

                    var fechaN = "";
                    var funerariaAfiliadoId;
                    var estadoNoAfiliacionId;
                    var grupoFamiliarId;
                    vm.edadContacto = "";

                    //Si el cliente prospecto tiene fecha de nacimiento se calcula la edad y se carga en el input
                    if (data.fechaNacimiento != null) {
                        fechaN = data.fechaNacimiento.substring(0, 10);
                        var fechaNacimiento = vm.calcularEdad(fechaN);
                        vm.edadContacto = fechaNacimiento;
                    }

                    //Se carga la información del cliente prospecto
                    vm.nuevaPersonaProspecto = {
                        id: '',
                        nombre: data.nombre,
                        apellido1: data.apellido1,
                        apellido2: data.apellido2,
                        paisId: data.paisId,
                        fechaNacimiento: fechaN
                    };

                    //Se debe reasignar las llaves foraneas del modelo cuando vienen con valores 0 colocarlos '' para poder guardarlos
                    if (data.estadoNoAfiliacionId == 0) {
                        estadoNoAfiliacionId = '';
                    } else {
                        estadoNoAfiliacionId = data.estadoNoAfiliacionId;
                    }

                    //Si la gestion indicada no tiene funeraria afiliada Id se coloca '' para poder guardarla posteriormente
                    if (data.funerariaAfiliadoId == 0) {
                        funerariaAfiliadoId = '';
                    } else {
                        funerariaAfiliadoId = data.funerariaAfiliadoId;

                        //Se llena el modelo para la directiva de buscarfuneraria y se ubica en la posicion segun el valor de la gestion prospecto indicada
                        vm.funeraria = {
                            id: data.funerariaAfiliadoId,
                            nombre: data.funerariaAfiliadoNombre
                        };

                        vm.selectedFuneraria = vm.funeraria;
                    }

                    //Si la gestion indicada no tiene grupo familiar Id afiliada se coloca '' para poder guardarla posteriormente
                    if (data.grupoFamiliarId == 0) {
                        grupoFamiliarId = '';
                    } else {
                        grupoFamiliarId = data.grupoFamiliarId;
                    }

                    //Si la gestion indicada tiene grupo familiar id (plan seleccionado) pero no pertenece a la sucursal seleccionada no se cargan los datos del plan
                    if (data.grupoFamiliarId != "" && data.planExequialEnSucursal == true) {
                        vm.planSeleccionado = {
                            id: data.planExequialId,
                            nombre: data.planExequialNombre
                        };

                        vm.grupoSeleccionado = {
                            id: data.grupoFamiliarId,
                            nombre: data.grupoFamiliarNombre,
                            planExequialId: data.planExequialId
                        };
                    }

                    //Se carga la información de la gestion prospecto
                    vm.nuevoGestionProspecto = {
                        id: '',
                        prospectoId: data.prospectoId,
                        empleadoId: data.empleadoId,
                        personaId: data.personaId,
                        empresaAfiliada: data.empresaAfiliada,
                        observaciones: data.observaciones,
                        estadoNoAfiliacionId: estadoNoAfiliacionId,
                        funerariaAfiliadoId: funerariaAfiliadoId,
                        grupoFamiliarId: grupoFamiliarId,
                        sucursalId: vm.nuevoGestionProspecto.sucursalId,
                        localidadId: vm.nuevoGestionProspecto.localidadId
                    };

                    vm.copiaAfiliados = data.afiliados;
                    vm.seCopiaAfiliados = true;

                    //Se guarda el prospecto (telefonoId y direccionId) que se indico al inicio
                    afiliacionesService.saveProspecto({ telefonoId: vm.clienteProspecto.telefono.id, direccionId: vm.clienteProspecto.direccion.id })
                       .success(function (data) {
                           vm.nuevoGestionProspecto.prospectoId = data.id;
                           vm.mostrarProspectos = false;
                       })
                       .error(function (error) {
                           $scope.mensajeError = error.message;
                       });

                })
                .error(function (error) {
                    $scope.mensajeError = error.message;
                });
            };

            //Funcion para copiar los afiliados prospecto con la gestion prospecto nueva.
            vm.copiarAfiliadosProspecto = function () {
                afiliacionesService.copiarAfiliadosProspecto({ gestionProspectoId: vm.nuevoGestionProspecto.id, afiliados: vm.copiaAfiliados })
                      .success(function (data) {
                          vm.cargarParentescosCliente();
                      })
                      .error(function (error) {
                          $scope.mensajeError = error.message;
                      });
                vm.seCopiaAfiliados = false;
            };

            //Funcion para calcular la edad segun la fecha de nacimiento indicada
            vm.calcularEdad = function (fechanacimiento) {
                var birthday = new Date(fechanacimiento);
                return ~~((Date.now() - birthday) / (31557600000));
            };



            vm.saveOrUpdateBeneficiario = function (frmBeneficiario) {
                vm.saveBeneficiario();
                frmBeneficiario.$setPristine();
            }

            //Controles para editar o eliminar un beneficiario
            vm.controles = {
                visible: [],
                mostrar: function ($index) {
                    vm.controles.visible[$index] = true;
                    vm.eliminandoBeneficiario = false;
                },
                ocultar: function ($index) {
                    vm.controles.visible[$index] = false;
                }
            };

            vm.cancelarUpdateBeneficiario = function (frmBeneficiario) {
                if (vm.editandoBeneficiario) {
                    vm.editandoBeneficiario = false;
                }
                frmBeneficiario.$setPristine();
                vm.limpiarFormularioBeneficiarios();
            };

            vm.noContinuar = function () {
                if (vm.activeStep == 3) {
                    vm.mostrarPasoTres = false;
                } else {
                    vm.mostrarPasoCuatro = false;
                }
                vm.mostrarNoContinuar = true;
            };

        }]);
})();



//(function () {
//    //Nombre del Controlador
//    var controllerId = 'app.views.afiliaciones.clienteProspecto';

//    /*****************************************************************
//     * 
//     * CONTROLADOR CLIENTE PROSPECTO
//     * 
//     *****************************************************************/
//    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.parametros', 'abp.services.app.afiliaciones', 'abp.services.app.personas',
//        function ($scope, $modal, parametrosService, afiliacionesService, personasService) {
//            var vm = this;

//            vm.activeStep = 1;

//            vm.soloLecturaBeneficiarios = true;
//            vm.copiaAfiliados = [];
//            vm.seCopiaAfiliados = false;

//            vm.controlOperacionesPlanCliente = {};
//            vm.controlOperacionesSucursal = {};
//            vm.controlOperacionesLocalidad = {};
//            vm.controlOperacionesTelefono = {};

//            vm.gruposFamiliares = [];

//            vm.planSeleccionado = {
//                id: '',
//                nombre: ''
//            };

//            vm.grupoSeleccionado = {
//                id: '',
//                nombre: '',
//                planExequialId: ''
//            };

//            //Variables para inicializar los ng-show de los divs.
//            vm.mostrarInformacion = true;
//            vm.mostrarInformacionContacto = true;
//            vm.mostrarNoContinuar = true;
//            vm.mostrarProspectos = true;

//            //Variables que sincroniza el comportamiento la directiva de plan cliente prospecto
//            vm.mostrarNucleoSeleccionado = false;

//            //Variable para limpiar los grupos familiares de la directiva
//            vm.gruposFamiliares = [];

//            //Inicializamos los modelos
//            vm.directivaTelefonoVisible = false;

//            vm.nuevoGestionProspecto = {
//                id: '',
//                prospectoId: '',
//                empleadoId: '',
//                personaId: '',
//                empresaAfiliada: '',
//                observaciones: '',
//                estadoNoAfiliacionId: '',
//                funerariaAfiliadoId: '',
//                grupoFamiliarId: '',
//                sucursalId: '',
//                localidadId: '',
//            };

//            vm.nuevaPersonaProspecto = {
//                id: '',
//                nombre: '',
//                apellido1: '',
//                apellido2: '',
//                paisId: '',
//                fechaNacimiento: ''
//            };

//            vm.nuevoBeneficiario = {
//                id: '',
//                gestionProspectoId: '',
//                parentescoId: '',
//                nombre: '',
//                apellido1: '',
//                apellido2: '',
//                edad: '',
//                ciudadResidenciaId: '',
//                bebePorNacer: 'false'
//            };

//            vm.parentescos = [];

//            vm.clienteProspecto = {
//                empleado: [],
//                sucursal: [],
//                sucursalId: [],
//                localidad: [],
//                localidadId: [],
//                telefono: [],
//                direccion: []
//            };

//            vm.seleccionoEmpleado = function (empleado) {

//                vm.clienteProspecto.empleado = empleado;

//                vm.nuevoGestionProspecto.empleadoId = empleado.id;
//                vm.nuevoGestionProspecto.sucursalId = empleado.sucursalId;
//                vm.nuevoGestionProspecto.localidadId = empleado.localidadId;

//                vm.clienteProspecto.sucursalId = empleado.sucursalId;
//                vm.clienteProspecto.localidadId = empleado.localidadId;

//                vm.ciudadResidenciaId = {
//                    localidadId: empleado.localidadId,
//                    localidad: empleado.localidad,
//                    departamentoIndicativo: empleado.departamentoIndicativo,
//                    departamentoId: empleado.departamentoId,
//                    departamento: empleado.departamento,
//                    paisIndicativo: empleado.paisIndicativo,
//                    paisId: empleado.paisId,
//                    pais: empleado.pais,
//                    id: empleado.localidadId
//                }

//                vm.nuevaPersonaProspecto.paisId = vm.clienteProspecto.localidad.paisId;

//                vm.controlOperacionesSucursal.asignarSucursal(vm.clienteProspecto.sucursalId);
//                vm.controlOperacionesLocalidad.asignarLocalidad(vm.clienteProspecto.localidadId);
//                vm.controlOperacionesTelefono.seleccionarLocalidad(vm.clienteProspecto.localidadId);
//            };

//            vm.seleccionoSucursal = function (sucursal) {
//                vm.controlOperacionesLocalidad.asignarLocalidad(sucursal.localidadId);
//                vm.controlOperacionesTelefono.seleccionarLocalidad(sucursal.localidadId);
//            };

//            vm.obtenerLocalidad = function (localidad) {
//                vm.controlOperacionesTelefono.seleccionarLocalidad(localidad.localidadId);
//            };

//            vm.mostrarDirectivaTelefono = function (mostrar) {
//                vm.directivaTelefonoVisible = mostrar;
//            };

//            vm.notificacionTelefonoGuardado = function (telefono) {
//                vm.clienteProspecto.telefono = telefono;
//                vm.directivaTelefonoVisible = false;
//            };

//            vm.direccionRegistrada = function (direccion) {
//                vm.clienteProspecto.direccion = direccion;
//            };

//            //Funcion para cargar los motivos de no afiliacion en el drop down
//            vm.cargarMotivoNoAfiliacion = function () {
//                parametrosService.getAllEstadosClienteProspecto()
//                .success(function (data) {
//                    vm.motivosNoAfiliacion = data.estados;
//                })
//                  .error(function (error) {
//                      $scope.mensajeError = error.message;
//                  });
//            };

//            vm.cargarMotivoNoAfiliacion();

//            //funcion para guardar un prospecto
//            vm.saveProspecto = function () {

//                afiliacionesService.getProspecto({ telefonoId: vm.clienteProspecto.telefono.id, direccionId: vm.clienteProspecto.direccion.id })
//                .success(function (data) {

//                    if (data.gestionesProspecto.length == 0) {
//                        afiliacionesService.saveProspecto({ telefonoId: vm.clienteProspecto.telefono.id, direccionId: vm.clienteProspecto.direccion.id })
//                        .success(function (data) {
//                            vm.nuevoGestionProspecto.prospectoId = data.id;
//                            vm.mostrarInformacion = !vm.mostrarInformacion;
//                        })
//                        .error(function (error) {
//                            $scope.mensajeError = error.message;
//                        });
//                    } else {
//                        vm.gestionesProspecto = data.gestionesProspecto;
//                        vm.mostrarProspectos = false;
//                    }

//                })
//                .error(function (error) {
//                    $scope.mensajeError = error.message;
//                });

//            };

//            //Funcion para calcular la fecha de nacimiento segun la edad indicada
//            vm.calcularFecha = function (edad) {
//                if (edad) {
//                    var anioactual = new Date().getFullYear();
//                    var anionacimiento = anioactual - edad;
//                    var fechacalculada = '01/01/' + anionacimiento;
//                }
//                return fechacalculada;
//            };

//            //Funcion para editar del contacto (boton editar contacto)
//            vm.editarContacto = function () {
//                if (vm.mostrarInformacionContacto === false) {
//                    vm.mostrarInformacionContacto = true;
//                } else {
//                    vm.mostrarNoContinuar = true;
//                }
//            };

//            vm.saveInformacionContacto = function (formulario) {
//                //Se verifica cuantos input tienen valor para guardar una persona nueva 
//                if (vm.edadContacto != null) {
//                    vm.nuevaPersonaProspecto.fechaNacimiento = vm.calcularFecha(vm.edadContacto);
//                }
//                personasService.savePersonaProspecto(vm.nuevaPersonaProspecto)
//                    .success(function (data) {
//                        vm.nuevoGestionProspecto.personaId = data.id;
//                        vm.nuevaPersonaProspecto.id = data.id;

//                        vm.saveGestionProspecto(formulario);
//                        vm.cargarPlanes();
//                    })
//                    .error(function (error) {
//                        $scope.mensajeError = error.message;
//                    });
//            };

//            //Función para guardar gestion contacto, el parametro de entrada (formulario) de la funcion es para identificar 
//            //cual boton fue presionado (No Continuar o Asignar Beneficiarios) para mostrar el formulario.
//            vm.saveGestionProspecto = function (formulario) {
//                afiliacionesService.saveGestionProspecto(vm.nuevoGestionProspecto)
//                 .success(function (data) {
//                     vm.nuevoGestionProspecto.id = data.id;

//                     //Se valida si el guardado viene desde el boton (Iniciar contacto)
//                     if (vm.seCopiaAfiliados == true) {
//                         vm.copiarAfiliadosProspecto();
//                     }
//                 })
//                 .error(function (error) {
//                     $scope.mensajeError = error.message;
//                 });

//                if (formulario === true) {
//                    vm.mostrarInformacionContacto = !vm.mostrarInformacionContacto;
//                } else {
//                    vm.mostrarNoContinuar = !vm.mostrarNoContinuar;
//                }
//            };

//            //Funcion para capturar el valor de la funeraria seleccionada en el typeahead
//            vm.seleccionoFuneraria = function (funeraria) {
//                vm.nuevoGestionProspecto.funerariaAfiliadoId = funeraria.id;
//            };

//            //Función para almacenar la visita registrada
//            vm.registrarVisita = function () {

//                vm.nuevoGestionProspecto.grupoFamiliarId = "";

//                afiliacionesService.saveGestionProspecto(vm.nuevoGestionProspecto)
//                    .success(function () {

//                        vm.limpiarFormulario();

//                        abp.notify.success(abp.localization.localize('afiliaciones_clienteprospecto_notificacionAgregadoCliente', 'Bow'),
//                        abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));
//                    })
//                    .error(function (error) {
//                        $scope.mensajeError = error.message;
//                    });
//            };

//            //funcion para limpiar el formulario
//            vm.limpiarFormulario = function () {

//                vm.clienteProspecto.empleado = "";
//                vm.clienteProspecto.sucursal = "";
//                vm.clienteProspecto.sucursalId = "";
//                vm.clienteProspecto.localidad = "";
//                vm.clienteProspecto.localidadId = "";
//                vm.clienteProspecto.telefono = "";
//                vm.clienteProspecto.direccion = "";
//                vm.grupoFamiliarId = "";

//                vm.nuevaPersonaProspecto = {
//                    id: '',
//                    nombre: '',
//                    apellido1: '',
//                    apellido2: '',
//                    paisId: '',
//                    fechaNacimiento: ''
//                };

//                vm.nuevoGestionProspecto = {
//                    id: '',
//                    prospectoId: '',
//                    empleadoId: '',
//                    personaId: '',
//                    empresaAfiliada: '',
//                    observaciones: '',
//                    estadoNoAfiliacionId: '',
//                    funerariaAfiliadoId: '',
//                    grupoFamiliarId: '',
//                    sucursalId: '',
//                    localidadId: '',
//                };

//                vm.planSeleccionado = {
//                    id: '',
//                    nombre: ''
//                };

//                vm.grupoSeleccionado = {
//                    id: '',
//                    nombre: '',
//                    planExequialId: ''
//                };

//                vm.parentescos = [];
//                vm.copiaAfiliados = [];
//                vm.seCopiaAfiliados = false;

//                vm.edadContacto = "";
//                vm.selectedFuneraria = "";

//                vm.mostrarInformacion = !vm.mostrarInformacion;
//                vm.mostrarInformacionContacto = true;
//                vm.mostrarNoContinuar = true;

//                vm.mostrarPlan = false;
//            }

//            //************* Asignar Beneficiarios *************

//            //Función para activar el formulario para agregar nuevo beneficiario (botón nuevo)
//            vm.agregarNuevoBeneficiario = function () {
//                vm.soloLecturaBeneficiarios = false;
//            };

//            //Función para cancelar la funcionalidad de agregar nuevo beneficiario (botón cancelar)
//            vm.cancelarNuevoBeneficiario = function () {
//                vm.limpiarFormularioBeneficiarios();
//            };

//            //Función para guardar un nuevo beneficiario (botón registrar)
//            vm.saveBeneficiario = function () {

//                vm.nuevoBeneficiario.parentescoId = vm.parentescoId.id;
//                vm.nuevoBeneficiario.ciudadResidenciaId = vm.ciudadResidenciaId.id;
//                vm.nuevoBeneficiario.gestionProspectoId = vm.nuevoGestionProspecto.id;

//                afiliacionesService.saveAfiliadoProspecto(vm.nuevoBeneficiario)
//                  .success(function () {

//                      vm.cargarParentescosCliente();
//                      vm.limpiarFormularioBeneficiarios();

//                      abp.notify.success(abp.localization.localize('afiliaciones_clienteprospecto_notificacionAgregadoBeneficiario', 'Bow'),
//                      abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));
//                  })
//                  .error(function (error) {
//                      abp.notify.warn(error.message, abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));
//                  });
//            };

//            //Función para cargar los parentescos asignados al cliente prospecto
//            vm.cargarParentescosCliente = function () {

//                afiliacionesService.getAfiliadosProspecto({ id: vm.nuevoGestionProspecto.id })
//                  .success(function (data) {
//                      vm.parentescos = data.afiliadosProspecto;
//                      vm.cargarPlanes();
//                  })
//                  .error(function (error) {
//                      $scope.mensajeError = error.message;
//                  });
//            };

//            //Funcion para eliminar un parentesco asociado desde la tabla parentescos
//            vm.eliminarParentescoOk = function (parentescoAfiliado) {
//                afiliacionesService.deleteAfiliadoProspecto({ id: parentescoAfiliado })
//                   .success(function () {

//                       vm.limpiarFormularioBeneficiarios();

//                       abp.notify.info(abp.localization.localize('afiliaciones_clienteprospecto_notificacionEliminadoParentesco', 'Bow'),
//                       abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));

//                       vm.cargarParentescosCliente();
//                   })
//                   .error(function (error) {
//                       $scope.mensajeError = error.message;
//                   });
//            };

//            //Funcion para cargar los planes que puede tener el cliente segun sucursal, edad y parentesco que se indicaron
//            vm.cargarPlanes = function () {
//                afiliacionesService.getPlanesProspecto({ sucursalId: vm.clienteProspecto.sucursalId, parentescos: vm.parentescos })
//                 .success(function (data) {
//                     vm.mostrarNucleoSeleccionado = false;

//                     vm.controlOperacionesPlanCliente.cargarNucleosPlan(vm.planSeleccionado, vm.grupoSeleccionado, data);
//                 })
//                 .error(function (error) {
//                     $scope.mensajeError = error.message;
//                 });
//            };

//            //Función para verificar si se puede eliminar el registro indicado
//            vm.puedeEliminarParentesco = function (parentescoAfiliado, funcionRetornarPuedeEliminar) {
//                funcionRetornarPuedeEliminar(true);

//                //afiliacionesService.puedeEliminarParentesco({ id: parentescoId }).success(function (data) {
//                //funcionRetornarPuedeEliminar(data.puedeEliminar);
//                //});
//            };

//            //Funcion para editar los datos de un parentesco
//            vm.editarParentesco = function (parentescoAfiliado) {
//                afiliacionesService.getAfiliadoProspecto({ id: parentescoAfiliado })
//                    .success(function (data) {
//                        vm.soloLecturaBeneficiarios = false;

//                        vm.parentescoId = {
//                            id: data.parentescoId,
//                            nombre: data.parentescoNombre
//                        }

//                        vm.ciudadResidenciaId = {
//                            id: data.ciudadResidenciaId,
//                            localidadId: data.localidadId,
//                            localidad: data.localidad,
//                            departamentoIndicativo: data.departamentoIndicativo,
//                            departamentoId: data.departamentoId,
//                            departamento: data.departamento,
//                            paisIndicativo: data.paisIndicativo,
//                            paisId: data.paisId,
//                            pais: data.pais
//                        }

//                        vm.nuevoBeneficiario = {
//                            id: data.id,
//                            edad: data.edad,
//                            nombre: data.nombre,
//                            apellido1: data.apellido1,
//                            apellido2: data.apellido2,
//                            bebePorNacer: String(data.bebePorNacer)
//                        };
//                    })
//            };

//            //Función para limpiar el formulario de beneficiarios
//            vm.limpiarFormularioBeneficiarios = function () {
//                $scope.frmBeneficiario.$setPristine();

//                vm.parentescoId = "";
//                vm.soloLecturaBeneficiarios = true;

//                vm.nuevoBeneficiario = {
//                    id: '',
//                    gestionProspectoId: '',
//                    parentescoId: '',
//                    nombre: '',
//                    apellido1: '',
//                    apellido2: '',
//                    edad: '',
//                    ciudadResidenciaId: '',
//                    bebePorNacer: 'false'
//                };
//            };

//            //función para guardar una afiliacion
//            vm.saveAfiliacion = function () {
//                //grupoFamiliarId seleccionado en la directiva de planes cliente prospecto
//                if (vm.grupoFamiliarId) {

//                    afiliacionesService.saveAfiliacion({ grupoFamiliarId: vm.grupoFamiliarId, gestionProspectoId: vm.nuevoGestionProspecto.id, beneficiosAdicionales: vm.adicionales })
//                    .success(function () {

//                        abp.notify.success(abp.localization.localize('afiliaciones_clienteprospecto_notificacionAgregadoAfiliacion', 'Bow'),
//                        abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));

//                        vm.limpiarFormulario();
//                    })
//                    .error(function (error) {
//                        $scope.mensajeError = error.message;
//                    });
//                } else {
//                    abp.notify.warn(abp.localization.localize('afiliaciones_clienteprospecto_notificacionSeleccionarPlan', 'Bow'),
//                    abp.localization.localize('afiliaciones_clienteprospecto_informacion', 'Bow'));
//                }
//            };


//            //******** Iniciar Contacto ***********

//            //Funcion para cargar toda a información de la gestion prospecto seleccionada (Iniciar Contacto)
//            vm.iniciarContacto = function (gestion) {

//                afiliacionesService.getGestionProspectoIniciarContacto({ id: gestion.id, sucursalId: vm.nuevoGestionProspecto.sucursalId })
//                .success(function (data) {

//                    var fechaN = "";
//                    var funerariaAfiliadoId;
//                    var estadoNoAfiliacionId;
//                    var grupoFamiliarId;
//                    vm.edadContacto = "";

//                    //Si el cliente prospecto tiene fecha de nacimiento se calcula la edad y se carga en el input
//                    if (data.fechaNacimiento != null) {
//                        fechaN = data.fechaNacimiento.substring(0, 10);
//                        var fechaNacimiento = vm.calcularEdad(fechaN);
//                        vm.edadContacto = fechaNacimiento;
//                    }

//                    //Se carga la información del cliente prospecto
//                    vm.nuevaPersonaProspecto = {
//                        id: '',
//                        nombre: data.nombre,
//                        apellido1: data.apellido1,
//                        apellido2: data.apellido2,
//                        paisId: data.paisId,
//                        fechaNacimiento: fechaN
//                    };

//                    //Se debe reasignar las llaves foraneas del modelo cuando vienen con valores 0 colocarlos '' para poder guardarlos
//                    if (data.estadoNoAfiliacionId == 0) {
//                        estadoNoAfiliacionId = '';
//                    } else {
//                        estadoNoAfiliacionId = data.estadoNoAfiliacionId;
//                    }

//                    //Si la gestion indicada no tiene funeraria afiliada Id se coloca '' para poder guardarla posteriormente
//                    if (data.funerariaAfiliadoId == 0) {
//                        funerariaAfiliadoId = '';
//                    } else {
//                        funerariaAfiliadoId = data.funerariaAfiliadoId;

//                        //Se llena el modelo para la directiva de buscarfuneraria y se ubica en la posicion segun el valor de la gestion prospecto indicada
//                        vm.funeraria = {
//                            id: data.funerariaAfiliadoId,
//                            nombre: data.funerariaAfiliadoNombre
//                        };

//                        vm.selectedFuneraria = vm.funeraria;
//                    }

//                    //Si la gestion indicada no tiene grupo familiar Id afiliada se coloca '' para poder guardarla posteriormente
//                    if (data.grupoFamiliarId == 0) {
//                        grupoFamiliarId = '';
//                    } else {
//                        grupoFamiliarId = data.grupoFamiliarId;
//                    }

//                    //Si la gestion indicada tiene grupo familiar id (plan seleccionado) pero no pertenece a la sucursal seleccionada no se cargan los datos del plan
//                    if (data.grupoFamiliarId != "" && data.planExequialEnSucursal == true) {
//                        vm.planSeleccionado = {
//                            id: data.planExequialId,
//                            nombre: data.planExequialNombre
//                        };

//                        vm.grupoSeleccionado = {
//                            id: data.grupoFamiliarId,
//                            nombre: data.grupoFamiliarNombre,
//                            planExequialId: data.planExequialId
//                        };
//                    }

//                    //Se carga la información de la gestion prospecto
//                    vm.nuevoGestionProspecto = {
//                        id: '',
//                        prospectoId: data.prospectoId,
//                        empleadoId: data.empleadoId,
//                        personaId: data.personaId,
//                        empresaAfiliada: data.empresaAfiliada,
//                        observaciones: data.observaciones,
//                        estadoNoAfiliacionId: estadoNoAfiliacionId,
//                        funerariaAfiliadoId: funerariaAfiliadoId,
//                        grupoFamiliarId: grupoFamiliarId,
//                        sucursalId: vm.nuevoGestionProspecto.sucursalId,
//                        localidadId: vm.nuevoGestionProspecto.localidadId
//                    };

//                    vm.copiaAfiliados = data.afiliados;
//                    vm.seCopiaAfiliados = true;

//                    //Se guarda el prospecto (telefonoId y direccionId) que se indico al inicio
//                    afiliacionesService.saveProspecto({ telefonoId: vm.clienteProspecto.telefono.id, direccionId: vm.clienteProspecto.direccion.id })
//                       .success(function (data) {
//                           vm.nuevoGestionProspecto.prospectoId = data.id;
//                           vm.mostrarProspectos = !vm.mostrarProspectos;
//                           vm.mostrarInformacion = !vm.mostrarInformacion;
//                       })
//                       .error(function (error) {
//                           $scope.mensajeError = error.message;
//                       });

//                })
//                .error(function (error) {
//                    $scope.mensajeError = error.message;
//                });
//            };

//            //Funcion para copiar los afiliados prospecto con la gestion prospecto nueva.
//            vm.copiarAfiliadosProspecto = function () {
//                afiliacionesService.copiarAfiliadosProspecto({ gestionProspectoId: vm.nuevoGestionProspecto.id, afiliados: vm.copiaAfiliados })
//                      .success(function (data) {
//                          vm.cargarParentescosCliente();
//                      })
//                      .error(function (error) {
//                          $scope.mensajeError = error.message;
//                      });
//                vm.seCopiaAfiliados = false;
//            };

//            //Funcion para calcular la edad segun la fecha de nacimiento indicada
//            vm.calcularEdad = function (fechanacimiento) {
//                var birthday = new Date(fechanacimiento);
//                return ~~((Date.now() - birthday) / (31557600000));
//            };

//        }]);
//})();