using System;
using System.Data;
using System.Configuration;
using System.Web;

[System.Xml.Serialization.XmlInclude(typeof(Livro))]
public class Livro
{
    private string _idLivro;
    private string _ISBN;
    private string _titulo;
    private string _precoVenda;
    private string _tempoEntrega;
    private string _nomeFornecedor;
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