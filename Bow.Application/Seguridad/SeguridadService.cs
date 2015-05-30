using Abp.Domain.Repositories;
using Bow.Seguridad.MultiTenancy;
using Bow.Seguridad.Usuarios;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Seguridad
{
    public class SeguridadService : ISeguridadService
    {
        #region Repositorios
        private TenantManager _tenantManager;
        private UserManager _userManager;
        #endregion

        public SeguridadService(TenantManager tenantManager, UserManager userManager)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
        }

        public async Task CrearTenant()
        {
            //List<Tenant> lista = _tenantManager.Tenants.ToList<Tenant>();
            //foreach (Tenant t in lista)
            //{
            //    string nombre = t.Name + t.TenancyName + t.Id;
            //}

            //Tenant nuevoTenant = new Tenant("Aurora", "La Aurora");

            //IdentityResult resultado = await _tenantManager.CreateAsync(nuevoTenant);


            /*Tenant nuevoTenant = new Tenant("Prueba", "Prueba 2");
            _tenantRepositorio.Insert(nuevoTenant);*/

            User usuario = new User();
            usuario.TenantId = 1;
            usuario.UserName = "aurora";
            usuario.Name = "Aurora";
            usuario.Surname="Aurora";
            usuario.EmailAddress = "aurora@gmail.com";
            usuario.IsEmailConfirmed = true;
            usuario.TenantId = 1;
            var result = await _userManager.CreateAsync(usuario, "aurora2015");
            //var lista = _userManager.Users;*/
        }
    }
}
