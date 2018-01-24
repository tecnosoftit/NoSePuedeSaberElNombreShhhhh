<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDetalleExcursion.ascx.cs"
    Inherits="uc_ucDetalleExcursion" %>

<%@ Register src="ucTarifasExcursiones.ascx" tagname="ucTarifasExcursiones" tagprefix="uc1" %>


<div class="detallePlan">

    <div class="contenidoPlan">
        <div class="tituloPlan">
            <asp:Label ID="strNombrePlan" runat="server" Text=""></asp:Label>
        </div>

        <div class="precioPlanes">
            <asp:Label ID="strTarifaCuotas" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblTDesde" runat="server" Text="desde"></asp:Label>&nbsp;
            <asp:Label ID="strRefereTipoMoneda" runat="server" Text=""></asp:Label>&nbsp;
            <asp:Label ID="dblPrecio" runat="server" Text=""></asp:Label>&nbsp;  
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
    <div class="galeriaPlan">
        <asp:Repeater ID="rptGaleria" runat="server">
            <ItemTemplate>
                <div class="foto">
                    <a style="text-decoration: none;" href='<%# DataBinder.Eval(Container,"DataItem.strNombreImagen") %>'>
                        <asp:Image runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.strNombreImagen") %>' ID="imgGal" />
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="imagenPlan">
        <asp:Image CssClass="largeImg" ID="strImagen" runat="server" />
    </div>

</div>
<div class="panelCompleto">
    <asp:Label ID="lblDescCarro" runat="server" Text=""></asp:Label>
   &nbsp;<uc1:ucTarifasExcursiones ID="ucTarifasExcursiones1" runat="server" />
</div>