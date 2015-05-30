using Abp.Domain.Entities;
using Bow.Afiliaciones.Entidades;
using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class Direccion : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Pista { get; set; }
        public int? ManzanaId { get; set; }
        public virtual Manzana ManzanaDireccion { get; set; }
        public int? BarrioId { get; set; }
        public virtual Barrio BarrioDireccion { get; set; }
        public int? TorieLocalidad1Id { get; set; }
        public virtual TorieLocalidad TorieLocalidad1 { get; set; }
        public int? Orientacion1 { get; set; }
        public int? SufijoLocalidad1Id { get; set; }
        public virtual SufijoLocalidad SufijoLocalidad1 { get; set; }
        public int? TorieLocalidad2Id { get; set; }
        public virtual TorieLocalidad TorieLocalidad2 { get; set; }
        public int? Orientacion2 { get; set; }
        public int? SufijoLocalidad2Id { get; set; }
        public virtual SufijoLocalidad SufijoLocalidad2 { get; set; }
        public string Porton { get; set; }
        public string Apartamento { get; set; }
        public string DireccionCompleta { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Empresa> Empresas { get; set; }
        public virtual ICollection<PersonaDireccion> PersonaDirecciones { get; set; }
        public virtual ICollection<Sucursal> Sucursales { get; set; }
        public virtual ICollection<Prospecto> DireccionProspecto { get; set; }

        public Direccion()
        {
            Empresas = new List<Empresa>();
            PersonaDirecciones = new List<PersonaDireccion>();
            Sucursales = new List<Sucursal>();
            DireccionProspecto = new List<Prospecto>();
        }
    }
}
