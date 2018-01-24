<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPopoupMensaje.ascx.cs"
    Inherits="uc_ucPopupMensaje" %>
<ajax:ModalPopupExtender ID="MPEMensaje" runat="server" TargetControlID="popdummylinkopen"
    BackgroundCssClass="ui-widget-shadow"  DropShadow="false" EnableViewState="true"
    CancelControlID="popdummylinkclose" PopupControlID="pnlPopupMensaje">
</ajax:ModalPopupExtender>
<asp:Panel runat="server" ID="pnlPopupMensaje">
    <div class="ventanaMensaje">
        <table width="100%" cellpadding="3">
            <tr>
                <td style="text-align: center;">
                    <img alt="" src="../App_Themes/Imagenes/logo.png" />
                </td>
            </tr>
            <tr>
                <td style="color: #999; font-size: 14px; text-align:center">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="text-align: center;">
                <td>
                    <asp:LinkButton CssClass="linkMensaje" ID="lnkIndex" runat="server" OnClick="lnkIndex_Click">Ir a inicio</asp:LinkButton>
                </td>
            </tr> 
        </table>
    </div>
    <a href="#" id="popdummylinkclose" style="display: none" runat="server"></a>
</asp:Panel>
<a href="#" id="popdummylinkopen" style="display: none" runat="server"></a>