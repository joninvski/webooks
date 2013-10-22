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
/// Summary description for Desconto
/// </summary>
public class Desconto
{
    private string _titulo;
    private string _tipoCapa;
    private bool _comDesconto = false;

    public Desconto()
    {
    }

    public string Titulo {

        get {
            return _titulo;
        }

        set{
            _titulo = value;
        }
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

    public bool ComDesconto {

        get {
            return _comDesconto;
        }

        set{
            _comDesconto = value;
        }
    }
}
