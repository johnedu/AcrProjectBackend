using Abp.Localization;
using Abp.UI;
using AutoMapper;
using Bow.Cartera.DTOs.OutputModels;
using Bow.Cartera.Repositorios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Cartera
{
    public class CarteraService : ICarteraService
    {

        # region Repositorios

        private IMonedaRepositorio _monedaRepositorio;

        # endregion

        public CarteraService(IMonedaRepositorio monedaRepositorio)
        {
            _monedaRepositorio = monedaRepositorio;
        }

        /***************************************************************************************************
         * Moneda
         * ************************************************************************************************/

        public GetAllMonedaOutput GetAllMoneda()
        {
            var listaMonedas = _monedaRepositorio.GetAllList();
            return new GetAllMonedaOutput { Monedas = Mapper.Map<List<MonedaOutput>>(listaMonedas) };
        }
    }
}
