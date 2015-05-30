using Abp.Localization;
using Abp.UI;
using AutoMapper;
using Bow.Utilidades;
using Bow.Utilidades.DTOs.InputModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones
{
    public class UtilidadesService : IUtilidadesService
    {

        # region Repositorios

        

        # endregion

        public UtilidadesService()
        {
            
        }

        /***************************************************************************************************
         * Lectura de Archivos
         * ************************************************************************************************/

        //  Método para leer los archivos
        public void ReadFile(ReadFileInput ruta)
        {
            string rutaCompleta = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\Tmp\\FileUploads\\" + ruta.Ruta;
            int counter = 0;
            string line;
            List<string> listaLineas = new List<string>();

            StreamReader file = new StreamReader(rutaCompleta);
            while ((line = file.ReadLine()) != null)
            {
                listaLineas.Add(line);
                counter++;
            }
            file.Close();
        }

    }
}
