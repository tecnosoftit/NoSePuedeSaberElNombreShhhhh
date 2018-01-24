using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Xml;
using WebService_SessionClose = WS_SsoftSabre.SessionClose;
using WebService_SessionCreate = WS_SsoftSabre.SessionCreateRQ;
using Ssoft.ValueObjects;
using WS_SsoftSabre.General;
using WS_SsoftSabre.SabreCommandLLS;
using WS_SsoftSabre.Air;

public class WebService_Session
{
    #region [ ATRIBUTOS ]

    protected string session_;

    #endregion

    #region [ CONSTRUCTOR ]

    public WebService_Session()
    {
        //this.session_ = AutenticacionSabre.GET_SabreSession();
    }

    #endregion

    #region [ PROPIEDADES ]

    public string Session_
    {
        get { return session_; }
        set { session_ = value; }
    }

    #endregion

    #region [ METODOS ]

    public string _Sabre_CrearSesion()
    {
        string Session_ = null;

        Ssoft.ValueObjects.VO_Credentials objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
        /*ASIGNAMOS LOS CRITERIOS BASICOS PARA CREAR LA SESSION*/
        VO_SessionCreateRQ vo_SessionCreateRQ = new VO_SessionCreateRQ(objvo_Credentials.User,
                                                                        objvo_Credentials.Password,
                                                                        objvo_Credentials.Ipcc,
                                                                        objvo_Credentials.Pcc,
                                                                        objvo_Credentials.Dominio
                                                                        );

        clsSessionCreateRQ objclsSessionCreateRQ = new clsSessionCreateRQ();
        Session_ = objclsSessionCreateRQ.getSesion(vo_SessionCreateRQ);
        VO_SabreCommandLLSRS vo = new VO_SabreCommandLLSRS();
        vo.BCDATA = true;
        vo.StrComando = "AAA" + objvo_Credentials.Pcc;

        clsSabreCommandLLS oclsSabreCommandLLS = new clsSabreCommandLLS();
        oclsSabreCommandLLS.StrSesion = Session_;
        SabreCommandLLSRS respuesta = oclsSabreCommandLLS.getEjecutarComando(vo);

        return Session_;
    }

    public WebService_SessionClose.SessionCloseRS _Sabre_CerrarSesion()
    {
        clsSessionClose objClsSessionClose = new clsSessionClose();
        return objClsSessionClose.CerrarSesion(session_);
    }

    #endregion

    #region [ DESTRUCTOR ]

    ~WebService_Session() { }

    #endregion
}
