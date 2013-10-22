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
/// Summary description for Encomenda
/// </summary>
[System.Xml.Serialization.XmlInclude(typeof(Encomenda))]
public class Encomenda
{
    private string _idEncomenda;
    private string _idCliente;
    private double _valorTotal;
    private string _dataCriacao;
    private string _estado;
    private Livro[] _listaLivros;
    private Cliente _cliente;
    private EstadoEncomenda[] _historicoEstado;


    public Encomenda()
    {
    }

    public string IdEncomenda
    {
        get
        {

            return _idEncomenda;

        }

        set
        {

            _idEncomenda = value;

        }

    }

    public string IdCliente
    {
        get
        {

            return _idCliente;

        }

        set
        {

            _idCliente = value;

        }

    }

    public double ValorTotal{
        
        get {
            return _valorTotal;
        }

        set {
            _valorTotal = value;
        }
    }

    public string DataCriacao
    {
        get
        {

            return _dataCriacao;

        }

        set
        {

            _dataCriacao = value;

        }

    }

    public string Estado
    {
        get
        {

            return _estado;

        }

        set
        {

            _estado = value;

        }

    }

    public Livro[] ListaLivros
    {
        get
        {

            return _listaLivros;

        }

        set
        {

            _listaLivros = value;

        }

    }

    public Cliente Cliente {

        get {
            return _cliente;
        }

        set {
            _cliente = value;
        }
    
    }

    public EstadoEncomenda[] HistoricoEstado
    {

        get
        {
            return _historicoEstado;
        }

        set
        {
            _historicoEstado = value;
        }

    }
}

[System.Xml.Serialization.XmlInclude(typeof(EstadoEncomenda))]
public class EstadoEncomenda
{
    private string _idEncomenda;
    private string _dataAlteracao;
    private string _nomeEstado;
    private int _contador;


    public EstadoEncomenda()
    {
    }

    public string IdEncomenda
    {
        get
        {

            return _idEncomenda;

        }

        set
        {

            _idEncomenda = value;

        }

    }

    public string DataAlteracao
    {
        get
        {

            return _dataAlteracao;

        }

        set
        {

            _dataAlteracao = value;

        }

    }

    public string NomeEstado
    {
        get
        {

            return _nomeEstado;

        }

        set
        {

            _nomeEstado = value;

        }

    }

    public int Contador {

        get {
            return _contador;
        }

        set {
            _contador = value;
        }
    
    }

}

[System.Xml.Serialization.XmlInclude(typeof(DadosGestao))]
public class DadosGestao {

    private int _numComprados;
    private int _numPesquisados;
    private int _numComDesconto;
    private int _numSemDesconto;

    public int NumComprados {
        get {
            return _numComprados;
        }

        set {
            _numComprados = value;
        }
    }

    public int NumPesquisados
    {
        get
        {
            return _numPesquisados;
        }

        set
        {
            _numPesquisados = value;
        }
    }

    public int NumComDesconto
    {
        get
        {
            return _numComDesconto;
        }

        set
        {
            _numComDesconto = value;
        }
    }

    public int NumSemDesconto
    {
        get
        {
            return _numSemDesconto;
        }

        set
        {
            _numSemDesconto = value;
        }
    }
}