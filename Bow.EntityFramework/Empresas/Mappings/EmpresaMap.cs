using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class EmpresaMap: MultiTenantMap<Empresa>
    {
        public EmpresaMap()
        {
            //Atributos
            Property(empresa => empresa.Documento).HasMaxLength(30);
            Property(empresa => empresa.Documento).IsRequired();

            Property(empresa => empresa.RazonSocial).HasMaxLength(100);
            Property(empresa => empresa.RazonSocial).IsRequired();

            Property(empresa => empresa.NombreComercial).HasMaxLength(100);

            //Llaves Foraneas
            HasMany<EmpresaTelefono>(empresaTelefono => empresaTelefono.EmpresaTelefonos)
              .WithRequired(empresaTelefono => empresaTelefono.EmpresaEmpresaTelefono)
              .HasForeignKey(empresaTelefono => empresaTelefono.EmpresaId)
              .WillCascadeOnDelete(false);

            HasMany<EmpresaContacto>(empresaContacto => empresaContacto.EmpresaContactos)
              .WithRequired(empresaContacto => empresaContacto.EmpresaEmpresaContacto)
              .HasForeignKey(empresaContacto => empresaContacto.EmpresaId)
              .WillCascadeOnDelete(false);

            HasMany<EmpresaContactoWeb>(empresaContactoWeb => empresaContactoWeb.EmpresaContactosWeb)
              .WithRequired(empresaContactoWeb => empresaContactoWeb.EmpresaEmpresaContactoWeb)
              .HasForeignKey(empresaContactoWeb => empresaContactoWeb.EmpresaId)
              .WillCascadeOnDelete(false);

            HasMany<EmpresaOrganizacion>(empresaOrganizacion => empresaOrganizacion.EmpresaOrganizacion)
              .WithRequired(empresaOrganizacion => empresaOrganizacion.EmpresaEmpresaOrganizacion)
              .HasForeignKey(empresaOrganizacion => empresaOrganizacion.EmpresaId)
              .WillCascadeOnDelete(false);

            HasMany<EmpresaInfoTributaria>(empresaInfoTributaria => empresaInfoTributaria.EmpresaInfoTributaria)
              .WithRequired(empresaInfoTributaria => empresaInfoTributaria.EmpresaEmpresaInfoTributaria)
              .HasForeignKey(empresaInfoTributaria => empresaInfoTributaria.EmpresaId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("empresa");
        }
    }
}