using Bow.EntityFramework.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Bow.Zonificacion.Entidades;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Empresas.Repositorios
{
    public class InfoTributariaLocalidadRepositorio : BowRepositoryBase<InfoTributariaLocalidad>, IInfoTributariaLocalidadRepositorio
    {
        public InfoTributariaLocalidadRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<Localidad> GetAllWithLocalidadAndDepartamentoAndPaisByInfoTributaria(int Id)
        {
            return GetAll().Where(loc => loc.InfoTributariaId == Id).Select(loc => loc.Localidad).Include(loc => loc.DepartamentoLocalidad.PaisDepartamento).OrderBy(loc => loc.DepartamentoLocalidad.PaisDepartamento.Nombre).ThenBy(loc => loc.DepartamentoLocalidad.Nombre).ThenBy(loc => loc.Nombre).ToList();
        }

        public List<Localidad> GetAllWithLocalidadByInfoTributariaAndPais(int infoTributariaId, int paisId)
        {
            return GetAll().Where(loc => loc.InfoTributariaId == infoTributariaId && loc.Localidad.DepartamentoLocalidad.PaisId == paisId).Select(loc => loc.Localidad).Include(loc => loc.DepartamentoLocalidad.PaisDepartamento).OrderBy(loc => loc.DepartamentoLocalidad.PaisDepartamento.Nombre).ThenBy(loc => loc.DepartamentoLocalidad.Nombre).ThenBy(loc => loc.Nombre).ToList();
        }

        public List<Localidad> GetAllWithLocalidadByInfoTributariaAndDepartamento(int infoTributariaId, int deptoId)
        {
            return GetAll().Where(loc => loc.InfoTributariaId == infoTributariaId && loc.Localidad.DepartamentoId == deptoId).Select(loc => loc.Localidad).Include(loc => loc.DepartamentoLocalidad.PaisDepartamento).OrderBy(loc => loc.DepartamentoLocalidad.PaisDepartamento.Nombre).ThenBy(loc => loc.DepartamentoLocalidad.Nombre).ThenBy(loc => loc.Nombre).ToList();
        }

        public List<Pais> GetAllPaisesByInfoTributaria(int infoTributariaId)
        {
            return GetAll().Where(loc => loc.InfoTributariaId == infoTributariaId).Select(p => p.Localidad.DepartamentoLocalidad.PaisDepartamento).Distinct().OrderBy(p => p.Nombre).ToList();
        }

        public List<Departamento> GetAllDepartamentosByInfoTributaria(int infoTributariaId)
        {
            return GetAll().Where(loc => loc.InfoTributariaId == infoTributariaId).Select(d => d.Localidad.DepartamentoLocalidad).Distinct().OrderBy(d => d.Nombre).ToList();
        }

        public List<InfoTributaria> GetAllInfoTributariaActivasByLocalidad(int LocalidadId)
        {
            return GetAll().Where(inf => inf.LocalidadId == LocalidadId && inf.InfoTributaria.Estado.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_VIGENTE).Include(inf => inf.InfoTributaria).Select(inf => inf.InfoTributaria).ToList();
        }
    }
}
