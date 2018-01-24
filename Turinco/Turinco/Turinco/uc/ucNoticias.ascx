<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNoticias.ascx.cs" Inherits="uc_ucNoticias" %>

<%@ Register Src="ucNoticiaIndex.ascx" TagName="ucNoticiaIndex" TagPrefix="uc1" %>

<div class="panelNoticias">
    <uc1:ucNoticiaIndex ID="ucNoticiaIndex" runat="server" />
</div>
<div class="panelNoticiasResultado">
    <ul id="carruselNoticias" class="jcarousel jcarousel-skin-tango">
        <asp:Repeater runat="server" ID="rptSeccion">
            <ItemTemplate>
                <li>
                    <div class="fotoNoticias">
                        <img alt="" src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen").ToString() %>' />
                    </div>
                    <div class="textoNoticias">
                        <asp:Label CssClass="fechaNoticia" ID="lblFecha" runat="server" Text=""><%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.dtmFecha")).ToString("dd/MMM/yyyy hh:mm")%> </asp:Label>
                        <br />
                        <asp:Label CssClass="tituloNoticias" ID="lbltitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>
                        <br />
                        <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                        
                    </div>
                    <a href='../Presentacion/Noticia.aspx?CODSEC=<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>&TPOMSJ=TM001&TMMSJ=93369'
                        class="botonBusqueda">Ver más
                    </a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
