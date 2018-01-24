using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsTotal
/// </summary>
public class clsTotal
{

    public string urlImagenAerolinea { get; set; }
    public string strMarketingAirline { get; set; }
    public string strNombre_Aerolinea { get; set; }
    public decimal intPrecioDesde { get; set; }
    public string StopQuantity { get; set; }
	
    public clsTotal()
	{
		//
		// TODO: Add constructor logic here
		//
        

	}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sPathImage"></param>
    /// <param name="sCodeAir"></param>
    /// <param name="sPrice"></param>
    /// <param name="sTipoParada"></param>
    public clsTotal(string sPathImage, string sCodeAir,string sName, decimal sPrice, string sTipoParada)
    {
        this.urlImagenAerolinea = sPathImage;//
        this.strMarketingAirline = sCodeAir;//
        this.strNombre_Aerolinea = sName;//
        this.intPrecioDesde = sPrice;//
        this.StopQuantity = sTipoParada;//
        // TODO: Add constructor logic here
        //


    }
    public  bool bMayor(clsTotal p2)
    {
        if ( p2 == null) return false;
        return (this.strMarketingAirline == p2.strMarketingAirline) && (this.intPrecioDesde > p2.intPrecioDesde);
    }

    public bool bLess(clsTotal p2)
    {
        if (p2 == null) return false;
        if ((this.strMarketingAirline == p2.strMarketingAirline) && (this.intPrecioDesde <= p2.intPrecioDesde))
        {
            return true;

        }
        else
        {
            return false;
        }
    }


}