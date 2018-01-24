function SetIdHotelNal(IdHotel) {
    $('#iHotel').attr('src', 'HotelNal.aspx?ID=' + IdHotel);
    $find('MPEEHotel').show();
    //$object("MPEEHotel").show();
}
//Definimos las constantes
var DATE_DELIMITER = '/';

//Fecha Corta y fecha larga
var DATE_SHORT_FORMAT = 1;
var DATE_LONG_FORMAT = 2;

//Formato de la fecha
//YMD : Año/Mes/Dia
//DMY : Dia/Mes/Año
//MDY : Mes/Dia/Año

var DATE_FORMAT_DMY = 2;
var DATE_FORMAT_YMD = 1;
var DATE_FORMAT_MDY = 3;
var DATE_FORMAT_DEFAULT = DATE_FORMAT_MDY;
var DATE_FORMAT_STRING = "MM/dd/yyyy";

/*CLASE BUSCADOR*/

var micierre = false;
function ConfirmarCierre() {
    if (event.clientY < 0) {
        //        event.returnValue = "";
        //        setTimeout('micierre = false', 100);
        micierre = true;
    }
}



function pageLoad(sender, args) {

    //$.datepicker.setDefaults($.datepicker.regional['es']);
    FormatoFecha = 'mm/dd/yy';

    // var TexBox_Calendarios_Tarifas = $("#UcDetalleTourRela1_UcTarifasTouresRela1_txtFecha");
    //     TexBox_Calendarios_Tarifas.datepicker({
    //        changeMonth: true,
    //        changeYear: true,
    //        minDate: +0, 
    //        maxDate: '+1Y',
    //        dateFormat: FormatoFecha
    //    }); 
    /*CALENDARIOS TARIFAS ROTATIVOS*/
    //    var TexBox_Calendarios_Tarifas = $("#ucDetallePlan_UcTarifasRotativos1_txtFecha");
    //     TexBox_Calendarios_Tarifas.datepicker({
    //        changeMonth: true,
    //        changeYear: true,
    //        minDate: +0, 
    //        maxDate: '+1Y',
    //        dateFormat: FormatoFecha
    //    }); 
    var TexBox_Calendarios_Comunes = $("input[id*='txtFecha'],input[id*='txtCarFecha']");
    TexBox_Calendarios_Comunes.datepicker({
        changeMonth: true,
        changeYear: true,
        minDate: -0,
        maxDate: '+10Y',
        dateFormat: FormatoFecha
    });

    //	Tabs_Destinos();
    //	Imagenes_Cambio();	

    //	//Hide (Collapse) the toggle containers on load
    $(".toggle_container_Preguntas").hide();

    //Switch the "Open" and "Close" state per click then slide up/down (depending on open/close state)
    $("h4.trigger").click(function () {
        $(this).toggleClass("active").next().slideToggle("slow");
    });

    try {
        es_carga_parcial = args.get_isPartialLoad();

    } catch (err) {

    }
//    if (args.get_isPartialLoad()) {

//        ObjBuscador = new clsJBuscador();
//        ObjBuscador.InicialzarJBuscador_PreInit();
//    }
    RecuperaSession();
    //CargarPagina(sender,args)
}

function ManejadorCierre() {
    if (micierre == true) {
        var url = 'CerrarSesion.aspx';
        window.open(url, '', 'width=0,height=0,scrollbars=NO');
    }
}

