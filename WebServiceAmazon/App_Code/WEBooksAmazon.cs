using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SEI
{
    class WEBooksAmazon {

        private com.amazon.webservices.AWSECommerceService envio;

        private com.amazon.webservices.ItemSearch pergunta;
        private com.amazon.webservices.ItemSearchRequest procuraItem;
        private com.amazon.webservices.ItemSearchRequest[] array;
        private com.amazon.webservices.ItemSearchResponse resposta;

        private com.amazon.webservices.Item[] itemsRetorno;

        private com.amazon.webservices.ItemLookup questItemLookup;
        private com.amazon.webservices.ItemLookupRequest[] questArrayItemLookupRequest;
        private com.amazon.webservices.ItemLookupResponse questItemLookupResponse;

        private com.amazon.webservices.Items[] itemsArrayRetorno;

        public com.amazon.webservices.Item[] ProcuraItem(string page, string keyword, string author, string title, string isbn)
        {
            Inicializacao();
            if( 0 < keyword.Length )
                procuraItem.Keywords = keyword;
            if( 0 < author.Length )
                procuraItem.Author   = author;
            if( 0 < title.Length )
                procuraItem.Title    = title;
            if (0 < isbn.Length)
                procuraItem.Power = "isbn:" + isbn;

            procuraItem.ItemPage = page;
            CompletaPedido();
            itemsRetorno = EfectuaPedido();            

            if ( null == itemsRetorno ) {
                throw new EfectuaPedidoException();
            }

            return itemsRetorno;
        }

        private void Inicializacao(){
            pergunta = new com.amazon.webservices.ItemSearch();
            pergunta.SubscriptionId = "10AGG53C5PNVFK3YBX02";
            procuraItem = new com.amazon.webservices.ItemSearchRequest();
            procuraItem.SearchIndex = "Books";
            procuraItem.ResponseGroup = new string[] { "Medium", "Subjects", "Offers" };

            return;
        }

        private void CompletaPedido() {
            array = new com.amazon.webservices.ItemSearchRequest[10];
            array[0] = procuraItem;
            pergunta.Request = array;
            return;
        }

        private com.amazon.webservices.Item[] EfectuaPedido()
        {
            envio = new com.amazon.webservices.AWSECommerceService();
            resposta = envio.ItemSearch(pergunta);
            return resposta.Items[0].Item;
        }

        public void ActualizaItem( ref Item[] items ) {
            questItemLookup = new com.amazon.webservices.ItemLookup();
            questItemLookup.AWSAccessKeyId = "10AGG53C5PNVFK3YBX02";

            questArrayItemLookupRequest = new com.amazon.webservices.ItemLookupRequest[10];
            int aux = 0;
            foreach (Item item in items)
            {
                com.amazon.webservices.ItemLookupRequest questItemLookupRequest =
                    new com.amazon.webservices.ItemLookupRequest();
                questItemLookupRequest.IdType = com.amazon.webservices.ItemLookupRequestIdType.ASIN;
                questItemLookupRequest.ResponseGroup = new string[] { "BrowseNodes" };
                questItemLookupRequest.ItemId = new string[] { item.Isbn };
                questArrayItemLookupRequest[0] = questItemLookupRequest;

                questItemLookup.Request = questArrayItemLookupRequest;
                itemsArrayRetorno = RealizaPedido();

                com.amazon.webservices.Item itemAux = itemsArrayRetorno[0].Item[0];
                object cenas = itemAux.BrowseNodes.BrowseNode.SyncRoot;
                for (int i = itemAux.BrowseNodes.BrowseNode.Length - 1; i >= 0; i--) {
                    com.amazon.webservices.BrowseNode no = itemAux.BrowseNodes.BrowseNode[i];
                        
                    while (no != null && no.Ancestors != null && no.Name != null)
                    {
                        item.Categoria += no.Name + ", ";
                        no = no.Ancestors[0];
                    }

                    item.Categoria += " / ";
                }

            }

            //com.amazon.webservices.ItemLookupRequest questItemLookupRequest =
            //        new com.amazon.webservices.ItemLookupRequest();
            //questItemLookupRequest.IdType = com.amazon.webservices.ItemLookupRequestIdType.ASIN;
            //questItemLookupRequest.ResponseGroup = new string[] { "BrowseNodes" };
            //questItemLookupRequest.ItemId = new string[] { "0375507906" };
            //questArrayItemLookupRequest[0] = questItemLookupRequest;

            //questItemLookup.Request = questArrayItemLookupRequest;
            //itemsArrayRetorno = RealizaPedido();

            //if (null == itemsArrayRetorno)
            //{
            //    throw new EfectuaPedidoException();
            //}

            //for (int i = 0; i < itemsArrayRetorno.Length; i++)
            //{
            //    com.amazon.webservices.Item itemAux = itemsArrayRetorno[0].Item[0];
            //    items[i].Categoria = itemAux.BrowseNodes.BrowseNode.ToString();
            //}

            return;
        }


        private com.amazon.webservices.Items[] RealizaPedido() {
            envio = new com.amazon.webservices.AWSECommerceService();
            questItemLookupResponse = envio.ItemLookup(questItemLookup);
            return questItemLookupResponse.Items;
        }

    }//class

}