<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDetalleTour.ascx.cs" Inherits="uc_DetalleTour" %>

<%@ Register Src="ucTarifasToures.ascx" TagName="ucTarifasCircuitos" TagPrefix="uc4" %>

<div class="detallePlan">

    <div class="contenidoPlan">
        <div class="tituloPlan">
            <asp:Label ID="strNombrePlan" runat="server" Text=""></asp:Label>
            <%--  <asp:Image ID="Image1" runat="server" Width="573" Height="150" />--%>
        </div>

        <div class="precioPlanes">
            <asp:Label ID="strTarifaCuotas" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblTDesde" runat="server" Text="desde"></asp:Label>&nbsp;
            <asp:Label ID="strRefereTipoMoneda" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strRefereTipoMoneda") %>'></asp:Label>&nbsp;
            <asp:Label ID="dblPrecio" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblPrecio") %>'></asp:Label>&nbsp;  
            <br />
            <asp:Label CssClass="referencia" ID="strTarifaReferencia" runat="server" Text=""></asp:Label>        
        </div>

        <asp:Label ID="lblurl" runat="server"></asp:Label>

        <div class="condiciones">
            <asp:Label ID="strDescripcion" runat="server"></asp:Label>

            <div class="items">
                <asp:Label CssClass="subtituloPlan" ID="lblTQueIncluye" runat="server" Text="Incluye"></asp:Label><br />
                <asp:Image ID="imgCategoria" runat="server" />
                <asp:Label ID="strIncluye" runat="server"></asp:Label>
            </div>
                    
            <div class="items">
                <asp:Label CssClass="subtituloPlan" ID="lblTQueNoIncluye" runat="server" Text="No incluye"></asp:Label><br />
                <asp:Label ID="strNoIncluye" runat="server"></asp:Label>
            </div>
                    
            <div class="items">
                <asp:Label CssClass="subtituloPlan" ID="lblTEnCuenta" runat="server" Text="Tenga en cuenta"></asp:Label><br />
                <asp:Label ID="strEncuenta" runat="server" Text="Label"></asp:Label>&nbsp;
            </div>
                    
            <div class="items">
                <asp:Label CssClass="subtituloPlan" ID="lblTCondiciones" runat="server" Text="Condiciones de Reserva"></asp:Label><br />
                <asp:Label ID="strRestriccion" runat="server"></asp:Label>&nbsp;
                        
                <p style="text-align: right" onclick="EsconderDetalle(getElementById('VerMas').id,this.children[0]);">
                    <asp:Label ID="lblVerMas" CssClass="verMas" runat="server" Text="Ver más"></asp:Label>
                </p>
                        
                <div id="VerMas" style="display: none">
                    <p>
                        <asp:Label ID="lblMas" runat="server" Text="No hay mas información"></asp:Label>
                    </p>
                </div>
            </div>
        </div>

    </div>

    <%--<div class="galeriaPlan">
        <asp:Repeater ID="rptGaleria" runat="server">
            <ItemTemplate>
                <div class="foto">
                    <a style="text-decoration: none;" href='<%# DataBinder.Eval(Container,"DataItem.strNombreImagen") %>'>
                        <asp:Image runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.strNombreImagen") %>' ID="imgGal" />
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>--%>

    <div class="imagenPlan">
        <asp:Image CssClass="largeImg" ID="strImagen" runat="server" />
    </div>

</div>

<!-- opciones planes -->
<div class="compartirPlan">
    <%--<input id="btnMeGusta" type="button" class="botonMeGusta" runat="server" />--%>
   <%-- <asp:Button ID="btnImprimir" Visible="false" CssClass="botonImprimir" runat="server" CommandName="PrintDesc" OnCommand="btn_Command" />
    <asp:Button ID="btnEnviar" Visible="false" CssClass="botonEnviar" runat="server" />
    <asp:Button ID="btnItinerario" CssClass="botonItinerario" runat="server" />
    <a href="https://livechat.boldchat.com/aid/6204590666656341798/bc.chat?cwdid=5726033846606487700" target="_blank" onclick="window.open((window.pageViewer && pageViewer.link || function(link){return link;})(this.href + (this.href.indexOf('?')>=0 ? '&amp;' : '?') + 'url=' + escape(document.location.href)), 'Chat1832357175445864370', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=640,height=480');return false;">
        <script language="JavaScript" type="text/javascript"></script>
        <asp:Button ID="btnChat" CssClass="botonChat" runat="server" />     
    </a> --%>
</div>

<div class="panelCompleto">
    <asp:Label ID="lblDescCarro" runat="server" Text=""></asp:Label>
    <!-- tarifas circuitos normal -->
    <asp:Panel ID="pSalidas" runat="server">
        <uc4:ucTarifasCircuitos ID="UcTarifasCircuitos1" runat="server" />
    </asp:Panel>
    
  
    <div style="text-align: center">
        <asp:Label ID="lblError" runat="server" ForeColor="Maroon"></asp:Label>
    </div>
</div>
