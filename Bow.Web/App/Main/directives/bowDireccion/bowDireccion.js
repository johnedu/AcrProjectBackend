(function () {
    angular.module('app')
        .directive("bowDireccion", function () {
            return {
                restrict: 'E',
                scope: {
                    notificarRegistroDireccion: '&registroDireccion',
                    localidadSeleccionada: '=',
                    inputLocalidadDisabled: '=inputLocalidadDisabled'
                },
                controller: ['$scope', 'abp.services.app.zonificacion', function ($scope, zonificacionService) {

                    $scope.barriomostrar = true;
                    $scope.mostrarAvenidaPrincipal = false;
                    $scope.mostrarAvenidaSecundaria = false;
                    //$scope.mostrarNomenclaturaUsa = false;
                    //$scope.mostrarNomenclaturaGeneral = false;

                    //Funcion para mostrar y ocultar los divs con nomenclatura
                    $scope.mostrarNomenclatura = function (mostrar) {
                        if ($scope.selectedLocalidad.paisId != null) {
                            zonificacionService.esPaisUsa({ id: $scope.selectedLocalidad.paisId })
                              .success(function (data) {
                                  var esUsa = data.esUsa;
                                  $scope.mostrarNomenclaturaPaisUsa = esUsa;
                                  $scope.mostrarNomenclaturaPais = !esUsa;
                                  $scope.barriomostrar = !esUsa;

                                  if (esUsa == true) {
                                      $scope.mostrarNomenclaturaUsa = mostrar;
                                  } else {
                                      $scope.mostrarNomenclaturaGeneral = mostrar;
                                  }
                              })
                        }
                    }

                    //Cargar Tories de la localidad seleccionada para el dropdow 1 via principal y dropdown tipo via USA
                    function cargarToriesLocalidad() {
                        zonificacionService.getToriesLocalidadByLocalidad({ localidadId: $scope.selectedLocalidad.localidadId })
                            .success(function (data) {
                                $scope.toriesLocalidad1 = data.toriesLocalidad;
                            });
                    }

                    //Funcion para limpiar el formulario.
                    function limpiarFormulario() {
                        $scope.barrios = "";
                        $scope.toriesLocalidad1 = "";
                        $scope.toriesLocalidad2 = "";
                        $scope.sufijosLocalidad = "";

                        $scope.tipoviausa = "";
                        $scope.avenidasusa = "";

                        $scope.pista = "";
                        $scope.orientacion1 = "";
                        $scope.orientacion2 = "";
                        $scope.porton = "";
                        $scope.apartamento = "";

                        $scope.numerousa = "";

                        $scope.zipusa1 = "";
                        $scope.zipusa2 = "";
                        $scope.nombre = "";
                        $scope.nombreusa = "";
                    }

                    //Cargar Tories de la localidad seleccionada para el dropdow 2 via secundaria
                    function cargarToriesLocalidadDisponibles(torieId) {
                        zonificacionService.getToriesLocalidadDisponiblesWithTipoOrientacionByTorieLocalidad({ torieLocalidadId: torieId, localidadId: $scope.selectedLocalidad.localidadId })
                            .success(function (data) {
                                $scope.toriesLocalidad2 = data.toriesLocalidad;
                            });
                    }

                    //Cargar sufijos de la localidad seleccionada para el dropdow 1 sufijo y dropdown 2 sufijo
                    function cargarSufijosLocalidad() {
                        zonificacionService.getSufijosLocalidadByLocalidad({ localidadId: $scope.selectedLocalidad.localidadId })
                            .success(function (data) {
                                $scope.sufijosLocalidad = data.sufijosLocalidad;
                            });
                    }

                    function mostrarAvenidaViaPrincipal(mostrar) {
                        $scope.mostrarAvenidaPrincipal = mostrar;
                    }

                    //Funcion que se ejecuta al seleccionar via principal para ocultar y mostrar controles si tipo orientacion es avenida
                    function esTipoOirentacionAvenidaViaPrincipal(data) {
                        mostrarAvenidaViaPrincipal(data.esAvenida);
                        if (data.esAvenida == true) {
                            $scope.orientacion1 = "";
                        }
                        cargarToriesLocalidadDisponibles($scope.torieLocalidad1);

                        $scope.sufijo1 = "";
                        $scope.torieLocalidad2 = "";
                    }

                    //Funcion que se ejecuta al seleccionar un objeto del dropdown via principal
                    $scope.viaPrincipalChange = function () {
                        if ($scope.torieLocalidad1 != null) {
                            zonificacionService.esTipoOrientacionAvenida({ id: $scope.torieLocalidad1 })
                                .success(function (data) {
                                    esTipoOirentacionAvenidaViaPrincipal(data);
                                });
                        }
                    }

                    function mostrarAvenidaViaSecundaria(mostrar) {
                        $scope.mostrarAvenidaSecundaria = mostrar;
                    }

                    //Funcion que se ejecuta al seleccionar via principal para ocultar y mostrar controles si tipo orientacion es avenida
                    function esTipoOrientacionAvenidaViaSecundaria(data) {
                        if (data.esAvenida == true) {
                            mostrarAvenidaViaSecundaria(true);
                            //$scope.orientacion2 = "";
                        }
                        else {
                            mostrarAvenidaViaSecundaria(false);
                        }
                        $scope.sufijo2 = "";
                    }

                    //Funcion que se ejecuta al seleccionar un objeto del dropdown via secundaria
                    $scope.viaSecundariaChange = function () {
                        if ($scope.torieLocalidad2 != null) {
                            zonificacionService.esTipoOrientacionAvenida({ id: $scope.torieLocalidad2 })
                              .success(function (data) {
                                  esTipoOrientacionAvenidaViaSecundaria(data);
                              });
                        }
                    }

                    //Funcion para cargar las avenidas de la localidad en los dropdown avenidas
                    function cargarAvenidasLocalidad() {
                        zonificacionService.getAvenidasByLocalidad({ id: $scope.selectedLocalidad.localidadId })
                           .success(function (data) {
                               $scope.avenidasLocalidad = data.avenidas;
                               $scope.avenidasusa = data.avenidas;
                           });
                    }

                    //Metodo para cargar los barrios de la localidad seleccionada.

                    $scope.localidadChange = function (localidadId) {

                        zonificacionService.getBarriosByLocalidad({ id: localidadId })
                          .success(function (data) {
                              $scope.barrios = data.barrios;
                              cargarToriesLocalidad();
                              cargarAvenidasLocalidad();
                              cargarSufijosLocalidad();
                              $scope.radioModel = "ConNomenclatura";

                              //Se ubica los dropdown en la posición inicial de seleccione...
                              $scope.barrio = "";
                              $scope.torieLocalidad1 = "";
                              $scope.avenida1Id = "";
                              $scope.avenida2Id = "";
                              $scope.sufijo1 = "";
                              $scope.sufijo2 = "";
                          })
                        $scope.mostrarNomenclatura(true);
                        mostrarAvenidaViaPrincipal(false);
                        mostrarAvenidaViaSecundaria(false);
                        limpiarFormulario();
                    }

                    //Guardar nombre de la manzana (Con Nomenclatura)
                    $scope.guardarDireccion = function () {
                        if ($scope.selectedLocalidad.paisId != "") {
                            var orientacion1 = null;
                            var orientacion2 = null;

                            //Se valida si se selecciono avenida o se ingreso un número para asignarlo a orientacion1 
                            if ($scope.avenida1Id != "") {
                                orientacion1 = $scope.avenida1Id;
                            } else {
                                orientacion1 = $scope.orientacion1;
                            }

                            //Se valida si se selecciono avenida o se ingreso un número para asignarlo a orientacion2
                            if ($scope.avenida2Id != "") {
                                orientacion2 = $scope.avenida2Id;
                            } else {
                                orientacion2 = $scope.orientacion2;
                            }

                            if ($scope.mostrarNomenclaturaGeneral === false) {
                                zonificacionService.saveDireccionSinNomenclatura({ barrioId: $scope.barrio, nombre: $scope.nombre, pista: $scope.pista })
                                 .success(function (data) {
                                     console.log("Registró: ", data);
                                     $scope.mensajeError = "";

                                     $scope.notificarRegistroDireccion({ direccion: data });
                                 }).error(function (error) {
                                     if (error.validationErrors != null) {
                                         $scope.mensajeError = error.validationErrors[0].message;
                                     } else {
                                         $scope.mensajeError = error.message;
                                     }
                                 });

                            } else if ($scope.mostrarNomenclaturaGeneral === true) {

                                zonificacionService.saveDireccionConNomenclatura({
                                    barrioId: $scope.barrio,
                                    pista: $scope.pista,
                                    torieLocalidad1Id: $scope.torieLocalidad1,
                                    torieLocalidad2Id: $scope.torieLocalidad2,
                                    orientacion1: orientacion1,
                                    orientacion2: orientacion2,
                                    sufijoLocalidad1Id: $scope.sufijo1,
                                    sufijoLocalidad2Id: $scope.sufijo2,
                                    porton: $scope.porton,
                                    apartamento: $scope.apartamento
                                })
                                .success(function (data) {
                                    console.log("Registró: ", data);
                                    $scope.mensajeError = "";

                                    $scope.notificarRegistroDireccion({ direccion: data });
                                }).error(function (error) {
                                    if (error.validationErrors != null) {
                                        $scope.mensajeError = error.validationErrors[0].message;
                                    } else {
                                        $scope.mensajeError = error.message;
                                    }
                                });

                            } else if ($scope.mostrarNomenclaturaUsa === false) {
                                zonificacionService.saveDireccionSinNomenclaturaUsa({ nombre: $scope.nombreusa, pista: $scope.pista, zipCode: $scope.zipusa1, localidadId: $scope.selectedLocalidad.localidadId })
                                 .success(function (data) {
                                     console.log("Registró: ", data);
                                     $scope.mensajeError = "";

                                     $scope.notificarRegistroDireccion({ direccion: data });
                                 }).error(function (error) {
                                     if (error.validationErrors != null) {
                                         $scope.mensajeError = error.validationErrors[0].message;
                                     } else {
                                         $scope.mensajeError = error.message;
                                     }
                                 });

                            }
                            else if ($scope.mostrarNomenclaturaUsa === true) {

                                zonificacionService.saveDireccionConNomenclaturaUsa({
                                    porton: $scope.numerousa,
                                    pista: $scope.pista,
                                    zipCode: $scope.zipusa2,
                                    sufijoLocalidad1Id: $scope.tipoviausa,
                                    orientacion1: $scope.nombreviausa,
                                    localidadId: $scope.selectedLocalidad.localidadId
                                })
                                .success(function (data) {
                                    console.log("Registró: ", data);
                                    $scope.mensajeError = "";

                                    $scope.notificarRegistroDireccion({ direccion: data });
                                }).error(function (error) {
                                    if (error.validationErrors != null) {
                                        $scope.mensajeError = error.validationErrors[0].message;
                                    } else {
                                        $scope.mensajeError = error.message;
                                    }
                                });

                            }
                        }
                    }

                    $scope.cancelModal = function () {
                        $modalInstance.dismiss('cancel');
                    }

                    $scope.seleccionoLocalidad = function (localidad) {
                        $scope.selectedLocalidad = localidad;
                        $scope.localidadChange(localidad.localidadId);
                    };

                    //  Si viene como parámetro la localidad, entonces la asignamos
                    if ($scope.localidadSeleccionada) {
                        $scope.seleccionoLocalidad($scope.localidadSeleccionada);
                    }


                }],
                templateUrl: '/App/Main/directives/bowDireccion/bowDireccion.cshtml'
            };
        })
})();