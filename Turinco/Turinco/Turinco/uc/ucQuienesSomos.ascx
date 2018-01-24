<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucQuienesSomos.ascx.cs" Inherits="uc_ucQuienesSomos" %>
 <div id="masthead">
		    <span class="head">Nuestra Empresa</span>
	    </div>
<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
       
        <div style="padding-left: 25px; margin-top: 25px;">
       
            <img id="imgQuienes" style="display:none;" runat="server" alt='<%# DataBinder.Eval(Container.DataItem, "strTitulo") %>' src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen").ToString() %>' class="full iblock" />
            <h4>
                <asp:Label ID="strTitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>
            </h4>
            <br />
                <div class="contenedorQuienes">
                        <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                    <asp:Repeater runat="server" ID="rptSeccion">
                        <ItemTemplate>
                            <%--<asp:Label CssClass="tituloQuienes" ID="strTitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>--%>
                            <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                        </ItemTemplate>
                    </asp:Repeater>  
                </div>              
        </div>
    </ItemTemplate>
</asp:Repeater>
