using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using Ssoft.Rules.Reservas;
using System.Configuration;
using System.Data;
using Ssoft.ValueObjects;

namespace Ssoft.Rules.WebServices
{
    public class clsReservas
    {
              
        public clsResultados GetReservaAirSabre(DataSet dsReserva, string sTA, string sITA, DataSet dsTarifa)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            try
            {
                clsReservasAirSabre cReservaSabre = new clsReservasAirSabre();
                cResultados.dsResultados = new csCarrito().CrearEstructuraReservasExterna();
                DataTable dtReserva = cResultados.dsResultados.Tables[csReservas.TABLA_RESERVA];
                DataTable dtItinerary = cResultados.dsResultados.Tables[csReservas.TABLA_TRANSAC];
                DataTable dtPax = cResultados.dsResultados.Tables[csReservas.TABLA_PAX];
                DataTable dtTarifa = cResultados.dsResultados.Tables[csReservas.TABLA_TARIFA];
                DataTable dtTax = cResultados.dsResultados.Tables[csReservas.TABLA_TAXFARE];
                DataTable dtPol = cResultados.dsResultados.Tables[csReservas.TABLA_POL];

                cParametros = cReservaSabre.setTblReserva(dsReserva, dtReserva);
                cResultados.Error = cParametros;
                if (cParametros.Id.Equals(0))
                {
                    cParametros.Id = 0;
                    cParametros.Message = "Reserva Cancelada, Record: ";
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.Severity =clsSeveridad.Alta;
                    cParametros.ViewMessage.Add("La reserva ya fue cancelada");
                    cParametros.Sugerencia.Add("No tiene itinerario");
                    cParametros.DatoAdicArr.Add("La reserva ya fue cancelada, No tiene itinerario");
                    ExceptionHandled.Publicar(cParametros);
                }
                else
                {
                    cReservaSabre.setModificaDatasetSabre(dsReserva, dsTarifa);
                    cParametros = cReservaSabre.setTblTransac(dsReserva, dtItinerary);
                    cParametros = cReservaSabre.setTblPax(dsReserva, dsTarifa, dtPax);
                    cParametros = cReservaSabre.setTblTax(dsTarifa, dtTax, dtPol, sTA, sITA);
                    cParametros = cReservaSabre.setTblTarifa(dsReserva, dsTarifa, dtTarifa, dtPol, dtTax);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = "GetReservaAirSabre";
                cParametros.Severity = clsSeveridad.Alta;
                ExceptionHandled.Publicar(cParametros);
                cResultados.Error = cParametros;
            }
            return cResultados;
        }
        /// <summary>
        /// Proceso para insertar la reserva desde sabre - Subir Reserva - SRSABRE
        /// </summary>
        /// <param name="dsReserva">DataSet de Sabre con los datos generales de la reserva</param>
        /// <param name="sTA">Valor de la TA</param>
        /// <param name="sITA">Valor del Iva de la TA</param>
        /// <param name="dsTarifa">DataSet de la Tarifa</param>
        /// <param name="sProyecto">Codigo del proyecto a la cual se va a asociar la reserva, si es0, se inserta como nueva</param>
        /// <param name="sContacto">Codigo del contacto al cual se le asociara la reserva</param>
        /// <returns>Clase de Resuldatos, con codigo de error</returns>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-09-13
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public clsResultados GetReservaAirSabre(DataSet dsReserva, string sTA, string sITA, DataSet dsTarifa, string sProyecto, string sContacto)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            try
            {
                clsReservasAirSabre cReservaSabre = new clsReservasAirSabre();
                cResultados.dsResultados = new csCarrito().CrearEstructuraReservasExterna();
                DataTable dtReserva = cResultados.dsResultados.Tables[csReservas.TABLA_RESERVA];
                DataTable dtItinerary = cResultados.dsResultados.Tables[csReservas.TABLA_TRANSAC];
                DataTable dtPax = cResultados.dsResultados.Tables[csReservas.TABLA_PAX];
                DataTable dtTarifa = cResultados.dsResultados.Tables[csReservas.TABLA_TARIFA];
                DataTable dtTax = cResultados.dsResultados.Tables[csReservas.TABLA_TAXFARE];
                DataTable dtPol = cResultados.dsResultados.Tables[csReservas.TABLA_POL];

                cParametros = cReservaSabre.setTblReserva(dsReserva, dtReserva, sProyecto, sContacto);
                cResultados.Error = cParametros;
                if (cParametros.Id.Equals(0))
                {
                    cParametros.Id = 0;
                    cParametros.Message = "Reserva Cancelada, Record: ";
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.ViewMessage.Add("La reserva ya fue cancelada");
                    cParametros.Sugerencia.Add("No tiene itinerario");
                    cParametros.DatoAdicArr.Add("La reserva ya fue cancelada, No tiene itinerario");
                    ExceptionHandled.Publicar(cParametros);
                }
                else
                {
                    cReservaSabre.setModificaDatasetSabre(dsReserva, dsTarifa);
                    cParametros = cReservaSabre.setTblTransac(dsReserva, dtItinerary);
                    cParametros = cReservaSabre.setTblPax(dsReserva, dsTarifa, dtPax);
                    cParametros = cReservaSabre.setTblTax(dsTarifa, dtTax, dtPol, sTA, sITA);
                    cParametros = cReservaSabre.setTblTarifa(dsReserva, dsTarifa, dtTarifa, dtPol, dtTax);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = "GetReservaAirSabre";
                cParametros.Severity = clsSeveridad.Alta;
               ExceptionHandled.Publicar(cParametros);
                cResultados.Error = cParametros;
            }
            return cResultados;
        }

     
    }
    public class clsReservasAirSabre
    {
        private const string TABLA_REF = "ItineraryRef";
        private const string TABLA_ITINERARY = "Air";
        private const string TABLA_PASSENGERTYPE = "PassengerTypeQuantity";
        private const string TABLA_PERSONNAME = "PersonName";

