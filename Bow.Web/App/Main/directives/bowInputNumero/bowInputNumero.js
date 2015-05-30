//  -------------   Opciones de la directiva    -------------   //

//        bowInputNumero { 
//            valorMaximo: 'Valor máximo que puede tener el input',
//            valorMinimo: 'Valor mínimo que puede tener el input',
//            maximoDecimales: 'Número máximo de decimales que puede tener el input',
//            maximoDigitos: 'Número máximo de digitos que puede tener el input sin contar el punto decimal y los decimales',
//            prefijo: 'Caracter que se le antepone al valor del input',
//            sufijo: 'Caracter que se le pospone al valor del input',
//            separadorMil: (Requerido)'Simbolo separador de miles (,)(.)'
//            separadorDecimal: (Requerido)'Simbolo separador de miles (.)(,)'
//        }

(function () {
    angular.module('app').directive('bowInputNumero', function () {
        var addSeparadorMil, teclasEspeciales, tieneMultiplesDecimales, noEsTeclaEspecial, noEsDigito, esNumero, valorEsValido, validarMaximoDecimales, validarMaximoDigitos, validarValorMaximo, validarValorMinimo;

        //  Validamos si el valor es un número
        esNumero = function (val) {
            return !isNaN(parseFloat(val)) && isFinite(val);
        };

        //  Validamos si las teclas ingresadas corresponden a números (45 a 57 son los códigos de las teclas numericas - 47 es el '/' que no es tecla válida)
        noEsDigito = function (which) {
            return which < 45 || which > 57 || which === 47;
        };

        //  Teclas especiales a parte de las númericas
        teclasEspeciales = [0, 8, 13];

        //  Validammos si no es una tecla especial
        noEsTeclaEspecial = function (which) {
            return teclasEspeciales.indexOf(which) === -1;
        };

        //  Validamos si tiene multiples puntos decimales
        tieneMultiplesDecimales = function (val, separadorDecimal) {
            return (val != null) && val.toString().split(separadorDecimal).length > 2;
        };

        //  Validamos si el valor tiene el número máximo de decimales o menos con la expresión regular
        validarMaximoDecimales = function (maximoDecimales, separadorDecimal) {
            var regexString, regexValida;
            if (maximoDecimales > 0) {
                regexString = "^-?\\d*\\.?\\d{0," + maximoDecimales + "}$";
            } else {
                regexString = "^-?\\d*$";
            }
            regexValida = new RegExp(regexString);
            return function (val) {
                return regexValida.test(val);
            };
        };

        //  Validamos si el valor es menor o igual al valor máximo definido
        validarValorMaximo = function (valorMaximo) {
            return function (val, numero) {
                return numero <= valorMaximo;
            };
        };

        //  Validamos si el valor es mayor o igual al valor mínimo definido
        validarValorMinimo = function (valorMinimo) {
            return function (val, numero) {
                return numero >= valorMinimo;
            };
        };

        //  Validamos si el valor tiene el número máximo de digitos o menos con la expresión regular
        validarMaximoDigitos = function (maximoDigitos, separadorDecimal) {
            var regexValida;
            regexValida = new RegExp("^-?\\d{0," + maximoDigitos + "}(\\" + separadorDecimal + "\\d*)?$");
            return function (val) {
                return regexValida.test(val);
            };
        };

        //  Se realizan las validaciones incluidas en la directiva
        valorEsValido = function (options) {
            var validaciones;
            validaciones = [];
            if (options.maximoDecimales != null) {
                validaciones.push({ funcion: validarMaximoDecimales(options.maximoDecimales, options.separadorDecimal), validacion: abp.localization.localize('directiva_bowInputNumero_maximoDecimales', 'Bow') });
            }
            if (options.valorMaximo != null) {
                validaciones.push({ funcion: validarValorMaximo(options.valorMaximo), validacion: abp.localization.localize('directiva_bowInputNumero_valorMaximo', 'Bow') });
            }
            if (options.valorMinimo != null) {
                validaciones.push({ funcion: validarValorMinimo(options.valorMinimo), validacion: abp.localization.localize('directiva_bowInputNumero_valorMinimo', 'Bow') });
            }
            if (options.maximoDigitos != null) {
                validaciones.push({ funcion: validarMaximoDigitos(options.maximoDigitos, options.separadorDecimal), validacion: abp.localization.localize('directiva_bowInputNumero_minimoDigitos', 'Bow') });
            }
            return function (val) {
                var i, numero, _i, _ref;
                if (!esNumero(val)) {
                    return abp.localization.localize('directiva_bowInputNumero_noEsNumero', 'Bow');
                }
                if (tieneMultiplesDecimales(val, options.separadorDecimal)) {
                    return abp.localization.localize('directiva_bowInputNumero_multiplesPuntosDecimales', 'Bow');
                }
                numero = Number(val);
                for (i = _i = 0, _ref = validaciones.length; 0 <= _ref ? _i < _ref : _i > _ref; i = 0 <= _ref ? ++_i : --_i) {
                    if (!validaciones[i].funcion(val, numero)) {
                        return validaciones[i].validacion;
                    }
                }
                return "EsValido";
            };
        };

        //  Ajustamos el valor del input con el separador de mil seleccionado
        addSeparadorMil = function (val, separadorMil, separadorDecimal) {
            var valorSeparado, decimales, numerosEnteros;
            decimales = val.indexOf('.') == -1 ? '' : val.replace(/^\d+(?=\.)/, '');
            numerosEnteros = val.replace(/(\.\d+)$/, '');
            valorSeparado = numerosEnteros.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1' + separadorMil);
            return "" + valorSeparado + decimales.replace('.', separadorDecimal);
        };

        return {
            restrict: 'A',
            require: 'ngModel',
            scope: {
                options: '@bowInputNumero'
            },
            link: function (scope, elem, attrs, ngModelCtrl) {
                var esValido, options;
                options = {};

                if (scope.options != null) {
                    options = scope.$eval(scope.options);
                }
                
                esValido = valorEsValido(options);

                //  Función que al cambiar el valor del input lo hace en el ng-model
                ngModelCtrl.$parsers.unshift(function (viewVal) {
                    var valorSinSeparador;
                    var re = new RegExp("\\" + options.separadorMil, "g");
                    valorSinSeparador = parseFloat(viewVal.replace(re, '').replace(options.separadorDecimal, '.'));
                    var respuesta = esValido(valorSinSeparador)
                    if (respuesta === "EsValido" || !valorSinSeparador) {
                        ngModelCtrl.$error = {};
                        ngModelCtrl.$setValidity('fcsaNumber', true);
                        return valorSinSeparador;
                    } else {
                        ngModelCtrl.$error.validacion = respuesta;
                        ngModelCtrl.$setValidity('fcsaNumber', false);
                        return void 0;
                    }
                });

                ngModelCtrl.$formatters.push(function (val) {
                    if ((options.nullDisplay != null) && (!val || val === '')) {
                        return options.nullDisplay;
                    }
                    if ((val == null) || !esValido(val)) {
                        return val;
                    }
                    ngModelCtrl.$setValidity('fcsaNumber', true);
                    val = addSeparadorMil(val.toString(), options.separadorMil, options.separadorDecimal);
                    if (options.prefijo != null) {
                        val = "" + options.prefijo + val;
                    }
                    if (options.sufijo != null) {
                        val = "" + val + options.sufijo;
                    }
                    return val;
                });

                //  Acción que se ejecuta cuando se quita el foco del input
                elem.on('blur', function () {
                    var formatter, viewValue, _i, _len, _ref;
                    viewValue = ngModelCtrl.$modelValue;
                    if ((viewValue == null) || !esValido(viewValue)) {
                        return;
                    }
                    _ref = ngModelCtrl.$formatters;
                    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                        formatter = _ref[_i];
                        viewValue = formatter(viewValue);
                    }
                    ngModelCtrl.$viewValue = viewValue;
                    return ngModelCtrl.$render();
                });

                //  Acción que se ejecuta cuando se pone el foco en el input
                elem.on('focus', function () {
                    var val;
                    val = elem.val();
                    //  Se limpia el valor de sufijos, prefijos y separadores
                    if (options.prefijo != null) {
                        val = val.replace(options.prefijo, '');
                    }
                    if (options.sufijo != null) {
                        val = val.replace(options.sufijo, '');
                    }
                   
                    var re = new RegExp("\\" + options.separadorMil, "g");
                    elem.val(val.replace(re, ''));
                    return elem[0].select();
                });

                //  Acción que se ejecuta con cada tecla presionada
                if (options.preventInvalidInput === true) {
                    return elem.on('keypress', function (e) {
                        if (noEsDigito(e.which && noEsTeclaEspecial(e.which))) {
                            return e.preventDefault();
                        }
                    });
                }
            }
        };
    });
})();