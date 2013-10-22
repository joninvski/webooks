using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;

[WebService(Namespace = "http://www.WEBooksBD.pt/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WEBooksBDService : System.Web.Services.WebService
{

    Servicos servico = new Servicos();

    public WEBooksBDService()
    {       
    }


    //CLIENTE

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nome"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="passwordValidacao"></param>
    /// <param name="telefone"></param>
    /// <param name="address"></param>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <param name="ZIPcode"></param>
    /// <param name="country"></param>
    /// <param name="numCartaoCredito"></param>
    /// <param name="dataValidadeCartaoValidade"></param>
    /// <exception cref="ErroPassword"/>
    /// <exception cref="DataValidadeException"/>
    /// <exception cref="UsernameExistenteException"/>
    /// <exception cref="UtilizadorNaoGuardado"/>

    [WebMethod]
    public string InsereCliente(string nome, string username, string password, string telefone, string numero, string address,
                                string city, string state, string ZIPcode, string country, string longitude, string latitude,
                                 string numCartaoCredito, string dataValidadeCartaoValidade) 
    {
        string estado = "activo";

        DateTime data = DateTime.Parse(dataValidadeCartaoValidade);

        if (data.Date < DateTime.Now.Date)
            throw new DataValidadeException();

        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            Utilizador utilizador = servico.getDadosUtilizador(username, comunicacaoBD);

            if (utilizador == null)
            {
                string idUtilizador = servico.InsereUtilizador(nome, username, password, comunicacaoBD);

                servico.InsereCliente(idUtilizador, telefone, numero, address, city, state,
                                                ZIPcode, country, latitude, longitude, numCartaoCredito, 
                                                dataValidadeCartaoValidade, estado, comunicacaoBD);
            }else
            {
                comunicacaoBD.Transacao.Rollback();
                throw new SoapException("Utilizador ja existente",null,new UsernameExistenteException());
            }
        }catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new UtilizadorNaoGuardado(); 
        }

        comunicacaoBD.Transacao.Commit();
        return username;
    }

    [WebMethod]
    public Livro[] HistoricoComprasLivros(string username)
    {
        ArrayList listaLivros = null;
        Livro[] livros = null;

        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            listaLivros = servico.getLivrosComprados(username, comunicacaoBD);
            livros = new Livro[listaLivros.Count];

            int i = 0;
            foreach (Livro livro in listaLivros)
            {
                livros[i++] = livro;
            }
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return null;
        }
        comunicacaoBD.Transacao.Commit();

        return livros;
    }

    /// <summary>
    /// Devolve as encomendas que foram entregues/compradas
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [WebMethod]
    public Encomenda[] HistoricoComprasEncomenda(string username)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        int jEnc = 0;
        ArrayList aux = new ArrayList();
        Encomenda[] encomendas = null;

        ArrayList listaEncomendas = this.MostraEncomendas("todas");
        Cliente cliente = servico.getDadosCliente(username, comunicacaoBD);

        if (cliente != null)
        {
            foreach (Encomenda enc in listaEncomendas)
            {
                if (enc.IdCliente == cliente.IdCliente && enc.Estado.Trim() == "entregue")
                {
                    aux.Add(enc);
                }
            }

            encomendas = new Encomenda[aux.Count];
            foreach (Encomenda encomenda in aux)
            {
                encomendas[jEnc++] = encomenda;
            }
        }
        else {
            comunicacaoBD.Transacao.Rollback();
            return null;
        }

        comunicacaoBD.Transacao.Commit();
        return encomendas;
    }

    /// <summary>
    /// Devolve a lista de encomendas do cliente
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [WebMethod]
    public Encomenda[] MostraEncomendasCliente(string username)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        int jEnc = 0;
        ArrayList aux = new ArrayList();
        Encomenda[] encomendas = null;

        ArrayList listaEncomendas = this.MostraEncomendas("todas");
        Cliente cliente = servico.getDadosCliente(username, comunicacaoBD);

        if (cliente != null)
        {

            foreach (Encomenda enc in listaEncomendas)
            {
                if (enc.IdCliente == cliente.IdCliente)
                {
                    aux.Add(enc);
                }
            }

            encomendas = new Encomenda[aux.Count];
            foreach (Encomenda encomenda in aux)
            {
                encomendas[jEnc++] = encomenda;
            }
        }

        return encomendas;
    }

    /// <summary>
    /// Devolve a lista de encomendas pendentes do cliente
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public bool MostraEncomendasPendentesCliente(string username)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        int jEnc = 0;
        ArrayList aux = new ArrayList();
        Encomenda[] encomendas = null;

        ArrayList listaEncomendas = this.MostraEncomendas("pendente");
        Cliente cliente = servico.getDadosCliente(username, comunicacaoBD);

        if (cliente != null)
        {

            foreach (Encomenda enc in listaEncomendas)
            {
                if (enc.IdCliente == cliente.IdCliente)
                {
                    return true;
                }
            }
        }

        return false;
    }

    [WebMethod]
    public void ApagaCliente(string username)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        Cliente cliente = servico.getDadosCliente(username, comunicacaoBD);

        try{
            if (MostraEncomendasPendentesCliente(username))
            {
                throw new Exception("ExistemEncomendasPendentes");
            }

            servico.apagaCliente(cliente.IdCliente, comunicacaoBD);
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return; 
        }
        comunicacaoBD.Transacao.Commit();
        return;
    }

    [WebMethod]
    public Cliente[] MostraClientes()
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        ArrayList listaClientes;
        Cliente[] clientes;

        int i = 0;

        try
        {
            listaClientes = servico.getClientes(comunicacaoBD);

            clientes = new Cliente[listaClientes.Count];
            foreach (Cliente cliente in listaClientes) {
                clientes[i++] = cliente; 
            }

        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return null;
        }
        comunicacaoBD.Transacao.Commit();
        return clientes;
    }

    [WebMethod]
    public Cliente MostraDadosCliente(string username)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        Cliente cliente;

        try
        {
            cliente = servico.getDadosCliente(username, comunicacaoBD);
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return null;
        }
        comunicacaoBD.Transacao.Commit();
        return cliente;
    }

    //GESTOR

    [WebMethod]
    public void InsereGestor(string nome, string username, string password)
    {        
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            Utilizador utilizador = servico.getDadosUtilizador(username, comunicacaoBD);

            if (utilizador == null)
            {
                string idUtilizador = servico.InsereUtilizador(nome, username, password, comunicacaoBD);

                servico.InsereGestor(idUtilizador, comunicacaoBD);
            }
            else
            {
                comunicacaoBD.Transacao.Rollback();
                throw new UsernameExistenteException();
            }
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new UtilizadorNaoGuardado();
        }

        comunicacaoBD.Transacao.Commit();
    }

    //CARRINHO DE COMPRAS

    [WebMethod]
    public void InsereLivroCarrinhoCompras(string username, string ISBN, int quantidade)
    {

      /*  if (quantidade <= 0)
        {
            throw new QuantidadeInvalidaExceptions();
        }*/

        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        try
        {
            Cliente cliente = servico.getDadosCliente(username, comunicacaoBD);

            if (cliente == null)
            {
                throw new Exception("ClienteInexistente");
            }

            string idCarrinho = servico.getIdCarrinho(cliente.IdCliente, comunicacaoBD);
            Livro livro = servico.getLivro(ISBN, comunicacaoBD);

            if (livro == null)
            {
                throw new Exception("LivroInexistente");
            }

            servico.insereLivroCarrinhoCompras(idCarrinho, cliente.IdCliente, livro.IdLivro, quantidade, comunicacaoBD);
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("LivroExistenteNoCarrinhoException");
        }

        comunicacaoBD.Transacao.Commit();
    }

    [WebMethod]
    public Livro[] ListaCarrinhoCompras(string username)

    {

        ArrayList listaLivros = null;

        Livro[] livros = null;



        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        try

        {

            listaLivros = servico.getLivrosDoCarrinho(username, comunicacaoBD);



            if (listaLivros.Count == 0)

            {

                throw new CarrinhoVazioException();

            }



            livros = new Livro[listaLivros.Count];



            int i = 0;

            foreach (Livro livro in listaLivros)

            {

                livros[i++] = livro;

            }

        }

        catch (SqlException e)

        {

            comunicacaoBD.Transacao.Rollback();

            return null;

        }

        comunicacaoBD.Transacao.Commit();



        return livros;

    }

    [WebMethod]
    public void RemoveLivroDoCarrinhoCompras(string username, string ISBN, string nomeFornecedor)

    {

        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();



        try

        {

            Cliente cliente = servico.getDadosCliente(username, comunicacaoBD);



            string idCarrinho = servico.getIdCarrinho(cliente.IdCliente, comunicacaoBD);



            Livro livro = servico.getLivro(ISBN, comunicacaoBD);



            servico.removeLivroCarrinhoCompras(idCarrinho, cliente.IdCliente, livro.IdLivro, comunicacaoBD);

        }

        catch (Exception e)

        {

            comunicacaoBD.Transacao.Rollback();

            throw new LivroNaoExistenteNoCarrinhoException();

        }

        comunicacaoBD.Transacao.Commit();

    }

    [WebMethod]
    public void LimpaCarrinhoCompras(string username)

    {

        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        try

        {

            Cliente cliente = servico.getDadosCliente(username, comunicacaoBD);



            if (cliente == null)

            {

                comunicacaoBD.Transacao.Rollback();

                throw new ClienteInexistente();

            }



            string idCarrinho = servico.getIdCarrinho(cliente.IdCliente, comunicacaoBD);



            servico.limpaCarrinhoCompras(idCarrinho, cliente.IdCliente, comunicacaoBD);



        }

        catch (SqlException e)

        {

            comunicacaoBD.Transacao.Rollback();

            throw new CarrinhoNaoLimpo();

        }

        comunicacaoBD.Transacao.Commit();



    }

    //PESQUISAS HISTORICO

    [WebMethod]
    public void InsereListaPesquisaHistorico(Livro[] listaLivros, string username)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        Livro aux = null;
        int i = 0;
        string[] livroAux = null;
        Cliente cliente = servico.getDadosCliente(username, comunicacaoBD);
       // string idPesquisasHistorico = servico.getIdPesquisasHistorico(cliente.IdCliente, comunicacaoBD);
        try
        {
            foreach (Livro livro in listaLivros)
            {
                aux = servico.getLivro(livro.ISBN, comunicacaoBD);

                livroAux = servico.verificaLivroPesquisasHistorico(cliente.IdCliente, aux.IdLivro, comunicacaoBD);

                if (livroAux != null)
                {
                    i = servico.incrementaNumeroPesquisasHistorico(cliente.IdCliente, livroAux[0], int.Parse(livroAux[1]) + 1, comunicacaoBD);
                }
                else
                {
                    i = servico.insereLivroPesquisasHistorico("", cliente.IdCliente, aux.IdLivro, 1, comunicacaoBD);                    
                }

                if (i == 0)
                {
                    throw new ErroGuardarLivro("Erro: a guardar o livro no Historico: " + livro.Titulo);
                }
            }
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new ErroGuardarLivro("Erro a guardar lista de livros no Historico" + aux.Titulo);
        }

        comunicacaoBD.Transacao.Commit();
    }

    [WebMethod]
    public Livro[] ListaPesquisaHistorico(string username)
    {
        ArrayList listaLivros = null;
        Livro[] livros = null;

        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            listaLivros = servico.getLivrosPesquisasHistorico(username, comunicacaoBD);

            livros = new Livro[listaLivros.Count];

            int i = 0;
            foreach (Livro livro in listaLivros)
            {
                livros[i++] = livro;
            }
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return null;
        }
        comunicacaoBD.Transacao.Commit();
        return livros;
    }

    // LIVRO

    [WebMethod]
    public void InsereListaLivros(Livro[] listaLivros)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        Livro aux = null;
        try{
            foreach (Livro livro in listaLivros) {

                aux = servico.getLivro(livro.ISBN, comunicacaoBD);                

                if (aux == null)
                {
                    aux = livro;
                    
                    int i = servico.guardaLivro(livro.ISBN, livro.Titulo, livro.Categoria, livro.Autores, livro.Editora, livro.AnoEdicao,
                                                livro.PrecoVenda.Replace(".", ","), livro.PrecoSemDesconto.Replace(",", "."), livro.UrlImagem, livro.TempoEntrega,
                                                livro.Desconto, livro.NomeFornecedor, comunicacaoBD);                    

                    if (i == 0)
                    {
                        throw new ErroGuardarLivro("Erro: a guardar o livro: " + livro.Titulo);
                    }
                }else {
                    servico.actualizaNumeroPesquisas(livro.ISBN, livro.NomeFornecedor, ++aux.NumPesquisas, livro.PrecoVenda, livro.PrecoSemDesconto, comunicacaoBD);
                }
            } 
        }catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new ErroGuardarLivro("Erro a guardar lista de livros " + aux.Titulo);
        }

        comunicacaoBD.Transacao.Commit();
    }

   // [WebMethod] Esta comentado de proposito
    public void InsereLivro(string ISBN, string titulo, string categoria, string autores,
                            string editora, string anoEdicao, string precoVenda, string precoSemDesconto, string urlImagem,
                            string tempoEntrega, bool desconto, string nomeFornecedor) 
    {

        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        int i = 0;

        try {

            Livro aux = servico.getLivro(ISBN, comunicacaoBD);

            if (aux == null)
            {
               i = servico.guardaLivro(ISBN, titulo, categoria, autores, editora, anoEdicao, precoVenda , 
                                        precoSemDesconto, urlImagem, tempoEntrega,
                                        desconto, nomeFornecedor, comunicacaoBD);
            }else
            {
                i = servico.actualizaNumeroPesquisas(aux.ISBN, aux.NomeFornecedor, ++aux.NumPesquisas, precoVenda, precoSemDesconto, comunicacaoBD);
            }

            if (i == 0) {
                comunicacaoBD.Transacao.Rollback();
                throw new ErroGuardarLivro();
            }
        }catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new ErroGuardarLivro();
        }

        comunicacaoBD.Transacao.Commit();
    }

    [WebMethod]
    public Encomenda[] MostraEncomendasLivro(string ISBN)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        int jEnc = 0;
        ArrayList aux = new ArrayList();
        Encomenda[] encomendas = null;

        ArrayList listaEncomendas = this.MostraEncomendas("todas");
        
        foreach (Encomenda enc in listaEncomendas)
        {
            foreach (Livro liv in enc.ListaLivros) {
                if (liv.ISBN.Trim() == ISBN) {
                    aux.Add(enc);
                }
            }
        }

        encomendas = new Encomenda[aux.Count];
        foreach (Encomenda encomenda in aux)
        {
            encomendas[jEnc++] = encomenda;
        }
        
        return encomendas;
    }

    [WebMethod]
    public Livro[] MostraLivrosMaisComprados(int numeroLivros)
    {
        ArrayList listaLivros = null;
        Livro[] livros = null;

        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            listaLivros = servico.getLivrosMaisComprados(numeroLivros, comunicacaoBD);

            livros = new Livro[listaLivros.Count];

            int i = 0;
            foreach (Livro livro in listaLivros)
            {
                livros[i++] = livro;
            }
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return null;
        }
        comunicacaoBD.Transacao.Commit();

        return livros;
    }

    [WebMethod]
    public Livro[] MostraLivrosMaisPesquisados(int numeroLivros)
    {
        ArrayList listaLivros = null;
        Livro[] livros = null;

        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            listaLivros = servico.getLivrosMaisPesquisados(numeroLivros, comunicacaoBD);

            livros = new Livro[listaLivros.Count];

            int i = 0;
            foreach (Livro livro in listaLivros)
            {
                livros[i++] = livro;
            }
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return null;
        }
        comunicacaoBD.Transacao.Commit();

        return livros;
    }

    [WebMethod]
    public int MostraNumeroLivrosPesquisados()
    {
        int num = 0;
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            num = servico.getNumeroLivrosPesquisados(comunicacaoBD);
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return 0;
        }
        comunicacaoBD.Transacao.Commit();

        return num;
    }

    [WebMethod]
    public int MostraNumeroLivrosComprados()
    {
        int num = 0;
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            num = servico.getNumeroLivrosComprados(comunicacaoBD);
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return 0;
        }
        comunicacaoBD.Transacao.Commit();

        return num;
    }

    [WebMethod]
    public int MostraNumeroLivrosCompradosComDesconto()
    {
        int num = 0;
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            num = servico.getNumeroLivrosDesconto(1,comunicacaoBD);
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return 0;
        }
        comunicacaoBD.Transacao.Commit();

        return num;
    }

    [WebMethod]
    public int MostraNumeroLivrosCompradosSemDesconto()
    {
        int num = 0;
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            num = servico.getNumeroLivrosDesconto(0, comunicacaoBD);
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            return 0;
        }
        comunicacaoBD.Transacao.Commit();

        return num;
    }

    [WebMethod]
    public DadosGestao MostraDadosGestao()
    {
        DadosGestao dadosGestao = new DadosGestao();

        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            dadosGestao.NumSemDesconto = servico.getNumeroLivrosDesconto(0, comunicacaoBD);
            dadosGestao.NumComDesconto = servico.getNumeroLivrosDesconto(1, comunicacaoBD);
            dadosGestao.NumComprados = servico.getNumeroLivrosComprados(comunicacaoBD);
            dadosGestao.NumPesquisados = servico.getNumeroLivrosPesquisados(comunicacaoBD); ;

        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("ERRO: Não e posivel aceder aos dados");
        }
        comunicacaoBD.Transacao.Commit();

        return dadosGestao;

    }

    // CHECKOUT

    [WebMethod]
    public Encomenda CheckOut(string username)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        string dataCriacao = DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year;
        Encomenda encomenda = null;

        try
        {
            Cliente cliente = servico.getDadosCliente(username, comunicacaoBD);

            if (cliente == null)
            {
                comunicacaoBD.Transacao.Rollback();
                throw new UtilizadorInexistente();
            }

            string idCarrinho = servico.getIdCarrinho(cliente.IdCliente, comunicacaoBD);

            if (idCarrinho == null)
            {
                comunicacaoBD.Transacao.Rollback();
                throw new CarrinhoVazioException();
            }

            ArrayList listalivros = servico.getLivrosDoCarrinho(username, comunicacaoBD);

            string idEncomenda = servico.CriaEncomenda(cliente.IdCliente, "0,0", dataCriacao, comunicacaoBD);

            //variavel qie
            //string comDesconto = "";

            foreach (Livro livro in listalivros)
            {
                servico.insereLinhaEncomenda(idEncomenda, livro.IdLivro, livro.Quantidade, livro.PrecoVenda, livro.Desconto, comunicacaoBD);
            }

            servico.limpaCarrinhoCompras(idCarrinho, cliente.IdCliente, comunicacaoBD);

            servico.CriaEstadoHistorico(idEncomenda, "colocada", 1, dataCriacao, comunicacaoBD);

            encomenda = getEncomenda(idEncomenda, comunicacaoBD);
            
        }catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
        }

        comunicacaoBD.Transacao.Commit();
        return encomenda;
    }

    // UTILIZADOR

    [WebMethod]
    public Utilizador LoginUtilizador(string username, string password)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        try
        {
            Gestor gestor = servico.getDadosGestor(username, comunicacaoBD);
            Cliente cliente = servico.getDadosCliente(username, comunicacaoBD);

            if (gestor != null)
            {
                if (gestor.Password.Trim() != password)
                {
                    comunicacaoBD.Transacao.Rollback();
                    throw new Exception();
                }
                else
                {
                    comunicacaoBD.Transacao.Commit();
                    return new Utilizador(gestor);
                }
            }
            else if (cliente != null)
            {
                if (cliente.Estado.Trim() == "inactivo") {
                    comunicacaoBD.Transacao.Rollback();
                    throw new Exception();
                }
                else if (cliente.Password.Trim() != password)
                {
                    comunicacaoBD.Transacao.Rollback();
                    throw new Exception();
                }
                else
                {
                    comunicacaoBD.Transacao.Commit();
                    return new Utilizador(cliente);
                }
            }
            else
            {
                comunicacaoBD.Transacao.Rollback();
                throw new Exception();
            }
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception();
        }
    }


    //ENCOMENDA

    [WebMethod]
    public void CancelarEncomenda(string idEncomenda)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            string[] ultimoEstado = servico.getUltimoEstado(idEncomenda, comunicacaoBD);

            if (ultimoEstado[1].Trim() == "pendente")
            {
                string dataAlteracao = DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year;

               int contador = int.Parse(ultimoEstado[0]) + 1;

                servico.CriaEstadoHistorico(idEncomenda, "cancelada", contador, dataAlteracao, comunicacaoBD);
            }
            else
            {
                // envia aviso de erro de nao poder cancelar encomenda
                comunicacaoBD.Transacao.Rollback();
                return;
            }
        }
        catch (SqlException es) {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("ERRO: Nao é possivel Cancelar encomenda");            
        }
        comunicacaoBD.Transacao.Commit();
    }

    [WebMethod]
    public void AlteraEstadoEncomenda(string idEncomenda, string novoEstado)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            string[] ultimoEstado = servico.getUltimoEstado(idEncomenda, comunicacaoBD);

            string dataAlteracao = DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year;

            int contador = int.Parse(ultimoEstado[0]) + 1;

            servico.CriaEstadoHistorico(idEncomenda, novoEstado.ToLower(), contador, dataAlteracao, comunicacaoBD);
           
        }
        catch (SqlException es)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("ERRO: Nao é possivel alterar estado encomenda");
        }
        comunicacaoBD.Transacao.Commit();
    }

    [WebMethod]
    public Encomenda[] MostraEncomendasPendentes() 
    {
        int jEnc = 0;
        ArrayList listaEncomendas = this.MostraEncomendas("pendente");
        
        Encomenda[] encomendas = new Encomenda[listaEncomendas.Count];
        foreach (Encomenda encomenda in listaEncomendas)
        {
            encomendas[jEnc++] = encomenda;
        }

        return encomendas;
    }

    [WebMethod]
    public Encomenda[] MostraEncomendasCanceladas()
    {
        int jEnc = 0;
        ArrayList listaEncomendas = this.MostraEncomendas("cancelada");

        Encomenda[] encomendas = new Encomenda[listaEncomendas.Count];
        foreach (Encomenda encomenda in listaEncomendas)
        {
            encomendas[jEnc++] = encomenda;
        }

        return encomendas;
    }

    [WebMethod]
    public Encomenda[] MostraEncomendasColocadas()
    {
        int jEnc = 0;
        ArrayList listaEncomendas = this.MostraEncomendas("colocada");

        Encomenda[] encomendas = new Encomenda[listaEncomendas.Count];
        foreach (Encomenda encomenda in listaEncomendas)
        {
            encomendas[jEnc++] = encomenda;
        }

        return encomendas;
    }

    [WebMethod]
    public Encomenda[] MostraEncomendasEntregues()
    {
        int jEnc = 0;
        ArrayList listaEncomendas = this.MostraEncomendas("entregue");

        Encomenda[] encomendas = new Encomenda[listaEncomendas.Count];
        foreach (Encomenda encomenda in listaEncomendas)
        {
            encomendas[jEnc++] = encomenda;
        }

        return encomendas;
    }

    [WebMethod]
    public Encomenda[] MostraListaEncomendas()
    {
        int jEnc = 0;
        ArrayList listaEncomendas = this.MostraEncomendas("todas");

        Encomenda[] encomendas = new Encomenda[listaEncomendas.Count];
        foreach (Encomenda encomenda in listaEncomendas)
        {
            encomendas[jEnc++] = encomenda;
        }

        return encomendas;
    }

   
    // TOPSELLERS

    [WebMethod]
    public void InsereListaTopSellers(LivroPromocao[] listaLivrosPromocao)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        try
        {
            servico.apagaTopSellers(comunicacaoBD);

            foreach (LivroPromocao livro in listaLivrosPromocao)
            {
                servico.guardaTopSellers(livro.TipoCapa, livro.Titulo, livro.NomeAutor, comunicacaoBD);
            }
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("Erro a guardar lista de livros com Desconto");
        }
        comunicacaoBD.Transacao.Commit();
    }

    // Auxiliares
    public ArrayList MostraEncomendas(string nomeEstado)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        ArrayList listaEncomendas = new ArrayList();
        ArrayList listaLivros = null;
        ArrayList listaEstados = null;

        ArrayList aux = new ArrayList();

        Livro[] livros;
        EstadoEncomenda[] historicoEstados;

        int iLivro = 0;
        int iEstado = 0;
        double valorTotal = 0.0;

        try
        {
            listaEncomendas = servico.getListaEncomendas(comunicacaoBD);
                        
            foreach (Encomenda encomenda in listaEncomendas)
            {
                listaLivros = servico.getLivrosEncomenda(encomenda.IdEncomenda, comunicacaoBD);
                listaEstados = servico.getHistoricoEstados(encomenda.IdEncomenda, comunicacaoBD);

                livros = new Livro[listaLivros.Count];
                historicoEstados = new EstadoEncomenda[listaEstados.Count];

                string[] estado = servico.getUltimoEstado(encomenda.IdEncomenda, comunicacaoBD);

                if (estado[1].Trim() == nomeEstado || nomeEstado == "todas")
                {
                    foreach (Livro livro in listaLivros)
                    {
                        livros[iLivro++] = livro;
                        double teste = double.Parse(livro.PrecoVenda);
                        valorTotal += teste * livro.Quantidade;
                    }

                    foreach (EstadoEncomenda estadoEnc in listaEstados) {
                        historicoEstados[iEstado++] = estadoEnc;
                    }

                    encomenda.ValorTotal = valorTotal;
                    encomenda.ListaLivros = livros;
                    encomenda.Estado = estado[1];
                    encomenda.HistoricoEstado = historicoEstados;

                    aux.Add(encomenda);

                    iLivro = 0;
                    iEstado = 0;
                    valorTotal = 0.0;
                    continue;
                }

                iLivro = 0;
                iEstado = 0;
                valorTotal = 0.0;


            }           
        }
        catch (SqlException es)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("ERRO: Nao é possivel Cancelar encomenda");
        }
        comunicacaoBD.Transacao.Commit();
        return aux;
    }

    public Encomenda getEncomenda(string idEncomenda, ComunicacaoBD comunicacaoBD)
    {
        ArrayList listaEncomendas = new ArrayList();
        ArrayList listaLivros = null;
        ArrayList listaEstados = null;

        Encomenda encomendaRetorna = null;

        Livro[] livros;
        EstadoEncomenda[] historicoEstados;

        int iLivro = 0;
        int iEstado = 0;
        double valorTotal = 0.0;

       
        listaEncomendas = servico.getListaEncomendas(comunicacaoBD);

        foreach (Encomenda encomenda in listaEncomendas)
        {
            if (encomenda.IdEncomenda.Trim() == idEncomenda)
            {
                listaLivros = servico.getLivrosEncomenda(encomenda.IdEncomenda, comunicacaoBD);
                listaEstados = servico.getHistoricoEstados(encomenda.IdEncomenda, comunicacaoBD);

                livros = new Livro[listaLivros.Count];
                historicoEstados = new EstadoEncomenda[listaEstados.Count];

                string[] estado = servico.getUltimoEstado(encomenda.IdEncomenda, comunicacaoBD);

                foreach (Livro livro in listaLivros)
                {
                    livros[iLivro++] = livro;
                    double teste = double.Parse(livro.PrecoVenda);
                    valorTotal += teste * livro.Quantidade;
                }

                foreach (EstadoEncomenda estadoEnc in listaEstados)
                {
                    historicoEstados[iEstado++] = estadoEnc;
                }

                encomenda.ValorTotal = valorTotal;
                encomenda.ListaLivros = livros;
                encomenda.Estado = estado[1];
                encomenda.HistoricoEstado = historicoEstados;

                encomendaRetorna = encomenda;

                break;
            }
        }
       
        return encomendaRetorna;
    }

   // [WebMethod]
    public void limpaBD()
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        string[] tabelas = new string[10] { "LinhaEncomenda", "Carrinho", "EstadoHistorico", "PesquisasHistorico", "Encomenda", "Cliente", "Gestor", "Livro", "Utilizador", "TopSellers" };
        


        try
        {
            foreach (string nometabela in tabelas) {
                servico.limpaTabela(nometabela, comunicacaoBD);
            }
        }
        catch (SqlException es)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("ERRO: Nao é possivel limpar tabelas");
        }
        comunicacaoBD.Transacao.Commit();
    }

    /*
   public ArrayList MostraEncomendas2(string nomeEstado)
   {
       ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

       ArrayList listaEncomendas = new ArrayList();
       ArrayList listaLivros = null;

       ArrayList aux = new ArrayList();

       Livro[] livros;

       int iLivro = 0;
       double valorTotal = 0.0;

       try
       {
           listaEncomendas = servico.getListaEncomendas(comunicacaoBD);

           foreach (Encomenda encomenda in listaEncomendas)
           {
               listaLivros = servico.getLivrosEncomenda(encomenda.IdEncomenda, comunicacaoBD);
               livros = new Livro[listaLivros.Count];

               string[] estado = servico.getUltimoEstado(encomenda.IdEncomenda, comunicacaoBD);

               if (nomeEstado != "todas")
               {
                   if (estado[1].Trim() != nomeEstado)
                   {
                       aux.Add(encomenda);
                       continue;
                   }
               }
                
               foreach (Livro livro in listaLivros)
               {
                   livros[iLivro++] = livro;

                   valorTotal += double.Parse(livro.PrecoVenda) * livro.Quantidade;
               }

               encomenda.ValorTotal = valorTotal;
               encomenda.ListaLivros = livros;
               encomenda.Estado = estado[1];                               

               iLivro = 0;
               valorTotal = 0.0;
           }
            
           foreach (Encomenda enc in aux)
           {
               listaEncomendas.Remove(enc);
           }            
       }
       catch (SqlException es)
       {
           comunicacaoBD.Transacao.Rollback();
           throw new Exception("ERRO: Nao é possivel Cancelar encomenda");
       }
       comunicacaoBD.Transacao.Commit();
       return listaEncomendas;
   }

   

     [WebMethod]
   public Encomenda[] MostraListaEncomendas2()
   {
       ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

       ArrayList listaEncomendas = new ArrayList();
       ArrayList listaLivros = null;

       Encomenda[] encomendas;
       Livro[] livros;

       int iLivro = 0;
       int jEnc = 0;
       double valorTotal = 0.0;

       try
       {
           listaEncomendas = servico.getListaEncomendas(comunicacaoBD);

           encomendas = new Encomenda[listaEncomendas.Count];

           foreach (Encomenda encomenda in listaEncomendas)
           {
               encomendas[jEnc] = encomenda;
               listaLivros = servico.getLivrosEncomenda(encomenda.IdEncomenda, comunicacaoBD);
               livros = new Livro[listaLivros.Count];

               string[] estado = servico.getUltimoEstado(encomenda.IdEncomenda, comunicacaoBD);

               foreach (Livro livro in listaLivros)
               {
                   livros[iLivro++] = livro;

                   valorTotal += double.Parse(livro.PrecoVenda) * livro.Quantidade;
               }
                
               encomendas[jEnc].ValorTotal = valorTotal;
               encomendas[jEnc].ListaLivros = livros;
               encomendas[jEnc].Estado = estado[1];
                
               iLivro = 0;
               valorTotal = 0.0;
               ++jEnc;
           }

       }
       catch (SqlException es)
       {
           comunicacaoBD.Transacao.Rollback();
           throw new Exception("ERRO: Nao é possivel Cancelar encomenda");
       }
       comunicacaoBD.Transacao.Commit();
       return encomendas;
   }
      
   //[WebMethod]

   public Cliente DadosCliente(string username)

   {

       Cliente cliente = null;

       //servico.iniciaTransacao();

       try{



           cliente = servico.getDadosCliente(username);



           if (cliente == null) {

               // servico.commitTransacao();

               throw new UtilizadorInexistente();

           }

       }

       catch (SqlException e)

       {

           //servico.rollbackTransacao();

           return null;

       }

       // servico.commitTransacao();

        

       return cliente;

   }



  // [WebMethod]

   public Cliente LoginCliente(string username, string password)

   {

       Cliente cliente = null;

       servico.iniciaTransacao();

       try{

           cliente = servico.getDadosCliente(username);



           if (cliente != null)

           {

               if (cliente.Password.Trim() != password)

               {

                   servico.commitTransacao();

                   throw new LoginInvalido();

               }

               cliente.Password = "************";

           }

           else {

               servico.commitTransacao();

               throw new LoginInvalido();

           }

       }

       catch (SqlException e)

       {

           servico.rollbackTransacao();

           return null;

       }

       servico.commitTransacao();

        

       return cliente;

   }



   //LIVRO

     

   //[WebMethod]

   public void insereLista()

   {

       Livro livro = new Livro();

       Livro livro1 = new Livro();



       livro.ISBN = "8574311375";

       livro.Titulo = "Medicina Legal: Aplicada ao Direito";

       livro.Categoria = "Categoria Desconhecida";

       livro.Autores = "Francisco Silveira Benfica\n";

       livro.Editora = "UNISINOS";

       livro.AnoEdicao = "2003";

       livro.PrecoVenda = "$0.0";

       livro.PrecoSemDesconto = "Preço Indisponível";

       livro.UrlImagem = "asfddsfasdf";

       livro.TempoEntrega = "n.a";

       livro.Desconto = 0.0F;

       livro.Top5 = 0.0F;

       livro.EstadoConservacao = "novo";

       livro.NomeFornecedor = "amazon";



       livro1.ISBN = "8574311375";

       livro1.Titulo = "Medicina Legal: Aplicada ao Direito";

       livro1.Categoria = "Categoria Desconhecida";

       livro1.Autores = "Francisco Silveira Benfica\n";

       livro1.Editora = "UNISINOS";

       livro1.AnoEdicao = "2003";

       livro1.PrecoVenda = "$0.0";

       livro1.PrecoSemDesconto = "Preço Indisponível";

       livro1.UrlImagem = "asfddsfasdf";

       livro1.TempoEntrega = "n.a";

       livro1.Desconto = 0.0F;

       livro1.Top5 = 0.0F;

       livro1.EstadoConservacao = "novo";

       livro1.NomeFornecedor = "amazon";



       InsereListaLivros(new Livro[2] { livro, livro1 });       

   }  



    



   //[WebMethod]

   public Livro DadosLivro(string ISBN, string nomeFornecedor)

   {

      // servico.iniciaTransacao();

       Livro livro = null;

       try{



           livro = servico.getLivro(ISBN, nomeFornecedor);



           if (livro == null) {

               //servico.commitTransacao();

               throw new LivroInexistente();

           }

       }

       catch (SqlException e)

       {

           //servico.rollbackTransacao();

       }



       //servico.commitTransacao();

       return livro;

   }    



    





   //GESTOR



   [WebMethod]

   public void InsereGestor(string nome, string username, string password)

   {

       servico.iniciaTransacao();

       int i = 0;

       try{

           Utilizador utilizador = servico.getDadosUtilizador(username);



           if (utilizador == null)

           {

            

               string idUtilizador = servico.InsereUtilizador(nome, username, password);



               i = servico.InsereGestor(idUtilizador);

           

           }

           else

           {

               servico.rollbackTransacao();

               throw new UsernameExistenteException();

           }

       }

       catch (SqlException e)

       {

           servico.rollbackTransacao();

           throw new UtilizadorNaoGuardado();

       }

       servico.commitTransacao();

   

   }



   //[WebMethod]

   public Gestor LoginGestor(string username, string password)

   {



       Gestor gestor = servico.getDadosGestor(username);



       if (gestor != null)

       {

           if (gestor.Password.Trim() != password)

           {

               throw new LoginInvalido();

           }

           gestor.Password = "************";

       }

       else

       {

           throw new LoginInvalido();

       }



       return gestor;

   }



    





       // METODOS AUXILIARES



       private Utilizador ClienteToUtilizador(Cliente cliente) {



           Utilizador utilizador = new Utilizador();



           utilizador.IdUtilizador = cliente.IdCliente;

           utilizador.Nome = cliente.Nome;

           utilizador.Username = cliente.Username;

           utilizador.TipoUtilizador = "cliente";



           return utilizador;

    

       }



       /// <summary>

       /// guarda o livro recebido na base de dados

       /// </summary>

       /// <param name="livro"></param>

       /// <returns>numero de linhas criadas</returns>

       //[WebMethod]

       public int InsereLivro(Livro livro)

       {

           if (livro.Autores.Length > 451)

           {

               livro.Autores = livro.Autores.Remove(450);

           }



           if (livro.Categoria.Length > 451)

           {

               livro.Categoria = livro.Categoria.Remove(450);

           }



           ////////////    TRANSACAO    //////////////

           int i = servico.guardaLivro(livro.ISBN, livro.Titulo, livro.Categoria, livro.Autores, livro.Editora, livro.AnoEdicao,

                                       livro.PrecoVenda, livro.PrecoSemDesconto, livro.UrlImagem, livro.TempoEntrega,

                                       livro.Desconto + "", livro.Top5 + "", livro.EstadoConservacao, livro.NomeFornecedor);

           ////////////    TRANSACAO    //////////////

           if (i == 0)

           {

               throw new ErroGuardarLivro("Erro: a guardar o livro: " + livro.Titulo);

           }



           return i;

       }



       //ENCOMENDA



       /// <summary>

       /// Cria uma encomenda com os livros do carrinho de compras associada ao cliente

       /// </summary>

       /// <param name="username">username do Cliente</param>

       /// <returns>numero de linhas criadas</returns> 

       //[WebMethod]

       public int CriaEncomenda(string username)

       {

           Cliente cliente = servico.getDadosCliente(username);

           ArrayList listalivros = servico.getLivrosDoCarrinho(username);



           ////////////    TRANSACAO    //////////////

           string idEncomenda = servico.CriaEncomenda(cliente.IdCliente, DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year);



           int i = 0;

           foreach (Livro livro in listalivros)

           {

    

               i += servico.insereLinhaEncomenda(idEncomenda, livro.IdLivro, livro.Quantidade, livro.PrecoVenda + "",

                   DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year, livro.TempoEntrega);

 

           }

           ////////////    TRANSACAO    //////////////

     

           return i;

       }



       /*

         [WebMethod]

       public int CriaCarrinhoCompras(string idCliente, string idLivro, int quantidade)

       {

           return servico.insereLivroCarrinhoCompras(null, idCliente, idLivro, quantidade);

       } 

          

   [WebMethod]

   public void TestaInsereListaLivros()

   {

       Livro livro1 = new Livro();

       livro1.Titulo = "Investing For Dummies, 4th Edition";

       livro1.ISBN = "0764599127";

       livro1.NomeFornecedor = "For Dummies";

       livro1.NumPesquisas = 1;

       livro1.Categoria = "teste";

       livro1.Autores = "teste";

       livro1.Editora = "teste";

       livro1.AnoEdicao = "2222";

       livro1.PrecoVenda = "$0.0";

       livro1.PrecoSemDesconto = "indisponivel";

       livro1.UrlImagem = "teste";

       livro1.TempoEntrega = "teste";

       livro1.Desconto = 0.0F;

       livro1.Top5 = 0.0F;

       livro1.EstadoConservacao = "teste";





       Livro livro2 = new Livro();

       livro2.ISBN = "458485393482";

       livro2.Titulo = "para apagar depressa";

       livro2.NomeFornecedor = "anamzon";

       livro2.NumPesquisas = 1;

       livro2.Categoria = "teste";

       livro2.Autores = "teste";

       livro2.Editora = "teste";

       livro2.AnoEdicao = "2222";

       livro2.PrecoVenda = "$0.0";

       livro2.PrecoSemDesconto = "indisponivel";

       livro2.UrlImagem = "teste";

       livro2.TempoEntrega = "teste";

       livro2.Desconto = 0.0F;

       livro2.Top5 = 0.0F;

       livro2.EstadoConservacao = "teste";





       Livro livro3 = new Livro();

       livro3.ISBN = "788485393434";

       livro3.Titulo = "se isto funciona muito fixew";

       livro3.NomeFornecedor = "microsoft";

       livro3.NumPesquisas = 1;

       livro3.PrecoSemDesconto = "$14.29";

       livro3.Categoria = "teste";

       livro3.Autores = "teste";

       livro3.Editora = "teste";

       livro3.AnoEdicao = "2222";

       livro3.PrecoVenda = "$0.0";

       livro3.UrlImagem = "teste";

       livro3.TempoEntrega = "teste";

       livro3.Desconto = 0.0F;

       livro3.Top5 = 0.0F;

       livro3.EstadoConservacao = "teste";





       Livro livro4 = new Livro();

       livro4.Titulo = "Investing For Dummies, 4th Edition";

       livro4.ISBN = "0764599127";

       livro4.NomeFornecedor = "For Dummies";

       livro4.NumPesquisas = 10;

       livro4.Categoria = "teste";

       livro4.Autores = "teste";

       livro4.Editora = "teste";

       livro4.AnoEdicao = "2222";

       livro4.PrecoVenda = "$0.0";

       livro4.PrecoSemDesconto = "indisponivel";

       livro4.UrlImagem = "teste";

       livro4.TempoEntrega = "teste";

       livro4.Desconto = 0.0F;

       livro4.Top5 = 0.0F;

       livro4.EstadoConservacao = "teste";



       Livro[] lista = new Livro[4]{livro1, livro2, livro3, livro4};



       InsereListaLivros(lista);

   }

     */



}

