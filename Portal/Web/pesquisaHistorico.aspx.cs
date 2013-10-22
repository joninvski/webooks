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



public partial class Web_pesquisaHistorico : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void PesquisaHistorico(object sender, EventArgs e)
    {
        WEBooksBDService servicoBaseDados = new WEBooksBDService();

        WEBooksBizTalk_WS.Utilizador user = (WEBooksBizTalk_WS.Utilizador)this.Session["utilizador"];
        

        if (user == null || user.TipoUtilizador.CompareTo("Gestor") != 0)
        {
            DivUserInexistente.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            DivUserInexistente.InnerText = "Necessita de estar logado com privilégios de gestor para realizar essa tarefa!";
            return;
        }
        else {
            //iniciacao da tabela
            DataTable resposta = new DataTable();

            resposta.Columns.Add("ImageUrl");
            resposta.Columns.Add("ISBN");
            resposta.Columns.Add("Titulo");
            resposta.Columns.Add("Categoria");
            resposta.Columns.Add("Autores");
            resposta.Columns.Add("Editora");
            resposta.Columns.Add("AnoEdicao");
            resposta.Columns.Add("PrecoVenda");

            //query de livros a amazon e companhia
            Livro[] bookList = servicoBaseDados.HistoricoComprasLivros(TBUserName.Text); //comando de acesso a BD

            //preenchimento da tabela
            foreach (Livro livro in bookList)
            {
                Object[] temp = new Object[8];

                temp[0] = livro.UrlImagem;
                temp[1] = livro.ISBN;
                temp[2] = livro.Titulo;
                temp[3] = livro.Categoria;
                temp[5] = livro.Editora;
                temp[6] = livro.AnoEdicao;
                temp[7] = livro.PrecoVenda;

                resposta.Rows.Add(temp);
            }

            HistoricoGrid.DataSource = resposta;
            HistoricoGrid.DataBind();
        }
    }
}

