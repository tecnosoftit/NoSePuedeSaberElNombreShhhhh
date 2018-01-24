<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucReservasHistoricas.ascx.cs" Inherits="uc_ucReservasHistoricas" %>

<table width="100%" cellpadding="2" cellspacing="0" style="font-size: 11px;" class="example tablasMiCuenta">
    <asp:Repeater ID="rptHistoricas" runat="server" OnItemCommand="rptVigentes_ItemCommand">
        <HeaderTemplate>
            <thead>
                <tr class="t_center">
                    <th>
                    </th>
                    
                    <th>
                        Loc.
                    </th>
                    
                    <th>
                        Récord
                    </th>
                    
                    <th>
                        Tipo de servicio
                    </th>
                    
                    <th>
                        Fecha de viaje
                    </th>
                    
                    <th>
                        Estado
                    </th>
                    
                    <th>
                        Nombre de pasajero
                    </th>
                </tr>
            </thead>
            
            <tbody>
        </HeaderTemplate>
        
        <ItemTemplate>
            <tr class="t_center">
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "intProyecto") %>' Text="Ver reserva" Visible="true"></asp:LinkButton>
                </td>
                
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "intProyecto") %>
                </td>
                
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "strCodigoReserva") %>
                </td>
                
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "strTipoServicio") %>
                </td>
                
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "dtmFechaIni") %>
                </td>
                
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "strEstado") %>
                </td>
                
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "NombreUsuario") %>
                </td>
                
            </tr>
        </ItemTemplate>
        
        <FooterTemplate>
            </tbody>
            <tfoot>
            </tfoot>
        </FooterTemplate>
    </asp:Repeater>
</table>
