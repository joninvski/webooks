using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WEBooksBD;

using WCPierce.Web.UI.WebControls;


/// <summary>
/// Summary description for Basics.
/// </summary>
public partial class MostraLocalizacao : System.Web.UI.Page
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
        WEBooksBizTalk_WS.Utilizador utilizador = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];

        this.Session.Add("UserMapa", null);

        if (utilizador == null || utilizador.TipoUtilizador != "Gestor")
        {
            DivUserInexistente.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            DivUserInexistente.InnerText = "Necessita de estar logado como Gestor";

            gMap.Visible = false;
            
            return;
        }
        gMap.Visible = true;

        WEBooksBDService servicoBD = new WEBooksBDService();

        Cliente[] listaClientes = servicoBD.MostraClientes();

        GPoint gp = new GPoint(float.Parse(listaClientes[0].Longitude), float.Parse(listaClientes[0].Latitude));

        GMarker gm;

        foreach (Cliente cli in listaClientes) {

            GPoint ponto = new GPoint( float.Parse(cli.Longitude), float.Parse(cli.Latitude) );

            gm = new GMarker(ponto, "<a href=\"./pesquisaHistoricoEncomendas.aspx?Username=" + cli.Username + ">" + cli.Username + "</a>");
            gMap.Overlays.Add(gm);
        }

       // gMap.AddControl(new GSmallMapControl());
        gMap.AddControl(new GLargeMapControl());

        gMap.AddControl(new GMapTypeControl());
      
        gMap.CenterAndZoom(gp, 4);
      
	}

	#region Web Form Designer generated code
	override protected void OnInit(EventArgs e)
	{
		//
		// CODEGEN: This call is required by the ASP.NET Web Form Designer.
		//
		InitializeComponent();
		base.OnInit(e);
	}
	
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{    
	}
	#endregion
}

