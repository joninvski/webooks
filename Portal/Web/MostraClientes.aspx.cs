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
//using WEBooksBizTalk_WS;


public partial class Web_pesquisaHistorico : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Session.Add("Cliente", null);
        this.MostraClientes(null, null);
    }    

    protected void MostraClientes(object sender, EventArgs e)
    {
        WEBooksBDService servicoBaseDados = new WEBooksBDService();

        WEBooksBizTalk_WS.Utilizador utilizador = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];
        

        if (utilizador == null || utilizador.TipoUtilizador != "Gestor")
        {
            DivUserInexistente.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            DivUserInexistente.InnerText = "Necessita de ser um Gestor";
            return;
        }

        DataTable Clientes = new DataTable();

        Clientes.Columns.Add("IdCliente");
        Clientes.Columns.Add("Nome");
        Clientes.Columns.Add("Username");
        Clientes.Columns.Add("Estado");

        //query de livros a amazon e companhia
        Cliente[] listaClientes = servicoBaseDados.MostraClientes(); //comando de acesso a BD

        if (listaClientes == null)
        {
            DivUserInexistente.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            DivUserInexistente.InnerText = "Nao existem Clientes Registados";
            return;
        }

        //preenchimento da tabela
        foreach (Cliente cliente in listaClientes)
        {
            Object[] cliTable = new Object[4];

            cliTable[0] = cliente.IdCliente;
            cliTable[1] = cliente.Nome;
            cliTable[2] = cliente.Username;
            cliTable[3] = cliente.Estado;

            Clientes.Rows.Add(cliTable);
        }
        ListaClientes.DataSource = Clientes;
        ListaClientes.DataBind();

        this.Session.Add("ListaClientes", listaClientes);
    }

    protected void GerirClientes(object sender, DataGridCommandEventArgs e)
    {
        TableCell idCliente = e.Item.Cells[0];
        TableCell username = e.Item.Cells[2];

        if (((LinkButton)e.CommandSource).CommandName == "Detalhes")
        {
            this.MostraDetalhesCliente(idCliente.Text);
        }
        else if (((LinkButton)e.CommandSource).CommandName == "Apagar")
        {
            this.ApagaCliente(username.Text);
        }
        else {
            DivUserInexistente.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            DivUserInexistente.InnerText = "Problemas comunicacao: Tente novamente";

            this.Session.Add("Cliente", null);

            return;
        }
    }

    protected void MostraDetalhesCliente(string idCliente)
    {
        //GridViewRow row = ListaClientes.SelectedRow;
        string username = null;
        Cliente[] listaClientes = (Cliente[])this.Session["ListaClientes"];

        foreach (Cliente cli in listaClientes)
        {
            if (cli.IdCliente == idCliente)
            {

                lbNome.Text = cli.Nome;
                lbUsername.Text = cli.Username;
                lbTelefone.Text = cli.Telefone;
                lbNrCartaoCredito.Text = cli.NumCartaoCredito;
                lbDataValidade.Text = cli.DataValidadeCartaoValidade;
                lbNumero.Text = cli.Numero;
                lbRua.Text = cli.Address;
                lbCidade.Text = cli.City;
                lbEstado.Text = cli.State;
                lbZipCode.Text = cli.ZIPcode;
                lbPais.Text = cli.Country;
                lbEstadoCliente.Text = cli.Estado;

                username = cli.Username.Trim();

                break;
            }
        }

        this.Session.Add("Cliente", username);
    }

    protected void BotaoApagaCliente(object sender, EventArgs e)
    {
        this.ApagaCliente((string)this.Session["Cliente"]);
    }

    protected void ApagaCliente(string username)
    {
        WEBooksBDService servicoBaseDados = new WEBooksBDService();

        try
        {
            servicoBaseDados.ApagaCliente(username);
        }
        catch (Exception ex)
        {
            DivUserInexistente.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            DivUserInexistente.InnerText = "O utilizador tem encomendas pendentes";
            return;
        }

        this.Session.Add("Cliente", null);
        this.Response.Redirect("~/Web/MostraClientes.aspx", true);
    }

    protected void VoltaClientes(object sender, EventArgs e)
    {
        this.Session.Add("Cliente", null);
        this.Response.Redirect("~/Web/MostraClientes.aspx", true);
        //this.PesquisaHistoricoEncomendas();

    }
}



