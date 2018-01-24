<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCoorporativo.ascx.cs"
    Inherits="uc_ucCoorporativo" %>

<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
        <asp:Repeater runat="server" ID="rptSeccion">
            <ItemTemplate>
                <div class="corporativo">
                    <div class="imagenCorp">
                        <img width="75" height="75" src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen")%>'
                            alt="" />
                    </div>
                    <div class="textoCorp">
                        <asp:Label CssClass="boldTerminos" ID="lbltitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>
                        <br />
                        <asp:Label CssClass="textoTerminos" ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
</asp:Repeater>
