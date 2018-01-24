<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMenuSuperior.ascx.cs" Inherits="uc_ucMenuSuperior" %>
<%--
<div class="menuPrincipal">
    <ul class="dropdown">
        <li><a href="../Presentacion/index.aspx?idSesion=<%=Request.QueryString["idSesion"] %>"><asp:Label ID="lblTInicio" runat="server" Text="INICIO"></asp:Label></a></li>
        <li><a href="../Presentacion/ResultadoOfertas.aspx?idSesion=<%=Request.QueryString["idSesion"] %>"><asp:Label ID="lblTMediosPago" runat="server" Text="OFERTAS"></asp:Label></a></li>
        <li><a href="../Presentacion/ResultadoPlanes.aspx?idSesion=<%=Request.QueryString["idSesion"] %>"><asp:Label ID="lblTCarro" runat="server" Text="DESTINOS Y PLANES TURISTICOS"></asp:Label></a></li>
        <li><a href="../Presentacion/Blog.aspx?idSesion=<%=Request.QueryString["idSesion"] %>"><asp:Label ID="lblTBlog" runat="server" Text="GRUPOS"></asp:Label></a></li>
        <li><a href="../Presentacion/MiCuenta.aspx?idSesion=<%=Request.QueryString["idSesion"] %>"><asp:Label ID="lblTMiCuenta" runat="server" Text="RECOMENDACIONES PARA EL VIAJERO"></asp:Label></a></li>
        <li><a href="../Presentacion/Blog.aspx?idSesion=<%=Request.QueryString["idSesion"] %>"><asp:Label ID="lblTContactenos" runat="server" Text="BLOG"></asp:Label></a></li>
        <li style="border:0;"><a href="../Presentacion/Contactenos.aspx?idSesion=<%=Request.QueryString["idSesion"] %>"><asp:Label ID="Label1" runat="server" Text="CONTÁCTENOS"></asp:Label></a></li>
    </ul>
</div>
--%>

<div runat="server" id="divMenu">
    <ul id="nav" class="list-buttons sup2">
        <li class="current-menu-item" id="id0">
            <a href="../Presentacion/index.aspx" onclick="GuardarSession();" class="link-button blue">
                <span>
                    Inicio
                </span>
            </a>            
        </li>
        <li id="id1">
            <a href="../Presentacion/planes.aspx?tipo=PLN" onclick="GuardarSession();" class="link-button blue">
                <span>
                    Paquetes
                </span>
            </a>
        </li>
        <li id="id2">
            <a href="../Presentacion/Cruceros.aspx?tipo=PLN" onclick="GuardarSession();" class="link-button blue">
                <span>
                    Cruceros
                </span>
            </a>
        </li>
        <li id="id3">
            <a href="../Presentacion/Ofertas.aspx?tipo=OFR" onclick="GuardarSession();" class="link-button blue">
                <span>
                    Ofertas
                </span>
            </a>
        </li>
        <li id="id4">
            <a href="../Presentacion/QuienesSomos.aspx" onclick="GuardarSession();" class="link-button blue">
                <span>
                    Quienes Somos
                </span>
            </a>
        </li>
        <li id="id5">
            <a href="../Presentacion/Utilitarios.aspx" onclick="GuardarSession();" class="link-button blue">
                <span>
                    Flyers
                </span>
            </a>
        </li>
        <%--<li class="iblock" id="id6">
            <a href="../Presentacion/ResultadoPlanes.aspx?ORIGEN=BUSCADOR&PaQ=1" onclick="GuardarSession();" class="iblock">
                <span>    
                    PAQUETES
                </span>
            </a>
        </li>
        <li class="iblock" id="id7">
            <a href="../Presentacion/Seguros.aspx" onclick="GuardarSession();" class="iblock">
                <span>
                    SEGUROS
                </span>
            </a>
        </li>--%>
    </ul>
</div>
