using
System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Services.Protocols;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using WEBooksAmazon;
//using WEBooksBD;
using WEBooksBizTalk_WS;

public partial class Web_procura : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Erros.InnerText = "";
        Page.SetFocus("PesquisarButton");

        idAnteriorPagina.Visible = false;
        idProximaPagina.Visible = false;
        paginaActual.Visible = false;

        this.Session.Add("Livro", null);

        /*
        if (this.Session["page"]) { 
            this.Session.Add("page", "")
        }*/
    }


    protected void OperacaoLivro(object sender,  GridViewCommandEventArgs e) {

        int index = Convert.ToInt32(e.CommandArgument);

        // Retrieve the row that contains the button clicked 
        // by the user from the Rows collection.
        GridViewRow row = PesquisaGrid.Rows[index];

        // get isbn do livro adicionado ao carrinho  
        ListItem ISBN = new ListItem();
        ISBN.Text = Server.HtmlDecode(row.Cells[1].Text); 
        
        if (e.CommandName == "AddCart") {
            this.AddToCart(ISBN.Text);
        }
        else if (e.CommandName == "Detalhes")
        {
            this.MostraDetalhesLivro(ISBN.Text);
        }
        else
        {
            Erros.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            Erros.InnerText = "Problemas comunicacao: Tente novamente";

            this.Session.Add("Livro", null);

            return;
        }
        
    }

    protected void Pesquisa(object sender, EventArgs e)
    {        
        string keyword = KeywordBox.Text;
        string autor = AutorBox.Text;
        string isbn = IsbnBox.Text;
        string titulo = TituloBox.Text;

        bool pesquisa = FazPesquisa("1", isbn, titulo, keyword, autor);

        if (pesquisa)
        {
            Erros.InnerText = "";
            this.Session.Add("page", "1");
            this.Session.Add("autor", autor);
            this.Session.Add("isbn", isbn);
            this.Session.Add("keyword", keyword);
            this.Session.Add("titulo", titulo);

            idProximaPagina.Visible = true;
            paginaActual.Visible = true;
            paginaActual.Text = " | Pagina: 1 | ";
        }
    }

    protected bool FazPesquisa(string page, string isbn, string titulo, string keyword, string autor)
    {
        if (keyword == "" && autor == "" && isbn == "" && titulo == "")
        {
            Erros.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            Erros.InnerText = "Tem que escrever em pelo menos um campo!";
            idAnteriorPagina.Visible = false;
            idProximaPagina.Visible = false;
            paginaActual.Visible = false;
            PesquisaGrid.Visible = false;
            return false;
        }

        //query de livros a amazon e companhia

        WEBooksBiztalk amazon = new WEBooksBiztalk();
        Book[] resultado = null;
        try
        {
            resultado  = amazon.PesquisaLivros(page, keyword, autor, titulo, isbn);
        }
        catch (Exception) {
            Erros.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            Erros.InnerText = "A sua pesquisa nao retornou resultados";
            idAnteriorPagina.Visible = false;
            idProximaPagina.Visible = false;
            paginaActual.Visible = false;
            PesquisaGrid.Visible = false;
            return false;
        }
        PesquisaGrid.Visible = true;
        this.MostraPagina(resultado);

        return true;
    }

    protected void MostraPagina(Book[] resultado)
    {        
        if (resultado != null)
        {
            //iniciacao da tabela
            DataTable resposta = new DataTable();
            resposta.Columns.Add("ImageUrl");
            resposta.Columns.Add("ISBN");
            resposta.Columns.Add("Titulo");
            resposta.Columns.Add("Autores");
            resposta.Columns.Add("Editora");
            resposta.Columns.Add("PrecoVenda");

        
            //preenchimento da tabela
            foreach (Book livro in resultado)
            {
                Object[] temp = new Object[6];

                if (!livro.ImageUrl.Contains("/"))
                {
                    temp[0] = "~/App_Themes/webook.jpg";
                    livro.ImageUrl = "~/App_Themes/webook.jpg";
                }
                else
                {
                    temp[0] = livro.ImageUrl;
                }

                temp[1] = livro.ISBN;
                temp[2] = livro.Titulo;

                if (livro.Autor == null)
                    temp[3] = "nao ha autor";
                else
                {
                    temp[3] += livro.Autor;
                }

                temp[4] = livro.Editora;
                temp[5] = livro.PrecoVenda;

                resposta.Rows.Add(temp);
            }

            PesquisaGrid.DataSource = resposta;
            PesquisaGrid.DataBind();
        }

        this.Session.Add("listaLivro", resultado);
    }

    protected void BotaoAddCart(object sender, EventArgs e)
    {
        string ISBN = (string)this.Session["LivroISBN"];

        this.AddToCart(ISBN);
    }

    protected void AddToCart(string ISBN)
    {

        WEBooksBiztalk baseDados = new WEBooksBiztalk();
        Utilizador utilizador = (Utilizador)Session["utilizador"];

        if (utilizador == null)
        {
            //mandar excepcao nao devia tar aki
            Erros.Attributes.Add("style", "color:Red; text-align:center; font-weight: bold; ");
            Erros.InnerText = "Necessita de estar logado para realizar essa opção!";
            return;
        }

        //GridViewRow row = PesquisaGrid.SelectedRow;

        //agora vou buscar o segundo argumento que e o ISBN por exemplo
        //e necessario ir buscar o id do livro para o ir buscar a BD

        try
        {
            baseDados.InsereLivroCarrinhoCompras(utilizador.Username, ISBN, 1);
        }
        catch (SoapException ex)
        {
            Erros.Attributes.Add("style", "text-align:center; font-weight: bold; ");
            Erros.InnerText = "O livro \"" + ISBN + "\" foi adicionada com sucesso!";
            return;
        }
        catch (Exception excart)
        {
            if (excart.Message.Contains("LivroExistenteNoCarrinhoException"))
            {
                Erros.Attributes.Add("style", "text-align:center; font-weight: bold; ");
                Erros.InnerText = "O livro \"" + ISBN + "\" ja esta no carrinho de compras!";
                return;
            }
            else
            {
                Erros.Attributes.Add("style", "text-align:center; font-weight: bold; ");
                Erros.InnerText = "O livro \"" + excart + "\" NÃO foi adionado ao carrinho com sucesso!";
                return;
            }
        }
        
    }

    /*
    protected Livro[] listaBookTolistaLivro(Item[] listaBook)
    {
        Livro[] listalivro = new Livro[listaBook.Length];

        int i = 0;
        foreach (Item book in listaBook)
        { 
                listalivro[i] = BookToLivro(book);
                i++;
        }
        return listalivro;
    }

    protected Livro BookToLivro(Item Book) 
    {
        Livro livro = new Livro();

        if (!Book.ImageUrl.Contains("/"))
        {
            livro.UrlImagem = "~/App_Themes/webook.jpg";
        }
        else
        {
            livro.UrlImagem = Book.ImageUrl;
        }

        livro.ISBN = Book.Isbn;
        livro.Titulo = Book.Titulo;
        livro.Categoria = Book.Categoria;
        livro.Autores = Book.Autor;
        livro.Editora = Book.Editora;
        livro.AnoEdicao = Book.Ano;
        livro.PrecoVenda = Book.Preco.ToString();
        livro.PrecoSemDesconto = "$0.0";
        livro.TempoEntrega = Book.Disponibilidade;
        livro.Desconto = false;
        livro.NomeFornecedor = "amazon";

        return livro;
    }    

    protected string FiltraCategoria(string categorias) {
        return categorias.Remove(categorias.IndexOf("Subjects"));
    }
    
    */

    protected void PaginaAnterior(object sender, EventArgs e)
    {
        string page = (string)this.Session["page"];
        string autor = (string)this.Session["autor"];
        string isbn = (string)this.Session["isbn"];
        string keyword = (string)this.Session["keyword"];
        string titulo = (string)this.Session["titulo"];

        FazPesquisa((int.Parse(page) - 1) + "", isbn, titulo, keyword, autor);
        this.Session.Add("page", (int.Parse(page) - 1) + "");

        if (int.Parse(page) - 1 != 1)
        {
            idAnteriorPagina.Visible = true;
        }
        idProximaPagina.Visible = true;
        paginaActual.Visible = true;
        paginaActual.Text = " | Pagina: " + (int.Parse(page) - 1) + " | ";
    }

    protected void PaginaSeguinte(object sender, EventArgs e)
    {
        string page = (string)this.Session["page"];
        string autor = (string)this.Session["autor"];
        string isbn = (string)this.Session["isbn"];
        string keyword = (string)this.Session["keyword"];
        string titulo = (string)this.Session["titulo"];

        FazPesquisa((int.Parse(page) + 1) + "", isbn, titulo, keyword, autor);

        this.Session.Add("page", (int.Parse(page) + 1) + "");

        idAnteriorPagina.Visible = true;
        idProximaPagina.Visible = true;
        paginaActual.Visible = true;
        paginaActual.Text = " | Pagina: " + (int.Parse(page) + 1) + " | ";
    }

    protected void MostraDetalhesLivro(string ISBN)
    {
        Book[] listaLivros = (Book[])this.Session["listaLivro"];

        foreach (Book liv in listaLivros)
        {
            if (liv.ISBN == ISBN)
            {
                imgLivro.ImageUrl = liv.ImageUrl;
                lbISBN.Text = ISBN;
                lbTitulo.Text = liv.Titulo;
                lbCategoria.Text = liv.Categoria;
                lbAutores.Text = liv.Autor;
                lbEditora.Text = liv.Editora;
                lbAnoEdicao.Text = liv.Ano;
                lbPrecoVenda.Text = liv.PrecoVenda + "";
                lbTempoEntrega.Text = liv.Disponibilidade;
                lbNomeFornecedore.Text = liv.Fornecedor;

                break;
            }
        }

        this.Session.Add("Livro", ISBN);
        this.Session.Add("LivroISBN", ISBN);
    }

    protected void VoltaLivros(object sender, EventArgs e)
    {
        this.Session.Add("Livro", null);
        this.Session.Add("LivroISBN", null);

        string page = (string)this.Session["page"];
        string autor = (string)this.Session["autor"];
        string isbn = (string)this.Session["isbn"];
        string keyword = (string)this.Session["keyword"];
        string titulo = (string)this.Session["titulo"];

        if (int.Parse(page) != 1)
        {
            idAnteriorPagina.Visible = true;
        }
        idProximaPagina.Visible = true;
        paginaActual.Visible = true;

        paginaActual.Text = " | Pagina: " + page + " | ";

        this.MostraPagina((Book[])this.Session["listaLivro"]);
    }

}

