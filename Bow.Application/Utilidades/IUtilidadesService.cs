using Abp.Application.Services;
using Bow.Utilidades.DTOs.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Utilidades
{
    public interface IUtilidadesService : IApplicationService
    {
        void ReadFile(ReadFileInput ruta);

    }
}
