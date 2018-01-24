<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDefault.ascx.cs" Inherits="uc_ucDefault" %>
<div id="boxLogin">
    <!--- box border -->
    <div id="lb">
    <div id="rb">
    <div id="bb"><div id="blc"><div id="brc">
    <div id="tb"><div id="tlc"><div id="trc">
    
    <div class="content">
<%--        <div class="ingresoLogin">
            <div class="subtitulosLogin">
                <asp:Label ID="Label4" runat="server" Text="Empresa"></asp:Label>
            </div>
            <div class="datos">
                <asp:DropDownList ID="ddlAplicacion" runat="server" Width="205px">
                </asp:DropDownList>
            </div>
        </div>--%>
        
        <div class="ingresoLogin">
            <div class="subtitulosLogin">
                <asp:Label ID="Label5" runat="server" Text="E-mail"></asp:Label>
            </div>
            <div class="datos">
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="formaBuscador" Width="200"></asp:TextBox>
            </div>
        </div>
        
        <div class="ingresoLogin">
            <div class="subtitulosLogin">
                <asp:Label ID="Label6" runat="server" Text="Password"></asp:Label>
            </div>
            <div class="datos">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="formaBuscador" TextMode="Password" Width="200"></asp:TextBox>
            </div>
        </div>
        
        <div class="botonIngresar">
            <asp:Button ID="btnEntrar" CssClass="boton" runat="server" OnClick="btnEntrar_Click" Text="Ingresar" />
        </div>
        
        <div class="botonIngresar">            
            <asp:LinkButton ID="lbtnRecordarContrasena" runat="server" CssClass="olvido" OnClick="lbtnRecordarContrasena_Click">¿Olvido su contraseña?</asp:LinkButton>
        </div>
        
        <div class="errorLogin">
            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
        </div>
    </div>
        
    <!--- end of box border -->
    </div></div></div></div>
    </div></div></div></div>
</div>
