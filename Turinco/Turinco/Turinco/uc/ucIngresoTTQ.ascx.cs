using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Utils;
using Ssoft.Pages;
using System.Net.Mail;

public partial class uc_ucIngresoTTQ : System.Web.UI.UserControl
{
    csResultadoVuelos cRefere = new csResultadoVuelos();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        Label lblUrltopflight = (Label)this.Parent.FindControl("lbltopflight");
        string sUrl=cRefere.SetEntrarTopFlight(txtUsuario.Text, txtPassword.Text, lblUrltopflight.Text);
        if (sUrl != "")
        {
            clsValidaciones.RedirectPagina(sUrl);
        }
    }

    protected void btnRegistro_Click(object sender, EventArgs e)
    {
        Label lblurltopflight = (Label)this.Parent.FindControl("lbltopflight");

        if (lblurltopflight.Text != "")
        {
            clsValidaciones.RedirectPagina(lblurltopflight.Text.Replace("loginttq", "login"));

        }
    }

    protected void lbOlvido_Click(object sender, EventArgs e)
    {
        Label lblurl3 = (Label)this.Parent.FindControl("lbltopflight");
        string strDir = clsValidaciones.GetKeyOrAdd("urlpagtopflight", "162.248.52.117");
        clsValidaciones.RedirectPagina("http://" + strDir + "/Pagina/Presentacion/loginttq.aspx?Externo=1&modal_vuelos=0&TB=Buscar%20vuelos&ETIPOSALIDA=Nacional&txtFechaMultiO1=2014-01-15&txt2VFechaMulti=2014-01-23&txt_Multi_O1=BOG%20&txt_Multi_D1=MDE%20&ddlMultiAdultos=1%20&ddlMultiNinios=0&ddlMultiBebes=0&");
    
    }
    protected void btnEnviar_Click(object sender, EventArgs e)
    {

        {


            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("jbernal@ssoftcolombia.com", "Tutiquete");
            mail.Subject = "INSCRIPCION TOP-FLIGHT";
            mail.Body = "NUEVO USUARIO SOLICITANDO SUSCRIPCION A TOP-FLIGHT " + TextUsuario.Text;
            mail.To.Add(new MailAddress("cleon@ssoftcolombia.com", "gerencia@tutiquete.com"));

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("jbernal@ssoftcolombia.com", "ssoftcolombia00");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
}