using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI;
using Ssoft.ManejadorExcepciones;
using Ssoft.Rules.Generales;
using System.Configuration;
using Ssoft.Rules.Pagina;
using Ssoft.Rules.Administrador;
using System.Web;
using Utils = Ssoft.Utils.Utils;
using Ssoft.Utils;
using Ssoft.ValueObjects;

namespace Ssoft.Rules.Reservas
{

    public class csReservaWs
    {
        protected string _strConexion = default(string);
        // Tablas
        public const string TABLA_MASTER = "tblReserva";
        public const string TABLA_SEGMENTOS = "tblTransac";
        public const string TABLA_PAX = "tblPax";
        public const string TABLA_TARIFA = "tblTarifa";
        public const string TABLA_TASAS = "tblTax";
        private const string FORMATO_NUMEROS = "#,##0.00";
        private static string FORMATO_FECHA_BD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");
        private static string FORMATO_FECHA = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
        /// <summary>
        /// Estable u obtiene es string de conexion
        /// </summary>
        public string Conexion
        {
            set { _strConexion = value; }
            get { return _strConexion; }
        }
        public csReservaWs()
        {
            Conexion = clsSesiones.getConexion();
        }
       
       
   
        private clsResultados GenerarEstructura()
        {
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            csReservas cReserva = new csReservas();
            try
            {
                cResultados.dsResultados = cReserva.CrearTablaReserva();
                cParametros.Id = 1;
                cResultados.Error = cParametros;
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Aplication;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error al crear la estructura de reservas";
                cParametros.ViewMessage.Add("Error al guardar en la base de datos");
                cParametros.Sugerencia.Add("Por favor comuniquese con el administrador");
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
  

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        private void GuardarDatosProyecto()
        {
            /*FECHA LIMITE DE PAGO*/
            DateTime dPlazo = clsSesiones.GET_TICKETE();
            const string strNombreCarroCompras = "CarritoCompras";
            clsCache cCache = new csCache().cCache();
            csCarrito csCarCompras = new csCarrito("Reserva" + cCache.SessionID, strNombreCarroCompras);
            string idRecord = clsSesiones.getProyecto();
            csGenerales cGeneral = new csGenerales();
            cGeneral.Conexion = clsValidaciones.GetKeyOrAdd("strConexion");

            //tblRefere otblRefere = new tblRefere();
            //Para el estado de la reserva
            //otblRefere.Get(clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva"), clsValidaciones.GetKeyOrAdd("EstadoReservaConfirmada", "HK"));
            //string sEstado = string.Empty;
            //string sFormaPago = string.Empty;
            //string sEstadoPago = string.Empty;
            //if (otblRefere.Respuesta == true)
            //    sEstado = otblRefere.intidRefere.Value.ToString();
            ////Para la forma de Pago
            //otblRefere.Get(clsValidaciones.GetKeyOrAdd("FormasPago", "FP"), clsValidaciones.GetKeyOrAdd("Efectivo", "efe"));
            //if (otblRefere.Respuesta == true)
            //    sFormaPago = otblRefere.intidRefere.Value.ToString();
            ////Para el estado del Pago
            //otblRefere.Get(clsValidaciones.GetKeyOrAdd("EstadoPago", "EstadoPago"), clsValidaciones.GetKeyOrAdd("EstadoPagoPendiente", "PP"));
            //if (otblRefere.Respuesta == true)
            //    sEstadoPago = otblRefere.intidRefere.Value.ToString();

            //csCarCompras.SaveDataProject(idRecord, cCache.Contacto, cCache.Contacto, "0", sEstado, sFormaPago, sEstadoPago);

        }
    }

}
