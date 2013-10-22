using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Utilizador
/// </summary>

[System.Xml.Serialization.XmlInclude(typeof(Utilizador))]
public class Utilizador
{
    private string _idUtilizador;
    private string _nome;
    private string _username;
    private string _password;
    private string _tipoUtilizador;

    public Utilizador()

    {

    }

    public Utilizador(Cliente cliente)

    {

        _nome = cliente._nome;

        _username = cliente.Username;

        _tipoUtilizador = cliente.GetType().ToString();

    }

    public Utilizador(Gestor gestor)

    {

        _nome = gestor._nome;

        _username = gestor.Username;

        _tipoUtilizador = gestor.GetType().ToString();

    }

    public string IdUtilizador

    {

        get

        {

            return _idUtilizador;

        }

        set

        {

            _idUtilizador = value;

        }

    }

    public string Nome

    {

        get

        {

            return _nome;

        }

        set

        {

            _nome = value;

        }

    }

    public string Username

    {

        get

        {

            return _username;

        }

        set

        {

            _username = value;

        }

    }

    public string Password

    {

        get

        {

            return _password;

        }

        set

        {

            _password = value;

        }

    }

    public string TipoUtilizador

    {

        get

        {

            return _tipoUtilizador;

        }

        set

        {

            _tipoUtilizador = value;

        }

    } 

}





/// <summary>

/// Summary description for Cliente

/// </summary>

 [System.Xml.Serialization.XmlInclude(typeof(Cliente))]
public class Cliente : Utilizador
{
     private string _idCliente;  
     private string _telefone;
     private string _numero;
     private string _address;
     private string _city;
     private string _state;
     private string _ZIPcode;
     private string _country;
     private string _latitude;
     private string _longitude;     
     private string _numCartaoCredito;
     private string _dataValidadeCartaoValidade;
     private string _estado;

     public Cliente()

    {

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

     public string Telefone

    {

        get

        {

            return _telefone;

        }

        set

        {

            _telefone = value;

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

    public string Latitude
    {
        get
        {
            return _latitude;
        }

        set
        {
            _latitude = value;
        }
    }

    public string Longitude
    {
        get
        {
            return _longitude;
        }

        set
        {
            _longitude = value;
        }
    }

     public string NumCartaoCredito

    {

        get

        {

            return _numCartaoCredito;

        }

        set

        {

            _numCartaoCredito = value;

        }

    }

     public string DataValidadeCartaoValidade

    {

        get

        {

            return _dataValidadeCartaoValidade;

        }

        set

        {

            _dataValidadeCartaoValidade = value;

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

}





/// <summary>

/// Summary description for Gestor

/// </summary>

[System.Xml.Serialization.XmlInclude(typeof(Gestor))]

public class Gestor : Utilizador

{

    private string _idGestor;

   

    public Gestor()

    {

    }



    public string IdGestor

    {

        get

        {

            return _idGestor;

        }

        set

        {

            _idGestor = value;

        }

    }

   

}



    