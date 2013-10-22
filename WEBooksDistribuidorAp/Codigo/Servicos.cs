using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Collections;

/// <summary>
/// Summary description for Servicos
/// </summary>

public class Servicos : CodigoInterno{

    public Servicos()
    {        
    }

    //+++++++++++++++++++++++++++++++ BASE DE DADOS ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public int invocaQuery(SqlCommand comando, ComunicacaoBD comunicacao)
    {
        int i = 0;

        comando.Transaction = comunicacao.Transacao;
        comando.Connection = comunicacao.Ligacao;

        i = comando.ExecuteNonQuery();

        return i;
    }

    public SqlDataReader invocaQueryLeitura(SqlCommand comando, ComunicacaoBD comunicacao)
    {
        comando.Connection = comunicacao.Ligacao;
        comando.Transaction = comunicacao.Transacao;

        SqlDataReader resposta = null;

        resposta = comando.ExecuteReader();

        return resposta;
    }

    //+++++++++++++++++++++++++++++++ LIVRO ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public int guardaLivro(string idEncomenda, string ISBN, string titulo, string precoVenda,  string tempoEntrega, 
                            string nomeFornecedor, int quantidade, ComunicacaoBD comunicacaoBD)
    {
        string query = "INSERT INTO Livro(idLivro, idEncomenda, ISBN, titulo, precoVenda, tempoEntrega, nomeFornecedor, quantidade)" +
                                    "VALUES( @idLivro, @idEncomenda, @ISBN, @titulo, @precoVenda, @tempoEntrega, @nomeFornecedor, @quantidade)";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idLivro", getGUID()));
        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));
        comando.Parameters.Add(new SqlParameter("@ISBN", ISBN));
        comando.Parameters.Add(new SqlParameter("@titulo", titulo));
        comando.Parameters.Add(new SqlParameter("@precoVenda", precoVenda.Replace('.', ',')));
        comando.Parameters.Add(new SqlParameter("@tempoEntrega", tempoEntrega));
        comando.Parameters.Add(new SqlParameter("@nomeFornecedor", nomeFornecedor));
        comando.Parameters.Add(new SqlParameter("@quantidade", quantidade));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }


    //+++++++++++++++++++++++++++++++ ENCOMENDA ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public string insereEncomenda(string idEncomenda, string valorTotal, string estado, string nomeCliente, string numero, 
                        string address, string city, string state, string ZIPcode, string country, int tempoEspera, ComunicacaoBD comunicacaoBD)
    {

        string query = "INSERT INTO Encomenda(idEncomenda, valorTotal, estado, nomeCliente, numero, address, city, state, ZIPcode, country, tempoEspera)" +
                                    "VALUES(@idEncomenda, @valorTotal, @estado, @nomeCliente, @numero, @address, @city, @state, @ZIPcode, @country, @tempoEspera)";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));
        comando.Parameters.Add(new SqlParameter("@valorTotal", valorTotal));
        comando.Parameters.Add(new SqlParameter("@estado", estado));
        comando.Parameters.Add(new SqlParameter("@nomeCliente", nomeCliente));
        comando.Parameters.Add(new SqlParameter("@numero", numero));
        comando.Parameters.Add(new SqlParameter("@address", address));
        comando.Parameters.Add(new SqlParameter("@city", city));
        comando.Parameters.Add(new SqlParameter("@state", state));
        comando.Parameters.Add(new SqlParameter("@ZIPcode", ZIPcode));
        comando.Parameters.Add(new SqlParameter("@country", country));
        comando.Parameters.Add(new SqlParameter("@tempoEspera", tempoEspera));

        invocaQuery(comando, comunicacaoBD);

        return idEncomenda;
    }

    public void alteraEstado(string idEncomenda, string estado, ComunicacaoBD comunicacaoBD)
    {
        string query = "UPDATE Encomenda" +
                        " SET " +
                             " estado = @estado" +
                         " WHERE " +
                             " idEncomenda = @idEncomenda";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@estado", estado));
        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));

        invocaQuery(comando, comunicacaoBD);
    }

    public void alteraTempoEspera(string idEncomenda, int tempoEspera, ComunicacaoBD comunicacaoBD)
    {
        string query = "UPDATE Encomenda" +
                        " SET " +
                             " tempoEspera = @tempoEspera" +
                         " WHERE " +
                             " idEncomenda = @idEncomenda";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@tempoEspera", tempoEspera));
        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));

        invocaQuery(comando, comunicacaoBD);
    }
  
    public string getEstado(string idEncomenda, ComunicacaoBD comunicacaoBD)
    {
        string query = "SELECT estado" +
                        " FROM Encomenda " +
                        " WHERE " +
                            " idEncomenda = @idEncomenda";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        string estado = null;
       
        int i = 0;
        string aux = null;

        resposta.Read();
        estado = (string)resposta["estado"];

        if (resposta != null)
            resposta.Close();

        return estado;
    }

    public ArrayList getListaEncomendas(ComunicacaoBD comunicacaoBD)
    {
        Encomenda encomenda = null;
        ArrayList listaEncomendas = new ArrayList();

        string query = "SELECT idEncomenda, valorTotal, estado, tempoEspera " +
                       " FROM Encomenda ";

        SqlCommand comando = new SqlCommand(query);

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            encomenda = new Encomenda();

            encomenda.IdEncomenda = (string)resposta["idEncomenda"];
            encomenda.ValorTotal = double.Parse((string)resposta["valorTotal"]);
            encomenda.Estado = (string)resposta["estado"];
            encomenda.TempoEspera = (int)resposta["tempoEspera"];

            listaEncomendas.Add(encomenda);
        }

        if (resposta != null)
            resposta.Close();

        return listaEncomendas;
    }
}

   



