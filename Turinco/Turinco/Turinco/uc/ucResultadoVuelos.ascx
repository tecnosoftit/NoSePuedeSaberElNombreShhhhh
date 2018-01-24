<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucResultadoVuelos.ascx.cs" Inherits="uc_ucResultadoVuelos" %>
<%@ Register Src="ucBuscadorAereo.ascx" TagName="ucBuscadorAereo" TagPrefix="uc1" %>
<%@ Register Src="ucFiltrosdeVuelos.ascx" TagName="ucFiltrosdeVuelos" TagPrefix="uc2" %>
<%@ Register Src="ucIngresoTTQ.ascx" TagName="ucIngresoTTQ" TagPrefix="uc3" %>

<div class="encabezadoResultadoVuelos blanco arial">
    <asp:Label ID="lblTResultados" runat="server" Text="Resumen búsqueda en vuelos"></asp:Label>
</div>

<div class="noneDisplay">
    <asp:Label ID="lblOrigen" runat="server" Text=""></asp:Label>, &nbsp;A&nbsp;
    <asp:Label ID="lblDestino" runat="server" Text=""></asp:Label>
    <span class="fechaTrayecto">
        <asp:Label ID="lblFechaSalida" runat="server" Text=""></asp:Label>
        -
        <asp:Label ID="lblFechaLlegada" runat="server" Text=""></asp:Label>
    </span>
    <asp:Label ID="lblTime" runat="server" Text=""></asp:Label>
</div>

<div class="resultados resultadosBorder">
    <div class="resumenVuelos">
        <div id="content-slider">
        </div>
        <div class="tituloFiltros">
            <div class="aerolinea">
                <asp:Label ID="Label4" runat="server" Text="Aerolínea"></asp:Label>
            </div>
            <div class="directo">
                <asp:Label ID="Label10" runat="server" Text="Sin Paradas"></asp:Label>
            </div>
            <div class="parada1">
                <asp:Label ID="Label14" runat="server" Text="1 Parada"></asp:Label>
            </div>
            <div class="parada2">
                <asp:Label ID="Label17" runat="server" Text="2+ Paradas"></asp:Label>
            </div>
        </div>
        <div id="content-scroll">
            <div id="content-holder" class="contenidoFiltros">
                <asp:Repeater runat="server" ID="dtFiltroAir" Visible="false">
                    <ItemTemplate>
                        <div class="columnaAerolinea">
                            <div class="aerolinea">
                                <asp:ImageButton ID="imgSeleccionar" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "strMarketingAirline")%>'
                                    OnCommand="FilteHeaderAir_Command" ToolTip='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>'
                                    runat="server"></asp:ImageButton>
                            </div>
                            <div class="directo">
                                <asp:Label ID="lblParadas" runat="server"><%#DataBinder.Eval(Container.DataItem, "intPrecioDesde")%></asp:Label>
                            </div>
                            <div class="parada1">
                                <asp:Label ID="lblParada1" runat="server"><%#DataBinder.Eval(Container.DataItem, "StopQuantity_1")%></asp:Label>
                            </div>
                            <div class="parada2">
                                <asp:Label ID="lblParada2" runat="server"><%#DataBinder.Eval(Container.DataItem, "StopQuantity_2")%></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater runat="server" ID="dsFilter" Visible="true" OnItemDataBound="dtlFilter_ItemDataBound">
                    <ItemTemplate>
                        <div class="columnaAerolinea">
                            <div class="aerolinea">
                                <asp:ImageButton ID="imgSeleccionar" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "strMarketingAirline")%>'
                                    OnCommand="FilteHeaderAir_Command" ToolTip='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>'
                                    runat="server"></asp:ImageButton>
                            </div>
                            <div class="directo">
                                <asp:Label ID="lblParadas" runat="server"><%#DataBinder.Eval(Container.DataItem, "intPrecioDesde")%></asp:Label>
                            </div>
                            <div class="parada1">
                                <asp:Label ID="lblParada1" runat="server"><%#DataBinder.Eval(Container.DataItem, "intPrecioDesde")%></asp:Label>
                            </div>
                            <div class="parada2">
                                <asp:Label ID="lblParada2" runat="server"><%#DataBinder.Eval(Container.DataItem, "intPrecioDesde")%></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
            <div class="alineacionCentro" runat="server" id="Div1">
                <ajax:UpdateProgress ID="udpEperar" runat="server">
                    <ProgressTemplate>
                        <div class="progressbar">
                        </div>
                        <img alt="" src="../App_Themes/Imagenes/loading.gif" /><br />
                        <asp:Label ID="lblEsperar" runat="server" Text="Espere por favor..."></asp:Label>
                    </ProgressTemplate>
                </ajax:UpdateProgress>
            </div>
        </div>
    </div>
