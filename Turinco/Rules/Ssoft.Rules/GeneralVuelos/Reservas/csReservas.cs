using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Ssoft.Sql;
using Ssoft.Utils;
using Ssoft.Data;
using Ssoft.ManejadorExcepciones;
using Ssoft.ValueObjects;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Web;
using Ssoft.Rules.Generales;
using SsoftQuery.Vuelos;
using SsoftQuery.Planes;


namespace Ssoft.Rules.Reservas
{
    public class csReservas
    {
       
      
        

        private DataSql pclsDataSql = new DataSql();
        private string sFormatoFecha = clsSesiones.getFormatoFecha();
        private string sFormatoFechaBD = clsSesiones.getFormatoFechaBD();
        private static string sCaracterDecimal = clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",");

        public const string TABLA_CARRITO = "CarritoCompras";
        public const string TABLA_RESERVA = "tblReserva";
        public const string TABLA_TRANSAC = "tblTransac";
        public const string TABLA_PAX = "tblPax";
        public const string TABLA_TARIFA = "tblTarifa";
        public const string TABLA_TAXFARE = "tblTax";
        public const string TABLA_REMARK = "tblRemark";
        public const string TABLA_POL = "tblPol";

        protected string gstrConexion = string.Empty;

        public csReservas()
        {
            Conexion = clsSesiones.getConexion();
            pclsDataSql.Conexion = clsSesiones.getConexion();
        }

        public string Conexion
        {
            set { this.gstrConexion = value; }
            get { return this.gstrConexion; }
        }


        public DataSet CrearTablaReserva()
        {
            try
            {
                DataTable tReserva = new DataTable();
                DataTable tTransac = new DataTable();
                DataTable tPax = new DataTable();
                DataTable tTarifa = new DataTable();
                DataTable tTax = new DataTable();
                DataTable tRemark = new DataTable();
                DataTable tPol = new DataTable();

                DataSet dsData = new DataSet();

                tReserva = CrearTablaMaster();
                tTransac = CrearTablaTransac();
                tPax = CrearTablaPax();
                tTarifa = CrearTablaTarifa();
                tTax = CrearTablaTax();
                tRemark = CrearTablaRemark();
                tPol = CrearTablaPol();

                dsData.Tables.Add(tReserva);
                dsData.Tables.Add(tTransac);
                dsData.Tables.Add(tPax);
                dsData.Tables.Add(tTarifa);
                dsData.Tables.Add(tTax);
                dsData.Tables.Add(tRemark);
                dsData.Tables.Add(tPol);

                return dsData;
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
                return null;
            }
        }

        public DataTable CrearTablaMaster()
        {
            try
            {
                DataTable tblTable = new DataTable();

                DataColumn dcintAplicacion = new DataColumn("intAplicacion");
                DataColumn dcintProyecto = new DataColumn("intProyecto");
                DataColumn dcstrLocalizadorExt = new DataColumn("strLocalizadorExt");
                DataColumn dcintContacto = new DataColumn("intContacto");
                DataColumn dcintCliente = new DataColumn("intCliente");
                DataColumn dcdtmFechaIni = new DataColumn("dtmFechaIni");
                DataColumn dcdtmFechaFin = new DataColumn("dtmFechaFin");
                DataColumn dcdtmVencimiento = new DataColumn("dtmVencimiento");
                DataColumn dcintEstado = new DataColumn("intEstado");
                DataColumn dcintResponsable = new DataColumn("intResponsable");
                DataColumn dcintTipoPlan = new DataColumn("intTipoPlan");
                DataColumn dcstrReserva = new DataColumn("strReserva");
                DataColumn dcdtmFecha = new DataColumn("dtmFecha");
                DataColumn dcstrObservacion = new DataColumn("strObservacion");
                DataColumn dcintCodigoPlan = new DataColumn("intCodigoPlan");
                DataColumn dcintFormaPago = new DataColumn("intFormaPago");
                DataColumn dcintEstadoPago = new DataColumn("intEstadoPago");
                DataColumn dcdtmFechaLimitePago = new DataColumn("dtmFechaLimitePago");
                DataColumn dcintCentroC = new DataColumn("intCentroC");
                DataColumn dcstrCodigo = new DataColumn("strCodigo");
                DataColumn dcintConsecRes = new DataColumn("intConsecRes");
                DataColumn dcintConvenio = new DataColumn("intConvenio");
                DataColumn dcstrConvenioCorp = new DataColumn("strConvenioCorp");
                DataColumn dcstrDuracion = new DataColumn("strDuracion");
                DataColumn dcstrCodigoAscesor = new DataColumn("strCodigoAscesor");

                tblTable.Columns.Add(dcintAplicacion);
                tblTable.Columns.Add(dcintProyecto);
                tblTable.Columns.Add(dcstrLocalizadorExt);
                tblTable.Columns.Add(dcintContacto);
                tblTable.Columns.Add(dcintCliente);
                tblTable.Columns.Add(dcdtmFechaIni);
                tblTable.Columns.Add(dcdtmFechaFin);
                tblTable.Columns.Add(dcdtmVencimiento);
                tblTable.Columns.Add(dcintEstado);
                tblTable.Columns.Add(dcintResponsable);
                tblTable.Columns.Add(dcintTipoPlan);
                tblTable.Columns.Add(dcstrReserva);
                tblTable.Columns.Add(dcdtmFecha);
                tblTable.Columns.Add(dcstrObservacion);
                tblTable.Columns.Add(dcintCodigoPlan);
                tblTable.Columns.Add(dcintFormaPago);
                tblTable.Columns.Add(dcintEstadoPago);
                tblTable.Columns.Add(dcdtmFechaLimitePago);
                tblTable.Columns.Add(dcintCentroC);
                tblTable.Columns.Add(dcstrCodigo);
                tblTable.Columns.Add(dcintConsecRes);
                tblTable.Columns.Add(dcintConvenio);
                tblTable.Columns.Add(dcstrConvenioCorp);
                tblTable.Columns.Add(dcstrDuracion);
                tblTable.Columns.Add(dcstrCodigoAscesor);

                tblTable.Rows.Clear();
                tblTable.TableName = TABLA_RESERVA;

                return tblTable;
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
                return null;
            }
        }

        public DataTable CrearTablaTransac()
        {
            try
            {
                DataTable tblTable = new DataTable();

                DataColumn dcstrReserva = new DataColumn("strReserva");
                DataColumn dcintTipoPlan = new DataColumn("intTipoPlan");
                DataColumn dcintCodigoPlan = new DataColumn("intCodigoPlan");
                DataColumn dcintSegmento = new DataColumn("intSegmento");
                DataColumn dcintOrigen = new DataColumn("intOrigen");
                DataColumn dcstrOrigen = new DataColumn("strOrigen");
                DataColumn dcintDestino = new DataColumn("intDestino");
                DataColumn dcstrDestino = new DataColumn("strDestino");
                DataColumn dcdtmFechaIni = new DataColumn("dtmFechaIni");
                DataColumn dcdtmFechaFin = new DataColumn("dtmFechaFin");
                DataColumn dcstrHoraIni = new DataColumn("strHoraIni");
                DataColumn dcstrHoraFin = new DataColumn("strHoraFin");
                DataColumn dcintProveedor = new DataColumn("intProveedor");
                DataColumn dcintCantidadPersonas = new DataColumn("intCantidadPersonas");
                DataColumn dcintTipoAcomodacion = new DataColumn("intTipoAcomodacion");
                DataColumn dcintTipoHabitacion = new DataColumn("intTipoHabitacion");
                DataColumn dcstrOperador = new DataColumn("strOperador");
                DataColumn dcstrConfirma = new DataColumn("strConfirma");
                DataColumn dcstrObservacion = new DataColumn("strObservacion");
                DataColumn dcintEstado = new DataColumn("intEstado");
                DataColumn dcstrCodigo = new DataColumn("strCodigo");
                DataColumn dcintConsecRes = new DataColumn("intConsecRes");
                DataColumn dcstrTipoAcomodacion = new DataColumn("strTipoAcomodacion");
                DataColumn dcstrTipoHabitacion = new DataColumn("strTipoHabitacion");
                DataColumn dcstrCantidadPersonas = new DataColumn("strCantidadPersonas");
                DataColumn dcstrRegimen = new DataColumn("strRegimen");

                tblTable.Columns.Add(dcstrReserva);
                tblTable.Columns.Add(dcintTipoPlan);
                tblTable.Columns.Add(dcintCodigoPlan);
                tblTable.Columns.Add(dcintSegmento);
                tblTable.Columns.Add(dcintOrigen);
                tblTable.Columns.Add(dcstrOrigen);
                tblTable.Columns.Add(dcintDestino);
                tblTable.Columns.Add(dcstrDestino);
                tblTable.Columns.Add(dcdtmFechaIni);
                tblTable.Columns.Add(dcdtmFechaFin);
                tblTable.Columns.Add(dcstrHoraIni);
                tblTable.Columns.Add(dcstrHoraFin);
                tblTable.Columns.Add(dcintProveedor);
                tblTable.Columns.Add(dcintCantidadPersonas);
                tblTable.Columns.Add(dcintTipoAcomodacion);
                tblTable.Columns.Add(dcintTipoHabitacion);
                tblTable.Columns.Add(dcstrOperador);
                tblTable.Columns.Add(dcstrConfirma);
                tblTable.Columns.Add(dcstrObservacion);
                tblTable.Columns.Add(dcintEstado);
                tblTable.Columns.Add(dcstrCodigo);
                tblTable.Columns.Add(dcintConsecRes);
                tblTable.Columns.Add(dcstrTipoAcomodacion);
                tblTable.Columns.Add(dcstrTipoHabitacion);
                tblTable.Columns.Add(dcstrCantidadPersonas);
                tblTable.Columns.Add(dcstrRegimen);

                tblTable.Rows.Clear();
                tblTable.TableName = TABLA_TRANSAC;

                return tblTable;
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
                return null;
            }
        }

        public DataTable CrearTablaPax()
        {
            try
            {
                DataTable tblTable = new DataTable();

                DataColumn dcstrReserva = new DataColumn("strReserva");
                DataColumn dcintCodigoPax = new DataColumn("intCodigoPax");
                DataColumn dcintTipoPax = new DataColumn("intTipoPax");
                DataColumn dcstrNombre = new DataColumn("strNombre");
                DataColumn dcdtmFechaNac = new DataColumn("dtmFechaNac");
                DataColumn dcintEdad = new DataColumn("intEdad");
                DataColumn dcstrCodigo = new DataColumn("strCodigo");
                DataColumn dcintConsecRes = new DataColumn("intConsecRes");
                DataColumn dcintGenero = new DataColumn("intGenero");
                DataColumn dcstrPasaporte = new DataColumn("strPasaporte");
                DataColumn dcintTipoDoc = new DataColumn("intTipoDoc");
                DataColumn dcstrViajeroFrecuente = new DataColumn("strViajeroFrecuente");
                DataColumn dcstrNacionalidad = new DataColumn("strNacionalidad");
                DataColumn dcstrPaisResidencia = new DataColumn("strPaisResidencia");
                DataColumn dcdtmFechaExpPasaporte = new DataColumn("dtmFechaExpPasaporte");
                DataColumn dcintSegmento = new DataColumn("intSegmento");
                DataColumn dcstrEmail = new DataColumn("strEmail");
                DataColumn dcstrTelefono = new DataColumn("strTelefono");
                //DataColumn dcstrDocumento = new DataColumn("strDocumento");

                tblTable.Columns.Add(dcstrReserva);
                tblTable.Columns.Add(dcintCodigoPax);
                tblTable.Columns.Add(dcintTipoPax);
                tblTable.Columns.Add(dcstrNombre);
                tblTable.Columns.Add(dcdtmFechaNac);
                tblTable.Columns.Add(dcintEdad);
                tblTable.Columns.Add(dcstrCodigo);
                tblTable.Columns.Add(dcintConsecRes);
                tblTable.Columns.Add(dcintGenero);
                tblTable.Columns.Add(dcstrPasaporte);
                tblTable.Columns.Add(dcintTipoDoc);
                tblTable.Columns.Add(dcstrViajeroFrecuente);
                tblTable.Columns.Add(dcstrNacionalidad);
                tblTable.Columns.Add(dcstrPaisResidencia);
                tblTable.Columns.Add(dcdtmFechaExpPasaporte);
                tblTable.Columns.Add(dcintSegmento);
                tblTable.Columns.Add(dcstrEmail);
                tblTable.Columns.Add(dcstrTelefono);
                //tblTable.Columns.Add(dcstrDocumento);

                tblTable.Rows.Clear();
                tblTable.TableName = TABLA_PAX;

                return tblTable;
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
                return null;
            }
        }

