using Bow.EntityFramework.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Abp.EntityFramework;
using Bow.EntityFramework;
using System.Linq.Dynamic;

namespace Bow.Personas.Repositorios
{
    public class PersonaRepositorio : BowRepositoryBase<Persona>, IPersonaRepositorio
    {

        public PersonaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<Persona> GetWithTipoDocumentoAndDepartamentoByDatosBasicosHomonimoConSinDocumento(string nombrePersona, string apellido1, string apellido2)
        {
            //Se crean las variables tipo lista para almacenar los resultados
            List<Persona> consulta = new List<Persona>();
            List<Persona> resultado = new List<Persona>();
            List<Persona> consultaNombreCompleto = new List<Persona>();

            //Proceso para separar los nombres del nombre compuesto
            string[] nombreCompuesto = nombrePersona.Split(' ');
            string[] nombre = new string[nombreCompuesto.Length];

            for (int i = 0; i < nombreCompuesto.Length; i++)
            {
                //Solo se agrega si en el nombre compuesto tiene un nombre mayor a 3 caracteres
                if (nombreCompuesto[i].Length > 3)
                {
                    nombre[i] = nombreCompuesto[i];
                }
            }

            //Se consulta con el nombre completo y se consulta por el segundo apellido si se indico.
            if (!string.IsNullOrEmpty(apellido2))
            {
                consultaNombreCompleto = GetAll().Where(p => p.Nombre.ToLower().Contains(nombrePersona) && p.Apellido1.ToLower() == apellido1.ToLower() && p.Apellido2.ToLower() == apellido2.ToLower())
                   .Include(to => to.PersonaTipoDocumentoPersona)
                   .Include(to => to.PersonaPais).ToList();
            }
            else
            {
                consultaNombreCompleto = GetAll().Where(p => p.Nombre.ToLower().Contains(nombrePersona) && p.Apellido1.ToLower() == apellido1.ToLower())
                   .Include(to => to.PersonaTipoDocumentoPersona)
                   .Include(to => to.PersonaPais).ToList();
            }

            //Se consulta por cada nombre del nombre compuesto obviando el resultado de la consulta del nombre completo
            for (int j = 0; j < nombre.Length; j++)
            {
                if (nombre[j] != null)
                {
                    var nombreConsultar = nombre[j].ToLower();

                    //Se verifica si indico el segundo apellido para realizar la consulta correspondiente
                    if (!string.IsNullOrEmpty(apellido2))
                    {
                        consulta = GetAll().Where(p => p.Nombre.ToLower().Contains(nombreConsultar) && p.Apellido1.ToLower() == apellido1.ToLower() && p.Apellido2.ToLower() == apellido2.ToLower())
                                .Include(to => to.PersonaTipoDocumentoPersona)
                                .Include(to => to.PersonaPais).ToList().Except(consultaNombreCompleto).ToList();
                    }
                    else
                    {
                        consulta = GetAll().Where(p => p.Nombre.ToLower().Contains(nombreConsultar) && p.Apellido1.ToLower() == apellido1.ToLower())
                               .Include(to => to.PersonaTipoDocumentoPersona)
                               .Include(to => to.PersonaPais).ToList().Except(consultaNombreCompleto).ToList();
                    }

                    //Se junta cada lista que arroja el resultado por cada nombre del nombre compuesto
                    resultado = resultado.Concat(consulta).ToList();
                }
            }

            //Se vuelve a juntar con la lista que arrojo la consulta por el nombre compuesto
            return resultado.Concat(consultaNombreCompleto).Take(30).ToList();

        }

        public List<Persona> GetWithTipoDocumentoAndPaisAndDepartamentoByDocumento(string numeroDocumento)
        {
            return GetAll().Where(p => p.NumeroDocumento.ToLower() == numeroDocumento.ToLower())
              .Include(to => to.PersonaTipoDocumentoPersona)
              .Include(to => to.PersonaPais).ToList();
        }

        public Persona GetWithTipoProfesion(int personaId)
        {
            return GetAll().Where(p => p.Id == personaId).Include(p => p.TipoProfesion).FirstOrDefault();
        }

    }
}
