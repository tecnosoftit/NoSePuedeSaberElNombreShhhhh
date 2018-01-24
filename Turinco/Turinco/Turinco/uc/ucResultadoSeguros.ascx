<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucResultadoSeguros.ascx.cs" Inherits="uc_ucResultadoSeguros" %>
<%@ Register Src="../uc/ucBuscadorSeguro.ascx" TagName="ucBuscadorSeguro" TagPrefix="uc1" %>

<asp:Label ID="Label1" runat="server" Text="Resumen de Búsqueda" CssClass="tituloSeguros arial"></asp:Label>

<div class="descripcionSeguros verdana">    
    <div class="resumenSeguros">
        <span class="tituloResumen">
            <asp:Label ID="lblTPais" runat="server" Text="Destino: "></asp:Label><br />
        </span>
        <asp:Label ID="lblDestino" runat="server"></asp:Label>
    </div>
    <div class="resumenSeguros">
        <span class="tituloResumen">
            <asp:Label ID="lblTFecRecoge" runat="server" Text="Fecha de salida: "></asp:Label><br />
        </span>
        <asp:Label ID="lblFechaSalida" runat="server" Text=""></asp:Label>
    </div>
    <div class="resumenSeguros">
        <span class="tituloResumen">
            <asp:Label ID="Label7" runat="server" Text="Fecha de regreso: "></asp:Label><br />
        </span>
        <asp:Label ID="lblFechaRegreso" runat="server" Text=""></asp:Label>
    </div>
    <div class="resumenSeguros">
        <span class="tituloResumen">
            <asp:Label ID="lblTCiudad" runat="server" Text="Cantidad de personas: "></asp:Label><br />
        </span>
        <asp:Label ID="lblPersonas" runat="server"></asp:Label>
    </div>
</div>