var clsJBuscador = function () {
    $.datepicker.setDefaults($.datepicker.regional['es']);
    /*METODOS PUBLICOS*/
    this.InicialzarJBuscador_PreInit = InicialzarJBuscador_PreInit;
    /*EVENTOS*/
    function InicialzarJBuscador_PreInit() {
        try {
            /*INICIALIZA BUSCADOR*/
            $(document).ready(Window_Load);
        } catch (Ex) {
            window.alert("No se encuentra el archivo [jquery-1.3.2.js]");
        }
        try {
            /*OBTENEMOS EL TAB INDEX*/
            intTabIndex = GetTabIndex();
        } catch (Ex) {
            window.alert("No se encuentra el archivo [jquery.cookies.2.1.0.js]");
        }

        function Window_Load() {
            $("#Ventana").hide();
            $("#Ventana2").hide();
            /*CUANDO DE CLICK SOBRE EL BOTON CON LA CLASE*/
            $('#btnEnviarAmigo').click(function () {
                /*INDICAMOS CUAL VA SER EL DIV QUE SE ABRA*/
                $("#Ventana").dialog('open');
            });

            $('#ucResultadoPlanes_UcDetallePlan1_btnItinerario').click(function () {
                $('#Ventana').dialog('open');
            });

            $('#ucResultadoPlanes_UcDetallePlan1_btnEnviar').click(function () {
                $('#Ventana2').dialog('open');
            });

            $('.ClaseItinerario').click(function () {

                $('#Ventana').dialog('open');

            });

            $('#ucResultadoPlanes_ctl00_btnItinerario').click(function () {

                $('#Ventana').dialog('open');

            });


            FormatoFecha = 'mm/dd/yy';
            DATE_DELIMITERLOCAL = "/";

            /*__________________________CALENDARIOS DE JQUERY_____________________________*/
            var TexBox_Calendarios_Comunes = $("input[id*='txtFecha'],input[id*='txtCarFecha'],input[id*='txtDesde'],input[id*='txtHasta']");
            var TexBox_Calendarios_Vuelos = $("input[id*='txt2VFecha'],input[id*='txtHasta']");
            //var TexBox_Calendarios_Hoteles = $("input[id*='txt2HFecha']");
            var TexBox_Calendarios_Carros = $("input[id*='txt2CFecha']");
            var TexBox_Calendarios_Tarjetas = $("input[id*='txt2TFecha']");
            var TexBOx_Calendarios_Combo = $("#TablaConTextBox input, input[name*='txtEdad1'], input[name*='txtNacimiento'], input[name*='txtPasaporteFecha'], #ucMiCuenta_TabContainer1_TabPanel3_txtNac, input[name*='txtFecNac'],  input[name*='txtNac']");
            var TexBOx_Calendarios_Diferencia = $("input[name*='txtPasaporteFecha']");

            var TexBox_Calendario_Buscador_Hotel = $("input[id*='txtFechaIngreso']");
            TexBox_Calendario_Buscador_Hotel.datepicker({ changeMonth: true, changeYear: true, minDate: +2, maxDate: '+1Y', numberOfMonths: 2, dateFormat: FormatoFecha,
                onSelect: function (dateText, inst) {
                    /*llamamos el metodo para actualizar la salida*/
                    SetValidarNochesHoteles();
                }
            });

            /*::::::VALIDACIONES FECHA HOTELES ACTUALIZACION FECHA SALIDA::::::*/
            /*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
            /*NOCHES*/
            var dropdownlistNoches_Hoteles = $("select[id*='cmbNoches']");
            dropdownlistNoches_Hoteles.change(SetValidarNochesHoteles);
            /*FECHA INGRESO*/
            var txtFechaSalida_Hoteles = $("input[id*='txtFechaIngreso']");
            /*FECHA SALIDA*/
            var txtFechaRegreso_hoteles = $("input[id*='txt2HFechaSalida']");
            txtFechaRegreso_hoteles.focus(function () { this.blur(); });
            /*FUNCION QUE ACTUALIZA*/
            function SetValidarNochesHoteles() {
                var FechaSalida = txtFechaSalida_Hoteles.val();
                var Dias = dropdownlistNoches_Hoteles.val();
                FechaSalida = FechaSalida.split('/');
                FechaSalida = FechaSalida[0] + '/' + FechaSalida[1] + '/' + FechaSalida[2];
                var miFechaSal = new Date(FechaSalida);
                miFechaSal.setTime(miFechaSal.getTime() + Dias * 24 * 60 * 60 * 1000);
                var mes = miFechaSal.getMonth() + 1;
                if (mes <= 9) {
                    mes = "0" + mes;
                }
                var dia = miFechaSal.getDate();
                if (dia <= 9) {
                    dia = "0" + dia
                }
                var FechaTotal = mes + "/" + dia + "/" + miFechaSal.getFullYear();
                txtFechaRegreso_hoteles.val(FechaTotal);
            }
            /*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
            /*::::::FIN VALIDACIONES FECHA HOTELES::::::*/


            /*EVENTOS DE LOS TEXBOX*/
            TexBox_Calendarios_Comunes.datepicker({
                changeMonth: true,
                changeYear: true,
                minDate: -0,
                maxDate: '+1Y',
                numberOfMonths: 2,
                dateFormat: FormatoFecha
            });

            TexBOx_Calendarios_Diferencia.datepicker({
                changeMonth: true,
                changeYear: true,
                minDate: -0,
                maxDate: '+10Y',
                numberOfMonths: 2,
                dateFormat: FormatoFecha
            });
            //              
            TexBOx_Calendarios_Combo.datepicker({
                changeMonth: true,
                changeYear: true,
                maxDate: +0,
                dateFormat: FormatoFecha,
                numberOfMonths: 1,
                yearRange: 'c-100:c+200'
            });



            TexBox_Calendarios_Vuelos.focus(Txt_OnFocus_Vuelos);
            //TexBox_Calendarios_Hoteles.focus(Txt_OnFocus_Hoteles);   
            TexBox_Calendarios_Carros.focus(Txt_OnFocus_Carros);
            TexBox_Calendarios_Tarjetas.focus(Txt_OnFocus_Tarjetas);
            //TexBOx_Calendarios_Combo.focus(Txt_OnFocus_Combo);                                   
            txtFechaSalidaTarjetas = $("input[name*='txtFechaSalidaTarjetas']");
            txtFechaRegresoTarjetas = $("input[name*='txt2TFechaRegresoTarjetas']");

            txtFechaSalidaVuelos = $("input[name*='txtFechaSalidaTarjetas']");
            txtFechaSalidaRegreso = $("input[name*='txtFechaSalidaTarjetas']");
            ibBuscartarjeta = $("input[id*='ibBuscartarjeta']");
            /*NUMERO PASAJEROS TARJETAS*/
            DropDownListNumeroPasajerosTarjetas = $("input[name*='DDLNumeroPasajeros']");
            TablaPasajerosTarjetasAsistencia = $('#TablaConTextBox tr');
            TabVuelosRadios = $("input[name*='modal_vuelos']");
            TabHotelsRadios = $("input[name*='modal_hotel']");
            TabPlansRadios = $("input[name*='modal_plan']");
            DropDownListNinosYBebes = $("select[name*='ddlMultiNinios'],select[name*='ddlMultiBebes']");
            tabvuels_solo_ida = $("#vuelos_solo_ida");
            tabvuelos_ida_vuelta = $("#vuelos_ida_vuelta");
            tabvuelos_multi_destinos = $("#vuelos_multi_destinos");
            tabhotels_nal = $("#hotel_nal");
            tabhotels_internal = $("#hotel_internal");
            tabsBuscadorGeneral = $('#tabs').tabs({ selected: intTabIndex });
            tabsBuscadorGeneral.show();
            textboxBuscador = $('#tabs input:text');
            dropDownList = $("#tabs  select");
            txtCiudadesCarros = $("input[name*='txtCarCiud']");
            /*HOTELBETS*/
            /*AUTOCOMPLETAR*/
            texboxAutocompletar = $("input[name*='txt_Multi'],input[name*='txtCarCiud'],input[name*='txtAerolineaCiudad'], input[name*='txtCiudadDestino'], input[name*='txtCiudadSalida']");
            texboxAutocompletarHotelBets = $("#hoteles input[name*='txtCiudadDestino111']");
            /**/
            /*ELEMENTOS*/
            //tablaPasajerosHoteles = $("#tabla_Pasajeros_Hoteles");
            /*::::::INICIO TABLA HOTELES::::::*/
            var cantidad_Habitaciones_Visibles = 1;
            var dropdownlist_Habitaciones_Hoteles = $(".habitacionColumna select[id*='cmbHabitaciones']")
            function EsconderOMostrar(intCandidad) { $(".fila").hide().slice(0, intCandidad).show(); }
            try { EsconderOMostrar(parseInt(dropdownlist_Habitaciones_Hoteles[0].value)) } catch (err) { }
            dropdownlist_Habitaciones_Hoteles.change(function () {
                EsconderOMostrar(parseInt(this.value));
                cantidad_Habitaciones_Visibles = parseInt(this.value);
                MostrarOcultarTitulos();
            });
            /*::::::EDADES NIÑOS::::::*/
            var dropDownList_Cantidad_Ninos_Hoteles = $("#tablaHabitacionesHoteles .fila .columnaNinos select");
            for (c = 0; c < dropDownList_Cantidad_Ninos_Hoteles.length; c++) {
                EsconderOMostrarEdades(parseInt(dropDownList_Cantidad_Ninos_Hoteles[c].value), dropDownList_Cantidad_Ninos_Hoteles[c]);
            }
            dropDownList_Cantidad_Ninos_Hoteles.change(function () {
                EsconderOMostrarEdades(parseInt(this.value), this);
                MostrarOcultarTitulos();
            });
            function EsconderOMostrarEdades(intCandidad, dropDownList_Ninos_Hotel) {
                $(dropDownList_Ninos_Hotel).parents(".fila").find(".columnaEdad1,.columnaEdad2").hide().slice(0, intCandidad).show();
            }
            /*:::::::MOSTRAR TITULOS DE EDADES*/
            function MostrarOcultarTitulos() {
                var tabla_Habitaciones_Hoteles = $("#tablaHabitacionesHoteles");

                var titulo_Adulto = tabla_Habitaciones_Hoteles.find("thead  #Adultos");
                titulo_Adulto.hide();
                var titulo_Nino = tabla_Habitaciones_Hoteles.find("thead  #Ninos");
                titulo_Nino.hide();
                var titulo_EdadUno = tabla_Habitaciones_Hoteles.find("thead  #EdadUno");
                titulo_EdadUno.hide();
                var titulo_EdadDos = tabla_Habitaciones_Hoteles.find("thead  #EdadDos");
                titulo_EdadDos.hide();

                if (cantidad_Habitaciones_Visibles != 0) {
                    titulo_Adulto.show();
                    titulo_Nino.show();
                }

                var cantidad_ninos = 0;
                var mostrarTituloUno = false;
                var mostrarTituloDos = false;

                for (c = 0; c < cantidad_Habitaciones_Visibles; c++) {
                    cantidad_ninos = parseInt(dropDownList_Cantidad_Ninos_Hoteles[c].value);
                    if (cantidad_ninos > 1) {
                        mostrarTituloUno = true;
                        mostrarTituloDos = true;
                    } else if (cantidad_ninos == 1) {
                        mostrarTituloUno = true;
                    }
                }
                if (mostrarTituloUno) {
                    titulo_EdadUno.show();
                }
                if (mostrarTituloDos) {
                    titulo_EdadDos.show();
                }
            }


            /*::::::FIN TABLA HOTELES::::::*/

            dialogLinkIcons = $('#dialog_link, ul#icons li');
            FilasTablaEdadesNinos = $("#tblEdadesNinos tr");
            FilasTablaEdadesInfantes = $("#tblEdadesInfantes tr");
            lblErrorGeneral = $("span[id*='lblErrorGen']");
            btnEnviar = $("#tabs .botonBuscar");
            btnEnviar.click(BtnEnviar_onClick);
            /*SUSCRIBIMOS EVENTOS*/
            ibBuscartarjeta.click(btnBuscarTarjeta_OnClick);
            dialogLinkIcons.hover();
            DropDownListNumeroPasajerosTarjetas.change(ddlPasajerosTarjetas_OnChange);
            TabVuelosRadios.click(ModalidadVuelos_Click);
            TabHotelsRadios.click(ModalidadHotels_Click);
            TabPlansRadios.click(ModalidadPlans_Click);
            DropDownListNinosYBebes.change(ddlMultiEdades_OnChange);
            tabsBuscadorGeneral.bind('tabsselect', tabsBuscadorGeneral_Select);
            /*METODOS DE INICIALIZACION*/
            InicializarTabGeneral();
            InicializarTabsVuelos();
            InicializarTabsHotels();
            try {   /*INICIALIZAMOS EDADES DE LOS NIÑOS E INFANTES DE VUELOS*/
                InicializarEdades();
            } catch (err) {
                /*ERROR*/
            }
            InicializarTablaPasajerosTarjetas(DropDownListNumeroPasajerosTarjetas);
            Seleccionar_DropDownList();

            //Hide (Collapse) the toggle containers on load
            //$(".toggle_container_Preguntas").hide(); 

            //Switch the "Open" and "Close" state per click then slide up/down (depending on open/close state)
            //	        $("h4.trigger").click(function(){
            //		        $(this).toggleClass("active").next().slideToggle("slow");
            //	        });

            var linkVerMas = $("a[id*='linkVerMas']");
            $(".block").find("table").hide();
            if (linkVerMas.length) {
                linkVerMas.click(function () {
                    $(this).parents(".block").find("table").slideToggle("slow");
                    var texto = trim($(this).text());
                    if (texto == "Ver Más")
                        $(this).text('Ocultar');
                    else
                        $(this).text('Ver Más');
                });
            }
        }
    }
    function AbrirMod() {
        $("#Ventana").dialog({
            bgiframe: true,
            height: Largo,
            width: Ancho,
            modal: true
        });
    }
    function Txt_OnFocus_Vuelos() {
        /*AGREGAMOS EL CALENDARIO*/
        var dtmFechaMinima = $("input[id*='txtFechaMultiO1']");

        if (dtmFechaMinima.length) {
            if (dtmFechaMinima.attr('value') != "") {
                ShowCalendar_Rango_Minimo(this.id, dtmFechaMinima.attr('value'));
            }
            else {
                ShowCalendar_Rango_Minimo(this.id, "");
            }
        }
    }

    function Txt_OnFocus_Hoteles() {
        /*AGREGAMOS EL CALENDARIO*/
        var dtmFechaMinima = $("input[id*='txtFechaIngreso']");
        //alert(dtmFechaMinima);
        if (dtmFechaMinima.length) {
            if (dtmFechaMinima.attr('value') != "") {
                ShowCalendar_Rango_Minimo(this.id, dtmFechaMinima.attr('value'));
            }
            else {
                ShowCalendar_Rango_Minimo(this.id, "");
            }
        }
    }

    function Txt_OnFocus_Carros() {
        /*AGREGAMOS EL CALENDARIO*/
        var dtmFechaMinima = $("input[id*='txtCarFechaRecoge']");

        if (dtmFechaMinima.length) {
            if (dtmFechaMinima.attr('value') != "") {
                ShowCalendar_Rango_Minimo(this.id, dtmFechaMinima.attr('value'));
            }
            else {
                ShowCalendar_Rango_Minimo(this.id, "");
            }
        }
    }

    function Txt_OnFocus_Tarjetas() {
        /*AGREGAMOS EL CALENDARIO*/
        var dtmFechaMinima = $("input[id*='txtFechaSalidaTarjetas']");

        if (dtmFechaMinima.length) {
            if (dtmFechaMinima.attr('value') != "") {
                ShowCalendar_Rango_Minimo(this.id, dtmFechaMinima.attr('value'));
            }
            else {
                ShowCalendar_Rango_Minimo(this.id, "");
            }
        }
    }
    /*VALIDACION DE FECHAS DE LOS CALENDARIOS DE JQUERY*/
    function ShowCalendar_Rango_Minimo(control, dtmFechaMin) {
        //alert('entre al rango');
        var FormatoFecha = 'mm/dd/yy';
        //$("#" + control).datepicker( "destroy" );
        var minDateLocal = "";
        var FechaHoy = new Date();
        this.year = FechaHoy.getFullYear();
        this.month = ((FechaHoy.getMonth() + 1) < 10) ? "0" + (FechaHoy.getMonth() + 1) : FechaHoy.getMonth() + 1;
        this.day = (FechaHoy.getDate() < 10) ? "0" + FechaHoy.getDate() : FechaHoy.getDate();
        var FechaHoySep = this.month + DATE_DELIMITERLOCAL + this.day + DATE_DELIMITERLOCAL + this.year;
        document.getElementById(control).value = '';
        if (dtmFechaMin == "") {
            this.minDateLocal = FechaHoySep;
        }
        else {
            var FechaSep = dtmFechaMin.split("/");
            switch (DATE_FORMAT_DEFAULT) {
                case DATE_FORMAT_YMD:
                    this.year = FechaSep[0];
                    this.month = FechaSep[1];
                    this.day = FechaSep[2];
                    break;

                case DATE_FORMAT_MDY:
                    this.year = FechaSep[2];
                    this.month = FechaSep[0];
                    this.day = FechaSep[1];
                    break;

                case DATE_FORMAT_DMY:
                    this.year = FechaSep[2];
                    this.month = FechaSep[1];
                    this.day = FechaSep[0];
                    break;
            }
            this.minDateLocal = this.month + DATE_DELIMITERLOCAL + this.day + DATE_DELIMITERLOCAL + this.year;
        }

        var FechaMinima = Date.parse(this.minDateLocal);

        FechaHoy = Date.parse(FechaHoySep);
        var FechaMinimaNum = FechaMinima - FechaHoy;
        FechaMinimaNum = (((FechaMinimaNum / 1000) / 60) / 60) / 24;
        $("#" + control).datepicker({
            minDate: +FechaMinimaNum,
            changeMonth: true,
            changeYear: true,
            defaultDate: +0,
            numberOfMonths: 2,
            dateFormat: FormatoFecha
        });
        $("#" + control).datepicker("show");
    }
    /*VALIDACIONES DE CAMPOS VACIOS*/
    function btnBuscarTarjeta_OnClick() {
        if (txtFechaSalidaTarjetas.attr('value') == '') {
            lblErrorGeneral.text('La fecha de salida no puede estar vacia');
            return false;
        }
        if (txtFechaRegresoTarjetas.attr('value') == '') {
            lblErrorGeneral.text('La fecha de regreso no puede estar vacia');
            return false;
        }
        if ($('#TablaConTextBox input').attr('value') == '') {
            lblErrorGeneral.text('La fecha del primer pasajero no debe estar vacia');
            return false;
        }
    }

    function Validacion_Fechas() {
        if (txtFechaSalidaTarjetas.attr('value') != '' && txtFechaRegresoTarjetas.attr('value') != '') {
            var FechaInicial = Date.parse(txtFechaSalidaTarjetas.attr('value'));
            var FechaFinal = Date.parse(txtFechaRegresoTarjetas.attr('value'));
            if (FechaInicial > FechaFinal) {
                window.alert('La fecha de salida no puede ser mayor que la fecha de llegada');
                //$(this).attr('value','');				 
            }
        }
    }
    function GuardarDropDownList() {
        $.each(dropDownList, function (i, val) {
            try {
                $(val).cookify();
            } catch (err) {
                /*ERROR*/
            }
        }
		);
    }

    function Seleccionar_DropDownList() {
        if ($("#asistencia").find('select').length) {
            dropDownListTarjetas = $("#asistencia").find('select');
            $.each(dropDownListTarjetas, function (i, val) {   /*FALTA IMPLEMENTACION*/
                // alert($(val).attr('id'));
                //alert($.get($.get($(val).attr('id'))));
            }
		);

        }
    }

    function Guardar_DropDownList() {

        if ($("#asistencia").find('select').length) {
            dropDownListTarjetas = $("#asistencia").find('select');
            $.each(dropDownListTarjetas, function (i, val) {
                /*FALTA IMPLEMENTACION*/
                //alert($.get($(val).attr('id')));                                
                //$(val).cookify();

            }
		);
        }
    }

    function BtnEnviar_onClick() {
        GuardarTextbox();
        Guardar_DropDownList();
    }

    function InicializarTablaPasajerosTarjetas(objeto) {
        if ($(objeto).length) {
            var intCantidad = $(objeto).attr('value');

            if (TablaPasajerosTarjetasAsistencia.length) {
                var objFilas = TablaPasajerosTarjetasAsistencia;

                /*limpiamos*/
                $.each(objFilas, function (i, val) {
                    $(val).hide();
                });
                /*volmemos visibles*/
                $.each(objFilas, function (i, val) {
                    if (i < intCantidad) {
                        $(val).show();
                    }
                });
            }
        }
    }


    function ddlPasajerosTarjetas_OnChange() {
        InicializarTablaPasajerosTarjetas(this);
    }

    function tabsBuscadorGeneral_Select(event, ui) {
        SetTabIndex(ui.index);
        /*BORRAMOS EL MENSAJE DE ERROR*/
        lblErrorGeneral.text('');
    }
    function linksDialogIcons_Hover() {
        /*hover states on the static widgets*/
        $(this).addclass('ui-state-hover');
        $(this).removeclass('ui-state-hover');
    }
    function ModalidadVuelos_Click() {
        try {
            ColocarTabVisibleIndex(this);
        }
        catch (err) {
            /*ERROR*/
        }
        /*BORRAMOS EL MENSAJE DE ERROR*/
        lblErrorGeneral.text('');
    }
    function ModalidadHotels_Click() {
        try {
            ColocarHotelVisibleIndex(this);
        }
        catch (err) {
            /*ERROR*/
        }
        /*BORRAMOS EL MENSAJE DE ERROR*/
        lblErrorGeneral.text('');
    }
    function ModalidadPlans_Click() {
        try {
            ColocarPlanVisibleIndex(this);
        }
        catch (err) {
            /*ERROR*/
        }
        /*BORRAMOS EL MENSAJE DE ERROR*/
        lblErrorGeneral.text('');
    }
    function ddlMultiEdades_OnChange() {
        try {
            ddlMultiEdades(this);
        } catch (err) {
            /*ERROR*/
        }
    }
    /*METODOS PRIVADOS*/

    function GetTabIndex() {
        intTabIndex = 0;
        try {

            if ($.cookies.test()) {
                intTabIndex = $.cookies.get("intTabIndex");
                if (intTabIndex == null) {
                    intTabIndex = 0;
                }
            }
            return intTabIndex;
        } catch (err) {
            /*ERROR*/
        }

    }
    function SetTabIndex(intTabIndex) {
        $.cookies.set("intTabIndex", intTabIndex);
    }



    function InicializarTabGeneral() {
        if (tabsBuscadorGeneral.length) {
            tabsBuscadorGeneral.show();
            try {
                CargarTextbox();
            }
            catch (err) {
                /*ERROR*/
            }
        }
    }
    function CargarTextbox() {
        if (textboxBuscador.length) {
            $.each(textboxBuscador, function (i, val) {
                $(val).cookieFill();
            }
		    );
        }
    }
    function GuardarTextbox() {
        $.each(textboxBuscador, function (i, val) {
            $(val).cookify();
        }
		);
    }

    function Borrar_Cookies() {
        try {
            $.each(textboxBuscador, function (i, val) {
                $.del(val);
            }
		    );
        } catch (err) {
            window.alert('Error intentando borrar cookies ' + err);
        }
    }

    function InicializarTabsVuelos() {
        InicializarAutocompletar();
        $.each(TabVuelosRadios, function (i, val) {
            if ($(val).attr("checked") == true) {
                ColocarTabVisibleIndex(val);
            }
        }
		);
    }
    function InicializarTabsHotels() {
        InicializarAutocompletar();
        $.each(TabHotelsRadios, function (i, val) {
            if ($(val).attr("checked") == true) {
                ColocarHotelVisibleIndex(val);
            }
        }
		);
    }
    function InicializarEdades() {
        if (DropDownListNinosYBebes.length) {
            $.each(DropDownListNinosYBebes, function (i, val) {
                try {
                    ddlMultiEdades(val);
                } catch (err) {

                }
            }
		);
        }
    }
    function ColocarTabVisibleIndex(objeto) {
        intTabModalidadVuelos = $(objeto).attr("value");
        /*IDA Y VUELTA*/
        if (intTabModalidadVuelos == 0) {
            tabvuels_solo_ida.show();
            tabvuelos_ida_vuelta.show();
            tabvuelos_multi_destinos.hide();

        } /*SOLO IDA*/
        else if (intTabModalidadVuelos == 1) {
            tabvuels_solo_ida.show();
            tabvuelos_ida_vuelta.hide();
            tabvuelos_multi_destinos.hide();

        } /*MULTIDESTINOS*/
        else if (intTabModalidadVuelos == 2) {
            tabvuels_solo_ida.show();
            tabvuelos_ida_vuelta.hide();
            tabvuelos_multi_destinos.show();
        }
    }
    function ColocarHotelVisibleIndex(objeto) {
        intTabModalidadHotels = $(objeto).attr("value");
        /*Nacional*/
        if (intTabModalidadHotels == 0) {
            tabhotels_nal.show();
            tabhotels_internal.hide();
        } /*Internacional*/
        else {
            tabhotels_nal.hide();
            tabhotels_internal.show();
        }
    }
    function ColocarPlanVisibleIndex(objeto) {
        intTabModalidadPlans = $(objeto).attr("value");
        /*Paquete*/
        if (intTabModalidadPlans == 'PAQ') {
            $("#plan_tiposervicio").hide();
            $("#plan_fechaviaje").show();
        } /*Traslados y Toures*/
        else if (intTabModalidadPlans == 'TRA') {
            $("#plan_tiposervicio").show();
            $("#plan_fechaviaje").show();
        } /*Souvenirs*/
        else {
            $("#plan_tiposervicio").hide();
            $("#plan_fechaviaje").hide();
        }
    }
    function ddlMultiEdades(objeto) {
        var objFilas;
        var intCantidaNinos = $(objeto).attr("value");
        var strnombre = $(objeto).attr("name");
        var ddlMultiNinios = $("select[name*='ddlMultiNinios']").attr("name");
        var ddlMultiBebes = $("select[name*='ddlMultiBebes']").attr("name");

        if (strnombre == ddlMultiNinios) {
            objFilas = FilasTablaEdadesNinos;
        }
        else if (strnombre == ddlMultiBebes) {
            objFilas = FilasTablaEdadesInfantes;
        }

        /*limpiamos*/
        $.each(objFilas, function (i, val) {
            $(val).hide();
        });
        /*volmemos visibles*/
        $.each(objFilas, function (i, val) {
            if (i < intCantidaNinos) {
                $(val).show();
            }
        });
    }
    function InicializarAutocompletar() {
        /*Autocompletado llamando una pagina aspx*/
        texboxAutocompletar.autocomplete('Pagina.aspx',
		{
		    matchCase: false,
		    //tamaño de la lista
		    width: 350,
		    //se indica si se quiere tener scroll
		    scroll: true,
		    //tamaño del scroll si tiene
		    scrollHeight: 500,
		    //se indica para enviar al servidor para pedir el numero de registros
		    max: 900,
		    //se indica para que nos seleccione las palabras que contengan el texto
		    matchContains: true,
		    //se asigna metodo 
		    formatItem: FormatoItems,
		    //se asigna metodo 
		    formatResult: FormatoResultado,
		    //se asigna metodo 
		    formatMatch: FormatoCoincidencia,
		    //se indica para 
		    autoFill: false,
		    //se indica para que autocomplete con determinado numero de caracteres        
		    minChars: 3,
		    //se indica para que se pueda seleccionar varias veces
		    multiple: false,
		    //se indica si la opcion de multiple es true
		    multipleSeparator: "*",
		    //parametros del cliente 
		    extraParams: { TipoRefere: "HOTELES" }
		});

        texboxAutocompletarHotelBets.autocomplete('Pagina.aspx',
		{
		    matchCase: false,
		    //tamaño de la lista
		    width: 350,
		    //se indica si se quiere tener scroll
		    scroll: true,
		    //tamaño del scroll si tiene
		    scrollHeight: 500,
		    //se indica para enviar al servidor para pedir el numero de registros
		    max: 900,
		    //se indica para que nos seleccione las palabras que contengan el texto
		    matchContains: true,
		    //se asigna metodo 
		    formatItem: FormatoItems,
		    //se asigna metodo 
		    formatResult: FormatoResultado,
		    //se asigna metodo 
		    formatMatch: FormatoCoincidencia,
		    //se indica para 
		    autoFill: false,
		    //se indica para que autocomplete con determinado numero de caracteres        
		    minChars: 3,
		    //se indica para que se pueda seleccionar varias veces
		    multiple: false,
		    //se indica si la opcion de multiple es true
		    multipleSeparator: "*"
		    //,
		    //parametros del cliente 
		    /*extraParams:{ TipoRefere:"AEROPUERTOS" }			*/
		});

        //evento que produce cuando se selecciona un valor de la lista
        $(texboxAutocompletar).result(function (event, data, formatted) {
            //window.alert(data[0]+' : '+data[1]);
        });
        //metodo que da formato a los items de la lista
        function FormatoItems(Fila) {
            //return Fila[0] + " (<strong>id: " + Fila[1] + "</strong>)";
            return Fila[0];
        }
        //metodo que da formato al resultado que se selecciono
        function FormatoResultado(Fila) {
            //			 return Fila[0]; 
            //Gabo Lista 

            return Fila[0].replace("<BR>", "").replace("&nbsp;&nbsp;", "");
        }
        //metodo que da el formato para la coincidencia
        function FormatoCoincidencia(Fila) {
            return Fila[0] + Fila[1];
        }


    }

}
ObjBuscador = new clsJBuscador();
ObjBuscador.InicialzarJBuscador_PreInit();

