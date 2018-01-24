<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTarifasExcursiones.ascx.cs"
    Inherits="uc_ucTarifasExcursiones" %>
    
<div class="panelResultados">
    <div class="tituloResultados">
        <asp:Label ID="Label15" runat="server" Text="CONSULTA LAS TARIFAS DISPONIBLES  &raquo;"></asp:Label>
    </div>

    <div class="contenidoResultado">
        <ajax:UpdatePanel ID="upRotativos" runat="server">
            <ContentTemplate>
                <!--- Cotizador -->
                <div class="cotizadorPlan">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label CssClass="bold" ID="lblTCategoria" runat="server" Text="Moneda "></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMoneda" runat="server" Text=""></asp:Label>
                        <asp:DropDownList Width="100" ID="ddlMoneda" runat="server" Visible="false">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label CssClass="bold" ID="lblTCabinasPax" runat="server" Text="Cantidad de personas"></asp:Label>
                    </td>
                    <td>
                        <div style="float: left; position: relative; padding: 3px; display: none">
                            <asp:Label ID="lblTCabinas" runat="server" Text="Habitaciones"></asp:Label>
                            <asp:DropDownList ID="ddlCabinas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCabinas_SelectedIndexChanged">
                                <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                <asp:ListItem Text="04" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; position: relative;">
                            <table width="100%" cellpadding="0">
                                <asp:Repeater ID="rptPasajeros" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="display: none">
                                                <asp:Label ID="lblTNoCabina" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Cabina") %>'></asp:Label>
                                            </td>
                                            <td style="display: none">
                                                <asp:Label CssClass="bold" ID="lblTAdultos" runat="server" Text="Adultos"></asp:Label>
                                                <br />
                                                <asp:Label CssClass="bold" ID="Label2" runat="server" Text="(+12 años)"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAdultos" runat="server">
                                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="display: none">
                                                <asp:Label CssClass="bold" ID="lblTNinos" runat="server" Text="Niños "></asp:Label>
                                                <br />
                                                <asp:Label CssClass="bold" ID="Label3" runat="server" Text="(2 - 11 años)"></asp:Label>
                                            </td>
                                            <td style="display: none">
                                                <asp:DropDownList ID="ddlNinos" runat="server">
                                                    <asp:ListItem Text="00" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </td>
                    <td>
                        <asp:Label CssClass="bold" ID="lblTFecha" runat="server" Text="Fecha de servicio"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox Width="100" ID="txtFecha" Style="float: left; position: relative; padding: 3px;
                            height: 15px" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblVigencia" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="text-align: right;">
                        <asp:Button CssClass="botonCotizarPlan" ID="btnCotizar" runat="server" Text="" OnCommand="btn_Command"
                            CommandName="Cotizar" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblErrorGen" runat="server" ForeColor="#186e9b"></asp:Label>
        </div>
                        
                <div class="cotizadorPlan">
                    <asp:Repeater ID="rptCabina" runat="server">
                <ItemTemplate>
                    <div style="float: left; position: relative; width: 98%; padding: 1%; font-weight: bold">
                        <div style="float: left; position: relative; margin-right: 5px; display: none;">
                            <asp:Label CssClass="bold" ID="lblTCabina" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NoCabina") %>'></asp:Label>
                        </div>
                        <div style="float: left; position: relative; margin-right: 5px; display: none;">
                            <asp:Label CssClass="bold" ID="lblTTarifasPersona" runat="server" Text="Tarifas por habitación en "></asp:Label>
                            <asp:Label ID="lblMonedaTarifa" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strMoneda") %>'></asp:Label>
                        </div>
                        <div style="float: left; position: relative; margin-right: 5px;">
                            <asp:Label CssClass="bold" ID="lblTDesde" runat="server" Text="Vigentes desde: "></asp:Label><asp:Label
                                ID="lblDesde" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dtmDesdeFecha") %>'></asp:Label>
                            <asp:Label ID="lblTHasta" runat="server" Text=" hasta: "></asp:Label><asp:Label ID="lblHasta"
                                runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dtmHastaFecha") %>'></asp:Label>
                        </div>
                        <div style="float: left; position: relative; margin-right: 5px; display: none;">
                            <asp:Label CssClass="bold" ID="lblTDuracionToures" runat="server" Text="Duración: "></asp:Label>
                            <asp:Label ID="lblDias" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intDias") %>'></asp:Label><asp:Label
                                ID="lblTDias" runat="server" Text=" dias / "></asp:Label>
                            <asp:Label ID="lblNoches" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intNoches") %>'></asp:Label><asp:Label
                                ID="lblTNoches" runat="server" Text=" noches"></asp:Label>
                        </div>
                        <div style="float: left; position: relative; margin-right: 5px; display: none;">
                            <asp:Label CssClass="bold" ID="lblTAcomodacion" runat="server" Text="Acomodación: "></asp:Label>
                            <asp:Label ID="lblAcomodacion" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strAcomodacion") %>'></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; position: relative; margin-bottom: 10px; width: 100%;">
                        <table width="100%" style="text-align: center; background: #FFF;" border="1">
                            <tr style="background-color: #F90; color: #000; font-weight: bold;">
                                <td style="width: 10%">
                                    <asp:Label ID="Label1" runat="server" Text="Adulto"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="Label5" runat="server" Text="Total sin impuestos"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="Label6" runat="server" Text="Total con impuestos"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="Label7" runat="server" Text="Seleccionar"></asp:Label>
                                </td>
                            </tr>
                            <asp:Repeater ID="rptTarifas" runat="server">
                                <ItemTemplate>
                                    <tr style="color: #000;">
                                        <td style="width: 10%;">
                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblTarifaFormato") %>'></asp:Label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblTotalSinImpuestosFormato") %>'></asp:Label>
                                            <asp:Label ID="lblTotalNoImp" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblTotalSinImpuestos") %>'></asp:Label>
                                        </td>
                                        <td style="width: 10%">
                                            <a class="impuestos" href="#this">
                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblTotalImpuestosFormato") %>'></asp:Label>
                                                <%--<asp:Label ID="lblmoneda" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strMoneda") %>'></asp:Label>--%>
                                                <div>
                                                    <%# DataBinder.Eval(Container, "DataItem.strHtmlImp")%>
                                                </div>
                                            </a>
                                            <asp:Label ID="lblTotalImp" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblTotalImpuestos") %>'></asp:Label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="lblIdTarifa" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.IdTarifa") %>'></asp:Label>
                                            <asp:RadioButton ID="rbTarifa" runat="server" OnCheckedChanged="rbTarifa_CheckedChanged"
                                                AutoPostBack="true" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <%-- <div style="float: left; position: relative;">
                               <input id="btn1" type="button" class="botonEnviarAmigo" runat="server" value=""  onclick='javascript:SetEnviar();'/>
                                <asp:Button ID="btnImprimir" CssClass="botonImprimirPlan" runat="server" CommandName="Print" OnCommand="btn_Command1" Text=""/>
                            </div>--%>
                </FooterTemplate>
            </asp:Repeater>
                    <div style="float: right; position: relative;">
                        <asp:Button CssClass="botonReservarPlan" OnClientClick="Show_Cortinilla_Interna();" ID="btnReservar" runat="server" Text="" OnCommand="btn_Command"
                            CommandName="Reservar"  Visible="false" />
                    </div>
                </div>
                        <div class="cotizadorPlan" style="text-align:center" runat="server" id="Div1">
                            <ajax:UpdateProgress ID="udpEsperar" runat="server" AssociatedUpdatePanelID="upRotativos">
                                <ProgressTemplate>
                                    <div class="progressbar">
                                    </div>
                                    <img alt="" src="../App_Themes/Imagenes/loading.gif" /><br />
                                    <asp:Label ID="lblEsperar" runat="server" Text="Espere por favor..."></asp:Label>
                                </ProgressTemplate>
                            </ajax:UpdateProgress>
                        </div>
                        <asp:Panel ID="pOfertasAereas" runat="server" Visible="false">                                             
                            <asp:Label ID="lblOfertas" runat="server" Text="Tenemos tarifas aereas especiales para este plan"></asp:Label><br />
                            <asp:Label ID="lblDisp" runat="server" Text="Verificar disponibilidad de cupos"></asp:Label><br />
                            <asp:Label ID="Label18" runat="server" Text="Elija ciudad de origen"></asp:Label>&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlCiudadOrigenOferta" runat="server">                            
                            </asp:DropDownList><br />
                            <asp:Button ID="btnVerificar" runat="server" CssClass="botonBuscar" Text="   Verificar   " OnCommand="btn_Command"
                                CommandName="VerificarOfertas"/>
                            <br />
                            <br />
                            <asp:Label ID="lblErrorOfertas"  ForeColor="#186e9b" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblFechaIni" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblFechaFin" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblNumAdultos" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblNumNinios" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblNumInfantes" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblAeroLinea" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblDisponibilidad" runat="server" Text="false" Visible="false"></asp:Label>
                            <asp:Label ID="lblCodigoOferta" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblPseudo" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCodigoConvenio" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCodigoPax" runat="server" Text="" Visible="false"></asp:Label>
                        </asp:Panel>
            </ContentTemplate>
        </ajax:UpdatePanel>
    </div>
</div>

<!-- Detalle Hotel -->
<ajax:ModalPopupExtender ID="MPEEHotelRot" BackgroundCssClass="ui-widget-shadow" DropShadow="false"
    runat="server" TargetControlID="dummyLink" Drag="false" BehaviorID="MPEEHotelRot"
    OnOkScript="" OkControlID="btnCerrar2" EnableViewState="true" PopupControlID="PanelHotelRot" />
<asp:Panel runat="server" ID="PanelHotelRot">

    <div style="float: right; position: relative;">
        <asp:Button ID="btnCerrar2" runat="server" CssClass="botonCerrarVuelosTarjeta" style="margin-top: -10px; margin-bottom:auto;" />
    </div>

    <div class="ventanaItinerario"> 
        <div class="tituloItinerario">
            <asp:Label ID="Label9" runat="server" Text="Detalle del Hotel"></asp:Label>
        </div>

        <div class="contenidoItinerario">
            <iframe id="iHotelRot" frameborder="0" width="700" height="360"></iframe>
        </div>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink"
    runat="server">dummy</a> 
  