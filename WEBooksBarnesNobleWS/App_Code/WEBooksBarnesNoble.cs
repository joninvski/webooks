using System;
using System.Data;
using System.Collections;
using System.Configuration;
using com.abundanttech.www;

/// <summary>
/// Summary description for WEBooksBarnesNoble
/// </summary>
public class WEBooksBarnesNoble
{
    private BNPrice BarnesNoble;

	public WEBooksBarnesNoble()
	{
        BarnesNoble = new BNPrice();
	}

    public Item[] EfectuaPedido(Item[] items) {
        
        foreach (Item item in items) {
            string preco = BarnesNoble.GetBNQuote(item.Isbn);

            if (preco.Equals("Price Not Available")) // livro nao encontrado
                preco = "0";

            double aux = Double.Parse(preco);
            item.Preco = aux / 100;
            item.Fornecedor = "Barnes&Noble";
        }

        return items;
    }

    public Item EfectuaPedido(string isbn){
        string preco = BarnesNoble.GetBNQuote(isbn);

        Item item = new Item();
        item.Isbn = isbn;
        double aux = Double.Parse(preco);
        item.Preco = aux / 100;
        item.Fornecedor = "Barnes and Noble";

        return item;
    }
}
