using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

[WebService(Namespace = "http://WEBooksNY.pt/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WEBooksNY : System.Web.Services.WebService
{
    private string blocoTipoLivro = "<div class=\"CollDisplayName\">[A-Z' ]+</div>[\r\n <>a-zA-Z0-9=\"-:./#&,'´`’]+</p>";
    private string nomesLivros = "[1-5]. [a-zA-Z0-9 ,#&:.\"'`´’-]+<?";

    string nomeLivro = "[0-9]+.[ ][a-zA-Z0-9 ''´’,:\"&#-]+(, (by|written|edited)|<|\\.)";

    public WEBooksNY()
    {
    }

    [WebMethod]
    public LivroPromocao[] LivrosTopSellers()
    {
       // ArrayList a = new ArrayList();
        
        ArrayList listaLivros = new ArrayList();
        int i = 0;

        MatchCollection mcl = procuraExpressaoHTML(blocoTipoLivro, "http://www.nytimes.com/pages/books/bestseller/index.html?adxnnl=1&adxnnlx=1146324455-vM6or6XjCvYWjE8bSLNz1Q"); 

        //listaLivros = new TopSellers[mcl.Count];

        foreach (Match ml in mcl)
        {
            foreach (Group g in ml.Groups)
            {
                MatchCollection listaNomes = procuraExpressaoString(nomesLivros, g.Value);

                getlistaLivros(listaNomes, retiraTipoLivro(g.Value), ref listaLivros);
            }
        }

        LivroPromocao[] livrospromocao = new LivroPromocao[listaLivros.Count];
        foreach (LivroPromocao livro in listaLivros) {
            livrospromocao[i++] = livro; 
        }
        return livrospromocao;
    }

    [WebMethod]
    public Desconto[] AssinalaLivrosDesconto(Desconto[] listaLivros)
    {
        LivroPromocao[] listaTopSellers = TopSellers();
        
        foreach(Desconto desconto in listaLivros){

            desconto.TipoCapa = devolveTipoCapa(desconto.TipoCapa);

            foreach (LivroPromocao livroPromocao in listaTopSellers)
            {
                if ((livroPromocao.Titulo.ToUpper().Contains(desconto.Titulo.ToUpper()) || desconto.Titulo.ToUpper().Contains(livroPromocao.Titulo.ToUpper()))
                        && livroPromocao.TipoCapa == desconto.TipoCapa)
                {
                    desconto.ComDesconto = true;
                }
            }            
        }
        return listaLivros;
    }

    [WebMethod]
    public string devolveTipoCapa(string tipoCapa)
    {
        if (tipoCapa.ToUpper().Contains("PAPERBACK"))
        {
            return "PAPERBACK";
        }
        else if (tipoCapa.ToUpper().Contains("HARDCOVER"))
        {
            return "HARDCOVER";
        }
        else
        {
            return tipoCapa;
        }
    }

    public MatchCollection procuraExpressaoHTML(string expressaoRegular, string url)
    {
        byte[] reqHTML;

        Regex expressao = new Regex(expressaoRegular);

        WebClient webClient = new WebClient();        
        reqHTML = webClient.DownloadData(url);
        UTF8Encoding objUTF8 = new UTF8Encoding();
        string webpage = objUTF8.GetString(reqHTML);

        MatchCollection listaComparacoes = expressao.Matches(webpage);

        return listaComparacoes;
    }

    public MatchCollection procuraExpressaoString(string expressaoRegular, string texto)
    {

        Regex expressao = new Regex(expressaoRegular);

        MatchCollection listaComparacoes = expressao.Matches(texto);

        return listaComparacoes;
    }    
    
    public string retiraTipoLivro(string texto)
    {

        Regex expressao = new Regex(">[A-Z' ]+<");
        string tipoLivro = null;

        MatchCollection listaComparacoes = expressao.Matches(texto);
      
        tipoLivro = Regex.Replace(listaComparacoes[0].Groups[0].Value,"[<>]",""); 

        return tipoLivro;
    }

    public void getlistaLivros(MatchCollection listaNomes, string tipoCapa, ref ArrayList listaLivros)
    {
        LivroPromocao livro = null;

     

        foreach (Match grupo in listaNomes) {

            livro = new LivroPromocao();

            livro.TipoCapa = tipoCapa;
            livro.Titulo = retiraNomeLivro(grupo.Groups[0].Value);
            livro.NomeAutor = retiraNomeAutor(grupo.Groups[0].Value);

            listaLivros.Add(livro);
        }
    }

    public string retiraNomeLivro(string texto)
    {

        Regex expressao = new Regex(nomeLivro);
        
        MatchCollection listaComparacoes = expressao.Matches(texto);

        string aux = Regex.Replace(listaComparacoes[0].Groups[0].Value, "([0-9]\\.[ ])", "");
        return Regex.Replace(aux, "((, by)|(, written)|(, edited)|<|\\.)[a-zA-Z0-9 ,:&#.\"'`´’-]*<?", "");
    }
    
    public string retiraNomeAutor(string texto)
    {
        string nomeAutor = "by [a-zA-Z0-9 \\.'`´’-]*<?";

        Regex expressao = new Regex(nomeAutor);

        MatchCollection listaComparacoes = expressao.Matches(texto);

        if (listaComparacoes.Count == 0) {
            return "";
        }

        string aux = Regex.Replace(listaComparacoes[0].Groups[0].Value, "by ", "");
        aux = Regex.Replace(aux, "(and)|(with)|(\\. Illustrated)", "\n\r|");
        return Regex.Replace(aux, "<", "");
    }
      

    public LivroPromocao[] TopSellers()
    {
        ArrayList a = new ArrayList();

        ArrayList listaLivros = new ArrayList();
        int i = 0;

        MatchCollection mcl = procuraExpressaoHTML(blocoTipoLivro, "http://www.nytimes.com/pages/books/bestseller/index.html?adxnnl=1&adxnnlx=1146324455-vM6or6XjCvYWjE8bSLNz1Q");
       
        //listaLivros = new TopSellers[mcl.Count];

        foreach (Match ml in mcl)
        {
            foreach (Group g in ml.Groups)
            {
                MatchCollection listaNomes = procuraExpressaoString(nomesLivros, g.Value);

                getlistaLivros(listaNomes, devolveTipoCapa(retiraTipoLivro(g.Value)), ref listaLivros);
            }
        }

        LivroPromocao[] livrospromocao = new LivroPromocao[listaLivros.Count];
        foreach (LivroPromocao livro in listaLivros)
        {
            livrospromocao[i++] = livro;
        }
        return livrospromocao;
    }

   [WebMethod]
    public Desconto[] testaAssinala() {

        Desconto[] descontos = new Desconto[10];

        for (int i = 0; i < 10; i++) {
            descontos[i] = new Desconto();
        }

        //nome certo - tipo certo
        descontos[0].TipoCapa = "Mass Market Paperback";
        descontos[0].Titulo = "The Da Vinci Code";

        //nome certo - tipo errado
        descontos[1].TipoCapa = "HARDCOVER FICTION";
        descontos[1].Titulo = "TWO LITTLE GIRLS IN BLUE";

        // nome errado - tipo errado
        descontos[2].TipoCapa = "CHILDREN'S BOOKS";
        descontos[2].Titulo = "OWEN and MZEE";

        descontos[3].TipoCapa = "Hardcover";
        descontos[3].Titulo = "MARLEY & ME";

        descontos[4].TipoCapa = "Hardcover";
        descontos[4].Titulo = "BLUE SHOES & HAPPINESS";

        //metade do nome
        descontos[5].TipoCapa = "Hardcover";
        descontos[5].Titulo = "THE jesus";

        descontos[6].TipoCapa = "Hardcover";
        descontos[6].Titulo = "5TH HORSEMAN";

        descontos[7].TipoCapa = "Hardcover";
        descontos[7].Titulo = "Nada de ejito";

        descontos[8].TipoCapa = "Hardcover";
        descontos[8].Titulo = "THE DA VINCI CODE";

        descontos[9].TipoCapa = "Hardcover";
        descontos[9].Titulo = "ta mal";

        return AssinalaLivrosDesconto(descontos);
    }
    
}
