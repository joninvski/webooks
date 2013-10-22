using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Item
/// </summary>
[System.Xml.Serialization.XmlInclude(typeof(Item))]
public class Item
{

    private string _titulo;
    private string _isbn;
    private string _autor;
    private string _imageUrl;
    private string _editora;
    private string _categoria;
    private double _preco;
    private string _ano;
    private string _disponibilidade;
    private string _fornecedor;
    private string _tipoCapa;

    public Item()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string Titulo {
        get { return _titulo; }
        set { _titulo = value; }
    }

    public string Isbn {
        get { return _isbn; }
        set { _isbn = value; }
    }

    public string Autor {
        get { return _autor; }
        set { _autor = value; }
    }

    public string ImageUrl {
        get { return _imageUrl; }
        set { _imageUrl = value; }
    }

    public string Editora {
        get { return _editora; }
        set { _editora = value; }
    }

    public string Categoria {
        get { return _categoria; }
        set { _categoria = value; }
    }

    public double Preco {
        get { return _preco; }
        set { _preco = value; }
    }

    public string Ano {
        get { return _ano; }
        set { _ano = value; }
    }

    public string Disponibilidade {
        get { return _disponibilidade; }
        set { _disponibilidade = value; }
    }

    public string Fornecedor {
        get { return _fornecedor; }
        set { _fornecedor = value; }
    }

    public string TipoCapa {
        get { return _tipoCapa; }
        set { _tipoCapa = value; }
    }
}