function SeguirComprando(Destino, Index) {
    //alert(Index);
    $.cookies.set("intTabIndex", Index);
    location.href = Destino;
}
function SetIdHotel(IdHotel) {
    //alert(IdHotel);
    $.cookies.set("intIdHotel", IdHotel);
    $('#iHotel').attr('src', 'Hotel.aspx?intIdHotel=' + IdHotel);
    $find('MPEEHotel').show();
    //$object("MPEEHotel").show();
}
function SetAbrirMPEE(PaginaDestino) {
    $('#iMPEE').attr('src', PaginaDestino);
    $find('MPEEGeneral').show();
}
function SetIdBoletin(IdBoletin) {
    //alert(IdHotel);
    $.cookies.set("intIdBoletin", IdBoletin);
    $('#iBoletin').attr('src', 'DetalleBoletin.aspx?Id=' + IdBoletin);
    $find('MPEEBoletin').show();
    //$object("MPEEHotel").show();
}
function SetIdHotelRot(IdHotel) {
    //alert(IdHotel);
    $.cookies.set("intIdHotelRot", IdHotel);
    $('#iHotelRot').attr('src', 'Hotel.aspx?intIdHotel=' + IdHotel);
    $find('MPEEHotelRot').show();
    //$object("MPEEHotel").show();
}
function SetIdTasa(Id) {
    //alert(IdHotel);
    $.cookies.set("intIdTasa", Id);
    $('#iHotel').attr('src', 'TasasCar.aspx?Id=' + Id);
    $find('MPEEHotel').show();
    //$object("MPEEHotel").show();
}
// Para abir una pagina en una modal
function SetIdPagina(Pagina) {
    $('#iPagina').attr('src', Pagina);
    $find('MPEEPagina').show();
}
function SetIdTarifaAir(Id) {
    //alert(IdHotel);
    $.cookies.set("intIdTarifa", Id);
    $('#iHotel').attr('src', 'TarifasAir.aspx?Id=' + Id);
    $find('MPEEHotel').show();
    //$object("MPEEHotel").show();
}
function SetPlanRela(idPlan, sTipoPlan, sIdSesion, Plantilla) {
    //alert(idPlan);  
    //alert('DetalleTourRela.aspx.aspx?id=' + idPlan + '&Codigo=' + idPlan + '&TipoPlan=' + sTipoPlan + '&idSesion=' + sIdSesion);    
    $('#iAdicionales').attr('src', 'DetalleTourRela.aspx?id=' + idPlan + '&Codigo=' + idPlan + '&TipoPlan=' + sTipoPlan + '&idSesion=' + sIdSesion + '&Plantilla=' + Plantilla);
    $find('MPEEAdicionales').show();
}
function SetEnviar() {
    //alert('entre');	       
    $find('MPEEEnvioAmigo').show();
}

function SetEnviarDesc() {
    //alert('entre');	       
    $find('MPEEEnvioAmigoDesc').show();
}

function SetIdHotelRotMulti(IdHotel, IdPlan) {
    //alert(IdHotel + '/' + IdPlan);    
    $('#iHotelRot').attr('src', 'HotelesMulti.aspx?intIdCat=' + IdHotel + '&id=' + IdPlan);
    $find('MPEEHotelRot').show();
    //$object("MPEEHotel").show();
}
function SetIdBarco(IdBarco) {
    //alert(IdBarco);
    $.cookies.set("intIdBarco", IdBarco);
    $('#iBarco').attr('src', 'Barco.aspx?intIdHotel=' + IdBarco);
    $find('MPEEBarco').show();
    //$object("MPEEHotel").show();
}
function SetIdHotel1(IdHotel, IdPlan) {
    //    alert(IdHotel);
    $.cookies.set("intIdPlan", IdHotel);
    $.cookies.set("intIdPlanP", IdPlan);
    $('#iHotel1').attr('src', 'Toures.aspx?intIdPlan=' + IdHotel + '&Codigo=' + IdPlan);
    $find('MPEEHotel1').show();
    //$object("MPEEHotel").show();
}
function MostrarVentanaConfirmacion() {
    //alert('entre');
    $find('MPEEConf').show();
}

function SetEsconderFacturacion(bEsconder) {
    if (bEsconder == 1) {
        $('#ucCarroCompras_dvFacturacion').show("slow");
    }
    else {
        $('#ucCarroCompras_dvFacturacion').hide("slow");
    }
}

function AbrirVentana(Division, PaginaDestino, Largo, Ancho) {

    //alert(PaginaDestino);
    if ($("#" + Division).length) {
        $("#" + Division).load(PaginaDestino);

        $("#" + Division).dialog({

            bgiframe: true,

            height: Largo,

            width: Ancho,

            modal: true,

            hide: 'slide',

            show: 'slide'

        });
    }
}

function mostrarPopUp(iProceso) {
    switch (iProceso) {
        case '1':
            if (terminos.style.display == 'none') {
                terminos.style.display = 'block';
            }
            else {
                terminos.style.display = 'none';
            }
            break;
        case "2":
            if (quienes.style.display == 'none') {
                quienes.style.display = 'block';
            }
            else {
                quienes.style.display = 'none';
            }
            break;
        case "3":
            if (amigo.style.display == 'none') {
                amigo.style.display = 'block';
            }
            else {
                amigo.style.display = 'none';
            }
            break;
        case "4":
            if (vAgencia.style.display == 'none') {
                vAgencia.style.display = 'block';
            }
            else {
                vAgencia.style.display = 'none';
            }
            break;
        case "5":
            if (vUsuario.style.display == 'none') {
                vUsuario.style.display = 'block';
            }
            else {
                vUsuario.style.display = 'none';
            }
            break;
        case "6":
            if (vContacto.style.display == 'none') {
                vContacto.style.display = 'block';
            }
            else {
                vContacto.style.display = 'none';
            }
            break;
        case "7":
            if (mapa.style.display == 'none') {
                mapa.style.display = 'block';
            }
            else {
                mapa.style.display = 'none';
            }
            break;
    }
}

function mostrarOcultarComponente(iProceso) {
    switch (iProceso) {
        case '1':
            if (vuelos_multi_destino3.style.display == 'none') {
                agregarOcultar1.style.display = 'none';
                agregarOcultar2.style.display = 'block';
                vuelos_multi_destino3.style.display = 'block';
                vuelos_multi_destino4.style.display = 'block';
            }
            else {
                agregarOcultar2.style.display = 'none';
                agregarOcultar3.style.display = 'block';
                vuelos_multi_destino5.style.display = 'block';
                vuelos_multi_destino6.style.display = 'block';
            }
            break;
        case '2':
            if (vuelos_multi_destino5.style.display == 'none') {
                agregarOcultar2.style.display = 'none';
                agregarOcultar1.style.display = 'block';
                vuelos_multi_destino3.style.display = 'none';
                vuelos_multi_destino4.style.display = 'none';
            }
            else {
                agregarOcultar3.style.display = 'none';
                agregarOcultar2.style.display = 'block';
                vuelos_multi_destino5.style.display = 'none';
                vuelos_multi_destino6.style.display = 'none';
            }
            break;

    }
}

/* Busqueda avanzada buscador aereo */
$(document).ready(function () {
    $("#avanzadas").hide();

    $(".show_hide").show();

    $('.show_hide').click(function () {
        $("#avanzadas").slideToggle();
    });

});

function EsconderDetalle(Control) {
    $('#' + Control).toggle();
}

function AbrirModalSola(Control, Largo, Ancho) {
    var Cont = $("#" + Control);
    $("#Ventana2").dialog({
        bgiframe: true,
        height: Largo,
        width: Ancho,
        modal: true//,	    
        //hide: 'slide',
        //show: 'slide'
    });
}
/*HABILITAR O DESHABILAR BOTON DE RESERVA CON EL CHECKBOX*/
$(document).ready(Pagina_Reserva_Cargar);
$(document).ready(Pagina_Reserva_Hotel_Cargar);
$(document).ready(Pagina_Reserva_Circuito_Cargar);


function Pagina_Reserva_Cargar() {
    /*OBTENEMOS EL CHECKBOX*/
    var chkAcepto = $("input[id*='cbAcepto']");
    /*SUSCRIBIMOS EL METODO AL EVENTO CLICK*/
    chkAcepto.click(chkAcepto_Click);
    /*LLAMAMOS LA FUNCION QUE MOS HABILITA O DESHABILTA EL BOTON*/
    HabilarDeshabilitar(chkAcepto);
}
function Pagina_Reserva_Hotel_Cargar() {
    /*OBTENEMOS EL CHECKBOX*/
    var chkAcepto_Hotel = $("input[id*='ucReservaHotel_cbAceptar']");
    /*SUSCRIBIMOS EL METODO AL EVENTO CLICK*/
    chkAcepto_Hotel.click(chkAcepto_Hotel_Click);
    /*LLAMAMOS LA FUNCION QUE MOS HABILITA O DESHABILTA EL BOTON*/
    HabilarDeshabilitarHotel(chkAcepto_Hotel);
}

function Pagina_Reserva_Circuito_Cargar() {
    /*OBTENEMOS EL CHECKBOX*/
    var chkAcepto_Circuito = $("input[id*='ucReservaCircuito_cbAcepto']");
    /*SUSCRIBIMOS EL METODO AL EVENTO CLICK*/
    chkAcepto_Circuito.click(chkAcepto_Circuito_Click);
    /*LLAMAMOS LA FUNCION QUE MOS HABILITA O DESHABILTA EL BOTON*/
    HabilarDeshabilitarCircuito(chkAcepto_Circuito);
}

function HabilarDeshabilitar(chkAcepto) {
    /*OBTNEMOS EL BOTON DE CONFIRMAR*/
    var btnConfirmar = $("input[id*='btnConfirmar']");
    /*VERIFICAMOS SI EL CHECKBOX EXISTE*/
    if (chkAcepto.length) {
        if (chkAcepto.attr("checked")) {
            btnConfirmar.attr("disabled", "");
        }
        else {
            btnConfirmar.attr("disabled", "true");
        }
    }
}

