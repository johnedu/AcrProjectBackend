using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class Entidad : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int DimensionId { get; set; }
        public Dimension DimensionPregunta { get; set; }
        public bool EstadoActiva { get; set; }
        public string FechaCreacion { get; set; }
        public int UsuarioIdCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public int? UsuarioIdModificacion { get; set; }
    }
}
