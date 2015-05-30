(function () {
    angular.module('app').controller('registrarNuevoDireccionController', ['$scope', '$modalInstance',
        function ($scope, $modalInstance) {

            $scope.$on('direccionRegistrada', function (e, data) {
                $modalInstance.close(data);
                         });

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }

        }]);
})();