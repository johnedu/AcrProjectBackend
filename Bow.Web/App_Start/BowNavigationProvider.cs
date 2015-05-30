using Abp.Application.Navigation;
using Abp.Localization;

namespace Bow.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class BowNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "menu_afiliacion",
                        new LocalizableString("menu_afiliacion", BowConsts.LocalizationSourceName),
                        icon: "icon-paper"
                        )
                        //.AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_afiliacion_administracion",
                        //        new LocalizableString("menu_afiliacion_administracion", BowConsts.LocalizationSourceName),
                        //        url: "#/afiliacion/administracion"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_afiliacion_cambioFechas",
                        //        new LocalizableString("menu_afiliacion_cambioFechas", BowConsts.LocalizationSourceName),
                        //        url: "#/afiliacion/cambio-fechas"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_afiliacion_contratacionTelefonica",
                        //        new LocalizableString("menu_afiliacion_contratacionTelefonica", BowConsts.LocalizationSourceName),
                        //        url: "#/afiliacion/contratacion-telefonica"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_afiliacion_sincronizacion",
                        //        new LocalizableString("menu_afiliacion_sincronizacion", BowConsts.LocalizationSourceName),
                        //        url: "#/afiliacion/sincronizacion"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_afiliacion_empresaAfiliada",
                        //        new LocalizableString("menu_afiliacion_empresaAfiliada", BowConsts.LocalizationSourceName),
                        //        url: "#/afiliacion/empresa-afiliada"
                        //    )
                        //)
                        .AddItem(
                            new MenuItemDefinition(
                                "menu_afiliacion_gestionarPeriodosVenta",
                                new LocalizableString("menu_afiliacion_gestionarPeriodosVenta", BowConsts.LocalizationSourceName),
                                url: "#/afiliaciones/periodosVenta"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "menu_afiliacion_grupoInformal",
                                new LocalizableString("menu_afiliacion_grupoInformal", BowConsts.LocalizationSourceName),
                                url: "#/afiliaciones/grupoInformal"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "menu_afiliacion_gestionarParentescos",
                                new LocalizableString("menu_afiliacion_gestionarParentescos", BowConsts.LocalizationSourceName),
                                url: "#/afiliaciones/parentesco"
                            )
                        )
                        //.AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_afiliacion_gestionAfiliacionVenta",
                        //        new LocalizableString("menu_afiliacion_gestionAfiliacionVenta", BowConsts.LocalizationSourceName),
                        //        url: "#/afiliaciones/infoAfiliacionVenta"
                        //    )
                        //)
                ).AddItem(
                    new MenuItemDefinition(
                        "menu_comercial",
                        new LocalizableString("menu_comercial", BowConsts.LocalizationSourceName),
                        icon: "icon-briefcase"
                        )
                        //.AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_comercial_desprendibleVendedor",
                        //        new LocalizableString("menu_comercial_desprendibleVendedor", BowConsts.LocalizationSourceName),
                        //        url: "#/comercial/desprendible-vendedor"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_comercial_pagoComision",
                        //        new LocalizableString("menu_comercial_pagoComision", BowConsts.LocalizationSourceName),
                        //        url: "#/comercial/pago-comision"
                        //    )
                        //)
                        .AddItem(
                            new MenuItemDefinition(
                                "menu_afiliaciones_planExequial",
                                new LocalizableString("menu_comercial_planExequial", BowConsts.LocalizationSourceName),
                                url: "#/afiliaciones/plan-exequial"
                            )

                        ).AddItem(
                            new MenuItemDefinition(
                                "menu_comercial_beneficio",
                                new LocalizableString("menu_comercial_beneficio", BowConsts.LocalizationSourceName),
                                url: "#/afiliaciones/beneficio"
                            )
                         )
                        // .AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_comercial_administracionSolicitudes",
                        //        new LocalizableString("menu_comercial_administracionSolicitudes", BowConsts.LocalizationSourceName),
                        //        url: "#/comercial/administracion-solicitudes"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_comercial_cambioCedula",
                        //        new LocalizableString("menu_comercial_cambioCedula", BowConsts.LocalizationSourceName),
                        //        url: "#/comercial/cambio-cedula"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_comercial_contactoComercial",
                        //        new LocalizableString("menu_comercial_contactoComercial", BowConsts.LocalizationSourceName),
                        //        url: "#/comercial/contacto-comercial"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_comercial_gestionContacto",
                        //        new LocalizableString("menu_comercial_gestionContacto", BowConsts.LocalizationSourceName),
                        //        url: "#/comercial/gestion-contacto"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_comercial_reportes",
                        //        new LocalizableString("menu_comercial_reportes", BowConsts.LocalizationSourceName),
                        //        url: "#/comercial/reportes"
                        //    )
                        //)
                ).AddItem(
                    new MenuItemDefinition(
                        "menu_cartera",
                        new LocalizableString("menu_cartera", BowConsts.LocalizationSourceName),
                        icon: "fa fa-money"
                        )
                        //.AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_cartera_pagos",
                        //        new LocalizableString("menu_cartera_pagos", BowConsts.LocalizationSourceName),
                        //        url: "#/cartera/pagos"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_cartera_facturas",
                        //        new LocalizableString("menu_cartera_facturas", BowConsts.LocalizationSourceName),
                        //        url: "#/cartera/facturas"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_cartera_plazos",
                        //        new LocalizableString("menu_cartera_plazos", BowConsts.LocalizationSourceName),
                        //        url: "#/cartera/plazos"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_cartera_condonarCuota",
                        //        new LocalizableString("menu_cartera_condonarCuota", BowConsts.LocalizationSourceName),
                        //        url: "#/cartera/condonar-cuota"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_cartera_cambioDiaCobro",
                        //        new LocalizableString("menu_cartera_cambioDiaCobro", BowConsts.LocalizationSourceName),
                        //        url: "#/cartera/cambio-dia-cobro"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_cartera_administracionTalonario",
                        //        new LocalizableString("menu_cartera_administracionTalonario", BowConsts.LocalizationSourceName),
                        //        url: "#/cartera/administracion-talonario"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_cartera_administracionRestricciones",
                        //        new LocalizableString("menu_cartera_administracionRestricciones", BowConsts.LocalizationSourceName),
                        //        url: "#/cartera/administracion-restricciones"
                        //    )
                        //)
                ).AddItem(
                    new MenuItemDefinition(
                        "menu_servicios",
                        new LocalizableString("menu_servicios", BowConsts.LocalizationSourceName),
                        icon: "fa fa-ambulance"
                        )
                        //.AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_servicios_ordenServicio",
                        //        new LocalizableString("menu_servicios_ordenServicio", BowConsts.LocalizationSourceName),
                        //        url: "#/servicios/orden-servicio"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_servicios_administracionConvenio",
                        //        new LocalizableString("menu_servicios_administracionConvenio", BowConsts.LocalizationSourceName),
                        //        url: "#/servicios/administracion-convenio"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_servicios_monitorOrden",
                        //        new LocalizableString("menu_servicios_monitorOrden", BowConsts.LocalizationSourceName),
                        //        url: "#/servicios/monitor-orden"
                        //    )
                        //)
                ).AddItem(
                    new MenuItemDefinition(
                        "menu_administracion",
                        new LocalizableString("menu_administracion", BowConsts.LocalizationSourceName),
                        icon: "fa fa-cogs"
                        ).AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_empresaAdministrativa",
                                new LocalizableString("menu_administracion_empresaAdministrativa", BowConsts.LocalizationSourceName),
                                url: "#/administracion/empresa-administrativa"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_personal",
                                new LocalizableString("menu_administracion_personal", BowConsts.LocalizationSourceName),
                                url: "#/administracion/empleados/empleado"
                            )
                        )
                        //.AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_administracion_envioCorreo",
                        //        new LocalizableString("menu_administracion_envioCorreo", BowConsts.LocalizationSourceName),
                        //        url: "#/administracion/envio-correo"
                        //    )
                        //).AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_administracion_ingresoRestriccion",
                        //        new LocalizableString("menu_administracion_ingresoRestriccion", BowConsts.LocalizationSourceName),
                        //        url: "#/administracion/ingreso-restriccion"
                        //    )
                        //)
                        .AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_administracionParametros",
                                new LocalizableString("menu_administracion_administracionParametros", BowConsts.LocalizationSourceName),
                                url: "#/administracion/parametro"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_tiposDocumento",
                                new LocalizableString("menu_administracion_tiposDocumento", BowConsts.LocalizationSourceName),
                                url: "#/administracion/tipos-documento"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_zonificacion",
                                new LocalizableString("menu_administracion_zonificacion", BowConsts.LocalizationSourceName),
                                url: "#/administracion/zonificacion"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_info_tributaria",
                                new LocalizableString("menu_administracion_infoTributaria", BowConsts.LocalizationSourceName),
                                url: "#/administracion/empresas/infoTributaria"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_empresas",
                                new LocalizableString("menu_administracion_organizacion", BowConsts.LocalizationSourceName),
                                url: "#/administracion/empresas/empresa"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_preferencias",
                                new LocalizableString("menu_administracion_preferenciasPersonales", BowConsts.LocalizationSourceName),
                                url: "#/administracion/personas/preferencia"

                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_actividadesEconomicas",
                                new LocalizableString("menu_administracion_actividadesEconomicas", BowConsts.LocalizationSourceName),
                                url: "#/administracion/empresas/actividadEconomica"

                            )
                        )

                );
        }
    }
}
