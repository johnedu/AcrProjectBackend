(function () {
    angular.module('app')
        .directive("bowBuscarNombreGrupoInformal", function () {
            return {
                restrict: 'E',
                scope: {
                    titulolabel: "@titulolabel",
                    selected: "=selected",
                    tituloplaceholder: "@tituloplaceholder",
                    notificarSeleccion: "&bowSeleccionoNombre",
                    notificarModeloActual: "&bowConsultarModeloActual",
                    requerido: "@ngRequired",
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.recargarListadoGruposInformales = function () {
                        scope.recargarListadoGruposInformales();
                    };
                    scope.internalControl.asignarGrupoSeleccionado = function (grupoInformalId) {
                        scope.asignarGrupoSeleccionado(grupoInformalId);
                    };
                    scope.internalControl.obtenerGrupoSeleccionado = function () {
                        scope.obtenerGrupoSeleccionado();
                    };
                },
                controller: ['$scope', 'abp.services.app.afiliaciones', function ($scope, afiliacionesService) {

                    $scope.gruposInformales = [];

                    function cargarGruposInformales() {
                        afiliacionesService.getAllGrupoInformal().success(function (data) {
                            $scope.gruposInformales = data.gruposInformales;
                        });
                    };
                    cargarGruposInformales();

                    $scope.onSelect = function ($item, $model, $label) {
                        $scope.notificarSeleccion({ grupoInformal: $model });
                    };

                    $scope.recargarListadoGruposInformales = function () {
                        cargarGruposInformales();
                    };

                    $scope.obtenerGrupoSeleccionado = function () {
                        $scope.notificarModeloActual({ grupoInformal: $scope.selected });
                    };

                    $scope.asignarGrupoSeleccionado = function (grupoInformalId) {
                        if ($scope.gruposInformales) {
                            $scope.selected = getObjectById(grupoInformalId, $scope.gruposInformales)
                        }
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

                }],
                templateUrl: '/App/Main/directives/bowBuscarNombreGrupoInformal/bowBuscarNombreGrupoInformal.cshtml'
            };
        })
})();