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

    public class InfoTributariaRepositorio : BowRepositoryBase<InfoTributaria>, IInfoTributariaRepositorio
    {
            public InfoTributariaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

            public List<InfoTributaria> GetAllListWithOpcionesAndLocalidades()
            {
                return GetAll().Include(opc => opc.InfoTributariaOpciones).Include(loc => loc.InfoTributariaLocalidades).ToList();
            }
        }
      
    }

