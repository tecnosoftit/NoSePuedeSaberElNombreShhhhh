<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucComentarios.ascx.cs" Inherits="uc_ucComentarios" %>
<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
        <asp:Repeater runat="server" ID="rptSeccion">
            <ItemTemplate>
                <asp:Repeater runat="server" ID="rptSeccion">
                    <ItemTemplate>
                        <div style="float:right; position:relative; width:410px; padding:15px; border-bottom:dashed 1px #CCC; background-color:#EAEAEA">
                            <asp:Label CssClass="boldTerminos" ID="lbltitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>
                            <br />
                            <asp:Label CssClass="textoTerminos" ID="strDescripcion" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strDescripcion") %> </asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
</asp:Repeater>