using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class Manzana : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int? TorieLocalidad1Id { get; set; }
        public TorieLocalidad TorieLocalidad1 { get; set; }
        public int? Orientacion1 { get; set; }
        public int? SufijoLocalidad1Id { get; set; }
        public SufijoLocalidad SufijoLocalidad1 { get; set; }
        public int? TorieLocalidad2Id { get; set; }
        public TorieLocalidad TorieLocalidad2 { get; set; }
        public int? Orientacion2 { get; set; }
        public int? SufijoLocalidad2Id { get; set; }
        public SufijoLocalidad SufijoLocalidad2 { get; set; }
        public int BarrioId { get; set; }
        public Barrio BarrioManzana { get; set; }
        public virtual ICollection<Direccion> Direcciones { get; set; }

        public Manzana()
        {
            Direcciones = new List<Direccion>();
        }
    }
}
