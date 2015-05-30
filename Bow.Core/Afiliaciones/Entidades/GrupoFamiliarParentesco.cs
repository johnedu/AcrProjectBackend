using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class GrupoFamiliarParentesco : EntidadMultiTenant
    {
        public int ParentescoId { get; set; }
        public virtual Parentesco Parentesco { get; set; }
        public int GrupoFamiliarId { get; set; }
        public virtual GrupoFamiliar GrupoFamiliar { get; set; }
        public string ValidarSoloIngreso { get; set; }

        public virtual ICollection<GrupoParentescoRango> GruposParentescoRango { get; set; }

        public GrupoFamiliarParentesco()
        {
            GruposParentescoRango = new List<GrupoParentescoRango>();
        }
    }
}