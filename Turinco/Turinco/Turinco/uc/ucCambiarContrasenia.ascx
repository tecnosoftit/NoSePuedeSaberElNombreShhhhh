<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCambiarContrasenia.ascx.cs" Inherits="uc_ucCambiarContrasenia" %>

<table width="100%" cellpadding="3">
    <tr>
        <td style="width: 142px">
            <strong>
                <span>
                    <asp:Label ID="lblTEmail" runat="server" Text="E-mail"></asp:Label>                                  
                </span>                        
            </strong>
        </td>

        <td style="width: 183px">
            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True" Width="160px"></asp:TextBox>            
        </td>

        <td style="width: 142px">
            <strong>
                <span>
                    
                </span>
            </strong>
        </td>

        <td style="width: 183px">           
        </td>
    </tr>

    <tr>
        <td style="width: 142px">
            <strong>
                <asp:Label ID="lblTPassword" runat="server" Text="Contraseña"></asp:Label>                      
            </strong>
        </td>

        <td style="width: 183px">            
            <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password"  Width="160px"></asp:TextBox>             
        </td>

        <td style="width: 142px">
            <strong>
                <asp:Label ID="lblTConfirmar1" runat="server" Text="Confirmar &nbsp;"></asp:Label>
                <asp:Label ID="lblTConfirmar2" runat="server" Text="contraseña"></asp:Label>                
            </strong>
        </td>

        <td style="width: 183px">
            <asp:TextBox ID="txtConfContrasena" runat="server" TextMode="Password" Width="160px"></asp:TextBox>           
        </td>
    </tr>

    <tr>
        <td colspan="3">
        </td>

        <td class="ContenedorBotonMicuenta">
            <asp:Button ID="btnGuardarContrasena" runat="server" Text="Guardar" CssClass="link-button florig" CommandName="Actualizar" OnCommand="setCommand" />
        </td>
    </tr>

    <tr>
        <td colspan="4">
            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="black"></asp:Label>
        </td>
    </tr>
</table>
