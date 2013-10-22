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

    public int invocaQuery(string query, ComunicacaoBD comunicacao)
    {
        int i = 0;
        SqlCommand comando = new SqlCommand();

        comando.Transaction = comunicacao.Transacao;
        comando.Connection = comunicacao.Ligacao;

        comando.CommandText = query;
        i =  comando.ExecuteNonQuery();

        return i;
    }

    public int invocaQuery(SqlCommand comando, ComunicacaoBD comunicacao)
    {
        int i = 0;

        comando.Transaction = comunicacao.Transacao;
        comando.Connection = comunicacao.Ligacao;

        i = comando.ExecuteNonQuery();

        return i;
    }

    /// <summary>
    /// Recebe uma query e exucuta a query na base de dados
    /// IMPORTANTE: a ligacao e fechada no metodo que chama este por causa de poder retornar o objecto SqlDataReader
    /// </summary>
    /// <param name="query"></param>
    /// <returns>o resultado da query a base de dados</returns>
    public SqlDataReader invocaQueryLeitura(string query, ComunicacaoBD comunicacao)
    {
        SqlCommand comando = new SqlCommand();   

        comando.Connection = comunicacao.Ligacao;
        comando.Transaction = comunicacao.Transacao;

        SqlDataReader resposta = null;       

        comando.CommandText = query;
        resposta = comando.ExecuteReader();

        return resposta;  
    }

    public SqlDataReader invocaQueryLeitura(SqlCommand comando, ComunicacaoBD comunicacao)
    {
        comando.Connection = comunicacao.Ligacao;
        comando.Transaction = comunicacao.Transacao;

        SqlDataReader resposta = null;

        resposta = comando.ExecuteReader();

        return resposta;
    }

    public int invocaQueryEscalar(SqlCommand comando, ComunicacaoBD comunicacao)
    {
        comando.Connection = comunicacao.Ligacao;
        comando.Transaction = comunicacao.Transacao;

        int resposta = 0;

        resposta = (int)comando.ExecuteScalar();

        return resposta;
    }

    //+++++++++++++++++++++++++++++++ UTILIZADOR ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public string InsereUtilizador(string nome, string username, string password, ComunicacaoBD comunicacaoBD)
    {
        string idUtilizador = getGUID();

        string query = "INSERT INTO Utilizador(idUtilizador, nome, username, password) " +
                                "values( @idUtilizador,  @nome, @username, @password)";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idUtilizador", idUtilizador));
        comando.Parameters.Add(new SqlParameter("@nome", nome));
        comando.Parameters.Add(new SqlParameter("@username", username));
        comando.Parameters.Add(new SqlParameter("@password", password));


        int linhasAlteradas = invocaQuery(comando, comunicacaoBD);

        if(linhasAlteradas == 0){
            return null;
        }        

        return idUtilizador;
    }

    public Utilizador getDadosUtilizador(string username, ComunicacaoBD comunicacaoBD)
    {
        Utilizador utilizador = null;

        string query = "SELECT * " +
                        " FROM Utilizador " +
                        " WHERE " +
                            " username = @username";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@username", username));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            utilizador = new Utilizador();

            utilizador.IdUtilizador = (string)resposta["idUtilizador"];
            utilizador.Nome = (string)resposta["nome"];
            utilizador.Username = (string)resposta["username"];
            utilizador.Password = (string)resposta["password"];
        }

        if (resposta != null)
            resposta.Close();

        return utilizador;
    }

 //+++++++++++++++++++++++++++++++ CLIENTE ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public int InsereCliente(string idCliente, string telefone, string numero, string address, string city, string state,
                                string ZIPcode, string country, string latitude, string longitude, 
                                string numCartaoCredito, string dataValidadeCartaoCredito, string estado, ComunicacaoBD comunicacaoBD)
    {
       string query = "INSERT INTO Cliente(idCliente, telefone, numero, address, city, state, ZIPcode," +
                                                    " country, latitude, longitude, numCartaoCredito, dataValidadeCartaoCredito, estado) " +
                                "values(@idCliente, @telefone, @numero, @address, @city, @state, @ZIPcode, @country, @latitude, " +
                                " @longitude, @numCartaoCredito, @dataValidadeCartaoCredito, @estado)";

       SqlCommand comando = new SqlCommand(query);

       comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));
       comando.Parameters.Add(new SqlParameter("@telefone", telefone));
       comando.Parameters.Add(new SqlParameter("@numero", numero));
       comando.Parameters.Add(new SqlParameter("@address", address));
       comando.Parameters.Add(new SqlParameter("@city", city));
       comando.Parameters.Add(new SqlParameter("@state", state));
       comando.Parameters.Add(new SqlParameter("@ZIPcode", ZIPcode));
       comando.Parameters.Add(new SqlParameter("@country", country));
       comando.Parameters.Add(new SqlParameter("@latitude", latitude));
       comando.Parameters.Add(new SqlParameter("@longitude", longitude));
       comando.Parameters.Add(new SqlParameter("@numCartaoCredito", numCartaoCredito));
       comando.Parameters.Add(new SqlParameter("@dataValidadeCartaoCredito", dataValidadeCartaoCredito));
       comando.Parameters.Add(new SqlParameter("@estado", estado));

       int i = invocaQuery(comando, comunicacaoBD);

       return i;
    }

    public Cliente getDadosCliente(string username, ComunicacaoBD comunicacaoBD)
    {
        Cliente cliente = null;

        string query = "SELECT Cliente.idCliente, Utilizador.nome, Utilizador.username, Utilizador.password, " +
                                " Cliente.telefone, Cliente.numero, Cliente.address, Cliente.city, Cliente.state, Cliente.ZIPcode, " +
                                " Cliente.country, Cliente.latitude, Cliente.longitude, Cliente.numCartaoCredito, " +
                                " Cliente.dataValidadeCartaoCredito, Cliente.estado" +
                        " FROM Cliente, Utilizador " +
                        " WHERE " +
                            " Cliente.idCliente = Utilizador.idUtilizador" +
                            " AND " +
                            " Cliente.idCliente = (SELECT Utilizador.idUtilizador" +
                                                    " FROM Utilizador " +
                                                    " WHERE " +
                                                        " Utilizador.username = @username)";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@username", username));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            cliente = new Cliente();

            cliente.IdCliente = (string)resposta["idCliente"];
            cliente.Nome = (string)resposta["nome"];
            cliente.Username = (string)resposta["username"];
            cliente.Password = (string)resposta["password"];
            cliente.Telefone = (string)resposta["telefone"];
            cliente.Numero = (string)resposta["numero"];
            cliente.Address = (string)resposta["address"];
            cliente.City = (string)resposta["city"];
            cliente.State = (string)resposta["state"];
            cliente.ZIPcode = (string)resposta["ZIPcode"];
            cliente.Country = (string)resposta["country"];
            cliente.Latitude = (string)resposta["latitude"];
            cliente.Longitude = (string)resposta["longitude"];
            cliente.NumCartaoCredito = (string)resposta["numCartaoCredito"];
            cliente.DataValidadeCartaoValidade = (string)resposta["dataValidadeCartaoCredito"];
            cliente.Estado = (string)resposta["estado"];
        }

        if (resposta != null)
            resposta.Close();

        return cliente;
    }

    public ArrayList getLivrosComprados(string username, ComunicacaoBD comunicacaoBD)
    {
        Livro livro = null;
        ArrayList listaLivros = new ArrayList();

        string query = "SELECT Livro.idLivro, Livro.ISBN, Livro.titulo, Livro.categoria, Livro.autores, " +
                                " Livro.editora, Livro.anoEdicao, Livro.precoVenda, Livro.precoSemDesconto, Livro.urlImagem, " +
                                " Livro.tempoEntrega, Livro.desconto, Livro.nomeFornecedor, Livro.numPesquisas" +
                        " FROM Livro, Encomenda, LinhaEncomenda, Cliente, Utilizador " +
                        " WHERE " +
                            " Utilizador.username = @username " +
                            " AND Utilizador.idUtilizador = Cliente.idCliente " +
                            " AND Cliente.idCliente = Encomenda.idCliente " +
                            " AND Encomenda.idEncomenda = LinhaEncomenda.idEncomenda " +
                            " AND LinhaEncomenda.idLivro = Livro.idLivro";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@username", username));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            livro = new Livro();

            livro.IdLivro = (string)resposta["idLivro"];
            livro.ISBN = (string)resposta["ISBN"];
            livro.Titulo = (string)resposta["titulo"];
            livro.Categoria = (string)resposta["categoria"];
            livro.Autores = (string)resposta["autores"];
            livro.Editora = (string)resposta["editora"];
            livro.AnoEdicao = (string)resposta["anoEdicao"];
            livro.PrecoVenda = (string)resposta["precoVenda"];
            livro.PrecoSemDesconto = (string)resposta["precoSemDesconto"];
            livro.UrlImagem = (string)resposta["urlImagem"];
            livro.TempoEntrega = (string)resposta["tempoEntrega"];
            livro.Desconto = (bool)resposta["desconto"];
            livro.NumPesquisas = (int)resposta["numPesquisas"];
            livro.NomeFornecedor = (string)resposta["nomeFornecedor"];

            listaLivros.Add(livro);
        }

        if (resposta != null)
            resposta.Close();

        return listaLivros;
    }

    public int apagaCliente(string idCliente, ComunicacaoBD comunicacaoBD)
    {
        string query = "UPDATE Cliente" +
                        " SET " +
                             " estado = @estado " +
                         " WHERE " +
                             " idCliente = @idCliente";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));
        comando.Parameters.Add(new SqlParameter("@estado", "inactivo"));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    public ArrayList getClientes(ComunicacaoBD comunicacaoBD)
    {
        Cliente cliente = null;
        ArrayList listaClientes = new ArrayList();

        string query = "SELECT Cliente.idCliente, Utilizador.nome, Utilizador.username, Utilizador.password, " +
                                " Cliente.telefone, Cliente.numero, Cliente.address, Cliente.city, Cliente.state, Cliente.ZIPcode, " +
                                " Cliente.country, Cliente.latitude, Cliente.longitude, Cliente.numCartaoCredito, " +
                                " Cliente.dataValidadeCartaoCredito, Cliente.estado" +
                        " FROM Cliente, Utilizador " +
                        " WHERE " +
                            " Cliente.idCliente = Utilizador.idUtilizador";

        SqlCommand comando = new SqlCommand(query);

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            cliente = new Cliente();

            cliente.IdCliente = (string)resposta["idCliente"];
            cliente.Nome = (string)resposta["nome"];
            cliente.Username = (string)resposta["username"];
            cliente.Password = (string)resposta["password"];
            cliente.Telefone = (string)resposta["telefone"];
            cliente.Numero = (string)resposta["numero"];
            cliente.Address = (string)resposta["address"];
            cliente.City = (string)resposta["city"];
            cliente.State = (string)resposta["state"];
            cliente.ZIPcode = (string)resposta["ZIPcode"];
            cliente.Country = (string)resposta["country"];
            cliente.Latitude = (string)resposta["latitude"];
            cliente.Longitude = (string)resposta["longitude"];
            cliente.NumCartaoCredito = (string)resposta["numCartaoCredito"];
            cliente.DataValidadeCartaoValidade = (string)resposta["dataValidadeCartaoCredito"];
            cliente.Estado = (string)resposta["estado"];

            listaClientes.Add(cliente);
        }

        if (resposta != null)
            resposta.Close();

        return listaClientes;
    }

    //+++++++++++++++++++++++++++++++ GESTOR ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public int InsereGestor(string idUtilizador, ComunicacaoBD comunicacaoBD)
    {
        string query = "INSERT INTO Gestor(idGestor) " +
                                 "values( @idUtilizador)";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idUtilizador", idUtilizador));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    public Gestor getDadosGestor(string username, ComunicacaoBD comunicacaoBD)
    {
        Gestor gestor = null;

        string query = "SELECT Gestor.idGestor, Utilizador.nome, Utilizador.username, Utilizador.password " +
                        " FROM Gestor, Utilizador " +
                        " WHERE " +
                            " Utilizador.username = @username " +
                            " AND Gestor.idGestor = Utilizador.idUtilizador ";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@username", username));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            gestor = new Gestor();

            gestor.IdGestor = (string)resposta["idGestor"];
            gestor.Nome = (string)resposta["nome"];
            gestor.Username = (string)resposta["username"];
            gestor.Password = (string)resposta["password"];
        }

        if (resposta != null)
            resposta.Close();

        return gestor;
    }


    //+++++++++++++++++++++++++++++++ CARRINHO COMPRAS ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public string getIdCarrinho(string idCliente, ComunicacaoBD comunicacaoBD)
    {
        string query = "SELECT idCarrinho " +
                        " FROM Carrinho " +
                        " WHERE " +
                            " idCliente = @idCliente";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        string idCarrinho;

        if (resposta.HasRows)
        {
            resposta.Read();
            idCarrinho = (string)resposta["idCarrinho"];
        }else
        {
            idCarrinho = null;
        }

        if (resposta != null)
            resposta.Close();

        return idCarrinho;
    }

    public int insereLivroCarrinhoCompras(string idCarrinho, string idCliente, string idLivro, int quantidade, ComunicacaoBD comunicacaoBD)
    {
        string query = "INSERT INTO Carrinho(idCarrinho, idCliente, idLivro, quantidade)" +
                                        "VALUES(@idCarrinho, @idCliente, @idLivro, @quantidade)";

        SqlCommand comando = new SqlCommand(query);

        if (idCarrinho == null)
        {
            comando.Parameters.Add(new SqlParameter("@idCarrinho", getGUID()));
        }else
        {
            comando.Parameters.Add(new SqlParameter("@idCarrinho", idCarrinho));
        }

        comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));
        comando.Parameters.Add(new SqlParameter("@idLivro", idLivro));
        comando.Parameters.Add(new SqlParameter("@quantidade", quantidade));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    public ArrayList getLivrosDoCarrinho(string username, ComunicacaoBD comunicacaoBD)
    {
        Livro livro = null;
        ArrayList listaLivros = new ArrayList();

        string query = "SELECT Livro.idLivro, Livro.ISBN, Livro.titulo, Livro.categoria, Livro.autores, " +
                                " Livro.editora, Livro.anoEdicao, Livro.precoVenda, Livro.precoSemDesconto, Livro.urlImagem, " +
                                " Livro.tempoEntrega, Livro.desconto, Livro.nomeFornecedor, Livro.numPesquisas, Carrinho.quantidade " +
                        " FROM Livro, Carrinho, Cliente, Utilizador " +
                        " WHERE " +
                            " Utilizador.username = @username" +
                            " AND " +
                            " Utilizador.idUtilizador = Cliente.idCliente " +
                            " AND " +
                            " Cliente.idCliente = Carrinho.idCliente " +
                            " AND " +
                            " Livro.idLivro  = Carrinho.idLivro " +
                            " AND " +
                            " Livro.idLivro IN (SELECT Carrinho.idLivro" +
                                                    " FROM Carrinho, Cliente, Utilizador " +
                                                    " WHERE " +
                                                        " Utilizador.username = @username" +
                                                        " AND Cliente.idCliente = Utilizador.idUtilizador " +
                                                        " AND Cliente.idCliente = Carrinho.idCliente )";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@username", username));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            livro = new Livro();

            livro.IdLivro = (string)resposta["idLivro"];
            livro.ISBN = (string)resposta["ISBN"];
            livro.Titulo = (string)resposta["titulo"];
            livro.Categoria = (string)resposta["categoria"];
            livro.Autores = (string)resposta["autores"];
            livro.Editora = (string)resposta["editora"];
            livro.AnoEdicao = (string)resposta["anoEdicao"];
            livro.PrecoVenda = (string)resposta["PrecoVenda"];
            livro.PrecoSemDesconto = (string)resposta["precoSemDesconto"];
            livro.UrlImagem = (string)resposta["urlImagem"];
            livro.TempoEntrega = (string)resposta["tempoEntrega"];
            livro.Desconto = (bool)resposta["desconto"];
            livro.NumPesquisas = (int)resposta["numPesquisas"];
            livro.NomeFornecedor = (string)resposta["nomeFornecedor"];
            livro.Quantidade = (int)resposta["quantidade"];

            //livro.PrecoVenda = livro.PrecoSemDesconto; //fazDesconto(livro.PrecoSemDesconto, livro.Desconto, livro.Top5);

            listaLivros.Add(livro);
        }

        if (resposta != null)
            resposta.Close();

        return listaLivros;
    }

    public void removeLivroCarrinhoCompras(string idCarrinho, string idCliente, string idLivro, ComunicacaoBD comunicacaoBD)
    {
        string query;

        query = "DELETE FROM Carrinho " +
                    "WHERE " +
                        "idCarrinho = @idCarrinho" +
                        " AND " +
                        "idCliente = @idCliente" +
                        " AND " +
                        "idLivro = @idLivro";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idCarrinho", idCarrinho));
        comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));
        comando.Parameters.Add(new SqlParameter("@idLivro", idLivro));

        invocaQuery(comando, comunicacaoBD);
    }

    public int limpaCarrinhoCompras(string idCarrinho, string idCliente, ComunicacaoBD comunicacaoBD)
    {
        string query = null;

        query = "DELETE FROM Carrinho " +
                    " WHERE " +
                        " idCarrinho = @idCarrinho " +
                        " AND " +
                        " idCliente = @idCliente";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idCarrinho", idCarrinho));
        comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    //+++++++++++++++++++++++++++++++ LIVRO ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public Livro getLivro(string ISBN, ComunicacaoBD comunicacaoBD)
    {
        Livro livro = null;

        string query = "SELECT * " +
                        " FROM Livro " +
                        " WHERE " +
                            " ISBN = @ISBN ";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@ISBN", ISBN));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            livro = new Livro();

            livro.IdLivro = (string)resposta["idLivro"];
            livro.ISBN = (string)resposta["ISBN"];
            livro.Titulo = (string)resposta["titulo"];
            livro.Categoria = (string)resposta["categoria"];
            livro.Autores = (string)resposta["autores"];
            livro.Editora = (string)resposta["editora"];
            livro.AnoEdicao = (string)resposta["anoEdicao"];
            livro.PrecoVenda = (string)resposta["PrecoVenda"];
            livro.PrecoSemDesconto = (string)resposta["precoSemDesconto"];
            livro.UrlImagem = (string)resposta["urlImagem"];
            livro.TempoEntrega = (string)resposta["tempoEntrega"];
            livro.Desconto = (bool)resposta["desconto"];
            livro.NumPesquisas = (int)resposta["numPesquisas"];
            livro.NomeFornecedor = (string)resposta["nomeFornecedor"];

            //livro.PrecoVenda = livro.PrecoSemDesconto; //fazDesconto(livro.PrecoSemDesconto, livro.Desconto, livro.Top5);
        }

        if (resposta != null)
            resposta.Close();

        return livro;
    }

    public int actualizaNumeroPesquisas(string ISBN, string nomeFornecedor, int numPesquisas, string precoVenda, string precoSemDesconto, ComunicacaoBD comunicacaoBD)
    {
        string query = null;

        query = "UPDATE Livro" +
                        " SET " +
                             " numPesquisas = @numPesquisas," +
                             " precoVenda = @precoVenda, " +
                             " precoSemDesconto = @precoSemDesconto " +
                         " WHERE " +
                             " ISBN = @ISBN" +
                             " AND " +
                             " nomeFornecedor = @nomeFornecedor";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@numPesquisas", numPesquisas));
        comando.Parameters.Add(new SqlParameter("@precoVenda", precoVenda));
        comando.Parameters.Add(new SqlParameter("@precoSemDesconto", precoSemDesconto));
        comando.Parameters.Add(new SqlParameter("@ISBN", ISBN));
        comando.Parameters.Add(new SqlParameter("@nomeFornecedor", nomeFornecedor));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    public int guardaLivro(string ISBN, string titulo, string categoria, string autores,
                            string editora, string anoEdicao, string precoVenda, string precoSemDesconto, string urlImagem,
                            string tempoEntrega, bool desconto, string nomeFornecedor, ComunicacaoBD comunicacaoBD)
    {
        string query = "INSERT INTO Livro(idLivro, ISBN, titulo, categoria, autores, editora, anoEdicao, precoVenda, precoSemDesconto," +
                                            " urlImagem, tempoEntrega, desconto, nomeFornecedor, numPesquisas)" +
                                    "VALUES( @idLivro, @ISBN, @titulo, @categoria, @autores, @editora, @anoEdicao, @precoVenda," +
                                             " @precoSemDesconto, @urlImagem, @tempoEntrega, @desconto, @nomeFornecedor, @numPesquisas)";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idLivro", getGUID()));
        comando.Parameters.Add(new SqlParameter("@ISBN", ISBN));
        comando.Parameters.Add(new SqlParameter("@titulo", titulo));
        comando.Parameters.Add(new SqlParameter("@categoria", categoria));
        comando.Parameters.Add(new SqlParameter("@autores", autores));
        comando.Parameters.Add(new SqlParameter("@editora", editora));
        comando.Parameters.Add(new SqlParameter("@anoEdicao", anoEdicao));
        comando.Parameters.Add(new SqlParameter("@precoVenda", precoVenda.Replace('.', ',')));
        comando.Parameters.Add(new SqlParameter("@precoSemDesconto", precoSemDesconto.Replace('.', ',')));
        comando.Parameters.Add(new SqlParameter("@tempoEntrega", tempoEntrega));
        comando.Parameters.Add(new SqlParameter("@urlImagem", urlImagem));
        comando.Parameters.Add(new SqlParameter("@desconto", desconto));
        comando.Parameters.Add(new SqlParameter("@nomeFornecedor", nomeFornecedor));
        comando.Parameters.Add(new SqlParameter("@numPesquisas", 1));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    public ArrayList getLivrosMaisComprados(int numeroLivros, ComunicacaoBD comunicacaoBD)
    {
        int i = 0;
        Livro livro = null;
        ArrayList listaLivros = new ArrayList();

        string query = "SELECT Livro.ISBN, Livro.idLivro, Livro.titulo, Livro.categoria, Livro.autores, Livro.editora, " +
                            " Livro.anoEdicao, Livro.precoVenda, Livro.precoSemDesconto, Livro.urlImagem, Livro.tempoEntrega, " + 
                            " Livro.desconto, Livro.nomeFornecedor, Livro.numPesquisas, SUM (LinhaEncomenda.quantidade) as quantidadeComprada " +
                        " FROM LinhaEncomenda, Livro " +
                        " WHERE  LinhaEncomenda.idLivro = Livro.idLivro " +
                        " GROUP BY Livro.ISBN, Livro.idLivro, Livro.titulo, Livro.categoria, Livro.autores, Livro.editora, " +
                            " Livro.anoEdicao, Livro.precoVenda, Livro.precoSemDesconto, Livro.urlImagem, Livro.tempoEntrega, " + 
                            " Livro.desconto, Livro.nomeFornecedor, Livro.numPesquisas " +
                        "ORDER BY quantidadeComprada DESC";

        SqlCommand comando = new SqlCommand(query);

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            if (i++ >= numeroLivros)
            {
                break;
            }

            livro = new Livro();

            livro.IdLivro = (string)resposta["idLivro"];
            livro.ISBN = (string)resposta["ISBN"];
            livro.Titulo = (string)resposta["titulo"];
            livro.Categoria = (string)resposta["categoria"];
            livro.Autores = (string)resposta["autores"];
            livro.Editora = (string)resposta["editora"];
            livro.AnoEdicao = (string)resposta["anoEdicao"];
            livro.PrecoVenda = (string)resposta["PrecoVenda"];
            livro.PrecoSemDesconto = (string)resposta["precoSemDesconto"];
            livro.UrlImagem = (string)resposta["urlImagem"];
            livro.TempoEntrega = (string)resposta["tempoEntrega"];
            livro.Desconto = (bool)resposta["desconto"];
            livro.NumPesquisas = (int)resposta["numPesquisas"];
            livro.NomeFornecedor = (string)resposta["nomeFornecedor"];
            livro.Quantidade = (int)resposta["quantidadeComprada"];

            listaLivros.Add(livro);

            
        }

        if (resposta != null)
            resposta.Close();

        return listaLivros;
    }

    public ArrayList getLivrosMaisPesquisados(int numeroLivros, ComunicacaoBD comunicacaoBD)
    {
        int i = 0;
        Livro livro = null;
        ArrayList listaLivros = new ArrayList();

        string query = "SELECT Livro.ISBN, Livro.idLivro, Livro.titulo, Livro.categoria, Livro.autores, Livro.editora, " +
                            " Livro.anoEdicao, Livro.precoVenda, Livro.precoSemDesconto, Livro.urlImagem, Livro.tempoEntrega, " +
                            " Livro.desconto, Livro.nomeFornecedor, Livro.numPesquisas " +
                        " FROM Livro " +
                        " GROUP BY Livro.ISBN, Livro.idLivro, Livro.titulo, Livro.categoria, Livro.autores, Livro.editora, " +
                            " Livro.anoEdicao, Livro.precoVenda, Livro.precoSemDesconto, Livro.urlImagem, Livro.tempoEntrega, " +
                            " Livro.desconto, Livro.nomeFornecedor, Livro.numPesquisas " +
                        "ORDER BY Livro.numPesquisas DESC";

        SqlCommand comando = new SqlCommand(query);

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            if (i++ >= numeroLivros)
            {
                break;
            }

            livro = new Livro();

            livro.IdLivro = (string)resposta["idLivro"];
            livro.ISBN = (string)resposta["ISBN"];
            livro.Titulo = (string)resposta["titulo"];
            livro.Categoria = (string)resposta["categoria"];
            livro.Autores = (string)resposta["autores"];
            livro.Editora = (string)resposta["editora"];
            livro.AnoEdicao = (string)resposta["anoEdicao"];
            livro.PrecoVenda = (string)resposta["PrecoVenda"];
            livro.PrecoSemDesconto = (string)resposta["precoSemDesconto"];
            livro.UrlImagem = (string)resposta["urlImagem"];
            livro.TempoEntrega = (string)resposta["tempoEntrega"];
            livro.Desconto = (bool)resposta["desconto"];
            livro.NumPesquisas = (int)resposta["numPesquisas"];
            livro.NomeFornecedor = (string)resposta["nomeFornecedor"];

            listaLivros.Add(livro);
        }

        if (resposta != null)
            resposta.Close();

        return listaLivros;
    }

    public int getNumeroLivrosPesquisados(ComunicacaoBD comunicacaoBD)
    {
        string query = "SELECT SUM(Livro.numPesquisas) as numeroLivrosPesquisados " +
                        " FROM Livro";

        SqlCommand comando = new SqlCommand(query);

        int resposta = invocaQueryEscalar(comando, comunicacaoBD);

        return resposta;
    }

    public int getNumeroLivrosComprados(ComunicacaoBD comunicacaoBD)
    {
        string query = "SELECT SUM(LinhaEncomenda.quantidade) as numeroLivrosComprados " +
                        " FROM LinhaEncomenda";

        SqlCommand comando = new SqlCommand(query);

        int resposta = invocaQueryEscalar(comando, comunicacaoBD);

        return resposta;
    }

    public int getNumeroLivrosDesconto(int comDesconto, ComunicacaoBD comunicacaoBD)
    {
        string query = "SELECT SUM(quantidade) as numeroLivrosComprados " +
                        " FROM LinhaEncomenda " + 
                        " WHERE comDesconto = @comDesconto";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@comDesconto", comDesconto));

        int resposta = invocaQueryEscalar(comando, comunicacaoBD);

        return resposta;
    }


    //+++++++++++++++++++++++++++++++ ENCOMENDA ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public string CriaEncomenda(string idCliente, string valorTotal, string dataCriacao, ComunicacaoBD comunicacaoBD)
    {
        string idEncomenda = getGUID();

        string query = "INSERT INTO Encomenda(idEncomenda, idCliente, valorTotal, dataCriacao)" +
                                    "VALUES(@idEncomenda, @idCliente, @valorTotal, @dataCriacao)";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));
        comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));
        comando.Parameters.Add(new SqlParameter("@valorTotal", valorTotal));
        comando.Parameters.Add(new SqlParameter("@dataCriacao", dataCriacao));

        invocaQuery(comando, comunicacaoBD);

        return idEncomenda;
    }

    public int insereLinhaEncomenda(string idEncomenda, string idLivro, int quantidade, 
                                    string precoVenda, bool comDesconto, ComunicacaoBD comunicacaoBD)
    {
        string query = "INSERT INTO LinhaEncomenda(idLinhaEncomenda, idEncomenda, idLivro, quantidade, precoVenda, comDesconto)" +
                                    "VALUES(@idLinhaEncomenda, @idEncomenda, @idLivro, @quantidade, @precoVenda, @comDesconto)";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idLinhaEncomenda", getGUID()));
        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));
        comando.Parameters.Add(new SqlParameter("@idLivro", idLivro));
        comando.Parameters.Add(new SqlParameter("@quantidade", quantidade));
        comando.Parameters.Add(new SqlParameter("@precoVenda", precoVenda));
        comando.Parameters.Add(new SqlParameter("@comDesconto", comDesconto));

        int i = invocaQuery(comando, comunicacaoBD);
        return i;
    }

    public int actualizaValorTotal(string idEncomenda, string valorTotal, ComunicacaoBD comunicacaoBD)
    {
        string query = "UPDATE Encomenda" +
                        " SET " +
                             " valorTotal = @valorTotal" +
                         " WHERE " +
                             " idEncomenda = @idEncomenda";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@valorTotal", valorTotal));
        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    public void CriaEstadoHistorico(string idEncomenda, string nomeEstado, int contador, string dataAlteracao, ComunicacaoBD comunicacaoBD)
    {       
        string query = "INSERT INTO EstadoHistorico(idEstado, idEncomenda, nomeEstado, contador, dataAlteracao)" +
                                    "VALUES(@idEstado, @idEncomenda, @nomeEstado, @contador, @dataAlteracao)";
        
        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idEstado", getGUID()));
        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));
        comando.Parameters.Add(new SqlParameter("@nomeEstado", nomeEstado));
        comando.Parameters.Add(new SqlParameter("@contador", contador));
        comando.Parameters.Add(new SqlParameter("@dataAlteracao", dataAlteracao));

        invocaQuery(comando, comunicacaoBD);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idEncomenda"></param>
    /// <param name="comunicacaoBD"></param>
    /// <returns> Array de Strings: 0 - contador
    ///                             1 - nome do Estado</returns>
    public string[] getUltimoEstado(string idEncomenda, ComunicacaoBD comunicacaoBD)
    {
        string query = "SELECT nomeEstado, MAX(contador) AS contador " +
                        " FROM EstadoHistorico " +
                        " WHERE " +
                            " idEncomenda = @idEncomenda" +
                        " GROUP BY nomeEstado";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        string[] estado = new string[2];
       
        int i = 0;
        string aux = null;

        resposta.Read();
        estado[0] = ((int)resposta["contador"]).ToString();
        estado[1] = (string)resposta["nomeEstado"];

        while (resposta.Read() == true)
        {           
            i = (int)resposta["contador"];
            aux = (string)resposta["nomeEstado"];

            if (i > int.Parse(estado[0]))
            {
                estado[0] = i.ToString();
                estado[1] = aux;
            }
        }

        if (resposta != null)
            resposta.Close();

        return estado;
    }

    public ArrayList getListaEncomendas(ComunicacaoBD comunicacaoBD)
    {
        Encomenda encomenda = null;
        ArrayList listaEncomendas = new ArrayList();

        string query = "SELECT Encomenda.idEncomenda, Encomenda.idCliente, Encomenda.valorTotal, Encomenda.dataCriacao, " +
                                    " Utilizador.username " +
                       " FROM Utilizador ,Cliente, Encomenda " +
                       " WHERE " +
                       "  Utilizador.idUtilizador = Cliente.idCliente " + 
                       " AND " +
                       "  Cliente.idCliente = Encomenda.idCliente";

        SqlCommand comando = new SqlCommand(query);

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            encomenda = new Encomenda();
            encomenda.Cliente = new Cliente();

            encomenda.IdEncomenda = (string)resposta["idEncomenda"];
            encomenda.IdCliente = (string)resposta["idCliente"];
            encomenda.ValorTotal = double.Parse((string)resposta["valorTotal"]);
            encomenda.DataCriacao = (string)resposta["dataCriacao"];
            encomenda.Cliente.Username = (string)resposta["username"];

            listaEncomendas.Add(encomenda);
        }

        if (resposta != null)
            resposta.Close();

        return listaEncomendas;
    }

    public ArrayList getHistoricoEstados(string idEncomenda, ComunicacaoBD comunicacaoBD)
    {
        EstadoEncomenda estadoEncomenda = null;
        ArrayList listaEstados = new ArrayList();

        string query = "SELECT EstadoHistorico.idEncomenda, EstadoHistorico.nomeEstado, EstadoHistorico.contador, " + 
                                " EstadoHistorico.dataAlteracao " +
                       " FROM EstadoHistorico " +
                       " WHERE " +
                       "  EstadoHistorico.idEncomenda = @idEncomenda " +
                       " ORDER BY EstadoHistorico.contador DESC";        

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            estadoEncomenda = new EstadoEncomenda();

            estadoEncomenda.IdEncomenda = (string)resposta["idEncomenda"];
            estadoEncomenda.NomeEstado = (string)resposta["nomeEstado"];
            estadoEncomenda.Contador = (int)resposta["contador"];
            estadoEncomenda.DataAlteracao = (string)resposta["dataAlteracao"];

            listaEstados.Add(estadoEncomenda);
        }

        if (resposta != null)
            resposta.Close();

        return listaEstados;
    }

    public ArrayList getLivrosEncomenda(string idEncomenda, ComunicacaoBD comunicacaoBD)
    {
        Livro livro = null;
        ArrayList listaLivros = new ArrayList();

        string query = "SELECT Livro.idLivro, Livro.ISBN, Livro.titulo, Livro.categoria, Livro.autores, Livro.editora, " +
                                " Livro.anoEdicao,Livro.urlImagem, Livro.tempoEntrega, Livro.nomeFornecedor, Livro.numPesquisas, " +
                                " LinhaEncomenda.comDesconto, LinhaEncomenda.precoVenda, LinhaEncomenda.quantidade" +
                        " FROM Livro, Encomenda, LinhaEncomenda" +
                        " WHERE " +
                            " Encomenda.idEncomenda = @idEncomenda " +
                            " AND Encomenda.idEncomenda = LinhaEncomenda.idEncomenda " +
                            " AND LinhaEncomenda.idLivro = Livro.idLivro";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idEncomenda", idEncomenda));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            livro = new Livro();

            livro.IdLivro = (string)resposta["idLivro"];
            livro.ISBN = (string)resposta["ISBN"];
            livro.Titulo = (string)resposta["titulo"];
            livro.Categoria = (string)resposta["categoria"];
            livro.Autores = (string)resposta["autores"];
            livro.Editora = (string)resposta["editora"];
            livro.AnoEdicao = (string)resposta["anoEdicao"];
            livro.PrecoVenda = ((string)resposta["precoVenda"]).Replace(".", ",");
            livro.PrecoSemDesconto = 0.0 + "";
            livro.UrlImagem = (string)resposta["urlImagem"];
            livro.TempoEntrega = (string)resposta["tempoEntrega"];
            livro.Desconto = (bool)resposta["comDesconto"];
            livro.NumPesquisas = (int)resposta["numPesquisas"];
            livro.NomeFornecedor = (string)resposta["nomeFornecedor"];
            livro.Quantidade = (int)resposta["quantidade"];

            listaLivros.Add(livro);
        }

        if (resposta != null)
            resposta.Close();

        return listaLivros;
    }


    //+++++++++++++++++++++++++++++++ PESQUISAS HISTORICO ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public string getIdPesquisasHistorico(string idCliente, ComunicacaoBD comunicacaoBD)
    {
        string query = "SELECT idPesquisasHistorico " +
                        " FROM PesquisasHistorico " +
                        " WHERE " +
                            " idCliente = @idCliente";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        string idPesquisasHistorico;

        if (resposta.HasRows)
        {
            resposta.Read();
            idPesquisasHistorico = (string)resposta["idPesquisasHistorico"];
        }
        else
        {
            idPesquisasHistorico = null;
        }

        if (resposta != null)
            resposta.Close();

        return idPesquisasHistorico;
    }

    public string[] verificaLivroPesquisasHistorico(string idCliente, string idLivro, ComunicacaoBD comunicacaoBD)
    {
        string query = "SELECT idLivro, numero " +
                        " FROM PesquisasHistorico " +
                        " WHERE " +
                            " idCliente = @idCliente " +
                            " AND " +
                            " idLivro = @idLivro"; ;

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));
        comando.Parameters.Add(new SqlParameter("@idLivro", idLivro));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        string[] LivroAux = null;

        if (resposta.HasRows)
        {
            resposta.Read();
            LivroAux = new string[2];
            LivroAux[0] = (string)resposta["idLivro"];
            LivroAux[1] = resposta["numero"].ToString();
        }
        else
        {
            LivroAux = null;
        }

        if (resposta != null)
            resposta.Close();

        return LivroAux;
    }

    public int insereLivroPesquisasHistorico(string idPesquisasHistorico, string idCliente, string idLivro, int numero, ComunicacaoBD comunicacaoBD)
    {
        string query = "INSERT INTO PesquisasHistorico(idPesquisasHistorico, idCliente, idLivro, numero)" +
                                        "VALUES(@idPesquisasHistorico, @idCliente, @idLivro, @numero)";

        SqlCommand comando = new SqlCommand(query);

        if (idPesquisasHistorico == null || idPesquisasHistorico == "")
        {
            comando.Parameters.Add(new SqlParameter("@idPesquisasHistorico", getGUID()));
        }
        else
        {
            comando.Parameters.Add(new SqlParameter("@idPesquisasHistorico", idPesquisasHistorico));
        }

        comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));
        comando.Parameters.Add(new SqlParameter("@idLivro", idLivro));
        comando.Parameters.Add(new SqlParameter("@numero", numero));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    public int incrementaNumeroPesquisasHistorico(string idCliente, string idLivro, int numero, ComunicacaoBD comunicacaoBD)
    {
        string query = null;

        query = "UPDATE PesquisasHistorico" +
                        " SET " +
                             " numero = @numero" +
                         " WHERE " +
                             " idCliente = @idCliente" +
                             " AND " +
                             " idLivro = @idLivro";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));
        comando.Parameters.Add(new SqlParameter("@idLivro", idLivro));
        comando.Parameters.Add(new SqlParameter("@numero", numero));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    public ArrayList getLivrosPesquisasHistorico(string username, ComunicacaoBD comunicacaoBD)
    {
        Livro livro = null;
        ArrayList listaLivros = new ArrayList();

        string query = "SELECT Livro.idLivro, Livro.ISBN, Livro.titulo, Livro.categoria, Livro.autores, " +
                                " Livro.editora, Livro.anoEdicao, Livro.precoVenda, Livro.precoSemDesconto, Livro.urlImagem, " +
                                " Livro.tempoEntrega, Livro.desconto, Livro.nomeFornecedor, PesquisasHistorico.numero " +
                        " FROM Livro, PesquisasHistorico, Cliente, Utilizador " +
                        " WHERE " +
                            " Utilizador.username = @username" +
                            " AND " +
                            " Utilizador.idUtilizador = Cliente.idCliente " +
                            " AND " +
                            " Cliente.idCliente = PesquisasHistorico.idCliente " +
                            " AND " +
                            " Livro.idLivro  = PesquisasHistorico.idLivro " +
                            " AND " +
                            " Livro.idLivro IN (SELECT PesquisasHistorico.idLivro" +
                                                    " FROM PesquisasHistorico, Cliente, Utilizador " +
                                                    " WHERE " +
                                                        " Utilizador.username = @username" +
                                                        " AND Cliente.idCliente = Utilizador.idUtilizador " +
                                                        " AND Cliente.idCliente = PesquisasHistorico.idCliente )";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@username", username));

        SqlDataReader resposta = invocaQueryLeitura(comando, comunicacaoBD);

        while (resposta.Read() == true)
        {
            livro = new Livro();

            livro.IdLivro = (string)resposta["idLivro"];
            livro.ISBN = (string)resposta["ISBN"];
            livro.Titulo = (string)resposta["titulo"];
            livro.Categoria = (string)resposta["categoria"];
            livro.Autores = (string)resposta["autores"];
            livro.Editora = (string)resposta["editora"];
            livro.AnoEdicao = (string)resposta["anoEdicao"];
            livro.PrecoVenda = (string)resposta["PrecoVenda"];
            livro.PrecoSemDesconto = (string)resposta["precoSemDesconto"];
            livro.UrlImagem = (string)resposta["urlImagem"];
            livro.TempoEntrega = (string)resposta["tempoEntrega"];
            livro.Desconto = (bool)resposta["desconto"];
            livro.NomeFornecedor = (string)resposta["nomeFornecedor"];
            livro.NumPesquisas = (int)resposta["numero"];

            listaLivros.Add(livro);
        }

        if (resposta != null)
            resposta.Close();

        return listaLivros;
    }

    // TOPSELLERS

    public int guardaTopSellers(string tipoCapa, string titulo, string autores, ComunicacaoBD comunicacaoBD)
    {
        string query = "INSERT INTO TopSellers(idTopSellers, tipoCapa, titulo, autores)" +
                                    "VALUES( @idTopSellers, @tipoCapa, @titulo, @autores)";

        SqlCommand comando = new SqlCommand(query);

        comando.Parameters.Add(new SqlParameter("@idTopSellers", getGUID()));
        comando.Parameters.Add(new SqlParameter("@tipoCapa", tipoCapa));
        comando.Parameters.Add(new SqlParameter("@titulo", titulo));
        comando.Parameters.Add(new SqlParameter("@autores", autores));

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    public int apagaTopSellers(ComunicacaoBD comunicacaoBD)
    {
        string query = "DELETE FROM TopSellers";

        SqlCommand comando = new SqlCommand(query);

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }


    // AUXILIARES

    public int limpaTabela(string nomeTabela, ComunicacaoBD comunicacaoBD)
    {
        string query = "DELETE FROM " + nomeTabela;

        SqlCommand comando = new SqlCommand(query);

        int i = invocaQuery(comando, comunicacaoBD);

        return i;
    }

    /*

    public int actualizaDadosTabelaCliente(string idCliente, string telefone, string address, string city, string state,

                                string ZIPcode, string country, string numCartaoCredito, string dataValidadeCartaoCredito)                                

    {

        string query = "UPDATE Cliente" +

                        " SET " +                           

                             ", telefone = '" + telefone + "'" +

                             ", address = '" + address + "'" +

                             ", city = '" + city + "'" +

                             ", state = '" + state + "'" +

                             ", ZIPcode = '" + ZIPcode + "'" +

                             ", country = '" + country + "'" +

                             ", numCartaoCredito = '" + numCartaoCredito + "'" +

                             ", dataValidadeCartaoCredito = '" + dataValidadeCartaoCredito + "'" +

                         "WHERE" +

                             "idCliente = '" + idCliente + "'";



        int i = invocaQuery(query);



        return i;

    }



    //+++++++++++++++++++++++++++++++ LIVRO ++++++++++++++++++++++++++++++++++++++++++++++++++++++



    



    /// <summary>

    /// actualiza o desconto e o top5 ou ambos

    /// </summary>

    /// <param name="idLivro"></param>

    /// <param name="ISBN"></param>

    /// <param name="desconto"></param>

    /// <param name="top5"></param>

    /// <returns>Numero de linhas afectadas</returns>

    public int actualizaDescontoLivro(string idLivro, string ISBN, string desconto, string top5)

    {

        string query = null;



        if (desconto != null)

        {

            query = "UPDATE Livro" +

                            " SET " +

                                 " desconto = '" + desconto + "'" +

                             " WHERE " +

                                 " ISBN = '" + ISBN + "'";

        }

        else if (top5 != null)

        {

            query = "UPDATE Livro" +

                            " SET " +

                                 ", top5 = '" + top5 + "'" +

                             " WHERE " +

                                 " ISBN = '" + ISBN + "'";

        }

        else

        {

            return 0;

        }



        int i = invocaQuery(query);



        return i;

    }



    



    /// <summary>

    /// vai buscar os dados do livro

    /// </summary>

    /// <param name="idLivro">identificador do livro</param>

    /// <returns name="Livro">devolve um livro</returns>

    public Livro getLivro(string idLivro)

    {

        Livro livro = null;



        string query = "SELECT * " +

                        " FROM Livro " +

                        " WHERE " +

                            " idLivro = '" + idLivro + "'";



        SqlDataReader resposta = invocaQueryLeitura(query);



        while (resposta.Read() == true)

        {

            livro = new Livro();



            livro.IdLivro = (string)resposta["idLivro"];

            livro.ISBN = (string)resposta["ISBN"];

            livro.Titulo = (string)resposta["titulo"];

            livro.Categoria = (string)resposta["categoria"];

            livro.Autores = (string)resposta["autores"];

            livro.Editora = (string)resposta["editora"];

            livro.AnoEdicao = (string)resposta["anoEdicao"];            

            livro.PrecoSemDesconto = (string)resposta["precoSemDesconto"];            

            livro.UrlImagem = (string)resposta["urlImagem"];

            livro.TempoEntrega = (string)resposta["tempoEntrega"];

            livro.Desconto = float.Parse((string)resposta["desconto"]);

            livro.Top5 = float.Parse((string)resposta["top5"]);

            livro.EstadoConservacao = (string)resposta["estadoConservacao"];

            livro.NumPesquisas = (int)resposta["numPesquisas"];

            livro.NomeFornecedor = (string)resposta["nomeFornecedor"];







            livro.PrecoVenda = fazDesconto(livro.PrecoSemDesconto, livro.Desconto, livro.Top5);

        }



        if (resposta != null)

            resposta.Close();



        return livro;

    }



    //+++++++++++++++++++++++++++++++ ENCOMENDA ++++++++++++++++++++++++++++++++++++++++++++++++++++++



    public string getIdEncomenda(string idCliente)

    {



        string query = "SELECT idEncomenda " +

                        " FROM Encomenda " +

                        " WHERE " +

                            " idCliente = '" + idCliente + "'";



        SqlDataReader resposta = invocaQueryLeitura(query);



        string idEncomenda;



        if (resposta.HasRows)

        {

            resposta.Read();

            idEncomenda = (string)resposta["idEncomenda"];

        }

        else

        {

            idEncomenda = null;

        }

        if (resposta != null)

            resposta.Close();



        return idEncomenda;

    }



    



    //////////////////////////////////////////////////////////////



    private string fazDesconto(string precoSemDesconto, float desconto, float top5) { 



        float precoVenda = 0.0F;



        string aux = precoSemDesconto.TrimStart(new char[1]{'$'});

        

        float precoDesconto = float.Parse(aux.Replace('.',','));



        precoVenda = precoDesconto - (precoDesconto * desconto + precoDesconto * top5);



        return "$"+precoVenda;

    }    

     */

}

   



