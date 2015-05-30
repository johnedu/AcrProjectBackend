using Abp.Domain.Repositories;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Repositorios
{
    public interface IMensajeRepositorio : IRepository<Mensaje>
    {
        List<Mensaje> GetAllMensajesByEmisor(string Emisor);

        List<Mensaje> GetAllMensajesByReceptor(string Receptor);

        Mensaje GetMensajeByIdWithReceptorAndEmisor(int MensajeId);
    }
}
