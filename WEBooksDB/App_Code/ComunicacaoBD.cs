using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public class ComunicacaoBD
{
    private SqlConnection _ligacao;
    private SqlTransaction _transacao;

    /// <summary>
    /// Cria uma ligacao e a respectiva transacao
    /// </summary>
    public ComunicacaoBD()
	{       
        string databaseName = ConfigurationManager.AppSettings["databaseName"];
        string serverName = ConfigurationManager.AppSettings["serverName"];
        string sqlLigacao = "server=" + serverName + ";database=" + databaseName + ";Trusted_Connection=yes";

        _ligacao = new SqlConnection(sqlLigacao);
        _ligacao.Open();
        _transacao = _ligacao.BeginTransaction();

	}

    /// <summary>
    /// Cria uma ligacao e respectiva transacao
    /// </summary>
    /// <param name="sqlLigacao">parametros de ligacao a base de dados</param>
    public ComunicacaoBD(string sqlLigacao)
    {
        _ligacao = new SqlConnection(sqlLigacao);
        _ligacao.Open();
        _transacao = _ligacao.BeginTransaction();
    }

    public void Commit() {
        _transacao.Commit();
    }

    public SqlConnection Ligacao {

        get {
            return _ligacao;
        }
        set {
            _ligacao = value;
        }
    
    }

    public SqlTransaction Transacao {
        get {
            return _transacao;
        }
        set {
            _transacao = value;
        }
    }


}
