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

    protected void MostraLivrosMaisPesquisados(object sender, EventArgs e)
    {
        WEBooksBDService servicoBaseDados = new WEBooksBDService();
       
        
        Livro[] listaLivros = servicoBaseDados.MostraLivrosMaisPesquisados(10);
        this.ApresentaResultados(listaLivros);
    }

    protected void MostraLivrosMaisComprados(object sender, EventArgs e)
    {
        WEBooksBDService servicoBaseDados = new WEBooksBDService();

        
        Livro[] listaLivros = servicoBaseDados.MostraLivrosMaisComprados(10);
        this.ApresentaResultados(listaLivros);
    }

    protected void ApresentaResultados(Livro[] listaLivros)
    {
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
    }
}