<asp:Repeater ID="dtlOfertas" runat="server">
    <ItemTemplate>
        <a href='#' onclick="Show_Cortinilla_Interna()" class="tituloSeguros arial">
            <%# DataBinder.Eval(Container,"DataItem.Nombre") %>
        </a>

        <div class="descripcionResultadoSeguro full">
            <a href='#' onclick="Show_Cortinilla();" class="iblock noneDisplay">
                <asp:Image ID="Image1" class="imagen" runat="server" AlternateText='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>'
                    ToolTip='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>' ImageUrl='<%# DataBinder.Eval(Container,"DataItem.strImagen") %>' />
            </a>

            <div class="detalle">
                <%-- <asp:Label ID="lblAcom" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strTarifaReferencia").ToString()) %>'></asp:Label>--%>
                <span class="noneDisplay">
                    <asp:Label ID="lblCant1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intDias") %>'></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblCant1T" runat="server" Text="días / "></asp:Label>
                    <asp:Label ID="lblCant2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intNoches") %>'></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblCant2T" runat="server" Text="noches"></asp:Label>
                </span>

                <div style="display: none;">
                    <asp:Label ID="lblIdTarifa" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.idTarifa") %>'></asp:Label>
                    <asp:Label ID="lblNombrePlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>'></asp:Label>
                </div> 

                <asp:Label ID="Label1" CssClass="full iblock" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'>
                </asp:Label>

                <div class="full iblock verCobertura">
                    <asp:Button ID="btndetalles" runat="server"  Text="Ver detalles" class="boton" OnClick="btndetalles_Click" />
                </div>
            </div>
            
            <div class="precio">
                <div class="labelValorFinal iblock full">
                    <asp:Label ID="lblTDesde" runat="server" Text="valor"></asp:Label>
                </div>
                <div class="valorFinal iblock full">
                    <asp:Label Visible="false" ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalleClasificacion") %>'></asp:Label>
                    <asp:Label ID="lblPrecioDesde" runat="server" style="display:none;" Text='<%# Decimal.Parse(DataBinder.Eval(Container,"DataItem.dblPrecio").ToString()).ToString("###,###.##")%>'></asp:Label>
                    <asp:Label ID="lblMonedaDesde" CssClass="moneda" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strRefereMoneda") %>'></asp:Label>
                    <asp:Label ID="lblPrecio" CssClass="precio" runat="server" Text='<%# DataBinder.Eval(Container,"Dataitem.precio") %>'></asp:Label>
                </div>
            </div>     
        </div>

        <div class="contenedorReservarSeguro">
            <asp:Button ID="btnReservar" runat="server" Text="RESERVAR" CssClass="boton" OnCommand="btn_Command" CommandName="Reservar" />
        </div>

        <div>            
            <asp:Repeater ID="rptSeguros" runat="server">
                <%--OnItemCommand="dtlOfertas_ItemCommand1"  OnItemDataBound="dlPlanes_ItemDataBound">--%>
                <ItemTemplate>
                    <div class="panelResultados">
                        <!--- box border -->
                        <div class="plb">
                            <div class="prb">
                                <div class="ptb">
                                    <div class="ptlc">
                                        <div class="ptrc">
                                            <div class="contenidoResultados">
                                                <div class="tituloResultados">
                                                    <asp:Label ID="lblIdTarifa" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.idTarifa") %>'></asp:Label>
                                                    <asp:Label ID="lblCodPlan" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>'></asp:Label>
                                                    <%--<asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strNombre") %>'></asp:Label>--%>
                                                </div>
                                                <div class="contenidoResultado">
                                                    <table width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <%-- <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDescripcion") %>'></asp:Label>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <a href='#this' onclick="javascript:SetAbrirMPEE('../Presentacion/Cobertura.aspx?id=<%# DataBinder.Eval(Container.DataItem, "intCodigo")%>');">
                                                                    Ver cobertura</a>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <span style="font-size: 12px; color: #137FC0; font-weight: bold;">
                                                                    <asp:Label ID="Label3" runat="server" Text="Valor "></asp:Label>&nbsp;
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strRefereMoneda") + " " + DataBinder.Eval(Container,"DataItem.dblTotalImpuestosFormato").ToString() %>'></asp:Label>
                                                                </span>&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnReservar" CssClass="botonBuscar" OnClientClick="Show_Cortinilla_Interna();"
                                                                    runat="server" Text="Reservar" OnCommand="btn_Command" CommandName="ReservarCruceros" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="pieResultados">
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div style="display: none;">
                <asp:Image runat="server" Visible="false" ImageUrl="~/App_Themes/Imagenes/ofertaPlan.png"
                    ID="imgOferta" />
                <asp:Image ID="ImageButton2" runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.urlImagenCategoria") %>' />
                <asp:Label ID="lblCodigoPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intcodigo") %>'></asp:Label>
            </div>
        </div>
        <div style="display:none;">
            <asp:Label ID="strNombrePlan" runat="server" Text=""></asp:Label>
            <asp:Label ID="strDescripcion" runat="server"></asp:Label>
           
            <asp:Label ID="strNoIncluye" runat="server"></asp:Label>
            <asp:Label ID="strRestriccion" runat="server"></asp:Label>
        </div>


        <ajax:ModalPopupExtender ID="MPEEGeneral" BackgroundCssClass="ui-widget-shadow" runat="server" TargetControlID="lblincl" OnOkScript="" OkControlID="btnCerrar1"  PopupControlID="PanelComentarios" />
        <asp:Panel runat="server" ID="PanelComentarios" CssClass="contenedorPopUpCondiciones" Style="display: none;">
            <asp:Button ID="btnCerrar1" Text="X" CssClass="btnCerrar azulClaro" runat="server" />

            <div class="condiciones">                
                <asp:Label ID="Label4" CssClass="tituloCondiciones" runat="server" Text="Detalle de cobertura"></asp:Label>
                
                <asp:Label ID="strIncluye" runat="server"></asp:Label>
            </div>
        </asp:Panel>
         <a href='#' id="lblincl"  onclick="return false" runat="server"></a>
    </ItemTemplate>
</asp:Repeater>
   <asp:Label ID="lblerror1" runat="server"></asp:Label>

<!-- resultados -->
<asp:Label ID="lblError" runat="server" Text=""></asp:Label>
