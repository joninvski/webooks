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
using WEBooksBizTalk_WS;



public partial class Web_checkOut : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        WEBooksBDService servicoBaseDados = new WEBooksBDService();

        DataTable resposta = new DataTable();

        resposta.Columns.Add("ImageUrl");
        resposta.Columns.Add("ISBN");
        resposta.Columns.Add("Titulo");
        resposta.Columns.Add("Categoria");
        resposta.Columns.Add("Autores");
        resposta.Columns.Add("Editora");
        resposta.Columns.Add("AnoEdicao");
        resposta.Columns.Add("PrecoVenda");
       // resposta.Columns.Add("TempoEntrega");

        //buscar o user a sessao
        WEBooksBizTalk_WS.Utilizador utilizador = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];
        

        /*if (utilizador == null)
        {
            //mandar excepcao nao devia tar aki

            this.Response.Write("<h1>NAO LOGADO</h1>");

            return;
        }
        */

        Livro[] carrinhoCompras = null;
        try
        {
            carrinhoCompras = servicoBaseDados.ListaCarrinhoCompras(utilizador.Username.Trim());

            foreach (Livro livro in carrinhoCompras)
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

            CarrinhoGrid.DataSource = resposta;
            CarrinhoGrid.DataBind();            

            this.Session["carrinhoVazio"] = "carrinho";
        }
        catch (Exception excarro)
        {
            this.Session["carrinhoVazio"] = "";
            ErroCarrinho.Attributes.Add("style", "color:Red;");
            ErroCarrinho.InnerText = "O Carrinho de Compras esta Vazio";
        }
    }

    protected void FazerCheckOut(object sender, EventArgs e) {

        WEBooksBizTalk_WS.Utilizador utilizador = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];        


        if (utilizador == null)
        {
            //mandar excepcao nao devia tar aki
            this.Response.Write("<h1>NAO LOGADO</h1>");
            return;
        }

        string username = utilizador.Username;

        WEBooksBiztalk servicoBizTalk = new WEBooksBiztalk();

        try
        {
            servicoBizTalk.RealizaEncomenda(ref username);
        }
        catch (Exception exCar)
        {
             this.Session["carrinhoVazio"] = "";
            ErroCarrinho.Attributes.Add("style", "color:Red;");
            ErroCarrinho.InnerText = "O Carrinho de Compras esta Vazio";
        }
    }

    protected void CarrinhoGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        WEBooksBDService servicoBaseDados = new WEBooksBDService();

        WEBooksBizTalk_WS.Utilizador utilizador = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];        

        GridViewRow row = CarrinhoGrid.SelectedRow;

        servicoBaseDados.RemoveLivroDoCarrinhoCompras(utilizador.Username, row.Cells[1].Text, "amazon");

        this.Response.Redirect("~/Web/carrinhoCompras.aspx", true);
    }
}

