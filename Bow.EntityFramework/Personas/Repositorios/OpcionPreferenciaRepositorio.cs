using Bow.EntityFramework.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Abp.EntityFramework;
using Bow.EntityFramework;
namespace Bow.Personas.Repositorios
{
    public class OpcionPreferenciaRepositorio : BowRepositoryBase<OpcionPreferencia>, IOpcionPreferenciaRepositorio
    {
        public OpcionPreferenciaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<OpcionPreferencia> GetAllListByPreferencia(int preferenciaId)
        {
            return GetAll().Where(op => op.PreferenciaId == preferenciaId).ToList();
        }

        public int GetCantidadOpcionPreferenciaRegistradasPorPersona(int Id)
        {
            OpcionPreferencia opcion = Get(Id);
            return opcion.PersonaPreferencia.Count();
        }

    }
}
