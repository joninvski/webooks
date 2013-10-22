using System;
using System.Data;
using System.Configuration;
using System.Web;

public class CodigoInterno
{
	public CodigoInterno()
	{        
	}
    
    public string getGUID()
    {
        return System.Guid.NewGuid().ToString();
    }   

}

