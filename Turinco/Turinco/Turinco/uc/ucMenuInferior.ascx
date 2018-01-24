<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMenuInferior.ascx.cs" Inherits="uc_ucMenuInferior" %>

<ul class="list-buttons sup2">
    <li id="menu1" class="current-menu-item">
        <a class="link-button blue" href="../Presentacion/QuienesSomos.aspx" onclick="GuardarSession()">
            <span>
                Inicio        
            </span>
        </a>
    </li>
    <li id="menu2">
        <a href="../Presentacion/contactenos.aspx" onclick="GuardarSession()" class="link-button blue">
            <span>
                Planes
            </span>
        </a>
    </li>
    <li id="menu3">
        <a href="../Presentacion/DestinosRecomendados.aspx" onclick="GuardarSession()" class="link-button blue">
            <span>
                Cruceros
            </span>
        </a>
    </li>
    <li id="menu4">
        <a href="../Presentacion/Vuelos.aspx" onclick="GuardarSession()" class="link-button blue">
            <span>
                Ofertas
            </span>
        </a>
    </li>
    <li id="menu5">
        <a href='http://www.booking.com/index.html?aid=355365' target="_blank" onclick="GuardarSession()" class="link-button blue">
            <span>
                Quienes Somos
            </span>
        </a>
    </li>
    <li id="Li4">
        <a href="../Presentacion/planes.aspx?tipo=OFR" onclick="GuardarSession();" class="link-button blue">
            <span>
                Utilitarios
            </span>
        </a>
    </li>
    <%--<li id="menu6" class="iblock">
        <a href="../Presentacion/ResultadoPlanes.aspx?ORIGEN=BUSCADOR" onclick="GuardarSession()">
            <span>
                PAQUETES
            </span>
        </a>
    </li>
    <li id="menu7" class="iblock">
        <a href="../Presentacion/Seguros.aspx" onclick="GuardarSession()">
            <span>
                SEGUROS
            </span>
        </a>
    </li>--%>
</ul>
