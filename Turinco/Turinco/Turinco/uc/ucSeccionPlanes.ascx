<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSeccionPlanes.ascx.cs" Inherits="uc_ucSeccionPlanes" %>

<%@ Register src="ucSeccionExperiencias.ascx" tagname="ucSeccionExperiencias" tagprefix="uc1" %>
<%--
<div class="box_side" style="width:100%;">
    <div class=" cabezote">
        <h4 class="white bk_vin">Top oferta planes</h4>
    </div>
    
    <ul class="inner_box_side">
        <li class="table_ t_center">
            <h5 class="vino">Destino</h5>
            <span class=" even block t_left">Semana de receso<br> en Santa Marta</span>
            <span class=" odd block t_left">Semana de receso<br> en Santa Marta</span>
        </li>
        
        <li class="table_ t_center">
            <h5 class="vino">Duración</h5>
            <span class="even block">
                <span class="block">16 días</span>
            </span>
            <span class="odd block">
                <span class="block">5 días</span>
            </span>
        </li>
        <li class="table_ t_center">
            <h5 class="vino">Ranking</h5>
            <span class="even block">
                <div id="star1" class="rating">&nbsp;</div>
            </span>
            <span class="odd block">
                <div id="star2" class="rating">&nbsp;</div>
            </span>
        </li>
        <li class="table_ t_center">
            <h5 class="vino">Precio desde</h5>
            <span class="even block">COP $235.000</span>
            <span class="odd block">COP $235.000</span>
        </li>
    </ul>     
</div> --%>

<div class="box_side">
    <uc1:ucSeccionExperiencias ID="ucSeccionExperiencias1" runat="server" />
</div>