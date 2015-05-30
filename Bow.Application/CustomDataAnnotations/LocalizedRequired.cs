using Abp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.CustomDataAnnotations
{
    public class LocalizedRequired : ValidationAttribute
    {
        public string _mensajeErrorKey;

        public LocalizedRequired()
        {
        }

        public LocalizedRequired(string mensajeErrorKey)
        {
            _mensajeErrorKey = mensajeErrorKey;
        }

        public override bool IsValid(object value)
        {
            bool respuesta = true;
            if (value == null)
            {
                respuesta = false;
                base.ErrorMessage = LocalizationHelper.GetString("Bow", _mensajeErrorKey);
            }
                
            return respuesta;
        }
    }
}
