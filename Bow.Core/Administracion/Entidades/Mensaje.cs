using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class Mensaje : EntidadMultiTenant
    {
        public int UsuarioEmisorId { get; set; }
        public Usuario UsuarioEmisor { get; set; }
        public int UsuarioReceptorId { get; set; }
        public Usuario UsuarioReceptor { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public bool FueLeido { get; set; }
    }
}
