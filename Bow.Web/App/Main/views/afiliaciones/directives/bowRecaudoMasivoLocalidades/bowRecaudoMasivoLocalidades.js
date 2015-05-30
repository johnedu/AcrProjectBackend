(function () {
    angular.module('app').directive('bowRecaudoMasivoLocalidades', ['$modal', function ($modal) {
        return {
            restrict: 'A',
            scope: {
                recaudoMasivoId: '=',
                recaudoMasivoNombre: '='
            },
            link: function (scope, elem, attrs) {
                elem.on('click', function () {
                    var modalInstance = $modal.open({
                        templateUrl: '/App/Main/views/afiliaciones/directives/bowRecaudoMasivoLocalidades/bowRecaudoMasivoLocalidades.cshtml',
                        controller: 'modalLocalidadesPlanExequialRecaudoController',
                        keyboard: false,
                        backdrop: 'static',
                        size: 'md',
                        resolve: {
                            recaudoMasivoId: function () {
                                return scope.recaudoMasivoId;
                            },
                            recaudoMasivoNombre: function () {
                                return scope.recaudoMasivoNombre;
                            }
                        }
                    });

                    modalInstance.result.then(function () {

                    }, function () {

                    });                
                })
            }
        }
    }]);

    angular.module('app').controller('modalLocalidadesPlanExequialRecaudoController', ['$scope', '$modalInstance', 'recaudoMasivoId', 'recaudoMasivoNombre', 'abp.services.app.afiliaciones',
        function ($scope, $modalInstance, recaudoMasivoId, recaudoMasivoNombre, afiliacionesService) {

            $scope.localidades = [];
            $scope.listaLocalidadesTabla = [];
            $scope.nombreRecaudoMasivo = recaudoMasivoNombre;

            function cargarLocalidades() {
                afiliacionesService.getAllLocalidadesByConvenio({ recaudoMasivoId: recaudoMasivoId }).success(function (data) {
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
                    afiliacionesService.getAllLocalidadesByConvenio({ recaudoMasivoId: recaudoMasivoId }).success(function (data) {
                        var listLocalidades = [];
                        for (var i in data.recaudoMasivoLocalidades) {
                            listLocalidades.push({ id: data.recaudoMasivoLocalidades[i].localidadId, nombre: data.recaudoMasivoLocalidades[i].localidad + " (" + data.recaudoMasivoLocalidades[i].departamento + ", " + data.recaudoMasivoLocalidades[i].pais + ")", tipoEntrada: "L" });
                        }
                        $scope.listaLocalidadesTabla = bow.tablas.paginar(listLocalidades, 5);
                    });
                }
                else if (TipoEntrada == "P") {
                    afiliacionesService.getAllLocalidadesByConvenioAndPais({ RecaudoMasivoId: recaudoMasivoId, PaisId: Id }).success(function (data) {
                        $scope.listaLocalidadesTabla = bow.tablas.paginar(data.recaudoMasivoLocalidades, 5);
                    });
                }
                else if (TipoEntrada == "D") {
                    afiliacionesService.getAllLocalidadesByConvenioAndDepartamento({ RecaudoMasivoId: recaudoMasivoId, DepartamentoId: Id }).success(function (data) {
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

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }


            ///********************************************************************
            // * Función encargada mostrar el listado de localidades de la información tributaria
            // ********************************************************************/
            $scope.cancelFormulario = function () {
                //  Limpiamos el formulario para que no se muestren las clases 'has-error'
                $scope.formLocalidad.$setPristine();
                $scope.selected = "";
                cargarLocalidades();
                $scope.cargarLocalidadesLista(0, "", "I");
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
        }]);
})();