        private const string TABLA_TAX = "TaxGen";
        private const string COLUMN_CODEPAX = "CodePax";
        private const string COLUMN_INTCODEPAX = "iCodePax";
        private const string COLUMN_CODETAX = "TaxCode";
        private const string COLUMN_CODEINTTAX = "iTaxCode";
        private const string COLUMN_CORRENCY = "CurrencyCode";
        private const string COLUMN_INTCORRENCY = "iCurrencyCode";
        private const string COLUMN_AMOUND = "Amount";
        private const string COLUMN_ADMON = "Admnon";

        public clsParametros setTblReserva(DataSet dsData, DataTable dtData)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {
                string sContacto = "0";
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    sContacto = cCache.Contacto;
                }
                if (dsData.Tables[TABLA_ITINERARY].Rows.Count > 0)
                {
                    string[] slFechaIni = clsValidaciones.Lista(dsData.Tables[TABLA_ITINERARY].Rows[0]["DepartureDateTime"].ToString(), "T");
                    string[] slFechaFin = clsValidaciones.Lista(dsData.Tables[TABLA_ITINERARY].Rows[dsData.Tables[TABLA_ITINERARY].Rows.Count - 1]["DepartureDateTime"].ToString(), "T");

                    string sFechaIni = string.Empty;
                    string sFechaFin = string.Empty;

                    string sHoraIni = string.Empty;
                    string sHoraFin = string.Empty;

                    if (slFechaIni.Length > 0)
                    {
                        sFechaIni = slFechaIni[0].ToString();
                    }
                    if (slFechaFin.Length > 0)
                    {
                        sFechaFin = slFechaFin[0].ToString();
                    }

                    if (slFechaIni.Length > 1)
                    {
                        sHoraIni = slFechaIni[1].ToString();
                    }
                    if (slFechaFin.Length > 1)
                    {
                        sHoraFin = slFechaFin[1].ToString();
                    }

                    DataRow fila = dtData.NewRow();

                    fila["intAplicacion"] = "1";
                    fila["intProyecto"] = "0";
                    fila["strLocalizadorExt"] = "";
                    fila["intContacto"] = sContacto;
                    fila["intCliente"] = "0";
                    fila["dtmFechaIni"] = sFechaIni;
                    fila["dtmFechaFin"] = sFechaFin;
                    fila["dtmVencimiento"] = sFechaIni;
                    fila["intEstado"] = "0";
                    fila["intResponsable"] = sContacto;
                    fila["intTipoPlan"] = "0";
                    fila["strReserva"] = dsData.Tables[TABLA_REF].Rows[0]["id"].ToString();
                    fila["dtmFecha"] = "";
                    fila["strObservacion"] = "";
                    fila["intCodigoPlan"] = "0";
                    fila["intFormaPago"] = "0";
                    fila["intEstadoPago"] = "0";
                    fila["dtmFechaLimitePago"] = "";
                    fila["intCentroC"] = "";
                    fila["strCodigo"] = "0";
                    fila["intConsecRes"] = "0";
                    fila["intConvenio"] = "0";
                    fila["strConvenioCorp"] = "";
                    fila["strDuracion"] = "";

                    dtData.Rows.Add(fila);
                    dtData.AcceptChanges();
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = "Reserva Cancelada, Record: ";
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.ViewMessage.Add("La reserva ya fue cancelada");
                    cParametros.Sugerencia.Add("No tiene itinerario");
                    cParametros.DatoAdicArr.Add("La reserva ya fue cancelada, No tiene itinerario");
                    ExceptionHandled.Publicar(cParametros);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.Severity =clsSeveridad.Alta;
                cParametros.ViewMessage.Add("No se encontro la Reserva");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Severity = clsSeveridad.Alta;
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        /// <summary>
        /// metodo pendiente por revision
        /// Insercion de datos de reserva, sobre carga para subir reserva - Subir Reserva - SRSABRE
        /// </summary>
        /// <param name="dsData">DataSet de sabre</param>
        /// <param name="dtData">DataTable de Reservas con la estructura de la base de datos</param>
        /// <param name="sProyecto">Codigo del proyecto a subir, si es 0, indica que es un proyectonuevo, si tiene codigo,se inserta la reserva en el proyecto</param>
        /// <param name="sContacto">Codigo del contacto al cual se le asociara la reserva</param>
        /// <returns>cParametros, clase clsParametros donde se guarda el error y las tablas dereserva</returns>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-09-13
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public clsParametros setTblReserva(DataSet dsData, DataTable dtData, string sProyecto, string sContacto)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {
                if (sContacto.Equals("0") || sContacto.Length.Equals(0))
                {
                    clsCache cCache = new csCache().cCache();
                    if (cCache != null)
                    {
                        sContacto = cCache.Contacto;
                    }
                }
                string sFormatoFechaBD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");

                //tblRefere otblRefere = new tblRefere();
                string iEstadoReserva = "0";
                string iEstadoPago = "0";
                string iEstadoEfectivo = "0";
                string iPlan = "0";
                string sEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReservaInicialAereo", "HK");
                string sTipoEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva");

                string sEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPagoPendiente", "PP");
                string sTipoEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPago", "EstadoPago");

                string sEstadoEfectivo = clsValidaciones.GetKeyOrAdd("Efectivo", "EFE");
                string sTipoFormaPago = clsValidaciones.GetKeyOrAdd("FormasPago", "FP");

                string sTiposPlan = clsValidaciones.GetKeyOrAdd("TiposPlan", "TipoPlan");
                string sAereo_WS = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");

                //otblRefere.Get(sTipoEstadoReserva, sEstadoReserva);
                //if (otblRefere.Respuesta)
                //    iEstadoReserva = otblRefere.intidRefere.Value;

                //otblRefere.Get(sTipoEstadoPago, sEstadoPago);
                //if (otblRefere.Respuesta)
                //    iEstadoPago = otblRefere.intidRefere.Value;

                //otblRefere.Get(sTipoFormaPago, sEstadoEfectivo);
                //if (otblRefere.Respuesta)
                //    iEstadoEfectivo = otblRefere.intidRefere.Value;

                //otblRefere.Get(sTiposPlan, sAereo_WS);
                //if (otblRefere.Respuesta)
                //    iPlan = otblRefere.intidRefere.Value;

                string sFecha = DateTime.Now.ToString(sFormatoFechaBD);
                if (dsData.Tables[TABLA_ITINERARY].Rows.Count > 0)
                {
                    string[] slFechaIni = clsValidaciones.Lista(dsData.Tables[TABLA_ITINERARY].Rows[0]["DepartureDateTime"].ToString(), "T");
                    string[] slFechaFin = clsValidaciones.Lista(dsData.Tables[TABLA_ITINERARY].Rows[dsData.Tables[TABLA_ITINERARY].Rows.Count - 1]["DepartureDateTime"].ToString(), "T");

                    string sFechaIni = string.Empty;
                    string sFechaFin = string.Empty;

                    string sHoraIni = string.Empty;
                    string sHoraFin = string.Empty;

                    if (slFechaIni.Length > 0)
                    {
                        sFechaIni = slFechaIni[0].ToString();
                    }
                    if (slFechaFin.Length > 0)
                    {
                        sFechaFin = slFechaFin[0].ToString();
                    }

                    if (slFechaIni.Length > 1)
                    {
                        sHoraIni = slFechaIni[1].ToString();
                    }
                    if (slFechaFin.Length > 1)
                    {
                        sHoraFin = slFechaFin[1].ToString();
                    }

                    DataRow fila = dtData.NewRow();

                    fila["intAplicacion"] = clsSesiones.getAplicacion().ToString();
                    if (sProyecto.Equals("0") || sProyecto.Length.Equals(0))
                    {
                        fila["intProyecto"] = "0";
                    }
                    else
                    {
                        fila["intProyecto"] = sProyecto;
                    }
                    fila["strLocalizadorExt"] = "";
                    fila["intContacto"] = sContacto;
                    fila["intCliente"] = "0";
                    fila["dtmFechaIni"] = sFechaIni;
                    fila["dtmFechaFin"] = sFechaFin;
                    fila["dtmVencimiento"] = sFechaIni;
                    fila["intEstado"] = iEstadoReserva;
                    fila["intResponsable"] = sContacto;
                    fila["intTipoPlan"] = iPlan;
                    fila["strReserva"] = dsData.Tables[TABLA_REF].Rows[0]["id"].ToString();
                    fila["dtmFecha"] = sFecha;
                    fila["strObservacion"] = "";
                    fila["intCodigoPlan"] = "0";
                    fila["intFormaPago"] = iEstadoEfectivo;
                    fila["intEstadoPago"] = iEstadoPago;
                    fila["dtmFechaLimitePago"] = sFecha;
                    fila["intCentroC"] = "";
                    fila["strCodigo"] = "0";
                    fila["intConsecRes"] = "0";
                    fila["intConvenio"] = "0";
                    fila["strConvenioCorp"] = "";
                    fila["strDuracion"] = "";

                    dtData.Rows.Add(fila);
                    dtData.AcceptChanges();
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = "Reserva Cancelada, Record: ";
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.ViewMessage.Add("La reserva ya fue cancelada");
                    cParametros.Sugerencia.Add("No tiene itinerario");
                    cParametros.DatoAdicArr.Add("La reserva ya fue cancelada, No tiene itinerario");
                   ExceptionHandled.Publicar(cParametros);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.ViewMessage.Add("No se encontro la Reserva");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Severity = clsSeveridad.Alta;
               ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        /// <summary>
        /// metodo pendiente por revision
        /// Insercion de datos de transacciones de la reserva, sobre carga para subir reserva - Subir Reserva - SRSABRE
        /// </summary>
        /// <param name="dsData">DataSet de sabre</param>
        /// <param name="dtData">DataTable de Segmentos con la estructura de la base de datos</param>
        /// <returns>cParametros, clase clsParametros donde se guarda el error y las tablas dereserva</returns>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-09-13
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public clsParametros setTblTransac(DataSet dsData, DataTable dtData)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {
                //tblRefere otblRefere = new tblRefere();
                DataTable dtItinerario = dsData.Tables[TABLA_ITINERARY];

                int iSegmento = 1;

                //tblRefere otblReferePlan = new tblRefere();
                string iEstadoReserva = "0";
                string iPlan = "0";
                string sEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReservaInicialAereo", "HK");
                string sTipoEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva");

                string sTiposPlan = clsValidaciones.GetKeyOrAdd("TiposPlan", "TipoPlan");
                string sAereo_WS = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");

                //otblReferePlan.Get(sTipoEstadoReserva, sEstadoReserva);
                //if (otblReferePlan.Respuesta)
                //    iEstadoReserva = otblReferePlan.intidRefere.Value;

                //otblReferePlan.Get(sTiposPlan, sAereo_WS);
                //if (otblReferePlan.Respuesta)
                //    iPlan = otblReferePlan.intidRefere.Value;

                //string sFecha = DateTime.Now.ToString(sFormatoFechaBD);

                foreach (DataRow drItinerario in dtItinerario.Rows)
                {
                    DataRow fila = dtData.NewRow();

                    fila["strReserva"] = dsData.Tables[TABLA_REF].Rows[0]["id"].ToString();
                    fila["intTipoPlan"] = iPlan;
                    fila["intCodigoPlan"] = "0";
                    fila["intCantidadPersonas"] = "0";
                    fila["intTipoAcomodacion"] = "0";
                    fila["intTipoHabitacion"] = "0";
                    fila["strConfirma"] = "";
                    fila["intEstado"] = iEstadoReserva;
                    fila["strCodigo"] = "0";
                    fila["intConsecRes"] = "0";
                    fila["strTipoAcomodacion"] = "";
                    fila["strTipoHabitacion"] = "";
                    fila["strCantidadPersonas"] = "";

                    string[] slFechaIni = clsValidaciones.Lista(drItinerario["DepartureDateTime"].ToString(), "T");
                    string[] slFechaFin = clsValidaciones.Lista(drItinerario["ArrivalDateTime"].ToString(), "T");

                    string sFechaIni = string.Empty;
                    string sFechaFin = string.Empty;

                    string sHoraIni = string.Empty;
                    string sHoraFin = string.Empty;

                    if (slFechaIni.Length > 0)
                    {
                        sFechaIni = slFechaIni[0].ToString();
                    }
                    if (slFechaFin.Length > 0)
                    {
                        sFechaFin = slFechaFin[0].ToString();
                    }

                    if (slFechaIni.Length > 1)
                    {
                        sHoraIni = slFechaIni[1].ToString();
                    }
                    if (slFechaFin.Length > 1)
                    {
                        sHoraFin = slFechaFin[1].ToString();
                    }

                    fila["strHoraIni"] = sHoraIni;
                    fila["strHoraFin"] = sHoraFin;
                    fila["intSegmento"] = iSegmento.ToString();
                    fila["dtmFechaIni"] = sFechaIni;
                    fila["dtmFechaFin"] = sFechaFin;
                    fila["strOperador"] = drItinerario["FlightNumber"].ToString();

                    foreach (DataRow drDepartureAirport in drItinerario.GetChildRows("Air_DepartureAirport"))
                    {
                        fila["strOrigen"] = drDepartureAirport["LocationCode"].ToString();
                        //otblRefere.Get(clsValidaciones.GetKeyOrAdd("AEROPUERTOS"), drDepartureAirport["LocationCode"].ToString(), drDepartureAirport["LocationCode"].ToString(), "0");
                        //if (otblRefere.Respuesta)
                        //{
                        //    fila["intOrigen"] = otblRefere.intidRefere.Value;
                        //}
                        //else
                        //{
                        //    fila["intOrigen"] = "0";
                        //}
                        fila["intOrigen"] = "0";
                    }
                    foreach (DataRow drArrivalAirport in drItinerario.GetChildRows("Air_ArrivalAirport"))
                    {
                        fila["strDestino"] = drArrivalAirport["LocationCode"].ToString();
                        //otblRefere.Get(clsValidaciones.GetKeyOrAdd("AEROPUERTOS"), drArrivalAirport["LocationCode"].ToString(), drArrivalAirport["LocationCode"].ToString(), "0");
                        //if (otblRefere.Respuesta)
                        //{
                        //    fila["intDestino"] = otblRefere.intidRefere.Value;
                        //}
                        //else
                        //{
                        //    fila["intDestino"] = "0";
                        //}
                        fila["intDestino"] = "0";
                    }
                    foreach (DataRow drOperatingAirline in drItinerario.GetChildRows("Air_OperatingAirline"))
                    {
                        //otblRefere.Get(clsValidaciones.GetKeyOrAdd("AIRLINESABRE"), drOperatingAirline["Code"].ToString(), drOperatingAirline["Code"].ToString());
                        //if (otblRefere.Respuesta)
                        //{
                        //    fila["intProveedor"] = otblRefere.intidRefere.Value;
                        //}
                        //else
                        //{
                        //    fila["intProveedor"] = "0";
                        //}
                        fila["intProveedor"] = "0";
                    }
                    foreach (DataRow drMarketingAirline in drItinerario.GetChildRows("Air_MarketingAirline"))
                    {
                        //otblRefere.Get(clsValidaciones.GetKeyOrAdd("AIRLINESABRE"), drMarketingAirline["Code"].ToString(), drMarketingAirline["Code"].ToString());
                        fila["strRegimen"] = drMarketingAirline["Code"].ToString();
                    }
                    dtData.Rows.Add(fila);
                    iSegmento++;
                }
                dtData.AcceptChanges();
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.ViewMessage.Add("La reserva ya fue cancelada");
                cParametros.Sugerencia.Add("No tiene itinerario");
                cParametros.DatoAdicArr.Add("La reserva ya fue cancelada, No tiene itinerario");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="dsData"></param>
        /// <param name="dsTarifa"></param>
        /// <param name="dtData"></param>
        /// <returns></returns>
        public clsParametros setTblPax(DataSet dsData, DataSet dsTarifa, DataTable dtData)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {
                List<VO_TarifaPago> lvoTarifaPago = new List<VO_TarifaPago>();
                lvoTarifaPago = clsSesiones.getTarifaPagoAir();

                //tblRefere otblRefere = new tblRefere();
                DataTable dtPassengerType = dsTarifa.Tables[TABLA_PASSENGERTYPE];
                DataTable dtPassengerName = dsData.Tables[TABLA_PERSONNAME];
                int iCodigoPax = 1;
                string sEmail = "";
                string sTel = "";
                foreach (DataRow drPersonName in dtPassengerName.Rows)
                {
                    if (drPersonName["GivenName"].ToString().Length > 0)
                    {
                        foreach (DataRow drCustomer in drPersonName.GetParentRows("Customer_PersonName"))
                        {
                            foreach (DataRow drEmail in drCustomer.GetChildRows("Customer_Email"))
                            {
                                string sEmailTemp = string.Empty;
                                sEmail = drEmail["Email_Column"].ToString();
                                sEmail = sEmail.Replace("Â", "");
                                break;
                            }
                            foreach (DataRow drTelephone in drCustomer.GetChildRows("Customer_Telephone"))
                            {
                                sTel = drTelephone["AreaCityCode"].ToString() + " " + drTelephone["PhoneNumber"].ToString();
                                break;
                            }
                        }
                        if (sEmail.Length > 0 && sTel.Length > 0)
                            break;
                    }
                }
                foreach (VO_TarifaPago drPersonName in lvoTarifaPago)
                {
                    DataRow fila = dtData.NewRow();

                    string sTipoPaxAdt = clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT");
                    try
                    {
                        if (drPersonName.Pasajero.SCodigo != null)
                            sTipoPaxAdt = drPersonName.Pasajero.SCodigo;
                    }
                    catch { }
                    string sTipoPax = sTipoPaxAdt;

                    fila["strReserva"] = dsData.Tables[TABLA_REF].Rows[0]["id"].ToString();
                    fila["intCodigoPax"] = iCodigoPax.ToString();
                    fila["dtmFechaNac"] = "";
                    fila["intEdad"] = "0";
                    fila["intGenero"] = "0";
                    fila["strCodigo"] = "0";
                    fila["intConsecRes"] = "0";
                    fila["strPasaporte"] = "";
                    fila["intTipoDoc"] = "0";
                    fila["strViajeroFrecuente"] = "";
                    fila["strNacionalidad"] = "";
                    fila["strPaisResidencia"] = "";
                    fila["dtmFechaExpPasaporte"] = "";
                    fila["intSegmento"] = "0";

                    //otblRefere.Get(clsValidaciones.GetKeyOrAdd("TiposPasajero", "Tip_Pasajero"), sTipoPax, sTipoPax, "0");
                    //if (otblRefere.Respuesta)
                    //{
                    //    fila["intTipoPax"] = otblRefere.intidRefere.Value;
                    //}
                    //else
                    //{
                    //    fila["intTipoPax"] = "0";
                    //}
                    fila["intTipoPax"] = "0";
                    fila["strNombre"] = drPersonName.Nombre;
                    fila["strEmail"] = sEmail;
                    fila["strTelefono"] = sTel;
                    dtData.Rows.Add(fila);
                    iCodigoPax++;
                }
                dtData.AcceptChanges();
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.ViewMessage.Add("La reserva ya fue cancelada");
                cParametros.Sugerencia.Add("No tiene itinerario");
                cParametros.DatoAdicArr.Add("La reserva ya fue cancelada, No tiene itinerario");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="dsData"></param>
        /// <param name="dsTarifa"></param>
        /// <param name="dtData"></param>
        /// <param name="dtDataPol"></param>
        /// <param name="dtTax"></param>
        /// <returns></returns>
        public clsParametros setTblTarifa(DataSet dsData, DataSet dsTarifa, DataTable dtData, DataTable dtDataPol, DataTable dtTax)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                //tblRefere otblRefere = new tblRefere();
                DataTable dtPassengerType = dsTarifa.Tables[TABLA_PASSENGERTYPE];
                foreach (DataRow dtPassenger in dtPassengerType.Rows)
                {
                    decimal dTax = 0;
                    decimal dTaxAdmon = 0;
                    decimal dTarifa = 0;
                    string sTipoPaxTax = "0";
                    DataRow fila = dtData.NewRow();

                    fila["strReserva"] = dsData.Tables[TABLA_REF].Rows[0]["id"].ToString();
                    fila["intCodigFare"] = "0";
                    fila["dblTotal"] = "0";
                    fila["dblDescuento"] = "0";
                    fila["dblTax"] = "0";
                    fila["intConsecRes"] = "0";
                    fila["dblPenalidad"] = "0";
                    fila["strCodigo"] = "0";
                    fila["intSegmento"] = "0";

                    string sDetallePax = dtPassenger["Code"].ToString();
                    if (dtPassenger["Code"].ToString().Contains("C"))
                    {
                        if (!dtPassenger["Code"].ToString().Equals("CNN"))
                        {
                            int iEdadPax = int.Parse(clsValidaciones.RetornaNumero(dtPassenger["Code"].ToString().Substring(1)));
                            sDetallePax = "Niño " + iEdadPax.ToString() + " Años";
                        }
                    }
                    //otblRefere.Get(clsValidaciones.GetKeyOrAdd("TiposPasajero", "Tip_Pasajero"), dtPassenger["Code"].ToString(), sDetallePax, "0");
                    //if (otblRefere.Respuesta)
                    //{
                    //    fila["intTipoPax"] = otblRefere.intidRefere.Value;
                    //    sTipoPaxTax = otblRefere.intidRefere.Value;
                    //}
                    //else
                    //{
                    //    fila["intTipoPax"] = "0";
                    //    sTipoPaxTax = "0";

                    //}

                    fila["intTipoPax"] = "0";
                    sTipoPaxTax = "0";

                    foreach (DataRow drPTC_FareBreakdown in dtPassenger.GetParentRows("PTC_FareBreakdown_PassengerTypeQuantity"))
                    {
                        foreach (DataRow drAirItineraryPricingInfo in drPTC_FareBreakdown.GetParentRows("AirItineraryPricingInfo_PTC_FareBreakdown"))
                        {
                            foreach (DataRow drItTotalFare in drAirItineraryPricingInfo.GetChildRows("AirItineraryPricingInfo_ItinTotalFare"))
                            {
                                foreach (DataRow drTotalFare in drItTotalFare.GetChildRows("ItinTotalFare_TotalFare"))
                                {
                                    dTarifa += decimal.Parse(drTotalFare["Amount"].ToString());
                                    //otblRefere.Get(clsValidaciones.GetKeyOrAdd("Moneda"), drTotalFare["CurrencyCode"].ToString(), drTotalFare["CurrencyCode"].ToString());
                                    //if (otblRefere.Respuesta)
                                    //{
                                    //    fila["intMoneda"] = otblRefere.intidRefere.Value;
                                    //}
                                    //else
                                    //{
                                    //    fila["intMoneda"] = "0";
                                    //}
                                    fila["intMoneda"] = "0";
                                }
                            }
                        }
                        foreach (DataRow drTaxes in dtTax.Rows)
                        {
                            if (sTipoPaxTax.Equals(drTaxes["intTipoPax"].ToString()))
                            {
                                if (drTaxes[COLUMN_ADMON].Equals(0))
                                {
                                    dTax += decimal.Parse(drTaxes["dblValorTax"].ToString());
                                }
                                else
                                {
                                    dTaxAdmon += decimal.Parse(drTaxes["dblValorTax"].ToString());
                                }
                            }
                        }
                        decimal dTarifaTotal = dTarifa - dTax;
                        decimal dTaxTotal = dTaxAdmon + dTax;
                        decimal dTotalTotal = dTarifaTotal + dTaxTotal;

                        fila["dblValor"] = dTarifaTotal.ToString();
                        fila["dblTax"] = dTaxTotal.ToString();
                        fila["dblTotal"] = dTotalTotal.ToString();
                    }
                    dtData.Rows.Add(fila);
                }
                dtData.AcceptChanges();
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.ViewMessage.Add("La reserva ya fue cancelada");
                cParametros.Sugerencia.Add("No tiene itinerario");
                cParametros.DatoAdicArr.Add("La reserva ya fue cancelada, No tiene itinerario");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="dsTarifa"></param>
        /// <param name="dtData"></param>
        /// <param name="dtDataPol"></param>
        /// <param name="sTA"></param>
        /// <param name="sITA"></param>
        /// <returns></returns>
        public clsParametros setTblTax(DataSet dsTarifa, DataTable dtData, DataTable dtDataPol, string sTA, string sITA)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                DataTable dtTax = dsTarifa.Tables[TABLA_TAX];
                //tblRefere otblRefere = new tblRefere();
                string sTaxSabre = clsValidaciones.GetKeyOrAdd("SABRETAX");

                try { dtData.Columns.Add(COLUMN_ADMON, typeof(int)); }
                catch { }
                dtData.AcceptChanges();

                string sTARefere = "0";
                string sITARefere = "0";

                //otblRefere.Get(sTaxSabre, clsValidaciones.GetKeyOrAdd("TASA_ADMINISTRATIVA", "TA"), clsValidaciones.GetKeyOrAdd("TASA_ADMINISTRATIVA", "TA"));
                //if (otblRefere.Respuesta)
                //{
                //    sTARefere = otblRefere.intidRefere.Value;
                //}

                //otblRefere.Get(sTaxSabre, clsValidaciones.GetKeyOrAdd("IVA_TA", "ITA"), clsValidaciones.GetKeyOrAdd("IVA_TA", "ITA"));
                //if (otblRefere.Respuesta)
                //{
                //    sITARefere = otblRefere.intidRefere.Value;
                //}


                string sTipoPaxAnt = string.Empty;
                foreach (DataRow drTax in dtTax.Rows)
                {
                    DataRow fila = dtData.NewRow();
                    fila["intCodigFare"] = "0";
                    fila["intConsecRes"] = "0";
                    fila["dblPorcent"] = "0";
                    fila["strCodigo"] = "0";
                    fila["intTipoPax"] = drTax[COLUMN_INTCODEPAX].ToString();
                    fila["intMoneda"] = drTax[COLUMN_INTCORRENCY].ToString();
                    fila["intCodigoTax"] = drTax[COLUMN_CODEINTTAX].ToString();
                    fila["dblValorTax"] = drTax[COLUMN_AMOUND].ToString();
                    fila[COLUMN_ADMON] = 0;
                    dtData.Rows.Add(fila);
                }
                foreach (DataRow drTax1 in dtTax.Rows)
                {
                    if (!sTipoPaxAnt.Equals(drTax1[COLUMN_INTCODEPAX].ToString()))
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            DataRow fila1 = dtData.NewRow();
                            fila1["intCodigFare"] = "0";
                            fila1["intConsecRes"] = "0";
                            fila1["dblPorcent"] = "0";
                            fila1["strCodigo"] = "0";
                            fila1["intTipoPax"] = drTax1[COLUMN_INTCODEPAX].ToString();
                            fila1["intMoneda"] = drTax1[COLUMN_INTCORRENCY];
                            fila1[COLUMN_ADMON] = 1;

                            if (i.Equals(0))
                            {
                                fila1["intCodigoTax"] = sTARefere;
                                fila1["dblValorTax"] = sTA;
                            }
                            else
                            {
                                fila1["intCodigoTax"] = sITARefere;
                                fila1["dblValorTax"] = sITA;
                            }
                            dtData.Rows.Add(fila1);
                        }
                    }
                    sTipoPaxAnt = drTax1[COLUMN_INTCODEPAX].ToString();
                }
                dtData.AcceptChanges();
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.Severity =clsSeveridad.Alta;
                cParametros.ViewMessage.Add("La reserva ya fue cancelada");
                cParametros.Sugerencia.Add("No tiene itinerario");
                cParametros.DatoAdicArr.Add("La reserva ya fue cancelada, No tiene itinerario");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        private DataTable setTax()
        {
            DataTable tblTax = new DataTable(TABLA_TAX);
            try
            {
                tblTax.Columns.Add(COLUMN_INTCODEPAX, typeof(string));
                tblTax.Columns.Add(COLUMN_CODEPAX, typeof(string));
                tblTax.Columns.Add(COLUMN_CODEINTTAX, typeof(string));
                tblTax.Columns.Add(COLUMN_CODETAX, typeof(string));
                tblTax.Columns.Add(COLUMN_INTCORRENCY, typeof(string));
                tblTax.Columns.Add(COLUMN_CORRENCY, typeof(string));
                tblTax.Columns.Add(COLUMN_AMOUND, typeof(decimal));
                tblTax.Columns.Add(COLUMN_ADMON, typeof(int));
            }
            catch { }
            return tblTax;
        }

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="dsData"></param>
        /// <param name="dsTarifa"></param>
        /// <returns></returns>
        public clsParametros setModificaDatasetSabre(DataSet dsData, DataSet dsTarifa)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                DataTable dtPassengerType = dsData.Tables[TABLA_PASSENGERTYPE];
                DataTable dtPassengerTypeName = dsTarifa.Tables[TABLA_PASSENGERTYPE];
                /*AGREGAMOS LA COLUMNA DE TASA EQUIVALENTE EN USD*/
                try { dtPassengerTypeName.Columns.Add("Quantity", typeof(int)); }
                catch { }
                dtPassengerTypeName.AcceptChanges();

                foreach (DataRow dtPassenger in dtPassengerTypeName.Rows)
                {
                    foreach (DataRow dtPassengerTypeGen in dtPassengerType.Rows)
                    {
                        if (dtPassenger["Code"].ToString().Equals(dtPassengerTypeGen["Code"].ToString()))
                        {
                            dtPassenger["Quantity"] = dtPassengerTypeGen["Quantity"];
                            break;
                        }
                    }
                }
                dtPassengerTypeName.AcceptChanges();
                if (!dsTarifa.Tables.Contains(TABLA_TAX))
                {
                    List<VO_TarifaPago> lvoTarifaPago = clsSesiones.getTarifaPagoAir();
                    DataTable dtData = setTax();
                    //tblRefere otblRefere = new tblRefere();

                    string sMoneda = clsValidaciones.GetKeyOrAdd("Moneda");
                    string sMonedaCop = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                    string sMonedaActual = sMonedaCop;
                    string sTipoPax = clsValidaciones.GetKeyOrAdd("TiposPasajero", "Tip_Pasajero");
                    string sTaxSabre = clsValidaciones.GetKeyOrAdd("SABRETAX");
                    string sTipoPaxAnt = string.Empty;

                    for (int i = 0; i < lvoTarifaPago.Count; i++)
                    {
                        if (!sTipoPaxAnt.Equals(lvoTarifaPago[i].Pasajero.SCodigo))
                        {
                            for (int j = 0; j < lvoTarifaPago[i].LImpuestos.Count; j++)
                            {
                                DataRow fila = dtData.NewRow();
                                fila[COLUMN_CODEPAX] = lvoTarifaPago[i].Pasajero.SCodigo;
                                fila[COLUMN_CODETAX] = lvoTarifaPago[i].LImpuestos[j].SCodigo;
                                fila[COLUMN_AMOUND] = lvoTarifaPago[i].LImpuestos[j].DValor;
                                try
                                {
                                    if (lvoTarifaPago[i].LImpuestos[j].SCodigoMoneda != null)
                                        fila[COLUMN_CORRENCY] = lvoTarifaPago[i].LImpuestos[j].SCodigoMoneda;
                                    else
                                        fila[COLUMN_CORRENCY] = sMonedaCop;
                                }
                                catch { }
                                string sDetallePax = lvoTarifaPago[i].Pasajero.SCodigo;
                                if (lvoTarifaPago[i].Pasajero.SCodigo.Contains("C"))
                                {
                                    if (!lvoTarifaPago[i].Pasajero.SCodigo.Equals("CNN"))
                                    {
                                        int iEdadPax = int.Parse(clsValidaciones.RetornaNumero(lvoTarifaPago[i].Pasajero.SCodigo.Substring(1)));
                                        sDetallePax = "Niño " + iEdadPax.ToString() + " Años";
                                    }
                                }
                                //otblRefere.Get(sTipoPax, lvoTarifaPago[i].Pasajero.SCodigo, sDetallePax, "0");
                                //if (otblRefere.Respuesta)
                                //{
                                //    fila[COLUMN_INTCODEPAX] = otblRefere.intidRefere.Value;
                                //}
                                //else
                                //{
                                //    fila[COLUMN_INTCODEPAX] = "0";
                                //}

                                fila[COLUMN_INTCODEPAX] = "0";

                                //otblRefere.Get(sTaxSabre, lvoTarifaPago[i].LImpuestos[j].SCodigo, lvoTarifaPago[i].LImpuestos[j].SCodigo, "0");
                                //if (otblRefere.Respuesta)
                                //{
                                //    fila[COLUMN_CODEINTTAX] = otblRefere.intidRefere.Value;
                                //}
                                //else
                                //{
                                //    fila[COLUMN_CODEINTTAX] = "0";
                                //}
                                fila[COLUMN_CODEINTTAX] = "0";

                                try
                                {
                                    if (lvoTarifaPago[i].LImpuestos[j].SCodigoMoneda != null)
                                        sMonedaActual = lvoTarifaPago[i].LImpuestos[j].SCodigoMoneda;
                                }
                                catch { }
                                //otblRefere.Get(sMoneda, sMonedaActual, sMonedaActual, "0");
                                //if (otblRefere.Respuesta)
                                //{
                                //    fila[COLUMN_INTCORRENCY] = otblRefere.intidRefere.Value;
                                //}
                                //else
                                //{
                                //    fila[COLUMN_INTCORRENCY] = "0";
                                //}
                                fila[COLUMN_INTCORRENCY] = "0";
                                dtData.Rows.Add(fila);
                            }
                        }
                        sTipoPaxAnt = lvoTarifaPago[i].Pasajero.SCodigo;
                    }
                    dtData.AcceptChanges();
                    dsTarifa.Tables.Add(dtData);
                    dsTarifa.AcceptChanges();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.Severity =clsSeveridad.Alta;
                cParametros.ViewMessage.Add("La reserva ya fue cancelada");
                cParametros.Sugerencia.Add("No tiene itinerario");
                cParametros.DatoAdicArr.Add("La reserva ya fue cancelada, No tiene itinerario");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
    }
}