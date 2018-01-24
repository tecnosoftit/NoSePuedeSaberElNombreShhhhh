<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTarifasRotativosMulti.ascx.cs"
    Inherits="uc_ucTarifasRotativosMulti" %>
    
<div class="panelResultados">
    <div class="contenidoResultado">
        <ajax:UpdatePanel ID="upRotativos" runat="server">
            <ContentTemplate>
                <!--- Cotizador -->
                <div class="cotizadorPlan">
                    <div class="full iblock grisOscuro vigencia">
                        <asp:Label ID="lblVigencia" runat="server" Text="" CssClass="labelCotizador"></asp:Label>
                        <asp:Label CssClass="labelCotizadorHabitaciones" ID="lblTCabinasPax" runat="server" Text="Habitaciones y pasajeros"></asp:Label>                       
                    </div>
                    <br />
                    <div class="ancho47 borderRight" style="margin-left: -100px;">
                        <div class="ancho100">
                            <asp:Label CssClass="labelCotizador" ID="lblTCategoria" runat="server" Text="Categoría del hotel "></asp:Label>

                            <asp:DropDownList CssClass="datoCotizador" ID="ddlCategoria" runat="server">
                            </asp:DropDownList>
                        </div>

                        <div class="ancho100">
                            <asp:Label CssClass="labelCotizador" ID="lblTFecha" runat="server" Text="Fecha de viaje"></asp:Label>
                            <asp:TextBox ID="txtFecha" runat="server" style="height: 10px; margin-top: 0px; margin-left: 10px; width: 113px;" CssClass="datoCotizador"></asp:TextBox>
                        </div>

                        <div class="ancho100">
                            <asp:Label CssClass="labelCotizador" ID="lblTDuracionRot" runat="server" Text="Tiempo de estadía"></asp:Label>
                            
                            <asp:DropDownList CssClass="datoCotizador" ID="ddlDuracion" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                    <div class="ancho47">
                        <div class="noneDisplay">
                            <asp:Label CssClass="labelCotizador" ID="lblTMoneda" runat="server" Text="Tarifas por habitación en "></asp:Label>       
                            <asp:Label ID="lblMoneda" runat="server" CssClass="datoCotizador" Text=""></asp:Label>
                        </div>

                        <div class="ancho100">
                            
                        </div>


                        <div class="ancho100" style="margin-left: 208px;">
                            <asp:Label CssClass="labelCotizador" ID="lblTCabinas" runat="server" Text="Número de Habitaciones"></asp:Label>

                            <asp:DropDownList ID="ddlCabinas" runat="server" CssClass="datoCotizador listaCotizador" AutoPostBack="True" OnSelectedIndexChanged="ddlCabinas_SelectedIndexChanged">
                                <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                <asp:ListItem Text="04" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="ancho100">                            
                            <div style="width:143%; float:left; font-size:9px; margin-left: -13px;">
                                <asp:Repeater ID="rptPasajeros" runat="server">                                                                        
                                    <ItemTemplate>
                                        <div class="borderCotizador">
                                            <div class="ancho50" style="display:none">
                                                <asp:TextBox ID="txtNumRealAdt" runat="server" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtNumRealCnn" runat="server" Visible="false"></asp:TextBox>  
                                                <asp:Label ID="lblTNoCabina" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Cabina") %>'></asp:Label>
                                            </div>

                                            <div class="ancho50" style="width:63%;">
                                                <div class="labelCotizador">
                                                    <asp:Label ID="lblTAdultos" runat="server" Text="Adultos"></asp:Label>
                                                    <asp:Label ID="lblEdadAdt" runat="server" Text=""></asp:Label>
                                                </div>
                                                
                                                <asp:DropDownList ID="ddlAdultos" runat="server" CssClass="datoCotizador listaCotizador">
                                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                    <%--<asp:ListItem Text="05" Value="5"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="ancho50" style="display:none;">
                                                <asp:Label CssClass="bold" ID="lblTJuniors" runat="server" Text="Junior "></asp:Label>
                                                <asp:Label CssClass="bold" ID="Label13" runat="server" Text="(12 a 17 Años)"></asp:Label>
                                            
                                                <asp:DropDownList ID="ddlJuniors" runat="server">
                                                    <asp:ListItem Text="00" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                </asp:DropDownList>                                            
                                            </div>

                                            <div class="ancho50" style="margin-left: 214px; width: 69%;">
                                                <div class="labelCotizador" style="padding-left:10px;">
                                                    <asp:Label ID="lblTNinos" runat="server" Text="Menores "></asp:Label>
                                                    <asp:Label ID="lblEdadCnn" runat="server" Text=""></asp:Label>
                                                </div>
                                                
                                                <asp:DropDownList CssClass="datoCotizador listaCotizador" ID="ddlNinos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNinos_Selected">
                                                    <asp:ListItem Text="00" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                </asp:DropDownList><br />
                                                <div class="contenedoredad_niños" style="margin-left: 69px; margin-top: 21px;">
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
                        <div class="contenedorBotonBuscar">
                        <asp:Button class="link-button white" ID="btnCotizar" runat="server" Text="Cotizar" OnCommand="btn_Command" CommandName="Cotizar" /> 
                        </div>
                    </div>

                    <asp:Label ID="lblErrorGen" runat="server"  ForeColor="#84021C"></asp:Label>                    
                </div>
                        
                <div class="cotizadorPlan">
                    <asp:Repeater ID="rptCabina" runat="server">
                        <ItemTemplate>
                            <div class="full azulOscuro iblock bold textLeft">
                                <asp:Label ID="lblTCabina" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NoCabina") %>'></asp:Label>
                            </div>

                            <div class="full grisOscuro iblock bold textLeft">
                                <asp:Label ID="lblTTarifasPersona" runat="server" Text="Tarifas por habitación en "></asp:Label>
                                <asp:Label ID="lblMonedaTarifa" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strMoneda") %>'></asp:Label>
                                &nbsp;&nbsp;/&nbsp;&nbsp;
                                <asp:Label ID="lblTDesde" runat="server" Text="Vigentes desde: "></asp:Label>
                                <asp:Label ID="lblDesde" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dtmDesdeFecha") %>'></asp:Label>
                                <asp:Label ID="lblTHasta" runat="server" Text=" hasta: "></asp:Label>
                                <asp:Label ID="lblHasta" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dtmHastaFecha") %>'></asp:Label>
                                &nbsp;&nbsp;/&nbsp;&nbsp;
                                <asp:Label ID="lblTDuracion" runat="server" Text="Duración: "></asp:Label>
                                <asp:Label ID="lblDias" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intDias") %>'></asp:Label>
                                <asp:Label ID="lblTDias" runat="server" Text=" dias / "></asp:Label>
                                <asp:Label ID="lblNoches" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intNoches") %>'></asp:Label>
                                <asp:Label ID="lblTNoches" runat="server" Text=" noches"></asp:Label>
                            </div>

                            <div class="iblock azulClaro full textLeft">
                                <asp:Label CssClass="bold" ID="lblTAcomodacion" runat="server" Text="Acomodación: "></asp:Label>
                                <asp:Label ID="lblAcomodacion" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strAcomodacion") %>'></asp:Label>
                            </div>

                            <%--<div style="width:100%; text-align:center; display:inline-block; padding:15px 0;">                           
                                <div style="display: inline-block; margin:0 auto;">
                                    <a href="https://livechat.boldchat.com/aid/6204590666656341798/bc.chat?cwdid=5726033846606487700" target="_blank" onclick="window.open((window.pageViewer && pageViewer.link || function(link){return link;})(this.href + (this.href.indexOf('?')>=0 ? '&amp;' : '?') + 'url=' + escape(document.location.href)), 'Chat1832357175445864370', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=640,height=480');return false;">
                                        <script language="JavaScript" type="text/javascript"></script>
                                        <img alt="" src="../App_Themes/Imagenes/btn_asesor.png" />
                                    </a>

                                    <a href="../Presentacion/Contactenos.aspx" style="display:none;">
                                        <img alt="" src="../App_Themes/Imagenes/TopFlight/btn_mensaje.png"/>
                                    </a>
                                </div>
                            </div>--%>

                            <div class="contenedorCotizador">
                                <asp:Repeater ID="rptHoteles" runat="server">
                                    <HeaderTemplate>
                                         <div class="filaCotizador">  
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador celda15">
                                                <asp:Label ID="Label9" runat="server" Text="Hotel"></asp:Label>
                                            </div>
                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador celda15">
                                                <asp:Label ID="Label10" runat="server" Text="Tipo habitación"></asp:Label>
                                            </div>
                                            
                                            <div class="celdaBaseCotizador tituloCotizador bordeTopCotizador celda10">
                                                <asp:Label ID="Label4" runat="server" Text="Alimentación"></asp:Label>
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
                                        <div class="filaCotizador" style="width: 103%;">
                                            <div class="celdaBaseCotizador celda15">
                                                <asp:Label ID="lblIdCategoria" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.idCategoria") %>'></asp:Label>
                                                <asp:Label ID="lblIdHotel" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intProveedor") %>'></asp:Label>
                                                <asp:Label ID="lblNombreHotel" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strNombre") %>'></asp:Label>
                                                <a href='#this' title='<%# DataBinder.Eval(Container,"DataItem.strHoteles") %>' onclick='javascript:SetIdHotelRotMulti("<%# DataBinder.Eval(Container.DataItem, "idcategoria")%>", "<%# DataBinder.Eval(Container.DataItem, "intidPlan")%>");' >
                                                    <%# DataBinder.Eval(Container.DataItem, "strNombre")%>
                                                </a>
                                                <asp:Label ID="lblHoteles" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strHoteles") %>'></asp:Label>
                                            </div>
                                            <div style="float:left; width: 805px;">
                                                <asp:Repeater ID="rptTarifas" runat="server">
                                                    <ItemTemplate>
                                                        <div class="filaCotizador" style="border-bottom: none;">
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
                                                                <asp:RadioButton ID="rbTarifa" runat="server" OnCheckedChanged="rbTarifa_CheckedChanged" AutoPostBack="true" />
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
                            <div style="float: left; position: relative;display:none;">
                                <input id="btn1" type="button" class="botonEnviarAmigo" runat="server" value="" onclick='javascript:popUp();'/>                                        
                                <input id="btnImprimir" type="button" Class="botonImprimirPlan" value=""   onclick='window.print();return false' />    
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                    <div class="contenedorBotonBuscar" style="float: right; position: relative;">
                        <asp:Button CssClass="link-button florig" ID="btnReservar" runat="server" Text="Reservar" OnCommand="btn_Command"
                            CommandName="Reservar"  Visible="false" style="height: 32px;"/>

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
  