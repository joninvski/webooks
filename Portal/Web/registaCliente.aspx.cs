using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using WEBooksBD;
using WEBooksBizTalk_WS;

public partial class Web_registaCliente : System.Web.UI.Page
{
    protected void RegistaCliente(object sender, EventArgs e)
    {
        WEBooksBiztalk servico = new WEBooksBiztalk();
       
            servico.RegistaCliente(tbNome.Text, tbUserName.Text, tbPassword.Text, tbTelefone.Text, tbNumero.Text, tbRua.Text, tbCidade.Text,
                tbEstado.Text, tbZipCode.Text, tbPais.Text, "longitude", "latitude", tbNrCartaoCredito.Text, tbDataValidade.Text);
        
        string[] cliente = new string[11]{ tbNome.Text, tbUserName.Text, tbTelefone.Text, tbNrCartaoCredito.Text, tbDataValidade.Text,
            tbNumero.Text, tbRua.Text, tbCidade.Text, tbEstado.Text, tbZipCode.Text, tbPais.Text};

        this.Session.Add("cliente", cliente);

        this.Response.Redirect("~/Web/registoClienteSucesso.aspx", true);
    }
}