function HabilarDeshabilitarCircuito(chkAcepto_Circuito) {
    /*OBTNEMOS EL BOTON DE CONFIRMAR*/
    var btnConfirmar = $("input[id*='ucReservaCircuito_btnConfirmar']");
    /*VERIFICAMOS SI EL CHECKBOX EXISTE*/
    if (chkAcepto_Circuito.length) {
        if (chkAcepto_Circuito.attr("checked")) {
            btnConfirmar.removeAttr("disabled");
        }
        else {
            btnConfirmar.attr("disabled", "false");
        }
    }
}

function HabilarDeshabilitarHotel(chkAcepto_Hotel) {
    /*OBTNEMOS EL BOTON DE CONFIRMAR*/
    var btnConfirmar = $("input[id*='ucReservaHotel_btnFinalizar']");
    /*VERIFICAMOS SI EL CHECKBOX EXISTE*/
    if (chkAcepto_Hotel.length) {
        if (chkAcepto_Hotel.attr("checked")) {
            btnConfirmar.removeAttr("disabled");           
        }
        else {
            btnConfirmar.attr("disabled", "false");            
        }
    }
}
function chkAcepto_Click() {
    var chkAcepto = $(this);
    HabilarDeshabilitar(chkAcepto);
}
function chkAcepto_Hotel_Click() {
    var chkAcepto_Hotel = $(this);
    HabilarDeshabilitarHotel(chkAcepto_Hotel);
}

function chkAcepto_Circuito_Click() {
    var chkAcepto_Circuito = $(this);
    HabilarDeshabilitarCircuito(chkAcepto_Circuito);
}

/*ABRE VENTANA DE ENVIAR_AMIGO_BLOG*/
function AbrirModBlogEnviarAmigo() {
    $("#Ventana").dialog({
        bgiframe: true,
        height: 735,
        width: 500,
        modal: true,
        hide: 'slide',
        show: 'slide',
        resizable: true
    });
}
function Traductor(control, divresultado) {
    /* obtenemos el texto y los idiomas origen y destino*/
    var nombid = control;
    var text = "";
    text = document.getElementById(control).value;
    var resultado = document.getElementById(divresultado);
    if (text != "") {
        var srcLang = "en";
        var dstLang = "es";
        /* llamada al traductor  */
        var arr_texto = text.split('.');
        var resultado_traduccion = "";

        for (var cont = 0; cont < arr_texto.length; cont++) {
            if (arr_texto[cont] != "") {
                google.language.translate(arr_texto[cont], srcLang, dstLang,
                function (result) {
                    if (!result.error) {
                        resultado_traduccion += result.translation;
                        resultado.innerHTML = resultado_traduccion
                    }
                    else
                        alert(result.error.message);
                }
                );
            }
        }
    }
}

function TraductorVisible() {  /*VOLVEMOS VISIBLE EL ENVIAR A UN AMIGO*/
    $find('ucDetalleVuelo_MPEEnviarAmigo').show();
}
/*IMPRIMIR PAGINA*/
function Imprimir_Pagina() {
    window.print();
}
function GuardarSession() {
    try {
        var SesionId = document.getElementById("hdfSesionId").value;
        if (SesionId != "") {
            //guardamos la cookie
            document.cookie = SesionId;
            sessvars.SessionID = SesionId;
            //            alert("sesion GuardarSesion " + SesionId);
        }
    } catch (err) {
        //        alert("Error GuardarSesion");
    }
}
function GuardarSessionTab(intTabIndexSet) {
    try {
        var SesionId = document.getElementById("hdfSesionId").value;
        if (SesionId != "") {
            //guardamos la cookie
            document.cookie = SesionId;
            sessvars.SessionID = SesionId;
            //            alert("sesion GuardarSesion " + SesionId);
        }
        Set_Tab_Buscador(intTabIndexSet);
    }
    catch (err) {
        //        alert("Error GuardarSesion");
    }
}

function Set_Tab_Buscador(intTabIndexSetBusc) {
    try {
        $.cookies.set("intTabIndex", intTabIndexSetBusc);
    }
    catch (err) {

    }
}

function RecuperaSession() {
    try {
        var SesionId = document.getElementById("hdfSesionId");
        if (SesionId.value == "") {
            if (sessvars.SesionId != "") {
                SesionId.value = sessvars.SessionID;
            }
            else {
                SesionId.value = document.cookie;
            }
        }
    }
    catch (err) {
    }
}
function RedirectPage(Page, Parameters) {
    try {
        var Url = Page;
        GuardarSession();
        if (Parameters != "") {
            if (Page.Container("?")) {
                Url = Page + "&" + Parameters;
            }
            else {
                Url = Page + "?" + Parameters;
            }
        }
        cambiar(Url);
    } catch (err) {

    }
}
function RedirectPage(Page) {
    try {
        var Url = Page;
        GuardarSession();
        if (Parameters != "") {
            if (Page.Container("?")) {
                Url = Page + "&" + Parameters;
            }
            else {
                Url = Page + "?" + Parameters;
            }
        }
        cambiar(Url);
    } catch (err) {

    }
}
function RedirectUser(Page, Tipo) {
    try {
        var txtUsuario = $("input[id*='txtUsuario']");
        var txtPassword = $("input[id*='txtPassword']");

        //        alert(txtUsuario);        
        //        alert(txtPassword);

        var Url = Page;
        //GuardarSession();
        if (!(Page.indexOf("?") == -1)) {
            Url = Page + "&ParamHtm=" + Tipo + "&User=" + txtUsuario.val() + "&Pass=" + txtPassword.val();
        }
        else {
            Url = Page + "?ParamHtm=" + Tipo + "&User=" + txtUsuario.val() + "&Pass=" + txtPassword.val();
        }
        cambiar(Url);
    } catch (err) {
        alert(err);
    }
}
function EsconderBuscadores() {
    if ($find('MPEEBuscador') != null) {
        $find('MPEEBuscador').hide();
    }
    if ($find('MPEEBuscadorAereo') != null) {
        $find('MPEEBuscadorAereo').hide();
    }
    if ($find('MPEEBuscadorHotel') != null) {
        $find('MPEEBuscadorHotel').hide();
    }
    if ($find('MPEEBuscadorPlanes') != null) {
        $find('MPEEBuscadorPlanes').hide();
    }
    if ($find('MPEEBuscadorAutos') != null) {
        $find('MPEEBuscadorAutos').hide();
    }
    if ($find('MPEEBuscadorSeguros') != null) {
        $find('MPEEBuscadorSeguros').hide();
    }
}
// General
function Show_Cortinilla() {
    try {
        EsconderBuscadores();
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlash").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_Cortinilla").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}
function Show_CortinillaBFM() {
    try {
        EsconderBuscadores();
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlasBFM").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_CortinillaBFM").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}






// ??
function Show_CortinillaTab(intTabIndexSet) {
    try {
        EsconderBuscadores();
        Set_Tab_Buscador(intTabIndexSet);
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlash").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_Cortinilla").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}

function Set_Tab_Buscador(intTabIndexSetBusc) {
    try {
        $.cookies.set("intTabIndex", intTabIndexSetBusc);
    }
    catch (err) {

    }
}
// Espera
function Show_CortinillaMenu() {
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlashMenu").dialog({
            resizable: false,
            modal: true,
            height: 220,
            width: 450,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_CortinillaMenu").dialog({
            resizable: false,
            modal: true,
            height: 220,
            width: 450,
            closeOnEscape: false

        });
    }

}
// Air
function Show_CortinillaAir() {
    try {
        EsconderBuscadores();
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlashAir").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_CortinillaAir").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}
// Car
function Show_CortinillaCar() {
    try {
        EsconderBuscadores();
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlashCar").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_CortinillaCar").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}
// Hotel
function Show_CortinillaHot() {
    try {
        EsconderBuscadores();
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlashHot").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_CortinillaHot").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}
// Planes
function Show_CortinillaPlan() {
    try {
        EsconderBuscadores();
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlashPlan").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_CortinillaPlan").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}
// Asistencia
function Show_CortinillaAsis() {
    try {
        EsconderBuscadores();
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlashAsis").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_CortinillaAsis").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}
// Cruceros
function Show_CortinillaCrc() {
    try {
        EsconderBuscadores();
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlashCrc").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_CortinillaCrc").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}
function Show_Cortinilla_Interna() {
    try {
        EsconderBuscadores();
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }
    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlashInterna").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_Cortinilla_Interna").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}

function Show_Cortinilla_Confirmacion() {
    try {
        GuardarSession();
    } catch (err) {
    }
    var txtEdades = $("input[id*='txtEdad1']");
    var blValidaciontxtEdades = true;

    $.each(txtEdades, function (i, val) {
        try {
            if ($(val).attr('value') == "") {
                blValidaciontxtEdades = false;

            }

        } catch (err) {

        }
    });
    /*SI NO HAY NINGUN CAMPO VACION MUESTRA EL MENSAJE*/
    if (blValidaciontxtEdades) {
        var buttons = $("#div_Cortinilla").dialog({
            modal: true,
            height: 450,
            width: 450,
            closeOnEscape: false

        });

    } else {
        alert('Por favor ingrese la fecha de nacimiento  de(los) infante(s) en el campo de edad.');
    }

    return blValidaciontxtEdades;
}

function validarbusquedagen() {
    intTabIndex = GetTabIndex();

    if (intTabIndex == 0) {
        var txt_Multi_O1 = $("input[id*='txt_Multi_O1']");
        var txtFechaMultiO1 = $("input[id*='txtFechaMultiO1']");
        var txt_Multi_D1 = $("input[id*='txt_Multi_D1']");
        var txt2VFechaMulti = $("input[id*='txt2VFechaMulti']");
        iPos = 1;
        $.each(TabVuelosRadios, function (i, val) {
            if ($(val).attr("checked") == true) {
                iPos = i;
            }
        }
		    );
        if (txt_Multi_O1.attr('value').length < 17) {
            txt_Multi_O1.focus();
            return false;
        }
        else {
            if (txtFechaMultiO1.attr('value') == '') {
                txtFechaMultiO1.focus();
                return false;
            }
            else {
                if (txt_Multi_D1.attr('value').length < 17) {
                    txt_Multi_D1.focus();
                    return false;
                }
                else {
                    if (iPos == 1) {
                        Show_Cortinilla()
                        return true;
                    }
                    else {
                        if (txt2VFechaMulti.attr('value') == '') {
                            txt2VFechaMulti.focus();
                            return false;
                        }
                        else {
                            Show_Cortinilla()
                            return true;
                        }
                    }
                }
            }
        }
    }
    else {
        if (intTabIndex == 1) {
            var txtCiudadDestino = $("input[id*='txtCiudadDestino']");
            var ddlCiudades = $("input[id*='ddlCiudades']");
            var txtFechaIngreso = $("input[id*='txtFechaIngreso']");
            var txt2HFechaSalida = $("input[id*='txt2HFechaSalida']");
            iPosH = 1;
            $.each(TabHotelsRadios, function (i, val) {
                if ($(val).attr("checked") == true) {
                    iPosH = i;
                }
            }
	            );

            if (iPosH == 1) {
                if (txtCiudadDestino.attr('value').length < 17) {
                    txtCiudadDestino.focus();
                    return false;
                }
            }
            else {
                if (ddlCiudades.attr('value') == '') {
                    ddlCiudades.focus();
                    return false;
                }
            }
            if (txtFechaIngreso.attr('value') == '') {
                txtFechaIngreso.focus();
                return false;
            }
            else {
                if (txt2HFechaSalida.attr('value') == '') {
                    txt2HFechaSalida.focus();
                    return false;
                }
                else {
                    Show_Cortinilla()
                    return true;
                }
            }
        }
        else {
            if (intTabIndex == 2) {
                var txtCarCiudRecoge = $("input[id*='txtCarCiudRecoge']");
                var txtCarFechaRecoge = $("input[id*='txtCarFechaRecoge']");
                var txtCarCiudEntrega = $("input[id*='txtCarCiudEntrega']");
                var txt2CFechaEntrega = $("input[id*='txt2CFechaEntrega']");
                if (txtCarCiudRecoge.attr('value').length < 17) {
                    txtCarCiudRecoge.focus();
                    return false;
                }
                else {
                    if (txtCarFechaRecoge.attr('value') == '') {
                        txtCarFechaRecoge.focus();
                        return false;
                    }
                    else {
                        if (txtCarCiudEntrega.attr('value').length < 17) {
                            txtCarCiudEntrega.focus();
                            return false;
                        }
                        else {
                            if (txt2CFechaEntrega.attr('value') == '') {
                                txt2CFechaEntrega.focus();
                                return false;
                            }
                            else {
                                Show_Cortinilla()
                                return true;
                            }
                        }
                    }
                }
            }
        }
    }
}
/*
CSS Browser Selector v0.3.4 (Sep 29, 2009)
Rafael Lima (http://rafael.adm.br)
http://rafael.adm.br/css_browser_selector
License: http://creativecommons.org/licenses/by/2.5/
Contributors: http://rafael.adm.br/css_browser_selector#contributors
*/
function css_browser_selector(u) { var ua = u.toLowerCase(), is = function (t) { return ua.indexOf(t) > -1; }, g = 'gecko', w = 'webkit', s = 'safari', o = 'opera', h = document.getElementsByTagName('html')[0], b = [(!(/opera|webtv/i.test(ua)) && /msie\s(\d)/.test(ua)) ? ('ie ie' + RegExp.$1) : is('firefox/2') ? g + ' ff2' : is('firefox/3.5') ? g + ' ff3 ff3_5' : is('firefox/3') ? g + ' ff3' : is('gecko/') ? g : is('opera') ? o + (/version\/(\d+)/.test(ua) ? ' ' + o + RegExp.$1 : (/opera(\s|\/)(\d+)/.test(ua) ? ' ' + o + RegExp.$2 : '')) : is('konqueror') ? 'konqueror' : is('chrome') ? w + ' chrome' : is('iron') ? w + ' iron' : is('applewebkit/') ? w + ' ' + s + (/version\/(\d+)/.test(ua) ? ' ' + s + RegExp.$1 : '') : is('mozilla/') ? g : '', is('j2me') ? 'mobile' : is('iphone') ? 'iphone' : is('ipod') ? 'ipod' : is('mac') ? 'mac' : is('darwin') ? 'mac' : is('webtv') ? 'webtv' : is('win') ? 'win' : is('freebsd') ? 'freebsd' : (is('x11') || is('linux')) ? 'linux' : '', 'js']; c = b.join(' '); h.className += ' ' + c; return c; }; css_browser_selector(navigator.userAgent);