</div>

<div class="tituloResultadoVuelos blanco arial">
    <asp:Label ID="lblTResultadosVuelos" runat="server" Text="Resultado vuelos"></asp:Label>
</div>


<div class="resultados">
    <div class="ordenarVuelos">
        <asp:Label ID="lblOrder" runat="server" Text="Ordenar"></asp:Label>
        <asp:DropDownList ID="lstOrder" runat="server" OnSelectedIndexChanged="lstOrder_SelectedIndexChanged"
            AutoPostBack="true">
            <asp:ListItem Text="Seleccione uno" Value="SequenceNumber ASC"></asp:ListItem>
            <asp:ListItem Text="Precio:menor a mayor" Value="IntTotalPesos ASC"></asp:ListItem>
            <asp:ListItem Text="Precio:mayor a menor" Value="IntTotalPesos DESC"></asp:ListItem>
        </asp:DropDownList>
    </div>

    <asp:Repeater ID="rptItinerario" runat="server" OnItemCommand="rptItinerario_ItemCommand">
        <ItemTemplate>
            <div class="vuelos">      
                <div class="detalleVuelo">
                    <asp:DataList Width="100%" runat="server" ID="dtlSegmentos" CellPadding="0" CellSpacing="0" BorderWidth="0">
                        <ItemTemplate>
                            <table width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos">
                                <tr>
                                    <td class="logoAerolinea">
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                                    </td>
                                    <td>
                                        <asp:Label CssClass="bold" ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label30" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label CssClass="bold" ID="Label27" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label35" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                    </td>
                                    <td class="alineacionCentro">
                                        <asp:Label CssClass="bold" ID="Label2" runat="server" Text="Paradas"></asp:Label><br />
                                        <asp:Label CssClass='<%# DataBinder.Eval(Container.DataItem, "strEstiloParada")%>'
                                            ID="Label36" ToolTip='<%# DataBinder.Eval(Container.DataItem, "strDescripcionParadas")%>'
                                            runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strParadas")%>'></asp:Label>
                                    </td>
                                    <td class="alineacionCentro">
                                        <asp:Label CssClass="bold" ID="Label5" runat="server" Text="Tiempo de vuelo"></asp:Label><br />
                                        <asp:Label ID="Label37" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ElapsedTime")%>'></asp:Label>
                                    </td>
                                </tr>
                                <tr class="datosAerolinea">
                                    <td>
                                    </td>
                                    <td colspan="4">
                                        Aerolinea:
                                        <asp:Label CssClass="bold" ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>'></asp:Label>
                                        &nbsp;|&nbsp; Vuelo:
                                        <asp:Label CssClass="bold" ID="Label78" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlightNumber")%>'></asp:Label>
                                        <br />
                                        Hora de salida:
                                        <asp:Label CssClass="bold" ID="Label28" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%>'></asp:Label>
                                        &nbsp;|&nbsp; Hora de llegada:
                                        <asp:Label CssClass="bold" ID="Label32" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="4" class="division">
                                        <h4 class="trigger">
                                            <a href="javascript:;">Detalles del Vuelo </a>
                                        </h4>
                                        <div class="toggle_container">
                                            Aeropuerto salida:
                                            <asp:Label ID="Label31" CssClass="bold" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Salida")%>'></asp:Label>
                                            <strong>de</strong>
                                            <asp:Label ID="Label29" runat="server" CssClass="bold" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>
                                            <br />
                                            Aeropuerto llegada:
                                            <asp:Label CssClass="bold" ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Llegada")%>'></asp:Label>
                                            <strong>de</strong>
                                            <asp:Label ID="Label33" CssClass="bold" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Llegada")%>'></asp:Label>
                                            <br />
                                            Tipo de avión:
                                            <asp:Label CssClass="bold" ID="Label16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strEquipment")%>'></asp:Label>
                                            &nbsp;|&nbsp; Clase:
                                            <asp:Label CssClass="bold" ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strClase")%>'></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div class="tarifasVuelo" style="width:624px;height: 75px;background:#28ABE3">
                    <!-- Precios -->
                    <div class="valorTotalVuelo">
                        <asp:Label CssClass="tituloValor" ID="lblTValorTotal" runat="server" Text="Valor total a pagar"></asp:Label>
                        <br />
                        <span style="font-size: 10px;">Todos los impuestos incluidos </span>
                        <br />
                        <asp:Label CssClass="tituloValor" ID="Label12" runat="server" ><%# DataBinder.Eval(Container.DataItem, "str_Tipo_Moneda")%></asp:Label>
                        <asp:Label CssClass="tituloValor" ID="Label52" runat="server" Text="">&nbsp;<%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalPesos")).ToString("###,###.##")%></asp:Label>
                    </div>
                    <div class="valorTotalVuelo">
                        <asp:Button ID="btnSeleccionar" CssClass="botonBuscador" OnClientClick="Show_Cortinilla_Interna();"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SequenceNumber")%>'
                            runat="server" Text="Seleccionar"></asp:Button>
                        <br />
                        <asp:Label ID="tituloImpuestoVuelo" ForeColor="red" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strTextoDG")%>'></asp:Label>
                    </div>
                </div>        
            </div>
        </ItemTemplate>
    </asp:Repeater>


    <asp:Repeater ID="rptItinerarioBFM" runat="server" OnItemCommand="rptItinerario_ItemCommand" EnableViewState="true">
        <ItemTemplate>
            <div class="vuelos">
                <div class="tituloOpcion">
                    Opción <asp:Label ID="lblSequence" runat="server" ></asp:Label>
                </div>
               
                <div class="detalleVuelo">
                    <div class="tablasTrayectosVuelos">
                        <asp:Repeater runat="server" ID="RptSegmentosIda" OnItemDataBound="RptSegmentosIda_ItemDataBound" EnableViewState="true">
                            <ItemTemplate>
                                <table id="tblIda" runat="server" width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos azulOscuro" style="font-size: 12px">
                                    <tr class="filaFechasVuelo">
                                        <td>
                                            <div class="div30px">
                                                <asp:Image ID="ImgIda" runat="server" Visible="false" />
                                            </div>                                        
                                        </td>

                                        <td>
                                            <div class="div285px">
                                                <asp:Label CssClass="bold" ID="lblIda" runat="server" Text="IDA" Visible="false"></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ID="lblCiudadSalida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ID="lblCiudadSalidaCod" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strDepartureAirport")%>'></asp:Label>
                                                <asp:Label CssClass="bold" ID="lblOdeId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "OriginDestinationOption_Id")%>'></asp:Label>
                                            </div>                                        
                                        </td>

                                        <td>
                                            <div class="div220px">
                                                <asp:Image ID="iNext" runat="server" Visible="false" CssClass="noneDisplay"/>
                                                <asp:Label CssClass="bold" ID="lblCiudadLlegada" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%>'></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ID="lblCiudadLlegadaCod" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strArrivalAirport")%>'></asp:Label>
                                            
                                                <asp:Label CssClass="bold" ID="lblFechaSalida" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div70">
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="div30px">
                                                <asp:RadioButton runat="server" GroupName="Sel" ID="rbtnSel" class="RadioButton"/>
                                            </div>                                        
                                        </td>

                                        <td>
                                            <div class="div285px">
                                                <asp:Label ID="lblNameAir" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>'></asp:Label>
                                                <asp:Label ID="lblMarketingAirline" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "strMarketingAirline")%>'></asp:Label>
                                                Salida:
                                                <asp:Label ID="lblHourDeparture" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%>'></asp:Label>
                                                <asp:Label ID="lblHourTotal" runat="server" Visible="false" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida"))%>'></asp:Label>
                                                &nbsp;|&nbsp; 
                                                Llegada:
                                                <asp:Label ID="lblHourArrival" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%>'></asp:Label>
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div220px">
                                                <asp:Label ID="lblTimeFly" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ElapsedTime")%>'></asp:Label>&nbsp;
                                            
                                                <ajax:UpdatePanel ID="upModalIda" runat="server" class="iblock fRight cantidadParadas">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lblHederIda" runat="server" Text='<%# Eval("strParadas") %>'
                                                            CommandArgument='<%# ConcatColumns(Eval("FlightNumber"),Eval("OriginDestinationOption_Id"),"I")%>'
                                                            OnCommand="lnkCustDetails_Click" CssClass="azulOscuro" style="margin-left: -85px;"></asp:LinkButton>
                                                    </ContentTemplate>
                                                </ajax:UpdatePanel>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="div70">
                                                <asp:Image ID="ImgAir" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' AlternateText='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>' />
                                            </div>
                                        </td>
                                    </tr>

                                    <tr id="trIda" runat="server" visible="false">
                                        <td colspan="5">
                                            <hr />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label CssClass="bold" ID="lblFly" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "FlightNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="Separador_Resultados">
                    </div>
                    <div class="tablasTrayectosVuelos" style="padding-top: 18px;">
                        <asp:Repeater runat="server" ID="RptSegmentosReg" OnItemDataBound="RptSegmentosReg_ItemDataBound" EnableViewState="true">
                            <ItemTemplate>
                                <table id="tblVuelta" runat="server" width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos azulOscuro" style="font-size: 12px">
                                    <tr class="filaFechasVuelo">
                                        <td>
                                            <div class="div30px">
                                                <asp:Image ID="ImgVuelta" runat="server" Visible="false"/>                                            
                                            </div>
                                        </td>
                                        <td>
                                            <div class="div285px">
                                                <asp:Label CssClass="bold" ID="lblVuelta" runat="server" Text="Vuelta" Visible="false"></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ID="lblCiudadSalida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ID="lblCiudadSalidaCod" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strDepartureAirport")%>'></asp:Label>
                                                <asp:Label CssClass="bold" ID="lblOdeId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "OriginDestinationOption_Id")%>'></asp:Label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="div220px">
                                                <asp:Image ID="iNext" runat="server" Visible="false" CssClass="noneDisplay" />
                                                <asp:Label CssClass="bold" ID="lblCiudadLlegada" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%>'></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ID="lblCiudadLlegadaCod" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strArrivalAirport")%>'></asp:Label>
                                                <asp:Label CssClass="bold" ID="lblFechaSalida" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="div70">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="div30px">
                                                <asp:RadioButton runat="server" GroupName="Sel" ID="rbtnSel" class="RadioButton" />                                            
                                            </div>
                                        </td>
                                        <td>
                                            <div class="div285px">
                                                salida:
                                                <asp:Label ID="lblHourDeparture" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%>'></asp:Label>
                                                <asp:Label ID="lblHourTotal" runat="server" Visible="false" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida"))%>'></asp:Label>
                                                &nbsp;|&nbsp; 
                                                llegada:
                                                <asp:Label ID="lblHourArrival" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%>'></asp:Label>
                                            </div>                                        
                                        </td>
                                        <td>
                                            <div class="div220px">
                                                <asp:Label ID="lblTimeFly" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ElapsedTime")%>'></asp:Label>&nbsp;
                                        
                                                <ajax:UpdatePanel ID="upModal" runat="server" class="iblock fRight cantidadParadas">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lblHeder" runat="server" Text='<%# Eval("strParadas") %>' CommandArgument='<%# ConcatColumns(Eval("FlightNumber"),Eval("OriginDestinationOption_Id"),"R")%>'
                                                            OnCommand="lnkCustDetails_Click" CssClass="azulOscuro" style="margin-left: -85px;"></asp:LinkButton>
                                                    </ContentTemplate>
                                                </ajax:UpdatePanel>
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div70">
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' AlternateText='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>' />
                                            </div>                                        
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label CssClass="bold" ID="lblFly" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "FlightNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>                         
                </div>

                <div class="tarifasVuelo" style="width:624px;height: 75px;background:#28ABE3">                    
                    <!-- Pasajeros -->
                    <div class="detalleTarifa grisOscuro">
                        <asp:Repeater runat="server" ID="RptTiposPasajeros">
                            <ItemTemplate>
                                <asp:Label ID="lblValorSinImp" runat="server" style="display:none;"></asp:Label>
                                
                                <asp:Repeater ID="RptTarifas" runat="server">
                                    <ItemTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr class="tituloTarifa">
                                                <td style="font-size:10px; color: #fff; font-weight: bold;">
                                                    Valor por
                                                    <asp:Label ID="Label55" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%></asp:Label>
                                                </td>
                                                <td>
                                                    <div class="toggle_container">
                                                        Imp. y Cargos
                                                        <asp:Label ID="lblTotalTasas" runat="server"><%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalImpuestosTasas")).ToString("###,###.##")%></asp:Label>
                                                        Valor por tipo de pasajero<br />
                                                        <asp:Label CssClass="bold" ID="Label58" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>&nbsp;&nbsp; <%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IntTotalTarifaConTaXPersona")).ToString("###,###.##")%></asp:Label>
                                                    </div>
                                                    <a class="impuestosVuelos" href="#this">
                                                        <span class="valorTarifa" style="color:#fff;">
                                                        <asp:Label ID="Label3" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>&nbsp;<%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IntTotalTarifaConTaXPersona")).ToString("###,###.##")%></asp:Label>
                                                    </span>
                                                        <div class="GloboImp">
                                                            <span style="color: #034185; float: left; text-align: center;">Detalle de tarifas, impuestos
                                                                y cargos administrativos por
                                                                <%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%></span> <span class="labelDetalleImpuestos">
                                                                    Tarifa</span>
                                                            <asp:Label ID="Label13" runat="server" CssClass="valorlDetalleImpuestos">		
			                                                        <%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>
				                                                        &nbsp;
			                                                        <%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "intBaseFare")).ToString("###,###.##")%>
                                                            </asp:Label>
                                                            <asp:Repeater runat="server" ID="RptImpuestos">
                                                                <ItemTemplate>
                                                                    <span class="labelDetalleImpuestos">
                                                                        <%# DataBinder.Eval(Container.DataItem, "strNombre_Impuesto")%>
                                                                    </span><span class="valorlDetalleImpuestos">
                                                                        <%# DataBinder.Eval(Container.DataItem, "CurrencyCode")%>&nbsp;&nbsp;
                                                                        <%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "Amount")).ToString("C")%>
                                                                    </span>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Precios -->
                    <div class="valorTotalVuelo grisOScuro">
                        <asp:Label CssClass="tituloTotal grisOscuro" ID="lblTValorTotal" runat="server" Text="Valor total a pagar" style="color:#fff;"></asp:Label>
                        
                        <div class="iblock full" style="color:#fff;">
                            <asp:Label ID="lblTotalOriginal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IntTotalPesos")%>'></asp:Label>
                            <asp:Label CssClass="monedaTotal grisOscuro" ID="Label12" runat="server" style="color:#fff;" >
                                <%# DataBinder.Eval(Container.DataItem, "str_Tipo_Moneda")%>
                            </asp:Label>
                            <asp:Label CssClass="valorTotal grisOscuro" ID="Label52" runat="server" Text="" style="color:#fff;">
                                <%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalPesos")).ToString("###,###.##")%>
                            </asp:Label>
                            <asp:Label CssClass="tituloValor" Visible="False" ID="lblAerolinea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strMarketingAirline")%>'></asp:Label>
                        </div>

                        <span class="labelImpuestosIncluidos grisOscuro" style="color:#fff;">
                            Todos los impuestos incluidos
                        </span>
                    </div>

                    <div class="seleccionarVuelos">
                        <asp:Button ID="btnSeleccionar" class="link-button white" OnClientClick="Show_Cortinilla_Interna();" runat="server" Text="Seleccionar"></asp:Button>
                        <asp:Label ID="tituloImpuestoVuelo" ForeColor="red" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strTextoDG")%>'></asp:Label>
                    </div>
                </div>         
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<%--Modal Mandatorio--%>
<%--Fin Modal Mandatorio--%>
<div class="mensajeError" id="dPanel" runat="server">
</div>
<%--<uc2:ucFiltrosdeVuelos ID="ucFiltros" runat="server" />--%>
<ajax:ModalPopupExtender ID="MPEEAdicionales" BackgroundCssClass="ui-widget-shadow"
    DropShadow="false" runat="server" TargetControlID="dummyLink4" Drag="false" BehaviorID="MPEEAdicionales"
    OnOkScript="" OkControlID="btnCerrarAdicionales" EnableViewState="true" PopupControlID="PanelAdicionales" />
