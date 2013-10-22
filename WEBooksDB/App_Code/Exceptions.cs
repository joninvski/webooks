using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

[Serializable]

public class UsernameExistenteException : SoapException, ISerializable
{
    private string _mensagem;

    public UsernameExistenteException()
    {
        _mensagem = String.Empty;
    }

    public UsernameExistenteException(string mensagem)
    {
        _mensagem = mensagem;
    }

    public UsernameExistenteException(SerializationInfo info, StreamingContext context)
    {
        _mensagem = (string)info.GetValue("_mensagem", typeof(string));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("_mensagem", _mensagem);
    } // Returns the exception information.  

    public override string Message
    {
        get
        {
            return _mensagem;
        }
    }
}



/*

[Serializable]

public class NumeroPedidosExcedidoException : RemotingException, ISerializable

{

    private string _mensagem;

    

    public NumeroPedidosExcedidoException(){



        _mensagem = String.Empty; 

    }



    public NumeroPedidosExcedidoException(string mensagem)

    {



        _mensagem = mensagem; 

    }



    public NumeroPedidosExcedidoException(SerializationInfo info, StreamingContext context)

    {



        _mensagem = (string)info.GetValue("_mensagem", typeof(string)); 

    } 

    

    public override void GetObjectData(SerializationInfo info, StreamingContext context){

        info.AddValue("_mensagem", _mensagem); 

    } // Returns the exception information.  

    

    public override string Message{ 

        get {

            return _mensagem; 

        } 

    } 

}

*/



[Serializable]

public class LivroExistenteNoCarrinhoException : RemotingException, ISerializable

{

    private string _mensagem;



    public LivroExistenteNoCarrinhoException()

    {



        _mensagem = "Erro na insercao do livro no carrinho, esse livro ja se encontra no carrinho";

    }



    public LivroExistenteNoCarrinhoException(string mensagem)

    {



        _mensagem = mensagem;

    }



    public LivroExistenteNoCarrinhoException(SerializationInfo info, StreamingContext context)

    {



        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class LivroNaoExistenteNoCarrinhoException : RemotingException, ISerializable

{

    private string _mensagem;



    public LivroNaoExistenteNoCarrinhoException()

    {



        _mensagem = "O Livro nao existe no carrinho de Compras";

    }



    public LivroNaoExistenteNoCarrinhoException(string mensagem)

    {



        _mensagem = mensagem;

    }



    public LivroNaoExistenteNoCarrinhoException(SerializationInfo info, StreamingContext context)

    {



        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class LoginInvalido : RemotingException, ISerializable

{

    private string _mensagem;



    public LoginInvalido()

    {

        _mensagem = "Username ou Password incorrectos";

    }



    public LoginInvalido(string mensagem)

    {

        _mensagem = mensagem;

    }



    public LoginInvalido(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class UtilizadorInexistente : RemotingException, ISerializable

{

    private string _mensagem;



    public UtilizadorInexistente()

    {

        _mensagem = "Username nao existente";

    }



    public UtilizadorInexistente(string mensagem)

    {

        _mensagem = mensagem;

    }



    public UtilizadorInexistente(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class ClienteInexistente : RemotingException, ISerializable

{

    private string _mensagem;



    public ClienteInexistente()

    {

        _mensagem = "Username nao existente";

    }



    public ClienteInexistente(string mensagem)

    {

        _mensagem = mensagem;

    }



    public ClienteInexistente(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class LivroInexistente : RemotingException, ISerializable

{

    private string _mensagem;



    public LivroInexistente()

    {

        _mensagem = "O Livro nao existe na base de dados";

    }



    public LivroInexistente(string mensagem)

    {

        _mensagem = mensagem;

    }



    public LivroInexistente(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class ErroGuardarLivro : RemotingException, ISerializable

{

    private string _mensagem;



    public ErroGuardarLivro()

    {

        _mensagem = "Livro nao guardado na base de dados";

    }



    public ErroGuardarLivro(string mensagem)

    {

        _mensagem = mensagem;

    }



    public ErroGuardarLivro(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class DataValidadeException : RemotingException, ISerializable

{

    private string _mensagem;



    public DataValidadeException()

    {

        _mensagem = "A data do cartao e inferior a data corrente";

    }



    public DataValidadeException(string mensagem)

    {

        _mensagem = mensagem;

    }



    public DataValidadeException(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class CarrinhoVazioException : RemotingException, ISerializable

{

    private string _mensagem;



    public CarrinhoVazioException()

    {

        _mensagem = "O Carrinho nao contem livros";

    }



    public CarrinhoVazioException(string mensagem)

    {

        _mensagem = mensagem;

    }



    public CarrinhoVazioException(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class QuantidadeInvalidaExceptions : RemotingException, ISerializable

{

    private string _mensagem;



    public QuantidadeInvalidaExceptions()

    {

        _mensagem = "Quantidade tem de ser superior a 0";

    }



    public QuantidadeInvalidaExceptions(string mensagem)

    {

        _mensagem = mensagem;

    }



    public QuantidadeInvalidaExceptions(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class UtilizadorNaoGuardado : RemotingException, ISerializable

{

    private string _mensagem;



    public UtilizadorNaoGuardado()

    {

        _mensagem = "Utilizador nao inserido na base de dados";

    }



    public UtilizadorNaoGuardado(string mensagem)

    {

        _mensagem = mensagem;

    }



    public UtilizadorNaoGuardado(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class CarrinhoNaoLimpo : RemotingException, ISerializable

{

    private string _mensagem;



    public CarrinhoNaoLimpo()

    {

        _mensagem = "Nao e possivel limpar o carrinho";

    }



    public CarrinhoNaoLimpo(string mensagem)

    {

        _mensagem = mensagem;

    }



    public CarrinhoNaoLimpo(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}



[Serializable]

public class ErroPassword : RemotingException, ISerializable

{

    private string _mensagem;



    public ErroPassword()

    {

        _mensagem = "A password e a password de confirmacao sao diferentes";

    }



    public ErroPassword(string mensagem)

    {

        _mensagem = mensagem;

    }



    public ErroPassword(SerializationInfo info, StreamingContext context)

    {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context)

    {

        info.AddValue("_mensagem", _mensagem);

    } // Returns the exception information.  



    public override string Message

    {

        get

        {

            return _mensagem;

        }

    }

}

