using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SsoftQuery.Reserva;
using System.Web.UI.WebControls;
using Ssoft.Utils;

namespace Ssoft.Pages.PaginaMiCuenta
{
    public class csListaReservas
    {
        public void mostrar_listado_reservas(Repeater rptReservas, string Empresa, string Usuario, string MesSolicitud,
            string AnioSolicitud, bool Vigentes)
        {
            DataTable dtReservas = new DataTable();
            csConsultasReserva Reservas = new csConsultasReserva();
            dtReservas = Reservas.consulta_reservas_usuario(Usuario, MesSolicitud, AnioSolicitud, Vigentes, Empresa);
            rptReservas.DataSource = dtReservas;
            rptReservas.DataBind();
        }

        public void mostrar_detalle_reserva(/*Repeater rptReservaHoteles,*/ Repeater rptReservaPlanes, Repeater rptReservaAereas,
            /*Repeater rptReservaAutos, Repeater rptReservaTrenes,*/ string strLocalizador, Label lblLocalizador)
        {
            try
            {
                csConsultasReserva Reservas = new csConsultasReserva();
                clsCache cCache = new clsCache();
                lblLocalizador.Text = strLocalizador;

                DataSet dt = new DataSet();
                string sIdioma = clsSesiones.getIdioma();
                dt = Reservas.consulta_detalle_general(strLocalizador);
                if (dt != null && dt.Tables.Count > 0)
                {
                    //cargar_reserva_tipo_servicio(dt, rptReservaHoteles, "Hoteles", cCache, strLocalizador);
                    cargar_reserva_tipo_servicio(dt, rptReservaPlanes, "Plan", cCache, strLocalizador);
                    cargar_reserva_tipo_servicio(dt, rptReservaAereas, "Aereo", cCache, strLocalizador);
                    //cargar_reserva_tipo_servicio(dt, rptReservaAutos, "Auto", cCache, strLocalizador);
                    //cargar_reserva_tipo_servicio(dt, rptReservaTrenes, "Trenes", cCache, strLocalizador);
                }
            }
            catch { }
        }


