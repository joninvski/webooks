using System;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Threading;

public class WEBooksDistribuidorService
{
    EncomendaWEBooks _encomendaWEBooks;

    Servicos servico = new Servicos();

    public WEBooksDistribuidorService()
    {       
    }    

   // LIVRO
    public void InsereListaLivros(Livro[] listaLivros, string idEncomenda)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        try{
            foreach (Livro livro in listaLivros) {

                int i = servico.guardaLivro(idEncomenda, livro.ISBN, livro.Titulo, livro.PrecoVenda.Replace(".", ","), 
                                            livro.TempoEntrega, livro.NomeFornecedor, livro.Quantidade, comunicacaoBD);
            } 
        }catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("Erro a guardar lista de livros: Nao é possivel realizar esta encomenda " + idEncomenda);
        }

        comunicacaoBD.Transacao.Commit();
    }

    //ENCOMENDA
    
    public void InsereEncomenda(ref Encomenda encomenda)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        int tempoEspera = 0;
        try
        {
            servico.insereEncomenda(encomenda.IdEncomenda, encomenda.ValorTotal + "", "colocada", encomenda.NomeCliente,
                                        encomenda.Numero, encomenda.Address, encomenda.City, encomenda.State,
                                        encomenda.ZIPcode, encomenda.Country, 0, comunicacaoBD);
            encomenda.TempoEspera = 0;

            foreach (Livro livro in encomenda.ListaLivros) { 

                tempoEspera = CalculaTempoEntrega(livro.TempoEntrega);

                if (encomenda.TempoEspera < tempoEspera) {
                    encomenda.TempoEspera = tempoEspera;
                }

                servico.guardaLivro(encomenda.IdEncomenda, livro.ISBN, livro.Titulo, livro.PrecoVenda.Replace(".", ","),
                                        livro.TempoEntrega, livro.NomeFornecedor, livro.Quantidade, comunicacaoBD); 
            }
        }
        catch (SqlException e)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("ERRO: encomenda nao pode ser atendida");
        }

        comunicacaoBD.Transacao.Commit();
    }

    public void CancelaEncomenda(object aux)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        WEBooksDistribuidorAp.LogMensagens text = new WEBooksDistribuidorAp.LogMensagens();

        ComunicacaoFila comFila = new ComunicacaoFila();

        string idEncomenda = EncomendaWEBooks.IdEncomenda;
        try
        {
            string ultimoEstado = servico.getEstado(idEncomenda, comunicacaoBD);

            if (ultimoEstado == "colocada" || ultimoEstado == "pendente")
            {                
                servico.alteraEstado(idEncomenda, "cancelada", comunicacaoBD);

                //Espera 5 seg antes de responder a WEBooks
                Thread.Sleep(new TimeSpan(0, 0, 5));
                EncomendaWEBooks.TipoMensagem = "cancelada";
                comFila.escreveFilaWeb(EncomendaWEBooks);

                text.actualizaLog(EncomendaWEBooks.TipoMensagem, false);
            }
            else
            {
                comunicacaoBD.Transacao.Rollback();
                throw new Exception("Erro: estado da encomenda nao permite que seja cancelada");
            }
        }
        catch (SqlException es) {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("ERRO: Nao é possivel Cancelar encomenda");            
        }
        comunicacaoBD.Transacao.Commit();
    }
    
    public void PendenteEncomenda(string idEncomenda)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            string ultimoEstado = servico.getEstado(idEncomenda, comunicacaoBD);

            if (ultimoEstado == "colocada")
            {
                servico.alteraEstado(idEncomenda, "pendente", comunicacaoBD);
            }
            else
            {
                comunicacaoBD.Transacao.Rollback();
                throw new Exception("Erro: estado da encomenda nao permite que passe para pendente");
            }
        }
        catch (SqlException es)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("ERRO: Nao é possivel colocar encomenda como pendente");
        }
        comunicacaoBD.Transacao.Commit();
    }
    
    public void EntregueEncomenda(string idEncomenda)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        try
        {
            string ultimoEstado = servico.getEstado(idEncomenda, comunicacaoBD);

            if (ultimoEstado == "colocada" || ultimoEstado == "pendente")
            {
                servico.alteraEstado(idEncomenda, "entregue", comunicacaoBD);
            }
            else
            {
                comunicacaoBD.Transacao.Rollback();
                throw new Exception("Erro: estado da encomenda nao permite que passe para entregue");
            }
        }
        catch (SqlException es)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("ERRO: Nao é possivel colocar a encomenda como entregue");
        }
        comunicacaoBD.Transacao.Commit();
    }
    
    public void ReduzTempoEspera(int tempo)
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();
        
        ArrayList listaEncomendas = this.MostraEncomendas();

        foreach (Encomenda encomenda in listaEncomendas)
        {
            encomenda.TempoEspera = encomenda.TempoEspera - tempo;
            if (encomenda.TempoEspera <= 0)
            {                
                this.EntregueEncomenda(encomenda.IdEncomenda);
                servico.alteraTempoEspera(encomenda.IdEncomenda, 0, comunicacaoBD);            
            }
            else {
                servico.alteraTempoEspera(encomenda.IdEncomenda, encomenda.TempoEspera, comunicacaoBD);
            }
        }

    }

    public void TempoVidaEncomenda(object aux)
    {
        WEBooksDistribuidorAp.LogMensagens text = new WEBooksDistribuidorAp.LogMensagens();

        Encomenda encomenda = new Encomenda(EncomendaWEBooks);

        ComunicacaoFila comFila = new ComunicacaoFila();

        InsereEncomenda(ref encomenda); //fica logo no estado colocada

        //Envia mensagem de aviso de mensagem colocada, na WEBooks fica pendente

        if (encomenda.TempoEspera != 0) {
            PendenteEncomenda(encomenda.IdEncomenda);

            //Espera 5 segundos antes de enviar a resposta a WEBooks
            EncomendaWEBooks.TipoMensagem = "pendente";
            Thread.Sleep(new TimeSpan(0,0,5));
            comFila.escreveFilaWeb(EncomendaWEBooks);

            text.escreveNoLog =  "Enviada: " + EncomendaWEBooks.TipoMensagem;            
        }

        Thread.Sleep(new TimeSpan(0, 0, encomenda.TempoEspera));//vai esperar que fiquem todos os livros disponiveis
        
        try
        {
            EncomendaWEBooks.TipoMensagem = "entregue";
            Thread.Sleep(new TimeSpan(0, 0, 5));
            comFila.escreveFilaWeb(EncomendaWEBooks);

            EntregueEncomenda(encomenda.IdEncomenda);
            text.actualizaLog(EncomendaWEBooks.TipoMensagem, false);            

        }
        catch (Exception) { 
            //nao faz nada, significa que mensagem foi cancelada anteriormente
        }
    
        //fim do tempo de vida da encomenda
    }

    // Auxiliares
    public ArrayList MostraEncomendas()
    {
        ComunicacaoBD comunicacaoBD = new ComunicacaoBD();

        ArrayList listaEncomendas = new ArrayList();

        ArrayList aux = new ArrayList();

        try
        {
            listaEncomendas = servico.getListaEncomendas(comunicacaoBD);

            foreach (Encomenda encomenda in listaEncomendas)
            {
                if (encomenda.Estado == "colocada" || encomenda.Estado == "pendente") {
                    aux.Add(encomenda);
                }
            }
        }
        catch (SqlException es)
        {
            comunicacaoBD.Transacao.Rollback();
            throw new Exception("ERRO: Nao é possivel lista encomendas");
        }
        comunicacaoBD.Transacao.Commit();
        return aux;
    }
    
    public int CalculaTempoEntrega(string tempoEntrega) {
        switch (tempoEntrega) { 
            case "Usually ships in 24 hours":
                return 0;
            case "Usually ships in 2-3 days":
                return 30;
            case "Usually ships in 5 to 7 days":
                return 70;
            case "Usually ships in 6 to 12 days":
                return 120;
            default :
                return 160;
        
        }
    
    }

    public EncomendaWEBooks EncomendaWEBooks {

        get {

            return _encomendaWEBooks;
        }

        set {
            _encomendaWEBooks = value;
        }
    
    }
}

