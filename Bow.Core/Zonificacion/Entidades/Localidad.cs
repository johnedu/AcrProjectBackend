using Abp.Domain.Entities;
using Bow.Afiliaciones.Entidades;
using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class Localidad : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int DepartamentoId { get; set; }
        public virtual Departamento DepartamentoLocalidad { get; set; }      
        public int Habitantes { get; set; }
        public virtual ICollection<Zona> Zonas { get; set; }
        public virtual ICollection<TorieLocalidad> TiposOrientacionLocalidad { get; set; }
        public virtual ICollection<SufijoLocalidad> SufijosLocalidad { get; set; }
        public virtual ICollection<Telefono> Telefonos { get; set; }
        public virtual ICollection<Barrio> Barrios { get; set; }
        public virtual ICollection<InfoTributariaLocalidad> InfoTributariaLocalidades { get; set; }
        public virtual ICollection<RecaudoMasivoCobertura> RecaudoMasivoCobertura { get; set; }
        public virtual ICollection<AfiliadoProspecto> AfiliadosProspecto { get; set; }
        public virtual ICollection<GestionProspecto> LocalidadGestionProspecto { get; set; }

        public Localidad()
        {
            Zonas = new List<Zona>();
            TiposOrientacionLocalidad = new List<TorieLocalidad>();
            SufijosLocalidad = new List<SufijoLocalidad>();
            Telefonos = new List<Telefono>();
            Barrios = new List<Barrio>();
            InfoTributariaLocalidades = new List<InfoTributariaLocalidad>();
            RecaudoMasivoCobertura = new List<RecaudoMasivoCobertura>();
            AfiliadosProspecto = new List<AfiliadoProspecto>();
            LocalidadGestionProspecto = new List<GestionProspecto>();
        }
    }
}