function OcultarFilas() {
    var Con = document.getElementById("ucBuscador_cmbHabitaciones").value;
    try {
        var Adultos = 10;
        for (var i = 1; i < Adultos; i++) {
            FilaAdultos = $("#adulto" + i);
            FilaNinos = $("#nino" + i);
            if (i <= Con) {
                FilaAdultos.show();
                FilaNinos.show();
            }
            else {
                FilaAdultos.hide();
                FilaNinos.hide();
            }
        }
    }
    catch (ex) {
        //alert(ex.error);
    }
}

function Ocultar() {
    var fila = document.getElementsByName("fila");
    for (k = 2; k < fila.length; k++) {
        fila[k].style.display = "none";
    }

    for (i = 1; i <= 9; i++) {
        var dato = "Edad" + i;
        var filaEdad = document.getElementsByName(dato);
        var iCon = 0;
        while (iCon < filaEdad.length) {
            filaEdad[iCon].style.display = "none";
            iCon++;
        }
    }

    var filaOc = document.getElementsByName("fila0");
    for (j = 0; j < filaOc.length; j++) {
        filaOc[j].style.display = "none";
    }
    OcultarEdades('1');

    OcultarFilas();
}

function OcultarEdades(id) {
    try {
        var dato = "Edad" + id;
        var filaEdad = document.getElementsByName(dato);
        var Hab = document.getElementById("ucBuscador_cmbHabitaciones").value;
        var Campo = "ucBuscador_cmbNiños" + id;
        var Selec = document.getElementById(Campo).value;
        var k = 0;
        var i = 0;

        while (k < filaEdad.length) {
            filaEdad[k].style.display = "none";
            k++;
        }

        while (i < Selec) {
            if (filaEdad[i].style.display == "none") {
                filaEdad[i].style.display = "";
            }
            i++;
        }

    } catch (ex) {
        //alert(ex);
    }
}

function ActualizarRegreso() {
    var FechaSalida = document.getElementById("ucBuscador_txt2HFechaSalida").value;
    var Dias = document.getElementById("ucBuscador_cmbNoches").value;

    FechaSalida = FechaSalida.split('/');
    FechaSalida = FechaSalida[0] + '/' + FechaSalida[1] + '/' + FechaSalida[2];

    var miFechaSal = new Date(FechaSalida);
    miFechaSal.setTime(miFechaSal.getTime() + Dias * 24 * 60 * 60 * 1000);

    var mes = miFechaSal.getMonth() + 1;
    if (mes <= 9) {
        mes = "0" + mes;
    }

    var dia = miFechaSal.getDate();
    if (dia <= 9) {
        dia = "0" + dia
    }
    var FechaTotal = mes + "/" + dia + "/" + miFechaSal.getFullYear();

    var FechaRegreso = document.getElementById("ucBuscador_txt2HFechaSalida");
    FechaRegreso.value = FechaTotal;
}

function ActualizarSalida() {
    var FechaSalida = document.getElementById("ucBuscador_txtFechaIngreso").value;
    var FechaRegreso = document.getElementById("ucBuscador_txt2HFechaSalida").value;

    FechaSalida = FechaSalida.split('/');
    FechaSalida = FechaSalida[0] + '/' + FechaSalida[1] + '/' + FechaSalida[2];

    FechaRegreso = FechaRegreso.split('/');
    FechaRegreso = FechaRegreso[0] + '/' + FechaRegreso[1] + '/' + FechaRegreso[2];

    var miFecha1 = new Date(FechaSalida);
    var miFecha2 = new Date(FechaRegreso);

    var cmbNoches = document.getElementById("ucBuscador_cmbNoches");
    if (miFecha2.getTime() < miFecha1.getTime()) {
        alert("CheckOut no puede ser inferior a CheckIn");
        cmbNoches.selectedIndex = 0;
        ActualizarRegreso();
    }
    else {
        var diferencia = miFecha2.getTime() - miFecha1.getTime();
        var dias = Math.floor(diferencia / (1000 * 60 * 60 * 24));
        //cmbNoches.selectedIndex = dias;
    }
}

function OcultarFilasInterno() {
    try {
        var Con = document.getElementById("ucBuscadorInterno_cmbHabitacionesInterno").value;
        var Hab = Con;
        var fila = document.getElementsByName("fila");
        var i = 2;
        var k = 2;
        var j = 0;
        var l = 0;
        Con = parseInt(Con) + parseInt(Con);

        while (k < fila.length) {
            fila[k].style.display = "none";
            k++;
        }

        while (i < Con) {
            if (fila[i].style.display == "none") {
                fila[i].style.display = "";
            }
            i++;
        }

        while (Hab < 9) {
            var Control = "ucBuscadorInterno_cmbNiños" + Hab;
            var campo = document.getElementById(Control);
            OcultarEdadesInterno(Hab);
            Hab++;
        }
    } catch (ex) {
        //alert(ex.error);
    }
}

function OcultarInterno() {
    var fila = document.getElementsByName("fila");
    for (k = 2; k < fila.length; k++) {
        fila[k].style.display = "none";
    }

    for (i = 1; i <= 9; i++) {
        var dato = "Edad" + i;
        var filaEdad = document.getElementsByName(dato);
        var iCon = 0;
        while (iCon < filaEdad.length) {
            filaEdad[iCon].style.display = "none";
            iCon++;
        }
    }

    var filaOc = document.getElementsByName("fila0");
    for (j = 0; j < filaOc.length; j++) {
        filaOc[j].style.display = "none";
    }
    OcultarEdadesInterno('1');

    OcultarFilasInterno();
}

function OcultarEdadesInterno(id) {
    try {
        var dato = "Edad" + id;
        var filaEdad = document.getElementsByName(dato);
        var Hab = document.getElementById("ucBuscadorInterno_cmbHabitacionesInterno").value;
        var Campo = "ucBuscadorInterno_cmbNiños" + id;
        var Selec = document.getElementById(Campo).value;
        var k = 0;
        var i = 0;

        while (k < filaEdad.length) {
            filaEdad[k].style.display = "none";
            k++;
        }

        while (i < Selec) {
            if (filaEdad[i].style.display == "none") {
                filaEdad[i].style.display = "";
            }
            i++;
        }

    } catch (ex) {
        //alert(ex);
    }
}

// Necesario para manejo de ventanas emergentes - caso traslados

function Listar(NombreControl, Padre) {
    //alert(NombreControl); 
    Control = NombreControl;
    $.ajax({
        type: "POST",
        url: urlashx,
        data: "Control=" + NombreControl + "&padre=" + Padre,
        success: function (html) {
            $("#" + Control).html(html);
        }
    });
}


$(document).ready(function () {
    $("#Ventana").hide();
    $('.ClaseModal').click(function () {
        $('#Ventana').dialog('open');
    });
    $('.ClaseItinerario').click(function () {
        $('#Ventana').dialog('open');
    });
});

$(document).ready(function () {
    $("#Ventana2").hide();
    $('.ClaseModal2').click(function () {
        $('#Ventana2').dialog('open');
    });
    $('.ClaseEnvio').click(function () {
        $('#Ventana2').dialog('open');
    });
    $('.ClaseEnvioSouv').click(function () {
        $('#Ventana2').dialog('open');
    });

    /*FUNCION PARA ROTAR LOS BANNERS*/
    rotarBannersHome(0);
});

function EsconderDetalle(Control) {
    $('#' + Control).toggle();
}

function EsconderPanelDetalle(ControlEsconder, ControlMostrar) {
    //alert(ControlEsconder);
    $('#' + ControlEsconder).hide();
    $('#' + ControlMostrar).show();
}

function CargarYEsconder(Control, PaginaDestino) {
    $('.Esconder').hide("slow");
    $('#' + Control).toggle("slow");
}

function AbrirModal(Control, PaginaDestino, Id, Largo, Ancho, TipoDetalle) {
    var Cont = $("#" + Control);
    var Pos = Cont.position();
    var Izq = Pos.left;
    var Arriba = Pos.top + Cont.height();
    //alert(Izq + "-"+ Arriba);
    $("#Ventana").load(PaginaDestino + "?Id=" + Id + "&Tipodetalle=" + TipoDetalle);
    $("#Ventana").dialog({
        bgiframe: true,
        height: Largo,
        width: Ancho,
        modal: true,
        //position: [Izq,Arriba],
        hide: 'slide',
        show: 'slide'
    });
}

function AbrirVentana(Division, PaginaDestino, Largo, Ancho) {
    //alert(PaginaDestino); 
    $("#" + Division).load(PaginaDestino);
    $("#" + Division).dialog({
        bgiframe: true,
        height: Largo,
        width: Ancho,
        modal: true,
        hide: 'slide',
        show: 'slide'
    });
}

function AbrirModalSola(Control, Largo, Ancho) {
    var Cont = $("#" + Control);
    var Pos = Cont.position();
    var Izq = Pos.left;
    var Arriba = Pos.top + Cont.height();

    $("#Ventana2").dialog({
        bgiframe: true,
        height: Largo,
        width: Ancho,
        modal: true,
        //position: [Izq,Arriba],
        hide: 'slide',
        show: 'slide'
    });
}

function AbrirModalSola2(Largo, Ancho) {
    $("#Ventana2").dialog({
        bgiframe: true,
        height: Largo,
        width: Ancho,
        modal: true,
        hide: 'slide',
        show: 'slide'
    });
}

function AbrirModalYCargarIframe(Largo, Ancho, PaginaCargar, LargoFrame, Anchoframe) {
    document.getElementById("iDetalle").src = PaginaCargar;
    document.getElementById("iDetalle").width = Anchoframe;
    document.getElementById("iDetalle").height = LargoFrame;
    $("#Ventana2").dialog({
        bgiframe: true,
        height: Largo,
        width: Ancho,
        modal: true,
        hide: 'slide',
        show: 'slide'
    });
}

function AbrirModalYCargarIframeDesact(Largo, Ancho, PaginaCargar, LargoFrame, Anchoframe) {
    document.getElementById("iDetalle").src = PaginaCargar;
    document.getElementById("iDetalle").width = Anchoframe;
    document.getElementById("iDetalle").height = LargoFrame;
    $("#Ventana2").dialog({
        bgiframe: true,
        height: Largo,
        width: Ancho,
        modal: true,
        hide: 'slide',
        show: 'slide'
    });
}

function changeContent(id, shtml) {
    if (document.getElementById || document.all) {
        var el = document.getElementById ? document.getElementById(id) : document.all[id];
        if (el && typeof el.innerHTML != "undefined") el.innerHTML = "<img src='" + shtml + "' border='0' width='235' height='116'  />";
    }
}
function cambiar(url) {
    window.top.location = url;
    // opener.location=url;
}

function cambiarSesion(url) {
    try {
        //GuardarSession();
        var SesionId = document.getElementById("hdfSesionId");
        if (SesionId.value == "") {
            if (sessvars.SesionId != "") {
                SesionId.value = sessvars.SessionID;
            }
            else {
                SesionId.value = document.cookie;
            }
        }

        url = url + "?idSesion=" + SesionId.value;
    } catch (err) {
    }
    window.top.location = url;
}

function loadinparent(url) {
    //alert('entre');
    parent.location.href = url;
    self.close();
}

function rotarBannersHome(posicion) {
    var banners = $(".menuPromo > a");
    var siguienteBanner = 0;
    banners.eq(posicion).click();
    banners.eq(posicion).removeClass("promo");
    banners.eq(posicion).addClass("wactive promo");
    if (banners.length > posicion) {
        siguienteBanner = posicion + 1;
    } else {
        siguienteBanner = 0;
    }
    setTimeout("rotarBannersHome(" + siguienteBanner + ")", 10000);
}


