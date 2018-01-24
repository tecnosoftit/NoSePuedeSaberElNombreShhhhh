using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;

namespace Ssoft.Rules.Reservas
{

    /// <summary>
    /// Clase que ofrece metodos de manejo de el carrito de compras
    /// </summary>
    public class csCarrito
    {
        #region [CAMPOS]

        csReservas cReserva = new csReservas();

        Utils.Utils csUtileria = new Utils.Utils();

        int intConsecutivo = default(int);

        string _strIdentificadorUnico = "";
        string _strNombreCarroDeCompras = "";

        DataTable tblDatosPasajeros;
        DataTable tblDatosTipoPasajero;
        DataTable tblDatosHabitaciones;
        DataSet dsReservas = new DataSet();

        string _intValorPenalidad = "0";
        string _intValorTotal = "0";
        string _intCodigoPlan = "0";
        string _intCodigoTarifa = "0";
        string _intNumeroHabitaciones = "0";
        string _intcantidadPersonas = "0";
        string _intCapacidad = "0";
        string _intCapacidadMaxima = "0";
        string _intNumeroDias = "0";
        string _intNumeroNoches = "0";
        string _intSegmento = "0";
        string _intNochesAdicionales = "0";
        string _intValorOferta = "0";

        String _strFechaInicialCarrito = "";
        String _strFechaFinalCarrito = "";

        String _strFechaInicial = "";
        String _strFechaFinal = "";
        String _strFechaVencimiento = "";
        String _strNombrePlan = "";
        String _strImagen = "";
        String _strDescripcion = "";
        String _strRestricciones = "";
        String _strIncluye = "";
        String _strNoIncluye = "";
        String _strBeneFicios = "";
        String _strEncuenta = "";
        String _strDetalles = "";
        String _strRuta = "";
        String _strUbicacion = "";
        String _strObservacion = "";
        String _strCodigoReserva = "0";
        String _strHoraIni = "";
        String _strHoraFin = "";
        String _strCodigo = "0";
        String _strOperador = "";
        String _strIdentificadorDelPlan = "";
        String _strLocalizadorExt = "0";
        String _strPasajeros = "";
        bool _bolImpuestos = true;

        String _strTipoPlan = "";
        string _intTipoPlan = "0";

        String _strSubTipoPlan = "";
        string _intSubTipoPlan = "0";

        String _strPais = "";
        string _intPais = "0";

        String _strCiudad = "";
        string _intCiudad = "0";

        String _strTemporada = "";
        string _intTemporada = "0";

        String _strCategoria = "";
        string _intCategoria = "0";

        String _strAcomodacion = "";
        string _intAcomodacion = "0";

        String _strTipoHabitacion = "";
        string _intTipoHabitacion = "0";

        String _strServicio = "";
        string _intServicio = "0";

        String _strTipoServicio = "";
        string _intTipoServicio = "0";

        String _strZonaGeografica = "";
        string _intZonaGeografica = "0";

        String _strTipoPasajero = "";
        string _intTipoPasajero = "0";

        String _strTipoMoneda = "";
        string _intTipoMoneda = "0";

        String _strSeccionPublicacion = "";
        string _intSeccionPublicacion = "0";

        String _strTipoPropiedad = "";
        string _intTipoPropiedad = "0";

        String _strOrigen = "";
        string _intOrigen = "0";

        String _strDestino = "";
        string _intDestino = "0";

        String _strEstado = "";
        string _intEstado = "0";

        String _strProveedor = "";
        string _intProveedor = "0";

        String _strConfirmacion = "";
        string _intConfirmacion = "0";

        string _strRecordRemark = "";
        string _intIdRefereRemark = "";
        int _intConsecutivoRemark = default(int);
        string _strDescripcionRemark = "";
        string _strDetalleRemark = "";

        //-------- Campos para la nueva estructura de reservas        
        string _intConvenio = "0";
        String _strConvenioCorp = "";
        String _strDuracion = "";
        String _strCantidadPersonas = "";
        String _strRegimen = "";
        string _intVigencia = "0";

        String _strHoteles = "";

        #endregion

        #region [PROPIEDADES]
        /// <summary>
        /// Obtine la tabla de los pasajeros de el plan actual.
        /// Aplica para planes que requieren guardar los detalles del pasajero.
        /// </summary>
        public DataTable TablaPasajeros
        {
            get { return tblDatosPasajeros; }
            protected set { tblDatosPasajeros = value; }
        }
        /// <summary>
        /// Obtiene los datos de los tipos de pasajeros asociados al plan actual.
        /// Aplica para planes donde cada tipo de pasajero tiene un valor distinto.
        /// </summary>
        public DataTable TablaTipoPasajero
        {
            get { return tblDatosTipoPasajero; }
            protected set { tblDatosTipoPasajero = value; }
        }
        /// <summary>
        /// Obtiene la tabla de habitaciones del plan actual.
        /// Aplica para planes como hoteles donde se pueden reservar varias habitaciones,
        /// y se nececita el detalle de cada una.
        /// </summary>
        public DataTable TablaHabitaciones
        {
            get { return this.CrearTablaHabitaciones(); }
            protected set { tblDatosHabitaciones = value; }
        }
        /// <summary>
        /// Asigna y obtiene el valor total del plan.
        /// </summary>
        public string IntValorOferta
        {
            get { return _intValorOferta; }
            set { _intValorOferta = value; }
        }
        public string IntValorTotal
        {
            get { return _intValorTotal; }
            set { _intValorTotal = value; }
        }
        /// <summary>
        /// Asigna y obtiene el valor de la Penalidad.
        /// </summary>
        public string IntValorPenalidad
        {
            get { return _intValorPenalidad; }
            set { _intValorPenalidad = value; }
        }
        /// <summary>
        /// Asigna y obtiene el codigo del plan.
        /// </summary>
        public string IntCodigoPlan
        {
            get { return _intCodigoPlan; }
            set { _intCodigoPlan = value; }
        }
        /// <summary>
        /// Asigna y obtiene el codigo de la tarifa.
        /// </summary>
        public string IntCodigoTarifa
        {
            get { return _intCodigoTarifa; }
            set { _intCodigoTarifa = value; }
        }
        /// <summary>
        /// Asigna y obtiene la cantidad de personas asiciadas al plan
        /// </summary>
        public string IntcantidadPersonas
        {
            get { return _intcantidadPersonas; }
            set { _intcantidadPersonas = value; }
        }
        /// <summary>
        /// Asigna y obtiene el LocalizadorExt,este campo es opcional.
        /// </summary>
        public string StrLocalizadorExt
        {
            get { return _strLocalizadorExt; }
            set { _strLocalizadorExt = value; }
        }
        /// <summary>
        /// Asigna y obtiene la capacidad de un plan.
        /// </summary>
        public string IntCapacidad
        {
            get { return _intCapacidad; }
            set { _intCapacidad = value; }
        }
        /// <summary>
        /// Asigna y obtiene la capacidad maxima de un plan.
        /// </summary>
        public string IntCapacidadMaxima
        {
            get { return _intCapacidadMaxima; }
            set { _intCapacidadMaxima = value; }
        }
        /// <summary>
        /// Campo de solo lectuta, obtiene el consecutivo del plan.
        /// </summary>
        public Int32 IntConsecutivo
        {
            get { return intConsecutivo; }
        }
        /// <summary>
        /// Asigna y obtine el numero de dias del plan.
        /// </summary>
        public string IntNumeroDias
        {
            get { return _intNumeroDias; }
            set { _intNumeroDias = value; }
        }
        /// <summary>
        /// Asigna y obtine el numero de noches del plan.
        /// </summary>
        public string IntNumeroNoches
        {
            get { return _intNumeroNoches; }
            set { _intNumeroNoches = value; }
        }

        /// <summary>
        /// Asigna y obtine el numero de noches adicionales de la reserva.
        /// </summary>
        public string IntNochesAdicionales
        {
            get { return _intNochesAdicionales; }
            set { _intNochesAdicionales = value; }
        }
        /// <summary>
        /// Asigna y obtiene el numero de habitaciones del plan.
        /// </summary>
        public string IntNumeroHabitaciones
        {
            get { return _intNumeroHabitaciones; }
            set { _intNumeroHabitaciones = value; }
        }
        /// <summary>
        /// Asigna y obtine el segmento del plan.
        /// </summary>
        public string IntSegmento
        {
            get { return _intSegmento; }
            set { _intSegmento = value; }
        }
        /// <summary>
        /// Asigna y obtiene el nombre identificador del tipo de plan dentro del carrito de compras
        /// cada habitacion es un segmento.
        /// </summary>
        public String StrIdentificadorDelPlan
        {
            get { return _strIdentificadorDelPlan; }
            set { _strIdentificadorDelPlan = value; }
        }

        public String StrPasajeros
        {
            get { return _strPasajeros; }
            set { _strPasajeros = value; }
        }

        public bool BolImpuestos
        {
            get { return _bolImpuestos; }
            set { _bolImpuestos = value; }
        }

