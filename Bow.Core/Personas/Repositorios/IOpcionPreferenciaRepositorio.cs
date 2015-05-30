using Abp.Domain.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Repositorios
{
    public interface IOpcionPreferenciaRepositorio : IRepository<OpcionPreferencia>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferenciaId"></param>
        /// <returns></returns>
        List<OpcionPreferencia> GetAllListByPreferencia(int preferenciaId);

        /// <summary>
        /// Retorna la cantidad de veces que se encuentra registrada una opción preferencia 
        /// en preferencia Persona
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int GetCantidadOpcionPreferenciaRegistradasPorPersona(int Id);
    }
}
