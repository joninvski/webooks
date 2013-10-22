using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

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
