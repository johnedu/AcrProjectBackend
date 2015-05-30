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
    public class EmpresaOrganizacionRepositorio : BowRepositoryBase<EmpresaOrganizacion>, IEmpresaOrganizacionRepositorio
    {
        public EmpresaOrganizacionRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<EmpresaOrganizacion> GetAllEmpresasByOrganizacion(int OrganizacionId)
        {
            return GetAll().Where(org => org.OrganizacionId == OrganizacionId).Include(org => org.EmpresaEmpresaOrganizacion).OrderBy(org => org.EmpresaEmpresaOrganizacion.RazonSocial).ToList();
        }

        public EmpresaOrganizacion GetEmpresaWithOrganizacion(int OrganizacionId, int EmpresaId)
        {
            return GetAll().Where(emp => emp.OrganizacionId == OrganizacionId && emp.EmpresaId == EmpresaId).Include(dir => dir.EmpresaEmpresaOrganizacion.Direccion).Include(tdoc => tdoc.EmpresaEmpresaOrganizacion.TipoDocumento.TipoDocumentoPersonaPais).Include(act => act.EmpresaEmpresaOrganizacion.ActividadEconomica).Include(est => est.EstadoEmpresaOrganizacion.EstadoNombreEstado).FirstOrDefault();
        }
    }
}
