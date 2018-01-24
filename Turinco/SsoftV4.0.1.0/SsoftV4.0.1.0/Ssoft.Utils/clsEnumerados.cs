namespace Ssoft.Utils
{
    public enum Enum_Tipo_Seccion_Publicacion
    {
        SP_NUESTROS_ALIADOS,
        SP_DOCUMENTACION,
        SP_RECOMENDACIONES,
        SP_AEROLINEAS,
        SP_BOLETIN,
        SP_HOTELES,
        SP_GRUPOS,
        SP_MEDIOS_PAGO,
        SP_QUIENES_SOMOS,
        SP_INF_CARROCOMPRAS,
        SP_CONDICIONES,
        SP_CONTACTENOS,
        SP_DESTINOS,
        SP_CONDICIONES_REGISTRO,
        SP_ENCABEZADO,
        SP_TESTIMONIOS,
        SP_CONGRESOS,
        NINGUNA,
        SP_SALIDAS,
        SP_CRUCEROS,
        SP_TURISMO_ESP,
        SP_NOTICIAS,
        SP_TEXTOS_LEGALES,
        SP_CLAUSULA_RESPONSABILIDAD,
        SP_PREGUNTASFRECUENTES,
        SP_BIENVENIDA,
        SP_IMAGENES,
        SP_ACERCA_BOG,
        SP_MARCABLANCA,
        SP_ACTIVIDADES,
        SP_COMO_COMPRAR,
        SP_PREGUNTAS_FRECUENTES,
        SP_SERVICIOS_PARA_AGENCIAS,
        SP_SERVICIOS_PARA_PROVEEDORES,
        SP_SERVICIOS_CORPORATIVOS,
        SP_FORMAS_DE_PAGO,
        SP_EXPERIENCIAS,
        SP_OLAS,
        SP_PROVEEDORES,
        SP_AYUDA,
        SP_ASESOR,
        SP_CERTIFICACIONES,
        SP_CHAT,
        SP_CONTRA,
        SP_FACEBOOK,
        SP_TWITTER,
        SP_MAPA,
        SP_COMENTARIOS,
        SP_COMPRAR_EMPRESA,
        SP_MENU,
        SP_VIAJAR_SEGURO,
        SP_PLANESDEAHORRO,
        SP_REVISTA_VAMOS,
        SP_HOTEL,
        SP_AUTOS,
        SP_PLANTILLA_1,
        SP_PLANTILLA_2,
        SP_PLANTILLA_3,
        SP_PLANTILLA_4,
        SP_LINKS,
        SP_OFERTAS_DYSNEY,
        SP_CONTACTO_SERVICIO,
        SP_SALA_PRENSA,
        SP_BENEFICIOS_CORPORATIVOS,
        SP_TEXTO_LOGIN,
        SP_DESTINOS_DESTACADOS
    }

    public enum Enum_Seccion_Informativa
    {
        DOCUMENTACION,
        RECOMENDACIONES,
        AEROLINEAS,
        BOLETIN,
        HOTELES,
        GRUPOS,
        MEDIOS_PAGO,
        QUIENES_SOMOS,
        INF_CARROCOMPRAS,
        CONDICIONES,
        CONTACTENOS,
        DESTINOS,
        CONDICIONES_REGISTRO,
        LOGIN,
        TESTIMONIOS,
        PAUTE,
        NINGUNA,
        NOTICIAS,
        MARCABLANCA,
        DEMOS_ALIACORP,
        SERVICIOS_CORPORATIVOS,
        PLANES_DE_AHORRO,
        PLANTILLA_1,
        PLANTILLA_2,
        PLANTILLA_3,
        PLANTILLA_4,
        REVISTA_VAMOS,
        SP_NUESTROS_ALIADOS,
        Destinos_Recomendados
    }

    public enum Enum_Tipo_Plantilla_Seccion
    {
        PlantillaUno,
        PlantillaRegistro,
        PlantillaContacto,
        PlantillaDestino,
        PlantillaDestinoDinamico,
        PlantillaFechasCategoria,
        PlantillaCarruselHome,
        PlantillaContactoServicio
    }

    public enum Enum_TipoPassenger
    {
        Adulto = 1,
        Ninio = 2,
        Infante = 3,
        Junior = 4
    }
    public enum Enum_TipoEdad
    {
        Meses = 1,
        Anios = 2
    }
    public enum Enum_Mapa
    {
        Ubicacion = 1,
        Posicion = 2,
        Url = 3
    }
    public enum Enum_Priority
    {
        Price = 1,
        Vendor = 2,
        Time = 3,
        Direct = 4
    }

    public enum Enum_Idioma
    {
        Espanol = 1,
        Ingles = 2
    }

    /*ENUMERACIONES CONTROL*/

    public enum Enum_ControlResultados
    {
        Error = 1,
        SinDatos = 2,
        Datos = 3
    }
    public enum Enum_ControlSolicitud
    {
        Error = 1,
        SinDatos = 2,
        Datos = 3
    }
    public enum Enum_ControlReserva
    {
        Error = 1,
        SinCerrar = 2,
        Cerrada = 3
    }
    public enum Enum_ListBagainFinder
    {
        Actual = 1,
        Anterior = 2,
        Siguiente = 3
    }
    public enum Enum_HotelControlResultados
    {
        Error = 1,
        SinDatos = 2,
        Datos = 3
    }
    public enum Enum_HotelControlSolicitud
    {
        Error = 1,
        SinDatos = 2,
        Datos = 3
    }
    public enum Enum_HotelControlReserva
    {
        Error = 1,
        SinCerrar = 2,
        Cerrada = 3
    }
    public enum Enum_CarControlResultados
    {
        Error = 1,
        SinDatos = 2,
        Datos = 3
    }
    public enum Enum_CarControlSolicitud
    {
        Error = 1,
        SinDatos = 2,
        Datos = 3
    }
    public enum Enum_CarControlReserva
    {
        Error = 1,
        SinCerrar = 2,
        Cerrada = 3
    }
    /*ENUMERACIONES DIAS*/

    public enum Enum_Dias
    {
        Lunes = 1,
        Martes = 2,
        Miercoles = 3,
        Jueves = 4,
        Viernes = 5,
        Sabado = 6,
        Domingo = 7,
        NoEspecificado = 0
    }
    /*ENUMERACIONES GLOBAL*/

    public enum Enum_TipoFotoItinerario
    {
        FotoGaleria = 1,
        FotoGrande = 2,
        FotoPequena = 3
    }
    public enum Enum_Global
    {
        Air = 1,
        Hotel = 2,
        Car = 3,
        Other = 4,
        Nothing = 5,
        Plan = 6,
        AsistCard = 7,
        AirNoGds = 8,
        Traslados = 9
    }
    public enum Enum_Proceso
    {
        Ninguno = 0,
        Resultados = 1,
        Solicitud = 2,
        Reserva = 3
    }
    public enum Enum_ResultadoBusqueda
    {
        Primero = 1,
        Ultimo = 2
    }
    public enum Enum_Ordenar
    {
        Hora = 1,
        Precio = 2,
        Aerolinea = 3
    }
    public enum Enum_Wizard
    {
        Inicio = 1,
        Proyecto = 2,
        Documentacion = 3,
        SolicitudAnticipo = 4,
        InformacionPasajeros = 5,
        ProyectoExistente = 6,
        Finalizar = 7
    }
    public enum Enum_EstadosProyecto
    {
        Abierta = 1,
        Cancelado = 2,
        Autorizado = 3,
        Solicitado = 4,
        Cerrado = 5,
        Todos = 6,
        Rechazado = 7,
        Legalizado = 8,
        PteLegalizar = 9,
        Reportadas = 10,
        Giradas = 11,
        AutorizadoAnt = 12,
        SolicitudCancel = 13,
        SolicitudCambio = 14,
        Devuelto = 15,
        GiradoDevuelto = 16,
        LegalizadoSolo = 17,
        Contabilizado = 18
    }
    /*ENUMERACION ORDEN RESULTADOS*/
    public enum Enum_OrdenXHora
    {
        Aerolinea = 1,
        HoraSalida = 2,
        Conexiones = 3,
    }
    /*ENUMERACION RESERVAS*/
    public enum Enum_ResutaldosProceso
    {
        Busqueda = 1,
        Detalles = 2,
        login = 3,
        Pasajeros = 4,
        Reservar = 5,
        Imprimir = 6
    }
    /*ENUMERACION FORMATOS XML*/
    public enum Enum_Encoding
    {
        Unicode = 1,
        UTF7 = 2,
        UTF8 = 3,
        UTF32 = 4,
        ISO88591 = 5
    }
    // FORMATOS DEL SERVIDOR
    public enum Enum_FormatoFecha
    {
        DMY = 1,
        MDY = 2,
        YMD = 3,
        YDM = 4,
        DYM = 5,
        MYD = 6,
        YMD_ = 7,
        DMYH = 8,
        MDYH = 9,
        YMDH = 10,
        YDMH = 11,
        DYMH = 12,
        MYDH = 13,
        YMDH_ = 14
    }
    public enum Enum_FormatoDecimal
    {
        Punto = 1,
        Coma = 2
    }
    public enum Enum_WebServices
    {
        SabreAir = 1,
        SabreHotel = 2,
        SabreCar = 3,
        HotelBedsHotel = 4,
        CotelcoHotel = 5,
        ZeusHotel = 6,
        AteneaPolizas = 7,
        HotelInterNal = 8,
        TouricoHotel = 9
    }
    public enum Enum_ProveedorWebServices
    {
        Sabre = 1,
        Amadeus = 2,
        HotelBeds = 3,
        Cotelco = 4,
        Zeus = 5,
        Planes = 6,
        Atenea = 7,
        Maf = 8,
        RedeBan = 9,
        TotalTrip = 10,
        Tame = 11,
        POL = 12,
        Trenes = 13,
        Tourico = 14,
        POLBG = 15,
        POLAVIATUR = 16
    }
    public enum Enum_TipoPlan
    {
        Alojamiento = 1,
        AlojamientoyTiquete = 2,
        Tiquete = 3,
        Excurcion = 4,
        Crucero = 5,
        Circuitos = 6,
        TodosLosTipos = 7

    }

    public enum Enum_TipoDestino
    {
	    Vacio = 0,
        Nacional = 1,
        Internacional = 2,
        Todos = 3
    }

    public enum Enum_TipoClasificacion
    {
        Plan = 1,
        Oferta = 2,
        Todos = 3
    }

    public enum Enum_MiembroSemper
    {
        si = 1,
        no = 2
    }
   
    /*ENUMERACION WSVUELOS*/
    public enum Enum_TipoRemark
    {
        Compuesto = 1,
        Direccion = 2,
        DireccionCliente = 3,
        Grupo = 4,
        Historico = 5,
        Impresion = 6,
        Libre = 7,
        Oculto = 8,
        Simple = 9
    }
    public enum Enum_TipoVuelo
    {
        Nacional = 1,
        Internacional = 2
    }
    public enum Enum_TipoTrayecto
    {
        Ida = 1,
        IdaRegreso = 2,
        Multidestino = 3
    }
    public enum Enum_TipoHotel
    {
        Nacional = 1,
        Internacional = 2
    }
    public enum Enum_TipoAuto
    {
        Nacional = 1,
        Internacional = 2
    }
    public enum Enum_TipoBusqueda
    {
        Bargain = 1,
        Hora = 2
    }
    public enum Enum_SabreCommandLLSRQRequestOutput
    {

        /// <comentarios/>
        SCREEN,

        /// <comentarios/>
        SDS,

        /// <comentarios/>
        SDSXML,

        /// <comentarios/>
        DATABAHN,
    }
    public enum Enum_RatePlanCandidatesTypeTPA_ExtensionsPromotionalSpotCodeType
    {

        /// <comentarios/>
        L,

        /// <comentarios/>
        C,
    }
    public enum Enum_OTA_VehLocDetailRQTPA_ExtensionsQueryType
    {

        /// <comentarios/>
        Policy,

        /// <comentarios/>
        Shop,

        /// <comentarios/>
        Quote,
    }
    /* Enumerados de corporativo */
    public enum Enum_ReferenciasCorp
    {
        Motivos_Viaje = 1,
        Negacion_Viaje = 2,
        Centro_Costos = 3,
        Gastos_Viaje = 4,
        Areas = 5,
        Politicas = 6,
        Actividad_Empresarial = 7,
        Viaticos = 8,
        Nivel_Viaticos = 9,
        Tarifa_Mayor = 10,
        Motivo_TM = 11,
        Horarios = 12,
        Proveedores = 13,
        Motivo_Cancel = 14
    }
    public enum Enum_Login
    {
        LoginGen = 1,
        LoginCorp = 2,
        LoginDefault = 3,
        LoginConcurso = 4,
        LoginAtenea = 5,
        LoginAdmin = 6,
        LoginCarro = 7,
        LoginMayorista = 8
    }
    public enum Enum_TipoContacto
    {
        Contacto = 1,
        Agencia = 2,
        Empresa = 3,
        Propietario = 4,
        Coordinador = 5,
        Comunidad = 6
    }
    public enum Enum_TipoArchivo
    {
        Excel = 1,
        Word = 2,
        HTML = 3,
        Plano = 4,
        PDF = 5,
        XML = 6,
        CSV = 7
    }
    public enum Enum_ArchivoEncabezado
    {
        Si = 1,
        No = 2
    }
    public enum Enum_TipoArchivoPlano
    {
        Columna = 1,
        Fila = 2
    }
    public enum Enum_TipoSalida
    {
        Page = 1,
        NewPage = 2,
        Export = 3,
        None = 4
    }
    public enum Enum_PoliticasCorp
    {
        TiempoMinimoSolicitudViajes = 1,
        TiempoMinimoPermitirReserva = 2
    }
    // Enumerados de Sql
    public enum TipoComando
    {
        Select,
        Update,
        Insert,
        Delete
    }
    public enum TipoCampo
    {
        String = 1,
        Numeric = 2,
        DateTime = 3,
        Boolean = 4,
        Text = 5,
        Bit = 6,
        Double = 7,
        Decimal = 8,
        Integer = 9
    }
    public enum Format
    {
        Money = 1,
        Decimal = 2,
        DateTime = 3,
        Numeric = 4
    }
    public enum Operador
    {
        Like,
        Mayor,
        Menor,
        Diferente
    }
    public enum OperacionEmail
    {
        Email,
        InsertarBD,
        Ambos
    }
    //public enum TipoControl
    //{
    //    DropDownList = 1,
    //    GridView = 2,
    //    Repeater = 3,
    //    BulletedList = 4,
    //    CheckBoxList = 5,
    //    DataList = 6
    //}
    public enum FormatMail
    {
        HTML,
        Text,
        PlantillaHTML
    }
    public enum PosicionText
    {
        Inicio = 1,
        Final = 2,
        Total = 3,
        Formato = 4
    }
    public enum Enum_Controls
    {
        Nulo = 0,
        Label = 1,
        Button = 2,
        LinkButton = 3,
        HiperLink = 4,
        RadioButton = 5,
        ImageButton = 6,
        TextBox = 7,
        CheckBox = 8,
        DataList = 9,
        Repeater = 10,
        DropDownList = 11,
        GridView = 12,
        BulletedList = 13,
        CheckBoxList = 14,
        ListBox = 15,
        RadioButtonList = 16,
        WebDataGrid = 17,
        WebHierarchicalDataGrid = 18,
        Literal = 19
    }
    public enum Enum_Horarios
    {
        Lunes_Viernes = 1,
        Sábado = 2,
        Domingo = 3,
        Festivo = 4
    }
    public enum Enum_UsuarioMaf
    {
        Vendedor = 1,
        Comprador = 2
    }
    public enum Enum_Alinear
    {
        Izquierda = 1,
        Derecha = 2,
        Centro = 3,
        Justificar = 4
    }
    public enum Enum_AccionReserva
    {
        Cancelar = 21,
        Confirma = 29
    }
    public enum Enum_Error
    {
        Log = 1,
        Transac = 2
    }
    public enum Enum_OrigenBusqueda
    {
        Normal = 1,
        Planes = 2,
        Ofertas = 3,
        OfertasFijas = 4
    }
    public enum Enum_TypeZone
    {
        SIMPLE = 1,
        GROUP = 2
    }
    public enum Enum_ProductoVasa
    {
        Infinite,
        Platinum,
        Signature
    }
    public enum Enum_UsuarioVasa
    {
        Banco = 1,
        Final = 2
    }
}