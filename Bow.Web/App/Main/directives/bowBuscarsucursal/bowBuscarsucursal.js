(function () {
    angular.module('app')
        .directive("bowBuscarsucursal", function () {
            return {
                restrict: 'E',
                scope: {
                    titulolabel: "@titulolabel",
                    tituloplaceholder: "@tituloplaceholder",
                    selected: "=ngModel",
                    notificarSeleccion: "&bowSeleccionosucursal",
                    disabled: "=ngDisabled",
                    controlOperaciones: '='
                },
                link: function (scope, element, attrs) {
                    scope.internalControl = scope.controlOperaciones || {};
                    scope.internalControl.asignarSucursal = function (sucursalId) {
                        scope.asignarSucursal(sucursalId);
                    }
                },
                controller: ['$scope', 'abp.services.app.empresas', function ($scope, empresasService) {
                    $scope.selected = '';
                    $scope.sucursales = [];
                    $scope.exprMostrar = "sucursal as sucursal.nombre + ' (' + sucursal.nombreEmpresa + ' - ' + sucursal.nombreOrganizacion + ')' for sucursal in sucursales | filter: $viewValue"

                    function cargarSucursales() {
                        empresasService.getAllSucursales().success(function (data) {
                            $scope.sucursales = data.sucursales;
                        });
                    };

                    $scope.asignarSucursal = function (sucursalId) {
                        $scope.selected = getObjectById(sucursalId, $scope.sucursales);
                    };

                    $scope.onSelect = function ($item, $model, $label) {
                        $scope.notificarSeleccion({ sucursal: $model });
                    };
                    cargarSucursales();

                    /************************************************************************
                    * Función para obtener el elemento de una lista según el id
                    ************************************************************************/
                    function getObjectById(id, arrayList) {
                        return arrayList.filter(function (obj) {
                            if (obj.id == id) {
                                return obj;
                            }
                        })[0];
                    }

                }],
                templateUrl: '/App/Main/directives/bowBuscarsucursal/bowBuscarsucursal.cshtml'
            };
        })
})();