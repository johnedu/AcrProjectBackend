using Bow.EntidadBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
   public class AfiliadoProspecto : EntidadMultiTenant
    {
        public int GestionProspectoId { get; set; }
        public GestionProspecto GestionProspecto { get; set; }
        public int ParentescoId { get; set; }
        public Parentesco Parentesco { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int Edad { get; set; }
        public int CiudadResidenciaId { get; set; }
        public Localidad CiudadResidencia { get; set; }
        public bool BebePorNacer { get; set; }
    }
}
