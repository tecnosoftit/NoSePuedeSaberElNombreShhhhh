<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSeccionExperiencias.ascx.cs"
    Inherits="uc_ucSeccionExperiencias" %>
<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
        <div class=" cabezote">
            <h4 class="white bk_vin">
                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTitulo") %>'></asp:Label>
            </h4>
        </div>
        <div class="inner_box_side">
            <p class="">
               <asp:Label ID="Label2" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString()) %>'></asp:Label>
            </p>
            <ul class="redes t_right">
                <li><a href="" class="inner_tw"></a></li>
                <li><a href="" class="inner_face"></a></li>
                <li><a href="" class="inner_gl"></a></li>
                <li><a href="" class="inner_mail"></a></li>
            </ul>
        </div>
    </ItemTemplate>
</asp:Repeater>
