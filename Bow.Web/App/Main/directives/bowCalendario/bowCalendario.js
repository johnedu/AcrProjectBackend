(function () {
    angular.module('app')
        .directive("bowCalendario", function () {
            return {
                restrict: 'E',
                scope: {
                    titulo: '@',
                    nombre: '@',
                    requerido: '@',
                    fechaminima: '=fechaMinima',
                    fechamaxima: '=fechaMaxima',
                    //fecha: '=ngModel',
                    //action: '&'
                },
                controller: ['$scope', function ($scope) {

                    //$scope.enviarFecha = function () {
                    //    console.log("FECHA ANTES DE ENVIAR: ", $scope.fecha);
                    //    $scope.action({ fecha: $scope.fecha });
                    //};

                    $scope.configuracionFecha = {
                        formato: 'dd/MM/yyyy',
                        fechaMinima: $scope.fechaminima,
                        fechaMaxima: $scope.fechamaxima,
                        abierto: false,
                        open: function ($event) {
                            $event.preventDefault();
                            $event.stopPropagation();
                            this.abierto = !this.abierto;
                        }
                    };
                    //TODO. No está tomando la fecha seleccionada
                    //TODO. Probar con link
                    /*
                    $scope.eventoBlur = function (fechaSeleccionada) {
                        console.log("FECHA CON DOM: ", $($scope.nombre.value) );
                        console.log("SCOPE COMPLETO: ", $scope);
                        console.log("Voy a enviar: ", fechaSeleccionada);
                        console.log("CON SCOPE: ", $scope.fecha);
                        $scope.$emit("blurDatepicker", fechaSeleccionada);
                    };
                    */
                }],
                /*
                link: function(scope, element, attrs, tabsCtrl) {
                    console.log("LINK SCOPE: ", scope);
                    

                    eventoBlur = function (fechaSeleccionada) {
                        console.log("Fecha: ", fechaSeleccionada);
                    };                    
                },
                */
                templateUrl: '/App/Main/directives/bowCalendario/bowCalendario.cshtml'
            };
        })
})();
