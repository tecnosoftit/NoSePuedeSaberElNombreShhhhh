<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucIngresoRecordar.ascx.cs"
    Inherits="uc_ucIngreso" %>
<!--- ingresar -->
<div style="float: left; position: relative; width: 100%;">
    <div class="panelIngresarRecordar">
        <!-- masthead -->
            <div id="masthead">
                <span class="head">Recordar Contraseña</span>
            </div>
        <!-- ENDS masthead -->
        <div class="tituloIngresar">
            <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label>
        </div>
        <br />
        <div>
            <p>
                Por favor ingrese el correo con el cual esta inscrito en nuestro programa.
            </p>
        </div>
        <div class="contenidoIngresar1">
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <asp:Repeater runat="server" ID="rptSeccion">
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString()) %></asp:Label>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td class="alineacionDerecha">
                        <asp:Label CssClass="textoLogin" ID="lblTEmail" runat="server" Text="E-mail"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsuario" style="  width: 252px; border-radius: 0px; height: 11px; background: #f1f1f1;" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Validacion_Recordar"
                            ControlToValidate="txtUsuario" runat="server" ErrorMessage="(*)"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <%--<tr>
                    <td class="alineacionDerecha" style="display: none;">
                        <asp:Label CssClass="textoLogin" ID="lblTPassword" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td style="display: none;">
                        <asp:TextBox ID="txtPassword" Width="200" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Validacion_Recordar"
                            ControlToValidate="txtPassword" runat="server" ErrorMessage="(*)"></asp:RequiredFieldValidator>
                    </td>
                </tr>--%>
                <tr>
                    <td class="ContenedorBotonLogin" colspan="2">
                        <asp:Button CssClass="link-button white" ID="lbOlvido" runat="server" CausesValidation="true"
                            OnClick="lbOlvido_Click" Text="Recordar" Style="float: left; margin-left: 371px;"
                            ValidationGroup="Validacion_Recordar"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="alineacionCentro">
                        <asp:Label ID="lblError" CssClass="Error" runat="server" ForeColor="Maroon"></asp:Label>
                        <asp:Label ID="lblContraseña" CssClass="Error" runat="server" ForeColor="Maroon"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="ContenedorBotonLogin" colspan="2" style="display: none;">
                        <%--<asp:Button CssClass="botonLogin" ID="btnEntrar" runat="server" ValidationGroup="Validacion_Recordar"
                            OnClick="btnEntrar_Click" Text="Ingresar"></asp:Button>--%>
                    </td>
                </tr>
            </table>
        </div>
        <div class="RecomendacionesRecordar">
            <h1>
                Tips de Seguridad
            </h1>
            <div>
                <li>No reveles jamas tu contraseña a nadie.</li>
                <li>No utilices la misma contraseña para sitios web distintos.</li>
                <li>Crea contraseñas que tengan al menos 8 caracteres.</li>
                <li>Incluye números, mayúsculas y símbolos.</li>
            </div>
        </div>
    </div>
</div>
