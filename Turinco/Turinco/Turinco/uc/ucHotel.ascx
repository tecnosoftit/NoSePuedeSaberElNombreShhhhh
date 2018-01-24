<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHotel.ascx.cs" Inherits="uc_ucHotel" %>

<table width="100%" border="0" cellspacing="0" cellpadding="3" style="background-color:#FFF; font-size:12px;">
    <tr>
        <td>
            <asp:Label CssClass="bold" ID="lblTCiudad" runat="server" Text="Ciudad: " style="color:#39a8e5; font-size:16px; float:left;display:inline-block;"></asp:Label>
            <asp:Label ID="Ciudad" runat="server" style="color:#39a8e5; font-size:18px; float:left; width:100%;"></asp:Label>
            <br />
            <asp:Label CssClass="bold" ID="lblTCategoria" runat="server" Text="Categoria: " style="color:#39a8e5; font-size:16px; float:left;"></asp:Label>
            <asp:Label ID="Categoria" runat="server" style="color:#39a8e5; font-size:16px; float:left;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <div style="float:left; position:relative; padding:3px; display:none;">
                <img src="" alt="" height="150" width="150" runat="server" id="Imagen" />    
            </div>

            <asp:Label CssClass="bold" ID="Nombre" runat="server" style="color:#39a8e5; font-size:18px; float:left; width:100%; margin-bottom:10px;"></asp:Label>

            <asp:Label ID="Descripcion" runat="server" style="color:#59595b; text-align:justify; font-size:12px;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataList ID="dtlGaleria" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>
                                <img src='<%# DataBinder.Eval(Container,"DataItem.Imagen") %>' alt="" height="110" width="140" runat="server" id="Imagen"/>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
</table>