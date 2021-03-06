<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTarifasCircuitos.ascx.cs"
    Inherits="uc_ucTarifasCircuitos" %>

<div class="panelResultados" style="float:none;">
    <div class="contenidoResultado">
        <ajax:UpdatePanel ID="upCircuitos" runat="server">
            <ContentTemplate>
                <!--- Cotizador -->
                <div class="cotizadorPlan">
                    <div class="ancho47 borderRight">
                        <div class="ancho100">
                            <asp:Label CssClass="labelCotizador" ID="lblTDuracionCirc" runat="server" Text="Duraci�n "></asp:Label>
                            <asp:Label ID="lblDuracion" runat="server" Text="" CssClass="datoCotizador"></asp:Label>
                        </div>

                        <div class="ancho100">
                            <asp:Label CssClass="labelCotizador" ID="lblTSalidas" runat="server" Text="Salidas"></asp:Label>
                        </div>

                        <div class="ancho100">
                            <asp:Label CssClass="labelCotizador" ID="lblTAnioSalida" runat="server" Text="A�o "></asp:Label>

                            <asp:DropDownList ID="ddlAnioSalida" CssClass="datoCotizador" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAnioSalida_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="ancho100">
                            <div style="width:48%; display:inline-block;">
                                    <asp:Label CssClass="labelCotizador" ID="lblTMesSalida" runat="server" Text="Mes "></asp:Label>
                                <asp:DropDownList ID="ddlMesSalida" CssClass="datoCotizador" style="width:90%;"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMesSalida_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div style="width:48%; display:inline-block;">
                                <asp:Label CssClass="labelCotizador" ID="lblTDiaSalida" runat="server" Text="D�a "></asp:Label>
                            <asp:DropDownList ID="ddlDiaSalida" CssClass="datoCotizador" style="width:90%;" runat="server">
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="ancho47">    
                        <div class="ancho100">
                            <asp:Label CssClass="labelCotizador" ID="lblTCabinasPax" runat="server" Text="HABITACIONES Y PASAJEROS"></asp:Label>                            
                        </div>

                        <div class="ancho100">
                            <asp:Label CssClass="labelCotizador" ID="lblTCabinas" runat="server" Text="N�mero de Habitaciones"></asp:Label>

                            <asp:DropDownList ID="ddlCabinas" CssClass="datoCotizador" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCabinas_SelectedIndexChanged">
                                <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                <asp:ListItem Text="04" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="ancho100"> 
                            <div style="width:100%; float:left; font-size:9px;">
                                <asp:Repeater ID="rptPasajeros" runat="server">
                                    <ItemTemplate>
                                        <div class="borderCotizador">
                                            <div class="ancho50" style="display:none">
                                                <asp:TextBox ID="txtNumRealAdt" runat="server" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtNumRealCnn" runat="server" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblTNoCabina" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Cabina") %>'></asp:Label>
                                            </div>

                                            <div class="ancho50">
                                                <div class="labelCotizador">
                                                    <asp:Label ID="lblTAdultos" runat="server" Text="Adultos "></asp:Label>
                                                    <asp:Label ID="lblEdadAdt" runat="server" Text=""></asp:Label>
                                                </div>

                                                <asp:DropDownList ID="ddlAdultos" runat="server" CssClass="datoCotizador">
                                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                    <%--<asp:ListItem Text="05" Value="5"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="ancho50" style="display:none;">
                                                <div class="labelCotizador">
                                                    <asp:Label ID="Label13" runat="server" Text="Juniors "></asp:Label>   
                                                    <asp:Label ID="Label16" runat="server" Text="(12 a 17 A�os)"></asp:Label>                                                                                                     
                                                </div>

                                                <asp:DropDownList ID="ddlJuniors" runat="server" CssClass="datoCotizador">
                                                    <asp:ListItem Text="00" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="ancho50">
                                                <div class="labelCotizador" style="padding-left:10px;">
                                                    <asp:Label ID="Label14" runat="server" Text="Menores "></asp:Label>
                                                    <asp:Label ID="lblEdadCnn" runat="server" Text=""></asp:Label>
                                                </div>

                                                <asp:DropDownList ID="ddlNinos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNinos_Selected" CssClass="datoCotizador">
                                                    <asp:ListItem Text="00" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />

                                                <div style="width:180px;">
                                                <asp:Repeater ID="rptEdadninos" runat="server">
                                                    <HeaderTemplate>
                                                        <asp:Label CssClass="bold" ID="lblTEdades" runat="server" Text="Edades:"></asp:Label><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label CssClass="bold" ID="lblNNino" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strPax") %>'></asp:Label>
                                                        <asp:DropDownList ID="ddlEdadNino" runat="server">
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
                                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                        </asp:DropDownList><br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="contenedorBotonBuscarPlanes">
                            <asp:Button CssClass="link-button white" ID="btnCotizar" runat="server" Text="Cotizar" OnCommand="btn_Command" CommandName="Cotizar" ValidationGroup="2" />
                        </div>
                    </div>

                    <div class="full iblock grisOscuro vigencia">
                    </div>

                    <div class="noneDisplay">
                        <asp:Label CssClass="bold" ID="lblTMoneda" runat="server" Text="Tarifas por habitaci�n en "></asp:Label>
                        <asp:Label ID="lblMoneda" runat="server" Text=""></asp:Label>                            
                    </div>

                    <asp:Label ID="lblErrorGen" runat="server" ForeColor="#84021C"></asp:Label>

                    <table width="100%" style="display: none"> 
                        <tr style="display: none">
                            <td>
                                <asp:Label ID="lblTBarco" runat="server" Text="Barco"></asp:Label>
                            </td>
                            <td>
                                <div id="dBarco" runat="server">
                                </div>
                            </td>
                        </tr>
                    </table>                    
                </div>

                <div class="cotizadorPlan">
                     <asp:Repeater ID="rptCabina" runat="server">
                        <ItemTemplate>
                            <div class="full azulOscuro iblock bold textLeft">
                                <asp:Label ID="lblTCabina" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NoCabina") %>'></asp:Label>                                
                            </div>

                            <div class="full grisOscuro iblock bold textLeft">
                                <asp:Label ID="lblTTarifasPersona" runat="server" Text="Tarifas por habitaci�n en "></asp:Label>
                                <asp:Label ID="lblMonedaTarifa" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strMoneda") %>'></asp:Label>
                                &nbsp;&nbsp;/&nbsp;&nbsp;
                                <asp:Label ID="lblTDesde" runat="server" Text="Vigentes desde: "></asp:Label>
                                <asp:Label ID="lblDesde" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dtmDesdeFecha") %>'></asp:Label>
                                <asp:Label ID="lblTHasta" runat="server" Text=" hasta: "></asp:Label>
                                <asp:Label ID="lblHasta" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dtmHastaFecha") %>'></asp:Label>
                                &nbsp;&nbsp;/&nbsp;&nbsp;
                                <asp:Label ID="lblTDuracion" runat="server" Text="Duraci�n: "></asp:Label>
                                <asp:Label ID="lblDias" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intDias") %>'></asp:Label>
                                <asp:Label ID="lblTDias" runat="server" Text=" dias / "></asp:Label>
                                <asp:Label ID="lblNoches" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intNoches") %>'></asp:Label>
                                <asp:Label ID="lblTNoches" runat="server" Text=" noches"></asp:Label>
                            </div>

                            <div class="iblock azulClaro full textLeft">
                                <asp:Label CssClass="bold" ID="lblTAcomodacion" runat="server" Text="Acomodaci�n: "></asp:Label>
                                <asp:Label ID="lblAcomodacion" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strAcomodacion") %>'></asp:Label>
                            </div>                        

                            <div class="contenedorCotizador">
                                <asp:Repeater ID="rptHoteles" runat="server">
                                    <HeaderTemplate>
                                       <div class="filaCotizador">  
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador celda15">
                                                <asp:Label ID="Label9" runat="server" Text="Hotel"></asp:Label>
                                            </div>
                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador celda15">
                                                <asp:Label ID="Label10" runat="server" Text="Tipo habitaci�n"></asp:Label>
                                            </div>
                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador celda10">
                                                <asp:Label ID="Label4" runat="server" Text="Alimentaci�n"></asp:Label>
                                            </div>
                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador celda10">
                                                <asp:Label ID="Label1" runat="server" Text="Tarifas adultos"></asp:Label>
                                            </div>
                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador noneDisplay">
                                                <asp:Label ID="Label11" runat="server" Text="Adulto adicional"></asp:Label>
                                            </div>
                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador noneDisplay">
                                                <asp:Label ID="Label3" runat="server" Text="Junior"></asp:Label>
                                            </div>
                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador celda10">
                                                <asp:Label ID="Label12" runat="server" Text="Tarifas menores"></asp:Label>
                                            </div>
                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador noneDisplay">
                                                 <asp:Label ID="Label7" runat="server" Text="Total sin impuestos"></asp:Label>
                                            </div>
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador celda10">
                                                 <asp:Label ID="Label14" runat="server" Text="Impuestos"></asp:Label>
                                            </div>                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador celda10">
                                                <asp:Label ID="Label6" runat="server" Text="Total con impuestos"></asp:Label>
                                            </div>
                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador bordeRightCotizador celda9">
                                                <asp:Label ID="Label8" runat="server" Text="Seleccionar"></asp:Label>
                                            </div>                                      
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="filaCotizador">
                                            <div class="celdaBaseCotizador celda15">
                                                <asp:Label ID="lblIdCategoria" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.CodigoCategoria") %>'></asp:Label>
                                                <asp:Label ID="lblIdHotel" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Codigo") %>'></asp:Label>
                                                <asp:Label ID="lblNombreHotel" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>'></asp:Label>
                                                <a href='#this' onclick='javascript:SetIdHotelRot("<%# DataBinder.Eval(Container.DataItem, "Codigo")%>");' class="nombreHotelCotizador" >
                                                    <%# DataBinder.Eval(Container.DataItem, "Nombre")%>
                                                </a>
                                            </div>
                                            <div style="float:left; width: 781px;">
                                                <asp:Repeater ID="rptTarifas" runat="server">                                                
                                                    <ItemTemplate>
                                                        <div class="filaCotizador">
                                                            <div class="celdaBaseCotizador celda15">
                                                                <asp:Label ID="lblIdTarifa" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.IdTarifa") %>'></asp:Label>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTipoHabitacion").ToString() + " " + DataBinder.Eval(Container,"DataItem.strSubTipoHab").ToString() %>'></asp:Label>
                                                            </div>
                                                            
                                                            <div class="celdaBaseCotizador celda10">
                                                                <a class="impuestos" href="#this">
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strRefereTipoAlim") %>'></asp:Label>
                                                                    <div class="GloboImp ">
                                                                        <%# DataBinder.Eval(Container, "DataItem.strTipoAlim")%>
                                                                    </div>
                                                                </a>
                                                            </div>
                                                            
                                                            <div class="celdaBaseCotizador celda10">
                                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblTarifaFormato") %>'></asp:Label>
                                                            </div>
                                                            
                                                            <div class="celdaBaseCotizador noneDisplay">
                                                                <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblPrecioAdt3Formato") %>'></asp:Label>
                                                            </div>
                                                            
                                                            <div class="celdaBaseCotizador noneDisplay">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblPrecioInfanteFormato") %>'></asp:Label>
                                                            </div>
                                                            
                                                            <div class="celdaBaseCotizador celda10">
                                                                <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblPrecioNinoFormato") %>'></asp:Label>
                                                            </div> 
                                                            
                                                            <div class="celdaBaseCotizador noneDisplay">
                                                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblTotalSinImpuestosFormato") %>'></asp:Label>
                                                                <asp:Label ID="lblTotalNoImp" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblTotalSinImpuestos") %>'></asp:Label>
                                                            </div> 
                                                            
                                                            <div class="celdaBaseCotizador celda10">
                                                                <a class="impuestos1" href="#this">
                                                                     <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblSumaImpuestos") %>'></asp:Label>
                                                                    <div class="GloboImp" style="display:none;">
                                                                        <%# DataBinder.Eval(Container, "DataItem.strHtmlImp")%>
                                                                    </div>
                                                                </a>                                                                
                                                            </div>
                                                            <div class="celdaBaseCotizador celda10">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblTotalImpuestosFormato") %>'></asp:Label>
                                                                <asp:Label ID="lblTotalImp" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblTotalImpuestos") %>'></asp:Label>
                                                            </div>
                                                            <div class="celdaBaseCotizador celda9 bordeRightCotizador">
                                                                <asp:RadioButton ID="rbTarifa" runat="server" OnCheckedChanged="rbTarifa_CheckedChanged" AutoPostBack="true" class="RadioPlanes" />
                                                            </div>                                       
                                                        </div>                                                        
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div style="float: left; position: relative; display:none;">
                                <input id="btn1" type="button" class="botonEnviarAmigo" runat="server" value="" onclick='javascript:popUp();'/>
                                  <input id="btnImprimir" type="button" Class="botonImprimirPlan" value=""   onclick='window.print();return false' />     
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                    <div style="float: right; position: relative; margin-right: 24px; width: 100px;">
                         <asp:Button CssClass="link-button white" ID="btnReservar" runat="server" Text="Reservar" OnCommand="btn_Command"
                            CommandName="Reservar"  Visible="false" />

                            <ajax:ModalPopupExtender ID="mdodalvalida" BackgroundCssClass="ui-widget-shadow"
                            runat="server" TargetControlID="dummyLink5" BehaviorID="mdodalvalida" OnOkScript=""
                            CancelControlID="Button1" PopupControlID="panelventana" />
                        <asp:Panel runat="server" ID="panelventana">
                            <div class="ventana4">
                                <div>
                                    <div class="btnCloseFirst">
                                        <asp:Button ID="Button1" class="btnCloseFirst_X" runat="server" Text="x" /></div>
                                    <div class="parrafoVentana4">
                                        <asp:Label ID="lblMensajeValida" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink5"
                            runat="server">dummy</a>

                    </div>
                </div>
                <div class="cotizadorPlan" style="text-align: center" runat="server" id="Div1">
                    <ajax:UpdateProgress ID="udpEsperar" runat="server" AssociatedUpdatePanelID="upCircuitos">
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
                    <asp:Button ID="btnVerificar" runat="server" CssClass="botonBuscar" Text="   Verificar   "
                        OnCommand="btn_Command" CommandName="VerificarOfertas" />
                    <br />
                    <br />
                    <asp:Label ID="lblErrorOfertas" ForeColor="#186e9b" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
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

    <div class="contenedorPopUpCondiciones">
        <asp:Button ID="btnCerrar2" runat="server" Text="X" CssClass="btnCerrar azulClaro" />
        <asp:Label ID="Label9" runat="server" Text="Detalle del Hotel" Visible="false"></asp:Label>

        <div class="condiciones">
            <iframe id="iHotelRot" frameborder="0" style="width:100%; height:391px; border:0;"></iframe>
        </div>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink"
    runat="server">dummy</a> 