using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Ssoft.Utils;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace Ssoft.Rules.Hoteles
{
      public class csDisenioHoteles : TemplateControl
    {
        //NOMBRES TABLAS
        private const string TABLA_HOTEL = "Hotel";
        private const string TABLA_TIPOS_HABITACION = "RoomType";
        private const string TABLA_OCUPACION = "Occup";
        private const string TABLA_DISPONIBILIDAD = "Avail";
        private const string TABLA_DESCUENTO = "Discount";
        private const string TABLA_SUPP = "Supp";
        private const string TABLA_DESAYUNO = "Board";
        private const string TABLA_CUARTO = "Room";
        private const string TABLA_NINIO = "Child";
        private const string TABLA_PRECIO = "Price";

        //NOMBRES RELACIONES
        private const string RELACION_HOTEL_ROOM_TYPE = "Hotel_RoomType";
        private const string RELACION_ROOMTYPE_OCCUP = "RoomType_Occup";
        private const string RELACION_ROOMTYPE_AVAIL = "RoomTFype_Avail";
        private const string RELACION_ROOMTYPE_DISCOUNT = "RoomType_Discount";
        private const string RELACION_OCCUP_PRICE = "Occup_Price";
        private const string RELACION_OCCUP_ROOM = "Occup_Room";
        private const string RELACION_OCCUP_BOARD = "Occup_Board";
        private const string RELACION_OCCUP_SUPP = "Occup_Supp";
        private const string RELACION_ROOM_CHILD = "Room_Child";

        private const string FORMATO_NUMEROS = "#,##0.00";

        public csDisenioHoteles()
        {
        }

        public void setRepeater(DataTable dtDatos, Repeater rptRepetidor)
        {
            if (dtDatos != null)
            {
                PagedDataSource objPds = new PagedDataSource();
                objPds.DataSource = dtDatos.DefaultView;
                rptRepetidor.DataSource = objPds;
                rptRepetidor.DataBind();
            }
        }
        public void setRepeater(DataTable dtDatos, Repeater rptRepetidor, string sOrden)
        {
            if (dtDatos != null)
            {
                PagedDataSource objPds = new PagedDataSource();
                dtDatos.DefaultView.Sort = sOrden;
                objPds.DataSource = dtDatos.DefaultView;
                rptRepetidor.DataSource = objPds;
                rptRepetidor.DataBind();
            }
        }
        public void setRepeater(DataRow[] dtDatos, Repeater rptRepetidor)
        {
            if (dtDatos != null)
            {
                rptRepetidor.DataSource = dtDatos;
                rptRepetidor.DataBind();
            }
        }
        public void setDatalist(DataTable dtDatos, DataList dtlDatalist)
        {
            if (dtDatos != null)
            {
                PagedDataSource objPds = new PagedDataSource();
                objPds.DataSource = dtDatos.DefaultView;
                dtlDatalist.DataSource = objPds;
                dtlDatalist.DataBind();
            }
        }

        public void setBookoHotelResultados
            (Label lblResultados,
            Repeater rptHoteles,
            DataSet dsResultados,
            string strWhere)
        {
            if (dsResultados != null)
            {

                DataTable dtHotels = dsResultados.Tables[TABLA_HOTEL];
                DataTable dtOccup = dsResultados.Tables[TABLA_OCUPACION];
                DataTable dtRoom = dsResultados.Tables[TABLA_CUARTO];
                DataTable dtPrice = dsResultados.Tables[TABLA_PRECIO];
                DataTable dtBoard = dsResultados.Tables[TABLA_DESAYUNO];
                DataTable dtRoomType = dsResultados.Tables[TABLA_TIPOS_HABITACION];
                DataTable dtDiscount = dsResultados.Tables[TABLA_DESCUENTO];

                setFiltrarTipoNoRefundable(dtRoomType);

                string sTablaTiposHabitacion = "tablaTiposHabitacion";
                string hotelId = "hotelId";
                string provider = "provider";
                string name = "name";
                string address = "address";
                string category = "category";
                string bestVal = "bestVal";
                string thumb = "thumb";
                string starsLevel = "starsLevel";
                string minAverPrice = "minAverPrice";
                string desc = "desc";
                string location = "location";
                string currency = "currency";
                string brandId = "brandId";
                string Hotel_Id = "Hotel_Id";
                string sDetallesURL = "DetallesURL";

                DataTable tblTiposHabitacion = new DataTable(sTablaTiposHabitacion);
                DataColumn dcHotelId = new DataColumn(hotelId);
                DataColumn dcprovider = new DataColumn(provider);
                DataColumn dcname = new DataColumn(name);
                DataColumn dcaddress = new DataColumn(address);
                DataColumn dccategory = new DataColumn(category);
                DataColumn dcbestVal = new DataColumn(bestVal);
                DataColumn dcthumb = new DataColumn(thumb);
                DataColumn dcstarsLevel = new DataColumn(starsLevel);
                DataColumn dcminAverPrice = new DataColumn(minAverPrice);
                DataColumn dcdesc = new DataColumn(desc);
                DataColumn dclocation = new DataColumn(location);
                DataColumn dccurrency = new DataColumn(currency);
                DataColumn dcbrandId = new DataColumn(brandId);
                DataColumn dcHotel_Id = new DataColumn(Hotel_Id);
                DataColumn dcDetallesURL = new DataColumn(sDetallesURL);

                tblTiposHabitacion.Columns.Add(dcHotelId);
                tblTiposHabitacion.Columns.Add(dcprovider);
                tblTiposHabitacion.Columns.Add(dcname);
                tblTiposHabitacion.Columns.Add(dcaddress);
                tblTiposHabitacion.Columns.Add(dccategory);
                tblTiposHabitacion.Columns.Add(dcbestVal);
                tblTiposHabitacion.Columns.Add(dcthumb);
                tblTiposHabitacion.Columns.Add(dcstarsLevel);
                tblTiposHabitacion.Columns.Add(dcminAverPrice);
                tblTiposHabitacion.Columns.Add(dcdesc);
                tblTiposHabitacion.Columns.Add(dclocation);
                tblTiposHabitacion.Columns.Add(dccurrency);
                tblTiposHabitacion.Columns.Add(dcbrandId);
                tblTiposHabitacion.Columns.Add(dcHotel_Id);
                tblTiposHabitacion.Columns.Add(dcDetallesURL);

                int iContadorCuarto = 0;
                DataRow[] drHoteles = dtHotels.Select(strWhere, "minAverPrice ASC");
                lblResultados.Text = drHoteles.Length.ToString();

                foreach (DataRow drHotel in drHoteles)
                {
                    DataRow filaTarifa = tblTiposHabitacion.NewRow();
                    filaTarifa[hotelId] = drHotel[hotelId];
                    filaTarifa[provider] = drHotel[provider];
                    filaTarifa[name] = drHotel[name];
                    filaTarifa[address] = drHotel[address];
                    filaTarifa[category] = drHotel[category];
                    filaTarifa[bestVal] = Utils.clsValidaciones.getDecimal(drHotel[bestVal].ToString());
                    filaTarifa[thumb] = drHotel[thumb];
                    filaTarifa[starsLevel] = drHotel[starsLevel];
                    filaTarifa[minAverPrice] = Utils.clsValidaciones.getDecimal(drHotel[minAverPrice].ToString());
                    filaTarifa[desc] = drHotel[desc];
                    filaTarifa[location] = drHotel[location];
                    filaTarifa[currency] = drHotel[currency];
                    filaTarifa[brandId] = drHotel[brandId];
                    filaTarifa[Hotel_Id] = drHotel[Hotel_Id];

                    string sURL = clsValidaciones.ObtenerUrlRutaPage("../Presentacion/Detalle_Hotel.aspx?ID=" + drHotel[hotelId].ToString() + "*" + drHotel[provider].ToString());
                    filaTarifa[sDetallesURL] = sURL;

                    tblTiposHabitacion.Rows.Add(filaTarifa);
                    iContadorCuarto++;
                }

                setRepeater(tblTiposHabitacion, rptHoteles);

                int iContadorHotel = 0;
                foreach (DataRow drHotel in drHoteles)
                {
                    Repeater rptTiposHabitacion = (Repeater)rptHoteles.Items[iContadorHotel].FindControl("rptTiposHabitacion");
                    Repeater RptEstrellas = (Repeater)rptHoteles.Items[iContadorHotel].FindControl("dlEstrellas");
                    setEstrellas(drHotel["starsLevel"].ToString(), RptEstrellas);
                    setTiposHabitacion(rptTiposHabitacion, drHotel, dtOccup, dtRoom, dtPrice, dtBoard, dtDiscount);
                    iContadorHotel++;
                }
            }
        }

        private void setFiltrarTipoNoRefundable
            (DataTable dtRoom)
        {
            try
            {
                bool bFiltrar=bool.Parse( clsValidaciones.GetKeyOrAdd("filtroHotelesB","0"));
                if (bFiltrar)
                {
                    DataRow[] drNORefundables = dtRoom.Select(" name like '%Non Refundable%'");
                    foreach(DataRow drNoRefundable in drNORefundables)
                    {
                        dtRoom.Rows.Remove(drNoRefundable);
                    }
                }
            }
            catch(Exception)
            {

            }
        }

        private void setEstrellas
            (string sNivelEstrellas,
            Repeater RptEstrellas)
        {
            if (RptEstrellas != null)
            {
                if (!String.IsNullOrEmpty(sNivelEstrellas))
                {
                    //decimal dNumStar = 0;
                    string[] sNumEs = null;

                    if (sNivelEstrellas.Contains("."))
                    {
                        sNumEs = sNivelEstrellas.Split(new char[] { '.' });
                    }
                    else
                    {
                        sNumEs = sNivelEstrellas.Split(new char[] { ',' });
                    }

                    int iNum = int.Parse(sNumEs[0]);

                    int iConStar = 0;
                    string sCamposStyle = "style";
                    string sEstrella = "stars";
                    string sEstrellaMedia = "stars2";
                    DataTable tblEstrellas = new DataTable("estrellas");
                    DataColumn dcStyle = new DataColumn(sCamposStyle);
                    tblEstrellas.Columns.Add(dcStyle);

                    while (iNum > iConStar)
                    {
                        DataRow drEstrella = tblEstrellas.NewRow();
                        drEstrella[sCamposStyle] = sEstrella;
                        tblEstrellas.Rows.Add(drEstrella);
                        iConStar++;
                    }

                    if (sNumEs.Length > 1)
                    {
                        DataRow drEstrella = tblEstrellas.NewRow();
                        drEstrella[sCamposStyle] = sEstrellaMedia;
                        tblEstrellas.Rows.Add(drEstrella);
                    }

                    RptEstrellas.DataSource = tblEstrellas;
                    RptEstrellas.DataBind();
                }
            }
        }

        private string getEstado
            (string sValor)
        {
            string sEstado = String.Empty;

            if (!String.IsNullOrEmpty(sValor))
            {
                if (sValor.Equals("True"))
                {
                    sEstado = "Disponible";
                }
                else
                {
                    sEstado = "A Solicitud";
                }
            }
            return sEstado;
        }

        public void setTiposHabitacion
          (Repeater rptTiposHabitacion,
          DataRow drHotel,
           DataTable dtOccup,
           DataTable dtRoom,
           DataTable dtPrice,
           DataTable dtBoard,
            DataTable dtDiscount)
        {
            string hotelId = drHotel["hotelId"].ToString();
            string provider = drHotel["provider"].ToString();
            string currency = drHotel["currency"].ToString();
            DataRow[] drTiposHabitacionXHotel = drHotel.GetChildRows(RELACION_HOTEL_ROOM_TYPE);

            if (drTiposHabitacionXHotel.Length > 0)
            {
                string sTablaTiposHabitacion = "tablaTiposHabitacion";
                string productId = "productId";
                string name = "name";
                string nights = "nights";
                string startDate = "startDate";
                string isAvailable = "isAvailable";
                string roomId = "roomId";
                string hotelRoomTypeId = "hotelRoomTypeId";
                string RoomType_Id = "RoomType_Id";
                string Hotel_Id = "Hotel_Id";
                string sURL_Reservar = "URL_Reservar";

                DataTable tblTiposHabitacion = new DataTable(sTablaTiposHabitacion);
                DataColumn dcHotelId = new DataColumn(productId);
                DataColumn dcname = new DataColumn(name);
                DataColumn dcaddress = new DataColumn(nights);
                DataColumn dccategory = new DataColumn(startDate);
                DataColumn dcbestVal = new DataColumn(isAvailable);
                DataColumn dcthumb = new DataColumn(roomId);
                DataColumn dcstarsLevel = new DataColumn(hotelRoomTypeId);
                DataColumn dcminAverPrice = new DataColumn(RoomType_Id);
                DataColumn dcHotel_Id = new DataColumn(Hotel_Id);
                DataColumn dcURL_Reservar = new DataColumn(sURL_Reservar);

                tblTiposHabitacion.Columns.Add(dcHotelId);
                tblTiposHabitacion.Columns.Add(dcname);
                tblTiposHabitacion.Columns.Add(dcaddress);
                tblTiposHabitacion.Columns.Add(dccategory);
                tblTiposHabitacion.Columns.Add(dcbestVal);
                tblTiposHabitacion.Columns.Add(dcthumb);
                tblTiposHabitacion.Columns.Add(dcstarsLevel);
                tblTiposHabitacion.Columns.Add(dcminAverPrice);
                tblTiposHabitacion.Columns.Add(dcHotel_Id);
                tblTiposHabitacion.Columns.Add(dcURL_Reservar);

                int iContadorCuarto = 0;
                foreach (DataRow drCuarto in drTiposHabitacionXHotel)
                {
                    DataRow filaTarifa = tblTiposHabitacion.NewRow();
                    filaTarifa[productId] = drCuarto[productId];
                    filaTarifa[name] = drCuarto[name];
                    filaTarifa[nights] = drCuarto[nights];
                    filaTarifa[startDate] = drCuarto[startDate];

                    string sDisponibilidad = getEstado(drCuarto[isAvailable].ToString());
                    filaTarifa[isAvailable] = sDisponibilidad;


                    filaTarifa[roomId] = drCuarto[roomId];
                    filaTarifa[hotelRoomTypeId] = drCuarto[hotelRoomTypeId];
                    filaTarifa[RoomType_Id] = drCuarto[RoomType_Id];
                    filaTarifa[Hotel_Id] = drCuarto[Hotel_Id];

                    string sURL = "HotelId=" + hotelId;
                    sURL += "&SelectedProductId=" + drCuarto[productId].ToString();
                    sURL += "&Provider=" + provider;
                    sURL += "&RoomId=" + drCuarto[roomId];


                    filaTarifa[sURL_Reservar] = sURL;

                    tblTiposHabitacion.Rows.Add(filaTarifa);
                    iContadorCuarto++;
                }

                setRepeater(tblTiposHabitacion, rptTiposHabitacion);

                int iContadorPlanes = 0;
                foreach (DataRow drTipoHabitacion in drTiposHabitacionXHotel)
                {
                    Repeater rptDisponibilidad = (Repeater)rptTiposHabitacion.Items[iContadorPlanes].FindControl("rptDisponibilidad");
                    Repeater rptOcupacion = (Repeater)rptTiposHabitacion.Items[iContadorPlanes].FindControl("rptOcupacion");
                    Repeater rptDesayuno = (Repeater)rptTiposHabitacion.Items[iContadorPlanes].FindControl("rptDesayuno");
                    Repeater rptDescuento = (Repeater)rptTiposHabitacion.Items[iContadorPlanes].FindControl("rptDescuento");
                    Panel pTarifas = (Panel)rptTiposHabitacion.Items[iContadorPlanes].FindControl("pTarifas");
                    HtmlInputButton btnMuestraOculta1 = (HtmlInputButton)rptTiposHabitacion.Items[iContadorPlanes].FindControl("btnMuestraOculta1");

                    Label lblPromedio = (Label)rptTiposHabitacion.Items[iContadorPlanes].FindControl("lblPromedio");
                    string sIDPanel = pTarifas.ClientID;
                    btnMuestraOculta1.Attributes.Add("onClick", "if(" + sIDPanel + ".style.display == 'none'){" + sIDPanel + ".style.display = '';" + sIDPanel + ".value='Ocultar Detalles';}else{" + sIDPanel + ".style.display = 'none';" + sIDPanel + ".value='Mostrar Detalles';} ");

                    DateTime dtFecha = DateTime.Parse(drTipoHabitacion[startDate].ToString());
                    setDisponibilidades(rptDisponibilidad, drTipoHabitacion, dtFecha);
                    setOcupacion(rptOcupacion, drTipoHabitacion, rptDesayuno, rptDescuento, dtOccup, dtRoom, dtPrice, dtBoard, dtDiscount, lblPromedio, currency);
                    iContadorPlanes++;
                }
            }
        }


        private void setDisponibilidades
          (Repeater rptDisponibilidad,
          DataRow drTipoHabitacion,
           DateTime dtFechaInicial)
        {
            DataRow[] drDisponibilidades = drTipoHabitacion.GetChildRows(RELACION_ROOMTYPE_AVAIL);

            if (drDisponibilidades.Length > 0)
            {
                string sDisponibilidad = "tablaTiposDisponibilidad";
                string offset = "offset";
                string status = "status";
                string RoomType_Id = "RoomType_Id";
                string sFecha = "fecha";

                DataTable tblDisponibilidad = new DataTable(sDisponibilidad);
                DataColumn dcHotelId = new DataColumn(offset);
                DataColumn dcname = new DataColumn(status);
                DataColumn dcaddress = new DataColumn(RoomType_Id);
                DataColumn dcFecha = new DataColumn(sFecha);

                tblDisponibilidad.Columns.Add(dcHotelId);
                tblDisponibilidad.Columns.Add(dcname);
                tblDisponibilidad.Columns.Add(dcaddress);
                tblDisponibilidad.Columns.Add(dcFecha);

                int iContadorCuarto = 0;
                foreach (DataRow drCuarto in drDisponibilidades)
                {
                    DataRow filaTarifa = tblDisponibilidad.NewRow();
                    filaTarifa[offset] = drCuarto[offset];
                    filaTarifa[status] = getEstado(drCuarto[status].ToString());
                    filaTarifa[RoomType_Id] = drCuarto[RoomType_Id];
                    filaTarifa[sFecha] = dtFechaInicial.ToString("dd/MMM");
                    dtFechaInicial = dtFechaInicial.AddDays(1);
                    tblDisponibilidad.Rows.Add(filaTarifa);
                    iContadorCuarto++;
                }

                setRepeater(tblDisponibilidad, rptDisponibilidad);


            }
        }

        private void setOcupacion
          (Repeater rptOcupacion,
            DataRow drTipoHabitacion,
            Repeater rptDesayuno,
            Repeater rptDescuento,
            DataTable dtOccup,
            DataTable dtRoom,
            DataTable dtPrice,
            DataTable dtBoard,
            DataTable dtDiscount,
            Label lblPromedio,
            string sCurrency)
        {
            DataRow[] drOcupacionesXTipoHab = drTipoHabitacion.GetChildRows(RELACION_ROOMTYPE_OCCUP);

            DataTable tblOcupacion = new DataTable("tblRooms");
            if (drOcupacionesXTipoHab.Length > 0)
            {
                string sIds = String.Empty;

                foreach (DataRow drOccup_Id in drOcupacionesXTipoHab)
                {
                    sIds += "," + drOccup_Id["Occup_Id"].ToString();
                }
                sIds = sIds.Substring(1);
                DataRow[] drRooms = dtRoom.Select("Occup_Id in (" + sIds + ")", "adultNum,childNum ASC");

                string occupId = "occupId";
                string maxAdult = "maxAdult";
                string maxChild = "maxChild";
                string price = "price";
                string tax = "tax";
                string dblBed = "dblBed";
                string hotelRoomTypeId = "hotelRoomTypeId";
                string RoomType_Id = "RoomType_Id";

                DataColumn dcHotelId = new DataColumn(occupId);
                DataColumn dcname = new DataColumn(maxAdult);
                DataColumn dcaddress = new DataColumn(maxChild);
                DataColumn dccategory = new DataColumn(price);
                DataColumn dcbestVal = new DataColumn(tax);
                DataColumn dcthumb = new DataColumn(dblBed);
                DataColumn dcstarsLevel = new DataColumn(hotelRoomTypeId);
                DataColumn dcminAverPrice = new DataColumn(RoomType_Id);

                tblOcupacion.Columns.Add(dcHotelId);
                tblOcupacion.Columns.Add(dcname);
                tblOcupacion.Columns.Add(dcaddress);
                tblOcupacion.Columns.Add(dccategory);
                tblOcupacion.Columns.Add(dcbestVal);
                tblOcupacion.Columns.Add(dcthumb);
                tblOcupacion.Columns.Add(dcstarsLevel);
                tblOcupacion.Columns.Add(dcminAverPrice);

                List<string> lstrOccup_Id = new List<string>();
                
                foreach (DataRow drRoom in drRooms)
                {
                    DataRow drOccupXRoom = drRoom.GetParentRow(RELACION_OCCUP_ROOM);
                    string strOccup_Id = drOccupXRoom["Occup_Id"].ToString();

                    if (!lstrOccup_Id.Contains(strOccup_Id))
                    {
                        lstrOccup_Id.Add(strOccup_Id);
                        DataRow filaOcupacion = tblOcupacion.NewRow();
                        filaOcupacion[occupId] = strOccup_Id;


                        string strADT = drRoom["adultNum"].ToString();
                        string strCNN = drRoom["childNum"].ToString();

                        int iADT = 0;
                        int iCNN = 0;
                        int iTotal = 0;

                        int.TryParse(strADT, out iADT);
                        int.TryParse(strCNN, out iCNN);
                        iTotal = iADT + iCNN;

                        filaOcupacion[maxAdult] = iTotal.ToString();
                        lblPromedio.Text = sCurrency + " " + drOccupXRoom["avrNightPrice"].ToString();
                        filaOcupacion[maxChild] = drOccupXRoom[maxChild];
                        filaOcupacion[price] = drOccupXRoom[price];
                        filaOcupacion[tax] = drOccupXRoom[tax];
                        filaOcupacion[dblBed] = drOccupXRoom[dblBed];
                        filaOcupacion[RoomType_Id] = drOccupXRoom[RoomType_Id];

                        tblOcupacion.Rows.Add(filaOcupacion);
                    }
                }
                DataRowCollection drcOcupacion = tblOcupacion.Rows;
                DataRow[] drOcupacionesTotales = new DataRow[drcOcupacion.Count];
                drcOcupacion.CopyTo(drOcupacionesTotales, 0);

                DataTable tblOcupaciones = getAcomodaciones(drOcupacionesTotales);
                setRepeater(tblOcupaciones, rptOcupacion);

                int iContadorPlanes = 0;
                lstrOccup_Id.Clear();

                foreach (DataRow drRoom in drRooms)
                {
                    DataRow drOccup_Room = drRoom.GetParentRow(RELACION_OCCUP_ROOM);
                    string strOccup_Id = drOccup_Room["Occup_Id"].ToString();

                    if (!lstrOccup_Id.Contains(strOccup_Id))
                    {
                        lstrOccup_Id.Add(strOccup_Id);
                        Repeater rptPrecios = (Repeater)rptOcupacion.Items[iContadorPlanes].FindControl("rptPrecios");
                        DataRow[] drPricesXRoom = drOccup_Room.GetChildRows(RELACION_OCCUP_PRICE);
                        setPrecios(rptPrecios, drPricesXRoom);

                        DataRow[] drDesayonosXRoom = drOccup_Room.GetChildRows(RELACION_OCCUP_BOARD);
                        setDesayuno(rptDesayuno, drDesayonosXRoom);

                        iContadorPlanes++;
                    }
                }
            }
            DataRow[] drDiscountXTipoHab = drTipoHabitacion.GetChildRows(RELACION_ROOMTYPE_DISCOUNT);

            if (drDiscountXTipoHab.Length > 0)
            {
                setDescuento(rptDescuento, drDiscountXTipoHab);
            }
        }

        private void setPrecios
          (Repeater rptPrecios,
          DataRow[] drPrecios)
        {

            if (drPrecios.Length > 0)
            {
                string sDisponibilidad = "tablaTiposDisponibilidad";
                string offset = "offset";
                string value = "value";
                string Occup_Id = "Occup_Id";

                DataTable tblPrecio = new DataTable(sDisponibilidad);
                DataColumn dcHotelId = new DataColumn(offset);
                DataColumn dcname = new DataColumn(value);
                DataColumn dcaddress = new DataColumn(Occup_Id);

                tblPrecio.Columns.Add(dcHotelId);
                tblPrecio.Columns.Add(dcname);
                tblPrecio.Columns.Add(dcaddress);

                int iContadorCuarto = 0;
                foreach (DataRow drPrecio in drPrecios)
                {
                    DataRow filaPrecio = tblPrecio.NewRow();
                    filaPrecio[offset] = drPrecio[offset];
                    if (drPrecio[value].ToString() == "0")
                    {
                        filaPrecio[value] = "Gratis";
                    }
                    else
                    {
                        filaPrecio[value] = drPrecio[value].ToString();
                    }
                    filaPrecio[Occup_Id] = drPrecio[Occup_Id];
                    tblPrecio.Rows.Add(filaPrecio);
                    iContadorCuarto++;
                }
                setRepeater(tblPrecio, rptPrecios);
            }
        }

        private void setDesayuno
          (Repeater rptDesayuno,
          DataRow[] drDesayunos)
        {
            if (drDesayunos.Length > 0)
            {
                string sDisponibilidad = "tablaTiposDesayuno";
                string bbId = "bbId";
                string name = "name";
                string price = "price";
                string sdefault = "default";
                string Occup_Id = "Occup_Id";

                DataTable tblOcupacion = new DataTable(sDisponibilidad);
                DataColumn dcHotelId = new DataColumn(bbId);
                DataColumn dcname = new DataColumn(name);
                DataColumn dccategory = new DataColumn(price);
                DataColumn dcbestVal = new DataColumn(sdefault);
                DataColumn dcthumb = new DataColumn(Occup_Id);

                tblOcupacion.Columns.Add(dcHotelId);
                tblOcupacion.Columns.Add(dcname);
                tblOcupacion.Columns.Add(dccategory);
                tblOcupacion.Columns.Add(dcbestVal);
                tblOcupacion.Columns.Add(dcthumb);

                int iContadorCuarto = 0;
                foreach (DataRow drDesayuno in drDesayunos)
                {
                    DataRow filaTarifa = tblOcupacion.NewRow();
                    filaTarifa[bbId] = drDesayuno[bbId];
                    filaTarifa[name] = drDesayuno[name];
                    filaTarifa[price] = drDesayuno[price];
                    filaTarifa[sdefault] = drDesayuno[sdefault];
                    filaTarifa[Occup_Id] = drDesayuno[Occup_Id];

                    tblOcupacion.Rows.Add(filaTarifa);
                    iContadorCuarto++;
                }

                setRepeater(tblOcupacion, rptDesayuno);



            }
        }

        private void setDescuento
          (Repeater rptDescuento,
          DataRow[] drDescuentos)
        {
            string strDescuento = string.Empty;
            if (drDescuentos.Length > 0)
            {
                string sDisponibilidad = "tablaDescuentos";
                string name = "name";

                DataTable tblOcupacion = new DataTable(sDisponibilidad);
                DataColumn dcname = new DataColumn(name);

                tblOcupacion.Columns.Add(dcname);

                int iContadorCuarto = 0;
                foreach (DataRow drDescuento in drDescuentos)
                {
                    DateTime dtFechaIni = DateTime.Parse(drDescuento["from"].ToString());
                    DateTime dtFechaFin = DateTime.Parse(drDescuento["to"].ToString());
                    String sFechaIni = new DateTime(dtFechaIni.Year, dtFechaIni.Month, dtFechaIni.Day).ToString("MMM dd");
                    String sFechaFin = new DateTime(dtFechaFin.Year, dtFechaFin.Month, dtFechaFin.Day).ToString("MMM dd");

                    strDescuento = "Descuento desde " + sFechaIni + " a " + sFechaFin + " pague " + drDescuento["pay"].ToString() + " noches, quedese " + drDescuento["stay"].ToString() + " noches ";

                    DataRow filaTarifa = tblOcupacion.NewRow();
                    filaTarifa[name] = strDescuento;

                    tblOcupacion.Rows.Add(filaTarifa);
                    iContadorCuarto++;
                }

                setRepeater(tblOcupacion, rptDescuento);
            }
        }

      

        public void setDatosHuespedes
            (Repeater rptHabitacion,
            DataTable dtRoom,
            DataTable dtBroad,
            DataTable dtOccup,
            DataTable dtDiscount,
            string sCurrency
            )
        {
            if (rptHabitacion != null)
            {
                int iContador = 0;
                int iContadorItems = rptHabitacion.Items.Count;

                while (iContador < iContadorItems)
                {
                    HiddenField hfHabitacion = (HiddenField)rptHabitacion.Items[iContador].FindControl("hfHabitacion");
                    Repeater rptOcupacion = (Repeater)rptHabitacion.Items[iContador].FindControl("rptOcupacion");
                    Repeater rptDesayuno = (Repeater)rptHabitacion.Items[iContador].FindControl("rptDesayuno");
                    Repeater rptDescuento = (Repeater)rptHabitacion.Items[iContador].FindControl("rptDescuento");
                    Label lblPreferencias = (Label)rptHabitacion.Items[iContador].FindControl("lblPreferencias");
                    
                    RadioButtonList rblAcomodaciones = (RadioButtonList)rptHabitacion.Items[iContador].FindControl("rblAcomodaciones");
                    setDetalleOcupaciones
                        (hfHabitacion.Value,
                        rptOcupacion,
                        rptDesayuno,
                        rptDescuento,
                        rblAcomodaciones,
                        dtRoom,
                        dtOccup,
                        dtBroad,
                        dtDiscount,
                        sCurrency,
                        lblPreferencias);
                    iContador++;
                }
            }
        }

        private void setDetalleOcupaciones
            (string sRoomId,
            Repeater rptOcupacion,
            Repeater rptDesayuno,
            Repeater rptDescuento,
            RadioButtonList rblAcomodaciones,
            DataTable dtRoom,
            DataTable dtOccup,
            DataTable dtBroad,
            DataTable dtDiscount,
            string sCurrency,
            Label lblPreferencias)
        {
            DataTable dtOCupaciones = new DataTable();
            if (dtRoom != null)
            {
                List<string> lsOccup_Id = new List<string>();
                List<string> lsRoomsIds = new List<string>();
                DataRow[] drOccup_Ids = dtRoom.Select("((adultNum=2 AND childNum=0) or (adultNum=1 AND childNum=1)) and seqNum=" + sRoomId, " Occup_Id ASC");

                foreach (DataRow drOccup_Id in drOccup_Ids)
                {
                    lsOccup_Id.Add(drOccup_Id["Occup_Id"].ToString());
                    lsRoomsIds.Add(drOccup_Id["Room_Id"].ToString());
                    lblPreferencias.Text = "Las preferencias de camas estan sujetas a la disponibilidad del proveedor en el Check-in";
                }

                setOcupacionesXRoom
                    (lsOccup_Id,
                    lsRoomsIds,
                    rptDesayuno,
                    rptOcupacion,
                    rptDescuento,
                    rblAcomodaciones,
                    dtOccup,
                    dtDiscount,
                    sCurrency);
            }
        }


        private void setOcupacionesXRoom
            (List<string> lsOccup_Id,
            List<string> lsRoomsIds,
            Repeater rptDesayuno,
            Repeater rptOcupacion,
            Repeater rptDescuento,
            RadioButtonList rblAcomodaciones,
            DataTable dtOccup,
            DataTable dtDiscount,
            string sCurrency)
        {
            if (lsOccup_Id != null && dtOccup != null && lsOccup_Id.Count > 0)
            {
                string sIds = String.Empty;

                foreach (string sID in lsOccup_Id)
                {
                    sIds += "," + sID;
                }
                sIds = sIds.Substring(1);
                DataRow[] drOccup = dtOccup.Select("Occup_Id in (" + sIds + ")"," Occup_Id ASC");
                
                DataTable dtOCupaciones = getAcomodaciones(drOccup);

                if (drOccup.Length > 0)
                {
                    setDesayuno(rptDesayuno, drOccup[0].GetChildRows(RELACION_OCCUP_BOARD));
                }
                DataRow[] drDiscountXTipoHab = dtDiscount.Select();

                if (drOccup.Length > 0)
                {
                    setDescuento(rptDescuento, drDiscountXTipoHab);
                }
                for (int i = 0; i < dtOCupaciones.Rows.Count; i++)
                {
                    dtOCupaciones.Rows[i]["price"] = "  " + sCurrency + " " + dtOCupaciones.Rows[i]["price"].ToString(); 
                }
                setRepeater(dtOCupaciones, rptOcupacion);

                foreach (string sID_Value in lsRoomsIds)
                {
                    rblAcomodaciones.Items.Add(new ListItem(String.Empty, sID_Value));
                }
                rblAcomodaciones.SelectedIndex = 0;

            }
        }

        public DataTable getAcomodaciones
            (DataRow[] drOcupaciones)
        {
            string sTablaTiposHabitacion = "tablaTiposHabitacion";
            DataTable tblOcupacion = new DataTable(sTablaTiposHabitacion);

            if (drOcupaciones != null && drOcupaciones.Length > 0)
            {
                string occupId = "occupId";
                string maxAdult = "maxAdult";
                string maxChild = "maxChild";
                string price = "price";
                string tax = "tax";
                string dblBed = "dblBed";
                string hotelRoomTypeId = "hotelRoomTypeId";
                string RoomType_Id = "RoomType_Id";
                string display = "display";

                DataColumn dcHotelId = new DataColumn(occupId);
                DataColumn dcname = new DataColumn(maxAdult);
                DataColumn dcaddress = new DataColumn(maxChild);
                DataColumn dccategory = new DataColumn(price);
                DataColumn dcbestVal = new DataColumn(tax);
                DataColumn dcthumb = new DataColumn(dblBed);
                DataColumn dcstarsLevel = new DataColumn(hotelRoomTypeId);
                DataColumn dcminAverPrice = new DataColumn(RoomType_Id);
                DataColumn dcdisplay = new DataColumn(display);

                tblOcupacion.Columns.Add(dcHotelId);
                tblOcupacion.Columns.Add(dcname);
                tblOcupacion.Columns.Add(dcaddress);
                tblOcupacion.Columns.Add(dccategory);
                tblOcupacion.Columns.Add(dcbestVal);
                tblOcupacion.Columns.Add(dcthumb);
                tblOcupacion.Columns.Add(dcstarsLevel);
                tblOcupacion.Columns.Add(dcminAverPrice);
                tblOcupacion.Columns.Add(dcdisplay);

                int iContadorCuarto = 0;
                foreach (DataRow drCuarto in drOcupaciones)
                {
                    DataRow filaOcupacion = tblOcupacion.NewRow();
                    filaOcupacion[occupId] = drCuarto[occupId];

                    string sMaxAdult = drCuarto["maxAdult"].ToString();
                    string sStyle = "display=none";

                    if (sMaxAdult.Equals("2"))
                    {
                        sStyle = "display=''";
                    }

                    filaOcupacion[maxAdult] = "~/App_Themes/Imagenes/" + sMaxAdult + ".gif";
                    filaOcupacion[maxChild] = drCuarto[maxChild];
                    filaOcupacion[price] = drCuarto[price];
                    filaOcupacion[tax] = drCuarto[tax];

                    string sCama = "2";
                    string sCamaDocle = drCuarto[dblBed].ToString();
                    if (!String.IsNullOrEmpty(sCamaDocle))
                    {
                        if (sCamaDocle.Equals("True"))
                        {
                            sCama = "1";
                        }
                    }
                    filaOcupacion[dblBed] = "~/App_Themes/Imagenes/" + sCama + ".gif";
                    filaOcupacion[display] = sStyle;

                    tblOcupacion.Rows.Add(filaOcupacion);
                    iContadorCuarto++;
                }
            }
            return tblOcupacion;
        }

        public DataTable SelectDistinct
            (string[] pColumnNames,
            DataTable pOriginalTable,
            string sOrden)
        {
            //FILTRAR LOS RESULTADOS
            DataTable distinctTable = new DataTable();

            int numColumns = pColumnNames.Length;

            for (int i = 0; i < numColumns; i++)
            {
                distinctTable.Columns.Add(pColumnNames[i], pOriginalTable.Columns[pColumnNames[i]].DataType);
            }

            Hashtable trackData = new Hashtable();

            foreach (DataRow currentOriginalRow in pOriginalTable.Rows)
            {

                StringBuilder hashData = new StringBuilder();

                DataRow newRow = distinctTable.NewRow();

                for (int i = 0; i < numColumns; i++)
                {

                    hashData.Append(currentOriginalRow[pColumnNames[i]].ToString());

                    newRow[pColumnNames[i]] = currentOriginalRow[pColumnNames[i]];

                }

                if (!trackData.ContainsKey(hashData.ToString()))
                {

                    trackData.Add(hashData.ToString(), null);

                    distinctTable.Rows.Add(newRow);

                }
            }

            if (sOrden != null)
            {
                //ORDENAR LOS RESULTADOS
                DataTable dtOrden = distinctTable.Copy();
                DataRow[] drRegistros = dtOrden.Select(null, sOrden);
                if (drRegistros != null && drRegistros.Length > 0)
                {
                    distinctTable.Rows.Clear();

                    foreach (DataRow drRegistro in drRegistros)
                    {
                        DataRow drDistinct = distinctTable.NewRow();
                        drDistinct[0] = drRegistro[0];
                        distinctTable.Rows.Add(drDistinct);
                    }
                }
            }
            return distinctTable;
        }

       

        private decimal getPrecioXAcomodacion
            (DataRow drOccup)
        {
            decimal dTotal = 0;
            if (drOccup != null)
            {
                DataRow[] drPrecios = drOccup.GetChildRows(RELACION_OCCUP_PRICE);

                foreach (DataRow drPrecio in drPrecios)
                {
                    dTotal += Utils.clsValidaciones.getDecimal(drPrecio["value"].ToString());
                }
            }
            return dTotal;
        }

        private string getDesayunoXAcomodacion
           (DataRow drOccup)
        {
            string sDesayunos= String.Empty;
            if (drOccup != null)
            {
                DataRow[] drDesayunos = drOccup.GetChildRows(RELACION_OCCUP_BOARD);

                foreach (DataRow drDesayuno in drDesayunos)
                {
                    sDesayunos += drDesayuno["name"].ToString();
                }
            }
            return sDesayunos;
        }

   
        public bool ValidarNombres(Repeater rptNombres)
        {
            int numColumns = rptNombres.Items.Count;
            bool bValidar = true;
            for (int i = 0; i < numColumns; i++)
            {
                TextBox txtNombre = (TextBox)rptNombres.Items[i].FindControl("txtNombre");
                TextBox txtApellido = (TextBox)rptNombres.Items[i].FindControl("txtApellido");
                string strNombre = txtNombre.Text + txtApellido.Text;

                for (int j = i+1; j < numColumns; j++)
                {
                    TextBox txtNombreC = (TextBox)rptNombres.Items[j].FindControl("txtNombre");
                    TextBox txtApellidoC = (TextBox)rptNombres.Items[j].FindControl("txtApellido");
                    string strNombreC = txtNombreC.Text + txtApellidoC.Text;
                    if (strNombre == strNombreC)
                    {
                        bValidar = false;
                    }
                }
            }
            return bValidar;
        }
    }
}

