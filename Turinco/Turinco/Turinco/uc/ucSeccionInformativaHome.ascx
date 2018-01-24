<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSeccionInformativaHome.ascx.cs"
    Inherits="uc_ucSeccionInformativaHome" %>

<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
        <h2 class="h-margin">            
            <%# DataBinder.Eval(Container,"DataItem.strTitulo") %></h2>
        <p>
            <span>
                <%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%>
            </span>
        </p>
    </ItemTemplate>
</asp:Repeater>
