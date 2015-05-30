using Abp.Application.Services;
using Bow.Cartera.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Cartera
{
    public interface ICarteraService : IApplicationService
    {
        /// <summary>
        ///     Se encarga de obtener la lista de monedas
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Cartera.DTOs.OutputModels.GetAllMonedaOutput"/>
        /// </return>
        GetAllMonedaOutput GetAllMoneda();

    }
}