        public DataTable CrearTablaTarifa()
        {
            try
            {
                DataTable tblTable = new DataTable();

                DataColumn dcstrReserva = new DataColumn("strReserva");
                DataColumn dcintTipoPax = new DataColumn("intTipoPax");
                DataColumn dcintCodigFare = new DataColumn("intCodigFare");
                DataColumn dcintMoneda = new DataColumn("intMoneda");
                DataColumn dcdblValor = new DataColumn("dblValor");
                DataColumn dcdblTotal = new DataColumn("dblTotal");
                DataColumn dcdblDescuento = new DataColumn("dblDescuento");
                DataColumn dcdblTax = new DataColumn("dblTax");
                DataColumn dcstrCodigo = new DataColumn("strCodigo");
                DataColumn dcintConsecRes = new DataColumn("intConsecRes");
                DataColumn dcdblPenalidad = new DataColumn("dblPenalidad");
                DataColumn dcintSegmento = new DataColumn("intSegmento");

                tblTable.Columns.Add(dcstrReserva);
                tblTable.Columns.Add(dcintTipoPax);
                tblTable.Columns.Add(dcintCodigFare);
                tblTable.Columns.Add(dcintMoneda);
                tblTable.Columns.Add(dcdblValor);
                tblTable.Columns.Add(dcdblTotal);
                tblTable.Columns.Add(dcdblDescuento);
                tblTable.Columns.Add(dcdblTax);
                tblTable.Columns.Add(dcstrCodigo);
                tblTable.Columns.Add(dcintConsecRes);
                tblTable.Columns.Add(dcdblPenalidad);
                tblTable.Columns.Add(dcintSegmento);

                tblTable.Rows.Clear();
                tblTable.TableName = TABLA_TARIFA;

                return tblTable;
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
                return null;
            }
        }

        public DataTable CrearTablaPol()
        {
            try
            {
                DataTable tblTable = new DataTable();

                DataColumn dcstrReserva = new DataColumn("strReserva");
                DataColumn dcstrCodigo = new DataColumn("strCodigo");
                DataColumn dcintCodigo = new DataColumn("intCodigo");
                DataColumn dcdblTotalBase = new DataColumn("dblTotalBase");
                DataColumn dcdblTotalTarifa = new DataColumn("dblTotalTarifa");
                DataColumn dcdblTotalIVA_Tarifa = new DataColumn("dblTotalIVA_Tarifa");
                DataColumn dcdblTotalImpuestos = new DataColumn("dblTotalImpuestos");
                DataColumn dcdblTotalImpuestoGasolina = new DataColumn("dblTotalImpuestoGasolina");
                DataColumn dcdblTotaBaselTA = new DataColumn("dblTotaBaselTA");
                DataColumn dcdblTotalIVA_TA = new DataColumn("dblTotalIVA_TA");
                DataColumn dcstrAerolinea = new DataColumn("strAerolinea");
                DataColumn dcdblTotalCarritoSinFormato = new DataColumn("dblTotalCarritoSinFormato");
                DataColumn dcintConsecRes = new DataColumn("intConsecRes");

                tblTable.Columns.Add(dcstrReserva);
                tblTable.Columns.Add(dcintCodigo);
                tblTable.Columns.Add(dcdblTotalBase);
                tblTable.Columns.Add(dcdblTotalTarifa);
                tblTable.Columns.Add(dcdblTotalIVA_Tarifa);
                tblTable.Columns.Add(dcdblTotalImpuestos);
                tblTable.Columns.Add(dcdblTotalImpuestoGasolina);
                tblTable.Columns.Add(dcdblTotaBaselTA);
                tblTable.Columns.Add(dcdblTotalIVA_TA);
                tblTable.Columns.Add(dcstrAerolinea);
                tblTable.Columns.Add(dcdblTotalCarritoSinFormato);
                tblTable.Columns.Add(dcintConsecRes);
                tblTable.Columns.Add(dcstrCodigo);

                tblTable.Rows.Clear();
                tblTable.TableName = TABLA_POL;

                return tblTable;
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
                return null;
            }
        }
        public DataTable CrearTablaTax()
        {
            try
            {
                DataTable tblTable = new DataTable();

                DataColumn dcintCodigFare = new DataColumn("intCodigFare");
                DataColumn dcintTipoPax = new DataColumn("intTipoPax");
                DataColumn dcintCodigoTax = new DataColumn("intCodigoTax");
                DataColumn dcintMoneda = new DataColumn("intMoneda");
                DataColumn dcdblValorTax = new DataColumn("dblValorTax");
                DataColumn dcdblPorcent = new DataColumn("dblPorcent");
                DataColumn dcstrCodigo = new DataColumn("strCodigo");
                DataColumn dcintConsecRes = new DataColumn("intConsecRes");

                tblTable.Columns.Add(dcintCodigFare);
                tblTable.Columns.Add(dcintTipoPax);
                tblTable.Columns.Add(dcintCodigoTax);
                tblTable.Columns.Add(dcintMoneda);
                tblTable.Columns.Add(dcdblValorTax);
                tblTable.Columns.Add(dcdblPorcent);
                tblTable.Columns.Add(dcstrCodigo);
                tblTable.Columns.Add(dcintConsecRes);

                tblTable.Rows.Clear();
                tblTable.TableName = TABLA_TAXFARE;

                return tblTable;
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
                return null;
            }
        }
        public DataTable CrearTablaRemark()
        {
            try
            {
                DataTable tblTable = new DataTable();

                DataColumn dcstrRecord = new DataColumn("strRecord");
                DataColumn dcintidrefere = new DataColumn("intidrefere");
                DataColumn dcintConsecutivo = new DataColumn("intConsecutivo");
                DataColumn dcstrDescripcion = new DataColumn("strDescripcion");
                DataColumn dcstrDetalle = new DataColumn("strDetalle");
                DataColumn dcintConsecRes = new DataColumn("intConsecRes");

                tblTable.Columns.Add(dcstrRecord);
                tblTable.Columns.Add(dcintidrefere);
                tblTable.Columns.Add(dcintConsecutivo);
                tblTable.Columns.Add(dcstrDescripcion);
                tblTable.Columns.Add(dcstrDetalle);
                tblTable.Columns.Add(dcintConsecRes);

                tblTable.Rows.Clear();
                tblTable.TableName = TABLA_REMARK;
                return tblTable;
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
                return null;
            }
        }

