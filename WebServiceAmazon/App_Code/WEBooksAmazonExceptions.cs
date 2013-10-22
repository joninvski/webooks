using System;

using System.Data;

using System.Configuration;

using System.Runtime.Remoting;

using System.Runtime.Serialization;

using System.Web;

using System.Web.Security;

using System.Web.UI;

using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;

using System.Web.UI.HtmlControls;



/// <summary>

/// Summary description for AmazonExceptions

/// </summary>

[Serializable]

public class EfectuaPedidoException : RemotingException, ISerializable

{

    #region ISerializable Members



    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)

    {

        throw new Exception("The method or operation is not implemented.");

    }



    #endregion

    

    private string _mensagem;

    public override string Message {

        get {

            return _mensagem;

        }

    }



    public EfectuaPedidoException()

    {

        _mensagem = "Pesquisa efectuada não retornou resultados.";

    }



	public EfectuaPedidoException(string msg)

	{
        _mensagem = "Pesquisa efectuada não retornou resultados." + msg;

	}







   public EfectuaPedidoException(SerializationInfo info, StreamingContext context) {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context) {

        info.AddValue("_mensagem", _mensagem);

    }

   

}



[Serializable]

public class ValidaParametrosException : RemotingException, ISerializable {

    #region ISerializable Members

    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {

        throw new Exception("The method or operation is not implemented.");

    }

    #endregion



    private string _mensagem;

    public override string Message {

        get { return _mensagem; }

    }



    public ValidaParametrosException() {

        _mensagem = "Parametros de pesquisa inválidos.";

    }



    public ValidaParametrosException(SerializationInfo info, StreamingContext context) {

        _mensagem = (string)info.GetValue("_mensagem", typeof(string));

    }



    public override void GetObjectData(SerializationInfo info, StreamingContext context) {

        info.AddValue("_mensagem", _mensagem);

    }

}