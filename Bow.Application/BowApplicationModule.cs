using System.Reflection;
using Abp.Modules;
using AutoMapper;
using Bow.Utilidades.AutoMapper;
using System.Linq;
using System;

namespace Bow
{
    [DependsOn(typeof(BowCoreModule))]
    public class BowApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            InitilizeAutoMapper();
        }

        /// <summary>
        /// Este método se encarga de crear los mappings para el automapper. Se tiene en esta clase
        /// ya que es necesario que solo se ejecute una vez en la aplicación. El método busca automáticamente
        /// con Reflection las clases que heredan de AutoMapperBaseProfile y crea la instancia de la clase (Con el método Activator.CreateInstance)
        /// para que los mappings queden definidos. En caso de que no se hiciera con reflection seria necesario llamar a cada una de las
        /// clases que heredan de AutoMapperBaseProfile donde se definen los mappings en este método.
        /// <author>Hozkar Patrick LLano</author>
        /// </summary>
        private void InitilizeAutoMapper()
        {
            Mapper.Initialize(x =>
                {
                    var profiles = typeof(AutoMapperBaseProfile).Assembly.GetTypes().Where(perfil => perfil.IsSubclassOf(typeof(AutoMapperBaseProfile)));
                    foreach (var perfil in profiles)
                    {
                        x.AddProfile((AutoMapperBaseProfile)Activator.CreateInstance(perfil));
                    }
                });
        }
    }
}
