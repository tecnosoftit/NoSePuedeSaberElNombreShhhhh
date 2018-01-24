<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPreguntasFrecuentes.ascx.cs"
    Inherits="uc_ucPreguntasFrecuentes" %>

<asp:Repeater ID="rptSeccion" runat="server">
    <ItemTemplate>
        <asp:Repeater ID="rptSeccion" runat="server">
            <ItemTemplate>
                <div class="preguntas">
                    <h4 class="trigger">
                        <asp:Label ID="Label1" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "strTitulo") %>'></asp:Label>
                    </h4>
                    <div class="toggle_container">
                        <asp:Label ID="Label2" runat="server" Text=' <%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
</asp:Repeater>