(function () {
    angular.module('app')
        .directive("bowCategoriaBeneficio", function () {
            return {
                restrict: 'E',
                scope: {
                    notificarBeneficio: '&bowBeneficioSeleccionado',
                    ocultarFormulario: '&bowOcultarFormulario',
                    grupoBeneficio: "=grupoBeneficioSeleccionado",
                    listadoBeneficiosAgregados: "=",
                    listadoTodosLosBeneficios: "=",
                    mostrarMensajeCategoriaAsignada: '&mostrarMensajeCategoriaAsignada',
                    planExequialId: '=',
                    control: '='
                },
                link      : function (scope, element, attrs) {
                    scope.internalControl = scope.control || {};
                    scope.internalControl.limpiar = function() {
                        scope.limpiarFormulario();
                    }
                },
                controller: ['$scope', 'abp.services.app.parametros', 'abp.services.app.afiliaciones',
                    function ($scope, parametrosService, afiliacionesService) {

                        $scope.beneficio = {
                            beneficioId: '',
                            beneficioNombre: '',
                            beneficioDescripcion: '',
                            categoriaId: '',
                            categoriaNombre: ''
                        }

                        $scope.formularioCategoriaVisible = false;
                        $scope.formularioBeneficioVisible = false;
                        $scope.nombreNuevaCategoria = '';
                        $scope.nombreNuevoBeneficio = '';
                        $scope.descripcionNuevoBeneficio = '';

                        function cargarCategorias() {
                            parametrosService.getCategorias().success(function (data) {
                                $scope.listadoCategorias = data.categorias;
                            });
                        };
                        cargarCategorias();

                        function cargarBeneficios(categoriaId) {
                            afiliacionesService.getBeneficiosPlanExequialByCategoria({ TipoCategoriaId: categoriaId, PlanExequialId: $scope.planExequialId, GrupoBeneficio: $scope.grupoBeneficio }).success(function (data) {
                                $scope.listadoBeneficios = data.beneficios;
                            });
                        };

                        $scope.categoriaNombreMostrar = function (mostrar) {
                            $scope.mostrarCategoriaNombre = mostrar;
                            $scope.mostrarBeneficios = mostrar;
                        };

                        $scope.seleccionarCategoria = function (categoriaId, categoriaNombre) {
                            $scope.categoriaNombreMostrar(true);
                            $scope.beneficio.categoriaId = categoriaId;
                            $scope.beneficio.categoriaNombre = categoriaNombre;

                            if (getObjectByCategoria(categoriaId, $scope.listadoBeneficiosAgregados) && $scope.grupoBeneficio == 'Propio') {
                                $scope.mostrarMensajeCategoriaAsignada({ mensaje: "Propio" });
                            } else if (getObjectByCategoria(categoriaId, $scope.listadoTodosLosBeneficios)) {
                                $scope.mostrarMensajeCategoriaAsignada({ mensaje: "Adicional" });
                            }

                            cargarBeneficios(categoriaId);
                        };

                        $scope.seleccionarBeneficio = function (beneficioId, beneficioNombre, beneficioDescripcion) {
                            $scope.mostrarBeneficios = true;
                            $scope.beneficio.beneficioId = beneficioId;
                            $scope.beneficio.beneficioNombre = beneficioNombre;
                            $scope.beneficio.beneficioDescripcion = beneficioDescripcion;
                            $scope.notificarBeneficio({ beneficio: $scope.beneficio });
                        };

                        $scope.limpiarFormulario = function () {
                            $scope.categoriaNombreMostrar(false);
                            $scope.beneficio = {
                                beneficioId: '',
                                beneficioNombre: '',
                                beneficioDescripcion: '',
                                categoriaId: '',
                                categoriaNombre: ''
                            }
                            $scope.listadoBeneficios = [];
                        };

                        $scope.cancelarFormulario = function () {
                            $scope.limpiarFormulario();
                            $scope.ocultarFormulario({ mostrar: false });
                        };

                        $scope.categoriaNombreMostrar(false);

                        function getObjectByCategoria(categoria, arrayList) {
                            return arrayList.filter(function (obj) {
                                if (obj.categoriaBeneficioId == categoria) {
                                    return obj
                                }
                            })[0]
                        }

                        $scope.mostrarFormularioCategoria = function (mostrar) {
                            $scope.formularioCategoriaVisible = mostrar;
                            if (mostrar) {
                                $scope.frmRegistrarCategoria.$setPristine();
                                $scope.nombreNuevaCategoria = '';
                            }
                        };

                        $scope.guardarCategoria = function () {
                            var nuevaCategoria = {
                                nombre: $scope.nombreNuevaCategoria,
                                esNuevo: true
                            };

                            parametrosService.saveCategoria(nuevaCategoria)
                            .success(function () {
                                abp.notify.success(abp.localization.localize('afiliaciones_beneficio_notificacionInsertadoCategoria', 'Bow') + ' ' + $scope.nombreNuevaCategoria, abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                                cargarCategorias();
                                $scope.formularioCategoriaVisible = false;
                            })
                           .error(function (error) {
                               abp.notify.error(error.message, abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                           });
                        };

                        $scope.mostrarFormularioBeneficio = function (mostrar) {
                            $scope.formularioBeneficioVisible = mostrar;
                            if (mostrar) {
                                $scope.frmRegistrarBeneficio.$setPristine();
                                $scope.nombreNuevoBeneficio = '';
                                $scope.descripcionNuevoBeneficio = '';
                            }
                        };

                        $scope.guardarBeneficio = function () {
                            var nuevoBeneficio = {
                                tipoId: $scope.beneficio.categoriaId,
                                nombre: $scope.nombreNuevoBeneficio,
                                descripcion: $scope.descripcionNuevoBeneficio,
                                esNuevo: true
                            };

                            afiliacionesService.saveBeneficio(nuevoBeneficio)
                                .success(function () {
                                    abp.notify.success(abp.localization.localize('afiliaciones_beneficio_notificacionInsertadoBeneficio', 'Bow') + ' ' + $scope.nombreNuevoBeneficio, abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                                    cargarBeneficios($scope.beneficio.categoriaId);
                                    $scope.formularioBeneficioVisible = false;
                                })
                                .error(function (error) {
                                    abp.notify.success(error.message, abp.localization.localize('afiliaciones_beneficio_informacion', 'Bow'));
                                });
                        };
                }],    
                templateUrl: '/App/Main/views/afiliaciones/directives/bowCategoriaBeneficio/bowCategoriaBeneficio.cshtml'
            };
        })
})();