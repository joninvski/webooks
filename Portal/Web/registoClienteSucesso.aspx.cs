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

public partial class Web_registoClienteSucesso : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        lbNome.Text = ((string[])this.Session["cliente"])[0];
        lbUsername.Text = ((string[])this.Session["cliente"])[1];
        lbTelefone.Text = ((string[])this.Session["cliente"])[2];
        lbNrCartaoCredito.Text = ((string[])this.Session["cliente"])[3];
        lbDataValidade.Text = ((string[])this.Session["cliente"])[4];
        lbNumero.Text = ((string[])this.Session["cliente"])[5];
        lbRua.Text = ((string[])this.Session["cliente"])[6];
        lbCidade.Text = ((string[])this.Session["cliente"])[7];
        lbEstado.Text = ((string[])this.Session["cliente"])[8];
        lbZipCode.Text = ((string[])this.Session["cliente"])[9];
        lbPais.Text = ((string[])this.Session["cliente"])[10];

        this.Session.Remove("cliente");
    }
}