        /// <summary>
        /// METODO PENDIENTE POR REVISION
        /// </summary>
        /// <param name="dsReserva"></param>
        /// <returns></returns>
        public string[] GuardarReserva(DataSet dsReserva)
        {
           // StringBuilder lstrSql = new StringBuilder();
           // int iConsecutivoRes = 0;
           // string pstrSql = string.Empty;
           string[] sRespuesta = new string[6];
           //string  ptblProyecto="INSERT INTO TBLPROYECTO VALUES ( ";
           //string ptblResMaster="";
           //string ptblResTransac="";
           //string ptblResPax="";
           //string ptblResFare="";
           //string ptblResFareTax = "";
           // sRespuesta[4] = "0";
           // sRespuesta[5] = "0";
           // string sMensajeReseva = "Proyecto:  ";
           // string iEstado = "0";
           // string iEmpresa = "0";
           // string iCliente = "0";
           // string iUNegocio = "0";

           // string sEstado = clsValidaciones.GetKeyOrAdd("EstadoProyectoInicial", "HK");
           // string sTipoEstado = clsValidaciones.GetKeyOrAdd("EstadoProyecto", "EstadoProyecto");

           // //tblRefere otblRefere = new tblRefere();
           // //otblRefere.Get(sTipoEstado, sEstado);
           // //if (otblRefere.Respuesta)
           // //    iEstado = otblRefere.intidRefere.Value;

           // try
           // {
           //     clsCache cCache = new csCache().cCache();
           //     if (cCache != null)
           //     {
           //         iEmpresa = cCache.Empresa.ToString();
                    
           //         try
           //         {
           //             iUNegocio = cCache.UNegocio.ToString();
           //         }
           //         catch { }
           //     }
           //     else
           //     {
           //         iEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");
           //         iCliente = clsValidaciones.GetKeyOrAdd("idAgencia", "0");
           //         iUNegocio = clsValidaciones.GetKeyOrAdd("idUNegocio", "0");
           //     }
           //     if (iEmpresa.Equals(""))
           //         iEmpresa = "0";

           //     if (iCliente.Equals(""))
           //         iCliente = "0";

           //     if (iUNegocio.Equals(""))
           //         iUNegocio = "0";
           // }
           // catch { }

           // try
           // {
           //     if (dsReserva.Tables[TABLA_RESERVA].Rows.Count > 0)
           //     {
           //         lstrSql.Append("BEGIN TRANSACTION   " + Environment.NewLine + Environment.NewLine);

           //         for (int i = 0; i < dsReserva.Tables[TABLA_RESERVA].Rows.Count; i++)
           //         {
           //             iConsecutivoRes = Int32.Parse(dsReserva.Tables[TABLA_RESERVA].Rows[i]["intConsecRes"].ToString().Trim());
           //             if (i == 0)
           //             {
                         
           //                 if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intAplicacion"].ToString().Length.Equals(0))
           //                     dsReserva.Tables[TABLA_RESERVA].Rows[i]["intAplicacion"] = "1";

           //                 if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intProyecto"].ToString().Length.Equals(0))
           //                     dsReserva.Tables[TABLA_RESERVA].Rows[i]["intProyecto"] = "0";

           //                 if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"].ToString().Length.Equals(0))
           //                     dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"] = "0";

           //                 if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"].ToString().Length.Equals(0))
           //                     dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"] = "0";

           //                 if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"].ToString().Length.Equals(0))
           //                     dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"] = "0";

           //                 //if (!dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCliente"].ToString().Length.Equals(0))
           //                 //    iCliente = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCliente"].ToString();

           //                 //ptblProyecto = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intAplicacion"].ToString();
           //                 //ptblProyecto =ptblProyecto+","+dsReserva.Tables[TABLA_RESERVA].Rows[i]["intProyecto"].ToString();
           //                 //ptblProyecto = ptblProyecto + "," + dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"].ToString();
           //                 //ptblProyecto = ptblProyecto + "," + iEmpresa;
           //                 //ptblProyecto = ptblProyecto + "," + iCliente;
           //                 //ptblProyecto = ptblProyecto + "," + iUNegocio;
           //                 //ptblProyecto = ptblProyecto + "," + dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmFechaIni"].ToString();
           //                 //ptblProyecto = ptblProyecto + "," + dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmFechaFin"].ToString();
           //                 //ptblProyecto = ptblProyecto + "," + dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmVencimiento"].ToString();
           //                 //ptblProyecto = ptblProyecto + "," + iEstado;
           //                 //ptblProyecto = ptblProyecto + "," + dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"].ToString();
           //                 //ptblProyecto = ptblProyecto + "," + dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"].ToString();

           //                 lstrSql.Append(ptblProyecto+")" + Environment.NewLine + Environment.NewLine);

           //                 sRespuesta[5] ="1000";
           //                 clsSesiones.setProyecto("1000");
           //                 sMensajeReseva += "1000" + "   Reservas:  ";
           //             }

                     
           //             if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCodigoPlan"].ToString().Length.Equals(0))
           //                 dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCodigoPlan"] = "0";

           //             if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"].ToString().Length.Equals(0))
           //                 dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"] = "0";

           //             if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstado"].ToString().Length.Equals(0))
           //                 dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstado"] = "0";

           //             if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"].ToString().Length.Equals(0))
           //                 dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"] = "0";

           //             if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"].ToString().Length.Equals(0))
           //                 dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"] = "0";

           //             if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intFormaPago"].ToString().Length.Equals(0))
           //                 dsReserva.Tables[TABLA_RESERVA].Rows[i]["intFormaPago"] = "0";

           //             if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstadoPago"].ToString().Length.Equals(0))
           //                 dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstadoPago"] = "0";

           //             if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCentroC"].ToString().Length.Equals(0))
           //                 dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCentroC"] = "0";

           //             //ptblResMaster.strReserva.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["strReserva"].ToString();
           //             //ptblResMaster.dtmFecha.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmFecha"].ToString();
           //             //ptblResMaster.dtmVencimiento.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmVencimiento"].ToString();
           //             //ptblResMaster.intCodigoPlan.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCodigoPlan"].ToString();
           //             //ptblResMaster.intContacto.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"].ToString();
           //             //ptblResMaster.intEstado.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstado"].ToString();
           //             //ptblResMaster.intResponsable.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"].ToString();
           //             //ptblResMaster.intTipoPlan.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"].ToString();
           //             //ptblResMaster.strLocalizadorExt.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["strLocalizadorExt"].ToString();
           //             //ptblResMaster.strObservacion.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["strObservacion"].ToString();
           //             //ptblResMaster.intFormaPago.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intFormaPago"].ToString();
           //             //ptblResMaster.intEstadoPago.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstadoPago"].ToString();
           //             //ptblResMaster.intCentroC.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCentroC"].ToString();
           //             //ptblResMaster.dtmFechaLimitePago.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmFechaLimitePago"].ToString();
           //             //ptblResMaster.strCodigo.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["strCodigo"].ToString();

           //             lstrSql.Append(ptblResMaster + Environment.NewLine + Environment.NewLine);
           //             if (i == 0)
           //                 sRespuesta[4] = ptblResMaster;
           //             else
           //                 sRespuesta[4] = sRespuesta[4] + ";" + ptblResMaster;

           //             sMensajeReseva += ptblResMaster + " - ";

           //             for (int j = 0; j < dsReserva.Tables[TABLA_TRANSAC].Rows.Count; j++)
           //             {
           //                 if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intConsecRes"].ToString().Trim() == dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intConsecRes"].ToString().Trim())
           //                 {
                              
           //                     if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intSegmento"].ToString().Length.Equals(0))
           //                     {
           //                         int Seg = j + 1;
           //                         dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intSegmento"] = Seg.ToString();
           //                     }

           //                     if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intOrigen"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intOrigen"] = "0";

           //                     if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intDestino"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intDestino"] = "0";

           //                     if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intCodigoPlan"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intCodigoPlan"] = "0";

           //                     if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intProveedor"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intProveedor"] = "0";

           //                     if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoPlan"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoPlan"] = "0";

           //                     if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intCantidadPersonas"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intCantidadPersonas"] = "0";

           //                     if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoAcomodacion"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoAcomodacion"] = "0";

           //                     if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoHabitacion"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoHabitacion"] = "0";

           //                     if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intEstado"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intEstado"] = "0";

           //                     //ptblResTransac.intSegmento.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intSegmento"].ToString();
           //                     //ptblResTransac.intOrigen.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intOrigen"].ToString();
           //                     //ptblResTransac.intDestino.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intDestino"].ToString();
           //                     //ptblResTransac.dtmFechaIni.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["dtmFechaIni"].ToString();
           //                     //ptblResTransac.strHoraIni.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strHoraIni"].ToString();
           //                     //ptblResTransac.dtmFechaFin.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["dtmFechaFin"].ToString();
           //                     //ptblResTransac.strHoraFin.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strHoraFin"].ToString();
           //                     //ptblResTransac.intCodigoPlan.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intCodigoPlan"].ToString();
           //                     //ptblResTransac.intProveedor.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intProveedor"].ToString();
           //                     //ptblResTransac.intTipo.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoPlan"].ToString();
           //                     //ptblResTransac.intCantidadPersonas.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intCantidadPersonas"].ToString();
           //                     //ptblResTransac.intTipoAcomodacion.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoAcomodacion"].ToString();
           //                     //ptblResTransac.intTipoHabitacion.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoHabitacion"].ToString();
           //                     //ptblResTransac.intEstado.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intEstado"].ToString();
           //                     //ptblResTransac.strOperador.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strOperador"].ToString();
           //                     //ptblResTransac.strConfirma.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strConfirma"].ToString();
           //                     //ptblResTransac.strObservacion.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strObservacion"].ToString();
           //                     //ptblResTransac.strOrigen.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strOrigen"].ToString();
           //                     //ptblResTransac.strDestino.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strDestino"].ToString();
           //                     //ptblResTransac.strCodigo.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strCodigo"].ToString();

           //                     lstrSql.Append(ptblResTransac.SaveString() + Environment.NewLine + Environment.NewLine);
                               
           //                 }
           //             }
           //             for (int k = 0; k < dsReserva.Tables[TABLA_PAX].Rows.Count; k++)
           //             {
           //                 if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intConsecRes"].ToString().Trim() == dsReserva.Tables[TABLA_PAX].Rows[k]["intConsecRes"].ToString().Trim())
           //                 {
           //                     ptblResPax.Inicialize();
           //                     ptblResPax.strReserva.Value = ptblResMaster.strReserva.Value;
           //                     if (dsReserva.Tables[TABLA_PAX].Rows[i]["intCodigoPax"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_PAX].Rows[i]["intCodigoPax"] = "0";

           //                     if (dsReserva.Tables[TABLA_PAX].Rows[i]["intTipoPax"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_PAX].Rows[i]["intTipoPax"] = "0";

           //                     if (dsReserva.Tables[TABLA_PAX].Rows[i]["intEdad"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_PAX].Rows[i]["intEdad"] = "0";

           //                     if (dsReserva.Tables[TABLA_PAX].Rows[i]["intGenero"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_PAX].Rows[i]["intGenero"] = "0";

           //                     if (dsReserva.Tables[TABLA_PAX].Rows[i]["intTipoDoc"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_PAX].Rows[i]["intTipoDoc"] = "0";

           //                     if (dsReserva.Tables[TABLA_PAX].Rows[i]["intGenero"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_PAX].Rows[i]["intGenero"] = "0";

           //                     if (dsReserva.Tables[TABLA_PAX].Rows[i]["intSegmento"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_PAX].Rows[i]["intSegmento"] = "1";

           //                     if (dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaExpPasaporte"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaExpPasaporte"] = "1900/01/01";

           //                     if (dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaNac"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaNac"] = "1900/01/01";

           //                     ptblResPax.intCodigoPax.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intCodigoPax"].ToString();
           //                     ptblResPax.intTipoPax.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intTipoPax"].ToString();
           //                     ptblResPax.strNombre.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strNombre"].ToString();
           //                     ptblResPax.dtmFechaNac.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaNac"].ToString();
           //                     ptblResPax.intEdad.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intEdad"].ToString();
           //                     ptblResPax.strCodigo.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strCodigo"].ToString();
           //                     try
           //                     {
           //                         ptblResPax.intGenero.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intGenero"].ToString();
           //                         ptblResPax.strPasaporte.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strPasaporte"].ToString();
           //                         ptblResPax.intTipoDoc.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intTipoDoc"].ToString();
           //                         ptblResPax.strViajeroFrecuente.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strViajeroFrecuente"].ToString();
           //                         ptblResPax.strNacionalidad.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strNacionalidad"].ToString();
           //                         ptblResPax.strPaisResidencia.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strPaisResidencia"].ToString();
           //                         ptblResPax.dtmFechaExpPasaporte.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaExpPasaporte"].ToString();
           //                         ptblResPax.intSegmento.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intSegmento"].ToString();
           //                         ptblResPax.strEmail.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strEmail"].ToString();
           //                         ptblResPax.strTelefono.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strTelefono"].ToString();
           //                     }
           //                     catch { }

           //                     lstrSql.Append(ptblResPax.SaveString() + Environment.NewLine + Environment.NewLine);
           //                 }
           //             }
           //             for (int h = 0; h < dsReserva.Tables[TABLA_TARIFA].Rows.Count; h++)
           //             {
           //                 if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intConsecRes"].ToString().Trim() == dsReserva.Tables[TABLA_TARIFA].Rows[h]["intConsecRes"].ToString().Trim())
           //                 {
           //                     ptblResFare.Inicialize();
           //                     ptblResFare.strReserva.Value = ptblResMaster.strReserva.Value;
           //                     if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["intCodigFare"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TARIFA].Rows[i]["intCodigFare"] = "0";

           //                     if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["intTipoPax"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TARIFA].Rows[i]["intTipoPax"] = "0";

           //                     if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["intMoneda"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TARIFA].Rows[i]["intMoneda"] = "0";

           //                     if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblValor"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblValor"] = "0";

           //                     if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblTotal"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblTotal"] = "0";

           //                     if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblTax"].ToString().Length.Equals(0))
           //                         dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblTax"] = "0";

           //                     ptblResFare.intCodigFare.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["intCodigFare"].ToString();
           //                     ptblResFare.intTipoPax.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["intTipoPax"].ToString();
           //                     ptblResFare.intMoneda.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["intMoneda"].ToString();
           //                     ptblResFare.dblValor.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblValor"].ToString();
           //                     ptblResFare.dblTotal.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblTotal"].ToString();
           //                     ptblResFare.dblDescuento.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblDescuento"].ToString();
           //                     ptblResFare.dblTax.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblTax"].ToString();
           //                     ptblResFare.strCodigo.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["strCodigo"].ToString();

           //                     lstrSql.Append(ptblResFare.SaveString() + Environment.NewLine + Environment.NewLine);

           //                     for (int m = 0; m < dsReserva.Tables[TABLA_TAXFARE].Rows.Count; m++)
           //                     {
           //                         if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intConsecRes"].ToString().Trim() == dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intConsecRes"].ToString().Trim()
           //                             && dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intTipoPax"].ToString().Trim() == dsReserva.Tables[TABLA_TARIFA].Rows[h]["intTipoPax"].ToString().Trim())
           //                         {
           //                             ptblResFareTax.Inicialize();
           //                             ptblResFareTax.intCodigoFare.Value = ptblResFare.intCodigFare.Value;
           //                             if (dsReserva.Tables[TABLA_TAXFARE].Rows[i]["intCodigoTax"].ToString().Length.Equals(0))
           //                                 dsReserva.Tables[TABLA_TAXFARE].Rows[i]["intCodigoTax"] = "0";

           //                             if (dsReserva.Tables[TABLA_TAXFARE].Rows[i]["intMoneda"].ToString().Length.Equals(0))
           //                                 dsReserva.Tables[TABLA_TAXFARE].Rows[i]["intMoneda"] = "0";

           //                             if (dsReserva.Tables[TABLA_TAXFARE].Rows[i]["dblPorcent"].ToString().Length.Equals(0))
           //                                 dsReserva.Tables[TABLA_TAXFARE].Rows[i]["dblPorcent"] = "0";

           //                             if (dsReserva.Tables[TABLA_TAXFARE].Rows[i]["dblValorTax"].ToString().Length.Equals(0))
           //                                 dsReserva.Tables[TABLA_TAXFARE].Rows[i]["dblValorTax"] = "0";

           //                             ptblResFareTax.intCodigoTax.Value = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intCodigoTax"].ToString();
           //                             ptblResFareTax.intMoneda.Value = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intMoneda"].ToString();
           //                             ptblResFareTax.dblPorcent.Value = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["dblPorcent"].ToString();
           //                             ptblResFareTax.dblValorTax.Value = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["dblValorTax"].ToString();
           //                             ptblResFareTax.strCodigo.Value = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["strCodigo"].ToString();

           //                             lstrSql.Append(ptblResFareTax.SaveString() + Environment.NewLine + Environment.NewLine);
           //                         }
           //                     }
           //                 }
           //             }
           //         }
           //         lstrSql.Append("COMMIT TRANSACTION   " + Environment.NewLine + Environment.NewLine);
           //         //Utils.Utils iprime=new Utils.Utils();
           //         //iprime.CrearHTML("D://Inserts//inset", lstrSql.ToString());

           //         ExceptionHandled.Publicar(sMensajeReseva + "SQL ejecutado " + lstrSql.ToString());

           //         pclsDataSql.UpdateInsert(lstrSql.ToString());
           //         sRespuesta[0] = "1";
           //         sRespuesta[1] = "";
           //         sRespuesta[2] = this.ToString();
           //         sRespuesta[3] = this.ToString();
           //         try
           //         {
           //             clsSesiones.setProyecto(ptblProyecto.intProyecto.Value);
           //             clsCache cCache = new csCache().cCache();
           //             clsCacheControl cCacheControl = new clsCacheControl();
           //             cCache.Proyecto = ptblProyecto.intProyecto.Value;
           //             cCacheControl.ActualizaXML(cCache);
           //             try
           //             {
           //                 if (clsValidaciones.GetKeyOrAdd("GeneraXMLReserva", "False").ToUpper().Equals("TRUE"))
           //                 {
           //                     dsReserva.DataSetName = "dsReserva";
           //                     string sXml = dsReserva.GetXml();
           //                     string sXmlScheme = dsReserva.GetXmlSchema();

           //                     clsSerializer csSerializer = new clsSerializer();
           //                     csSerializer.SaveXML("XmlReserva", sXml);
           //                     csSerializer.SaveXSD("XsdReservaScheme", sXmlScheme);
           //                 }
           //             }
           //             catch { }
           //         }
           //         catch { }
           //         return sRespuesta;
           //     }
           //     else
           //     {
           //         sRespuesta[0] = "2";
           //         sRespuesta[1] = "";
           //         sRespuesta[2] = this.ToString();
           //         sRespuesta[3] = this.ToString();
           //         return sRespuesta;
           //     }
           // }
           // catch (Exception Ex)
           // {
           //     ExceptionHandled.Publicar("Error generado, SQL ejecutado " + lstrSql.ToString());
           //     ExceptionHandled.Publicar(Ex);

           //     sRespuesta[0] = "0";
           //     sRespuesta[1] = Ex.Message.ToString();
           //     sRespuesta[2] = Ex.Source.ToString();
           //     sRespuesta[3] = Ex.StackTrace.ToString();
           //     sRespuesta[4] = "0";
           //     return sRespuesta;
           // }

           return sRespuesta;
        }


        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="dsReserva"></param>
        /// <returns></returns>
        public clsMensajes GuardaReserva(DataSet dsReserva)
        {
            //StringBuilder lstrSql = new StringBuilder();
            //int iConsecutivoRes = 0;
            //string pstrSql = string.Empty;
            clsMensajes cParametros = new clsMensajes();
            //cParametros.DatoAdic = "0";
            //ptblProyecto.Conexion = this.Conexion;
            //ptblResMaster.Conexion = this.Conexion;
            //ptblResTransac.Conexion = this.Conexion;
            //ptblResPax.Conexion = this.Conexion;
            //ptblResFare.Conexion = this.Conexion;
            //ptblResFareTax.Conexion = this.Conexion;
            //string sMensajeReseva = "Proyecto:  ";
            //string iEstado = "0";
            //string iEmpresa = "0";
            //string iCliente = "0";
            //string iUNegocio = "0";

            //string sEstado = clsValidaciones.GetKeyOrAdd("EstadoProyectoInicial", "HK");
            //string sTipoEstado = clsValidaciones.GetKeyOrAdd("EstadoProyecto", "EstadoProyecto");

            ////tblRefere otblRefere = new tblRefere();
            ////otblRefere.Get(sTipoEstado, sEstado);
            ////if (otblRefere.Respuesta)
            ////    iEstado = otblRefere.intidRefere.Value;

            //try
            //{
            //    clsCache cCache = new csCache().cCache();
            //    if (cCache != null)
            //    {
            //        iEmpresa = cCache.Empresa.ToString();
                   
            //        try
            //        {
            //            if (cCache.UNegocio != null)
            //                iUNegocio = cCache.UNegocio.ToString();
            //        }
            //        catch { }
            //    }
            //    else
            //    {
            //        iEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");
            //        iCliente = clsValidaciones.GetKeyOrAdd("idAgencia", "0");
            //        iUNegocio = clsValidaciones.GetKeyOrAdd("idUNegocio", "0");
            //    }
            //}
            //catch { }

            //try
            //{
            //    if (dsReserva.Tables[TABLA_RESERVA].Rows.Count > 0)
            //    {
            //        lstrSql.Append("BEGIN TRANSACTION   " + Environment.NewLine + Environment.NewLine);

            //        for (int i = 0; i < dsReserva.Tables[TABLA_RESERVA].Rows.Count; i++)
            //        {
            //            iConsecutivoRes = Int32.Parse(dsReserva.Tables[TABLA_RESERVA].Rows[i]["intConsecRes"].ToString().Trim());
            //            if (i == 0)
            //            {
            //                ptblProyecto.Inicialize();
            //                if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intAplicacion"].ToString().Length.Equals(0))
            //                    dsReserva.Tables[TABLA_RESERVA].Rows[i]["intAplicacion"] = "1";

            //                if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intProyecto"].ToString().Length.Equals(0))
            //                    dsReserva.Tables[TABLA_RESERVA].Rows[i]["intProyecto"] = "0";

            //                if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"].ToString().Length.Equals(0))
            //                    dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"] = "0";

            //                if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"].ToString().Length.Equals(0))
            //                    dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"] = "0";

            //                if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"].ToString().Length.Equals(0))
            //                    dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"] = "0";

            //                //if (!dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCliente"].ToString().Length.Equals(0))
            //                //    iCliente = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCliente"].ToString();

            //                ptblProyecto.intAplicacion.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intAplicacion"].ToString();
            //                ptblProyecto.intProyecto.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intProyecto"].ToString();
            //                ptblProyecto.intContacto.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"].ToString();
            //                ptblProyecto.intEmpresa.Value = iEmpresa;
            //                ptblProyecto.intCliente.Value = iCliente;
            //                ptblProyecto.intUNegocio.Value = iUNegocio;
            //                ptblProyecto.dtmFechaIni.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmFechaIni"].ToString();
            //                ptblProyecto.dtmFechaFin.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmFechaFin"].ToString();
            //                ptblProyecto.dtmVencimiento.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmVencimiento"].ToString();
            //                ptblProyecto.intEstado.Value = iEstado;
            //                ptblProyecto.intResponsable.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"].ToString();
            //                ptblProyecto.intTipo.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"].ToString();

            //                lstrSql.Append(ptblProyecto.SaveString() + Environment.NewLine + Environment.NewLine);
            //                cParametros.DatoAdicArr = ptblProyecto.intProyecto.Value;
            //                sMensajeReseva += ptblProyecto.intProyecto.Value + "   Reservas:  ";
            //            }
            //            ptblResMaster.Inicialize();
            //            ptblResMaster.intProyecto.Value = ptblProyecto.intProyecto.Value;
            //            if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCodigoPlan"].ToString().Length.Equals(0))
            //                dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCodigoPlan"] = "0";

            //            if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"].ToString().Length.Equals(0))
            //                dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"] = "0";

            //            if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstado"].ToString().Length.Equals(0))
            //                dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstado"] = "0";

            //            if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"].ToString().Length.Equals(0))
            //                dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"] = "0";

            //            if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"].ToString().Length.Equals(0))
            //                dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"] = "0";

            //            if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intFormaPago"].ToString().Length.Equals(0))
            //                dsReserva.Tables[TABLA_RESERVA].Rows[i]["intFormaPago"] = "0";

            //            if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstadoPago"].ToString().Length.Equals(0))
            //                dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstadoPago"] = "0";

            //            if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCentroC"].ToString().Length.Equals(0))
            //                dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCentroC"] = "0";

            //            ptblResMaster.strReserva.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["strReserva"].ToString();
            //            ptblResMaster.dtmFecha.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmFecha"].ToString();
            //            ptblResMaster.dtmVencimiento.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmVencimiento"].ToString();
            //            ptblResMaster.intCodigoPlan.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCodigoPlan"].ToString();
            //            ptblResMaster.intContacto.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intContacto"].ToString();
            //            ptblResMaster.intEstado.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstado"].ToString();
            //            ptblResMaster.intResponsable.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intResponsable"].ToString();
            //            ptblResMaster.intTipoPlan.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intTipoPlan"].ToString();
            //            ptblResMaster.strLocalizadorExt.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["strLocalizadorExt"].ToString();
            //            ptblResMaster.strObservacion.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["strObservacion"].ToString();
            //            ptblResMaster.intFormaPago.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intFormaPago"].ToString();
            //            ptblResMaster.intEstadoPago.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intEstadoPago"].ToString();
            //            ptblResMaster.dtmFechaLimitePago.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["dtmFechaLimitePago"].ToString();
            //            ptblResMaster.intCentroC.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["intCentroC"].ToString();
            //            ptblResMaster.strCodigo.Value = dsReserva.Tables[TABLA_RESERVA].Rows[i]["strCodigo"].ToString();

            //            lstrSql.Append(ptblResMaster.SaveString() + Environment.NewLine + Environment.NewLine);

            //            if (i == 0)
            //                cParametros.DatoAdic = ptblResMaster.strReserva.Value;
            //            else
            //                cParametros.DatoAdic = cParametros.DatoAdic + ";" + ptblResMaster.strReserva.Value;

            //            sMensajeReseva += ptblResMaster.strReserva.Value + " - ";

            //            for (int j = 0; j < dsReserva.Tables[TABLA_TRANSAC].Rows.Count; j++)
            //            {
            //                if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intConsecRes"].ToString().Trim() == dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intConsecRes"].ToString().Trim())
            //                {
            //                    ptblResTransac.Inicialize();
            //                    ptblResTransac.strReserva.Value = ptblResMaster.strReserva.Value;
            //                    if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intSegmento"].ToString().Length.Equals(0))
            //                    {
            //                        int Seg = j + 1;
            //                        dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intSegmento"] = Seg.ToString();
            //                    }

            //                    if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intOrigen"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intOrigen"] = "0";

            //                    if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intDestino"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intDestino"] = "0";

            //                    if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intCodigoPlan"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intCodigoPlan"] = "0";

            //                    if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intProveedor"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intProveedor"] = "0";

            //                    if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoPlan"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoPlan"] = "0";

            //                    if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intCantidadPersonas"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intCantidadPersonas"] = "0";

            //                    if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoAcomodacion"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoAcomodacion"] = "0";

            //                    if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoHabitacion"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intTipoHabitacion"] = "0";

            //                    if (dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intEstado"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TRANSAC].Rows[i]["intEstado"] = "0";

            //                    ptblResTransac.intSegmento.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intSegmento"].ToString();
            //                    ptblResTransac.intOrigen.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intOrigen"].ToString();
            //                    ptblResTransac.intDestino.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intDestino"].ToString();
            //                    ptblResTransac.dtmFechaIni.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["dtmFechaIni"].ToString();
            //                    ptblResTransac.strHoraIni.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strHoraIni"].ToString();
            //                    ptblResTransac.dtmFechaFin.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["dtmFechaFin"].ToString();
            //                    ptblResTransac.strHoraFin.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strHoraFin"].ToString();
            //                    ptblResTransac.intCodigoPlan.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intCodigoPlan"].ToString();
            //                    ptblResTransac.intProveedor.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intProveedor"].ToString();
            //                    ptblResTransac.intTipo.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoPlan"].ToString();
            //                    ptblResTransac.intCantidadPersonas.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intCantidadPersonas"].ToString();
            //                    ptblResTransac.intTipoAcomodacion.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoAcomodacion"].ToString();
            //                    ptblResTransac.intTipoHabitacion.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoHabitacion"].ToString();
            //                    ptblResTransac.intEstado.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intEstado"].ToString();
            //                    ptblResTransac.strOperador.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strOperador"].ToString();
            //                    ptblResTransac.strConfirma.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strConfirma"].ToString();
            //                    ptblResTransac.strObservacion.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strObservacion"].ToString();
            //                    ptblResTransac.strOrigen.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strOrigen"].ToString();
            //                    ptblResTransac.strDestino.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strDestino"].ToString();
            //                    ptblResTransac.strCodigo.Value = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strCodigo"].ToString();

            //                    lstrSql.Append(ptblResTransac.SaveString() + Environment.NewLine + Environment.NewLine);
            //                    //if (j == 0)
            //                    //{
            //                    //    try
            //                    //    {
            //                    //        GuardarLogReserva(ptblResMaster.strReserva.Value, int.Parse(ptblResMaster.intTipoPlan.Value), int.Parse(ptblResMaster.intCodigoPlan.Value), ptblResTransac.intOrigen.Value, ptblResTransac.intDestino.Value, ptblResTransac.dtmFechaIni.Value);
            //                    //    }
            //                    //    catch (Exception Ex)
            //                    //    {
            //                    //    }
            //                    //}
            //                }
            //            }
            //            for (int k = 0; k < dsReserva.Tables["tblPax"].Rows.Count; k++)
            //            {
            //                if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intConsecRes"].ToString().Trim() == dsReserva.Tables["tblPax"].Rows[k]["intConsecRes"].ToString().Trim())
            //                {
            //                    ptblResPax.Inicialize();
            //                    ptblResPax.strReserva.Value = ptblResMaster.strReserva.Value;
            //                    if (dsReserva.Tables[TABLA_PAX].Rows[i]["intCodigoPax"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_PAX].Rows[i]["intCodigoPax"] = "0";

            //                    if (dsReserva.Tables[TABLA_PAX].Rows[i]["intTipoPax"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_PAX].Rows[i]["intTipoPax"] = "0";

            //                    if (dsReserva.Tables[TABLA_PAX].Rows[i]["intEdad"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_PAX].Rows[i]["intEdad"] = "0";

            //                    if (dsReserva.Tables[TABLA_PAX].Rows[i]["intGenero"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_PAX].Rows[i]["intGenero"] = "0";

            //                    if (dsReserva.Tables[TABLA_PAX].Rows[i]["intTipoDoc"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_PAX].Rows[i]["intTipoDoc"] = "0";

            //                    if (dsReserva.Tables[TABLA_PAX].Rows[i]["intGenero"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_PAX].Rows[i]["intGenero"] = "0";

            //                    if (dsReserva.Tables[TABLA_PAX].Rows[i]["intSegmento"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_PAX].Rows[i]["intSegmento"] = "1";

            //                    if (dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaExpPasaporte"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaExpPasaporte"] = "1900/01/01";

            //                    if (dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaNac"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaNac"] = "1900/01/01";

            //                    ptblResPax.intCodigoPax.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intCodigoPax"].ToString();
            //                    ptblResPax.intTipoPax.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intTipoPax"].ToString();
            //                    ptblResPax.strNombre.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strNombre"].ToString();
            //                    ptblResPax.dtmFechaNac.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaNac"].ToString();
            //                    ptblResPax.intEdad.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intEdad"].ToString();
            //                    ptblResPax.strCodigo.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strCodigo"].ToString();
            //                    try
            //                    {
            //                        ptblResPax.intGenero.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intGenero"].ToString();
            //                        ptblResPax.strPasaporte.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strPasaporte"].ToString();
            //                        ptblResPax.intTipoDoc.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intTipoDoc"].ToString();
            //                        ptblResPax.strViajeroFrecuente.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strViajeroFrecuente"].ToString();
            //                        ptblResPax.strNacionalidad.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strNacionalidad"].ToString();
            //                        ptblResPax.strPaisResidencia.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strPaisResidencia"].ToString();
            //                        ptblResPax.dtmFechaExpPasaporte.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaExpPasaporte"].ToString();
            //                        ptblResPax.intSegmento.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["intSegmento"].ToString();
            //                        ptblResPax.strEmail.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strEmail"].ToString();
            //                        ptblResPax.strTelefono.Value = dsReserva.Tables[TABLA_PAX].Rows[k]["strTelefono"].ToString();
            //                    }
            //                    catch { }
            //                    lstrSql.Append(ptblResPax.SaveString() + Environment.NewLine + Environment.NewLine);
            //                }
            //            }
            //            for (int h = 0; h < dsReserva.Tables[TABLA_TARIFA].Rows.Count; h++)
            //            {
            //                if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intConsecRes"].ToString().Trim() == dsReserva.Tables[TABLA_TARIFA].Rows[h]["intConsecRes"].ToString().Trim())
            //                {
            //                    ptblResFare.Inicialize();
            //                    ptblResFare.strReserva.Value = ptblResMaster.strReserva.Value;
            //                    if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["intCodigFare"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TARIFA].Rows[i]["intCodigFare"] = "0";

            //                    if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["intTipoPax"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TARIFA].Rows[i]["intTipoPax"] = "0";

            //                    if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["intMoneda"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TARIFA].Rows[i]["intMoneda"] = "0";

            //                    if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblValor"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblValor"] = "0";

            //                    if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblTotal"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblTotal"] = "0";

            //                    if (dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblTax"].ToString().Length.Equals(0))
            //                        dsReserva.Tables[TABLA_TARIFA].Rows[i]["dblTax"] = "0";

            //                    ptblResFare.intCodigFare.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["intCodigFare"].ToString();
            //                    ptblResFare.intTipoPax.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["intTipoPax"].ToString();
            //                    ptblResFare.intMoneda.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["intMoneda"].ToString();
            //                    ptblResFare.dblValor.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblValor"].ToString();
            //                    ptblResFare.dblTotal.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblTotal"].ToString();
            //                    ptblResFare.dblTax.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblTax"].ToString();
            //                    ptblResFare.strCodigo.Value = dsReserva.Tables[TABLA_TARIFA].Rows[h]["strCodigo"].ToString();

            //                    lstrSql.Append(ptblResFare.SaveString() + Environment.NewLine + Environment.NewLine);

            //                    for (int m = 0; m < dsReserva.Tables[TABLA_TAXFARE].Rows.Count; m++)
            //                    {
            //                        if (dsReserva.Tables[TABLA_RESERVA].Rows[i]["intConsecRes"].ToString().Trim() == dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intConsecRes"].ToString().Trim()
            //                            && dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intTipoPax"].ToString().Trim() == dsReserva.Tables[TABLA_TARIFA].Rows[h]["intTipoPax"].ToString().Trim())
            //                        {
            //                            ptblResFareTax.Inicialize();
            //                            ptblResFareTax.intCodigoFare.Value = ptblResFare.intCodigFare.Value;
            //                            if (dsReserva.Tables[TABLA_TAXFARE].Rows[i]["intCodigoTax"].ToString().Length.Equals(0))
            //                                dsReserva.Tables[TABLA_TAXFARE].Rows[i]["intCodigoTax"] = "0";

            //                            if (dsReserva.Tables[TABLA_TAXFARE].Rows[i]["intMoneda"].ToString().Length.Equals(0))
            //                                dsReserva.Tables[TABLA_TAXFARE].Rows[i]["intMoneda"] = "0";

            //                            if (dsReserva.Tables[TABLA_TAXFARE].Rows[i]["dblPorcent"].ToString().Length.Equals(0))
            //                                dsReserva.Tables[TABLA_TAXFARE].Rows[i]["dblPorcent"] = "0";

            //                            if (dsReserva.Tables[TABLA_TAXFARE].Rows[i]["dblValorTax"].ToString().Length.Equals(0))
            //                                dsReserva.Tables[TABLA_TAXFARE].Rows[i]["dblValorTax"] = "0";

            //                            ptblResFareTax.intCodigoTax.Value = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intCodigoTax"].ToString();
            //                            ptblResFareTax.intMoneda.Value = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intMoneda"].ToString();
            //                            ptblResFareTax.dblPorcent.Value = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["dblPorcent"].ToString();
            //                            ptblResFareTax.dblValorTax.Value = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["dblValorTax"].ToString();
            //                            ptblResFareTax.strCodigo.Value = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["strCodigo"].ToString();

            //                            lstrSql.Append(ptblResFareTax.SaveString() + Environment.NewLine + Environment.NewLine);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        lstrSql.Append("COMMIT TRANSACTION   " + Environment.NewLine + Environment.NewLine);

            //        ExceptionHandled.Publicar(sMensajeReseva + "SQL ejecutado " + lstrSql.ToString());

            //        pclsDataSql.UpdateInsert(lstrSql.ToString());
            //        cParametros.id = 1;
            //        cParametros.Mensaje = "Los datos de la reserva se guardaron exitosamente";
            //        cParametros.Source = this.ToString();
            //        cParametros.StackTrace = this.ToString();
            //        try
            //        {
            //            clsSesiones.setProyecto(ptblProyecto.intProyecto.Value);
            //            clsCache cCache = new csCache().cCache();
            //            clsCacheControl cCacheControl = new clsCacheControl();
            //            cCache.Proyecto = ptblProyecto.intProyecto.Value;
            //            cCacheControl.ActualizaXML(cCache);
            //            try
            //            {
            //                if (clsValidaciones.GetKeyOrAdd("GeneraXMLReserva", "False").ToUpper().Equals("TRUE"))
            //                {
            //                    dsReserva.DataSetName = "dsReserva";
            //                    string sXml = dsReserva.GetXml();
            //                    string sXmlScheme = dsReserva.GetXmlSchema();

            //                    clsSerializer csSerializer = new clsSerializer();
            //                    csSerializer.SaveXML("XmlReserva", sXml);
            //                    csSerializer.SaveXSD("XmlReservaScheme", sXmlScheme);
            //                }
            //            }
            //            catch { }
            //        }
            //        catch { }
            //        return cParametros;
            //    }
            //    else
            //    {
            //        cParametros.id = 2;
            //        cParametros.Mensaje = "";
            //        cParametros.Source = this.ToString();
            //        cParametros.StackTrace = this.ToString();
            //        cParametros.Complemento = lstrSql.ToString();
            //        cParametros.DatoAdic = "0";
            //        return cParametros;
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    ExceptionHandled.Publicar("Error generado, SQL ejecutado " + lstrSql.ToString());
            //    ExceptionHandled.Publicar(Ex);

            //    cParametros.id = 0;
            //    cParametros.Mensaje = Ex.Message.ToString();
            //    cParametros.Source = Ex.Source.ToString();
            //    cParametros.StackTrace = Ex.StackTrace.ToString();
            //    cParametros.Complemento = lstrSql.ToString();
            //    cParametros.Sugerencia = "";
            //    return cParametros;
            //}

            return cParametros;
        }

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="dsReserva"></param>
        /// <returns></returns>
        public clsParametros GuardaReservaGen(DataSet dsReserva)
        {
            clsCache cCache = new csCache().cCache();
            clsParametros cParametros = new clsParametros();
            string sreservas = string.Empty;

            if (dsReserva != null)
            {
                if (dsReserva.Tables.Count > 0)
                {
                    string CadenaInsertTblResMaster = string.Empty;
                    string CadenaInsertTblproyecto = string.Empty;
                    string CadenaIsertTblTransac = string.Empty;
                    string sCadenaIsertTblTransac = string.Empty;
                    string CadenaIsertTblPax = string.Empty;
                    string sCadenaIsertTblPax = string.Empty;
                    string bMaster = string.Empty;
                    string CadenaIsertTblFare = string.Empty;
                    string sCadenaIsertTblFare = string.Empty;
                    string CadenaIsertTblFareTax = string.Empty;
                    string sCadenaIsertTblFareTax = string.Empty;
                    string bProyecto = string.Empty;
                    string bTransac = string.Empty;
                    string bValidaPax = string.Empty;
                    #region CamposTablaProyecto
                    string sEstado = clsValidaciones.GetKeyOrAdd("EstadoProyectoInicial", "HK");
                    string intAplicacion = string.Empty;
                    string intEmpresa = string.Empty;
                    string intTipo = string.Empty;
                    string intContacto = string.Empty;
                    string intCliente = string.Empty;
                    string intResponsable = string.Empty;
                    string dtmFechaIni = string.Empty;
                    string dtmFechaFin = string.Empty;
                    string dtmVencimiento = string.Empty;
                    string intEstado = string.Empty;
                    string strDescripcion = string.Empty;
                    string intResponsable1 = string.Empty;
                    string intTipoReserva = string.Empty;
                    string intTipoViaje = string.Empty;
                    string dtmFechaSolicitud = string.Empty;
                    string strObservaciones = string.Empty;
                    string dtmCFecha = string.Empty;
                    string dtmMFecha = string.Empty;
                    #endregion
                    #region validaciones
                    intAplicacion = clsSesiones.getAplicacion().ToString();
                    if (cCache != null)
                    {
                        try
                        {
                            if (!cCache.Empresa.ToString().Length.Equals(0) && cCache.Empresa.ToString() != "")
                            {
                                intEmpresa = cCache.Empresa.ToString();
                            }
                            else
                            {
                                intEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");
                            }
                        }
                        catch { }
                    }

                    if (dsReserva.Tables[TABLA_RESERVA].Rows[0]["intProyecto"].ToString().Length.Equals(0))
                        dsReserva.Tables[TABLA_RESERVA].Rows[0]["intProyecto"] = "0";

                    if (dsReserva.Tables[TABLA_RESERVA].Rows[0]["intResponsable"].ToString().Length.Equals(0))
                        dsReserva.Tables[TABLA_RESERVA].Rows[0]["intResponsable"] = "0";

                    if (dsReserva.Tables[TABLA_RESERVA].Rows[0]["intTipoPlan"].ToString().Length.Equals(0))
                        dsReserva.Tables[TABLA_RESERVA].Rows[0]["intTipoPlan"] = "0";

                    if (dsReserva.Tables[TABLA_RESERVA].Rows[0]["intContacto"].ToString().Length.Equals(0))
                        dsReserva.Tables[TABLA_RESERVA].Rows[0]["intContacto"] = "0";

                    if (dsReserva.Tables[TABLA_RESERVA].Rows[0]["intAplicacion"].ToString() != "0" && dsReserva.Tables[TABLA_RESERVA].Rows[0]["intAplicacion"].ToString() != "")
                    {
                        intAplicacion = dsReserva.Tables[TABLA_RESERVA].Rows[0]["intAplicacion"].ToString();
                    }
                    #endregion
                    #region asignaciones
                    intContacto = dsReserva.Tables[TABLA_RESERVA].Rows[0]["intContacto"].ToString();

                    intCliente = intEmpresa;
                    dtmFechaIni = dsReserva.Tables[TABLA_RESERVA].Rows[0]["dtmFechaIni"].ToString();
                    dtmFechaFin = dsReserva.Tables[TABLA_RESERVA].Rows[0]["dtmFechaFin"].ToString();
                    dtmVencimiento = dsReserva.Tables[TABLA_RESERVA].Rows[0]["dtmVencimiento"].ToString();
                    intEstado = new CsConsultasVuelos().ConsultaCodigo(sEstado, "TBLESTADOPROYECTO", "INTESTADO", "STRCODE");
                    intResponsable = dsReserva.Tables[TABLA_RESERVA].Rows[0]["intResponsable"].ToString();
                    dtmFechaSolicitud = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString();
                    intTipo = "1";
                    strDescripcion = "Ning";
                    intResponsable1 = "0";
                    intTipoReserva = "0";
                    intTipoViaje = "0";
                    strObservaciones = "Ning";

                    string dtmFechaCreacion = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString();
                    string dtmMFechaCreacion = dtmFechaCreacion;
                    string bValida = string.Empty;
                    string bValidaFare = string.Empty;

                    #endregion

                    CadenaInsertTblproyecto = intAplicacion + "," + intEmpresa + "," + intTipo + "," + intContacto + "," + intCliente + "," + intResponsable + "," + "'" + dtmFechaIni + "'" + "," + "'" + dtmFechaFin + "'" + "," + "'" + dtmVencimiento + "'" + "," + intEstado + "," + "'" + strDescripcion + "'" + "," + intResponsable1 + "," + intTipoReserva + "," + intTipoViaje + "," + "'" + dtmFechaSolicitud + "'" + "," + "'" + strObservaciones + "'" + "," + "'" + dtmFechaCreacion + "'" + "," + "'" + dtmMFechaCreacion + "'";

                    try
                    {

                        bProyecto = new CsConsultasVuelos().EjecutarSPConsulta("SPInsertarProyecto", new string[18] { intAplicacion, intEmpresa, intTipo, intContacto, intCliente, intResponsable, "'" + dtmFechaIni + "'", "'" + dtmFechaFin + "'", "'" + dtmVencimiento + "'", intEstado, "'" + strDescripcion + "'", intResponsable1, intTipoReserva, intTipoViaje, "'" + dtmFechaSolicitud + "'", "'" + strObservaciones + "'", "'" + dtmFechaCreacion + "'", "'" + dtmMFechaCreacion + "'" });
                        //bProyecto = new csGenerales().ConsultarCodigos(CadenaInsertTblproyecto);
                        if (bProyecto != "0" && bProyecto != null)
                        {
                            clsSesiones.setProyecto(bProyecto);
                        }
                        else
                        {
                            ExceptionHandled.Publicar("Problema en Insercion de la tblproyecto:" + CadenaInsertTblproyecto);
                        }


                    }
                    catch
                    {
                        ExceptionHandled.Publicar("Problema en Insercion de la tblproyecto:" + CadenaInsertTblproyecto);
                    }


                    #region CamposTablaResmarter y asignaciones
                    ///Se Comentarean datos que no se llevan en el momento  tanto en codigo como en el procedimientoalmacenado
                    string intProyecto = string.Empty;
                    sreservas = dsReserva.Tables[TABLA_RESERVA].Rows[0]["strReserva"].ToString();
                    string strReserva = dsReserva.Tables[TABLA_RESERVA].Rows[0]["strReserva"].ToString();
                    if (strReserva == null || strReserva == "" || strReserva == "0")
                        strReserva = GetCodigoReservaPlan(dsReserva.Tables["CarritoCompras"].Rows[0]["strTipoPlan"].ToString());
                    string intTipoServicio = dsReserva.Tables[TABLA_RESERVA].Rows[0]["intTipoPlan"].ToString();
                    string intCodigoPlan = "0";
                    if (dsReserva.Tables[TABLA_CARRITO].Rows[0]["StrIdentificadorDelPlan"].ToString() != "CIRC" && dsReserva.Tables[TABLA_CARRITO].Rows[0]["StrIdentificadorDelPlan"].ToString() != "EXC")
                        intCodigoPlan = dsReserva.Tables[TABLA_RESERVA].Rows[0]["intTipoPlan"].ToString();
                    else
                        intCodigoPlan = dsReserva.Tables[TABLA_RESERVA].Rows[0]["intCodigoPlan"].ToString();

                    string dtmFechaCreacionMaster = dsReserva.Tables[TABLA_RESERVA].Rows[0]["dtmFecha"].ToString();
                    string dtmVencimientoMaster = dsReserva.Tables[TABLA_RESERVA].Rows[0]["dtmVencimiento"].ToString();
                    string intEstadoMaster = dsReserva.Tables[TABLA_RESERVA].Rows[0]["intEstado"].ToString();
                    string intContactoMaster = dsReserva.Tables[TABLA_RESERVA].Rows[0]["intContacto"].ToString();
                    string intPcc = "0";
                    string strPcc = "0";
                    string intFormaPago = dsReserva.Tables[TABLA_RESERVA].Rows[0]["intFormaPago"].ToString();
                    string dtmFechaLimitePago = dsReserva.Tables[TABLA_RESERVA].Rows[0]["dtmFechaLimitePago"].ToString();
                    string intEstadoPago = dsReserva.Tables[TABLA_RESERVA].Rows[0]["intEstadoPago"].ToString();
                    if (intEstadoPago.Equals("0") || intEstadoPago.Equals(""))
                    {
                        string strEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPagoPendiente", "PP");
                        intEstadoPago = new CsConsultasVuelos().ConsultaCodigo(strEstadoPago, "tblEstadosPago", "intCodigo", "strCode");
                        if (intEstadoPago == "" || intEstadoPago == null)
                        {
                            intEstadoPago = "1";
                        }
                    }
                    //string strMotivoCancel=" ";
                    //string dtmFechaCancel="1900/01/01";
                    //string dblValorCancel="0";
                    //string intResponsableCancel="0";
                    string strObservacion = dsReserva.Tables[TABLA_RESERVA].Rows[0]["strObservacion"].ToString(); ;
                    string strLocalizadorExt = "0";
                    //string strTansaccionPOL=" ";
                    //string strAutorizacionPOL=" ";
                    string intOrdenServicio = "0";
                    //string strNumFactura=" ";
                    //string strReciboCaja=" ";
                    //string dblBaseComisionable="0";
                    //string dblComisionTarifa="0";
                    //string intEstadoPagoProv="0";
                    //string dtmFechaPagoProv="";
                    //string intMotivoCancel="0";
                    //string intEstadoPagoComision="0";
                    //string dtmFechaPagoComision="1900/01/01";
                    //string intIdVoucher="0";
                    //string dtmFechavoucher="1900/01/01";
                    //string intContactoVoucher="0";
                    //string strVoucher=" ";
                    //string intImpresion="0";
                    string strDuracion = dsReserva.Tables[TABLA_RESERVA].Rows[0]["strDuracion"].ToString();
                    string dtmMFechaMaster = dsReserva.Tables[TABLA_RESERVA].Rows[0]["dtmFecha"].ToString();
                    string intContactoUser = "0";
                    string strCodigoAsesor = dsReserva.Tables[TABLA_RESERVA].Rows[0]["strCodigoAscesor"].ToString();
                    if (intEstadoMaster.Equals("0") && intTipoServicio.Equals("1"))
                    {
                        string strEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPagoPendiente", "PP");
                        intEstadoMaster = new CsConsultasVuelos().ConsultaCodigo(strEstadoPago, "tblEstadosPago", "intCodigo", "strCode");
                        if (intEstadoMaster == "" || intEstadoMaster == null)
                        {
                            intEstadoMaster = "0";
                        }

                    }
                    else if (intEstadoMaster.Equals("") && intTipoServicio.Equals("3"))
                    {
                        intEstadoMaster = "1";
                    }

                    #endregion
                    intProyecto = bProyecto;

                    //CadenaInsertTblResMaster="EXEC SPTBLRESMASTER "+balida+",'"+strReserva+"',"+intTipoServicio+","+intCodigoPlan+",'"+dtmFechaCreacion+"','"+dtmVencimiento+"',"+intEstado+","+intContacto+","+intPcc+",'"+strPcc+"',"+intFormaPago+",'"+dtmFechaLimitePago+"',"+intEstadoPago+",'"+strMotivoCancel+"','"+dtmFechaCancel+"',"+dblValorCancel+","+intResponsableCancel+",'"+strObservacion+"','"+strLocalizadorExt+"','"+strTansaccionPOL+"','"+strAutorizacionPOL+"',"+intOrdenServicio+",'"+strNumFactura+"'";
                    CadenaInsertTblResMaster = intProyecto + "," + "'" + strReserva + "'" + "," + intTipoServicio + "," + intCodigoPlan + "," + "'" + dtmFechaCreacionMaster + "'" + "," + "'" + dtmVencimientoMaster + "'" + "," + intEstadoMaster + "," + intContactoMaster + "," + intPcc + "," + "'" + strPcc + "'" + "," + intFormaPago + "," + "'" + dtmFechaLimitePago + "'" + "," + intEstadoPago + "," + "'" + strObservacion + "'" + "," + "'" + strLocalizadorExt + "'" + "," + intOrdenServicio + "," + "'" + strDuracion + "'" + "," + "'" + dtmMFechaMaster + "'" + "," + intContactoUser + "," + "'" + strCodigoAsesor + "'";
                    if (!bProyecto.Equals("0") && bProyecto != null)
                    {

                        try
                        {
                            //bMaster = new csGenerales().ConsultarCodigos(CadenaInsertTblResMaster);
                            bMaster = new CsConsultasVuelos().EjecutarSPConsulta("SPInsertarResMaster", new string[20] { intProyecto, "'" + strReserva + "'", intTipoServicio, intCodigoPlan, "'" + dtmFechaCreacionMaster + "'", "'" + dtmVencimientoMaster + "'", intEstadoMaster, intContactoMaster, intPcc, "'" + strPcc + "'", intFormaPago, "'" + dtmFechaLimitePago + "'", intEstadoPago, "'" + strObservacion + "'", "'" + strLocalizadorExt + "'", intOrdenServicio, "'" + strDuracion + "'", "'" + dtmMFechaMaster + "'", intContactoUser, "'" + strCodigoAsesor + "'" });
                            if (bMaster == "0" && bMaster == null)
                            {
                                ExceptionHandled.Publicar("Problema en Insercion de la Tblmaster:" + CadenaInsertTblResMaster);
                            }

                        }
                        catch
                        {
                            ExceptionHandled.Publicar("Problema en Insercion de la tblResMaster:" + CadenaInsertTblResMaster);
                        }

                    }
                    else
                    {
                        ExceptionHandled.Publicar("Problema en Insercion de la tblproyecto:");
                    }



                    for (int j = 0; j < dsReserva.Tables[TABLA_TRANSAC].Rows.Count; j++)
                    {
                        if (dsReserva.Tables[TABLA_RESERVA].Rows[0]["intConsecRes"].ToString().Trim() == dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intConsecRes"].ToString().Trim())
                        {
                            #region validaciones

                            if (dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intSegmento"].ToString().Length.Equals(0))
                            {
                                int Seg = j + 1;
                                dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intSegmento"] = Seg.ToString();
                            }

                            if (dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intOrigen"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intOrigen"] = "0";

                            if (dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intDestino"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intDestino"] = "0";

                            if (dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intCodigoPlan"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intCodigoPlan"] = "0";

                            if (dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intProveedor"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intProveedor"] = "0";

                            if (dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoPlan"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoPlan"] = "0";

                            if (dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intCantidadPersonas"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intCantidadPersonas"] = "0";

                            if (dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoAcomodacion"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoAcomodacion"] = "0";

                            if (dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoHabitacion"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoHabitacion"] = "0";

                            if (dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intEstado"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intEstado"] = "0";

                            #endregion
                            #region asignaciones
                            string intCodigoMaster = bMaster;
                            string intSegmento = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intSegmento"].ToString();
                            string intProveedor = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intProveedor"].ToString();
                            string strOperador = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strOperador"].ToString();
                            string dtmFechaIniTransac = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["dtmFechaIni"].ToString();
                            string strHoraIni = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strHoraIni"].ToString();
                            string dtmFechaFinTransac = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["dtmFechaFin"].ToString();
                            string strHoraFin = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strHoraFin"].ToString();
                            string intOrigen = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intOrigen"].ToString();
                            string intDestino = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intDestino"].ToString();
                            string strObservacionTransac = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strObservacion"].ToString();
                            string intCantidadPersonas = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intCantidadPersonas"].ToString();
                            string intTipoAcomodacion = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoAcomodacion"].ToString();
                            string intTipoHabitacion = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intTipoHabitacion"].ToString();
                            string intEstadoTransac = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["intEstado"].ToString();
                            string strTipoAcomodacion = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strTipoAcomodacion"].ToString();
                            string strTipoHabitacion = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strTipoHabitacion"].ToString();
                            string strCantidadPersonas = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strCantidadPersonas"].ToString();
                            string strRegimen = dsReserva.Tables[TABLA_TRANSAC].Rows[j]["strRegimen"].ToString();
                            string strContactoUser = "0";


                            #endregion
                            if (!bMaster.Equals("0") && bMaster != null)
                            {

                                try
                                {
                                    CadenaIsertTblTransac = intCodigoMaster + "," + intSegmento + "," + intProveedor + "," + "'" + strOperador + "'" + "," + "'" + dtmFechaIniTransac + "'" + "," + "'" + strHoraIni + "'" + "," + "'" + dtmFechaFinTransac + "'" + "," + "'" + strHoraFin + "'" + "," + intOrigen + "," + intDestino + "," + "'" + strObservacionTransac + "'" + "," + intCantidadPersonas + "," + intTipoAcomodacion + "," + intTipoHabitacion + "," + intEstadoTransac + "," + "'" + strTipoAcomodacion + "'" + "," + "'" + strTipoHabitacion + "'" + "," + "'" + strCantidadPersonas + "'" + "," + "'" + strRegimen + "'" + "," + "'" + strContactoUser + "'";
                                    bTransac = new CsConsultasVuelos().EjecutarSPConsulta("SPINSERTARTRANSACT", new string[20] { intCodigoMaster, intSegmento, intProveedor, "'" + strOperador + "'", "'" + dtmFechaIniTransac + "'", "'" + strHoraIni + "'", "'" + dtmFechaFinTransac + "'", "'" + strHoraFin + "'", intOrigen, intDestino, "'" + strObservacionTransac + "'", intCantidadPersonas, intTipoAcomodacion, intTipoHabitacion, intEstadoTransac, "'" + strTipoAcomodacion + "'", "'" + strTipoHabitacion + "'", "'" + strCantidadPersonas + "'", "'" + strRegimen + "'", "'" + strContactoUser + "'" });

                                    if (bTransac.Equals("0") || bTransac == null)
                                    {
                                        ExceptionHandled.Publicar("Problema en Insercion de la tblTransact:" + CadenaIsertTblTransac);
                                    }
                                }
                                catch
                                {

                                    ExceptionHandled.Publicar("Problema en Insercion de la tblTransact:" + CadenaIsertTblTransac);
                                }

                            }
                            else
                            {
                                ExceptionHandled.Publicar("Problema en Insercion de la tblResMaster");
                            }

                            sCadenaIsertTblTransac += CadenaIsertTblTransac + " | ";
                        }
                    }

                    for (int k = 0; k < dsReserva.Tables["tblPax"].Rows.Count; k++)
                    {
                        if (dsReserva.Tables[TABLA_RESERVA].Rows[0]["intConsecRes"].ToString().Trim() == dsReserva.Tables["tblPax"].Rows[k]["intConsecRes"].ToString().Trim())
                        {

                            #region Validaciones
                            //                    ptblResPax.strReserva.Value = ptblResMaster.strReserva.Value;
                            if (dsReserva.Tables[TABLA_PAX].Rows[k]["intCodigoPax"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_PAX].Rows[k]["intCodigoPax"] = "0";

                            if (dsReserva.Tables[TABLA_PAX].Rows[k]["intTipoPax"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_PAX].Rows[k]["intTipoPax"] = "0";

                            if (dsReserva.Tables[TABLA_PAX].Rows[k]["intEdad"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_PAX].Rows[k]["intEdad"] = "0";

                            if (dsReserva.Tables[TABLA_PAX].Rows[k]["intGenero"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_PAX].Rows[k]["intGenero"] = "1";

                            if (dsReserva.Tables[TABLA_PAX].Rows[k]["intTipoDoc"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_PAX].Rows[k]["intTipoDoc"] = "0";

                            if (dsReserva.Tables[TABLA_PAX].Rows[k]["intGenero"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_PAX].Rows[k]["intGenero"] = "0";

                            if (dsReserva.Tables[TABLA_PAX].Rows[k]["intSegmento"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_PAX].Rows[k]["intSegmento"] = "1";

                            if (dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaExpPasaporte"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaExpPasaporte"] = "1980/01/01";

                            if (dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaNac"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaNac"] = "1980/01/01";
                            #endregion
                            #region Asignaciones
                            string intCodigoMaster = bMaster;
                            string intCodigoPax = dsReserva.Tables[TABLA_PAX].Rows[k]["intCodigoPax"].ToString();
                            string intTipoPax = dsReserva.Tables[TABLA_PAX].Rows[k]["intTipoPax"].ToString();
                            string strNombre = dsReserva.Tables[TABLA_PAX].Rows[k]["strNombre"].ToString();
                            string intEdad = dsReserva.Tables[TABLA_PAX].Rows[k]["intEdad"].ToString();
                            string dtmFechaNac = dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaNac"].ToString();
                            string strTelefono = dsReserva.Tables[TABLA_PAX].Rows[k]["strTelefono"].ToString();
                            string strDocumento = dsReserva.Tables[TABLA_PAX].Rows[k]["strDocumento"].ToString();
                            string strNumTiquete = " ";
                            string strEmail = dsReserva.Tables[TABLA_PAX].Rows[k]["strEmail"].ToString();
                            string intGenero = dsReserva.Tables[TABLA_PAX].Rows[k]["intGenero"].ToString();
                            string strPasaporte = dsReserva.Tables[TABLA_PAX].Rows[k]["strPasaporte"].ToString();
                            string intTipoDoc = dsReserva.Tables[TABLA_PAX].Rows[k]["intTipoDoc"].ToString();
                            string strViajeroFrecuente = dsReserva.Tables[TABLA_PAX].Rows[k]["strViajeroFrecuente"].ToString();
                            string strNacionalidad = dsReserva.Tables[TABLA_PAX].Rows[k]["strNacionalidad"].ToString();
                            string strPaisResidencia = dsReserva.Tables[TABLA_PAX].Rows[k]["strPaisResidencia"].ToString();
                            string dtmFechaExpPasaporte = dsReserva.Tables[TABLA_PAX].Rows[k]["dtmFechaExpPasaporte"].ToString();
                            string intSegmento = dsReserva.Tables[TABLA_PAX].Rows[k]["intSegmento"].ToString();
                            string dtmFechaCreacionPax = "1980/01/01";
                            intSegmento = "1";
                            if (intGenero.Equals("0"))
                                intGenero = "1";


                            #endregion


                            try
                            {
                                CadenaIsertTblPax = intCodigoMaster + "," + intCodigoPax + "," + intTipoPax + "," + "'" + strNombre + "'" + "," + intEdad + "," + "'" + dtmFechaNac + "'" + "," + "'" + strTelefono + "'" + "," + "'" + strDocumento + "'" + "," + "'" + strNumTiquete + "'" + "," + "'" + strEmail + "'" + "," + intGenero + "," + "'" + strPasaporte + "'" + "," + intTipoDoc + "," + "'" + strViajeroFrecuente + "'" + "," + "'" + strNacionalidad + "'" + "," + "'" + strPaisResidencia + "'" + "," + "'" + dtmFechaExpPasaporte + "'" + "," + intSegmento + "," + "'" + dtmFechaCreacionPax + "'" + "," + "0";
                                if (!bMaster.Equals("0") && bMaster != null)
                                {
                                    bValida = new CsConsultasVuelos().EjecutarSPConsulta("SPINSERTARPAX", new string[20] { intCodigoMaster, intCodigoPax, intTipoPax, "'" + strNombre + "'", intEdad, "'" + dtmFechaNac + "'", "'" + strTelefono + "'", "'" + strDocumento + "'", "'" + strNumTiquete + "'", "'" + strEmail + "'", intGenero, "'" + strPasaporte + "'", intTipoDoc, "'" + strViajeroFrecuente + "'", "'" + strNacionalidad + "'", "'" + strPaisResidencia + "'", "'" + dtmFechaExpPasaporte + "'", intSegmento, "'" + dtmFechaCreacionPax + "'", "0" });
                                    if (bValida.Equals("0") || bValida == null)
                                    {
                                        ExceptionHandled.Publicar("Problema en Insercion de la tblResPax:" + CadenaIsertTblPax);
                                    }
                                }
                                else
                                {
                                    ExceptionHandled.Publicar("Problema en Insercion de la Master");
                                }
                            }
                            catch
                            {

                                ExceptionHandled.Publicar("Problema en Insercion de la tblPax:" + CadenaIsertTblPax);
                            }
                        }

                        sCadenaIsertTblPax += CadenaIsertTblPax + " | ";
                    }

                    for (int h = 0; h < dsReserva.Tables[TABLA_TARIFA].Rows.Count; h++)
                    {
                        if (dsReserva.Tables[TABLA_RESERVA].Rows[0]["intConsecRes"].ToString().Trim() == dsReserva.Tables[TABLA_TARIFA].Rows[h]["intConsecRes"].ToString().Trim())
                        {
                            #region validaciones
                            if (dsReserva.Tables[TABLA_TARIFA].Rows[h]["intCodigFare"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TARIFA].Rows[h]["intCodigFare"] = "0";

                            if (dsReserva.Tables[TABLA_TARIFA].Rows[h]["intTipoPax"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TARIFA].Rows[h]["intTipoPax"] = "0";

                            if (dsReserva.Tables[TABLA_TARIFA].Rows[h]["intMoneda"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TARIFA].Rows[h]["intMoneda"] = "0";

                            if (dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblValor"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblValor"] = "0";

                            if (dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblTotal"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblTotal"] = "0";

                            if (dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblTax"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblTax"] = "0";

                            if (dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblPenalidad"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblPenalidad"] = "0";

                            if (dsReserva.Tables[TABLA_TARIFA].Rows[h]["intSegmento"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TARIFA].Rows[h]["intSegmento"] = "1";

                            if (dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblDescuento"].ToString().Length.Equals(0))
                                dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblDescuento"] = "0";

                            #endregion
                            #region Asignaciones

                            string intCodigoMaster = bMaster;
                            string intTipoPax = dsReserva.Tables[TABLA_TARIFA].Rows[h]["intTipoPax"].ToString();
                            string intMoneda = dsReserva.Tables[TABLA_TARIFA].Rows[h]["intMoneda"].ToString();
                            string dblValor = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblValor"].ToString().Replace(",", ".");
                            string dblTax = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblTax"].ToString().Replace(",", ".");
                            string dblTotal = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblTotal"].ToString().Replace(",", ".");
                            string dblPenalidad = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblPenalidad"].ToString().Replace(",", ".");
                            string dblDescuento = dsReserva.Tables[TABLA_TARIFA].Rows[h]["dblDescuento"].ToString();
                            string intSegmento = dsReserva.Tables[TABLA_TARIFA].Rows[h]["intSegmento"].ToString();
                            //string Identity = dsReserva.Tables[TABLA_TARIFA].Rows[h]["intCodigFare"].ToString();
                            intSegmento = "1";

                            #endregion
                            if (bMaster != "0" && bMaster != null)
                            {
                                try
                                {
                                    CadenaIsertTblFare = intCodigoMaster + "," + intTipoPax + "," + intMoneda + "," + dblValor + "," + dblTax + "," + dblTotal + "," + dblPenalidad + "," + dblDescuento + "," + intSegmento + "," + "0";

                                    bValidaFare = new CsConsultasVuelos().EjecutarSPConsulta("SPInsertarFare", new string[10] { intCodigoMaster, intTipoPax, intMoneda, dblValor, dblTax, dblTotal, dblPenalidad, dblDescuento, intSegmento, "0" });
                                    if (bValidaFare == "0" && bValidaFare == null)
                                    {
                                        ExceptionHandled.Publicar("Problema en Insercion de la tblFare:" + CadenaIsertTblFare);
                                    }
                                }
                                catch
                                {
                                    ExceptionHandled.Publicar("Problema en Insercion de la TBLFARE:" + CadenaIsertTblFare);
                                }
                            }
                            else
                            {
                                ExceptionHandled.Publicar("Problema en Insercion de la tblPax");
                            }

                            sCadenaIsertTblFare += CadenaIsertTblFare + " | ";
                            DataTable tblTaxes = getTablaImpuestosSumados(dsReserva.Tables[TABLA_TAXFARE]);

                            for (int m = 0; m < tblTaxes.Rows.Count; m++)
                            {
                                if (dsReserva.Tables[TABLA_RESERVA].Rows[0]["intConsecRes"].ToString().Trim() == dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intConsecRes"].ToString().Trim()
                                    && dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intTipoPax"].ToString().Trim() == dsReserva.Tables[TABLA_TARIFA].Rows[h]["intTipoPax"].ToString().Trim())
                                {

                                    #region validaciones
                                    if (dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intCodigoTax"].ToString().Length.Equals(0))
                                        dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intCodigoTax"] = "0";

                                    if (dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intMoneda"].ToString().Length.Equals(0))
                                        dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intMoneda"] = "0";

                                    if (dsReserva.Tables[TABLA_TAXFARE].Rows[m]["dblPorcent"].ToString().Length.Equals(0))
                                        dsReserva.Tables[TABLA_TAXFARE].Rows[m]["dblPorcent"] = "0";

                                    if (dsReserva.Tables[TABLA_TAXFARE].Rows[m]["dblValorTax"].ToString().Length.Equals(0))
                                        dsReserva.Tables[TABLA_TAXFARE].Rows[m]["dblValorTax"] = "0";
                                    #endregion
                                    #region asignaciones

                                    string intCodigoFare = bValidaFare;
                                    string dblPorcent = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["dblPorcent"].ToString().Replace(",", ".");
                                    string dblValorTax = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["dblValorTax"].ToString().Replace(",", ".");
                                    string intMonedaFareTax = dsReserva.Tables[TABLA_TAXFARE].Rows[m]["intMoneda"].ToString();
                                    string intTpotax = dsReserva.Tables[TABLA_TAXFARE].Rows[m][2].ToString();
                                    //string Identity int OUT



                                    #endregion

                                    if (bValidaFare != "0" && bValidaFare != null)
                                    {
                                        try
                                        {
                                            CadenaIsertTblFareTax = intCodigoFare + "," + dblPorcent + "," + dblValorTax + "," + intMonedaFareTax + "," + intTpotax + "," + "0";
                                            bValida = new CsConsultasVuelos().EjecutarSPConsulta("SPInsertarFareTax", new string[6] { intCodigoFare, dblPorcent, dblValorTax, intMonedaFareTax, intTpotax, "0" });
                                            if (bValida == "0" && bValida == null)
                                            {
                                                ExceptionHandled.Publicar("Problema en Insercion de la tblFareTax:" + CadenaIsertTblFareTax);
                                            }

                                        }
                                        catch
                                        {
                                            ExceptionHandled.Publicar("Problema en Insercion de la tblResfareTax:" + CadenaIsertTblFareTax);
                                        }

                                    }
                                    else
                                    {
                                        ExceptionHandled.Publicar("Problema en Insercion de la tblResfare");
                                    }
                                    sCadenaIsertTblFareTax += CadenaIsertTblFareTax;
                                }
                            }

                        }
                    }


                    string Scadena = CadenaInsertTblResMaster + "| " + CadenaInsertTblproyecto + " | " + sCadenaIsertTblTransac + " | " + sCadenaIsertTblPax + " | " + sCadenaIsertTblFare + " | " + sCadenaIsertTblFareTax;
                    ExceptionHandled.Publicar("Cadenas enviadas a los procedimientos " + Scadena);
                    cParametros.Id = 1;
                    cParametros.Message = "Cadenas enviadas a los procedimientos " + Scadena;
                    cParametros.Metodo = "GuardaReservaGen";
                    ExceptionHandled.Publicar(cParametros);
                    cParametros.TipoLog = Enum_Error.Log;


                }
                else
                {
                    cParametros.Id = 0;
                }
            }

            return cParametros;
        }
        /// <summary>
        /// BFM Metodo que suma los impuestos en caso de que un impuesto venga mas de una vez (erl proceso se corrige para que se haga unicamente por tipo de pax)
        /// Autor: Juan Camilo Diaz
        /// Fecha: 18-03-2013
        /// </summary>
        /// <param name="tblTax"></param>
        /// <returns></returns>
        private DataTable getTablaImpuestosSumados(DataTable tblTax)
        {
            try
            {
                for (int m = 0; m < tblTax.Rows.Count; m++)
                {
                    string sIdTax = tblTax.Rows[m]["intCodigoTax"].ToString();
                    string sIdTipoPax = tblTax.Rows[m]["intTipoPax"].ToString();
                    decimal dValue = Convert.ToDecimal(tblTax.Rows[m]["dblValorTax"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal));
                    for (int n = m + 1; n < tblTax.Rows.Count; n++)
                    {
                        if (tblTax.Rows[n]["intCodigoTax"].ToString().Trim().Equals(sIdTax) && tblTax.Rows[n]["intTipoPax"].ToString().Trim().Equals(sIdTipoPax))
                        {
                            dValue += Convert.ToDecimal(tblTax.Rows[n]["dblValorTax"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal));
                            tblTax.Rows.RemoveAt(n);
                            n--;
                        }
                    }
                    tblTax.Rows[m]["dblValorTax"] = dValue;
                }
            }
            catch { }
            return tblTax;
        }


        /// <summary>
        /// Metodo que genera el codigo de reserva de un plan
        /// </summary>
        /// <param name="sTipoPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-09
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public string GetCodigoReservaPlan(string sTipoPlan)
        {
            string Respuesta = string.Empty;
            try
            {
                csConsultasPlanes cPlanes = new csConsultasPlanes();
                DataTable dstDatos = cPlanes.ConsultaCodReservaPlan(sTipoPlan);
                if (dstDatos != null && dstDatos.Rows.Count > 0)
                {
                    Utils.Utils cUtil = new Utils.Utils();
                    string Consecutivo = string.Empty;
                    string Prefijo = string.Empty;
                    int intTotal = 10;
                    Consecutivo = dstDatos.Rows[0]["intConsecutivo"].ToString();
                    Prefijo = dstDatos.Rows[0]["strCodReserva"].ToString();
                    intTotal = intTotal - Prefijo.Length;
                    Respuesta = cUtil.LlenarDatos("0", Consecutivo, intTotal);
                    Respuesta = Prefijo + Respuesta;
                }
                return Respuesta;
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql. Procedimiento: GetCodReservaPlanes";
                ExceptionHandled.Publicar(cMensaje);
                return Respuesta;
            }
        }

       /// <summary>
       /// metodo pendiente por revision
       /// </summary>
       /// <param name="strReserva"></param>
       /// <param name="strTipo"></param>
       /// <param name="strOrigen"></param>
       /// <param name="strDestino"></param>
       /// <param name="strFechaRegreso"></param>
       /// <param name="strFechaSalida"></param>
        public void GuardarLogReserva(string strReserva,
                                   string strTipo,
                                   string strOrigen,
                                   string strDestino,
                                   string strFechaRegreso,
                                   string strFechaSalida)
        {
            //string pstrSql = string.Empty;
            //try
            //{
            //    pstrSql = " INSERT INTO tblLogReservaGen (strReserva, strTipo, strOrigen, strDestino, strFechaSalida, strFechaRegreso) " +
            //              " VALUES  ('" +
            //              strReserva + "', '" +
            //              strTipo + "', '" +
            //              strOrigen + "', '" +
            //              strDestino + "', '" +
            //              strFechaRegreso + "', '" +
            //              strFechaSalida + "') ";

            //    pclsDataSql.UpdateInsert(pstrSql);
            //}
            //catch (Exception Ex)
            //{
            //    ExceptionHandled.Publicar("Error generado, SQL ejecutado " + pstrSql);
            //    ExceptionHandled.Publicar(Ex);
            //}
        }
        public void GuardarLogReserva(string strReserva,
                                   int intTipo,
                                   int intCodigo,
                                   string strOrigen,
                                   string strDestino,
                                   string strFechaSalida)
        {
            //string pstrSql = string.Empty;
            //try
            //{
            //    pstrSql = " INSERT INTO tblLogReserva (strReserva, intTipo, intCodigo, strOrigen, strDestino, strFechaSalida) " +
            //              " VALUES  ('" +
            //              strReserva + "', " +
            //              intTipo + ", " +
            //              intCodigo + ", '" +
            //              strOrigen + "', '" +
            //              strDestino + "', '" +
            //              strFechaSalida + "') ";

            //    pclsDataSql.UpdateInsert(pstrSql);
            //}
            //catch (Exception Ex)
            //{
            //    ExceptionHandled.Publicar("Error generado, SQL ejecutado " + pstrSql);
            //    ExceptionHandled.Publicar(Ex);
            //}
        }
        public bool CambiarEstadoReserva(string strReserva, int intEstado, string strMotivoCancel)
        {
            string pstrSql = string.Empty;
            try
            {
                pstrSql = " UPDATE tblResMaster  SET  intEstado = " + intEstado + ", strMotivoCancel = '" + strMotivoCancel + "'   WHERE  strReserva = '" + strReserva + "'  ";
                pstrSql += " UPDATE tblResTransac  SET  intEstado = " + intEstado + "   WHERE  strReserva = '" + strReserva + "'  ";

                pclsDataSql.UpdateInsert(pstrSql);
                return true;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error Guardando Estado de reserva. Error generado, SQL ejecutado " + pstrSql;
                return false;
            }
        }
        public bool CambiarEstadoProyecto(string strProyecto, int intEstado)
        {
            string pstrSql = string.Empty;
            try
            {
                pstrSql = " UPDATE tblproyecto  SET  intEstado = " + intEstado + "   WHERE  intProyecto = " + strProyecto + "  ";

                pclsDataSql.UpdateInsert(pstrSql);
                return true;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error Guardando estado del proyecto. Error generado, SQL ejecutado " + pstrSql;
                return false;
            }
        }
        public bool CambiarEstadoPagoReserva(string strReserva, int intEstado, int intFormaPago)
        {
            string pstrSql = string.Empty;
            try
            {
                pstrSql = " UPDATE tblResMaster  SET  intEstadoPago = " + intEstado + ", intFormaPago = " + intFormaPago + "    WHERE  strReserva = '" + strReserva + "'  ";

                pclsDataSql.UpdateInsert(pstrSql);
                return true;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error Guardando Estado del pago. Error generado, SQL ejecutado " + pstrSql;
                return false;
            }
        }
        public bool CambiarEstadoAnticipoProyecto(string strProyecto, int intEstado)
        {
            string pstrSql = string.Empty;
            try
            {
                pstrSql = " UPDATE tblproyecto  SET  intEstadoAnticipo = " + intEstado + "   WHERE  intProyecto = " + strProyecto + "  ";

                pclsDataSql.UpdateInsert(pstrSql);
                return true;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error Guardando Estado de anticipo. Error generado, SQL ejecutado " + pstrSql;

                return false;
            }
        }
        public bool CambiarEstadoSolicitud(string strProyecto, int intEstado, int intMotivo)
        {
            string pstrSql = string.Empty;
            try
            {
                if (intMotivo.Equals(0))
                {
                    pstrSql = " UPDATE tblproyecto  SET  intEtapa = " + intEstado + "   WHERE  intProyecto = " + strProyecto + "  ";
                }
                else
                {
                    pstrSql = " UPDATE tblproyecto  SET  intEtapa = " + intEstado + ", intMotivoSolicitud = " + intMotivo + "   WHERE  intProyecto = " + strProyecto + "  ";
                }

                pclsDataSql.UpdateInsert(pstrSql);
                return true;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error Guardando Estado de solicitud. Error generado, SQL ejecutado " + pstrSql;
                return false;
            }
        }
       
    }
 
}
