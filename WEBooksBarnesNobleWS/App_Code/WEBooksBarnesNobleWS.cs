using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://webooks.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WEBooksBarnesNobleWS : System.Web.Services.WebService
{
    public WEBooksBarnesNobleWS () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public Item[] ProcuraItems(Item[] itemsAmazon) {

        WEBooksBarnesNoble BarnesNoble = new WEBooksBarnesNoble();

        Item[] itemBarnesNoble = BarnesNoble.EfectuaPedido(itemsAmazon);

        return itemBarnesNoble; 
    } // ProcuraItem()


    [WebMethod]
    public Item ProcuraItem(string isbn){

        WEBooksBarnesNoble BarnesNoble = new WEBooksBarnesNoble();

        Item item = BarnesNoble.EfectuaPedido(isbn);

        return item;
    } // ProcuraItem()
}
