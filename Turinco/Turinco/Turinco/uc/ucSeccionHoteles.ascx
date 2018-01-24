<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSeccionHoteles.ascx.cs" Inherits="uc_ucSeccionHoteles" %>

<%@ Register src="ucSeccionExperiencias.ascx" tagname="ucSeccionExperiencias" tagprefix="uc1" %>

<div class="box_side" style="width:100%;"><!--inicio box_side-->
    <div class=" cabezote">
        <h4 class="white bk_vin">Top oferta hoteles</h4>
    </div>
    <ul class="inner_box_side">
        <li class="table_ t_center">
            <h5 class="vino">Hotel</h5>
            <span class="even block">Marriot</span>
            <span class="odd block">Marriot</span>
        </li>
        <li class="table_ t_center">
            <h5 class="vino"> Ciudad </h5>
            <span class="even block">
                <span class="block">Bogotá</span>
            </span>
            <span class="odd block">
                <span class="block">Bogotá</span>
            </span>
        </li>
        <li class="table_ t_center">
            <h5 class="vino">Acomodación</h5>
            <span class="even block">
                <span class="block">Doble</span>
            </span>
            <span class="odd block">
                <span class="block">Doble</span>
            </span>
        </li>
        <li class="table_ t_center">
            <h5 class="vino">Precio por noche</h5>
            <span class="even block">COP $235.000</span>
            <span class="odd block">COP $235.000</span>
        </li>
    </ul>      
</div><!-- fin box_side -->

<div class="box_side"><!--inicio box_side-->
    <uc1:ucSeccionExperiencias ID="ucSeccionExperiencias1" runat="server" />
</div><!-- fin box_side -->