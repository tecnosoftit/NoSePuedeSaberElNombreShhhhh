using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;

public class VO_DataTravelItineraryAddInfo
{
    #region [ ATRIBUTOS ]

    private int id_;
    private string nombre_;
    private string apellido_;
    private bool infante_;
    private string tipo_;
    private List<string> telefono_;
    private string codigoLocalizacion_;
    private string codigoArea_;
    private string email_;
    private string aeroliena_;
    private string viajeroFrecuente_;
    private string tipodocumento_;
    private string documento_;
    private string fecha_;
    private string edad_;
    private string nacionalidad_;
    private string genero_;
    private string tipoGen_;

    #endregion

    #region [ CONSTRUCTOR ]

    public VO_DataTravelItineraryAddInfo(int Id_,
                                        string Nombre_,
                                        string Apellido_,
                                        bool Infante_,
                                        string Tipo_
                                       )
    {
        this.id_ = Id_;
        this.nombre_ = Nombre_;
        this.apellido_ = Apellido_;
        this.infante_ = Infante_;
        this.tipo_ = Tipo_;
    }

    public VO_DataTravelItineraryAddInfo(string Nombre_,
                                        string Apellido_,
                                        string Email_,
                                        List<string> Telefono_,
                                        string CodigoArea_,
                                        string CodigoLocalizacion_
                                       )
    {
        this.nombre_ = Nombre_;
        this.apellido_ = Apellido_;
        this.email_ = Email_;
        this.tipo_ = Tipo_;
        this.telefono_ = Telefono_;
        this.codigoArea_ = CodigoArea_;
        this.codigoLocalizacion_ = CodigoLocalizacion_;
    }

    public VO_DataTravelItineraryAddInfo(int Id_,
                                        string Nombre_,
                                        string Apellido_,
                                        bool Infante_,
                                        string Tipo_,
                                        List<string> Telefono_,
                                        string CodigoLocalizacion_,
                                        string CodigoArea_,
                                        string Email_,
                                        string aeroliena_,
                                        string viajeroFrecuente_
                                       )
    {
        this.id_ = Id_;
        this.nombre_ = Nombre_;
        this.apellido_ = Apellido_;
        this.infante_ = Infante_;
        this.tipo_ = Tipo_;
        this.telefono_ = Telefono_;
        this.codigoLocalizacion_ = CodigoLocalizacion_;
        this.codigoArea_ = CodigoArea_;
        this.email_ = Email_;
        this.aeroliena_ = aeroliena_;
        this.viajeroFrecuente_ = viajeroFrecuente_;
    }

    #endregion

    #region [ PROPIEDADES ]

    public int Id_
    {
        get { return id_; }
        set { id_ = value; }
    }

    public string Nombre_
    {
        get { return nombre_; }
        set { nombre_ = value; }
    }

    public string Apellido_
    {
        get { return apellido_; }
        set { apellido_ = value; }
    }

    public string Tipo_
    {
        get { return tipo_; }
        set { tipo_ = value; }
    }

    public bool Infante_
    {
        get { return infante_; }
        set { infante_ = value; }
    }

    public List<string> Telefono_
    {
        get { return telefono_; }
        set { telefono_ = value; }
    }

    public string CodigoLocalizacion_
    {
        get { return codigoLocalizacion_; }
        set { codigoLocalizacion_ = value; }
    }

    public string CodigoArea_
    {
        get { return codigoArea_; }
        set { codigoArea_ = value; }
    }

    public string Email_
    {
        get { return email_; }
        set { email_ = value; }
    }

    public string Aeroliena_
    {
        get { return aeroliena_; }
        set { aeroliena_ = value; }
    }

    public string ViajeroFrecuente_
    {
        get { return viajeroFrecuente_; }
        set { viajeroFrecuente_ = value; }
    }

    public string TipoDocumento_
    {
        get { return tipodocumento_; }
        set { tipodocumento_ = value; }
    }

    public string Documento_
    {
        get { return documento_; }
        set { documento_ = value; }
    }

    public string Fecha_
    {
        get { return fecha_; }
        set { fecha_ = value; }
    }

    public string Edad_
    {
        get { return edad_; }
        set { edad_ = value; }
    }

    public string Nacionalidad_
    {
        get { return nacionalidad_; }
        set { nacionalidad_ = value; }
    }

    public string Genero_
    {
        get { return genero_; }
        set { genero_ = value; }
    }

    public string TipoGen_
    {
        get { return tipoGen_; }
        set { tipoGen_ = value; }
    }

    #endregion

    #region [ METODOS ]

    //public static List<VO_DataTravelItineraryAddInfo> GET_PASAJEROS(List<VO_TblRespax> ListPax_, string Aerolinea_)
    //{
    //    List<VO_DataTravelItineraryAddInfo> Info_ = new List<VO_DataTravelItineraryAddInfo>();

    //    try
    //    {
    //        for (int i = 0; i < ListPax_.Count; i++)
    //        {
    //            List<string> ListTel = new List<string>();
    //            ListTel.Add("");

    //            VO_TblRespax P_ = ListPax_[i];

    //            VO_DataTravelItineraryAddInfo Informacion_ = new VO_DataTravelItineraryAddInfo
    //            ((i + 1), P_.NombrePasajero_.Split(new Char[] { ' ' })[0], P_.NombrePasajero_.Split(new Char[] { ' ' })[1],
    //            false, P_.TipoPasajero_, ListTel, "H", "571", "", Aerolinea_, "");

    //            Info_.Add(Informacion_);

    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        Info_.Clear();
    //        new ControlErrores_Archivos(Ex, "Modulo de carga de pasajeros _GET_PASAJEROS()")._ArchivoError();
    //    }

    //    return Info_;
    //}

    #endregion

    #region [ DESTRUCTOR ]

    ~VO_DataTravelItineraryAddInfo() { }

    #endregion
}