<asp:Panel runat="server" ID="PanelAdicionales">
    <div class="ventana4">
        <table width="100%" cellspacing="0" cellpadding="2">
            <tr class="tituloPopUp">
                <td>
                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align: right">
                    <asp:Button ID="btnCerrarAdicionales" CssClass="botonCerrar" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblmensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink4"
    runat="server">dummy>dummy</a> <a href="#" style="display: none; visibility: hidden;"
        onclick="return false" id="dummyLink5" runat="server">dummy>dummy</a>
<asp:Button runat="server" ID="btnShowModalPopup" Style="display: none" />
<div class="contenedorPopUp">
    <ajax:ModalPopupExtender ID="mdlDetail" runat="server" TargetControlID="btnShowModalPopup"
        PopupControlID="divPopUp" PopupDragHandleControlID="panelDragHandle" Drag="true"
        CancelControlID="btnClose" DropShadow="true" />
</div>
<br />
<div class="popUpStyle" id="divPopUp" style="display: none;">
    <div id="popup-detail" class="flights-popup popup-detail  popup">
        <div class="popup-border">
        </div>
        <div class="popup-container">
            <div class="popup-header">
                <h4>
                    &nbsp;<asp:Panel runat="Server" ID="panelDragHandle">
                        Detalle del vuelo
                    </asp:Panel>
                </h4>
            </div>
            <ajax:UpdatePanel ID="upModalINT" runat="server">
                <ContentTemplate>
                    <asp:Repeater ID="rptItineraryModal" runat="server">
                        <ItemTemplate>
                            <asp:Repeater ID="rptDetalleModal" runat="server">
                                <ItemTemplate>
                                    <div class="popup-content">
                                        <div class="detail">
                                            <div class="segment">
                                                <ul class="detail-info">
                                                    <li class="itinerary"><span class="location">Aerolinea:
                                                        <asp:Label CssClass="bold" ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>'></asp:Label>
                                                        &nbsp;|&nbsp; Vuelo: <span class="number">
                                                            <asp:Label CssClass="bold" ID="Label78" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlightNumber")%>'></asp:Label>
                                                        </span></li>
                                                    <li class="data"><span class="name">
                                                        <asp:Label CssClass="bold" ID="Label2" runat="server" Text="Paradas"> </asp:Label>
                                                        <asp:Label CssClass="bold" ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strEstiloParada")%>'></asp:Label><br />
                                                    </li>
                                                    <li class="data"><span class="name">
                                                        <asp:Label CssClass="bold" ID="Label5" runat="server" Text="Tiempo de vuelo"></asp:Label>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ElapsedTime")%>'></asp:Label>
                                                    </span><span class="airline"><span class="logoPequeño"></span>
                                                        <img src='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                                                    </span></li>
                                                    <li class="itinerary"><span class="location">Sale de
                                                        <asp:Label CssClass="bold" ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>
                                                        -
                                                        <asp:Label ID="Label8" CssClass="bold" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Salida")%>'></asp:Label>
                                                    </span><span class="date">
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label></span>
                                                    </li>
                                                    <li class="itinerary"><span class="location">Llega a
                                                        <asp:Label CssClass="bold" ID="Label27" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%>'></asp:Label>
                                                        -
                                                        <asp:Label CssClass="bold" ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Llegada")%>'></asp:Label></span>
                                                        <span class="date">
                                                            <asp:Label ID="Label35" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label></span>
                                                    </li>
                                                </ul>
                                            </div>
                                            <span class="time">Hora de salida:
                                                <asp:Label CssClass="bold" ID="Label28" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%>'></asp:Label>
                                                &nbsp;|&nbsp; Hora de llegada:
                                                <asp:Label CssClass="bold" ID="Label32" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%>'></asp:Label>
                                            </span>
                                            <div class="detail-footer">
                                                <div class="bottom-box">
                                                    <span class="detail-local-hour">Horarios en hora local de cada ciudad</span>
                                                    <div class="baggage">
                                                    </div>
                                                </div>
                                                <div id="caja">
                                                    <div class="rules">
                                                        <span class="airline"><span class="logoPequeño"><span>
                                                            <img src='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                                                        </span><span class="name"><strong>
                                                            <%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%></strong> </span>
                                                        </span></span>
                                                        <div class="text">
                                                            Aeropuerto salida:
                                                            <asp:Label ID="Label31" CssClass="bold" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Salida")%>'></asp:Label>
                                                            <strong>de</strong>
                                                            <asp:Label ID="Label29" runat="server" CssClass="bold" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>
                                                            <br />
                                                            Aeropuerto llegada:
                                                            <asp:Label CssClass="bold" ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Llegada")%>'></asp:Label>
                                                            <strong>de</strong>
                                                            <asp:Label ID="Label33" CssClass="bold" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Llegada")%>'></asp:Label>
                                                            <br />
                                                            Tipo de avión:
                                                            <asp:Label CssClass="bold" ID="Label16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strEquipment")%>'></asp:Label>
                                                            &nbsp;|&nbsp; Clase:
                                                            <asp:Label CssClass="bold" ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strClase")%>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </ajax:UpdatePanel>
        </div>
        <span class="popup-close-button popup-close">
            <asp:Button ID="btnClose" runat="server" Text="×" /></span> <span class="popup-arrow popup-arrow-top"
                style="left: 345px;"></span>
    </div>
