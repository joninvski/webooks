using
System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WEBooksNY_WS;
using WEBooksBD;


public partial class Web_procura : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MostraComDesconto();
    }

    protected bool MostraComDesconto()
    {
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
        resposta.Columns.Add("TempoEntrega");

        //query de livros a amazon e companhia

        WEBooksNY newYorkTimes = new WEBooksNY();

        WEBooksNY_WS.LivroPromocao[] listaPromocao = newYorkTimes.LivrosTopSellers();

        WEBooksBDService baseDados = new WEBooksBDService();

        baseDados.InsereListaTopSellers(NYtoBD(listaPromocao));

        //preenchimento da tabela
        foreach (WEBooksNY_WS.LivroPromocao livro in listaPromocao)
        {
            Object[] temp = new Object[9];

           temp[0] = "~/App_Themes/webook3.jpg";
           
            temp[1] = "indisponivel";
            temp[2] = livro.Titulo;

            temp[3] = "nao ha categoria";
            
            temp[4] = livro.NomeAutor;


            temp[5] = "indisponivel";
            temp[6] = "indisponivel";
            temp[7] = "indisponivel";
            temp[8] = "indisponivel";

            // temp[9] = "NAO HA DESCRICAO";

            resposta.Rows.Add(temp);
        }

        DescontoGrid.DataSource = resposta;
        DescontoGrid.DataBind();

        return true;
    }

    protected WEBooksBD.LivroPromocao[] NYtoBD(WEBooksNY_WS.LivroPromocao[] listaPromocao){

        WEBooksBD.LivroPromocao[] listaLivros = new WEBooksBD.LivroPromocao[listaPromocao.Length];
        int i = 0;

        foreach (WEBooksNY_WS.LivroPromocao livroNY in listaPromocao) { 
            WEBooksBD.LivroPromocao livroBD = new WEBooksBD.LivroPromocao();

            livroBD.NomeAutor = livroNY.NomeAutor;
            livroBD.TipoCapa = livroNY.TipoCapa;
            livroBD.Titulo = livroNY.Titulo;

            listaLivros[i++] = livroBD;
        }

        return listaLivros;
    }
}
