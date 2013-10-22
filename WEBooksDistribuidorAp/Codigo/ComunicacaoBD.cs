using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Messaging;
using System.Xml;

public class ComunicacaoBD
{
    private SqlConnection _ligacao;
    private SqlTransaction _transacao;

    /// <summary>
    /// Cria uma ligacao e a respectiva transacao
    /// </summary>
    public ComunicacaoBD()
	{
        string databaseName = "WEBooksDistribuidor";
        string serverName = "localhost";
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

public class ComunicacaoFila
{
    public System.Messaging.MessageQueue mqDist;
    public System.Messaging.MessageQueue mqWeb;

    /// <summary>
    /// Cria uma ligacao e a respectiva transacao
    /// </summary>
    public ComunicacaoFila()
    {
        //Criar a fila da WEBooks
        if (MessageQueue.Exists(@".\Private$\Web"))
            mqWeb = new System.Messaging.MessageQueue(@".\Private$\Web");
        else
            mqWeb = MessageQueue.Create(@".\Private$\Web");

        //Criar a fila do Distribuidor
        if (MessageQueue.Exists(@".\Private$\Dist"))
            mqDist = new System.Messaging.MessageQueue(@".\Private$\Dist");
        else
            mqDist = MessageQueue.Create(@".\Private$\Dist");

        mqDist.Formatter = new XmlMessageFormatter(new Type[] { typeof(XmlDataDocument) });
    }

    public System.Messaging.Message LeFilaDist() {
       return mqDist.Receive(new TimeSpan(0, 0, 3));
    }

    public void escreveFilaDist(System.Messaging.Message mes)
    {
        mqDist.Send(mes);
    }

    //envia mensagem para a Queue da WEBooks
    public void escreveFilaWeb(System.Messaging.Message mes)
    {
        mqWeb.Send(mes);
    }

    public void escreveFilaWeb(EncomendaWEBooks encomendaWEBooks)
    {
        System.Messaging.Message mes = new Message();
        mes.Body = encomendaWEBooks.Encomenda2XmlDocument();
        mqWeb.Send(mes);
    }
    
}