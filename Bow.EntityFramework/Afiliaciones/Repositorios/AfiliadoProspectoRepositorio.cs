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

namespace Bow.Afiliaciones.Repositorios
{
    public class AfiliadoProspectoRepositorio : BowRepositoryBase<AfiliadoProspecto>, IAfiliadoProspectoRepositorio
    {
        public AfiliadoProspectoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }


        public List<AfiliadoProspecto> GetWithParentescoAndLocalidadByGestionProspecto(int gestionProspectoId)
        {
            return GetAll().Where(a => a.GestionProspectoId == gestionProspectoId)
                .Include(p => p.Parentesco)
                .Include(l => l.CiudadResidencia).ToList();
        }

        public AfiliadoProspecto GetWithParentescoAndLocalidadByAfiliadoProspecto(int afiliadoProspectoId)
        {
            return GetAll().Where(af => af.Id == afiliadoProspectoId)
                .Include(p => p.Parentesco)
                .Include(l => l.CiudadResidencia.DepartamentoLocalidad.PaisDepartamento).FirstOrDefault();
        }
    }
}