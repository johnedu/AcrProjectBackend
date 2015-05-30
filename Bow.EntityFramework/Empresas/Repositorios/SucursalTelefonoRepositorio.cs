using Bow.EntityFramework.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Empresas.Repositorios
{
    public class SucursalTelefonoRepositorio : BowRepositoryBase<SucursalTelefono>, ISucursalTelefonoRepositorio
    {
        public SucursalTelefonoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<SucursalTelefono> GetAllTelefonosWithLocalidad(int SucursalId)
        {
            return GetAll().Where(suc => suc.SucursalId == SucursalId).Include(loc => loc.TelefonoSucursalTelefono.LocalidadTelefono.DepartamentoLocalidad).OrderBy(tel => tel.TelefonoSucursalTelefono.Numero).ToList();
        }
    }
}
