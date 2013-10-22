using System;
using System.Data;
using System.Configuration;
using System.Web;

public class Encomenda
{
    private string _idEncomenda;
    private double _valorTotal;
    private string _nomeCliente;
    private string _numero;
    private string _address;
    private string _city;
    private string _state;
    private string _ZIPcode;
    private string _country;
    private string _estado;
    private Livro[] _listaLivros;

    private int _tempoEspera;

    public Encomenda(EncomendaWEBooks encomendaWEBooks)
    {
        IdEncomenda = encomendaWEBooks.IdEncomenda;

        _listaLivros = new Livro[encomendaWEBooks.ListaBook.Length];
        int i = 0;
        foreach (Book book in encomendaWEBooks.ListaBook) {
            _listaLivros[i] = new Livro();

            _listaLivros[i].ISBN = book.ISBN;
            _listaLivros[i].NomeFornecedor = book.Fornecedor;
            _listaLivros[i].PrecoVenda = book.PrecoVenda + "";
            _listaLivros[i].Quantidade = book.Quantidade;
            _listaLivros[i].TempoEntrega = book.Disponibilidade;
            _listaLivros[i].Titulo = book.Titulo;

            i++;
        }

        //Encher os campos que nao veem da fila

        ValorTotal = 0.0;
        NomeCliente = "indisponivel";

        Numero = "indisponivel";
        Address = "indisponivel";
        City = "indisponivel";
        State = "indisponivel";
        ZIPcode = "indisponivel";
        Country = "indisponivel";
        Estado = "indisponivel";

        TempoEspera = 0;


    }

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

    public double ValorTotal{
        
        get {
            return _valorTotal;
        }

        set {
            _valorTotal = value;
        }
    }

    public string NomeCliente
    {
        get
        {
            return _nomeCliente;
        }

        set
        {
            _nomeCliente = value;
        }
    }

     public string Numero
    {
        get
        {
            return _numero;
        }

        set
        {
            _numero = value;
        }
    }

    public string Address
    {

        get
        {

            return _address;

        }

        set
        {

            _address = value;

        }

    }

    public string City
    {

        get
        {

            return _city;

        }

        set
        {

            _city = value;

        }

    }

    public string State
    {

        get
        {

            return _state;

        }

        set
        {

            _state = value;

        }

    }

    public string ZIPcode
    {

        get
        {

            return _ZIPcode;

        }

        set
        {

            _ZIPcode = value;

        }

    }

    public string Country
    {

        get
        {

            return _country;

        }

        set
        {

            _country = value;

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

    public int TempoEspera
    {
        get
        {

            return _tempoEspera;

        }

        set
        {

            _tempoEspera = value;

        }

    }

}  

