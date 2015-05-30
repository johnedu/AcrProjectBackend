using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class Parentesco : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int Posicion { get; set; }
        public string Genero { get; set; }
        public int Repeticiones { get; set; }
        public string Limite { get; set; }
        public int? EdadDiferencia { get; set; }
        public bool CoincidirApellidos { get; set; }

        public virtual ICollection<GrupoFamiliarParentesco> GruposFamiliaresParentesco { get; set; }
        public virtual ICollection<AfiliadoProspecto> AfiliadosProspecto { get; set; }

        public Parentesco()
        {
            GruposFamiliaresParentesco = new List<GrupoFamiliarParentesco>();
            AfiliadosProspecto = new List<AfiliadoProspecto>();
        }

    }
}