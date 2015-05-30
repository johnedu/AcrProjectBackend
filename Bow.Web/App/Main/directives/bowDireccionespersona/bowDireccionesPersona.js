(function () {
    angular.module('app')
        .directive("bowDireccionespersona", function () {
            return {
                restrict: 'E',
                scope: {
                    direccionesPersona: '=bowAdmindireccionespersona',
                    personaId: '=bowPersonaid',
                    soloLecturaConsultar: '=ngDisabled'
                },
                controller: ['$scope', 'abp.services.app.personas', 'abp.services.app.parametros', function ($scope, personasService, parametrosService) {

                    //Matriz para almacenar el $index de la fila a inactivar el direccion
                    $scope.mostrarInactivarDireccion = [];

                    $scope.estadoActivoDireccion = true;

                    //Funcion para cargar los tipos de ubicación en el dropdown
                    function cargarTipoUbicacion() {
                        parametrosService.getTiposByParametroUbicacion()
                        .success(function (data) {
                            $scope.tipos = data.tipos;
                        })
                    };
                    cargarTipoUbicacion();

                    //Proceso para cargar la fecha actual con el formato similar al que viene de c#
                    var now = new Date();
                    var dia = now.getDate();
                    var mes = now.getMonth() + 1;
                    var year = now.getFullYear();
                    var nombreMes = ["ene.", "feb.", "mar.", "abr.", "may.", "jun.",
                        "jul.", "ago.", "sep.", "oct.", "nov.", "dic."];


                    $scope.inactivarDireccion = function ($index) {
                        if ($scope.direccionesPersona[$index].tipoCambio != "N") {
                            $scope.direccionesPersona[$index].tipoCambio = "M";
                        }

                        $scope.direccionesPersona[$index].fechaCancelacion = dia + " " + nombreMes[mes - 1] + " " + year;
                        $scope.direccionesPersona[$index].nombreEstado = false;
                        $scope.direccionesPersona[$index].nombreEstadoActivo = false;
                        $scope.mostrarInactivarDireccion[$index] = true;

                    };

                    $scope.changeTipoUbicacionDireccion = function ($index, tipoUbicacion) {

                        //Variable para almacenar la cantidad de tipos de ubicacion repetidos 
                        var tipoUbicacionYaAsignado = 0;

                        //Ciclo para recorrer el grid de direcciones.
                        for (var i = 0; i < $scope.direccionesPersona.length; i++) {

                            if ($scope.direccionesPersona[i].tipoUbicacionId === tipoUbicacion && $scope.direccionesPersona[i].nombreEstado === true) {
                                tipoUbicacionYaAsignado = tipoUbicacionYaAsignado + 1;
                            }
                        }

                        //Si la cantidad es mayor a 1, entonces se muestra el mensaje de que ya existe otro tipo ubicacion asignado
                        if (tipoUbicacionYaAsignado > 1) {

                            abp.notify.warn(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_direccionTipoUbicacionYaExiste', 'Bow'),
                            abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));

                            $scope.direccionesPersona[$index].tipoUbicacionId = $scope.tipos[0];

                        } else {
                            //Se registra el cambio en el tipo de ubicacion para actualizar en bd
                            if ($scope.direccionesPersona[$index].tipoCambio != "N") {
                                $scope.direccionesPersona[$index].tipoCambio = "M";
                            }
                        }
                    };

                    //Funcion para mostrar y ocultar filas de la tabla direcciones de persona según valor del checkbox activo
                    $scope.mostrarFilasDireccion = function (valor) {
                        if ($scope.estadoActivoDireccion === true && valor === false) {
                            return false;
                        }
                        else {
                            return true;
                        }
                    };

                    //Proceso para cargar el registro de direccion ingresado en la grilla
                    $scope.direccionRegistrada = function (direccion) {
                        console.log("Direccion registrada : " + direccion.id);

                        var existeDireccion = false;

                        //Verificamos si la direccion ya ha sido ingresada
                        if ($scope.direccionesPersona != null) {
                            for (var i = 0; i < $scope.direccionesPersona.length; i++) {
                                if ($scope.direccionesPersona[i].direccionId == direccion.id) {
                                    existeDireccion = true;
                                }
                            }
                        }

                        if (existeDireccion == false) {
                            var nuevoDireccion =
                            {
                                id: 0,
                                personaId: $scope.personaId,
                                direccionId: direccion.id,
                                direccionCompleta: direccion.direccionCompleta,
                                nombreLocalidad: direccion.localidadCompleta,
                                tipoUbicacionId: '',
                                tipoUbicacionNombre: '',
                                fechaIngreso: dia + " " + nombreMes[mes - 1] + " " + year,
                                nombreEstado: true,
                                nombreEstadoActivo: 'Inactivar',
                                fechaCancelacion: '',
                                tipoCambio: 'N'
                            };

                            $scope.direccionesPersona.push(nuevoDireccion);

                            //Se ubica el dropdown del tipo de ubicacion en la primera posición
                            //var listaCantidad = $scope.direccionesPersona.length - 1;
                            //$scope.direccionesPersona[listaCantidad].tipoUbicacionId = $scope.tipos[0].id;

                        } else {
                            abp.notify.warn(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_direccionYaAsignado', 'Bow'),
                            abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));
                        }
                    };


                }],
                templateUrl: '/App/Main/directives/bowDireccionespersona/bowDireccionesPersona.cshtml'
            };
        })
})();