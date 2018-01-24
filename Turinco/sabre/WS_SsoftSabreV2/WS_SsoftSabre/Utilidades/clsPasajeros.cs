using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Ssoft.ValueObjects;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WS_SsoftSabre.Utilidades
{
    public class clsPasajeros
    {
        //public void getDatosPasajeros(Repeater rptPax)
        //{
        //    // se llenan los responsables
        //    List<clsPassenger> cPassenger = SesionesWs.getPassenger();
        //    int iNumPax = cPassenger.Count;

        //    List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo = new List<VO_DataTravelItineraryAddInfo>();

        //    iPasajeros = Sesiones.setNumeroPasajeros(iNumPax);

        //    for (int i = 0; i < iNumPax; i++)
        //    {
        //        cPassenger[i].RespNombre = ((TextBox)rptHabitaciones.Items[i].FindControl("txtNombre")).Text;
        //        cPassenger[i].RespApellido = ((TextBox)rptHabitaciones.Items[i].FindControl("txtApellido")).Text;
        //        cPassenger[i].RespTelefono = ((TextBox)rptHabitaciones.Items[i].FindControl("txtTelefono")).Text;
        //        cPassenger[i].Preferencias = ((TextBox)rptHabitaciones.Items[i].FindControl("txtNotas")).Text;

        //        lvo_DataTravelItineraryAddInfo.Add(getDatosPasajero(cPassenger[i]));
        //    }
        //    SesionesWs.setPassenger(cPassenger);

        //    Sesiones.SET_LVO_DataTravelItineraryAddInfo(lvo_DataTravelItineraryAddInfo);
        //    Sesiones.SET_LOAD_PASAJERO(true);
        //    Sesiones.SET_PANTALLA_RESPUESTA("ReservaHotel.aspx?ID=" + hdRoom.Value);
        //    Response.Redirect("ReservaHotel.aspx?ID=" + hdRoom.Value);
        //}
        //private VO_DataTravelItineraryAddInfo getPasajero(List<clsPassenger> cPassenger)
        //{
        //    string sTipo = "ADT";
        //    Utils oUtilidad = new Utils();
        //    VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo;
        //    string sTelefono;
        //    List<string> lTelefonos;

        //    lTelefonos = new List<string>();

        //    string sNombre = oUtilidad.CambiarCaracter(getTextBoxNombre(iPasajero).Text + " " + getTipoPasajeroSabre(iPasajero, sTipo));
        //    string sApellido = oUtilidad.CambiarCaracter(getTextBoxApellido(iPasajero).Text);

        //    vo_DataTravelItineraryAddInfo =
        //        new VO_DataTravelItineraryAddInfo
        //        (
        //            iPasajero,
        //            sNombre,
        //            sApellido,
        //            false,
        //            sTipo
        //        );

        //    sTelefono = getTextBoxTelefono(iPasajero).Text;
        //    if (sTelefono != null && sTelefono.Length > 0)
        //    {
        //        lTelefonos.Add(sTelefono);
        //        vo_DataTravelItineraryAddInfo.Telefono_ = lTelefonos;
        //    }
        //    else
        //    {
        //        if (cCache.Telefono != null && cCache.Telefono.Length > 0)
        //        {
        //            lTelefonos.Add(cCache.Telefono);
        //            vo_DataTravelItineraryAddInfo.Telefono_ = lTelefonos;
        //        }
        //    }

        //    //if (iPasajero.Equals(1))
        //    //{
        //    vo_DataTravelItineraryAddInfo.Email_ = cCache.User;
        //    //}


        //    return vo_DataTravelItineraryAddInfo;
        //}
    }
}
