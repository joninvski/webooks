using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;

//using com.amazon.webservices;

using SEI;



[WebService(Namespace = "http://webooks.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WEBooksAmazonWS : System.Web.Services.WebService
{
    public WEBooksAmazonWS () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]
    public Item[] ProcuraItem(string page, string keyword, string author, string title, string isbn) {

        bool isFalse = ValidaParametros(ref keyword, ref author, ref title, ref isbn);

        if (isFalse) {
            return null;
            //throw new ValidaParametrosException();
        }

        WEBooksAmazon amazonECS = new WEBooksAmazon();
        com.amazon.webservices.Item[] pesquisaAmazon;

        try
        {
           pesquisaAmazon = amazonECS.ProcuraItem(page, keyword, author, title, isbn);
        }
        catch (EfectuaPedidoException ex) {
            return new Item[1];
        }

       // Item[] livros = new Item[pesquisaAmazon.Length];

        ArrayList livros = new ArrayList();

        foreach (com.amazon.webservices.Item i in pesquisaAmazon)
        {
            Item livro = new Item();
            livro.Fornecedor = "Amazon";

            try {
                if (null == i.ItemAttributes.Title)
                    livro.Titulo = "Título Desconhecido\n\r";
                else
                    livro.Titulo = i.ItemAttributes.Title;
            }
            catch (NullReferenceException e) {
                livro.Titulo = "Título Desconhecido\n\r";
            }

            try {
                if (null == i.ItemAttributes.Author)
                    livro.Autor = "Autor Desconhecido\n\r";
                else
                    foreach (string aux in i.ItemAttributes.Author)
                        livro.Autor += (aux + "\n\r");
            }
            catch (NullReferenceException e) {
                livro.Autor = "Autor Desconhecido";
            }

            try {
                if (null == i.MediumImage.URL)
                    livro.ImageUrl = "Imagem Indisponível\n\r";
                else
                    livro.ImageUrl = i.MediumImage.URL;
            }
            catch (NullReferenceException e) {
                livro.ImageUrl = "Imagem Indisponível";
            }

            try {
                if (null == i.ItemAttributes.ISBN)
                {
                    continue;
                    livro.Isbn = "ISBN Desconhecido\n\r";
                }
                else
                    livro.Isbn = i.ItemAttributes.ISBN;
            }
            catch (NullReferenceException e){
                continue;
                livro.Isbn = "ISBN Desconhecido\n\r";
            }

            try{
                if (null == i.ItemAttributes.ListPrice.Amount)
                {
                    continue;
                    //livro.Preco = "Preço Indisponível\n\r";
                }
                else {
                    string auxS = i.ItemAttributes.ListPrice.Amount;

                    double auxD = Double.Parse(auxS);
                    
                    livro.Preco = auxD / 100;
                }
            }
            catch (NullReferenceException e) {
                continue;
                //livro.Preco = "Preço Indisponível\n\r";
            }

            try {
                if (null == i.ItemAttributes.Publisher)
                    livro.Editora = "Editora Desconhecida\n\r";
                else
                    livro.Editora = i.ItemAttributes.Publisher;
            }
            catch (NullReferenceException e) {
                livro.Editora = "Editora Desconhecida";
            }

            try {
                if (null == i.ItemAttributes.PublicationDate)
                    livro.Ano = "Data de Edição Desconhecida\n\r";
                else                    
                    livro.Ano = i.ItemAttributes.PublicationDate;
            }
            catch (NullReferenceException e) {
                livro.Ano = "Data de Edição Desconhecida";
            }

            try
            {
                if (null == i.Offers.Offer[0].OfferListing[0].Availability)
                    livro.Disponibilidade = "Disponibilidade indisponivel\n\r";
                else
                    livro.Disponibilidade = i.Offers.Offer[0].OfferListing[0].Availability;
            }
            catch (NullReferenceException e)
            {
                livro.Disponibilidade = "Disponibilidade indisponivel\n\r";
            }

            try {
                if( null == i.ItemAttributes.Binding )
                    livro.TipoCapa = "Tipo de Capa desconhecida\n\r";
                else
                    livro.TipoCapa = i.ItemAttributes.Binding;
            } catch (NullReferenceException e) {
                livro.TipoCapa = "Tipo de Capa desconhecida\n\r";
            }

            livros.Add(livro);

        }

        Item[] listaLivros = new Item[livros.Count];

        int j = 0;

        foreach (Item livroItem in livros) {
            listaLivros[j++] = livroItem; 
        }

        amazonECS.ActualizaItem( ref listaLivros );

        return listaLivros;

    }

    private bool ValidaParametros(ref string keyword, ref string author, ref string title, ref string isbn) {

        bool i = true;

        if (0 < keyword.Length)

            i = false;

        else

            keyword = "";

        if (0 < author.Length)

            i = false;

        else

            author = "";

        if (0 < title.Length)

            i = false;

        else

            title = "";

        if (0 < isbn.Length)

            i = false;

        else

            isbn = "";

        if (i)

            return true;

        return false;

    }
}