function GetCampos() {
    var sStr = "";
    var Tabla = document.getElementById('tblFormAdicional');
    if (Tabla != null) {
        var Filas = Tabla.getElementsByTagName('tr');
        for (i = 0; i < Filas.length; i++) {
            Celdas = Filas[i].getElementsByTagName('td');
            for (x = 0; x < Celdas.length; x++) {
                Controles = Celdas[x].getElementsByTagName('input');
                if (Controles.length != 0) {
                    for (a = 0; a < Controles.length; a++) {
                        if (Controles[a].type == 'checkbox') {
                            if (Controles[a].checked) {
                                sStr += 'Si' + '    '
                            }
                            else {
                                sStr += 'No' + '    '
                            }
                        }
                        else {
                            sStr += Controles[a].value + '    ';
                        }
                    }
                    sStr += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                else {
                    Controles = Celdas[x].getElementsByTagName('select');
                    if (Controles.length != 0) {
                        for (a = 0; a < Controles.length; a++) {
                            sStr += Controles[a].value + '    ';
                        }
                        sStr += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                    else {
                        Controles = Celdas[x].getElementsByTagName('table');
                        if (Controles.length != 0) {
                            //alert('entro table');
                            for (a = 0; a < Controles.length; a++) {
                                Controles = Celdas[x].getElementsByTagName('input');
                                if (Controles.length != 0) {
                                    for (a = 0; a < Controles.length; a++) {
                                        if (Controles[a].type == 'checkbox') {
                                            if (Controles[a].checked) {
                                                sStr += 'Si' + '    '
                                            }
                                            else {
                                                sStr += 'No' + '    '
                                            }
                                        }
                                        else {
                                            sStr += Controles[a].value + '    ';
                                        }
                                    }
                                }
                                else {
                                    Controles = Celdas[x].getElementsByTagName('select');
                                    if (Controles.length != 0) {
                                        for (a = 0; a < Controles.length; a++) {
                                            sStr += Controles[a].value + '    ';
                                        }
                                    }
                                    else {
                                        sStr += Celdas[x].innerHTML + ' ';
                                    }
                                }
                            }
                            sStr += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else {
                            //alert(Celdas[x].innerHTML.indexOf(":"));
                            if (Celdas[x].innerHTML.indexOf(":") == -1) {
                                sStr += '<strong>' + Celdas[x].innerHTML + ': ' + '</strong>';
                            }
                            else {
                                sStr += '<strong>' + Celdas[x].innerHTML + ' </strong>';
                            }
                        }
                    }
                }
            }
            sStr += "<br />";
        }
    }
    else {
        //alert("Tabla nula");
        //alert(sStr);
    }
    return sStr;
}

function RecorrerFormContactenos() {
    var sStr = GetCampos();
    //alert("CONTACTENOS  " + sStr);
    var Resp = uc_ucContactenos.csContacto(sStr);
}

function RecorrerFormSuEstilo() {
    var sStr = GetCampos();
    //alert("SU ESTILO    " + sStr);
    var Resp = uc_ucPlanViajarASuEstilo.csContacto(sStr);
}


function clear_textbox(TextBoxId) {
    if (document.getElementById(TextBoxId) == "Ingrese su e-mail")
        document.getElementById(TextBoxId).value = "";
}
function redirecpagepupop(url) {
    //alert(url);
    //window.opener.location.href = url;
    //window.close(); 
    //window.top.location=url;
    //opener.location=url;
    window.open(url, "Buscador", "width=580,height=500,top=100,left=500")
}
function redirecpageterminos(url) {
    //  window.open("http://www.aviatur.travel/aviaturcms/buscadores/buscador/sabre/condiciones.html" , "Terminos" ,"width=460,height=580,scrollbars=YES,resizable=NO,Directories=NO") 
    //   window.open(url , "Terminos" ,"width=460,height=580,scrollbars=YES,resizable=NO,Directories=NO") 
    window.open("http://www.aviatur.travel/turismo/aviaturtravel/buscadores_sabre/condiciones_busqueda_sabre.php", "Terminos", "width=460,height=580,scrollbars=YES,resizable=NO,Directories=NO")
}
function onblur_tex(TextBoxId) {
    if (TextBoxId.value.length > 0) {
        var txt_Multi_O1E = document.getElementById("txt_Multi_O1E");
        var txt_Multi_D1E = document.getElementById("txt_Multi_D1E");
        var txtCiudadDestinoE = document.getElementById("txtCiudadDestinoE");
        var txtCarCiudRecogeE = document.getElementById("txtCarCiudRecogeE");
        var txtCarCiudEntregaE = document.getElementById("txtCarCiudEntregaE");

        var txt_Multi_O2E = document.getElementById("txt_Multi_O2E");
        var txt_Multi_D2E = document.getElementById("txt_Multi_D2E");
        var txt_Multi_O3E = document.getElementById("txt_Multi_O3E");
        var txt_Multi_D3E = document.getElementById("txt_Multi_D3E");
        var txt_Multi_O4E = document.getElementById("txt_Multi_O4E");
        var txt_Multi_D4E = document.getElementById("txt_Multi_D4E");
        try {
            txt_Multi_O1E.style.display = "none";
            txt_Multi_D1E.style.display = "none";
        }
        catch (err) { }
        try {
            txt_Multi_O2E.style.display = "none";
            txt_Multi_D2E.style.display = "none";
            txt_Multi_O3E.style.display = "none";
            txt_Multi_D3E.style.display = "none";
            txt_Multi_O4E.style.display = "none";
            txt_Multi_D4E.style.display = "none";
        }
        catch (err) { }
        try {
            txtCiudadDestinoE.style.display = "none";
        }
        catch (err) { }
        try {
            txtCarCiudRecogeE.style.display = "none";
            txtCarCiudEntregaE.style.display = "none";
        }
        catch (err) { }
        if (TextBoxId.value.length < 17) {
            TextBoxId.focus();
            var lblError = document.getElementById(TextBoxId.id + "E");
            lblError.style.display = "block";
        }
    }
}


function SetUniqueRadioButton(nameregex, rid) {
    //alert(rid);
    re = new RegExp(nameregex);
    rb = document.getElementById(rid);
    var inputs = document.getElementsByTagName('input');
    for (i = 0; i < inputs.length; i++) {
        elm = inputs[i]
        if (elm.type == 'radio') {
            if (re.test(elm.name)) {
                elm.checked = false;
            }
        }
    }

    rb.checked = true;
}


function ActivarDivFP(rbl) {
    //var value = rbl.options[sel.selectedIndex].value;
    //alert(rbl.value);
    var divTC = document.getElementById('DivTC');
    var divPSE = document.getElementById('DivPSE');
    var divEfe = document.getElementById('DivEfe');

//    divTC.style.visibility = "visible";
//    divPSE.style.visibility = "hidden";
//    divEfe.style.visibility = "hidden";
    if (rbl.value == "EFE")//finchBay
    {
        divTC.style.display = "none";
        divPSE.style.display = "none";
        divEfe.style.display = "block";
    }
    if (rbl.value == "TC")//finchBay
    {
        divTC.style.display = "block";
        divPSE.style.display = "none";
        divEfe.style.display = "none";
    }
    if (rbl.value == "PSE")//finchBay
    {
        divTC.style.display = "none";
        divPSE.style.display = "block";
        divEfe.style.display = "none";
    }
}
$(document).ready(function () {
    $("input[id *= 'txtCiudad']").keyup(function () {
        $(".autocompletarCiudad").remove();
        html_lista = "<div class='autocompletarCiudad'><ul>";
        for (i = 0; i < autocompletar.length; i++) {
            if (autocompletar[i].toLowerCase().indexOf($("input[id *= 'txtCiudad']").val().toLowerCase()) != -1) {
                html_lista += "<li><a href='javascript:poner_texto(\""+autocompletar[i]+"\")'>" + autocompletar[i] + "</a></li>";
            }
        }
        html_lista += "</ul></div>";
        $("input[id *= 'txtCiudad']").after(html_lista);
    });
    /*$("input[id *= 'txtCiudad']").focus(function () {
    html_lista = "<div class='autocompletarCiudad'><ul>";
    for (i = 0; i < autocompletar.length; i++) {
    html_lista += "<li>" + autocompletar[i] + "</li>";
    }
    html_lista += "</ul></div>";
    $("input[id *= 'txtCiudad']").after(html_lista);
    });*/
});
function poner_texto(texto) {
    $(".autocompletarCiudad").remove();
    $("input[id *= 'txtCiudad']").val(texto);
}

function menuMovil() {
    $("#nav").toggle();
}

function Show_Cortinilla_Validacion(tab) {
    try {
        EsconderBuscadores();
    } catch (err) {
    }
    try {
        GuardarSession();
    } catch (err) {
    }

    if (tab === 1) {
        var tipo_vuelo = $("input:radio[name*='modal_vuelos']:checked").val();
        if (tipo_vuelo === "1") {
            var txt_origen = $("input[id*=txt_Multi_O1]").val();
            var txt_destino = $("input[id*=txt_Multi_D1]").val();
            var txt_fecha_origen = $("input[id*=txtFechaMultiO1]").val();
            if (txt_origen === "") {
                $("input[id*=txt_Multi_O1]").val("Seleccione Origen");
                $("input[id*=txt_Multi_O1]").addClass("lblError");
            }
            if (txt_destino === "") {
                $("input[id*=txt_Multi_D1]").val("Seleccione Destino");
                $("input[id*=txt_Multi_D1]").addClass("lblError");
            }
            if (txt_fecha_origen === "") {
                $("input[id*=txtFechaMultiO1]").val("Elija Fecha");
                $("input[id*=txtFechaMultiO1]").addClass("lblError");
            }
            if (txt_origen === "" || txt_origen === "Seleccione Origen" || txt_destino === "" || txt_destino === "Seleccione Destino" || txt_fecha_origen === "" || txt_fecha_origen === "Elija Fecha") {
                return false;
            }
        }
        if (tipo_vuelo === "0") {
            var txt_origen = $("input[id*=txt_Multi_O1]").val();
            var txt_destino = $("input[id*=txt_Multi_D1]").val();
            var txt_fecha_origen = $("input[id*=txtFechaMultiO1]").val();
            var txt_fecha_destino = $("input[id*=txt2VFechaMulti]").val();
            if (txt_origen === "") {
                $("input[id*=txt_Multi_O1]").val("Seleccione Origen");
                $("input[id*=txt_Multi_O1]").addClass("lblError");
            }
            if (txt_destino === "") {
                $("input[id*=txt_Multi_D1]").val("Seleccione Destino");
                $("input[id*=txt_Multi_D1]").addClass("lblError");
            }
            if (txt_fecha_origen === "") {
                $("input[id*=txtFechaMultiO1]").val("Elija Fecha");
                $("input[id*=txtFechaMultiO1]").addClass("lblError");
            }
            if (txt_fecha_destino === "") {
                $("input[id*=txt2VFechaMulti]").val("Elija Fecha");
                $("input[id*=txt2VFechaMulti]").addClass("lblError");
            }
            if (txt_origen === "" || txt_origen === "Seleccione Origen" || txt_destino === "" || txt_destino === "Seleccione Destino" || txt_fecha_origen === "" || txt_fecha_origen === "Elija Fecha" || txt_fecha_destino === "" || txt_fecha_destino === "Elija Fecha") {
                return false;
            }
        }
        if (tipo_vuelo === "2") {
            var txt_origen = $("input[id*=txt_Multi_O1]").val();
            var txt_destino = $("input[id*=txt_Multi_D1]").val();
            var txt_fecha_origen = $("input[id*=txtFechaMultiO1]").val();
            var txt_origen2 = $("input[id*=txt_Multi_O2]").val();
            var txt_destino2 = $("input[id*=txt_Multi_D2]").val();
            var txt_fecha_origen2 = $("input[id*=txtFechaMultiO2]").val();
            if (txt_origen === "") {
                $("input[id*=txt_Multi_O1]").val("Seleccione Origen");
                $("input[id*=txt_Multi_O1]").addClass("lblError");
            }
            if (txt_destino === "") {
                $("input[id*=txt_Multi_D1]").val("Seleccione Destino");
                $("input[id*=txt_Multi_D1]").addClass("lblError");
            }
            if (txt_fecha_origen === "") {
                $("input[id*=txtFechaMultiO1]").val("Elija Fecha");
                $("input[id*=txtFechaMultiO1]").addClass("lblError");
            }
            if (txt_origen2 === "") {
                $("input[id*=txt_Multi_O2]").val("Seleccione Origen");
                $("input[id*=txt_Multi_O2]").addClass("lblError");
            }
            if (txt_destino2 === "") {
                $("input[id*=txt_Multi_D2]").val("Seleccione Destino");
                $("input[id*=txt_Multi_D2]").addClass("lblError");
            }
            if (txt_fecha_origen2 === "") {
                $("input[id*=txtFechaMultiO2]").val("Elija Fecha");
                $("input[id*=txtFechaMultiO2]").addClass("lblError");
            }
            if (txt_origen === "" || txt_origen === "Seleccione Origen" || txt_destino === "" || txt_destino === "Seleccione Destino" || txt_fecha_origen === "" || txt_fecha_origen === "Elija Fecha" || txt_origen2 === "" || txt_origen2 === "Seleccione Origen" || txt_destino2 === "" || txt_destino2 === "Seleccione Destino" || txt_fecha_origen2 === "" || txt_fecha_origen2 === "Elija Fecha") {
                return false;
            }
            else {
                for (i = 3; i < 7; i++) {
                    var txt_origen = $("input[id*=txt_Multi_O" + i + "]").val();
                    var txt_destino = $("input[id*=txt_Multi_D" + i + "]").val();
                    var txt_fecha_origen = $("input[id*=txtFechaMultiO" + i + "]").val();
                    if (txt_origen !== "" || txt_destino !== "" || txt_fecha_origen !== "") {
                        if (txt_origen === "") {
                            $("input[id*=txt_Multi_O" + i + "]").val("Seleccione Origen");
                            $("input[id*=txt_Multi_O" + i + "]").addClass("lblError");
                        }
                        if (txt_destino === "") {
                            $("input[id*=txt_Multi_D" + i + "]").val("Seleccione Destino");
                            $("input[id*=txt_Multi_D" + i + "]").addClass("lblError");
                        }
                        if (txt_fecha_origen === "") {
                            $("input[id*=txtFechaMultiO" + i + "]").val("Elija Fecha");
                            $("input[id*=txtFechaMultiO" + i + "]").addClass("lblError");
                        }
                        if (txt_origen === "" || txt_origen === "Seleccione Origen" || txt_destino === "" || txt_destino === "Seleccione Destino" || txt_fecha_origen === "" || txt_fecha_origen === "Elija Fecha") {
                            return false;
                        }
                    }
                }
            }
        }
    }
    if (tab === 2) {
        var txt_destino = $("input[id*=txtCiudadDestino]").val();
        var txt_fecha_ingreso = $("input[id*=txtFechaIngreso]").val();
        var txt_fecha_salida = $("input[id*=txt2HFechaSalida]").val();
        if (txt_destino === "") {
            $("input[id*=txtCiudadDestino]").val("Seleccione Destino");
            $("input[id*=txtCiudadDestino]").addClass("lblError");
        }
        if (txt_fecha_ingreso === "") {
            $("input[id*=txtFechaIngreso]").val("Elija Fecha");
            $("input[id*=txtFechaIngreso]").addClass("lblError");
        }
        if (txt_fecha_salida === "") {
            $("input[id*=txt2HFechaSalida]").val("Elija Fecha");
            $("input[id*=txt2HFechaSalida]").addClass("lblError");
        }
        if (txt_destino === "" || txt_destino === "Seleccione Destino" || txt_fecha_ingreso === "" || txt_fecha_ingreso === "Elija Fecha" || txt_fecha_salida === "" || txt_fecha_salida === "Elija Fecha") {
            return false;
        }
    }
    if (tab === 3) {
        var txt_origen = $("input[id*=txt_Multi_VueloHotel_O1]").val();
        var txt_destino = $("input[id*=txt_Multi_VueloHotel_D1]").val();
        var txt_fecha_ingreso = $("input[id*=txt1FechaVueloHotelIni]").val();
        var txt_fecha_salida = $("input[id*=txt2FechaVueloHotelFin]").val();
        var chk_estadia_parcial = $("input:checkbox[id*=chkestadiap]:checked").val();
        var txt_fecha_parcial_inicial = $("input[id*=txt1FechaHoteliniAva]").val();
        var txt_fecha_parcial_final = $("input[id*=txt2FechaHotelFinAva]").val();
        if (txt_origen === "") {
            $("input[id*=txt_Multi_VueloHotel_O1]").val("Seleccione Origen");
            $("input[id*=txt_Multi_VueloHotel_O1]").addClass("lblError");
        }
        if (txt_destino === "") {
            $("input[id*=txt_Multi_VueloHotel_D1]").val("Seleccione Destino");
            $("input[id*=txt_Multi_VueloHotel_D1]").addClass("lblError");
        }
        if (txt_fecha_ingreso === "") {
            $("input[id*=txt1FechaVueloHotelIni]").val("Elija Fecha");
            $("input[id*=txt1FechaVueloHotelIni]").addClass("lblError");
        }
        if (txt_fecha_salida === "") {
            $("input[id*=txt2FechaVueloHotelFin]").val("Elija Fecha");
            $("input[id*=txt2FechaVueloHotelFin]").addClass("lblError");
        }
        if (txt_origen === "" || txt_origen === "Seleccione Origen" || txt_destino === "" || txt_destino === "Seleccione Destino" || txt_fecha_ingreso === "" || txt_fecha_ingreso === "Elija Fecha" || txt_fecha_salida === "" || txt_fecha_salida === "Elija Fecha") {
            return false;
        }
        else {
            if (chk_estadia_parcial === "on") {
                if (txt_fecha_parcial_inicial === "") {
                    $("input[id*=txt1FechaHoteliniAva]").val("Elija Fecha");
                    $("input[id*=txt1FechaHoteliniAva]").addClass("lblError");
                }
                if (txt_fecha_parcial_final === "") {
                    $("input[id*=txt2FechaHotelFinAva]").val("Elija Fecha");
                    $("input[id*=txt2FechaHotelFinAva]").addClass("lblError");
                }
                if (txt_fecha_parcial_inicial === "" || txt_fecha_parcial_inicial === "Elija Fecha" || txt_fecha_parcial_final === "" || txt_fecha_parcial_final === "Elija Fecha") {
                    return false;
                }
            }
        }
    }

    if (navigator.appName == "Microsoft Internet Explorer") {
        var buttons = $("#div_CortinillaFlashInterna").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }
    else {
        var buttons = $("#div_Cortinilla_Interna").dialog({
            resizable: false,
            modal: true,
            height: 290,
            width: 480,
            closeOnEscape: false

        });
    }

}

$(document).ready(function () {
    $("input[id*=txt]").focus(function () {
        if ($(this).hasClass("lblError")) {
            $(this).val("");
            $(this).removeClass("lblError")
            $("#tipoTarjeta").html("");
        }
    });
    $("input[id*='txtNumTarjeta']").validarTarjetaCredito();


});

function ValidaSoloNumeros() {
    if ((event.keyCode < 48) || (event.keyCode > 57))
        event.returnValue = false;
}

function popUp(valor) {
    
    i = 0;
    salir = true;
    validacion = true;
    estadoValidacion = 1;
    if (valor == 1) {
        while (salir) {
            var nombre_pasajero = $("input[id*='ctl0" + i + "_txtNombre1']").val();
            if (nombre_pasajero == undefined) {
                salir = false;
            }
            else {
                var apellido_pasajero = $("input[id*='ctl0" + i + "_txtApellido1']").val();
                var txtEdad = $("input[id*='ctl0" + i + "_txtEdad1']").val();
                var txtDocumento = $("input[id*='ctl0" + i + "_txtDocumento1']").val();
                var numTarjeta = $("input[id*='txtNumTarjeta']").val();
                var txtBanco = $("input[id*='txtBanco']").val();
                var txtAnioVencimiento = $("input[id*='txtAnioVencimiento']").val();
                var txtCodSeguridad = $("input[id*='txtCodSeguridad']").val();
                var txtNumCuotas = $("input[id*='txtCuotas']").val();
                var txtTitular = $("input[id*='txtTitular']").val();
                var txtIdentificacion = $("input[id*='txtIdentificacion']").val();
                var txtMailPersonal = $("input[id*='txtMailPersonal']").val();
                //var txtTelefono = $("input[id*='txtTelefono']").val();
                var txtCiudad = $("input[id*='txtCiudad']").val();
                var txtCelular = $("input[id*='txtCelular']").val();
                //var txtMailPersonalConfirmar = $("input[id*='txtMailPersonalConfirmar']").val();
                var txtDireccion = $("input[id*='ucReservaVuelos_txtDireccion']").val();
                var txtPais = $("input[id*='txtPais']").val();
                var txtTelefonoOficina = $("input[id*='txtTelefonoOficina']").val();
                var txtTelefonoOtro = $("input[id*='txtTelefonoOtro']").val();
                var rdbFormasPago = $("input:radio[name*='rblFormasPago']:checked").val();
                var chkAcepto = $("input:checkbox[name*='cbAcepto']:checked").val();

                if (nombre_pasajero === "" || nombre_pasajero === "Nombres") {
                    $("input[id*='ctl0" + i + "_txtNombre1']").val("Nombres");
                    $("input[id*='ctl0" + i + "_txtNombre1']").addClass("lblError");
                    validacion = false;
                    //alert('nombre');
                }
                if (apellido_pasajero === "" || apellido_pasajero === "Apellidos") {
                    $("input[id*='ctl0" + i + "_txtApellido1']").val("Apellidos");
                    $("input[id*='ctl0" + i + "_txtApellido1']").addClass("lblError");
                    validacion = false;
                    //alert('apellido');
                }
                if (txtEdad === "" || txtEdad === "Fecha de nacimiento") {
                    $("input[id*='ctl0" + i + "_txtEdad1']").val("Fecha de nacimiento");
                    $("input[id*='ctl0" + i + "_txtEdad1']").addClass("lblError");
                    validacion = false;
                    //alert('fecha nac');
                }
                if (txtDocumento === "" || txtDocumento === "N Documento") {
                    $("input[id*='ctl0" + i + "_txtDocumento1']").val("N Documento");
                    $("input[id*='ctl0" + i + "_txtDocumento1']").addClass("lblError");
                    validacion = false;
                    //alert('documento');
                }
                if (txtMailPersonal === "" || txtMailPersonal === "Correo Electronico") {
                    $("input[id*='txtMailPersonal']").val("Correo Electronico");
                    $("input[id*='txtMailPersonal']").addClass("lblError");
                    validacion = false;
                    //alert('mail');
                }
                //                if (txtTelefono === "" || txtTelefono === "Telefono") {
                //                    $("input[id*='txtTelefono']").val("Telefono");
                //                    $("input[id*='txtTelefono']").addClass("lblError");
                //                    validacion = false;
                //                }
                if (txtCiudad === "" || txtCiudad === "Ciudad") {
                    $("input[id*='txtCiudad']").val("Ciudad");
                    $("input[id*='txtCiudad']").addClass("lblError");
                    validacion = false;
                    //alert('ciudad');
                }
                if (txtCelular === "" || txtCelular === "Celular") {
                    $("input[id*='txtCelular']").val("Celular");
                    $("input[id*='txtCelular']").addClass("lblError");
                    validacion = false;
                    //alert('celular');
                }
                if (chkAcepto === undefined || rdbFormasPago === undefined) {
                    validacion = false;
                }
                if (rdbFormasPago === "TC") {
                    if ($("#tipoTarjeta").html() != "Valida") {
                        validacion = false;
                    }
                    if (numTarjeta === "" || numTarjeta === "Numero de la tarjeta") {
                        $("input[id*='txtNumTarjeta']").val("Numero de la tarjeta");
                        $("input[id*='txtNumTarjeta']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtBanco === "" || txtBanco === "Banco") {
                        $("input[id*='txtBanco']").val("Banco");
                        $("input[id*='txtBanco']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtAnioVencimiento === "" || txtAnioVencimiento === "Año vencimiento") {
                        $("input[id*='txtAnioVencimiento']").val("Año vencimiento");
                        $("input[id*='txtAnioVencimiento']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtCodSeguridad === "" || txtCodSeguridad === "Codigo de seguridad") {
                        $("input[id*='txtCodSeguridad']").val("Codigo de seguridad");
                        $("input[id*='txtCodSeguridad']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtNumCuotas === "" || txtNumCuotas === "Numero de cuotas") {
                        $("input[id*='txtCuotas']").val("Numero de cuotas");
                        $("input[id*='txtCuotas']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtTitular === "" || txtTitular === "Titular") {
                        $("input[id*='txtTitular']").val("Titular");
                        $("input[id*='txtTitular']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtIdentificacion === "" || txtIdentificacion === "Identificacion") {
                        $("input[id*='txtIdentificacion']").val("Identificacion");
                        $("input[id*='txtIdentificacion']").addClass("lblError");
                        validacion = false;
                    }
                    /*if (txtMailPersonalConfirmar === "" || txtMailPersonalConfirmar === "Confirmacion Correo Electronico") {
                    $("input[id*='txtMailPersonalConfirmar']").val("Confirmacion Correo Electronico");
                    $("input[id*='txtMailPersonalConfirmar']").addClass("lblError");
                    validacion = false;
                    }*/
                    if (txtDireccion === "" || txtDireccion === "Direccion") {
                        $("input[id*='ucReservaVuelos_txtDireccion']").val("Direccion");
                        $("input[id*='ucReservaVuelos_txtDireccion']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtPais === "" || txtPais === "Pais") {
                        $("input[id*='txtPais']").val("Pais");
                        $("input[id*='txtPais']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtTelefonoOficina === "" || txtTelefonoOficina === "Telefono horas laborales") {
                        $("input[id*='txtTelefonoOficina']").val("Telefono horas laborales");
                        $("input[id*='txtTelefonoOficina']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtTelefonoOtro === "" || txtTelefonoOtro === "Otro telefono") {
                        $("input[id*='txtTelefonoOtro']").val("Otro telefono");
                        $("input[id*='txtTelefonoOtro']").addClass("lblError");
                        validacion = false;
                    }
                    /*if (chkAcepto === false) {
                    validacion = false;
                    }*/
                }

                if (rdbFormasPago === "TCPOL") {
                    if ($("#tipoTarjeta").html() != "TARJETA VALIDA") {
                        validacion = false;
                    }

                    numTarjeta = $("input[id*='txtNumTarjetaPOL']").val();
                    txtCodSeguridad = $("input[id*='txtCodSeguridadPOL']").val();
                    txtTitular = $("input[id*='txtTitularPOL']").val();
                    txtIdentificacion = $("input[id*='txtIdentificacionPOL']").val();
                    txtTelefonoOficina = $("input[id*='txtTelefonoOficinaPOL']").val();
                    txtTelefonoOtro = $("input[id*='txtTelefonoOtroPOL']").val();
                    txtCuotas = $("input[id*='txtCuotasPOL']").val();

                    if (numTarjeta === "" || numTarjeta === "Numero de la tarjeta") {
                        $("input[id*='txtNumTarjeta']").val("Numero de la tarjeta");
                        $("input[id*='txtNumTarjeta']").addClass("lblError");
                        validacion = false;
                        //alert('num tarjeta');
                    }

                    if (txtAnioVencimiento === "" || txtAnioVencimiento === "Año vencimiento") {
                        $("input[id*='txtAnioVencimiento']").val("Año vencimiento");
                        $("input[id*='txtAnioVencimiento']").addClass("lblError");
                        validacion = false;
                        alert('ano vencimiento');
                    }
                    if (txtCodSeguridad === "" || txtCodSeguridad === "Codigo de seguridad") {
                        $("input[id*='txtCodSeguridad']").val("Codigo de seguridad");
                        $("input[id*='txtCodSeguridad']").addClass("lblError");
                        validacion = false;
                        //alert('con segu');
                    }

                    if (txtTitular === "" || txtTitular === "Titular") {
                        $("input[id*='txtTitular']").val("Titular");
                        $("input[id*='txtTitular']").addClass("lblError");
                        validacion = false;
                        //alert('Titular');
                    }
                    if (txtIdentificacion === "" || txtIdentificacion === "Identificacion") {
                        $("input[id*='txtIdentificacion']").val("Identificacion");
                        $("input[id*='txtIdentificacion']").addClass("lblError");
                        validacion = false;
                        //alert('id titular');
                    }

                    if (txtTelefonoOficina === "" || txtTelefonoOficina === "Telefono horas laborales") {
                        $("input[id*='txtTelefonoOficina']").val("Telefono horas laborales");
                        $("input[id*='txtTelefonoOficina']").addClass("lblError");
                        validacion = false;
                        //alert('tel oficina');
                    }
                    if (txtTelefonoOtro === "" || txtTelefonoOtro === "Otro telefono") {
                        $("input[id*='txtTelefonoOtro']").val("Otro telefono");
                        $("input[id*='txtTelefonoOtro']").addClass("lblError");
                        validacion = false;
                        //alert('tel otro');
                    }

                    if (txtCuotas === "" || txtCuotas === "Cuotas") {
                        $("input[id*='txtCuotasPOL']").val("Cuotas");
                        $("input[id*='txtCuotasPOL']").addClass("lblError");
                        validacion = false;
                        //alert('tel otro');
                    }
                    /*if (chkAcepto === false) {
                    validacion = false;
                    }*/
                }
            }
            i++;
        }
        if (validacion == false) {
            return false;
        }
        $find('MPEEReserva').show();
    }
    if (valor == 2) {
        validacion = true;
        var nombre_pasajero = $("input[id*='txtNombre1']").val();
        var apellido_pasajero = $("input[id*='txtApellido1']").val();
        var txtEdad = $("input[id*='txtEdad1']").val();
        var txtDocumento = $("input[id*='txtDocumento1']").val();
        var numTarjeta = $("input[id*='txtNumTarjeta']").val();
        var txtBanco = $("input[id*='txtBanco']").val();
        var txtAnioVencimiento = $("input[id*='txtAnioVencimiento']").val();
        var txtCodSeguridad = $("input[id*='txtCodSeguridad']").val();
        var txtNumCuotas = $("input[id*='txtCuotas']").val();
        var txtTitular = $("input[id*='txtTitular']").val();
        var txtIdentificacion = $("input[id*='txtIdentificacion']").val();
        //var txtMailPersonal = $("input[id*='txtMailPersonal']").val();
        //var txtMailPersonalConfirmar = $("input[id*='txtMailPersonalConfirmar']").val();
        var txtDireccion = $("input[id*='ucReservaHotel_txtDireccion']").val();
        var txtPais = $("input[id*='txtPais']").val();
        var txtTelefonoOficina = $("input[id*='txtTelefonoOficina']").val();
        var txtTelefonoOtro = $("input[id*='txtTelefonoOtro']").val();
        var rdbFormasPago = $("input:radio[name*='rblFormasPago']:checked").val();
        var chkAcepto = $("input:checkbox[name*='cbAceptar']:checked").val();

        if (nombre_pasajero === "" || nombre_pasajero === "Seleccione el usuario") {
            $("input[id*='txtNombre1']").val("Seleccione el usuario");
            $("input[id*='txtNombre1']").addClass("lblError");
            validacion = false;
        }
        if (apellido_pasajero === "" || apellido_pasajero === "Seleccione el usuario") {
            $("input[id*='txtApellido1']").val("Seleccione el usuario");
            $("input[id*='txtApellido1']").addClass("lblError");
            validacion = false;
        }
        if (txtEdad === "" || txtEdad === "Seleccione el usuario") {
            $("input[id*='txtEdad1']").val("Seleccione el usuario");
            $("input[id*='txtEdad1']").addClass("lblError");
            validacion = false;
        }
        if (txtDocumento === "" || txtDocumento === "Seleccione el usuario") {
            $("input[id*='txtDocumento1']").val("Seleccione el usuario");
            $("input[id*='txtDocumento1']").addClass("lblError");
            validacion = false;
        }
        if (chkAcepto === undefined || rdbFormasPago === undefined) {
            validacion = false;
        }
        if (rdbFormasPago === "TC") {
            if ($("#tipoTarjeta").html() !== "Valida") {
                validacion = false;
            }
            if (numTarjeta === "" || numTarjeta === "Numero de la tarjeta") {
                $("input[id*='txtNumTarjeta']").val("Numero de la tarjeta");
                $("input[id*='txtNumTarjeta']").addClass("lblError");
                validacion = false;
            }
            if (txtBanco === "" || txtBanco === "Banco") {
                $("input[id*='txtBanco']").val("Banco");
                $("input[id*='txtBanco']").addClass("lblError");
                validacion = false;
            }
            if (txtAnioVencimiento === "" || txtAnioVencimiento === "Año vencimiento") {
                $("input[id*='txtAnioVencimiento']").val("Año vencimiento");
                $("input[id*='txtAnioVencimiento']").addClass("lblError");
                validacion = false;
            }
            if (txtCodSeguridad === "" || txtCodSeguridad === "Codigo de seguridad") {
                $("input[id*='txtCodSeguridad']").val("Codigo de seguridad");
                $("input[id*='txtCodSeguridad']").addClass("lblError");
                validacion = false;
            }
            if (txtNumCuotas === "" || txtNumCuotas === "Numero de cuotas") {
                $("input[id*='txtCuotas']").val("Numero de cuotas");
                $("input[id*='txtCuotas']").addClass("lblError");
                validacion = false;
            }
            if (txtTitular === "" || txtTitular === "Titular") {
                $("input[id*='txtTitular']").val("Titular");
                $("input[id*='txtTitular']").addClass("lblError");
                validacion = false;
            }
            if (txtIdentificacion === "" || txtIdentificacion === "Identificacion") {
                $("input[id*='txtIdentificacion']").val("Identificacion");
                $("input[id*='txtIdentificacion']").addClass("lblError");
                validacion = false;
            }
            /*if (txtMailPersonal === "" || txtMailPersonal === "Correo Electronico") {
            $("input[id*='txtMailPersonal']").val("Correo Electronico");
            $("input[id*='txtMailPersonal']").addClass("lblError");
            validacion = false;
            }
            if (txtMailPersonalConfirmar === "" || txtMailPersonalConfirmar === "Confirmacion Correo Electronico") {
            $("input[id*='txtMailPersonalConfirmar']").val("Confirmacion Correo Electronico");
            $("input[id*='txtMailPersonalConfirmar']").addClass("lblError");
            validacion = false;
            }*/
            if (txtDireccion === "" || txtDireccion === "Direccion") {
                $("input[id*='ucReservaHotel_txtDireccion']").val("Direccion");
                $("input[id*='ucReservaHotel_txtDireccion']").addClass("lblError");
                validacion = false;
            }
            if (txtPais === "" || txtPais === "Pais") {
                $("input[id*='txtPais']").val("Pais");
                $("input[id*='txtPais']").addClass("lblError");
                validacion = false;
            }
            if (txtTelefonoOficina === "" || txtTelefonoOficina === "Telefono horas laborales") {
                $("input[id*='txtTelefonoOficina']").val("Telefono horas laborales");
                $("input[id*='txtTelefonoOficina']").addClass("lblError");
                validacion = false;
            }
            if (txtTelefonoOtro === "" || txtTelefonoOtro === "Otro telefono") {
                $("input[id*='txtTelefonoOtro']").val("Otro telefono");
                $("input[id*='txtTelefonoOtro']").addClass("lblError");
                validacion = false;
            }
            /*if (chkAcepto === false) {
            validacion = false;
            }*/
        }
        if (validacion == false) {
            return false;
        }
        if (navigator.appName == "Microsoft Internet Explorer") {
            var buttons = $("#div_CortinillaFlashInterna").dialog({
                resizable: false,
                modal: true,
                height: 290,
                width: 480,
                closeOnEscape: false

            });
        }
        else {
            var buttons = $("#div_Cortinilla_Interna").dialog({
                resizable: false,
                modal: true,
                height: 290,
                width: 480,
                closeOnEscape: false

            });
        }
    }
    if (valor == 3) {
        while (salir) {
            var nombre_pasajero = $("input[id*='ctl0" + i + "_txtNombre']").val();
            if (nombre_pasajero == undefined) {
                salir = false;
            }
            else {
                var apellido_pasajero = $("input[id*='ctl0" + i + "_txtApellido']").val();
                var txtEdad = $("input[id*='ctl0" + i + "_txtNacimiento']").val();
                var txtDocumento = $("input[id*='ctl0" + i + "_txtPasaporte']").val();
                var numTarjeta = $("input[id*='txtNumTarjeta']").val();
                var txtBanco = $("input[id*='txtBanco']").val();
                var txtAnioVencimiento = $("input[id*='txtAnioVencimiento']").val();
                var txtCodSeguridad = $("input[id*='txtCodSeguridad']").val();
                var txtNumCuotas = $("input[id*='txtCuotas']").val();
                var txtTitular = $("input[id*='txtTitular']").val();
                var txtIdentificacion = $("input[id*='txtIdentificacion']").val();
                //var txtMailPersonal = $("input[id*='txtMailPersonal']").val();
                //var txtMailPersonalConfirmar = $("input[id*='txtMailPersonalConfirmar']").val();
                var txtDireccion = $("input[id*='ucReservaVuelos_txtDireccion']").val();
                var txtPais = $("input[id*='txtPais']").val();
                var txtTelefonoOficina = $("input[id*='txtTelefonoOficina']").val();
                var txtTelefonoOtro = $("input[id*='txtTelefonoOtro']").val();
                var rdbFormasPago = $("input:radio[name*='rblFormasPago']:checked").val();
                var chkAcepto = $("input:checkbox[name*='cbAcepto']:checked").val();

                if (nombre_pasajero === "" || nombre_pasajero === "Seleccione el usuario") {
                    $("input[id*='ctl0" + i + "_txtNombre']").val("Seleccione el usuario");
                    $("input[id*='ctl0" + i + "_txtNombre']").addClass("lblError");
                    validacion = false;
                }
                if (apellido_pasajero === "" || apellido_pasajero === "Seleccione el usuario") {
                    $("input[id*='ctl0" + i + "_txtApellido']").val("Seleccione el usuario");
                    $("input[id*='ctl0" + i + "_txtApellido']").addClass("lblError");
                    validacion = false;
                }
                if (txtEdad === "" || txtEdad === "Seleccione el usuario") {
                    $("input[id*='ctl0" + i + "_txtNacimiento']").val("Seleccione el usuario");
                    $("input[id*='ctl0" + i + "_txtNacimiento']").addClass("lblError");
                    validacion = false;
                }
                if (txtDocumento === "" || txtDocumento === "Seleccione el usuario") {
                    $("input[id*='ctl0" + i + "_txtPasaporte']").val("Seleccione el usuario");
                    $("input[id*='ctl0" + i + "_txtPasaporte']").addClass("lblError");
                    validacion = false;
                }
                if (chkAcepto === undefined || rdbFormasPago === undefined) {
                    validacion = false;
                }
                if (rdbFormasPago === "TC") {
                    if ($("#tipoTarjeta").html() !== "Valida") {
                        validacion = false;
                    }
                    if (numTarjeta === "" || numTarjeta === "Numero de la tarjeta") {
                        $("input[id*='txtNumTarjeta']").val("Numero de la tarjeta");
                        $("input[id*='txtNumTarjeta']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtBanco === "" || txtBanco === "Banco") {
                        $("input[id*='txtBanco']").val("Banco");
                        $("input[id*='txtBanco']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtAnioVencimiento === "" || txtAnioVencimiento === "Año vencimiento") {
                        $("input[id*='txtAnioVencimiento']").val("Año vencimiento");
                        $("input[id*='txtAnioVencimiento']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtCodSeguridad === "" || txtCodSeguridad === "Codigo de seguridad") {
                        $("input[id*='txtCodSeguridad']").val("Codigo de seguridad");
                        $("input[id*='txtCodSeguridad']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtNumCuotas === "" || txtNumCuotas === "Numero de cuotas") {
                        $("input[id*='txtCuotas']").val("Numero de cuotas");
                        $("input[id*='txtCuotas']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtTitular === "" || txtTitular === "Titular") {
                        $("input[id*='txtTitular']").val("Titular");
                        $("input[id*='txtTitular']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtIdentificacion === "" || txtIdentificacion === "Identificacion") {
                        $("input[id*='txtIdentificacion']").val("Identificacion");
                        $("input[id*='txtIdentificacion']").addClass("lblError");
                        validacion = false;
                    }
                    /*if (txtMailPersonal === "" || txtMailPersonal === "Correo Electronico") {
                    $("input[id*='txtMailPersonal']").val("Correo Electronico");
                    $("input[id*='txtMailPersonal']").addClass("lblError");
                    validacion = false;
                    }
                    if (txtMailPersonalConfirmar === "" || txtMailPersonalConfirmar === "Confirmacion Correo Electronico") {
                    $("input[id*='txtMailPersonalConfirmar']").val("Confirmacion Correo Electronico");
                    $("input[id*='txtMailPersonalConfirmar']").addClass("lblError");
                    validacion = false;
                    }*/
                    if (txtDireccion === "" || txtDireccion === "Direccion") {
                        $("input[id*='ucReservaVuelos_txtDireccion']").val("Direccion");
                        $("input[id*='ucReservaVuelos_txtDireccion']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtPais === "" || txtPais === "Pais") {
                        $("input[id*='txtPais']").val("Pais");
                        $("input[id*='txtPais']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtTelefonoOficina === "" || txtTelefonoOficina === "Telefono horas laborales") {
                        $("input[id*='txtTelefonoOficina']").val("Telefono horas laborales");
                        $("input[id*='txtTelefonoOficina']").addClass("lblError");
                        validacion = false;
                    }
                    if (txtTelefonoOtro === "" || txtTelefonoOtro === "Otro telefono") {
                        $("input[id*='txtTelefonoOtro']").val("Otro telefono");
                        $("input[id*='txtTelefonoOtro']").addClass("lblError");
                        validacion = false;
                    }
                    /*if (chkAcepto === false) {
                    validacion = false;
                    }*/
                }
            }
            i++;
        }
        if (validacion == false) {
            return false;
        }
        //$find('MPEEReserva').show();
    }
    if (valor === 4) {
        while (salir) {
            var nombre_pasajero = $("input[id*='ctl0" + i + "_txtNombre']").val();
            if (nombre_pasajero == undefined) {
                salir = false;
            }
            else {
                var apellido_pasajero = $("input[id*='ctl0" + i + "_txtApellido']").val();
                var numTarjeta = $("input[id*='txtNumTarjeta']").val();
                var txtBanco = $("input[id*='txtBanco']").val();
                //var txtAnioVencimiento = $("input[id*='txtAnioVencimiento']").val();
                var txtCodSeguridad = $("input[id*='txtCodSeguridad']").val();
                var txtTitular = $("input[id*='txtTitular']").val();
                var txtIdentificacion = $("input[id*='txtIdentificacion']").val();
                var txtMailPersonal = $("input[id*='txtMailPersonal']").val();
                var txtMailPersonalConfirmar = $("input[id*='txtMailPersonalConfirmar']").val();
                var txtTelefono = $("input[id*='txtTelefono']").val();
                var chkAcepto = $("input:checkbox[id*='cbAcepto']:checked").val();

                if (nombre_pasajero === "" || nombre_pasajero === "Digite el nombre") {
                    $("input[id*='ctl0" + i + "_txtNombre']").val("Digite el nombre");
                    $("input[id*='ctl0" + i + "_txtNombre']").addClass("lblError");
                    validacion = false;
                }
                if (apellido_pasajero === "" || apellido_pasajero === "Digite el apellido") {
                    $("input[id*='ctl0" + i + "_txtApellido']").val("Digite el apellido");
                    $("input[id*='ctl0" + i + "_txtApellido']").addClass("lblError");
                    validacion = false;
                }
                if (numTarjeta === "" || numTarjeta === "Numero de la tarjeta") {
                    $("input[id*='txtNumTarjeta']").val("Numero de la tarjeta");
                    $("input[id*='txtNumTarjeta']").addClass("lblError");
                    validacion = false;
                }
                if (txtBanco === "" || txtBanco === "Banco") {
                    $("input[id*='txtBanco']").val("Banco");
                    $("input[id*='txtBanco']").addClass("lblError");
                    validacion = false;
                }
                /*if (txtAnioVencimiento === "" || txtAnioVencimiento === "Año") {
                $("input[id*='txtAnioVencimiento']").val("Año");
                $("input[id*='txtAnioVencimiento']").addClass("lblError");
                validacion = false;
                }*/
                if (txtCodSeguridad === "" || txtCodSeguridad == "Codigo de seguridad") {
                    $("input[id*='txtCodSeguridad']").val("Codigo de seguridad");
                    $("input[id*='txtCodSeguridad']").addClass("lblError");
                    validacion = false;
                }
                if (txtTitular === "" || txtTitular === "Titular") {
                    $("input[id*='txtTitular']").val("Titular");
                    $("input[id*='txtTitular']").addClass("lblError");
                    validacion = false;
                }
                if (txtIdentificacion === "" || txtIdentificacion === "Identificacion") {
                    $("input[id*='txtIdentificacion']").val("Identificacion");
                    $("input[id*='txtIdentificacion']").addClass("lblError");
                    validacion = false;
                }
                if (txtMailPersonal === "" || txtMailPersonal === "Correo Electronico") {
                    $("input[id*='txtMailPersonal']").val("Correo Electronico");
                    $("input[id*='txtMailPersonal']").addClass("lblError");
                    validacion = false;
                }
                if (txtMailPersonalConfirmar === "" || txtMailPersonalConfirmar === "Confirmacion Correo Electronico") {
                    $("input[id*='txtMailPersonalConfirmar']").val("Confirmacion Correo Electronico");
                    $("input[id*='txtMailPersonalConfirmar']").addClass("lblError");
                    validacion = false;
                }
                if (txtTelefono === "") {
                    $("input[id*='txtTelefono']").val("Telefono");
                    $("input[id*='txtTelefono']").addClass("lblError");
                    validacion = false;
                }
                if (chkAcepto === false) {
                    validacion = false;
                }
            }
            i++;
        }
        if (validacion === false) {
            return false;
        }
        $find('MPEEReserva').show();
    }
}
