using Bow.EntityFramework.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;
using System.Data.Entity;

namespace Bow.Empresas.Repositorios
{
    public class SucursalRepositorio : BowRepositoryBase<Sucursal>, ISucursalRepositorio
    {
        public SucursalRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<Sucursal> GetAllSucursalesWithTipoByEmpresa(int EmpresaOrganizacionId)
        {
            return GetAll().Where(suc => suc.EmpresaOrganizacionId == EmpresaOrganizacionId).Include(suc => suc.SucursalTipo).OrderBy(org => org.Nombre).ToList();
        }

        public Sucursal GetSucursalWithTipoAndEstadoAndDireccion(int EmpresaOrganizacionId, int SucursalId)
        {
            return GetAll().Where(suc => suc.EmpresaOrganizacionId == EmpresaOrganizacionId && suc.Id == SucursalId).Include(suc => suc.SucursalTipo).Include(suc => suc.SucursalEstado).Include(suc => suc.SucursalDireccion.BarrioDireccion.Localidad).OrderBy(org => org.Nombre).FirstOrDefault();
        }

        public List<Sucursal> GetAllSucursalesWithEmpresaAndOrganizacion()
        {
            return GetAll().Include(eo => eo.EmpresaOrganizacion)
                .Include(o => o.EmpresaOrganizacion.OrganizacionEmpresaOrganizacion)
                .Include(e => e.EmpresaOrganizacion.EmpresaEmpresaOrganizacion).ToList();
        }

        public Sucursal GetSucursalByIdWithEmpresaAndOrganizacion(int SucursalId)
        {
            return GetAll().Where(suc => suc.Id == SucursalId).Include(suc => suc.EmpresaOrganizacion.EmpresaEmpresaOrganizacion).FirstOrDefault();
        }
    }
}