</div>
<div class="contenedorPopUp">
    <ajax:ModalPopupExtender ID="MdlError" runat="server" TargetControlID="btnShowModalPopup"
        PopupControlID="divError" PopupDragHandleControlID="pnlModalError" Drag="true"
        CancelControlID="btnClose" DropShadow="true" />
    <div class="popUpStyle" id="divError" style="display: none;">
        <div id="Div3" class="flights-popup popup-detail  popup">
            <div class="popup-border">
            </div>
            <div class="popup-container">
                <div class="popup-header">
                    <h4>
                        &nbsp;<asp:Panel runat="Server" ID="pnlModalError">
                            Por favor verificar
                        </asp:Panel>
                    </h4>
                </div>
                <br />
                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Visible="false" ForeColor="Red"
                    Text="Por favor seleccione su vuelo"></asp:Label><br />
            </div>
        </div>
    </div>
</div>
<ajax:ModalPopupExtender ID="MPETopflight" BackgroundCssClass="ui-widget-shadow"
    DropShadow="false" Drag="false" EnableViewState="true" runat="server" TargetControlID="dummyLinkTopflight"
    OnOkScript="" OkControlID="btnCerrarf" BehaviorID="MPETopflight" PopupControlID="PanelIframe" />
<asp:Panel runat="server" ID="PanelIframe">
    <div>
        <asp:Button ID="btnCerrarf" CssClass="cerrarTopFlight" runat="server" />

        <asp:Label ID="lbltopflight" runat="server" Visible="false"></asp:Label>
        <uc3:ucIngresoTTQ ID="ucIngresoTTQ" runat="server" />
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLinkTopflight"
    runat="server">dummy</a>
