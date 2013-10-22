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
//using WEBooksAmazon;
using WEBooksBD;


public partial class Web_procura : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WEBooksBizTalk_WS.Utilizador utilizador = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];
        

        if (utilizador == null || utilizador.TipoUtilizador != "Gestor")
        {
            ErrosDadosGestao.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            ErrosDadosGestao.InnerText = "Necessita de ser um Gestor";
            return;
        }
        else {
            TabelaDadosGestao.Visible = true;
            this.MostraDadosGestao(null, null);
        }       
    }

    protected void MostraDadosGestao(object sender, EventArgs e)
    {        
       WEBooksBDService baseDados = new WEBooksBDService();

       DadosGestao dadosGestao = baseDados.MostraDadosGestao();

       lbLivrosComprados.Text = dadosGestao.NumComprados + "";
       lbLivrosPesquisados.Text = dadosGestao.NumPesquisados + "";
       lbLivrosCompradosComDesconto.Text = dadosGestao.NumComDesconto + "";
       lbLivrosCompradosSemDesconto.Text = dadosGestao.NumSemDesconto + "";
    }
  

}

