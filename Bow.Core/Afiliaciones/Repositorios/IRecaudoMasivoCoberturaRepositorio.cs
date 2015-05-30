using Abp.Domain.Repositories;
using Bow.Afiliaciones.Entidades;
using Bow.Empresas.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Repositorios
{
    public interface IRecaudoMasivoCoberturaRepositorio : IRepository<RecaudoMasivoCobertura>
    {
        List<Localidad> GetAllLocalidadAndDepartamentoAndPaisByConvenio(int ConvenioId);

        List<Localidad> GetAllLocalidadByConvenioAndPais(int ConvenioId, int paisId);

        List<Localidad> GetAllLocalidadByConvenioAndDepartamento(int ConvenioId, int deptoId);

        List<Pais> GetAllPaisesByConvenio(int convenioId);

        List<Departamento> GetAllDepartamentosByConvenio(int convenioId);

        List<RecaudoMasivo> GetAllRecaudosMasivosByLocalidad(int localidadId);
    }
}
