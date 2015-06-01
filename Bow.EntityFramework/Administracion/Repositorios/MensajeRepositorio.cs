using Abp.EntityFramework;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bow.Administracion.Repositorios
{
    public class MensajeRepositorio : BowRepositoryBase<Mensaje>, IMensajeRepositorio
    {
        public MensajeRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<Mensaje> GetAllMensajesByEmisor(string Emisor)
        {
            return GetAll().Where(m => m.UsuarioEmisor.Coda == Emisor && m.TenantId == 1).Include(m => m.UsuarioEmisor).Include(m => m.UsuarioReceptor).OrderByDescending(m => m.Id).ToList();
        }

        public List<Mensaje> GetAllMensajesByReceptor(string Receptor)
        {
            return GetAll().Where(m => m.UsuarioReceptor.Coda == Receptor).Include(m => m.UsuarioEmisor).Include(m => m.UsuarioReceptor).OrderByDescending(m => m.Id).ToList();
        }

        public Mensaje GetMensajeByIdWithReceptorAndEmisor(int MensajeId)
        {
            return GetAll().Where(m => m.Id == MensajeId).Include(m => m.UsuarioEmisor).Include(m => m.UsuarioReceptor).OrderByDescending(m => m.Id).FirstOrDefault();
        }
    }
}
