<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFiltrosdeVuelos.ascx.cs"
    Inherits="uc_ucFiltrosdeVuelos" %>
<div class="filtroResultados filtrosVuelosCont">
    <div class="filtroResultadosTitulo">
        Filtrar por:
    </div>
    <div class="filtroResultadosTitulo" style="display: none;">
        Precio</div>
    <div class="filtroResultadosLista">
        <!--Repetidor de FILTROS-->
        <!--Filtro precio-->
        <div style="display: none;">
            <div class="filtroDetalle">
                Desde:<asp:TextBox ID="txtFrom" runat="server" ToolTip="Precio Desde" Width="60px"></asp:TextBox>
                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtFrom"
                    FilterType="Numbers" />
                Hasta:
                <asp:TextBox ID="txtTo" runat="server" ToolTip="Precio hasta" Width="60px"></asp:TextBox>
                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtTo"
                    FilterType="Numbers" />
                <asp:Button ID="btnSearch" runat="server" class="botonBuscadorFiltro" Text="Buscar"
                    OnClick="btnSearch_Click" />
            </div>
        </div>
        <div class="filtro">
            <div class="filtroTitulo">
                Aerolíneas</div>
            <div class="filtroDetalle">
                <asp:CheckBoxList ID="chkaerolinea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnCheckedChangedMayor">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>
    <div class="bgPieFiltro">
    </div>
</div>
