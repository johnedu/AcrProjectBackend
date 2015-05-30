(function () {
    var controllerId = 'app.views.layout.sidebar';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$state', function ($rootScope, $state) {
            var vm = this;

            vm.languages = abp.localization.languages;
            vm.currentLanguage = abp.localization.currentLanguage;

            vm.menu = abp.nav.menus.MainMenu;
            vm.currentMenuName = $state.current.menu;

            vm.menuSeleccion = undefined;

            vm.seleccionoOpcionMenu = function (opcionMenu) {
                if (vm.menuSeleccion === opcionMenu) {
                    vm.menuSeleccion = undefined;
                }
                else {
                    vm.menuSeleccion = opcionMenu;
                }
                
                console.log(vm.menuSeleccion);
            }

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                vm.currentMenuName = toState.menu;
            });
        }
    ]);
})();