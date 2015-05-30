var bow = bow || {};
(function () {
    bow.tablas = bow.tablas || {};
    bow.fechas = bow.fechas || {};

    /**
     * Método encargado de paginar los registros de una tabla
     * 
     * @param datos. Arreglo con los datos a paginar
     * @param limite. Cantidad de filas por página
     * @param inicial. Posición inicial del cursor de paginación (página inicial a mostrar)
     * @return Objeto para utilizar en las etiquetas <pagination> y <ng-repeat>
     */
    bow.tablas.paginar = function (datos, limite) {
        var paginacion = {
            totalFilas: datos.length,
            filasPorPagina: limite,
            paginaInicial: 1,
            paginaActual: 1,
            filasMostrar: datos,
            pageChanged: function () {
                var inicio = ((this.paginaActual - 1) * this.filasPorPagina);
                var fin = inicio + this.filasPorPagina;
                this.filasMostrar = datos.slice(inicio, fin);
                return this.filasMostrar;
            }
        };

        paginacion.pageChanged();

        return paginacion;
    };

    /**
     * Método encargado de configurar los parámetros de los componentes datepicker para las fechas
     * 
     * @param fechaMinima. Arreglo con los datos a paginar
     * @param limite. Cantidad de filas por página
     * @param inicial. Posición inicial del cursor de paginación (página inicial a mostrar)
     * @return Objeto para utilizar en las etiquetas <pagination> y <ng-repeat>
     */
    bow.fechas.configurarDatePicker = function (fechaMinima, fechaMaxima) {
        return {
            format: 'dd/MM/yyyy',
            opciones: {
                minDate: fechaMinima,
                maxDate: fechaMaxima,
                opened: false
            },
            open: function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                this.opciones.opened = !this.opciones.opened;
            }
        };
    };


})();