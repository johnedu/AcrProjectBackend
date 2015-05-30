(function () {
    var controllerId = 'app.views.develop';
    angular.module('app').controller(controllerId, ['$scope', '$modal', 'Upload', function ($scope, $modal, Upload) {
        var vm = this;


        //******************* Upload AWS *************************************

        $scope.$watch('files', function () {
            $scope.upload($scope.files);
        });

        $scope.upload = function (files) {
            if (files && files.length) {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    Upload.upload({
                        url: 'https://bowtest.s3.amazonaws.com/', 
                        method: 'POST',
                    fields : {
                        key: file.name, 
                        AWSAccessKeyId: 'AKIAJ7Q2QHY3KPD34RYQ',
                        acl: 'private', 
                        policy: 'ewogICJleHBpcmF0aW9uIjogIjIwMjAtMDEtMDFUMDA6MDA6MDBaIiwKICAiY29uZGl0aW9ucyI6IFsKICAgIHsiYnVja2V0IjogImJvd3Rlc3QifSwKICAgIFsic3RhcnRzLXdpdGgiLCAiJGtleSIsICIiXSwKICAgIHsiYWNsIjogInByaXZhdGUifSwKICAgIFsic3RhcnRzLXdpdGgiLCAiJENvbnRlbnQtVHlwZSIsICIiXSwKICAgIFsic3RhcnRzLXdpdGgiLCAiJGZpbGVuYW1lIiwgIiJdLAogICAgWyJjb250ZW50LWxlbmd0aC1yYW5nZSIsIDAsIDUyNDI4ODAwMF0KICBdCn0=',
                        signature: 'iLGmKUgOeCsp3uv3ayngvdbVVeY=',
                        "Content-Type": file.type != '' ? file.type : 'application/octet-stream', 
                        filename: file.name 
                    },
                    file: file,
                    }).progress(function (evt) {
                        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                        console.log('progress: ' + progressPercentage + '% ' + evt.config.file.name);
                    }).success(function (data, status, headers, config) {
                        console.log('file ' + config.file.name + 'uploaded. Response: ' + data);
                    });
                }
            }
        };

        //******************* Registrar Nueva Direccion **********************
        vm.abrirModalRegistrarDireccion = function () {
            var modalInstance = $modal.open({
                templateUrl: '/App/Main/views/zonificacion/direccion/registrarNuevoDireccion.cshtml',
                controller: 'registrarNuevoDireccionController',
                size: 'lg'
            });

            modalInstance.result.then(function () {
                abp.notify.success(abp.localization.localize('zonificacion_direccion_notificacionInsertadoDireccion', 'Bow'),
                abp.localization.localize('zonificacion_direccion_informacion', 'Bow')
                    );
            }, function () {
                console.log("Ocurrió un problema al cargar el modal del nueva direccion");
            });
        }

        /************************************************************************
         * Abrir Modal para registrar nuevo teléfono
         ************************************************************************/
        vm.abrirModalRegistrarTelefono = function () {
            var modalInstance = $modal.open({
                templateUrl: '/App/Main/views/zonificacion/telefono/modalRegistrarTelefono.cshtml',
                controller: 'modalRegistrarTelefonoController',
                size: 'md'
            });
        };

        //******************* Registrar Nueva Persona **********************
        // *********** 

        vm.abrirModalGestionarNuevaPersona = function () {
            var modalInstance = $modal.open({
                templateUrl: '/App/Main/views/personas/persona/partials/modalGestionarNuevaPersona.cshtml',
                controller: 'modalGestionarNuevaPersonaController',
                size: 'lg'
            });

            modalInstance.result.then(function () {
                //abp.notify.success(abp.localization.localize('zonificacion_direccion_notificacionInsertadoDireccion', 'Bow'),
                //abp.localization.localize('zonificacion_direccion_informacion', 'Bow')
                //);
            }, function () {
                console.log("Ocurrió un problema al cargar el modal nueva persona");
            });
        };

        $scope.$on('direccionRegistrada', function (e, data) {
            console.log("Desde afuera recibe: ", data);
        });

        //******************* Registrar Nueva Persona **********************
        // *********** 

        //Abrir modal para editar por parametro o ingresar nuevas personas
        vm.abrirModalGestionarNuevaPersona = function (personaEstado) {
            var modalInstance = $modal.open({
                templateUrl: '/App/Main/views/personas/persona/partials/modalGestionarNuevaPersona.cshtml',
                controller: 'modalGestionarNuevaPersonaController',
                size: 'lg',
                resolve: {
                    personaEstado: function () {
                        return personaEstado;
                    }
                }
            });

            modalInstance.result.then(function () {
            }, function () {
                console.log("Ocurrió un problema al cargar el modal nueva persona");
            });
        }

        //Funcion que se ejecuta al seleccionar la sucursal desde la directiva de Buscar Sucursales
        $scope.seleccionoSucursal = function (sucursal) {
            //alert(JSON.stringify(sucursal));
            $scope.selectedSucursal = sucursal;
        };

        $scope.mostrarDireccion = function (direccion) {
            console.log('registró: ', direccion);
        }

        //Resultado Directiva de la persona guardada
        $scope.mostrarPersona = function (persona) {
            console.log('registró: ', persona);
        }

        // Variable para para enviar la persona a editar
        $scope.personaEstadoEditar = {
            personaId: '16',
            estado: 'E'
        }

        // Variable para para enviar la persona nueva
        $scope.personaEstadoNuevo = {
            personaId: '0',
            estado: 'N'
        }

        // Variable para para enviar la persona a consultar
        $scope.personaEstadoConsultar = {
            personaId: '16',
            estado: 'C'
        }

        $scope.resultadoBusqueda = function (personas) {
            console.log('registró: ', personas);
        }

        $scope.seleccionoPersona = function (persona) {
            //alert(JSON.stringify(persona));
            console.log(persona);
        }

        $scope.seleccionoEmpleado= function (empleado) {
            //alert(JSON.stringify(empleado));
            console.log(empleado);
        }

        $scope.seleccionoNombre = function (nombre) {
            console.log(nombre);
        }

        $scope.seleccionoApellido = function (apellido) {
            console.log(apellido);
        }

    }
    ]);
})();
