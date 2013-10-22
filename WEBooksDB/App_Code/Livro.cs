using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

[System.Xml.Serialization.XmlInclude(typeof(Livro))]
public class Livro
{
    private string _idLivro;
    private string _ISBN;
    private string _titulo;
    private string _categoria;
    private string _autores;
    private string _editora;
    private string _anoEdicao;
    private string _precoVenda;
    private string _precoSemDesconto;
    private string _urlImagem;
    private string _tempoEntrega;
    private bool _desconto;
    private int _numPesquisas;
    private string _nomeFornecedor;

    //usadas para o carrinho
    private int _quantidade;

    public Livro()
    {
    }

    public string IdLivro
    {
        get

        {

            return _idLivro;

        }

        set

        {

            _idLivro = value;

        }

    }

    public string ISBN

    {

        get

        {

            return _ISBN;

        }

        set

        {

            _ISBN = value;

        }

    }

    public string Titulo

    {

        get

        {

            return _titulo;

        }

        set

        {

            _titulo = value;

        }

    }

    public string Categoria

    {

        get

        {

            return _categoria;

        }

        set

        {

            _categoria = value;

        }

    }

    public string Autores

    {

        get

        {

            return _autores;

        }

        set

        {

            _autores = value;

        }

    }

    public string Editora

    {

        get

        {

            return _editora;

        }

        set

        {

            _editora = value;

        }

    }

    public string AnoEdicao

    {

        get

        {

            return _anoEdicao;

        }

        set

        {

            _anoEdicao = value;

        }

    }

    public string PrecoVenda

    {

        get

        {

            return _precoVenda;

        }

        set

        {

            _precoVenda = value;

        }

    }

    public string PrecoSemDesconto

    {

        get

        {

            return _precoSemDesconto;

        }

        set

        {

            _precoSemDesconto = value;

        }

    }

    public string UrlImagem

    {

        get

        {

            return _urlImagem;

        }

        set

        {

            _urlImagem = value;

        }

    }

    public string TempoEntrega

    {

        get

        {

            return _tempoEntrega;

        }

        set

        {

            _tempoEntrega = value;

        }

    }

    public bool Desconto

    {

        get

        {

            return _desconto;

        }

        set

        {

            _desconto = value;

        }

    }

    public string NomeFornecedor

    {

        get

        {

            return _nomeFornecedor;

        }

        set

        {

            _nomeFornecedor = value;

        }

    }

    public int NumPesquisas
    {

        get
        {

            return _numPesquisas;

        }

        set
        {

            _numPesquisas = value;

        }

    }


    //usadas para o carrinho
    public int Quantidade

    {

        get

        {

            return _quantidade;

        }

        set

        {

            _quantidade = value;

        }

    }
}

[System.Xml.Serialization.XmlInclude(typeof(LivroPromocao))]
public class LivroPromocao
{
    private string _tipoCapa;
    private string _titulo;
    private string _nomeAutor;


    public LivroPromocao()
    {
    }

    public string TipoCapa
    {
        get
        {
            return _tipoCapa;
        }
        set
        {
            _tipoCapa = value;
        }
    }

    public string Titulo
    {
        get
        {
            return _titulo;
        }
        set
        {
            _titulo = value;
        }
    }

    public string NomeAutor
    {
        get
        {
            return _nomeAutor;
        }
        set
        {
            _nomeAutor = value;
        }
    }
}