using Abp.EntityFramework;
using Bow.Afiliaciones.Entidades;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Bow.Zonificacion.Entidades;
using Bow.Empresas.Entidades;

namespace Bow.Afiliaciones.Repositorios
{
    public class RecaudoMasivoCoberturaRepositorio: BowRepositoryBase<RecaudoMasivoCobertura>, IRecaudoMasivoCoberturaRepositorio
    {
        public RecaudoMasivoCoberturaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<Localidad> GetAllLocalidadAndDepartamentoAndPaisByConvenio(int ConvenioId)
        {
            return GetAll().Where(loc => loc.RecaudoMasivoId == ConvenioId).Select(loc => loc.LocalidadRecaudoMasivo).Include(loc => loc.DepartamentoLocalidad.PaisDepartamento).OrderBy(loc => loc.DepartamentoLocalidad.PaisDepartamento.Nombre).ThenBy(loc => loc.DepartamentoLocalidad.Nombre).ThenBy(loc => loc.Nombre).ToList();
        }

        public List<Localidad> GetAllLocalidadByConvenioAndPais(int ConvenioId, int paisId)
        {
            return GetAll().Where(loc => loc.RecaudoMasivoId == ConvenioId && loc.LocalidadRecaudoMasivo.DepartamentoLocalidad.PaisId == paisId).Select(loc => loc.LocalidadRecaudoMasivo).Include(loc => loc.DepartamentoLocalidad.PaisDepartamento).OrderBy(loc => loc.DepartamentoLocalidad.PaisDepartamento.Nombre).ThenBy(loc => loc.DepartamentoLocalidad.Nombre).ThenBy(loc => loc.Nombre).ToList();
        }

        public List<Localidad> GetAllLocalidadByConvenioAndDepartamento(int ConvenioId, int deptoId)
        {
            return GetAll().Where(loc => loc.RecaudoMasivoId == ConvenioId && loc.LocalidadRecaudoMasivo.DepartamentoId == deptoId).Select(loc => loc.LocalidadRecaudoMasivo).Include(loc => loc.DepartamentoLocalidad.PaisDepartamento).OrderBy(loc => loc.DepartamentoLocalidad.PaisDepartamento.Nombre).ThenBy(loc => loc.DepartamentoLocalidad.Nombre).ThenBy(loc => loc.Nombre).ToList();
        }

        public List<Pais> GetAllPaisesByConvenio(int convenioId)
        {
            return GetAll().Where(loc => loc.RecaudoMasivoId == convenioId).Select(p => p.LocalidadRecaudoMasivo.DepartamentoLocalidad.PaisDepartamento).Distinct().OrderBy(p => p.Nombre).ToList();
        }

        public List<Departamento> GetAllDepartamentosByConvenio(int convenioId)
        {
            return GetAll().Where(loc => loc.RecaudoMasivoId == convenioId).Select(d => d.LocalidadRecaudoMasivo.DepartamentoLocalidad).Distinct().OrderBy(d => d.Nombre).ToList();
        }

        public List<RecaudoMasivo> GetAllRecaudosMasivosByLocalidad(int localidadId)
        {
            return GetAll().Where(rec => rec.LocalidadId == localidadId).Select(d => d.RecaudoMasivoCoberturaRecaudoMasivo).OrderBy(d => d.Nombre).ToList();
        }
    }
}
