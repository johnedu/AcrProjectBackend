(function () {
    angular.module('app')
        .directive("bowInputsolonumeros", function () {
            return {
                require: 'ngModel',
                restrict: 'A',
                link: function (scope, element, attr, ctrl) {
                    function inputValue(val) {
                        if (val) {
                            var digits = val.replace(/[^0-9]/g, '');

                            if (digits !== val) {
                                ctrl.$setViewValue(digits);
                                ctrl.$render();
                            } else
                            {
                                return parseInt(digits, 10);
                            }
                        }
                        return undefined;
                    }
                    ctrl.$parsers.push(inputValue);
                },
                controller: ['$scope', function ($scope) {



                }]
            };
        })
})();