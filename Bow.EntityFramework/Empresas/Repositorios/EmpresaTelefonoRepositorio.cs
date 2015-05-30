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
    public class EmpresaTelefonoRepositorio : BowRepositoryBase<EmpresaTelefono>, IEmpresaTelefonoRepositorio
    {
        public EmpresaTelefonoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<EmpresaTelefono> GetAllTelefonosWithLocalidad(int EmpresaId)
        {
            return GetAll().Where(emp => emp.EmpresaId == EmpresaId).Include(loc => loc.TelefonoEmpresaTelefono.LocalidadTelefono.DepartamentoLocalidad).OrderBy(tel => tel.TelefonoEmpresaTelefono.Numero).ToList();
        }
    }
}
