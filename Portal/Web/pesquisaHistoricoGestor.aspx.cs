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
       // WEBooksBizTalk_WS.Utilizador utilizador = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];

        WEBooksBizTalk_WS.Utilizador utilizador = new WEBooksBizTalk_WS.Utilizador();
        utilizador.TipoUtilizador = "Gestor";

        if (utilizador == null || utilizador.TipoUtilizador != "Gestor")
        {
            lbInsiraLogin.Visible = false;
            tbUserName.Visible = false;
            btPesquisaHistorico.Visible = false;
            rbCliente.Visible = false;
            rbLivro.Visible = false;
            rbTodas.Visible = false;

            DivUserInexistente.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            DivUserInexistente.InnerText = "Necessita de ser um Gestor";
            return;
        }
        else {
            lbInsiraLogin.Visible = true;
            tbUserName.Visible = true;
            btPesquisaHistorico.Visible = true;
            rbCliente.Visible = true;
            rbLivro.Visible = true;
            rbTodas.Visible = true;
        }

        DivUserInexistente.InnerText = "";
        this.Session.Add("Encomenda", null);
    }

    protected void PesquisaHistoricoEncomendas(object sender, EventArgs e)
    {
        WEBooksBDService servicoBaseDados = new WEBooksBDService();
               
        WEBooksBizTalk_WS.Utilizador utilizador = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];

        if (utilizador == null )
        {
            DivUserInexistente.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            DivUserInexistente.InnerText = "Necessita de estar logado";
            return;
        }
        
        DataTable Encomendas = new DataTable();

        Encomendas.Columns.Add("IdEncomenda");
        Encomendas.Columns.Add("Cliente");
        Encomendas.Columns.Add("DataCriacao");
        Encomendas.Columns.Add("Estado");
        Encomendas.Columns.Add("ValorTotal");

        Encomenda[] listaEncomendas = null;
        if (rbCliente.Checked == true)
        {
            listaEncomendas = servicoBaseDados.MostraEncomendasCliente(tbUserName.Text); //comando de acesso a BD
        }else if(rbLivro.Checked == true){
            listaEncomendas = servicoBaseDados.MostraEncomendasLivro(tbUserName.Text); //comando de acesso a BD
        }
        else if (rbTodas.Checked == true) {
            listaEncomendas = servicoBaseDados.MostraListaEncomendas();
        }

        if (listaEncomendas == null)
        {
            this.Session.Add("ListaEncomendas", null);
            this.Session.Add("mostraEncomendas", null);
            this.Session.Add("username", null);

            DivUserInexistente.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            DivUserInexistente.InnerText = "Utilizador não tem encomendas \\ Livro nao foi comprado ";
            return;
        }

        //preenchimento da tabela
        foreach (Encomenda enc in listaEncomendas)
        {
            Object[] encTable = new Object[5];

            encTable[0] = enc.IdEncomenda;
            encTable[1] = enc.Cliente.Username;
            encTable[2] = enc.DataCriacao;
            encTable[3] = enc.Estado;
            encTable[4] = enc.ValorTotal;

            Encomendas.Rows.Add(encTable);
        }
        HistoricoEncomendas.DataSource = Encomendas;
        HistoricoEncomendas.DataBind();

        this.Session.Add("ListaEncomendas", listaEncomendas);
        this.Session.Add("mostraEncomendas", "mostra");
        this.Session.Add("username", tbUserName.Text);
        
        
    }

    protected void MostraDetalhes(string idEncomenda)
    {
        DivUserInexistente.InnerText = "";

        WEBooksBizTalk_WS.Utilizador utilizador = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];

        
        string username = (string)this.Session["username"];

        Encomenda[] listaEncomendas = (Encomenda[])this.Session["ListaEncomendas"];

        Livro[] listaLivros = null;

        EstadoEncomenda[] listaEstados = null;

        foreach (Encomenda enc in listaEncomendas) {
            if (enc.IdEncomenda == idEncomenda)
            {
                listaLivros = enc.ListaLivros;
                listaEstados = enc.HistoricoEstado;

                lbidEncomenda.Text = enc.IdEncomenda;
                lbNomeComprador.Text = enc.Cliente.Username;
       //         lbEstado.Text = enc.Estado;
                lbDataCriacao.Text = enc.DataCriacao;
                lbValorTotal.Text = enc.ValorTotal + "";
            }
        }

        //Lista de Estados
        DataTable estados = new DataTable();

        estados.Columns.Add("contador");
        estados.Columns.Add("nomeEstado");
        estados.Columns.Add("dataAlteracao");

        //preenchimento da tabela
        foreach (EstadoEncomenda estadoEnc in listaEstados)
        {
            Object[] tempEstado = new Object[3];

            tempEstado[0] = estadoEnc.Contador;
            tempEstado[1] = estadoEnc.NomeEstado;
            tempEstado[2] = estadoEnc.DataAlteracao;

            estados.Rows.Add(tempEstado);
        }

        TabelaEstados.DataSource = estados;
        TabelaEstados.DataBind();


        //Lista de Livros
        DataTable resposta = new DataTable();

        resposta.Columns.Add("ImageUrl");
        resposta.Columns.Add("ISBN");
        resposta.Columns.Add("Titulo");
        resposta.Columns.Add("Categoria");
        resposta.Columns.Add("Autores");
        resposta.Columns.Add("Editora");
        resposta.Columns.Add("AnoEdicao");
        resposta.Columns.Add("PrecoVenda");

        //preenchimento da tabela
        foreach (Livro livro in listaLivros)
        {
            Object[] temp = new Object[8];

            temp[0] = livro.UrlImagem;
            temp[1] = livro.ISBN;
            temp[2] = livro.Titulo;
            temp[3] = livro.Categoria;
            temp[4] = livro.Autores;
            temp[5] = livro.Editora;
            temp[6] = livro.AnoEdicao;
            temp[7] = livro.PrecoVenda;

            resposta.Rows.Add(temp);
        }

        LivroTabela.DataSource = resposta;
        LivroTabela.DataBind();

        this.Session.Add("Encomenda", resposta);
    }

    protected void CancelaEncomenda(string idEncomenda)
    {
        WEBooksBizTalk_WS.Utilizador utilizador = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];
        WEBooksBDService servicoBaseDados = new WEBooksBDService();
        WEBooksBizTalk_WS.WEBooksBiztalk servicoBiztalk = new WEBooksBizTalk_WS.WEBooksBiztalk();

        WEBooksBD.Encomenda[] listaEncomendas = (WEBooksBD.Encomenda[])this.Session["ListaEncomendas"];
        WEBooksBD.Encomenda encomenda = null;
        foreach (WEBooksBD.Encomenda enc in listaEncomendas) {
            if (enc.IdEncomenda == idEncomenda) {
                encomenda = enc;
                break;
            }
        }

        WEBooksBizTalk_WS.Encomenda encomendaBiz = new WEBooksBizTalk_WS.Encomenda();

        encomendaBiz.IdEncomenda = encomenda.IdEncomenda;

        try
        {
            servicoBiztalk.CancelaEncomenda(ref encomendaBiz);
        }
        catch (Exception)
        {
            DivUserInexistente.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            DivUserInexistente.InnerText = "A encomenda " + encomendaBiz.IdEncomenda + "ja se encontra cancelada";
        }

        this.PesquisaHistoricoEncomendas(null, null);
    }

    protected void OperacaoEncomenda(object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);

        // Retrieve the row that contains the button clicked 
        // by the user from the Rows collection.
        GridViewRow row = HistoricoEncomendas.Rows[index];

        // get isbn do livro adicionado ao carrinho  
        ListItem IdEncomenda = new ListItem();
        IdEncomenda.Text = Server.HtmlDecode(row.Cells[0].Text);

        if (e.CommandName == "Cancelar")
        {
            this.CancelaEncomenda(IdEncomenda.Text);
        }
        else if (e.CommandName == "MostaDestalhes")
        {
            this.MostraDetalhes(IdEncomenda.Text);
        }
    }

    protected void VoltaEncomendas(object sender, EventArgs e)
    {       
        this.Session.Add("Encomenda", null);
        this.PesquisaHistoricoEncomendas(sender, e);
        //this.Response.Redirect("~/Web/pesquisaHistoricoGestor.aspx", true);
        //this.PesquisaHistoricoEncomendas();

    }
}



