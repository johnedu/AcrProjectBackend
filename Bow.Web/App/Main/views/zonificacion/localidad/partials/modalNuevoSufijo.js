(function () {
    angular.module('app').controller('modalNuevoSufijoController', ['$scope', '$modalInstance', 'abp.services.app.zonificacion',
        function ($scope, $modalInstance, zonificacionService) {

            //Variables inicializar matriz para eliminar de la lista
            $scope.mensajeEliminar = [];

            $scope.nuevoSufijo = {
                nombre: '',
                sufijoId: ''
            };

            $scope.sufijos = {
                sufijoId: '',
                nombre: ''
            };

            //Controles para eliminar un Sufijo
            $scope.controlesSufijo = {
                visible: [],
                mostrar: function ($index) {
                    $scope.controlesSufijo.visible[$index] = true;
                },
                ocultar: function ($index) {
                    $scope.controlesSufijo.visible[$index] = false;
                }
            };


            /***************************************************************
             * FUNCIONES
             **************************************************************/
            //Metodo para cargar los sufijos existentes
            cargarSufijos = function () {
                zonificacionService.getSufijos().success(function (data) {
                    $scope.sufijos = bow.tablas.paginar(data.sufijos, 5);
                });
            }
            cargarSufijos();

            $scope.cerrarModal = function () {
                $modalInstance.close();
            }

            limpiarFormulario = function () {
                $scope.nuevoSufijo = {
                    nombre: '',
                    sufijoId: ''
                };
                $scope.frmSufijo.$setPristine();
            }

            /***************************************************************
             * REGISTRAR SUFIJOS
             **************************************************************/
            $scope.registrarSufijo = function () {
                zonificacionService.saveSufijo($scope.nuevoSufijo)
                    .success(function () {
                        cargarSufijos();
                        abp.notify.success(abp.localization.localize('zonificacion_sufijo_notificacionInsertadoSufijo', 'Bow') + ' ' + $scope.nuevoSufijo.nombre, "Información");
                        limpiarFormulario();
                    }).error(function (error) {
                        abp.notify.error(error.message + ' ' + $scope.nuevoSufijo.nombre, "Información");
                    });
            }


            /*************************************************************
             * ELIMINAR SUFIJO
             ************************************************************/
            $scope.puedeEliminarSufijo = function (sufijoId, funcionRetornarPuedeEliminar) {
                zonificacionService.puedeEliminarSufijo({ id: sufijoId }).success(function (data) {
                    funcionRetornarPuedeEliminar(data.puedeEliminar);
                });
            }

            $scope.noPuedeEliminar = function () {
                abp.notify.error(abp.localization.localize('parametro_sufijo_notificacionNoEliminarSufijo', 'Bow'),
                            abp.localization.localize('zonificacion_manzana_informacion', 'Bow'));
            }

            $scope.eliminarSufijoOk = function (sufijo) {
                console.log("sufijo:", sufijo);
                zonificacionService.deleteSufijo({ id: sufijo.id })
                   .success(function (data) {
                       abp.notify.info(abp.localization.localize('zonificacion_sufijo_notificacionEliminadoSufijo', 'Bow') + sufijo.nombre, abp.localization.localize('zonificacion_sufijo_informacion', 'Bow'));
                       cargarSufijos();
                   });
            }










            $scope.okModal = function () {
                zonificacionService.saveSufijo($scope.nuevoSufijo)
                    .success(function () {
                        $modalInstance.close($scope.nuevoSufijo.nombre);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }


            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }

        }]);
})();