        public void cargar_reserva_tipo_servicio(DataSet dtRes, Repeater rptReserva, string Tipo, clsCache cCache, string strLocalizador)
        {
            try
            {
                csConsultasReserva Reservas = new csConsultasReserva();
                DataTable dt = dtRes.Tables[0].Copy();
                DataTable dt2 = new DataTable();
                //if (Tipo.Equals("Plan"))
                //    dt2 = dtRes.Tables[1].Copy();
                //else
                dt2 = dtRes.Tables[1].Copy();
                string sIdioma = clsSesiones.getIdioma();
                DataTable dtgv = new DataTable();
                string sReservaAir = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
                string sReservaTT = clsValidaciones.GetKeyOrAdd("WS_HOTEL_TT", "WS_TT");
                string sReservaHB = clsValidaciones.GetKeyOrAdd("WS_HOTEL_HB", "HOTBED");
                string sReservaHNal = clsValidaciones.GetKeyOrAdd("TipoPlanHotelWS", "HOT");
                string sReservaTame = clsValidaciones.GetKeyOrAdd("Aereo_WSTame", "AIR_TAME");
                string sReservaCar = clsValidaciones.GetKeyOrAdd("TIPOPLANCARWS", "CAR");
                string sReservaTren = clsValidaciones.GetKeyOrAdd("Trenes_Ws", "TREN");

                #region Eliminacion Registros Servicio
                switch (Tipo)
                {
                    case "Plan":
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["strcodigo"].ToString().Equals(sReservaAir) ||
                                dt.Rows[i]["strcodigo"].ToString().Equals(sReservaTame) ||
                                dt.Rows[i]["strcodigo"].ToString().Equals(sReservaTT) ||
                                dt.Rows[i]["strcodigo"].ToString().Equals(sReservaHB) ||
                                dt.Rows[i]["strcodigo"].ToString().Equals(sReservaHNal) ||
                                dt.Rows[i]["strcodigo"].ToString().Equals(sReservaCar))
                            {
                                dt.Rows.Remove(dt.Rows[i]);
                                dt2.Rows.Remove(dt2.Rows[i]);
                                i--;
                                dt.AcceptChanges();
                            }
                        }
                        break;
                    case "Aereo":
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["strcodigo"].ToString() != sReservaAir &&
                                dt.Rows[i]["strcodigo"].ToString() != sReservaTame)
                            {
                                dt.Rows.Remove(dt.Rows[i]);
                                dt2.Rows.Remove(dt2.Rows[i]);
                                i--;
                                dt.AcceptChanges();
                            }
                        }
                        break;
                    case "Auto":
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["strcodigo"].ToString() != sReservaCar)
                            {
                                dt.Rows.Remove(dt.Rows[i]);
                                dt2.Rows.Remove(dt2.Rows[i]);
                                i--;
                                dt.AcceptChanges();
                            }
                        }
                        break;
                    case "Trenes":
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["strRefere"].ToString() != sReservaTren)
                            {
                                dt.Rows.Remove(dt.Rows[i]);
                                dt2.Rows.Remove(dt2.Rows[i]);
                                i--;
                                dt.AcceptChanges();
                            }
                        }
                        break;
                    case "Hoteles":
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["strcodigo"].ToString() != sReservaHB &&
                                dt.Rows[i]["strcodigo"].ToString() != sReservaTT &&
                                dt.Rows[i]["strcodigo"].ToString() != sReservaHNal)
                            {
                                dt.Rows.Remove(dt.Rows[i]);
                                dt2.Rows.Remove(dt2.Rows[i]);
                                i--;
                                dt.AcceptChanges();
                            }
                        }
                        break;
                }

                #endregion

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    if (dt.Rows[x]["texto1"].ToString() == "")
                        dt.Rows[x]["label1"] = string.Empty;
                    if (dt.Rows[x]["texto2"].ToString() == "")
                        dt.Rows[x]["label2"] = string.Empty;
                    //if (dt.Rows[x]["texto102"].ToString() == "")
                    //    dt.Rows[x]["label102"] = string.Empty;
                    //if (dt.Rows[x]["texto103"].ToString() == "")
                    //    dt.Rows[x]["label103"] = string.Empty;
                    //if (dt.Rows[x]["texto104"].ToString() == "")
                    //    dt.Rows[x]["label104"] = string.Empty;
                    //if (dt.Rows[x]["texto105"].ToString() == "")
                    //    dt.Rows[x]["label105"] = string.Empty;
                }
                dt = validar_fecha_pagos(dt, "dtmFechaPagoProv", "dtmFechaPagoProv2");
                dt = validar_fecha_pagos(dt, "dtmFechaPagoComision", "dtmFechaPagoCom");
                rptReserva.DataSource = dt.DefaultView;
                rptReserva.DataBind();
                //CargarCombos(rptReserva);
                //SeleccionarCombosRepetidores(rptReserva, dt);
                //CargarMultiples();
                DataTable tblRegValor = dt2.Clone();
                for (int i = 0; i < rptReserva.Items.Count; i++)
                {
                    tblRegValor.Rows.Clear();
                    tblRegValor.Rows.Add(dt2.Rows[i].ItemArray);
                    ((Repeater)(rptReserva.Items[i].FindControl("rptValor"))).DataSource = tblRegValor.DefaultView;
                    ((Repeater)(rptReserva.Items[i].FindControl("rptValor"))).DataBind();

                    dt = Reservas.consulta_detalle_tipo_servicio(strLocalizador, ((Label)(rptReserva.Items[i].FindControl("lblReserva"))).Text, ((Label)(rptReserva.Items[i].FindControl("lblRefereTipoPlan"))).Text);
                    ((Repeater)(rptReserva.Items[i].FindControl("rptPlan"))).DataSource = dt.DefaultView;
                    ((Repeater)(rptReserva.Items[i].FindControl("rptPlan"))).DataBind();

                    if (Tipo.Equals("Plan"))
                    {
                        if (((Label)(rptReserva.Items[i].FindControl("lblRefereTipoPlan"))).Text != clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "Tras"))
                        {
                            for (int x = 0; x < ((Repeater)(rptReserva.Items[i].FindControl("rptPlan"))).Items.Count; x++)
                            {
                                string iSegmento = ((Label)((Repeater)(rptReserva.Items[i].FindControl("rptPlan"))).Items[x].FindControl("lblSegmento")).Text;
                                dtgv = Reservas.consulta_pasajeros_reserva(((Label)(rptReserva.Items[i].FindControl("lblReserva"))).Text, iSegmento);
                                if (dtgv == null || dtgv.Rows.Count == 0)
                                    dtgv = Reservas.consulta_pasajeros_reserva(((Label)(rptReserva.Items[i].FindControl("lblReserva"))).Text, "0");
                                ((Repeater)((Repeater)(rptReserva.Items[i].FindControl("rptPlan"))).Items[x].FindControl("rptPasajeros")).Visible = true;
                                ((Repeater)((Repeater)(rptReserva.Items[i].FindControl("rptPlan"))).Items[x].FindControl("rptPasajeros")).DataSource = dtgv;
                                ((Repeater)((Repeater)(rptReserva.Items[i].FindControl("rptPlan"))).Items[x].FindControl("rptPasajeros")).DataBind();
                            }
                            ((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).Visible = false;
                        }
                        else
                        {
                            for (int x = 0; x < ((Repeater)(rptReserva.Items[i].FindControl("rptPlan"))).Items.Count; x++)
                            {
                                if (((Label)(rptReserva.Items[x].FindControl("lblRefereTipoPlan"))).Text == clsValidaciones.GetKeyOrAdd("Aereo_WSTame", "AIR_TAME"))
                                    ((Button)(rptReserva.Items[x].FindControl("btnEmisionTAME"))).Visible = true;

                                ((Repeater)((Repeater)(rptReserva.Items[i].FindControl("rptPlan"))).Items[x].FindControl("rptPasajeros")).Visible = true;
                            }
                            ((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).Visible = true;
                            dtgv = Reservas.consulta_pasajeros_reserva(((Label)(rptReserva.Items[i].FindControl("lblReserva"))).Text, "1");
                            ((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).DataSource = dtgv;
                            ((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).DataBind();
                        }
                    }
                    else
                    {
                        dtgv = Reservas.consulta_pasajeros_reserva(((Label)(rptReserva.Items[i].FindControl("lblReserva"))).Text, "1");
                        ((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).DataSource = dtgv;
                        ((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).DataBind();
                    }

                    if (Tipo.Equals("Aereo"))
                    {
                        for (int x = 0; x < ((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).Items.Count; x++)
                        {
                            if (((TextBox)((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).Items[x].FindControl("txtNumTiquete")).Text != "" &&
                                ((TextBox)((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).Items[x].FindControl("txtNumTiquete")).Text != "0")
                                ((TextBox)((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).Items[x].FindControl("txtNumTiquete")).Enabled = false;
                        }
                        //ValidarEmision(rptReserva);
                    }
                    if (((Label)(rptReserva.Items[i].Controls[3])).Text.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTarjetas", "TJAS")))
                    {
                        ((Repeater)(rptReserva.Items[i].FindControl("rptPasajeros"))).Visible = false;
                    }
                    //DesactivarControlesHoteles(rptReserva, i, ((Label)(rptReserva.Items[i].FindControl("lblRefereTipoPlan"))).Text);
                }
            }
            catch { }
        }

        private DataTable validar_fecha_pagos(DataTable tblReserva, string strNombreColumna, string strNombreNueva)
        {
            tblReserva.Columns.Add(strNombreNueva);
            int i = 0;
            while (i < tblReserva.Rows.Count)
            {
                if (tblReserva.Rows[i][strNombreColumna].ToString() != "")
                {
                    if (Convert.ToDateTime(tblReserva.Rows[i][strNombreColumna].ToString()).ToString("yyyy/MM/dd").Equals("1900/01/01"))
                    {
                        tblReserva.Rows[i][strNombreNueva] = "";
                    }
                    else
                    {
                        tblReserva.Rows[i][strNombreNueva] = Convert.ToDateTime(tblReserva.Rows[i][strNombreColumna].ToString()).ToString(clsValidaciones.GetKey("FormatoFecha", "MM/dd/yyyy"));
                    }
                }
                i++;
            }
            return tblReserva;
        }
    }
}