<ajax:ModalPopupExtender ID="mdodalvalida" BackgroundCssClass="ui-widget-shadow"
    runat="server" TargetControlID="dummyLink5" BehaviorID="mdodalvalida" OnOkScript=""
    CancelControlID="Button1" PopupControlID="panelventana" />
<asp:Panel runat="server" ID="panelventana">
    <div class="ventana4">
        <div>
            <div class="btnCloseFirst">
                <asp:Button ID="Button1" class="btnCloseFirst_X" runat="server" Text="x" /></div>
            <div class="parrafoVentana4">
                <asp:Label ID="Label9" runat="server" Font-Bold="true" ForeColor="red" Text="No se ha seleccionado ningun vuelo. Por favor seleccione su vuelo"></asp:Label>
            </div>
        </div>
    </div>
</asp:Panel>
<!-- Validacion itinerarios Juan Camilo Diaz 2013-10-03-->
<!-- Validacion itinerarios-->
<ajax:ModalPopupExtender ID="MPEEDisponibilidad" BackgroundCssClass="ui-widget-shadow"
    DropShadow="false" runat="server" TargetControlID="dummyLink" Drag="false" BehaviorID="MPEEDisponibilidad"
    OnOkScript="" OkControlID="btnCerrar" EnableViewState="true" PopupControlID="PanelDisponibilidad" />
<asp:Panel runat="server" ID="PanelDisponibilidad">
    <div class="ventanaError1">
        <asp:Button ID="btnCerrar" CssClass="botonCerrar" runat="server" Style="float: right;
            margin-top: -30px; margin-right: -30px;" />
        <div class="LabelVentanaError">
            Lo sentimos, los siguientes vuelos no cuentan con disponibilidad
        </div>
        <div class="LabelVentanaError">
            <asp:Repeater ID="rptDispo" runat="server">
                <ItemTemplate>
                    <strong>Número de vuelo:</strong>&nbsp;<%# DataBinder.Eval(Container.DataItem, "FlightNumber")%><br />
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="LabelVentanaError">
            Por favor intenta con otro itinerario
        </div>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink"
    runat="server">dummy</a> 