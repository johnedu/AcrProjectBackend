function DirectivasException(message) {
    this.name = 'DirectivasException';
    this.message = message;
}
DirectivasException.prototype = new Error();
DirectivasException.prototype.constructor = DirectivasException;

function CampoRequeridoException(message) {
    this.name = 'CampoRequeridoException';
    this.message = message;
}
CampoRequeridoException.prototype = new DirectivasException();
CampoRequeridoException.prototype.constructor = CampoRequeridoException;

function ListaVaciaException(message) {
    this.name = 'ListaVaciaException';
    this.message = message;
}
ListaVaciaException.prototype = new DirectivasException();
ListaVaciaException.prototype.constructor = ListaVaciaException;