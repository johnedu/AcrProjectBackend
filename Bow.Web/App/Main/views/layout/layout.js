(function () {

    

    var controllerId = 'app.views.layout';
    angular.module('app').controller(controllerId, [
        '$scope', function ($scope) {
            var vm = this;
            //TODO. Remover!
            //Layout logic...
            //Se inicializan los tooltips a través de bootstrap, ya que los de AngularUI no aparecen
            //vm.inicializarTooltips = function () {
            //    $('[data-toggle="tooltip"]').tooltip();
            //};
        }]);
})();