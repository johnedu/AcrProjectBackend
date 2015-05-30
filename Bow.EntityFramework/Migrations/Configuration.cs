namespace Bow.Migrations
{
    using Bow.Afiliaciones.Entidades;
    using Bow.Cartera.Entidades;
    using Bow.Empresas.Entidades;
    using Bow.Migrations.Data;
    using Bow.Parametros.Entidades;
    using Bow.Zonificacion.Entidades;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bow.EntityFramework.BowDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Bow";
        }

        protected override void Seed(Bow.EntityFramework.BowDbContext context)
        {
            cargarTiposOrientacion(context);
            cargarTiposZona(context);
            cargarTiposTelefono(context);
            cargarPaises(context);
            cargarEstadosPreferenciasPersonales(context);
            cargarProfesiones(context);
            cargarEstadosCiviles(context);
            cargarInformacionTributaria(context);
            cargarOrganizacion(context);  //ESTO SE DEBE QUITAR CUANDO SE CUADRE LO DE USUARIOS
            cargarEstadosTelefonosPersona(context);
            cargarMediosContacto(context);
            cargarAreasEmpresa(context);
            cargarSucursalEmpresaOrganizacion(context);
            cargarEstadosEmpleado(context);
            cargarEstadosPlanExequial(context);
            cargarMoneda(context);
            cargarTiposCategorias(context);
            cargarEstadosGrupoFamiliar(context);
            //cargarTiposParentesco(context);
            cargarTiposBeneficioPlanExequial(context);
            cargarEstadosBeneficioPlanExequial(context);
            cargarEstadosTiposRolEmpleadoZona(context);
            cargarRecaudoMasivo(context);
            cargarFunerariasProspecto(context);
            cargarEstadosClienteProspecto(context);
            cargarEstadosGrupoInformal(context);
            cargarEstadosGrupoInformalEmpleado(context);

            new InitialDataBuilder().Build(context);
        }

        /// /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga los tipos de orientación básicos en el sistema
        /// </summary>
        /// <param name="context"></param>
        /// //////////////////////////////////////////////////////////////////////////////
        private void cargarTiposOrientacion(Bow.EntityFramework.BowDbContext context)
        {
            var tiposOrientacion = new List<TipoOrientacion>
            {
                new TipoOrientacion { Nombre = BowConsts.TIPO_ORIENTACION_CALLE },
                new TipoOrientacion { Nombre = BowConsts.TIPO_ORIENTACION_CARRERA },
                new TipoOrientacion { Nombre = BowConsts.TIPO_ORIENTACION_AVENIDA },
                new TipoOrientacion { Nombre = BowConsts.TIPO_ORIENTACION_DIAGONAL },
                new TipoOrientacion { Nombre = BowConsts.TIPO_ORIENTACION_TRANSVERSAL }
            };

            tiposOrientacion.ForEach(to => context.Set<TipoOrientacion>().AddOrUpdate(t => t.Nombre, to));

            context.SaveChanges();

        }

        /// ////////////////////////////////////////////////////////////////
        /// <summary>
        /// Cargando los tipos de Zona
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////
        private void cargarTiposZona(Bow.EntityFramework.BowDbContext context)
        {
            context.Set<Parametro>().AddOrUpdate(p => p.Nombre, new Parametro { Nombre = BowConsts.PARAMETRO_TIPOS_DE_ZONA });

            context.SaveChanges();
        }

        /// /////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de los tipos de teléfono
        /// </summary>
        /// <param name="context"></param>
        /// /////////////////////////////////////////////////////////////////
        private void cargarTiposTelefono(Bow.EntityFramework.BowDbContext context)
        {
            Parametro parametroTipoTelefono = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_TIPOS_DE_TELEFONO);

            if (parametroTipoTelefono == null)
            {
                Parametro parametroTipoTel = new Parametro { Nombre = BowConsts.PARAMETRO_TIPOS_DE_TELEFONO };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroTipoTel);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_TELEFONO_FIJO, ParametroTipo = parametroTipoTel });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_TELEFONO_CELULAR, ParametroTipo = parametroTipoTel });
            }
            else
            {
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_TELEFONO_FIJO, ParametroId = parametroTipoTelefono.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_TELEFONO_CELULAR, ParametroId = parametroTipoTelefono.Id });
            }

            context.SaveChanges();
        }

        /// ///////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de Paises Iniciales
        /// </summary>
        /// <param name="context"></param>
        /// ///////////////////////////////////////////////////////////////////
        private void cargarPaises(Bow.EntityFramework.BowDbContext context)
        {
            context.Set<Pais>().AddOrUpdate(p => p.Nombre, new Pais { Nombre = BowConsts.PAIS_COLOMBIA, Indicativo = "57" });
            context.Set<Pais>().AddOrUpdate(p => p.Nombre, new Pais { Nombre = BowConsts.PAIS_ESTADOSUNIDOS, Indicativo = "1" });

            context.SaveChanges();
        }

        /// ////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de los estados de las preferencias personales
        /// </summary>
        /// <param name="context"></param>
        /// /////////////////////////////////////////////////////////////////////
        private void cargarEstadosPreferenciasPersonales(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoActivoPreferencia = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_PREFERENCIA);
            Estado estadoInactivoPreferencia = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_PREFERENCIA);

            Parametro parametroPreferencia = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_PREFERENCIA);

            if (parametroPreferencia == null)
            {
                parametroPreferencia = new Parametro { Nombre = BowConsts.PARAMETRO_PREFERENCIA };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroPreferencia);

            }
            else
            {
                existeParametro = true;
            }


            //Estado Activo
            if (estadoActivoPreferencia == null)
            {
                Estado estadoActivoPref = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_ACTIVO };

                if (existeParametro == false)
                {
                    estadoActivoPref.ParametroEstado = parametroPreferencia;
                }
                else
                {
                    estadoActivoPref.ParametroId = parametroPreferencia.Id;
                }

                NombreEstado nombreEstadoActivoPreferencia = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivoPreferencia == null)
                {
                    NombreEstado nombreEstadoActivoPref = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoActivoPref);
                    estadoActivoPref.EstadoNombreEstado = nombreEstadoActivoPref;
                }
                else
                {
                    estadoActivoPref.EstadoNombreId = nombreEstadoActivoPreferencia.Id;
                }

                context.Set<Estado>().Add(estadoActivoPref);
            }

            //Estado Inactivo           
            if (estadoInactivoPreferencia == null)
            {
                Estado estadoInactivoPref = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_INACTIVO };

                if (existeParametro == false)
                {
                    estadoInactivoPref.ParametroEstado = parametroPreferencia;
                }
                else
                {
                    estadoInactivoPref.ParametroId = parametroPreferencia.Id;
                }

                NombreEstado nombreEstadoInactivoPreferencia = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO);

                if (nombreEstadoInactivoPreferencia == null)
                {
                    NombreEstado nombreEstadoInactivoPref = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInactivoPref);
                    estadoInactivoPref.EstadoNombreEstado = nombreEstadoInactivoPref;
                }
                else
                {
                    estadoInactivoPref.EstadoNombreId = nombreEstadoInactivoPreferencia.Id;
                }

                context.Set<Estado>().Add(estadoInactivoPref);
            }

            context.SaveChanges();
        }

        /// //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de las profesiones
        /// </summary>
        /// <param name="context"></param>
        /// //////////////////////////////////////////////////////////////////////////
        private void cargarProfesiones(Bow.EntityFramework.BowDbContext context)
        {
            Parametro parametroProfesion = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_PROFESIONES);

            if (parametroProfesion == null)
            {
                Parametro parametroTipoProfesion = new Parametro { Nombre = BowConsts.PARAMETRO_PROFESIONES };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroTipoProfesion);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_PROFESION_NO_IDENTIFICADA, ParametroTipo = parametroTipoProfesion });
            }
            else
            {
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_PROFESION_NO_IDENTIFICADA, ParametroId = parametroProfesion.Id });
            }

            context.SaveChanges();
        }

        /// //////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de los estados civiles
        /// </summary>
        /// <param name="context"></param>
        /// /////////////////////////////////////////////////////////////////////////////
        private void cargarEstadosCiviles(Bow.EntityFramework.BowDbContext context)
        {
            Parametro parametroEstadoCivil = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_ESTADO_CIVIL);

            if (parametroEstadoCivil == null)
            {
                Parametro parametroTipoEstadoCivil = new Parametro { Nombre = BowConsts.PARAMETRO_ESTADO_CIVIL };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroTipoEstadoCivil);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_ESTADO_CIVIL_NO_IDENTIFICADO, ParametroTipo = parametroTipoEstadoCivil });
            }
            else
            {
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_ESTADO_CIVIL_NO_IDENTIFICADO, ParametroId = parametroEstadoCivil.Id });
            }

            context.SaveChanges();
        }

        /// ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de Información Tributaria Básica
        /// </summary>
        /// <param name="context"></param>
        /// ///////////////////////////////////////////////////////////////////////
        private void cargarInformacionTributaria(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoVigenteInfoTributaria = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_VIGENTE && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_VIGENTE && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_INFO_TRIBUTARIA);
            Estado estadoNoVigenteInfoTributaria = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_NO_VIGENTE && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_NO_VIGENTE && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_INFO_TRIBUTARIA);

            Parametro parametroInfoTributaria = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_INFO_TRIBUTARIA);

            if (parametroInfoTributaria == null)
            {
                parametroInfoTributaria = new Parametro { Nombre = BowConsts.PARAMETRO_INFO_TRIBUTARIA };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroInfoTributaria);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_INFO_TRIBUTARIA_VALOR, ParametroTipo = parametroInfoTributaria });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_INFO_TRIBUTARIA_PORCENTAJE, ParametroTipo = parametroInfoTributaria });
            }
            else
            {
                existeParametro = true;
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_INFO_TRIBUTARIA_VALOR, ParametroId = parametroInfoTributaria.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_INFO_TRIBUTARIA_PORCENTAJE, ParametroId = parametroInfoTributaria.Id });
            }


            //Estado Vigente
            if (estadoVigenteInfoTributaria == null)
            {
                Estado estadoVigenteInfoTribu = new Estado { Motivo = BowConsts.ESTADO_INFO_TRIBUTARIA_MOTIVO_VIGENTE };

                if (existeParametro == false)
                {
                    estadoVigenteInfoTribu.ParametroEstado = parametroInfoTributaria;
                }
                else
                {
                    estadoVigenteInfoTribu.ParametroId = parametroInfoTributaria.Id;
                }

                NombreEstado nombreEstadoVigenteInfoTributaria = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_VIGENTE && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_VIGENTE);

                if (nombreEstadoVigenteInfoTributaria == null)
                {
                    NombreEstado nombreEstadoVigenteInfoTribu = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_VIGENTE, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_VIGENTE };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoVigenteInfoTribu);
                    estadoVigenteInfoTribu.EstadoNombreEstado = nombreEstadoVigenteInfoTribu;
                }
                else
                {
                    estadoVigenteInfoTribu.EstadoNombreId = nombreEstadoVigenteInfoTributaria.Id;
                }

                context.Set<Estado>().Add(estadoVigenteInfoTribu);
            }

            //Estado No Vigente
            if (estadoNoVigenteInfoTributaria == null)
            {
                Estado estadoNoVigenteInfoTribu = new Estado { Motivo = BowConsts.ESTADO_INFO_TRIBUTARIA_MOTIVO_NO_VIGENTE };

                if (existeParametro == false)
                {
                    estadoNoVigenteInfoTribu.ParametroEstado = parametroInfoTributaria;
                }
                else
                {
                    estadoNoVigenteInfoTribu.ParametroId = parametroInfoTributaria.Id;
                }

                NombreEstado nombreEstadoNoVigenteInfoTributaria = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_NO_VIGENTE && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_NO_VIGENTE);

                if (nombreEstadoNoVigenteInfoTributaria == null)
                {
                    NombreEstado nombreEstadoNoVigenteInfoTribu = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_NO_VIGENTE, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_NO_VIGENTE };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoNoVigenteInfoTribu);
                    estadoNoVigenteInfoTribu.EstadoNombreEstado = nombreEstadoNoVigenteInfoTribu;
                }
                else
                {
                    estadoNoVigenteInfoTribu.EstadoNombreId = nombreEstadoNoVigenteInfoTributaria.Id;
                }

                context.Set<Estado>().Add(estadoNoVigenteInfoTribu);
            }

            context.SaveChanges();
        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de los Estados de Telefonos
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarEstadosTelefonosPersona(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoTelefonoActivo = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.ESTADO_TELEFONO_DIRECCION_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_TELEFONO_DIRECCION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_TIPOS_DE_UBICACION);
            Estado estadoTelefonoInactivo = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.ESTADO_TELEFONO_DIRECCION_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_TELEFONO_DIRECCION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_TIPOS_DE_UBICACION);

            Parametro parametroEstadoTelefono = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_TIPOS_DE_UBICACION);

            if (parametroEstadoTelefono == null)
            {
                parametroEstadoTelefono = new Parametro { Nombre = BowConsts.PARAMETRO_TIPOS_DE_UBICACION };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroEstadoTelefono);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_UBICACION_PERSONAL, ParametroTipo = parametroEstadoTelefono });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_UBICACION_RESIDENCIAL, ParametroTipo = parametroEstadoTelefono });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_UBICACION_LABORAL, ParametroTipo = parametroEstadoTelefono });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_UBICACION_ALTERNO, ParametroTipo = parametroEstadoTelefono });
            }
            else
            {
                existeParametro = true;
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_UBICACION_PERSONAL, ParametroId = parametroEstadoTelefono.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_UBICACION_RESIDENCIAL, ParametroId = parametroEstadoTelefono.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_UBICACION_LABORAL, ParametroId = parametroEstadoTelefono.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_UBICACION_ALTERNO, ParametroId = parametroEstadoTelefono.Id });
            }


            //Estado Activo
            if (estadoTelefonoActivo == null)
            {
                Estado estadoActivo = new Estado { Motivo = BowConsts.ESTADO_TELEFONO_DIRECCION_ACTIVO };

                if (existeParametro == false)
                {
                    estadoActivo.ParametroEstado = parametroEstadoTelefono;
                }
                else
                {
                    estadoActivo.ParametroId = parametroEstadoTelefono.Id;
                }

                NombreEstado nombreEstadoActivo = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.ESTADO_TELEFONO_DIRECCION_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_TELEFONO_DIRECCION_ACTIVO);

                if (nombreEstadoActivo == null)
                {
                    NombreEstado nombreEstadoAct = new NombreEstado { Nombre = BowConsts.ESTADO_TELEFONO_DIRECCION_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_TELEFONO_DIRECCION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoAct);
                    estadoActivo.EstadoNombreEstado = nombreEstadoAct;
                }
                else
                {
                    estadoActivo.EstadoNombreId = nombreEstadoActivo.Id;
                }

                context.Set<Estado>().Add(estadoActivo);
            }

            //Estado inactivo
            if (estadoTelefonoInactivo == null)
            {
                Estado estadoInactivo = new Estado { Motivo = BowConsts.ESTADO_TELEFONO_DIRECCION_INACTIVO };

                if (existeParametro == false)
                {
                    estadoInactivo.ParametroEstado = parametroEstadoTelefono;
                }
                else
                {
                    estadoInactivo.ParametroId = parametroEstadoTelefono.Id;
                }

                NombreEstado nombreEstadoInactivo = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.ESTADO_TELEFONO_DIRECCION_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_TELEFONO_DIRECCION_INACTIVO);

                if (nombreEstadoInactivo == null)
                {
                    NombreEstado nombreEstadoInac = new NombreEstado { Nombre = BowConsts.ESTADO_TELEFONO_DIRECCION_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_TELEFONO_DIRECCION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInac);
                    estadoInactivo.EstadoNombreEstado = nombreEstadoInac;
                }
                else
                {
                    estadoInactivo.EstadoNombreId = nombreEstadoInactivo.Id;
                }

                context.Set<Estado>().Add(estadoInactivo);
            }

            context.SaveChanges();
        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de la Organización
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarOrganizacion(Bow.EntityFramework.BowDbContext context)
        {
            Organizacion nombreOrganizacion = context.Set<Organizacion>().FirstOrDefault(p => p.Nombre == BowConsts.ORGANIZACION_EMPRESA_NOMBRE_DEFECTO);
            if (nombreOrganizacion == null)
            {
                Organizacion nuevoNombreOrganizacion = new Organizacion { Nombre = BowConsts.ORGANIZACION_EMPRESA_NOMBRE_DEFECTO, Logo = "" };
                context.Set<Organizacion>().AddOrUpdate(p => p.Nombre, nuevoNombreOrganizacion);
            }

            // PARÁMETRO NATURALEZA EMPRESA CON TIPOS
            Parametro parametroNaturalezaEmpresa = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_NATURALEZA_EMPRESA);

            if (parametroNaturalezaEmpresa == null)
            {
                Parametro parametroNaturalezaEmp = new Parametro { Nombre = BowConsts.PARAMETRO_NATURALEZA_EMPRESA };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroNaturalezaEmp);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.NATURALEZA_EMPRESA_NATURAL, ParametroTipo = parametroNaturalezaEmp });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.NATURALEZA_EMPRESA_JURIDICA, ParametroTipo = parametroNaturalezaEmp });
            }
            else
            {
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.NATURALEZA_EMPRESA_NATURAL, ParametroId = parametroNaturalezaEmpresa.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.NATURALEZA_EMPRESA_JURIDICA, ParametroId = parametroNaturalezaEmpresa.Id });
            }

            // PARÁMETRO ESTADO EMPRESA ORGANIZACION
            bool existeParametro = false;
            Estado estadoEmpresaOrganizacionActivo = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_EMPRESA_ORGANIZACION);
            Estado estadoEmpresaOrganizacionInactivo = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_EMPRESA_ORGANIZACION);

            Parametro parametroEmpresaOrganizacion = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_EMPRESA_ORGANIZACION);

            if (parametroEmpresaOrganizacion == null)
            {
                parametroEmpresaOrganizacion = new Parametro { Nombre = BowConsts.PARAMETRO_EMPRESA_ORGANIZACION };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroEmpresaOrganizacion);
            }
            else
            {
                existeParametro = true;
            }

            //Estado Activa
            if (estadoEmpresaOrganizacionActivo == null)
            {
                Estado estadoActivo = new Estado { Motivo = BowConsts.ESTADO_EMPRESA_ORGANIZACION_ACTIVO };

                if (existeParametro == false)
                {
                    estadoActivo.ParametroEstado = parametroEmpresaOrganizacion;
                }
                else
                {
                    estadoActivo.ParametroId = parametroEmpresaOrganizacion.Id;
                }

                NombreEstado nombreEstadoActivo = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivo == null)
                {
                    nombreEstadoActivo = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoActivo);
                    estadoActivo.EstadoNombreEstado = nombreEstadoActivo;
                }
                else
                {
                    estadoActivo.EstadoNombreId = nombreEstadoActivo.Id;
                }

                context.Set<Estado>().Add(estadoActivo);
            }

            //Estado inactivo
            if (estadoEmpresaOrganizacionInactivo == null)
            {
                Estado estadoInactivo = new Estado { Motivo = BowConsts.ESTADO_EMPRESA_ORGANIZACION_INACTIVO };

                if (existeParametro == false)
                {
                    estadoInactivo.ParametroEstado = parametroEmpresaOrganizacion;
                }
                else
                {
                    estadoInactivo.ParametroId = parametroEmpresaOrganizacion.Id;
                }

                NombreEstado nombreEstadoInactiva = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO);

                if (nombreEstadoInactiva == null)
                {
                    nombreEstadoInactiva = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInactiva);
                    estadoInactivo.EstadoNombreEstado = nombreEstadoInactiva;
                }
                else
                {
                    estadoInactivo.EstadoNombreId = nombreEstadoInactiva.Id;
                }

                context.Set<Estado>().Add(estadoInactivo);
            }

            context.SaveChanges();
        }

        /// //////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de los Medios de Contacto
        /// </summary>
        /// <param name="context"></param>
        /// /////////////////////////////////////////////////////////////////////////////
        private void cargarMediosContacto(Bow.EntityFramework.BowDbContext context)
        {
            Parametro parametroMediosContacto = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_MEDIOS_DE_CONTACTO);

            if (parametroMediosContacto == null)
            {
                Parametro parametroMedios = new Parametro { Nombre = BowConsts.PARAMETRO_MEDIOS_DE_CONTACTO };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroMedios);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_MEDIO_CONTACTO_FACEBOOK, ParametroTipo = parametroMedios });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_MEDIO_CONTACTO_TWITTER, ParametroTipo = parametroMedios });
            }
            else
            {
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_MEDIO_CONTACTO_FACEBOOK, ParametroId = parametroMediosContacto.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_MEDIO_CONTACTO_TWITTER, ParametroId = parametroMediosContacto.Id });
            }

            context.SaveChanges();
        }

        /// //////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de las áreas de la empresa
        /// </summary>
        /// <param name="context"></param>
        /// /////////////////////////////////////////////////////////////////////////////
        private void cargarAreasEmpresa(Bow.EntityFramework.BowDbContext context)
        {
            Parametro parametroAreaEmpresa = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_AREA_EMPRESA);

            if (parametroAreaEmpresa == null)
            {
                parametroAreaEmpresa = new Parametro { Nombre = BowConsts.PARAMETRO_AREA_EMPRESA };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroAreaEmpresa);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_AREA_EMPRESA_GERENCIA, ParametroTipo = parametroAreaEmpresa });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_AREA_EMPRESA_TESORERIA, ParametroTipo = parametroAreaEmpresa });
            }
            else
            {
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_AREA_EMPRESA_GERENCIA, ParametroId = parametroAreaEmpresa.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_AREA_EMPRESA_TESORERIA, ParametroId = parametroAreaEmpresa.Id });
            }

            context.SaveChanges();
        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de las sucursales de la empresa
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarSucursalEmpresaOrganizacion(Bow.EntityFramework.BowDbContext context)
        {
            // PARÁMETRO SUCURSAL EMPRESA CON TIPOS
            Parametro parametroSucursalEmpresa = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_SUCURSAL_EMPRESA);

            if (parametroSucursalEmpresa == null)
            {
                parametroSucursalEmpresa = new Parametro { Nombre = BowConsts.PARAMETRO_SUCURSAL_EMPRESA };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroSucursalEmpresa);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_SUCURSAL_SEDE_PRINCIPAL, ParametroTipo = parametroSucursalEmpresa });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_SUCURSAL_PUNTO_RECAUDO, ParametroTipo = parametroSucursalEmpresa });
            }
            else
            {
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_SUCURSAL_SEDE_PRINCIPAL, ParametroId = parametroSucursalEmpresa.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_SUCURSAL_PUNTO_RECAUDO, ParametroId = parametroSucursalEmpresa.Id });
            }

            // PARÁMETRO ESTADO EMPRESA ORGANIZACION
            Estado estadoSucursalEmpresaActivo = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_SUCURSAL_EMPRESA);
            Estado estadoSucursalEmpresaInactivo = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_SUCURSAL_EMPRESA);

            //Estado Activa
            if (estadoSucursalEmpresaActivo == null)
            {
                Estado estadoActivo = new Estado { Motivo = BowConsts.MOTIVO_ESTADO_SUCURSAL_EMPRESA_ACTIVO };

                estadoActivo.ParametroId = parametroSucursalEmpresa.Id;

                NombreEstado nombreEstadoActivo = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivo == null)
                {
                    nombreEstadoActivo = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoActivo);
                    estadoActivo.EstadoNombreEstado = nombreEstadoActivo;
                }
                else
                {
                    estadoActivo.EstadoNombreId = nombreEstadoActivo.Id;
                }

                context.Set<Estado>().Add(estadoActivo);
            }

            //Estado inactivo
            if (estadoSucursalEmpresaActivo == null)
            {
                Estado estadoInactivo = new Estado { Motivo = BowConsts.MOTIVO_ESTADO_SUCURSAL_EMPRESA_INACTIVO };

                estadoInactivo.ParametroId = parametroSucursalEmpresa.Id;

                NombreEstado nombreEstadoInactiva = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO);

                if (nombreEstadoInactiva == null)
                {
                    nombreEstadoInactiva = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInactiva);
                    estadoInactivo.EstadoNombreEstado = nombreEstadoInactiva;
                }
                else
                {
                    estadoInactivo.EstadoNombreId = nombreEstadoInactiva.Id;
                }

                context.Set<Estado>().Add(estadoInactivo);
            }

            context.SaveChanges();
        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de los Estados del Empleado
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarEstadosEmpleado(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoActivoEmpleado = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_ESTADO_DE_EMPLEADO);
            Estado estadoInactivoEmpleado = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_ESTADO_DE_EMPLEADO);

            Parametro parametroEstadoEmpleado = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_ESTADO_DE_EMPLEADO);

            if (parametroEstadoEmpleado == null)
            {
                parametroEstadoEmpleado = new Parametro { Nombre = BowConsts.PARAMETRO_ESTADO_DE_EMPLEADO };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroEstadoEmpleado);
            }
            else
            {
                existeParametro = true;
            }

            //Estado Activo
            if (estadoActivoEmpleado == null)
            {
                Estado estadoActivoEmp = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_ACTIVO };

                if (existeParametro == false)
                {
                    estadoActivoEmp.ParametroEstado = parametroEstadoEmpleado;
                }
                else
                {
                    estadoActivoEmp.ParametroId = parametroEstadoEmpleado.Id;
                }

                NombreEstado nombreEstadoActivoEmpleado = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivoEmpleado == null)
                {
                    NombreEstado nombreEstadoActivoEmpl = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoActivoEmpl);
                    estadoActivoEmp.EstadoNombreEstado = nombreEstadoActivoEmpl;
                }
                else
                {
                    estadoActivoEmp.EstadoNombreId = nombreEstadoActivoEmpleado.Id;
                }

                context.Set<Estado>().Add(estadoActivoEmp);
            }

            //Estado Inactivo           
            if (estadoInactivoEmpleado == null)
            {
                Estado estadoInactivoEmp = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_INACTIVO };

                if (existeParametro == false)
                {
                    estadoInactivoEmp.ParametroEstado = parametroEstadoEmpleado;
                }
                else
                {
                    estadoInactivoEmp.ParametroId = parametroEstadoEmpleado.Id;
                }

                NombreEstado nombreEstadoInactivoEmpleado = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO);

                if (nombreEstadoInactivoEmpleado == null)
                {
                    NombreEstado nombreEstadoInactivoPref = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInactivoPref);
                    estadoInactivoEmp.EstadoNombreEstado = nombreEstadoInactivoPref;
                }
                else
                {
                    estadoInactivoEmp.EstadoNombreId = nombreEstadoInactivoEmpleado.Id;
                }

                context.Set<Estado>().Add(estadoInactivoEmp);
            }

            context.SaveChanges();
        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de los Estados del Plan Exequial
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarEstadosPlanExequial(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoActivoPlanExequial = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_PLAN_EXEQUIAL);
            Estado estadoInactivoPlanExequial = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_PLAN_EXEQUIAL);

            Parametro parametroEstadoPlanExequial = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_PLAN_EXEQUIAL);

            if (parametroEstadoPlanExequial == null)
            {
                parametroEstadoPlanExequial = new Parametro { Nombre = BowConsts.PARAMETRO_PLAN_EXEQUIAL };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroEstadoPlanExequial);
            }
            else
            {
                existeParametro = true;
            }

            //Estado Activo
            if (estadoActivoPlanExequial == null)
            {
                estadoActivoPlanExequial = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_ACTIVO };

                if (existeParametro == false)
                {
                    estadoActivoPlanExequial.ParametroEstado = parametroEstadoPlanExequial;
                }
                else
                {
                    estadoActivoPlanExequial.ParametroId = parametroEstadoPlanExequial.Id;
                }

                NombreEstado nombreEstadoActivoPlanExequial = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivoPlanExequial == null)
                {
                    nombreEstadoActivoPlanExequial = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoActivoPlanExequial);
                    estadoActivoPlanExequial.EstadoNombreEstado = nombreEstadoActivoPlanExequial;
                }
                else
                {
                    estadoActivoPlanExequial.EstadoNombreId = nombreEstadoActivoPlanExequial.Id;
                }

                context.Set<Estado>().Add(estadoActivoPlanExequial);
            }

            //Estado Inactivo           
            if (estadoInactivoPlanExequial == null)
            {
                estadoInactivoPlanExequial = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_INACTIVO };

                if (existeParametro == false)
                {
                    estadoInactivoPlanExequial.ParametroEstado = parametroEstadoPlanExequial;
                }
                else
                {
                    estadoInactivoPlanExequial.ParametroId = parametroEstadoPlanExequial.Id;
                }

                NombreEstado nombreEstadoInactivoPlanExequial = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO);

                if (nombreEstadoInactivoPlanExequial == null)
                {
                    nombreEstadoInactivoPlanExequial = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInactivoPlanExequial);
                    estadoInactivoPlanExequial.EstadoNombreEstado = nombreEstadoInactivoPlanExequial;
                }
                else
                {
                    estadoInactivoPlanExequial.EstadoNombreId = nombreEstadoInactivoPlanExequial.Id;
                }

                context.Set<Estado>().Add(estadoInactivoPlanExequial);
            }

            context.SaveChanges();
        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de Moneda
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarMoneda(Bow.EntityFramework.BowDbContext context)
        {
            Moneda monedaPeso = context.Set<Moneda>().FirstOrDefault(p => p.Nombre == BowConsts.MONEDA_NOMBRE_PESO && p.Simbolo == BowConsts.MONEDA_SIMBOLO_PESO);
            if (monedaPeso == null)
            {
                monedaPeso = new Moneda { Nombre = BowConsts.MONEDA_NOMBRE_PESO, Simbolo = BowConsts.MONEDA_SIMBOLO_PESO };
                context.Set<Moneda>().AddOrUpdate(p => p.Nombre, monedaPeso);
            }

            context.SaveChanges();
        }

        private void cargarTiposCategorias(Bow.EntityFramework.BowDbContext context)
        {
            Parametro parametroCategoriaBneficio = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_CATEGORIAS_BENEFICIOS);

            if (parametroCategoriaBneficio == null)
            {
                Parametro parametroTipoTel = new Parametro { Nombre = BowConsts.PARAMETRO_CATEGORIAS_BENEFICIOS };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroTipoTel);
            }

            context.SaveChanges();
        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de los Estados del Grupo Familiar
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarEstadosGrupoFamiliar(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoActivoGrupoFamiliar = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_GRUPO_FAMILIAR);
            Estado estadoInactivoGrupoFamiliar = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_GRUPO_FAMILIAR);

            Parametro parametroEstadoGrupoFamiliar = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_GRUPO_FAMILIAR);

            if (parametroEstadoGrupoFamiliar == null)
            {
                parametroEstadoGrupoFamiliar = new Parametro { Nombre = BowConsts.PARAMETRO_GRUPO_FAMILIAR };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroEstadoGrupoFamiliar);
            }
            else
            {
                existeParametro = true;
            }

            //Estado Activo
            if (estadoActivoGrupoFamiliar == null)
            {
                estadoActivoGrupoFamiliar = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_ACTIVO };

                if (existeParametro == false)
                {
                    estadoActivoGrupoFamiliar.ParametroEstado = parametroEstadoGrupoFamiliar;
                }
                else
                {
                    estadoActivoGrupoFamiliar.ParametroId = parametroEstadoGrupoFamiliar.Id;
                }

                NombreEstado nombreEstadoActivoGrupoFamiliar = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivoGrupoFamiliar == null)
                {
                    nombreEstadoActivoGrupoFamiliar = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoActivoGrupoFamiliar);
                    estadoActivoGrupoFamiliar.EstadoNombreEstado = nombreEstadoActivoGrupoFamiliar;
                }
                else
                {
                    estadoActivoGrupoFamiliar.EstadoNombreId = nombreEstadoActivoGrupoFamiliar.Id;
                }

                context.Set<Estado>().Add(estadoActivoGrupoFamiliar);
            }

            //Estado Inactivo           
            if (estadoInactivoGrupoFamiliar == null)
            {
                estadoInactivoGrupoFamiliar = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_INACTIVO };

                if (existeParametro == false)
                {
                    estadoInactivoGrupoFamiliar.ParametroEstado = parametroEstadoGrupoFamiliar;
                }
                else
                {
                    estadoInactivoGrupoFamiliar.ParametroId = parametroEstadoGrupoFamiliar.Id;
                }

                NombreEstado nombreEstadoInactivoGrupoFamiliar = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO);

                if (nombreEstadoInactivoGrupoFamiliar == null)
                {
                    nombreEstadoInactivoGrupoFamiliar = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInactivoGrupoFamiliar);
                    estadoInactivoGrupoFamiliar.EstadoNombreEstado = nombreEstadoInactivoGrupoFamiliar;
                }
                else
                {
                    estadoInactivoGrupoFamiliar.EstadoNombreId = nombreEstadoInactivoGrupoFamiliar.Id;
                }

                context.Set<Estado>().Add(estadoInactivoGrupoFamiliar);
            }

            context.SaveChanges();
        }

        /// /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga los tipos de parentesco básicos en el sistema
        /// </summary>
        /// <param name="context"></param>
        /// //////////////////////////////////////////////////////////////////////////////
        //private void cargarTiposParentesco(Bow.EntityFramework.BowDbContext context)
        //{
        //    var tiposParentesco = new List<Parentesco>
        //    {
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_ESPOSO, Orden = 1 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_ESPOSA, Orden = 2 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_COMPANERO, Orden = 3 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_COMPANERA, Orden = 4 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_PADRE, Orden = 5 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_MADRE, Orden = 6 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_HIJO, Orden = 7 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_HIJA, Orden = 8 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_HERMANO, Orden = 9 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_HERMANA, Orden = 10 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_HIJASTRO, Orden = 11 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_HIJASTRA, Orden = 12 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_ENTENADO, Orden = 13 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_ENTENADA, Orden = 14 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_PADRASTRO, Orden = 15 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_MADRASTRA, Orden = 16 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_HERMANASTRO, Orden = 17 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_HERMANASTRA, Orden = 18 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_SUEGRO, Orden = 19 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_SUEGRA, Orden = 20 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_NIETO, Orden = 21 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_NIETA, Orden = 22 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_SOBRINO, Orden = 23 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_SOBRINA, Orden = 24 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_PRIMO, Orden = 25 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_PRIMA, Orden = 26 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_ABUELO, Orden = 27 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_ABUELA, Orden = 28 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_TIO, Orden = 29 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_TIA, Orden = 30 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_BISABUELO, Orden = 31 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_BISABUELA, Orden = 32 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_BISNIETO, Orden = 33 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_BISNIETA, Orden = 34 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_TIO_ABUELO, Orden = 35 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_TIA_ABUELA, Orden = 36 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_PRIMO_2DO, Orden = 37 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_PRIMA_2DA, Orden = 38 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_YERNO, Orden = 39 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_NUERA, Orden = 40 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_CUNADO, Orden = 41 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_CUNADA, Orden = 42 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_CONSUEGRO, Orden = 43 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_CONSUEGRA, Orden = 44 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_CONCUNADO, Orden = 45 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_CONCUNADA, Orden = 46 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_COMADRE, Orden = 47 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_COMPADRE, Orden = 48 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_AHIJADO, Orden = 49 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_AHIJADA, Orden = 50 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_PADRINO, Orden = 51 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_MADRINA, Orden = 52 },
        //        new Parentesco { Nombre = BowConsts.PARENTESCO_OTROS, Orden = 53 }
        //    };
        //    tiposParentesco.ForEach(to => context.Set<Parentesco>().AddOrUpdate(t => t.Nombre, to));

        //    context.SaveChanges();
        //}

        /// /////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de los tipos de beneficio de un plan exequial
        /// </summary>
        /// <param name="context"></param>
        /// /////////////////////////////////////////////////////////////////
        private void cargarTiposBeneficioPlanExequial(Bow.EntityFramework.BowDbContext context)
        {
            Parametro parametroTipoBeneficio = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL);

            if (parametroTipoBeneficio == null)
            {
                parametroTipoBeneficio = new Parametro { Nombre = BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroTipoBeneficio);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_BENEFICIO_PLAN_EXEQUIAL_PROPIO, ParametroTipo = parametroTipoBeneficio });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_BENEFICIO_PLAN_EXEQUIAL_ADICIONAL, ParametroTipo = parametroTipoBeneficio });
            }
            else
            {
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_BENEFICIO_PLAN_EXEQUIAL_PROPIO, ParametroId = parametroTipoBeneficio.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_BENEFICIO_PLAN_EXEQUIAL_ADICIONAL, ParametroId = parametroTipoBeneficio.Id });
            }

            context.SaveChanges();
        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de los Estados del beneficio de un plan exequial
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarEstadosBeneficioPlanExequial(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoActivoBeneficioPlanExequial = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL);
            Estado estadoInactivoBeneficioPlanExequial = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL);

            Parametro parametroEstadoBeneficioPlanExequial = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL);

            if (parametroEstadoBeneficioPlanExequial == null)
            {
                parametroEstadoBeneficioPlanExequial = new Parametro { Nombre = BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroEstadoBeneficioPlanExequial);
            }
            else
            {
                existeParametro = true;
            }

            //Estado Activo
            if (estadoActivoBeneficioPlanExequial == null)
            {
                estadoActivoBeneficioPlanExequial = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_ACTIVO };

                if (existeParametro == false)
                {
                    estadoActivoBeneficioPlanExequial.ParametroEstado = parametroEstadoBeneficioPlanExequial;
                }
                else
                {
                    estadoActivoBeneficioPlanExequial.ParametroId = parametroEstadoBeneficioPlanExequial.Id;
                }

                NombreEstado nombreEstadoActivoBeneficioPlanExequial = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivoBeneficioPlanExequial == null)
                {
                    nombreEstadoActivoBeneficioPlanExequial = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoActivoBeneficioPlanExequial);
                    estadoActivoBeneficioPlanExequial.EstadoNombreEstado = nombreEstadoActivoBeneficioPlanExequial;
                }
                else
                {
                    estadoActivoBeneficioPlanExequial.EstadoNombreId = nombreEstadoActivoBeneficioPlanExequial.Id;
                }

                context.Set<Estado>().Add(estadoActivoBeneficioPlanExequial);
            }

            //Estado Inactivo           
            if (estadoInactivoBeneficioPlanExequial == null)
            {
                estadoInactivoBeneficioPlanExequial = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_INACTIVO };

                if (existeParametro == false)
                {
                    estadoInactivoBeneficioPlanExequial.ParametroEstado = parametroEstadoBeneficioPlanExequial;
                }
                else
                {
                    estadoInactivoBeneficioPlanExequial.ParametroId = parametroEstadoBeneficioPlanExequial.Id;
                }

                NombreEstado nombreEstadoInactivoBeneficioPlanExequial = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO);

                if (nombreEstadoInactivoBeneficioPlanExequial == null)
                {
                    nombreEstadoInactivoBeneficioPlanExequial = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInactivoBeneficioPlanExequial);
                    estadoInactivoBeneficioPlanExequial.EstadoNombreEstado = nombreEstadoInactivoBeneficioPlanExequial;
                }
                else
                {
                    estadoInactivoBeneficioPlanExequial.EstadoNombreId = nombreEstadoInactivoBeneficioPlanExequial.Id;
                }

                context.Set<Estado>().Add(estadoInactivoBeneficioPlanExequial);
            }

            context.SaveChanges();
        }



        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Carga de los Estados y tipos de rol empleado en zona
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarEstadosTiposRolEmpleadoZona(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoRolEmpleadoActivo = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_ROL_EMPLEADO_ZONA);
            Estado estadoRolEmpleadoInactivo = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_ROL_EMPLEADO_ZONA);

            Parametro parametroEstadoRolEmpleado = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_ROL_EMPLEADO_ZONA);

            if (parametroEstadoRolEmpleado == null)
            {
                parametroEstadoRolEmpleado = new Parametro { Nombre = BowConsts.PARAMETRO_ROL_EMPLEADO_ZONA };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroEstadoRolEmpleado);
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_ROL_EMPLEADO_ZONA_RECAUDADOR, ParametroTipo = parametroEstadoRolEmpleado });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_ROL_EMPLEADO_ZONA_MULTIFUNCIONAL, ParametroTipo = parametroEstadoRolEmpleado });
            }
            else
            {
                existeParametro = true;
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_ROL_EMPLEADO_ZONA_RECAUDADOR, ParametroId = parametroEstadoRolEmpleado.Id });
                context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_ROL_EMPLEADO_ZONA_MULTIFUNCIONAL, ParametroId = parametroEstadoRolEmpleado.Id });
            }

            //Estado Activo
            if (estadoRolEmpleadoActivo == null)
            {
                Estado estadoActivo = new Estado { Motivo = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO };

                if (existeParametro == false)
                {
                    estadoActivo.ParametroEstado = parametroEstadoRolEmpleado;
                }
                else
                {
                    estadoActivo.ParametroId = parametroEstadoRolEmpleado.Id;
                }

                NombreEstado nombreEstadoActivo = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivo == null)
                {
                    NombreEstado nombreEstadoAct = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoAct);
                    estadoActivo.EstadoNombreEstado = nombreEstadoAct;
                }
                else
                {
                    estadoActivo.EstadoNombreId = nombreEstadoActivo.Id;
                }

                context.Set<Estado>().Add(estadoActivo);
            }

            //Estado inactivo
            if (estadoRolEmpleadoInactivo == null)
            {
                Estado estadoInactivo = new Estado { Motivo = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO };

                if (existeParametro == false)
                {
                    estadoInactivo.ParametroEstado = parametroEstadoRolEmpleado;
                }
                else
                {
                    estadoInactivo.ParametroId = parametroEstadoRolEmpleado.Id;
                }

                NombreEstado nombreEstadoInactivo = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO);

                if (nombreEstadoInactivo == null)
                {
                    NombreEstado nombreEstadoInac = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInac);
                    estadoInactivo.EstadoNombreEstado = nombreEstadoInac;
                }
                else
                {
                    estadoInactivo.EstadoNombreId = nombreEstadoInactivo.Id;
                }

                context.Set<Estado>().Add(estadoInactivo);
            }

            context.SaveChanges();
        }


        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de los recaudos masivos
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarRecaudoMasivo(Bow.EntityFramework.BowDbContext context)
        {
            RecaudoMasivo recaudo = context.Set<RecaudoMasivo>().FirstOrDefault(p => p.Nombre == BowConsts.RECAUDO_MASIVO_NOMBRE_CHEC && p.Clave == BowConsts.RECAUDO_MASIVO_CLAVE_CHEC);
            Organizacion organizacionAlmacenada = context.Set<Organizacion>().FirstOrDefault(p => p.Nombre == BowConsts.ORGANIZACION_EMPRESA_NOMBRE_DEFECTO);

            if (recaudo == null)
            {
                recaudo = new RecaudoMasivo { Nombre = BowConsts.RECAUDO_MASIVO_NOMBRE_CHEC, Clave = BowConsts.RECAUDO_MASIVO_CLAVE_CHEC, OrganizacionId = organizacionAlmacenada.Id };
                context.Set<RecaudoMasivo>().AddOrUpdate(p => p.Nombre, recaudo);
            }

            context.SaveChanges();
        }


        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de las funerarias prospecto
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarFunerariasProspecto(Bow.EntityFramework.BowDbContext context)
        {
            var funerariasProspecto = new List<FunerariaProspecto>
            {
                new FunerariaProspecto { Nombre = BowConsts.FUNERARIA_PORSPECTO_OLIVOS },
                new FunerariaProspecto { Nombre = BowConsts.FUNERARIA_PORSPECTO_JARDINES },
            };

            funerariasProspecto.ForEach(to => context.Set<FunerariaProspecto>().AddOrUpdate(t => t.Nombre, to));

            context.SaveChanges();
        }



        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de los Estados del Cliente Prospecto
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarEstadosClienteProspecto(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoActivoClienteProspecto = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_ESTADO_CLIENTE_PROSPECTO);

            Parametro parametroEstadoClienteProspecto = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_ESTADO_CLIENTE_PROSPECTO);

            if (parametroEstadoClienteProspecto == null)
            {
                parametroEstadoClienteProspecto = new Parametro { Nombre = BowConsts.PARAMETRO_ESTADO_CLIENTE_PROSPECTO };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroEstadoClienteProspecto);
            }
            else
            {
                existeParametro = true;
            }

            //Estado Activo
            if (estadoActivoClienteProspecto == null)
            {
                //Estado motivo no afiliacion (Afiliado a otra funeraria)
                estadoActivoClienteProspecto = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_AFILIADO_OTRA_FUNERARIA };

                if (existeParametro == false)
                {
                    estadoActivoClienteProspecto.ParametroEstado = parametroEstadoClienteProspecto;
                }
                else
                {
                    estadoActivoClienteProspecto.ParametroId = parametroEstadoClienteProspecto.Id;
                }

                NombreEstado nombreEstadoActivoClienteProspecto = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivoClienteProspecto == null)
                {
                    nombreEstadoActivoClienteProspecto = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoActivoClienteProspecto);
                    estadoActivoClienteProspecto.EstadoNombreEstado = nombreEstadoActivoClienteProspecto;
                }
                else
                {
                    estadoActivoClienteProspecto.EstadoNombreId = nombreEstadoActivoClienteProspecto.Id;
                }

                context.Set<Estado>().Add(estadoActivoClienteProspecto);

                //Estado motivo no afiliacion (No esta interesado)
                estadoActivoClienteProspecto = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_NO_ESTA_INTERESADO };

                if (existeParametro == false)
                {
                    estadoActivoClienteProspecto.ParametroEstado = parametroEstadoClienteProspecto;
                }
                else
                {
                    estadoActivoClienteProspecto.ParametroId = parametroEstadoClienteProspecto.Id;
                }

                estadoActivoClienteProspecto.EstadoNombreId = nombreEstadoActivoClienteProspecto.Id;
                context.Set<Estado>().Add(estadoActivoClienteProspecto);

                //Estado motivo no afiliacion (No tiene dinero)
                estadoActivoClienteProspecto = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_NO_TIENE_DINERO };

                if (existeParametro == false)
                {
                    estadoActivoClienteProspecto.ParametroEstado = parametroEstadoClienteProspecto;
                }
                else
                {
                    estadoActivoClienteProspecto.ParametroId = parametroEstadoClienteProspecto.Id;
                }

                estadoActivoClienteProspecto.EstadoNombreId = nombreEstadoActivoClienteProspecto.Id;
                context.Set<Estado>().Add(estadoActivoClienteProspecto);

                //Estado motivo no afiliacion (No hay contacto)
                estadoActivoClienteProspecto = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_NO_HAY_CONTACTO };

                if (existeParametro == false)
                {
                    estadoActivoClienteProspecto.ParametroEstado = parametroEstadoClienteProspecto;
                }
                else
                {
                    estadoActivoClienteProspecto.ParametroId = parametroEstadoClienteProspecto.Id;
                }

                estadoActivoClienteProspecto.EstadoNombreId = nombreEstadoActivoClienteProspecto.Id;
                context.Set<Estado>().Add(estadoActivoClienteProspecto);
            }

            context.SaveChanges();

        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de los Estados del Grupo Informal
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarEstadosGrupoInformal(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoActivoGrupoInformal = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_GRUPO_INFORMAL);
            Estado estadoInactivoGrupoInformal = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_GRUPO_INFORMAL);

            Parametro parametroEstadoGrupoInformal = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_GRUPO_INFORMAL);

            if (parametroEstadoGrupoInformal == null)
            {
                parametroEstadoGrupoInformal = new Parametro { Nombre = BowConsts.PARAMETRO_GRUPO_INFORMAL };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroEstadoGrupoInformal);
            }
            else
            {
                existeParametro = true;
            }

            //Estado Activo
            if (estadoActivoGrupoInformal == null)
            {
                estadoActivoGrupoInformal = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_ACTIVO };

                if (existeParametro == false)
                {
                    estadoActivoGrupoInformal.ParametroEstado = parametroEstadoGrupoInformal;
                }
                else
                {
                    estadoActivoGrupoInformal.ParametroId = parametroEstadoGrupoInformal.Id;
                }

                NombreEstado nombreEstadoActivoGrupoInformal = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivoGrupoInformal == null)
                {
                    nombreEstadoActivoGrupoInformal = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoActivoGrupoInformal);
                    estadoActivoGrupoInformal.EstadoNombreEstado = nombreEstadoActivoGrupoInformal;
                }
                else
                {
                    estadoActivoGrupoInformal.EstadoNombreId = nombreEstadoActivoGrupoInformal.Id;
                }

                context.Set<Estado>().Add(estadoActivoGrupoInformal);
            }

            //Estado Inactivo           
            if (estadoInactivoGrupoInformal == null)
            {
                estadoInactivoGrupoInformal = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_INACTIVO };

                if (existeParametro == false)
                {
                    estadoInactivoGrupoInformal.ParametroEstado = parametroEstadoGrupoInformal;
                }
                else
                {
                    estadoInactivoGrupoInformal.ParametroId = parametroEstadoGrupoInformal.Id;
                }

                NombreEstado nombreEstadoInactivoGrupoInformal = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO);

                if (nombreEstadoInactivoGrupoInformal == null)
                {
                    nombreEstadoInactivoGrupoInformal = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInactivoGrupoInformal);
                    estadoInactivoGrupoInformal.EstadoNombreEstado = nombreEstadoInactivoGrupoInformal;
                }
                else
                {
                    estadoInactivoGrupoInformal.EstadoNombreId = nombreEstadoInactivoGrupoInformal.Id;
                }

                context.Set<Estado>().Add(estadoInactivoGrupoInformal);
            }

            context.SaveChanges();
        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Carga de los Estados del Empleado del Grupo Informal
        /// </summary>
        /// <param name="context"></param>
        /// ////////////////////////////////////////////////////////////////////////////
        private void cargarEstadosGrupoInformalEmpleado(Bow.EntityFramework.BowDbContext context)
        {
            bool existeParametro = false;
            Estado estadoActivoGrupoInformalEmpleado = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_GRUPO_INFORMAL_EMPLEADO);
            Estado estadoInactivoGrupoInformalEmpleado = context.Set<Estado>().FirstOrDefault(p => p.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO && p.ParametroEstado.Nombre == BowConsts.PARAMETRO_GRUPO_INFORMAL_EMPLEADO);

            Parametro parametroEstadoGrupoInformalEmpleado = context.Set<Parametro>().FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_GRUPO_INFORMAL_EMPLEADO);

            if (parametroEstadoGrupoInformalEmpleado == null)
            {
                parametroEstadoGrupoInformalEmpleado = new Parametro { Nombre = BowConsts.PARAMETRO_GRUPO_INFORMAL_EMPLEADO };
                context.Set<Parametro>().AddOrUpdate(p => p.Nombre, parametroEstadoGrupoInformalEmpleado);
            }
            else
            {
                existeParametro = true;
            }

            //Estado Activo
            if (estadoActivoGrupoInformalEmpleado == null)
            {
                estadoActivoGrupoInformalEmpleado = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_ACTIVO };

                if (existeParametro == false)
                {
                    estadoActivoGrupoInformalEmpleado.ParametroEstado = parametroEstadoGrupoInformalEmpleado;
                }
                else
                {
                    estadoActivoGrupoInformalEmpleado.ParametroId = parametroEstadoGrupoInformalEmpleado.Id;
                }

                NombreEstado nombreEstadoActivoGrupoInformalEmpleado = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO);

                if (nombreEstadoActivoGrupoInformalEmpleado == null)
                {
                    nombreEstadoActivoGrupoInformalEmpleado = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_ACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoActivoGrupoInformalEmpleado);
                    estadoActivoGrupoInformalEmpleado.EstadoNombreEstado = nombreEstadoActivoGrupoInformalEmpleado;
                }
                else
                {
                    estadoActivoGrupoInformalEmpleado.EstadoNombreId = nombreEstadoActivoGrupoInformalEmpleado.Id;
                }

                context.Set<Estado>().Add(estadoActivoGrupoInformalEmpleado);
            }

            //Estado Inactivo           
            if (estadoInactivoGrupoInformalEmpleado == null)
            {
                estadoInactivoGrupoInformalEmpleado = new Estado { Motivo = BowConsts.ESTADO_MOTIVO_INACTIVO };

                if (existeParametro == false)
                {
                    estadoInactivoGrupoInformalEmpleado.ParametroEstado = parametroEstadoGrupoInformalEmpleado;
                }
                else
                {
                    estadoInactivoGrupoInformalEmpleado.ParametroId = parametroEstadoGrupoInformalEmpleado.Id;
                }

                NombreEstado nombreEstadoInactivoGrupoInformal = context.Set<NombreEstado>().FirstOrDefault(p => p.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && p.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO);

                if (nombreEstadoInactivoGrupoInformal == null)
                {
                    nombreEstadoInactivoGrupoInformal = new NombreEstado { Nombre = BowConsts.NOMBREESTADO_NOMBRE_INACTIVO, Abreviacion = BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO };
                    context.Set<NombreEstado>().AddOrUpdate(p => p.Nombre, nombreEstadoInactivoGrupoInformal);
                    estadoInactivoGrupoInformalEmpleado.EstadoNombreEstado = nombreEstadoInactivoGrupoInformal;
                }
                else
                {
                    estadoInactivoGrupoInformalEmpleado.EstadoNombreId = nombreEstadoInactivoGrupoInformal.Id;
                }

                context.Set<Estado>().Add(estadoInactivoGrupoInformalEmpleado);
            }

            context.SaveChanges();
        }


    }
}
