using Bow.EntityFramework.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Zonificacion.Repositorios
{
    public class TelefonoRepositorio : BowRepositoryBase<Telefono>, ITelefonoRepositorio
    {
        public TelefonoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public Telefono GetWithLocalidad(int id)
        {
            return GetAll().Where(to => to.Id == id).Include(to => to.LocalidadTelefono.DepartamentoLocalidad).FirstOrDefault();
        }

        public Telefono GetWithLocalidadAndDepartamentoandTipo(int id)
        {
            return GetAll().Where(to => to.Id == id).Include(to => to.LocalidadTelefono.DepartamentoLocalidad).Include(to => to.TipoTelefono).FirstOrDefault();
        }

        public Telefono GetWithLocalidadAndDepartamentoandTipo(string numero, string extension)
        {
            if (extension == "")
            {
                return GetAll().Where(to => to.Numero == numero).Include(to => to.LocalidadTelefono.DepartamentoLocalidad).Include(to => to.TipoTelefono).FirstOrDefault();
            }
            else
            {
                return GetAll().Where(to => to.Numero == numero && to.Extension == extension).Include(to => to.LocalidadTelefono.DepartamentoLocalidad).Include(to => to.TipoTelefono).FirstOrDefault();
            }
        }
    }
}
