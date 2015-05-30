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
    public class GestionProspectoRepositorio : BowRepositoryBase<GestionProspecto>, IGestionProspectoRepositorio
    {
        public GestionProspectoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public GestionProspecto GetAllGestionProspecto (int gestionId)
        {
            return GetAll().Where(g => g.Id == gestionId)
                .Include(p => p.Persona)
                .Include(g => g.GrupoFamiliar.PlanExequialGrupoFamiliar)
                .Include(e => e.EstadoNoAfiliacion)
                .Include(f => f.FunenariaAfiliado)
                .Include(a => a.AfiliadoProspecto)
                .FirstOrDefault();
        }
       
    }
}
