using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WEBooksBD;
//using WEBooksAmazon;
using WEBooksBizTalk_WS;


public partial class MasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e){
    }

    protected void FazerLogin(object sender, EventArgs e)
    {
        WEBooksBiztalk servicoBD = new WEBooksBiztalk();

        ErroLogin.InnerText = "";

        WEBooksBizTalk_WS.Utilizador utilizador = null;
       

        try
        {
            utilizador = servicoBD.LoginUtilizador(LoginBox.Text, PasswordBox.Text);
            this.Session.Add("utilizador", utilizador);
        }
        catch (Exception excp) {
            ErroLogin.Attributes.Add("style", "color:Red;");
            ErroLogin.InnerText = "Login Errado";
            return;
        }

        if(utilizador.TipoUtilizador.Equals("Cliente")){
            this.Response.Redirect("~/Web/carrinhoCompras.aspx", true);
        }else{
            this.Response.Redirect("~/Web/pesquisaHistoricoGestor.aspx", true);
        }
    }

    protected void LoginType_Authenticate(object sender, AuthenticateEventArgs e)
    {
        e.Authenticated = true;
    }

    protected void FazerLogOut(object sender, EventArgs e)
    {
        this.Session.RemoveAll();
        this.Response.Redirect("~/index.aspx",true);
    }
}