        /// <summary>
        /// Asigna y obtiene la fecha inicial del plan,aplica para planes que como los hoteles que tienen varias habitaciones,
        /// cada habitacion es un segmento.
        /// </summary>
        public String StrFechaInicial
        {
            get
            {
                if (!_strFechaInicial.Equals(""))
                {
                    return clsValidaciones.ConverFecha(_strFechaInicial, clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy"),
                        clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/dd/MM"));
                }
                return _strFechaInicial;
            }
            set { _strFechaInicial = value; }
        }
        /// <summary>
        /// Asigna y obtiene la fecha final del plan.
        /// </summary>
        public String StrFechaFinal
        {
            get
            {
                if (!_strFechaFinal.Equals(""))
                {
                    return clsValidaciones.ConverFecha(_strFechaFinal, clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy"),
                        clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/dd/MM"));
                }
                return _strFechaFinal;
            }
            set { _strFechaFinal = value; }
        }

        public String StrFechaInicialCarrito
        {
            get
            {
                if (!_strFechaInicialCarrito.Equals(""))
                {
                    return clsValidaciones.ConverFecha(_strFechaInicialCarrito, clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy"),
                        clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/dd/MM"));
                }
                return _strFechaInicialCarrito;
            }
            set { _strFechaInicialCarrito = value; }
        }
        /// <summary>
        /// Asigna y obtiene la fecha final del plan.
        /// </summary>
        public String StrFechaFinalCarrito
        {
            get
            {
                if (!_strFechaFinalCarrito.Equals(""))
                {
                    return clsValidaciones.ConverFecha(_strFechaFinalCarrito, clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy"),
                        clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/dd/MM"));
                }
                return _strFechaFinalCarrito;
            }
            set { _strFechaFinalCarrito = value; }
        }

        public String StrFechaVencimiento
        {
            get
            {
                if (!_strFechaVencimiento.Equals(""))
                {
                    return clsValidaciones.ConverFecha(_strFechaVencimiento, clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy"),
                        clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/dd/MM"));
                }
                return _strFechaVencimiento;
            }
            set { _strFechaVencimiento = value; }
        }
        /// <summary>
        /// Asigna y obtiene la hora inicial del plan.
        /// </summary>
        public String StrHoraIni
        {
            get { return _strHoraIni; }
            set { _strHoraIni = value; }
        }
        /// <summary>
        /// Asigna y obtiene la hora final del plan.
        /// </summary>
        public String StrHoraFin
        {
            get { return _strHoraFin; }
            set { _strHoraFin = value; }
        }
        /// <summary>
        /// Asigna y obtiene el nombre del plan.
        /// </summary>
        public String StrNombrePlan
        {
            get { return _strNombrePlan; }
            set { _strNombrePlan = value; }
        }
        /// <summary>
        /// Asigna y obtiene la imagen del plan.
        /// </summary>
        public String StrImagen
        {
            get { return _strImagen; }
            set { _strImagen = value; }
        }
        /// <summary>
        /// Asigna y obtiene la descripcion del plan.
        /// </summary>
        public String StrDescripcion
        {
            get { return _strDescripcion; }
            set { _strDescripcion = value; }
        }
        /// <summary>
        /// Asigna y obtiene las restriciones del plan.
        /// </summary>
        public String StrRestricciones
        {
            get { return _strRestricciones; }
            set { _strRestricciones = value; }
        }
        /// <summary>
        /// Asigna y obtiene lo que incluye el plan.
        /// </summary>
        public String StrIncluye
        {
            get { return _strIncluye; }
            set { _strIncluye = value; }
        }
        /// <summary>
        /// Asigna y obtiene lo que no incluye el plan.
        /// </summary>
        public String StrNoIncluye
        {
            get { return _strNoIncluye; }
            set { _strNoIncluye = value; }
        }
        /// <summary>
        /// Asigna y obtiene lo beneficios del plan.
        /// </summary>
        public String StrBeneFicios
        {
            get { return _strBeneFicios; }
            set { _strBeneFicios = value; }
        }
        /// <summary>
        /// Asigna y obtiene el campo de tenga en cuenta del plan.
        /// </summary>
        public String StrEncuenta
        {
            get { return _strEncuenta; }
            set { _strEncuenta = value; }
        }
        /// <summary>
        /// Asigna y obtiene los detalles del plan.
        /// </summary>
        public String StrDetalles
        {
            get { return _strDetalles; }
            set { _strDetalles = value; }
        }
        /// <summary>
        /// Asigna y obtiene la ruta del plan,aplica para planes que tinen varios recorridos.
        /// </summary>
        public String StrRuta
        {
            get { return _strRuta; }
            set { _strRuta = value; }
        }
        /// <summary>
        /// Asigna y obtiene la ubicacion del plan,aplica para planes como casas o apartementos.
        /// </summary>
        public String StrUbicacion
        {
            get { return _strUbicacion; }
            set { _strUbicacion = value; }
        }
        /// <summary>
        /// Asigna y obtiene la observacion del plan.
        /// </summary>
        public String StrObservacion
        {
            get { return _strObservacion; }
            set { _strObservacion = value; }
        }
        /// <summary>
        /// Asigna y obtiene el tipo de plan.
        /// </summary>
        public String StrTipoPlan
        {
            get { return _strTipoPlan; }
            set { _strTipoPlan = value; }
        }
        /// <summary>
        /// Asigna y obtiene el codigo que indica que es una insersion o actualizacion.
        /// no es obligatorio ya que por defecto es "0" que indica que es una insercion.
        /// Y "1" que es una actualizacion.
        /// </summary>
        public String StrCodigo
        {
            get
            {
                if (_strCodigo == null)
                {
                    _strCodigo = "0";
                }
                return _strCodigo;
            }
            set { _strCodigo = value; }
        }
        /// <summary>
        /// Asigna y obtiene el hotel asiciado a un plan de circuitos,
        /// donde el circuito solo tiene un hotel asiciado.
        /// </summary>
        public String StrOperador
        {
            get { return _strOperador; }
            set { _strOperador = value; }
        }
        /// <summary>
        /// Asigna y obtiene y obtiene el codigo de reserva que no es obligatorio,
        /// porque es generado automaticamente.Aplica para hoteles de web services.
        /// </summary>
        public String StrCodigoReserva
        {
            get
            {
                if (_strCodigoReserva == null)
                {
                    _strCodigoReserva = "0";
                } return _strCodigoReserva;
            }
            set { _strCodigoReserva = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere del tipo de plan.
        /// </summary>
        public string IntTipoPlan
        {
            get { return _intTipoPlan; }
            set { _intTipoPlan = value; }
        }
        /// <summary>
        /// Asigna y obtiene el  subtipo del plan. 
        /// solo aplica para planes especificos con subtipos. 
        /// eje:Casas de alquiler
        /// </summary>
        public String StrSubTipoPlan
        {
            get { return _strSubTipoPlan; }
            set { _strSubTipoPlan = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere del subTipoplan
        /// </summary>
        public string IntSubTipoPlan
        {
            get { return _intSubTipoPlan; }
            set { _intSubTipoPlan = value; }
        }
        /// <summary>
        /// Asigna y obtiene el  pais del plan
        /// </summary>
        public String StrPais
        {
            get { return _strPais; }
            set { _strPais = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere del pais del plan.
        /// </summary>
        public string IntPais
        {
            get { return _intPais; }
            set { _intPais = value; }
        }
        /// <summary>
        /// Asigna y obtiene la ciudad del plan.
        /// </summary>
        public String StrCiudad
        {
            get { return _strCiudad; }
            set { _strCiudad = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere de la ciudad del plan.
        /// </summary>
        public string IntCiudad
        {
            get { return _intCiudad; }
            set { _intCiudad = value; }
        }
        /// <summary>
        /// Asigna y obtiene de la temporada del plan.
        /// Aplica para planes que pueden tener temporadas.
        /// </summary>
        public String StrTemporada
        {
            get { return _strTemporada; }
            set { _strTemporada = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere de la temporada del plan.
        /// </summary>
        public string IntTemporada
        {
            get { return _intTemporada; }
            set { _intTemporada = value; }
        }
        /// <summary>
        /// Asigna y obtiene la categoria del plan.
        /// Aplica para planes que puedan tener categoria.
        /// </summary>
        public String StrCategoria
        {
            get { return _strCategoria; }
            set { _strCategoria = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere de la categoria del plan.
        /// </summary>
        public string IntCategoria
        {
            get { return _intCategoria; }
            set { _intCategoria = value; }
        }
        /// <summary>
        /// Asigna y obtiene la acomodacion del plan.        
        /// </summary>
        public String StrAcomodacion
        {
            get { return _strAcomodacion; }
            set { _strAcomodacion = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere de la acomodacion del plan
        /// </summary>
        public string IntAcomodacion
        {
            get { return _intAcomodacion; }
            set { _intAcomodacion = value; }
        }
        /// <summary>
        /// Asigna y obtiene el tipo de habitacion del plan.
        /// </summary>
        public String StrTipoHabitacion
        {
            get { return _strTipoHabitacion; }
            set { _strTipoHabitacion = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere del tipo de habitacion del plan.
        /// </summary>
        public string IntTipoHabitacion
        {
            get { return _intTipoHabitacion; }
            set { _intTipoHabitacion = value; }
        }
        /// <summary>
        /// Asigna y obtiene el sevicio del plan.
        /// No es obligatorio solo para planes que lo requieran.
        /// </summary>
        public String StrServicio
        {
            get { return _strServicio; }
            set { _strServicio = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere del servicio del plan.
        /// </summary>
        public string IntServicio
        {
            get { return _intServicio; }
            set { _intServicio = value; }
        }
        /// <summary>
        /// Asigna y obtiene el tipo de servicion.
        /// No es obligatorio solo para planes que lo requieran.
        /// </summary>
        public String StrTipoServicio
        {
            get { return _strTipoServicio; }
            set { _strTipoServicio = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere del tipo de servicio del plan.
        /// </summary>
        public string IntTipoServicio
        {
            get { return _intTipoServicio; }
            set { _intTipoServicio = value; }
        }
        /// <summary>
        /// Asigna y obtiene la zona geografica del plan que generalmente es el continente donde se ubica el plan.
        /// </summary>
        public String StrZonaGeografica
        {
            get { return _strZonaGeografica; }
            set { _strZonaGeografica = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere de la zona geografica del plan.
        /// </summary>
        public string IntZonaGeografica
        {
            get { return _intZonaGeografica; }
            set { _intZonaGeografica = value; }
        }
        /// <summary>
        /// Asigna y obtiene el tipo de pasajero del plan,
        /// aplica para planes que tengan un solo pasajero.
        /// </summary>
        public String StrTipoPasajero
        {
            get { return _strTipoPasajero; }
            set { _strTipoPasajero = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere del tipo pasajero.
        /// </summary>
        public string IntTipoPasajero
        {
            get { return _intTipoPasajero; }
            set { _intTipoPasajero = value; }
        }
        /// <summary>
        /// Asigna y obtiene el tipo de moneda con que se referencia el valor del plan.
        /// </summary>
        public String StrTipoMoneda
        {
            get { return _strTipoMoneda; }
            set { _strTipoMoneda = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere del tipo de moneda del plan.
        /// </summary>
        public string IntTipoMoneda
        {
            get { return _intTipoMoneda; }
            set { _intTipoMoneda = value; }
        }
        /// <summary>
        /// Asigna y obtiene la seccion de publicacion del plan.
        /// La seccion de publicacion es la parte en donde se esta mostrando el plan de la pagina.
        /// </summary>
        public String StrSeccionPublicacion
        {
            get { return _strSeccionPublicacion; }
            set { _strSeccionPublicacion = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere de la seccion de punblicacion.
        /// </summary>
        public string IntSeccionPublicacion
        {
            get { return _intSeccionPublicacion; }
            set { _intSeccionPublicacion = value; }
        }
        /// <summary>
        /// Asigna y obtiene el tipo de propiedad del plan.
        /// Aplica para planes donde se reserva casas o apartementos. 
        /// </summary>
        public String StrTipoPropiedad
        {
            get { return _strTipoPropiedad; }
            set { _strTipoPropiedad = value; }
        }
        /// <summary>
        /// como casas o apartementos  del tipo de propiedad del plan.
        /// </summary>
        public string IntTipoPropiedad
        {
            get { return _intTipoPropiedad; }
            set { _intTipoPropiedad = value; }
        }
        /// <summary>
        /// Asigna y obtiene el continente,pais o ciudad donde inicial el plan.
        /// Aplica para planes como circuitos.
        /// </summary>
        public String StrOrigen
        {
            get { return _strOrigen; }
            set { _strOrigen = value; }
        }
        /// <summary>
        /// Asigna y obtiene el IdRefere del Origen del plan. 
        /// </summary>
        public string IntOrigen
        {
            get { return _intOrigen; }
            set { _intOrigen = value; }
        }
        /// <summary>
        ///  Asigna y obtiene el IdRefere del continente ,pais o ciudad del plan,
        /// aplica para planes como circuitos.
        /// </summary>
        public String StrDestino
        {
            get { return _strDestino; }
            set { _strDestino = value; }
        }
        /// <summary>
        ///  Asigna y obtiene el IdRefere del plan.
        /// </summary>
        public string IntDestino
        {
            get { return _intDestino; }
            set { _intDestino = value; }
        }
        /// <summary>
        ///  Asigna y obtiene el estado del plan , 
        /// que puede ser activo o inactivo.
        /// </summary>
        public String StrEstado
        {
            get { return _strEstado; }
            set { _strEstado = value; }
        }
        /// <summary>
        ///  Asigna y obtiene el IdRefere del estado del plan.
        /// </summary>
        public string IntEstado
        {
            get { return _intEstado; }
            set { _intEstado = value; }
        }
        /// <summary>
        ///  Asigna y obtiene el proveedor del plan,
        /// puede ser el nombre de la empresa que provee el plan.
        /// </summary>
        public String StrProveedor
        {
            get { return _strProveedor; }
            set { _strProveedor = value; }
        }
        /// <summary>
        ///  Asigna y obtiene el IdRefere del proveedor del plan.
        /// </summary>
        public string IntProveedor
        {
            get { return _intProveedor; }
            set { _intProveedor = value; }
        }
        /// <summary>
        ///  Asigna y obtiene la confirmacion del plan.
        /// </summary>
        public String StrConfirmacion
        {
            get { return _strConfirmacion; }
            set { _strConfirmacion = value; }
        }
        /// <summary>
        ///  Asigna y obtiene el IdRefere de la confirmacion del plan.
        /// </summary>
        public string IntConfirmacion
        {
            get { return _intConfirmacion; }
            set { _intConfirmacion = value; }
        }

        public string StrRecordRemark
        {
            get { return _strRecordRemark; }
            set { _strRecordRemark = value; }
        }

        public string IntIdRefereRemark
        {
            get { return _intIdRefereRemark; }
            set { _intIdRefereRemark = value; }
        }

        public Int32 intConsecutivoRemark
        {
            get { return _intConsecutivoRemark; }
        }

        public string StrDescripcionRemark
        {
            get { return _strDescripcionRemark; }
            set { _strDescripcionRemark = value; }
        }

        public string StrDetalleRemark
        {
            get { return _strDetalleRemark; }
            set { _strDetalleRemark = value; }
        }

        public string IntConvenio
        {
            get { return _intConvenio; }
            set { _intConvenio = value; }
        }

        public String StrConvenioCorp
        {
            get { return _strConvenioCorp; }
            set { _strConfirmacion = value; }
        }

        public String StrDuracion
        {
            get { return _strDuracion; }
            set { _strDuracion = value; }
        }

        public String StrCantidadPersonas
        {
            get { return _strCantidadPersonas; }
            set { _strCantidadPersonas = value; }
        }

        public String StrRegimen
        {
            get { return _strRegimen; }
            set { _strRegimen = value; }
        }

        public String StrHoteles
        {
            get { return _strHoteles; }
            set { _strHoteles = value; }
        }

        public string IntVigencia
        {
            get { return _intVigencia; }
            set { _intVigencia = value; }
        }

  
        #endregion

        #region [CONSTRUCTORES]
        public csCarrito()
        {
        }
        /// <summary>
        /// Constructor  obligatorio que inicializa la estructura del  carrito de compras,creando un archivo XML y dandole un nombre al carrito
        /// y si ya existe  identifica el archivo  XML  existente  del carrito de compras.
        /// </summary>
        /// <param name="strIdentificadorUnico">Nombre con el cual se llama al archivo fisico XML del carrito de compras que debe ser unico por session</param>
        /// <param name="strNombreCarroDeCompras">Nombre con el cual se llama al carrito de compras</param>
        public csCarrito(string strIdentificadorUnico, string strNombreCarroDeCompras)
        {
            this._strNombreCarroDeCompras = strNombreCarroDeCompras;

            this._strIdentificadorUnico = strIdentificadorUnico;

            InicializarDataset(strIdentificadorUnico, strNombreCarroDeCompras);
        }

        #endregion

        #region [METODOS PRIVADOS]

        private DataSet InicializarDataset(string strIdentificadorUnico, string strNombreCarroDeCompras)
        {
            this.dsReservas = RecuperarDataSet(strIdentificadorUnico);

            if (dsReservas == null)
            {
                this.dsReservas = this.CrearEstructuraReservas();

                this.GuardarDataSetOActualizarDataSet(strIdentificadorUnico, dsReservas);

                intConsecutivo = 1;
            }
            else
            {
                DataTable tblDatosCarro = dsReservas.Tables[strNombreCarroDeCompras];

                if (tblDatosCarro != null)
                {
                    if (tblDatosCarro.Rows.Count > 0)
                    {
                        intConsecutivo = ((int)tblDatosCarro.Rows[tblDatosCarro.Rows.Count - 1]["intConsecRes"]) + 1;
                    }
                    else
                    {
                        this.intConsecutivo = 1;
                    }
                }

            }

            return dsReservas;
        }

        private void ModificarTblReserva(DataSet dsReservas)
        {
            DataTable tblDatosReserva = dsReservas.Tables[csReservas.TABLA_RESERVA];

            tblDatosReserva.Columns["intConsecRes"].DataType = intConsecutivo.GetType();

            tblDatosReserva.Columns.Add("strContacto", typeof(string));

            tblDatosReserva.Columns.Add("strCliente", typeof(string));

            tblDatosReserva.Columns.Add("strResponsable", typeof(string));

            tblDatosReserva.Columns.Add("StrTipoPlan", StrTipoPlan.GetType());

            tblDatosReserva.Columns.Add("StrEstado", StrEstado.GetType());

            tblDatosReserva.Columns.Add("StrFormaPago", typeof(string));

            tblDatosReserva.Columns.Add("StrEstadoPago", typeof(string));

            tblDatosReserva.AcceptChanges();
        }

        private void ModificarTblTransac(DataSet dsReservas)
        {
            DataTable TblTarifa = dsReservas.Tables[csReservas.TABLA_TRANSAC];

            TblTarifa.Columns["intConsecRes"].DataType = intConsecutivo.GetType();

            TblTarifa.Columns.Add("StrProveedor", StrProveedor.GetType());

            TblTarifa.Columns.Add("StrOrigen", StrOrigen.GetType());

            TblTarifa.Columns.Add("StrDestino", StrDestino.GetType());

            TblTarifa.Columns.Add("StrAcomodacion", StrAcomodacion.GetType());

            TblTarifa.Columns.Add("StrSubTipoHab", StrAcomodacion.GetType());

            TblTarifa.Columns.Add("dblValor", IntValorTotal.GetType());

            TblTarifa.Columns.Add("intIdVigencia", IntVigencia.GetType());

            TblTarifa.Columns.Add("StrHoteles", StrHoteles.GetType());

            TblTarifa.AcceptChanges();

        }

        private void ModificarTblPax(DataSet dsReservas)
        {
            DataTable tblDatosPax = dsReservas.Tables[csReservas.TABLA_PAX];
            tblDatosPax.Columns["intConsecRes"].DataType = intConsecutivo.GetType();
            tblDatosPax.Columns["intCodigoPax"].DataType = intConsecutivo.GetType();
            DataColumn[] dtPrimaryKeys = { tblDatosPax.Columns["intCodigoPax"] };
            tblDatosPax.PrimaryKey = dtPrimaryKeys;
            tblDatosPax.Columns["intCodigoPax"].AutoIncrement = true;
            tblDatosPax.Columns["intCodigoPax"].AutoIncrementSeed = 1;
            tblDatosPax.Columns["intCodigoPax"].AutoIncrementStep = 1;
            tblDatosPax.Columns.Add("strTipoPax", typeof(string));
            tblDatosPax.Columns.Add("strValor", typeof(string));
            tblDatosPax.Columns.Add("strTarifa", typeof(string));
            tblDatosPax.Columns.Add("strTipoMoneda", typeof(string));
            tblDatosPax.Columns.Add("strDocumento", typeof(string));
            tblDatosPax.AcceptChanges();
        }

        private void ModificarTbltax(DataSet dsReservas)
        {
            DataTable tblDatosTax = dsReservas.Tables[csReservas.TABLA_TAXFARE];
            tblDatosTax.Columns["intConsecRes"].DataType = typeof(int);
            tblDatosTax.Columns["intCodigoTax"].DataType = typeof(int);
            //DataColumn[] dtPrimaryKeys = { tblDatosTax.Columns["intCodigoTax"] };
            //tblDatosTax.PrimaryKey = dtPrimaryKeys;
            tblDatosTax.AcceptChanges();
        }

        private void ModificarTblTarifa(DataSet dsReservas)
        {
            DataTable TblTarifa = dsReservas.Tables[csReservas.TABLA_TARIFA];
            TblTarifa.Columns["intConsecRes"].DataType = intConsecutivo.GetType();
            TblTarifa.Columns.Add("StrTipoMoneda", StrTipoMoneda.GetType());
            TblTarifa.Columns.Add("StrTipoPax", typeof(string));
            TblTarifa.AcceptChanges();
        }

        private void ModificarTablaRemark(DataSet dsReservas)
        {
            DataTable dtRemark = dsReservas.Tables[csReservas.TABLA_REMARK];
            dtRemark.Columns["intConsecRes"].DataType = intConsecutivo.GetType();
            dtRemark.Columns["strRecord"].DataType = StrRecordRemark.GetType();
            dtRemark.Columns["intidrefere"].DataType = IntIdRefereRemark.GetType();
            dtRemark.Columns["intConsecutivo"].DataType = intConsecutivoRemark.GetType();
            dtRemark.Columns["strDescripcion"].DataType = StrDescripcionRemark.GetType();
            dtRemark.Columns["strDetalle"].DataType = StrDetalleRemark.GetType();
        }

        private DataTable CrearTablaGeneral(string strNombreCarroDeCompras)
        {
            DataTable tbldatos = new DataTable();

            tbldatos.TableName = strNombreCarroDeCompras;

            tbldatos.Columns.Add("intConsecRes", typeof(int));

            //tbldatos.Columns["intConsecRes"].AutoIncrement = true;

            //tbldatos.Columns["intConsecRes"].AutoIncrementSeed = 1;

            //tbldatos.Columns["intConsecRes"].AutoIncrementStep = 1;

            DataColumn[] dtPrimaryKeys = { tbldatos.Columns["intConsecRes"] };

            tbldatos.PrimaryKey = dtPrimaryKeys;

            tbldatos.Columns.Add("IntValorTotal", typeof(String));

            tbldatos.Columns.Add("IntCodigoTarifa", typeof(int));

            tbldatos.Columns.Add("IntNumeroHabitaciones", typeof(int));

            tbldatos.Columns.Add("IntcantidadPersonas", typeof(int));

            tbldatos.Columns.Add("StrLocalizadorExt", typeof(string));

            tbldatos.Columns.Add("IntCapacidad", typeof(int));

            tbldatos.Columns.Add("IntCapacidadMaxima", typeof(int));

            tbldatos.Columns.Add("IntNumeroDias", typeof(int));

            tbldatos.Columns.Add("IntNumeroNoches", typeof(int));

            tbldatos.Columns.Add("IntNochesAdicionales", typeof(int));

            tbldatos.Columns.Add("StrIdentificadorDelPlan", typeof(String));

            tbldatos.Columns.Add("StrPasajeros", typeof(String));

            tbldatos.Columns.Add("StrFechaInicial", typeof(String));

            tbldatos.Columns.Add("StrHoraFin", typeof(String));

            tbldatos.Columns.Add("StrHoraIni", typeof(String));

            tbldatos.Columns.Add("StrFechaFinal", typeof(String));

            tbldatos.Columns.Add("StrFechaVencimiento", typeof(String));

            tbldatos.Columns.Add("StrNombrePlan", typeof(String));

            tbldatos.Columns.Add("StrImagen", typeof(String));

            tbldatos.Columns.Add("StrDescripcion", typeof(String));

            tbldatos.Columns.Add("StrRestricciones", typeof(String));

            tbldatos.Columns.Add("StrIncluye", typeof(String));

            tbldatos.Columns.Add("StrNoIncluye", typeof(String));

            tbldatos.Columns.Add("StrBeneFicios", typeof(String));

            tbldatos.Columns.Add("StrEncuenta", typeof(String));

            tbldatos.Columns.Add("StrObservacion", typeof(String));

            tbldatos.Columns.Add("StrDetalles", typeof(String));

            tbldatos.Columns.Add("StrRuta", typeof(String));

            tbldatos.Columns.Add("StrUbicacion", typeof(String));

            tbldatos.Columns.Add("StrTipoPlan", typeof(String));

            tbldatos.Columns.Add("IntTipoPlan", typeof(int));

            tbldatos.Columns.Add("StrSubTipoPlan", typeof(String));

            tbldatos.Columns.Add("IntSubTipoPlan", typeof(int));

            tbldatos.Columns.Add("StrPais", typeof(String));

            tbldatos.Columns.Add("IntPais", typeof(int));

            tbldatos.Columns.Add("StrCiudad", typeof(String));

            tbldatos.Columns.Add("IntCiudad", typeof(int));

            tbldatos.Columns.Add("StrTemporada", typeof(String));

            tbldatos.Columns.Add("IntTemporada", typeof(string));

            tbldatos.Columns.Add("StrCategoria", typeof(String));

            tbldatos.Columns.Add("IntCategoria", typeof(int));

            tbldatos.Columns.Add("StrAcomodacion", typeof(String));

            tbldatos.Columns.Add("IntAcomodacion", typeof(int));

            tbldatos.Columns.Add("StrTipoHabitacion", typeof(String));

            tbldatos.Columns.Add("IntTipoHabitacion", typeof(int));

            tbldatos.Columns.Add("StrServicio", typeof(String));

            tbldatos.Columns.Add("IntServicio", typeof(int));

            tbldatos.Columns.Add("StrTipoServicio", typeof(String));

            tbldatos.Columns.Add("IntTipoServicio", typeof(int));

            tbldatos.Columns.Add("StrZonaGeografica", typeof(String));

            tbldatos.Columns.Add("IntZonaGeografica", typeof(int));

            tbldatos.Columns.Add("StrTipoPasajero", typeof(String));

            tbldatos.Columns.Add("IntTipoPasajero", typeof(int));

            tbldatos.Columns.Add("StrTipoMoneda", typeof(String));

            tbldatos.Columns.Add("IntTipoMoneda", typeof(int));

            tbldatos.Columns.Add("StrSeccionPublicacion", typeof(String));

            tbldatos.Columns.Add("IntSeccionPublicacion", typeof(int));

            tbldatos.Columns.Add("StrTipoPropiedad", typeof(String));

            tbldatos.Columns.Add("IntTipoPropiedad", typeof(int));

            tbldatos.Columns.Add("StrOrigen", typeof(String));

            tbldatos.Columns.Add("strReserva", typeof(String));

            tbldatos.Columns.Add("IntOrigen", typeof(int));

            tbldatos.Columns.Add("StrDestino", typeof(String));

            tbldatos.Columns.Add("IntDestino", typeof(int));

            tbldatos.Columns.Add("IntConfirmacion", typeof(int));

            tbldatos.Columns.Add("BolImpuestos", typeof(bool));

            tbldatos.Columns.Add("IntConvenio", typeof(int));

            tbldatos.Columns.Add("StrConvenioCorp", typeof(String));

            tbldatos.Columns.Add("StrDuracion", typeof(String));

            tbldatos.Columns.Add("StrCantidadPersonas", typeof(String));

            tbldatos.Columns.Add("StrRegimen", typeof(String));

            tbldatos.Columns.Add("IntValorOferta", typeof(String));

            tbldatos.Columns.Add("DblValorBono", typeof(decimal));

            tbldatos.Columns.Add("DblValorCalculoBono", typeof(decimal));

            tbldatos.AcceptChanges();

            return tbldatos;
        }

        private DataTable CrearTablaDatosVisibles()
        {
            DataTable tbldatosVisibles = new DataTable();

            tbldatosVisibles.TableName = "TablaDatosVisibles";

            tbldatosVisibles.Columns.Add("intConsecRes", typeof(int));

            DataColumn[] dtPrimaryKeys = { tbldatosVisibles.Columns["intConsecRes"] };

            tbldatosVisibles.PrimaryKey = dtPrimaryKeys;

            tbldatosVisibles.Columns.Add("NombrePlan", typeof(string));

            tbldatosVisibles.Columns.Add("ValorTotalPlan", typeof(string));

            tbldatosVisibles.Columns.Add("TipoMoneda", typeof(string));

            tbldatosVisibles.Columns.Add("DatoUno", typeof(string));

            tbldatosVisibles.Columns.Add("DatoDos", typeof(string));

            tbldatosVisibles.Columns.Add("DatoTres", typeof(string));

            tbldatosVisibles.Columns.Add("DatoCuatro", typeof(string));

            tbldatosVisibles.Columns.Add("DatoCinco", typeof(string));

            tbldatosVisibles.Columns.Add("DatoSeis", typeof(string));

            tbldatosVisibles.Columns.Add("DatoSiete", typeof(string));

            tbldatosVisibles.Columns.Add("DatoOcho", typeof(string));

            tbldatosVisibles.Columns.Add("DatoNueve", typeof(string));

            tbldatosVisibles.AcceptChanges();

            return tbldatosVisibles;
        }

        private DataTable CrearTablaHabitaciones()
        {
            DataTable tblDatosHabitaciones = new DataTable();
            tblDatosHabitaciones.TableName = "tblHabitaciones";
            tblDatosHabitaciones.Columns.Add("intConsecRes", typeof(int));

            tblDatosHabitaciones.Columns.Add("intNumeroHabitacion", typeof(int));
            tblDatosHabitaciones.Columns["intNumeroHabitacion"].AutoIncrement = true;
            tblDatosHabitaciones.Columns["intNumeroHabitacion"].AutoIncrementSeed = 1;
            tblDatosHabitaciones.Columns["intNumeroHabitacion"].AutoIncrementStep = 1;
            tblDatosHabitaciones.Columns.Add("intCantPersonas", typeof(int));
            tblDatosHabitaciones.Columns.Add("intAcomodacion", typeof(int));
            tblDatosHabitaciones.Columns.Add("intTipoHabitacion", typeof(int));
            tblDatosHabitaciones.AcceptChanges();
            return tblDatosHabitaciones;
        }

        public void GuardarDataSetOActualizarDataSet(string IdentificadorUnico, DataSet dsDatos)
        {
            clsSerializer dsSerializado = new clsSerializer();
            dsSerializado.DatasetXML(dsDatos, IdentificadorUnico);
        }

        public void GuardarTablaOActualizarTabla(string IdentificadorUnico, DataTable tbldatos, string strNombreTabla)
        {
            DataSet dsTablaDatosCarrito = new DataSet();
            DataTable tblDatosCarrito = new DataTable();
            try
            {
                tbldatos.DataSet.Tables.Remove(tbldatos);
            }
            catch (Exception)
            { }
            try
            {
                dsTablaDatosCarrito = RecuperarDataSet(IdentificadorUnico);
                try
                {
                    dsTablaDatosCarrito.Tables.Remove(strNombreTabla);
                }
                catch (Exception)
                { }

                tbldatos.TableName = strNombreTabla;
                dsTablaDatosCarrito.Tables.Add(tbldatos);
                GuardarDataSetOActualizarDataSet(IdentificadorUnico, dsTablaDatosCarrito);
            }
            catch (Exception)
            {
                dsTablaDatosCarrito = new DataSet();
                tbldatos.TableName = strNombreTabla;
                dsTablaDatosCarrito.Tables.Add(tbldatos);
                GuardarDataSetOActualizarDataSet(IdentificadorUnico, dsTablaDatosCarrito);
            }
        }

        public DataTable RecuperarTabla(string IdentificadorUnico, string nombreTabla)
        {
            DataTable tbldatos = new DataTable();
            try
            {
                clsSerializer dsSerializado = new clsSerializer();
                DataSet dsDatosSession = RecuperarDataSet(IdentificadorUnico);
                tbldatos = dsDatosSession.Tables[nombreTabla];
            }
            catch (Exception)
            {
                tbldatos = null;
            }
            return tbldatos;
        }

        public DataTable RecuperarTabla()
        {
            DataTable tbldatos = new DataTable();
            try
            {
                clsSerializer dsSerializado = new clsSerializer();
                DataSet dsDatosSession = RecuperarDataSet(_strIdentificadorUnico);
                tbldatos = dsDatosSession.Tables[_strNombreCarroDeCompras];
            }
            catch (Exception)
            {
                tbldatos = null;
            }
            return tbldatos;
        }

        private DataSet RecuperarDataSet(string IdentificadorUnico)
        {
            DataSet dsDatos = new DataSet();
            try
            {
                clsSerializer dsSerializado = new clsSerializer();
                dsDatos = dsSerializado.XMLDataset(IdentificadorUnico);
            }
            catch (Exception)
            {
                dsDatos = null;
            }
            return dsDatos;
        }

        private void InitializeEstructura(DataSet dsExtructuraGeneral)
        {
            for (int d = 0; d < dsExtructuraGeneral.Tables.Count; d++)
            {
                for (int c = 0; c < dsExtructuraGeneral.Tables[d].Columns.Count; c++)
                {
                    if (dsExtructuraGeneral.Tables[d].Columns[c].DataType == typeof(int) && dsExtructuraGeneral.Tables[d].Columns[c].AutoIncrement == false)
                    {
                        dsExtructuraGeneral.Tables[d].Columns[c].DefaultValue = 0;
                    }
                    else if (dsExtructuraGeneral.Tables[d].Columns[c].DataType == typeof(string))
                    {
                        dsExtructuraGeneral.Tables[d].Columns[c].DefaultValue = "";
                    }
                }
            }
            dsExtructuraGeneral.AcceptChanges();
        }

        private void SaveCarrito(DataTable tblDatosCarrito)
        {

            DataRow dtNewRow = tblDatosCarrito.NewRow();

            if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                dtNewRow["IntValorTotal"] = clsValidaciones.Redondeo(clsValidaciones.getDecimal(IntValorTotal)).ToString();
            else
                dtNewRow["IntValorTotal"] = clsValidaciones.getDecimalNotRound(IntValorTotal).ToString();
            try
            {
                dtNewRow["IntValorOferta"] = clsValidaciones.Redondeo(clsValidaciones.getDecimal(IntValorOferta)).ToString();
            }
            catch { }
            dtNewRow["IntCodigoTarifa"] = clsValidaciones.Redondeo(clsValidaciones.getDecimal(IntValorTotal)).ToString();
            dtNewRow["IntNumeroHabitaciones"] = IntNumeroHabitaciones;
            dtNewRow["IntcantidadPersonas"] = IntcantidadPersonas;
            dtNewRow["StrLocalizadorExt"] = StrLocalizadorExt;
            dtNewRow["IntCapacidad"] = IntCapacidad;
            dtNewRow["IntCapacidadMaxima"] = IntCapacidadMaxima;
            dtNewRow["IntNumeroDias"] = IntNumeroDias;
            dtNewRow["IntNumeroNoches"] = IntNumeroNoches;
            dtNewRow["IntNochesAdicionales"] = IntNochesAdicionales;
            dtNewRow["StrPasajeros"] = StrPasajeros;
            dtNewRow["StrIdentificadorDelPlan"] = StrIdentificadorDelPlan;
            dtNewRow["StrFechaInicial"] = StrFechaInicial;
            dtNewRow["StrFechaFinal"] = StrFechaFinal;
            dtNewRow["StrFechaVencimiento"] = StrFechaVencimiento;
            dtNewRow["StrNombrePlan"] = StrNombrePlan;
            dtNewRow["StrImagen"] = StrImagen;
            dtNewRow["StrDescripcion"] = StrDescripcion;
            dtNewRow["StrRestricciones"] = StrRestricciones;
            dtNewRow["StrIncluye"] = StrIncluye;
            dtNewRow["StrBeneFicios"] = StrBeneFicios;
            dtNewRow["StrNoIncluye"] = StrNoIncluye;
            dtNewRow["StrBeneFicios"] = StrBeneFicios;
            dtNewRow["StrEncuenta"] = StrEncuenta;
            dtNewRow["StrDetalles"] = StrDetalles;
            dtNewRow["StrRuta"] = StrRuta;
            dtNewRow["StrUbicacion"] = StrUbicacion;
            dtNewRow["StrObservacion"] = StrUbicacion;
            dtNewRow["strReserva"] = StrCodigoReserva;

            dtNewRow["StrTipoPlan"] = StrTipoPlan;
            dtNewRow["IntTipoPlan"] = IntTipoPlan;

            dtNewRow["StrSubTipoPlan"] = StrSubTipoPlan;
            dtNewRow["IntSubTipoPlan"] = IntSubTipoPlan;

            dtNewRow["StrPais"] = StrPais;
            dtNewRow["IntPais"] = IntPais;

            dtNewRow["StrCiudad"] = StrCiudad;
            dtNewRow["IntCiudad"] = IntCiudad;

            dtNewRow["StrTemporada"] = StrTemporada;
            dtNewRow["IntTemporada"] = IntTemporada;

            dtNewRow["StrCategoria"] = StrCategoria;
            dtNewRow["IntCategoria"] = IntCategoria;

            dtNewRow["StrAcomodacion"] = StrAcomodacion;
            dtNewRow["IntAcomodacion"] = IntAcomodacion;

            dtNewRow["StrTipoHabitacion"] = StrTipoHabitacion;
            dtNewRow["IntTipoHabitacion"] = IntTipoHabitacion;

            dtNewRow["StrServicio"] = StrServicio;
            dtNewRow["IntServicio"] = IntServicio;

            dtNewRow["StrTipoServicio"] = StrTipoServicio;
            dtNewRow["IntTipoServicio"] = IntTipoServicio;

            dtNewRow["StrZonaGeografica"] = StrZonaGeografica;
            dtNewRow["IntZonaGeografica"] = IntZonaGeografica;

            dtNewRow["StrTipoPasajero"] = StrTipoPasajero;
            dtNewRow["IntTipoPasajero"] = IntTipoPasajero;

            dtNewRow["StrTipoMoneda"] = StrTipoMoneda;
            dtNewRow["IntTipoMoneda"] = IntTipoMoneda;

            dtNewRow["StrSeccionPublicacion"] = StrSeccionPublicacion;
            dtNewRow["IntSeccionPublicacion"] = IntSeccionPublicacion;

            dtNewRow["StrTipoPropiedad"] = StrTipoPropiedad;
            dtNewRow["IntTipoPropiedad"] = IntTipoPropiedad;

            dtNewRow["StrOrigen"] = StrOrigen;
            dtNewRow["IntOrigen"] = IntOrigen;

            dtNewRow["StrDestino"] = StrDestino;
            dtNewRow["IntDestino"] = IntDestino;

            dtNewRow["intConsecRes"] = this.intConsecutivo;

            dtNewRow["IntConfirmacion"] = IntConfirmacion;

            dtNewRow["BolImpuestos"] = BolImpuestos;

            try
            { dtNewRow["IntConvenio"] = IntConvenio; }
            catch (Exception)
            {
                try
                { dtNewRow["IntConvenio"] = "0"; }
                catch (Exception) { }
            }

            dtNewRow["StrConvenioCorp"] = StrConvenioCorp;

            dtNewRow["StrDuracion"] = StrDuracion;

            dtNewRow["StrCantidadPersonas"] = StrCantidadPersonas;

            dtNewRow["StrRegimen"] = StrRegimen;

            dtNewRow["intConsecRes"] = IntConsecutivo;

            tblDatosCarrito.Rows.Add(dtNewRow);

            tblDatosCarrito.AcceptChanges();
        }
        private void ModificarTblReserva(DataTable tblDatosReserva)
        {
            try
            {
                tblDatosReserva.Rows[0]["dtmFechaFin"] = StrFechaFinal;
                tblDatosReserva.AcceptChanges();
            }
            catch { }
        }
        private void ModificarTblCarrito(DataTable tblDatosReserva)
        {
            try
            {
                tblDatosReserva.Rows[0]["StrFechaFinal"] = StrFechaFinal;
                tblDatosReserva.AcceptChanges();
            }
            catch { }
        }
        private void SaveTblReserva(DataTable tblDatosReserva)
        {

            DataRow drNewRow = tblDatosReserva.NewRow();

            drNewRow["intConsecRes"] = this.intConsecutivo;

            drNewRow["intCodigoPlan"] = IntCodigoPlan;

            drNewRow["intTipoPlan"] = IntTipoPlan;

            drNewRow["strLocalizadorExt"] = StrLocalizadorExt;

            drNewRow["dtmFechaIni"] = StrFechaInicial;

            drNewRow["dtmFechaFin"] = StrFechaFinal;

            drNewRow["dtmFecha"] = DateTime.Now.ToString("yyyy/MM/dd");

            drNewRow["dtmVencimiento"] = StrFechaVencimiento;

            drNewRow["dtmFechaLimitePago"] = StrFechaFinal;

            drNewRow["strObservacion"] = StrObservacion;

            drNewRow["strReserva"] = StrCodigoReserva;

            drNewRow["strCodigo"] = StrCodigo;

            drNewRow["intConvenio"] = IntConvenio;

            drNewRow["strConvenioCorp"] = StrConvenioCorp;

            drNewRow["strDuracion"] = StrDuracion;

            tblDatosReserva.Rows.Add(drNewRow);

            tblDatosReserva.AcceptChanges();
        }

        private void SaveTblTransac(DataTable tblDatosTransac)
        {

            if (IntSegmento != null)
            {
                if (!IntSegmento.Equals(""))
                {
                    if (!IntSegmento.Equals("1") && !IntSegmento.Equals("0"))
                    {
                        int _intSegmento = Convert.ToInt32(IntSegmento);


                        DataRow dtNewRow = tblDatosTransac.NewRow();

                        dtNewRow["intConsecRes"] = IntConsecutivo;

                        dtNewRow["strReserva"] = StrCodigoReserva;

                        dtNewRow["intTipoPlan"] = IntTipoPlan;

                        dtNewRow["intCodigoPlan"] = IntCodigoPlan;

                        dtNewRow["intSegmento"] = IntSegmento;

                        dtNewRow["intOrigen"] = IntOrigen;

                        dtNewRow["intDestino"] = IntDestino;

                        dtNewRow["dtmFechaIni"] = StrFechaInicial;

                        dtNewRow["dtmFechaFin"] = StrFechaFinal;

                        dtNewRow["strHoraIni"] = StrHoraIni;

                        dtNewRow["strHoraFin"] = StrHoraFin;

                        dtNewRow["intProveedor"] = IntProveedor;

                        dtNewRow["intCantidadPersonas"] = IntcantidadPersonas;

                        dtNewRow["intTipoAcomodacion"] = IntAcomodacion;

                        dtNewRow["intTipoHabitacion"] = IntTipoHabitacion;

                        dtNewRow["strOperador"] = StrOperador;

                        dtNewRow["strConfirma"] = StrConfirmacion;

                        dtNewRow["strObservacion"] = StrObservacion;

                        dtNewRow["intEstado"] = IntEstado;

                        dtNewRow["strCodigo"] = StrCodigo;

                        dtNewRow["strOrigen"] = StrOrigen;

                        dtNewRow["strDestino"] = StrDestino;

                        dtNewRow["strTipoHabitacion"] = StrTipoHabitacion;

                        dtNewRow["strTipoAcomodacion"] = StrAcomodacion;

                        dtNewRow["strCantidadPersonas"] = StrCantidadPersonas;

                        dtNewRow["strRegimen"] = StrRegimen;

                        dtNewRow["StrSubTipoHab"] = StrTipoPropiedad;

                        dtNewRow["dblValor"] = IntValorTotal;

                        dtNewRow["StrProveedor"] = StrProveedor;

                        dtNewRow["intIdVigencia"] = IntVigencia;

                        dtNewRow["StrHoteles"] = StrHoteles;

                        tblDatosTransac.Rows.Add(dtNewRow);

                    }
                    else
                    {

                        DataRow dtNewRow = tblDatosTransac.NewRow();

                        dtNewRow["intConsecRes"] = IntConsecutivo;

                        dtNewRow["strReserva"] = StrCodigoReserva;

                        dtNewRow["intTipoPlan"] = IntTipoPlan;

                        dtNewRow["intCodigoPlan"] = IntCodigoPlan;

                        dtNewRow["intSegmento"] = IntSegmento;

                        dtNewRow["intOrigen"] = IntOrigen;

                        dtNewRow["intDestino"] = IntDestino;

                        dtNewRow["dtmFechaIni"] = StrFechaInicial;

                        dtNewRow["dtmFechaFin"] = StrFechaFinal;

                        dtNewRow["strHoraIni"] = StrHoraIni;

                        dtNewRow["strHoraFin"] = StrHoraFin;

                        dtNewRow["intProveedor"] = IntProveedor;

                        dtNewRow["intCantidadPersonas"] = IntcantidadPersonas;

                        dtNewRow["intTipoAcomodacion"] = IntAcomodacion;

                        dtNewRow["intTipoHabitacion"] = IntTipoHabitacion;

                        dtNewRow["strOperador"] = StrOperador;

                        dtNewRow["strConfirma"] = StrConfirmacion;

                        dtNewRow["strObservacion"] = StrObservacion;

                        dtNewRow["intEstado"] = IntEstado;

                        dtNewRow["strCodigo"] = StrCodigo;

                        dtNewRow["strOrigen"] = StrOrigen;

                        dtNewRow["strDestino"] = StrDestino;

                        dtNewRow["strTipoHabitacion"] = StrTipoHabitacion;

                        dtNewRow["strTipoAcomodacion"] = StrAcomodacion;

                        dtNewRow["strCantidadPersonas"] = StrCantidadPersonas;

                        dtNewRow["strRegimen"] = StrRegimen;

                        dtNewRow["StrSubTipoHab"] = StrTipoPropiedad;

                        dtNewRow["dblValor"] = IntValorTotal;

                        dtNewRow["StrProveedor"] = StrProveedor;

                        dtNewRow["intIdVigencia"] = IntVigencia;

                        dtNewRow["StrHoteles"] = StrHoteles;

                        tblDatosTransac.Rows.Add(dtNewRow);
                    }
                }
            }
            tblDatosTransac.AcceptChanges();
        }
        public DataSet CrearEstructuraReservasExterna()
        {
            this.dsReservas = cReserva.CrearTablaReserva();
            this.ModificarTblReserva(dsReservas);
            this.ModificarTblTransac(dsReservas);
            this.ModificarTblPax(dsReservas);
            this.ModificarTbltax(dsReservas);
            this.ModificarTblTarifa(dsReservas);
            this.ModificarTablaRemark(dsReservas);

            InitializeEstructura(dsReservas);
            dsReservas.AcceptChanges();
            return dsReservas;
        }
        private DataSet CrearEstructuraReservas()
        {
            DataTable tblDatosCarrito = CrearTablaGeneral(this._strNombreCarroDeCompras);

            DataTable tblDatosVisibles = CrearTablaDatosVisibles();

            this.dsReservas = cReserva.CrearTablaReserva();

            string strNombreCarrito = tblDatosCarrito.TableName;

            this.tblDatosHabitaciones = this.CrearTablaHabitaciones();

            this.ModificarTblReserva(dsReservas);

            this.ModificarTblTransac(dsReservas);

            this.ModificarTblPax(dsReservas);

            this.ModificarTbltax(dsReservas);

            this.ModificarTblTarifa(dsReservas);

            this.ModificarTablaRemark(dsReservas);

            dsReservas.Tables.Add(tblDatosCarrito);

            dsReservas.Tables.Add(tblDatosHabitaciones);

            dsReservas.Tables.Add(tblDatosVisibles);

            string strRelacion = default(string);

            InitializeEstructura(dsReservas);

            for (int t = 0; t < dsReservas.Tables.Count; t++)
            {
                if (!dsReservas.Tables[t].TableName.Equals(strNombreCarrito))
                {
                    try
                    {
                        strRelacion = strNombreCarrito + "_" + dsReservas.Tables[t].TableName;
                        DataRelation dtRelaRes = new DataRelation(strRelacion, dsReservas.Tables[strNombreCarrito].Columns["intConsecRes"], dsReservas.Tables[t].Columns["intConsecRes"]);
                        dsReservas.Relations.Add(dtRelaRes);
                    }
                    catch { }
                }
            }
            dsReservas.AcceptChanges();

            return dsReservas;
        }

        private DataTable Distinto(DataTable tablaOrigen, params string[] columnas)
        {
            DataView vista = new DataView(tablaOrigen);

            return vista.ToTable(true, columnas);
        }

        #endregion

        #region[METODOS PÚBLICOS]

        /// <summary>
        /// Llena un control de servidor con los datos que lleva el carrito de compras
        /// </summary>
        /// <param name="ControlLLenado">Puede ser un Repeater o un GridView </param>
        /// <param name="strIdentificadorUnico">Nombre con el cual se identifica el archivo del carrito de compras tiene que ser unico por cada session</param>
        /// <param name="strNombreCarroDeCompras">Nombre con el cual va se llamo al carrito de compras en el metodo constructor </param>
        public void VaciarEnControl(Control ControlLLenado)
        {
            if (ControlLLenado is Repeater)
            {
                ((Repeater)ControlLLenado).DataSource = RecuperarTabla(this._strIdentificadorUnico, "TablaDatosVisibles");
                ((Repeater)ControlLLenado).DataBind();
            }
            else if (ControlLLenado is GridView)
            {
                ((GridView)ControlLLenado).DataSource = RecuperarTabla(this._strIdentificadorUnico, "TablaDatosVisibles");
                ((GridView)ControlLLenado).DataBind();
            }
        }
        /// <summary>
        /// Elimina un plan del carrito de compras
        /// </summary>
        /// <param name="strIdentificadorUnico">Nombre con el cual se identifica el archivo del carrito de compras tiene que ser unico por cada session</param></param>
        /// <param name="strNombreCarroDeCompras">Nombre con el cual va se llamo al carrito de compras en el metodo constructor </param>
        /// <param name="strCodigoDeReferencia">Es un numero que debe coincidir con el consecutivo de reserva que esta en un campo del carrito de compras</param>
        /// <returns></returns>
        public int EliminarItemDelCarrito(string strCodigoDeReferencia)
        {
            if (int.Parse(strCodigoDeReferencia) > 0)
                strCodigoDeReferencia = (int.Parse(strCodigoDeReferencia) - 1).ToString();

            int iCod = Convert.ToInt32(strCodigoDeReferencia);
            //bool blConfirmacion = false;
            bool blConfirmacion = true;

            int intCodigoDeReservaHotelBookHotel = 0;

            this.dsReservas = RecuperarDataSet(this._strIdentificadorUnico);

            DataTable tblDatos = dsReservas.Tables[this._strNombreCarroDeCompras];

            //blConfirmacion = tblDatos.Rows.Contains(strCodigoDeReferencia);

            if (tblDatos.Rows.Count > 0 && tblDatos.Rows.Count >= iCod)
            {
                if (blConfirmacion)
                {
                    try
                    {
                        intCodigoDeReservaHotelBookHotel = Convert.ToInt32(tblDatos.Rows[iCod]["strReserva"]);
                    }
                    catch (Exception)
                    {
                        intCodigoDeReservaHotelBookHotel = 0;
                    }
                    tblDatos.Rows[iCod].Delete();
                    tblDatos.AcceptChanges();
                }
            }

            dsReservas.AcceptChanges();

            GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, dsReservas);

            return intCodigoDeReservaHotelBookHotel;
        }
        /// <summary>
        /// Inserta  un pasajero que esta asociado a un plan del carrito de compras,  
        /// solo se insertan los valores que son diferentes de null
        /// </summary>        
        /// </param>
        /// <param name="strNombrePasajero">Campo opcional se puede enviar null</param>
        /// <param name="strFechaNacimiento">Campo opcional se puede enviar null</param>
        /// <param name="strTipoPasajero">Campo opcional se puede enviar null</param>
        /// <param name="IntTipoPasajero">Campo opcional se puede enviar null</param>
        /// <param name="intEdad">Campo opcional se puede enviar null</param>
        /// <param name="intValorPasajero">Campo opcional se puede enviar null</param>
        /// 
        public void SavePerson(string strNombrePasajero, string strFechaNacimiento, string strTipoPasajero, int? IntTipoPasajero, int? intEdad, int? intValorPasajero)
        {
            SavePerson(strNombrePasajero, strFechaNacimiento, strTipoPasajero, IntTipoPasajero, intEdad, intValorPasajero, null);
        }

        public void SavePerson(string strNombrePasajero, string strFechaNacimiento, string strTipoPasajero, int? IntTipoPasajero, int? intEdad, int? intValorPasajero, String strTelefono)
        {
            DataTable tblDatosPax = dsReservas.Tables["tblPax"];

            DataRow drNewRow = tblDatosPax.NewRow();

            drNewRow["intConsecRes"] = this.intConsecutivo;

            drNewRow["strReserva"] = StrCodigoReserva;

            drNewRow["dtmFechaNac"] = strFechaNacimiento;

            drNewRow["strTipoPax"] = strTipoPasajero;

            drNewRow["strNombre"] = strNombrePasajero;

            drNewRow["intTipoPax"] = IntTipoPasajero;

            if (intEdad == null)
            {
                drNewRow["intEdad"] = 0;
            }
            else
            {
                drNewRow["intEdad"] = intEdad;
            }

            drNewRow["strCodigo"] = StrCodigo;

            drNewRow["strValor"] = intValorPasajero;

            tblDatosPax.Rows.Add(drNewRow);

            tblDatosPax.AcceptChanges();

            this.tblDatosPasajeros = tblDatosPax;

        }

        public void SavePerson(string strNombrePasajero, string strFechaNacimiento, string strTipoPasajero, int? IntTipoPasajero, int? intEdad, int? intValorPasajero, String strTelefono, int iSegmento, int? intValorTarifaPasajero)
        {
            DataTable tblDatosPax = dsReservas.Tables["tblPax"];

            DataRow drNewRow = tblDatosPax.NewRow();

            drNewRow["intConsecRes"] = this.intConsecutivo;

            drNewRow["strReserva"] = StrCodigoReserva;

            drNewRow["dtmFechaNac"] = strFechaNacimiento;

            drNewRow["strTipoPax"] = strTipoPasajero;

            drNewRow["strNombre"] = strNombrePasajero;

            drNewRow["intTipoPax"] = IntTipoPasajero;

            if (intEdad == null)
            {
                drNewRow["intEdad"] = 0;
            }
            else
            {
                drNewRow["intEdad"] = intEdad;
            }

            drNewRow["strCodigo"] = StrCodigo;

            if (intValorPasajero != 0)
                drNewRow["strValor"] = Convert.ToDecimal(intValorPasajero).ToString("###,###.###");
            else
                drNewRow["strValor"] = "0";

            if (intValorTarifaPasajero != 0)
                drNewRow["strTarifa"] = Convert.ToDecimal(intValorTarifaPasajero).ToString("###,###.###");
            else
                drNewRow["strTarifa"] = "0";

            drNewRow["intSegmento"] = iSegmento;

            drNewRow["strTipoMoneda"] = StrTipoMoneda;

            tblDatosPax.Rows.Add(drNewRow);

            tblDatosPax.AcceptChanges();

            this.tblDatosPasajeros = tblDatosPax;

        }

        public void SavePerson(string strNombrePasajero, string strFechaNacimiento, string strTipoPasajero, int? IntTipoPasajero, int? intEdad, decimal? intValorPasajero, String strTelefono, int iSegmento, decimal? intValorTarifaPasajero)
        {
            DataTable tblDatosPax = dsReservas.Tables["tblPax"];

            DataRow drNewRow = tblDatosPax.NewRow();

            drNewRow["intConsecRes"] = this.intConsecutivo;

            drNewRow["strReserva"] = StrCodigoReserva;

            drNewRow["dtmFechaNac"] = strFechaNacimiento;

            drNewRow["strTipoPax"] = strTipoPasajero;

            drNewRow["strNombre"] = strNombrePasajero;

            drNewRow["intTipoPax"] = IntTipoPasajero;

            if (intEdad == null)
            {
                drNewRow["intEdad"] = 0;
            }
            else
            {
                drNewRow["intEdad"] = intEdad;
            }

            drNewRow["strCodigo"] = StrCodigo;

            if (intValorPasajero != 0)
                drNewRow["strValor"] = Convert.ToDecimal(intValorPasajero).ToString("###,##0.00");
            else
                drNewRow["strValor"] = "0";

            if (intValorTarifaPasajero != 0)
                drNewRow["strTarifa"] = Convert.ToDecimal(intValorTarifaPasajero).ToString("###,##0.00");
            else
                drNewRow["strTarifa"] = "0";

            drNewRow["intSegmento"] = iSegmento;

            drNewRow["strTipoMoneda"] = StrTipoMoneda;

            tblDatosPax.Rows.Add(drNewRow);

            tblDatosPax.AcceptChanges();

            this.tblDatosPasajeros = tblDatosPax;

        }

        /// <summary>
        /// actualiza un pasajero que esta asociado a un plan del carrito de compras,  
        /// solo se actualizan  los valores que son diferentes de null
        /// </summary>
        /// <param name="intIdPasajero">Numero que identifica a un pasajero,si se envia null inserta y si se envia con un numero lo actualiza el pasajero
        /// </param>
        /// <param name="strNombrePasajero">Campo opcional se puede enviar null</param>
        /// <param name="strFechaNacimiento">Campo opcional se puede enviar null</param>
        /// <param name="strTipoPasajero">Campo opcional se puede enviar null</param>
        /// <param name="IntTipoPasajero">Campo opcional se puede enviar null</param>
        /// <param name="intEdad">Campo opcional se puede enviar null</param>
        /// <param name="intValorPasajero">Campo opcional se puede enviar null</param>
        public void UpdatePerson(int intIdPasajero, string strNombrePasajero, string strFechaNacimiento, string strTipoPasajero, int? IntTipoPasajero, int? intEdad, int? intValorPasajero)
        {
            DataTable tblDatosPax = dsReservas.Tables["tblPax"];

            if (tblDatosPax.Rows.Contains(intIdPasajero))
            {
                DataRow dtrPax = tblDatosPax.Rows.Find(intIdPasajero);

                if (strFechaNacimiento != null)
                {
                    dtrPax["dtmFechaNac"] = strFechaNacimiento;
                }
                if (strTipoPasajero != null)
                {
                    dtrPax["strTipoPax"] = strTipoPasajero;
                }
                if (strNombrePasajero != null)
                {
                    dtrPax["strNombre"] = strNombrePasajero;
                }
                if (IntTipoPasajero != null)
                {
                    dtrPax["intTipoPax"] = IntTipoPasajero;
                }
                if (intEdad != null)
                {
                    dtrPax["intEdad"] = intEdad;
                }
                if (StrCodigoReserva != null)
                {
                    dtrPax["strReserva"] = StrCodigoReserva;
                }
                //if (StrCodigo != null)
                //{
                //    dtrPax["strCodigo"] = StrCodigo;
                //}
            }

            tblDatosPax.AcceptChanges();

            GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, this.dsReservas);

            this.tblDatosPasajeros = tblDatosPax;
        }
        public void UpdatePerson(int intIdPasajero, string strNombrePasajero, string strFechaNacimiento, string strTipoPasajero, int? IntTipoPasajero, int? intEdad, int? intValorPasajero,
            string sidGenero, string sNacionalidad, string sPasaporte, string sdtmFechaExpiracion, string sPaisResidencia)
        {
            DataTable tblDatosPax = dsReservas.Tables["tblPax"];

            if (tblDatosPax.Rows.Contains(intIdPasajero))
            {
                DataRow dtrPax = tblDatosPax.Rows.Find(intIdPasajero);

                if (strFechaNacimiento != null)
                {
                    dtrPax["dtmFechaNac"] = strFechaNacimiento;
                }
                if (strTipoPasajero != null)
                {
                    dtrPax["strTipoPax"] = strTipoPasajero;
                }
                if (strNombrePasajero != null)
                {
                    dtrPax["strNombre"] = strNombrePasajero;
                }
                if (IntTipoPasajero != null)
                {
                    dtrPax["intTipoPax"] = IntTipoPasajero;
                }
                if (intEdad != null)
                {
                    dtrPax["intEdad"] = intEdad;
                }
                if (StrCodigoReserva != null)
                {
                    dtrPax["strReserva"] = StrCodigoReserva;
                }
                if (sidGenero != null)
                {
                    dtrPax["intGenero"] = sidGenero;
                }
                if (sNacionalidad != null)
                {
                    dtrPax["strNacionalidad"] = sNacionalidad;
                }
                if (sPasaporte != null)
                {
                    dtrPax["strPasaporte"] = sPasaporte;
                }
                if (sdtmFechaExpiracion != null)
                {
                    dtrPax["dtmFechaExpPasaporte"] = sdtmFechaExpiracion;
                }
                if (sPaisResidencia != null)
                {
                    dtrPax["strPaisResidencia"] = sPaisResidencia;
                }

                //if (StrCodigo != null)
                //{
                //    dtrPax["strCodigo"] = StrCodigo;
                //}
            }

            tblDatosPax.AcceptChanges();

            GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, this.dsReservas);

            this.tblDatosPasajeros = tblDatosPax;
        }

        public void UpdatePerson(int intIdPasajero, string strNombrePasajero, string strFechaNacimiento, string strTipoPasajero, int? IntTipoPasajero, int? intEdad, int? intValorPasajero,
            string sidGenero, string sNacionalidad, string sPasaporte, string sdtmFechaExpiracion, string sPaisResidencia, string sTipoIdentificacion)
        {
            DataTable tblDatosPax = dsReservas.Tables["tblPax"];

            if (tblDatosPax.Rows.Contains(intIdPasajero))
            {
                DataRow dtrPax = tblDatosPax.Rows.Find(intIdPasajero);

                if (strFechaNacimiento != null)
                {
                    dtrPax["dtmFechaNac"] = strFechaNacimiento;
                }
                if (strTipoPasajero != null)
                {
                    dtrPax["strTipoPax"] = strTipoPasajero;
                }
                if (strNombrePasajero != null)
                {
                    dtrPax["strNombre"] = strNombrePasajero;
                }
                if (IntTipoPasajero != null)
                {
                    dtrPax["intTipoPax"] = IntTipoPasajero;
                }
                if (intEdad != null)
                {
                    dtrPax["intEdad"] = intEdad;
                }
                if (StrCodigoReserva != null)
                {
                    dtrPax["strReserva"] = StrCodigoReserva;
                }
                if (sidGenero != null)
                {
                    dtrPax["intGenero"] = sidGenero;
                }
                if (sNacionalidad != null)
                {
                    dtrPax["strNacionalidad"] = sNacionalidad;
                }
                if (sPasaporte != null)
                {
                    dtrPax["strDocumento"] = sPasaporte;
                }
                if (sdtmFechaExpiracion != null)
                {
                    dtrPax["dtmFechaExpPasaporte"] = sdtmFechaExpiracion;
                }
                if (sPaisResidencia != null)
                {
                    dtrPax["strPaisResidencia"] = sPaisResidencia;
                }
                if (sTipoIdentificacion != null)
                {
                    dtrPax["intTipoDoc"] = sTipoIdentificacion;
                }

                //if (StrCodigo != null)
                //{
                //    dtrPax["strCodigo"] = StrCodigo;
                //}
            }

            tblDatosPax.AcceptChanges();

            GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, this.dsReservas);

            this.tblDatosPasajeros = tblDatosPax;
        }
        public void UpdatePerson(int intIdPasajero, string sidGenero, string sNacionalidad, string sPasaporte, string sdtmFechaExpiracion, string sPaisResidencia, string sTipoIdentificacion, string sDocumento, string sPaxFrecuente)
        {
            DataTable tblDatosPax = dsReservas.Tables["tblPax"];

            if (tblDatosPax.Rows.Contains(intIdPasajero))
            {
                DataRow dtrPax = tblDatosPax.Rows.Find(intIdPasajero);

                if (sidGenero != null)
                {
                    dtrPax["intGenero"] = sidGenero;
                }
                if (sNacionalidad != null)
                {
                    dtrPax["strNacionalidad"] = sNacionalidad;
                }
                if (sPasaporte != null)
                {
                    dtrPax["strPasaporte"] = sPasaporte;
                }
                if (sDocumento != null)
                {
                    dtrPax["strDocumento"] = sDocumento;
                }
                if (sdtmFechaExpiracion != null)
                {
                    dtrPax["dtmFechaExpPasaporte"] = sdtmFechaExpiracion;
                }
                if (sPaisResidencia != null)
                {
                    dtrPax["strPaisResidencia"] = sPaisResidencia;
                }
                if (sTipoIdentificacion != null)
                {
                    dtrPax["intTipoDoc"] = sTipoIdentificacion;
                }
                if (sPaxFrecuente != null)
                {
                    dtrPax["strViajeroFrecuente"] = sPaxFrecuente;
                }

                //if (StrCodigo != null)
                //{
                //    dtrPax["strCodigo"] = StrCodigo;
                //}
            }

            tblDatosPax.AcceptChanges();

            GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, this.dsReservas);

            this.tblDatosPasajeros = tblDatosPax;
        }
        /// <summary>
        /// Guarda los tipos de pasajeros asociados a un plan todos los parametros son obligatorios
        /// </summary>
        /// <param name="strTipoPax">string con el  tipo de pasajero</param>
        /// <param name="intTipoPax">IdRefere del tipo pasajero</param>
        /// <param name="intValorTipoPax">Numero con el valor de ese tipo de pasajero</param>
        public void SaveTipoPax(string strTipoPax, int intTipoPax, int intValorTipoPax)
        {

            DataTable tblDatosTarifa = dsReservas.Tables["tblTarifa"];

            DataRow dtNewRow = tblDatosTarifa.NewRow();

            dtNewRow["intCodigFare"] = "0";

            dtNewRow["dblTax"] = "0";

            dtNewRow["strReserva"] = StrCodigoReserva;

            dtNewRow["StrTipoPax"] = strTipoPax;

            dtNewRow["intTipoPax"] = intTipoPax;

            dtNewRow["intMoneda"] = IntTipoMoneda;

            dtNewRow["dblValor"] = intValorTipoPax;

            dtNewRow["dblTotal"] = intValorTipoPax;//IntValorTotal;

            dtNewRow["strCodigo"] = StrCodigo;

            dtNewRow["strTipoMoneda"] = StrTipoMoneda;

            dtNewRow["intConsecRes"] = this.intConsecutivo;

            tblDatosTarifa.Rows.Add(dtNewRow);

            tblDatosTarifa.AcceptChanges();

        }
        public void SaveTipoPax(string strTipoPax, int intTipoPax, string intValorTipoPax)
        {

            DataTable tblDatosTarifa = dsReservas.Tables["tblTarifa"];
            DataRow dtNewRow = tblDatosTarifa.NewRow();
            dtNewRow["intCodigFare"] = "0";
            dtNewRow["dblTax"] = "0";
            dtNewRow["strReserva"] = StrCodigoReserva;
            dtNewRow["StrTipoPax"] = strTipoPax;
            dtNewRow["intTipoPax"] = intTipoPax;
            dtNewRow["intMoneda"] = IntTipoMoneda;
            dtNewRow["dblValor"] = intValorTipoPax.ToString();
            dtNewRow["dblTotal"] = intValorTipoPax.ToString();
            dtNewRow["strCodigo"] = StrCodigo;
            dtNewRow["strTipoMoneda"] = StrTipoMoneda;
            dtNewRow["intConsecRes"] = this.intConsecutivo;
            tblDatosTarifa.Rows.Add(dtNewRow);
            tblDatosTarifa.AcceptChanges();

        }
        public void SaveTipoPax(string strTipoPax, int intTipoPax, string dblValorConImpuestosTipoPax, string dblValorSinImpuestos, string dblTax)
        {

            DataTable tblDatosTarifa = dsReservas.Tables["tblTarifa"];
            DataRow dtNewRow = tblDatosTarifa.NewRow();
            dtNewRow["intCodigFare"] = "0";
            dtNewRow["dblTax"] = dblTax;
            dtNewRow["strReserva"] = StrCodigoReserva;
            dtNewRow["StrTipoPax"] = strTipoPax;
            dtNewRow["intTipoPax"] = intTipoPax;
            dtNewRow["intMoneda"] = IntTipoMoneda;
            dtNewRow["dblValor"] = dblValorSinImpuestos;
            dtNewRow["dblTotal"] = dblValorConImpuestosTipoPax;//IntValorTotal;
            dtNewRow["strCodigo"] = StrCodigo;
            dtNewRow["strTipoMoneda"] = StrTipoMoneda;
            dtNewRow["intConsecRes"] = this.intConsecutivo;
            tblDatosTarifa.Rows.Add(dtNewRow);
            tblDatosTarifa.AcceptChanges();

        }
        public void SaveTipoPax(string strTipoPax, int intTipoPax, string dblValorConImpuestosTipoPax, string dblValorSinImpuestos, string dblTax, string dblDescuento, string dblPenalidad)
        {
            SaveTipoPax(strTipoPax, intTipoPax, dblValorConImpuestosTipoPax, dblValorSinImpuestos, dblTax, dblDescuento, dblPenalidad, null);
        }
        public void SaveTipoPax(string strTipoPax, int intTipoPax, string dblValorConImpuestosTipoPax, string dblValorSinImpuestos, string dblTax, string dblDescuento, string dblPenalidad, int? iSegmento)
        {

            DataTable tblDatosTarifa = dsReservas.Tables["tblTarifa"];
            DataRow dtNewRow = tblDatosTarifa.NewRow();
            dtNewRow["intCodigFare"] = "0";
            dtNewRow["dblTax"] = dblTax;
            dtNewRow["strReserva"] = StrCodigoReserva;
            dtNewRow["StrTipoPax"] = strTipoPax;
            dtNewRow["intTipoPax"] = intTipoPax;
            dtNewRow["intMoneda"] = IntTipoMoneda;
            dtNewRow["dblValor"] = dblValorSinImpuestos;
            dtNewRow["dblTotal"] = dblValorConImpuestosTipoPax;
            dtNewRow["dblDescuento"] = dblDescuento;
            dtNewRow["dblPenalidad"] = dblPenalidad;
            dtNewRow["strCodigo"] = StrCodigo;
            dtNewRow["strTipoMoneda"] = StrTipoMoneda;
            dtNewRow["intConsecRes"] = this.intConsecutivo;
            if (iSegmento != null)
            {
                dtNewRow["intSegmento"] = iSegmento;
            }
            tblDatosTarifa.Rows.Add(dtNewRow);
            tblDatosTarifa.AcceptChanges();
        }
        public void SaveTipoPax(string strTipoPax, int intTipoPax, string dblValorConImpuestosTipoPax, string dblValorSinImpuestos, string dblTax, int iSegmento)
        {

            DataTable tblDatosTarifa = dsReservas.Tables["tblTarifa"];
            DataRow dtNewRow = tblDatosTarifa.NewRow();
            dtNewRow["intCodigFare"] = "0";
            dtNewRow["dblTax"] = dblTax;
            dtNewRow["strReserva"] = StrCodigoReserva;
            dtNewRow["StrTipoPax"] = strTipoPax;
            dtNewRow["intTipoPax"] = intTipoPax;
            dtNewRow["intMoneda"] = IntTipoMoneda;
            dtNewRow["dblValor"] = dblValorSinImpuestos;
            dtNewRow["dblTotal"] = dblValorConImpuestosTipoPax;//IntValorTotal;
            dtNewRow["strCodigo"] = StrCodigo;
            dtNewRow["strTipoMoneda"] = StrTipoMoneda;
            dtNewRow["intConsecRes"] = this.intConsecutivo;
            dtNewRow["intSegmento"] = iSegmento;
            tblDatosTarifa.Rows.Add(dtNewRow);
            tblDatosTarifa.AcceptChanges();

        }
        public void Saveremark(int intIdRefere, int intConsecutivoRemark, string strDescripcion, string strDetalle)
        {
            DataTable tblDatosremark = dsReservas.Tables["tblRemark"];
            DataRow drNewRow = tblDatosremark.NewRow();
            drNewRow["intConsecRes"] = this.intConsecutivo;
            drNewRow["intidrefere"] = intIdRefere;
            drNewRow["strDescripcion"] = strDescripcion;
            drNewRow["strDetalle"] = strDetalle;
            tblDatosremark.Rows.Add(drNewRow);
            tblDatosremark.AcceptChanges();
        }
        /// <summary>
        /// Guarda todos los campos  de  un plan en el carrito de compras,que fueron previamente asignados al objeto del carrito de compras
        /// </summary>
        public void AddFields()
        {
            if (IntSegmento.Equals("1"))
            {
                this.dsReservas = RecuperarDataSet(this._strIdentificadorUnico);
                SaveCarrito(dsReservas.Tables[this._strNombreCarroDeCompras]);
                SaveTblReserva(dsReservas.Tables["tblReserva"]);
                SaveTblTransac(dsReservas.Tables["tblTransac"]);
            }
            else
            {
                SaveTblTransac(dsReservas.Tables["tblTransac"]);
                ModificarTblReserva(dsReservas.Tables["tblReserva"]);
                ModificarTblCarrito(dsReservas.Tables[this._strNombreCarroDeCompras]);
            }
        }
        /// <summary>
        /// Este metodo solo se llama una vez por aplicacion y es el que asigna los campos de la tabla proyecto para el carrito de compras,
        /// este metodo solo se debe llamar cuando se hace la reserva final.
        /// </summary>        
        /// <param name="strCodigoProyecto">Es el codigo del proyecto general</param>
        /// <param name="strCodigoResponsable">Es el nombre de la persona que hace la reserva general,que debe estar ya registrado</param>
        /// <param name="strCodigoContacto">Es el codigo de la persona que hace la reserva general,que debe estar ya registrado</param>
        /// <param name="strCodigoCliente">Es el codigo de la empresa que hace la reserva general,que debe estar ya registrado</param>
        /// <param name="strCodigoEstado">Es el IdRefere del estado de la reserva que esta actualmente en solicitud</param>
        /// <param name="strCodigoFormaPago">Es el IdRefere de la forma de pago</param>
        /// <param name="strCodigoEstadoPago">Es el IdRefere del esto de pago</param>
        public void SaveDataProject(string strCodigoProyecto, string strCodigoResponsable, string strCodigoContacto, string strCodigoCliente, string strCodigoEstado, string strCodigoFormaPago, string strCodigoEstadoPago)
        {
            DataTable tblDatosReserva = dsReservas.Tables["tblReserva"];
            DataTable tblDatosTransact = dsReservas.Tables["tblTransac"];

            for (int f = 0; f < tblDatosReserva.Rows.Count; f++)
            {
                //tblDatosReserva.Rows[f]["strCodigo"] = StrCodigo;

                tblDatosReserva.Rows[f]["intProyecto"] = strCodigoProyecto;

                tblDatosReserva.Rows[f]["intResponsable"] = strCodigoResponsable;

                tblDatosReserva.Rows[f]["intContacto"] = strCodigoContacto;

                tblDatosReserva.Rows[f]["intCliente"] = strCodigoCliente;

                tblDatosReserva.Rows[f]["intEstado"] = strCodigoEstado;

                tblDatosReserva.Rows[f]["intFormaPago"] = strCodigoFormaPago;

                tblDatosReserva.Rows[f]["intEstadoPago"] = strCodigoEstadoPago;
            }
            tblDatosReserva.AcceptChanges();

            for (int c = 0; c < tblDatosTransact.Rows.Count; c++)
            {
                tblDatosTransact.Rows[c]["intEstado"] = strCodigoEstado;
                //tblDatosTransact.Rows[c]["intProveedor"] = this.IntProveedor;
            }
            tblDatosTransact.AcceptChanges();

            GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, dsReservas);

        }
        /// <summary>
        ///metodo para insertar el strCodigo y valida las reservas ya insertadas      
        /// </summary>        
        /// <param name="strCodigoProyecto">Es el codigo que determina si se inserta actualiza (1)</param>      
        public void Save_Update(string strCodigo)
        {
            DataTable tblDatosReserva = dsReservas.Tables["tblReserva"];
            DataTable tblDatosTransact = dsReservas.Tables["tblTransac"];
            DataTable tblDatosPax = dsReservas.Tables["tblPax"];
            DataTable tblDatosTarifa = dsReservas.Tables["tblTarifa"];
            DataTable tblDatosTax = dsReservas.Tables["tblTax"];

            for (int f = 0; f < tblDatosReserva.Rows.Count; f++)
            {
                tblDatosReserva.Rows[f]["strCodigo"] = strCodigo;
            }
            tblDatosReserva.AcceptChanges();

            for (int c = 0; c < tblDatosTransact.Rows.Count; c++)
            {
                tblDatosTransact.Rows[c]["strCodigo"] = strCodigo;
            }
            tblDatosTransact.AcceptChanges();

            for (int c = 0; c < tblDatosPax.Rows.Count; c++)
            {
                tblDatosPax.Rows[c]["strCodigo"] = strCodigo;
            }
            tblDatosPax.AcceptChanges();

            for (int c = 0; c < tblDatosTarifa.Rows.Count; c++)
            {
                tblDatosTarifa.Rows[c]["strCodigo"] = strCodigo;
            }
            tblDatosTarifa.AcceptChanges();

            for (int c = 0; c < tblDatosTax.Rows.Count; c++)
            {
                tblDatosTax.Rows[c]["strCodigo"] = strCodigo;
            }
            tblDatosTax.AcceptChanges();

            GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, dsReservas);

        }
        /// <summary>
        /// Obtiene todos los pasajeros asociados a un plan del carrito de compras 
        /// </summary>
        /// <param name="intConsecutivo">Numero consecutivo que esta un campo del carrito de compras</param>
        /// <returns></returns>
        public DataTable GetTablaXTipoPlan(string StrIdentificadorDelPlan)
        {
            DataTable tblDatosCarrito = RecuperarTabla(this._strIdentificadorUnico, this._strNombreCarroDeCompras);

            DataTable tblDatosCarritoDevuel = tblDatosCarrito.Clone();

            DataRow[] FilasDevueltas;

            FilasDevueltas = tblDatosCarrito.Select("StrIdentificadorDelPlan like '" + StrIdentificadorDelPlan + "'");

            if (FilasDevueltas != null)
            {
                for (int f = 0; f < FilasDevueltas.Length; f++)
                {
                    tblDatosCarritoDevuel.Rows.Add(FilasDevueltas[f].ItemArray);
                }
            }

            return tblDatosCarritoDevuel;
        }
        /// <summary>
        /// Obtiene todos los pasajeros asociados a un plan del carrito de compras 
        /// </summary>
        /// <param name="intConsecutivo">Numero consecutivo que esta un campo del carrito de compras</param>
        /// <returns></returns>
        public DataTable GetTablaPasajeros(int intConsecutivo)
        {
            DataTable tblDatosPax = RecuperarTabla(this._strIdentificadorUnico, "tblPax");
            DataTable tblDatosPaxDevuel = tblDatosPax.Clone();
            DataRow[] FilasDevueltas;
            FilasDevueltas = tblDatosPax.Select("intConsecRes = " + intConsecutivo.ToString());
            if (FilasDevueltas != null)
            {
                for (int f = 0; f < FilasDevueltas.Length; f++)
                {
                    tblDatosPaxDevuel.Rows.Add(FilasDevueltas[f].ItemArray);
                }
            }
            this.tblDatosPasajeros = tblDatosPaxDevuel;
            return tblDatosPaxDevuel;
        }
        /// <summary>
        /// Obtiene una tabla con los tipos de pasajeros asiciados a un plan
        /// </summary>
        /// <param name="intConsecutivo">Numero consecutivo del carrito de compras</param>
        /// <returns>Tabla con todos los tipos de pasajeros de un plan</returns>
        public DataTable GetTablaTipoPax(int intConsecutivo)
        {
            DataTable tblDatosTipoPax = RecuperarTabla(this._strIdentificadorUnico, "tblTarifa");
            DataTable tblDatosPaxDevuel = tblDatosTipoPax.Clone();
            DataRow[] FilasDevueltas;

            FilasDevueltas = tblDatosTipoPax.Select("intConsecRes=" + intConsecutivo.ToString());
            if (FilasDevueltas != null)
            {
                for (int f = 0; f < FilasDevueltas.Length; f++)
                {
                    tblDatosPaxDevuel.Rows.Add(FilasDevueltas[f].ItemArray);
                }
            }
            this.tblDatosTipoPasajero = tblDatosPaxDevuel;
            return tblDatosPaxDevuel;
        }
        /// <summary>
        /// Guarda las Habitaciones en una tabla con sus respetivas personas
        /// </summary>
        /// <param name="intCantidadPersonas"></param>
        public void SaveHabitacionesHotel(int intCantidadPersonas)
        {
            this.tblDatosHabitaciones = dsReservas.Tables["tblHabitaciones"];

            DataRow dtNewRow = tblDatosHabitaciones.NewRow();

            dtNewRow["intConsecRes"] = this.intConsecutivo;

            dtNewRow["intNumeroHabitacion"] = DBNull.Value;

            dtNewRow["intCantPersonas"] = intCantidadPersonas;

            dtNewRow["intAcomodacion"] = IntAcomodacion;

            dtNewRow["intTipoHabitacion"] = IntTipoHabitacion;

            tblDatosHabitaciones.Rows.Add(dtNewRow);

            tblDatosHabitaciones.AcceptChanges();
        }
        /// <summary>
        /// Remueve todos los planes del carrito de compras
        /// </summary>
        public void LimpiarCarrito()
        {
            try
            {
                this.dsReservas = RecuperarDataSet(this._strIdentificadorUnico);
                dsReservas.Clear();
                dsReservas.AcceptChanges();
                GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, dsReservas);
                clsSesiones.CLEAR_SESSION_PROYECTO();

            }
            catch { }
            csCache.IniciaProyecto();
        }
        /// <summary>
        /// Recupera la tabla de habitaciones de un hotel de disney
        /// </summary>
        /// <param name="intConsecutivo"></param>
        /// <returns></returns>
        public DataTable GetHabitacionesHotel(int intConsecutivo)
        {
            this.tblDatosHabitaciones = RecuperarTabla(this._strIdentificadorUnico, "tblHabitaciones");
            DataTable tblDatosPaxDevuel = tblDatosHabitaciones.Clone();
            DataRow[] FilasDevueltas;
            FilasDevueltas = tblDatosHabitaciones.Select("intConsecRes = " + intConsecutivo);
            if (FilasDevueltas != null)
            {
                for (int f = 0; f < FilasDevueltas.Length; f++)
                {
                    tblDatosPaxDevuel.Rows.Add(FilasDevueltas[f]);
                }
            }
            this.tblDatosHabitaciones = tblDatosPaxDevuel;
            return tblDatosPaxDevuel;
        }
        /// <summary>
        ///Asigna los campos que se ven en el carrito de compras
        /// </summary>
        /// <param name="strDatosVisibles"></param>
        public void AddDatosVisibles(params string[] strDatosVisibles)
        {

            DataTable tblDatosVisibles = dsReservas.Tables["TablaDatosVisibles"];

            DataRow dtNewRow = tblDatosVisibles.NewRow();

            dtNewRow["intConsecRes"] = this.intConsecutivo;

            dtNewRow["NombrePlan"] = this.StrNombrePlan;

            dtNewRow["ValorTotalPlan"] = this.IntValorTotal;

            dtNewRow["TipoMoneda"] = this.StrTipoMoneda;

            for (int c = 0; c < strDatosVisibles.Length; c++)
            {
                if (strDatosVisibles[c] != null)
                {
                    dtNewRow[(c + 4)] = strDatosVisibles[c];
                }
            }

            tblDatosVisibles.Rows.Add(dtNewRow);

            tblDatosVisibles.AcceptChanges();
        }
        /// <summary>
        /// Guarda los datos en el archivo xml
        /// </summary>
        public void Save()
        {
            this.dsReservas.AcceptChanges();

            GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, this.dsReservas);
        }
        /// <summary>
        /// Devuelve el valor con los valores totales y sus tipos de moneda
        /// </summary>
        /// <returns></returns>
        public string Total()
        {
            DataTable tblDatosCarrito = RecuperarTabla(this._strIdentificadorUnico, this._strNombreCarroDeCompras);

            DataTable tblTiposMoneda = this.Distinto(tblDatosCarrito, "strTipoMoneda");

            tblTiposMoneda.Columns.Add("IntValorTotal", typeof(int));

            for (int c = 0; c < tblTiposMoneda.Rows.Count; c++)
            {
                int intValorTipo = 0;

                int intValorTipoTemp = 0;

                for (int k = 0; k < tblDatosCarrito.Rows.Count; k++)
                {
                    if (tblTiposMoneda.Rows[c]["strTipoMoneda"].ToString().Equals(tblDatosCarrito.Rows[k]["strTipoMoneda"].ToString()))
                    {
                        try
                        {
                            intValorTipoTemp = Convert.ToInt32(tblDatosCarrito.Rows[k]["IntValorTotal"]);
                        }
                        catch (Exception)
                        {
                            intValorTipoTemp = 0;
                        }
                        intValorTipo = intValorTipo + intValorTipoTemp;

                        tblTiposMoneda.Rows[c]["IntValorTotal"] = intValorTipo;
                    }
                }
            }

            StringBuilder strValoresAndTipos = new StringBuilder();

            for (int f = 0; f < tblTiposMoneda.Rows.Count; f++)
            {
                strValoresAndTipos.AppendLine(tblTiposMoneda.Rows[f]["IntValorTotal"].ToString() + ":" + tblTiposMoneda.Rows[f]["strTipoMoneda"].ToString() + "</br>");
            }

            return strValoresAndTipos.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intConsecutivo"></param>
        /// <returns></returns>
        public DataSet GetDsReservas()
        {
            return RecuperarDataSet(this._strIdentificadorUnico);
        }
        /// <summary>
        /// Guarda las observaciones del plan
        /// </summary>
        /// <param name="intConsecutivoReserva"></param>
        /// <param name="strObservaciones"></param>
        public void GuardarObervaciones(int intConsecutivoReserva, string strObservaciones)
        {
            DataTable tblCarritoCompras = this.dsReservas.Tables[this._strNombreCarroDeCompras];

            if (tblCarritoCompras.Rows.Contains(intConsecutivoReserva))
            {
                DataRow dtrEncontrada = tblCarritoCompras.Rows.Find(intConsecutivoReserva);

                DataRow[] dtrFilasRelacionadas = dtrEncontrada.GetChildRows(this._strNombreCarroDeCompras + "_tblReserva");

                for (int k = 0; k < tblCarritoCompras.Rows.Count; k++)
                {
                    tblCarritoCompras.Rows[k]["StrObservacion"] = strObservaciones;
                }

                for (int c = 0; c < dtrFilasRelacionadas.Length; c++)
                {
                    dtrFilasRelacionadas[c]["strObservacion"] = strObservaciones;

                    dtrFilasRelacionadas[c].Table.AcceptChanges();
                }

            }
            GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, this.dsReservas);
        }
        public void ActualizarRecordReserva(String strRecordReserva, string strCodigoConsecutivo)
        {
            DataTable tblDatos = dsReservas.Tables[this._strNombreCarroDeCompras];
            DataRelationCollection dtRelacionColeccion = dsReservas.Relations;
            List<string> strListaRelaciones = new List<string>();
            strListaRelaciones.Add("CarritoCompras_tblReserva");
            strListaRelaciones.Add("CarritoCompras_tblTransac");
            strListaRelaciones.Add("CarritoCompras_tblPax");
            strListaRelaciones.Add("CarritoCompras_tblTarifa");

            if (tblDatos.Rows.Contains(strCodigoConsecutivo))
            {
                DataRow drFilaEncontrada = tblDatos.Rows.Find(strCodigoConsecutivo);
                drFilaEncontrada["strReserva"] = strRecordReserva;

                foreach (string strRelacion in strListaRelaciones)
                {
                    DataRow[] drFilasRelacionadas = drFilaEncontrada.GetChildRows(strRelacion);

                    foreach (DataRow dtFilaRelacionada in drFilasRelacionadas)
                    {
                        dtFilaRelacionada["strReserva"] = strRecordReserva;
                    }
                }
            }
            dsReservas.AcceptChanges();
            GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, dsReservas);

        }
        public void AddTasa(String intCodigoTax, String dblPorcent, String dblValorTax, String intTipoMoneda, String intTipoPax)
        {
            DataTable DtResFareTax = dsReservas.Tables["tblTax"];
            DataRow drNewFareTax = DtResFareTax.NewRow();
            drNewFareTax["intCodigoTax"] = intCodigoTax;
            drNewFareTax["dblPorcent"] = dblPorcent;
            drNewFareTax["intMoneda"] = intTipoMoneda;
            drNewFareTax["dblValorTax"] = dblValorTax;
            drNewFareTax["intTipoPax"] = intTipoPax;
            drNewFareTax["strCodigo"] = "0";
            drNewFareTax["intCodigFare"] = "0";
            drNewFareTax["intConsecRes"] = this.intConsecutivo.ToString();
            DtResFareTax.Rows.Add(drNewFareTax);
            DtResFareTax.AcceptChanges();
        }

        public void AddValueBono(decimal dValorBono, decimal dValorCalculoBono)
        {           
                this.dsReservas = RecuperarDataSet(this._strIdentificadorUnico);
                DataTable tCarrito = dsReservas.Tables[this._strNombreCarroDeCompras];
                tCarrito.Rows[tCarrito.Rows.Count - 1]["DblValorBono"] = dValorBono;
                tCarrito.Rows[tCarrito.Rows.Count - 1]["DblValorCalculoBono"] = dValorCalculoBono;
                GuardarDataSetOActualizarDataSet(this._strIdentificadorUnico, dsReservas);
        }
        #endregion
    }
}
