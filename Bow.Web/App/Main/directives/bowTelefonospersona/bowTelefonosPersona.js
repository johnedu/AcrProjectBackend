(function () {
    angular.module('app')
        .directive("bowTelefonospersona", function () {
            return {
                restrict: 'E',
                scope: {
                    telefonosPersona: '=bowAdmintelefonospersona',
                    personaId: '=bowPersonaid',
                    soloLecturaConsultar: '=ngDisabled'
                },
                controller: ['$scope', 'abp.services.app.personas', 'abp.services.app.parametros', function ($scope, personasService, parametrosService) {

                    //Matriz para almacenar el $index de la fila a inactivar el telefono
                    $scope.mostrarInactivarTelefono = [];

                    $scope.estadoActivoTelefono = true;

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


                    $scope.inactivarTelefono = function ($index) {
                        if ($scope.telefonosPersona[$index].tipoCambio != "N") {
                            $scope.telefonosPersona[$index].tipoCambio = "M";
                        }

                        $scope.telefonosPersona[$index].fechaCancelacion = dia + " " + nombreMes[mes - 1] + " " + year;
                        $scope.telefonosPersona[$index].nombreEstado = false;
                        $scope.telefonosPersona[$index].nombreEstadoActivo = false;
                        $scope.mostrarInactivarTelefono[$index] = true;
                    };

                    $scope.changeTipoUbicacionTelefono = function ($index, tipoUbicacion) {

                        //Variable para almacenar la cantidad de tipos de ubicacion repetidos 
                        var tipoUbicacionYaAsignado = 0;

                        //Ciclo para recorrer el grid de telefonos.
                        for (var i = 0; i < $scope.telefonosPersona.length; i++) {

                            if ($scope.telefonosPersona[i].tipoUbicacionId === tipoUbicacion && $scope.telefonosPersona[i].nombreEstado === true) {
                                tipoUbicacionYaAsignado = tipoUbicacionYaAsignado + 1;
                            }
                        }

                        //Si la cantidad es mayor a 1, entonces se muestra el mensaje de que ya existe otro tipo ubicacion asignado
                        if (tipoUbicacionYaAsignado > 1) {

                            abp.notify.warn(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_telefonoTipoUbicacionYaExiste', 'Bow'),
                            abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));

                            $scope.telefonosPersona[$index].tipoUbicacionId = $scope.tipos[0];

                        } else {
                            //Se registra el cambio en el tipo de ubicacion para actualizar en bd
                            if ($scope.telefonosPersona[$index].tipoCambio != "N") {
                                $scope.telefonosPersona[$index].tipoCambio = "M";
                            }
                        }
                    };

                    //Funcion para mostrar y ocultar filas de la tabla telefonos de persona según valor del checkbox activo
                    $scope.mostrarFilasTelefono = function (valor) {
                        if ($scope.estadoActivoTelefono === true && valor === false) {
                            return false;
                        }
                        else {
                            return true;
                        }
                    };

                    //Función para guardar el telefono, retorna el objeto que almaceno
                    $scope.registroTelefono = function (telefono) {
                        console.log("telefono Afuera: ", telefono);

                        var existeTelefono = false;

                        //Verificamos si el telefono ya ha sido ingresado
                        if ($scope.telefonosPersona != null) {
                            for (var i = 0; i < $scope.telefonosPersona.length; i++) {
                                if ($scope.telefonosPersona[i].telefonoId == telefono.id) {
                                    existeTelefono = true;
                                }
                            }
                        }

                        if (existeTelefono == false) {

                            //Proceso para cargar el registro de telefono ingresado en la grilla
                            var nuevoTelefono =
                               {
                                   id: 0,
                                   personaId: $scope.personaId,
                                   telefonoId: telefono.id,
                                   telefonoNumero: telefono.telefonoCompleto,
                                   nombreLocalidad: telefono.ubicacion,
                                   tipoUbicacionId: '',
                                   tipoUbicacionNombre: '',
                                   fechaIngreso: dia + " " + nombreMes[mes - 1] + " " + year,
                                   nombreEstado: true,
                                   nombreEstadoActivo: 'Inactivar',
                                   fechaCancelacion: '',
                                   tipoCambio: 'N'
                               };

                            $scope.telefonosPersona.push(nuevoTelefono);

                            //Se ubica el dropdown del tipo de ubicacion en la primera posición
                            //var listaCantidad = $scope.telefonosPersona.length - 1;
                            //$scope.telefonosPersona[listaCantidad].tipoUbicacionId = $scope.tipos[0].id;

                        } else {
                            abp.notify.warn(abp.localization.localize('personas_nuevapersona_modalNuevaPersona_telefonoYaAsignado', 'Bow'),
                            abp.localization.localize('personas_nuevapersona_modalNuevaPersona_informacion', 'Bow'));
                        }
                    };

                }],
                templateUrl: '/App/Main/directives/bowTelefonospersona/bowTelefonosPersona.cshtml'
            };
        })
})();