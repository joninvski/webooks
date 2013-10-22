using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using us.geocoder.rpc;

[WebService(Namespace = "http://WEBooksGeocoder.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WEBooksGeoCoder : System.Web.Services.WebService
{
    public WEBooksGeoCoder () {
    }

    [WebMethod]
    public CoordGeo GeoCode(string numero, string zip, string rua, string estado, string cidade)
    {
        string morada = null;
        if (zip == "" || zip == null)
        {
            morada = numero + " " + rua + ", " + cidade + " " + estado;
        }
        else {
            morada = numero + " " + rua + ", " + cidade + " " + zip;
        }

        GeoCode_Service geoCoder = new GeoCode_Service();
        GeocoderAddressResult[] resultado = geoCoder.geocode_address(morada);

        if (resultado[0] == null) {
            throw new Exception("Impossivel descobrir as coordenadas geograficas da morada fornecida");
        }

        CoordGeo ret = new CoordGeo();
        ret.Latitude = resultado[0].lat;
        ret.Longitude = resultado[0].@long;

        return ret;
    }
}

[System.Xml.Serialization.XmlInclude(typeof(CoordGeo))]
public class CoordGeo
{
    float _latitude;
    float _longitude;

    public float Latitude{
        get { return _latitude; }
        set { _latitude = value; }
    }

    public float Longitude
    {
        get { return _longitude; }
        set { _longitude = value; }
    }
}