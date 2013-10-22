using System;
using System.Text;
using System.Xml;


    public class EncomendaWEBooks
    {
        private string _idEncomenda = null;
        private string _dataCriacao = null;
        private string _tipoMensagem = null;
        private string _morada = null;
        private Book[] _listaBooks = null;


        public EncomendaWEBooks(XmlDocument xmlDoc)
        {
            //buscar a raiz da Encomenda
            XmlNodeList nosEncomenda = xmlDoc.GetElementsByTagName("ns0:EncomendaDist");
            XmlNode noEncomenda = nosEncomenda.Item(0);

            //buscar as propriedades que estao dentro da raiz da encomenda
            XmlNodeList elementosEncomenda = noEncomenda.ChildNodes;
            foreach (XmlNode elementoEncomenda in elementosEncomenda)
            {
                if (elementoEncomenda == null)
                {
                    continue; //apenas por precaucao
                }
                else if (elementoEncomenda.Name.Equals("idEncomenda"))
                {
                    this.IdEncomenda = elementoEncomenda.InnerText;
                }
                else if (elementoEncomenda.Name.Equals("DataCriacao"))
                {
                    this.DataCriacao = elementoEncomenda.InnerText;
                }
                else if (elementoEncomenda.Name.Equals("TipoMensagem"))
                {
                    this.TipoMensagem = elementoEncomenda.InnerText;
                }else if (elementoEncomenda.Name.Equals("Morada"))
                {
                    this.Morada = elementoEncomenda.InnerText;
                }
                else if (elementoEncomenda.Name.Equals("ListaBook"))
                {
                    //para cada livro criar o objecto book
                    XmlNodeList elementoListaBook = elementoEncomenda.ChildNodes;
                    this.ListaBook = new Book[elementoListaBook.Count];
                    int i = 0;
                    foreach (XmlNode elementoBookDist in elementoListaBook)
                    {
                        this.ListaBook[i] = new Book(elementoBookDist);
                        i++;
                    }
                }
            }
        }

        public EncomendaWEBooks()
        {           
        }

        public string IdEncomenda
        {
            get { return _idEncomenda; }
            set { _idEncomenda = value; }
        }

        public string DataCriacao
        {
            get { return _dataCriacao; }
            set { _dataCriacao = value; }
        }

        public string TipoMensagem
        {
            get { return _tipoMensagem; }
            set { _tipoMensagem = value; }
        }

        public string Morada
        {
            get { return _morada; }
            set { _morada = value; }
        }

        public Book[] ListaBook
        {
            get { return _listaBooks; }
            set { _listaBooks = value; }
        }

        public XmlDocument Encomenda2XmlDocument() {
            return this.Encomenda2XmlDocument(this.IdEncomenda);
        }

        public XmlDocument Encomenda2XmlDocument(string tipoMensagem)
        {
            XmlDocument ret = new XmlDocument();
            string temp = "";
            temp = "<ns0:EncomendaDist xmlns:ns0=\"http://www.WEBooks.pt/Distribuidor\">";

            if (this.IdEncomenda != null)
            {
                temp += "<idEncomenda>" + this.IdEncomenda + "</idEncomenda>";
            }
            if (this.DataCriacao != null)
            {
                temp += "<DataCriacao>" + this.DataCriacao + "</DataCriacao>";
            }
            if (this.TipoMensagem != null)
            {
                temp += "<TipoMensagem>" + this.TipoMensagem + "</TipoMensagem>";
            }
            if (this.Morada != null)
            {
                temp += "<Morada>" + this.Morada + "</Morada>";
            }
            temp += "</ns0:EncomendaDist>";
            ret.InnerXml = temp;

            return ret;
        }

        public override string ToString()
        {
            string ret = "<ns0:EncomendaDist xmlns:ns0=\"http://www.WEBooks.pt/Distribuidor\">";
            if (this.IdEncomenda != null)
            {
                ret += "<idEncomenda>" + this.IdEncomenda + "</idEncomenda>";
            }
            if (this.DataCriacao != null)
            {
                ret += "<DataCriacao>" + this.DataCriacao + "</DataCriacao>";
            }
            if (this.TipoMensagem != null)
            {
                ret += "<TipoMensagem>" + this.TipoMensagem + "</TipoMensagem>";
            }
            if (this.Morada != null)
            {
                ret += "<Morada>" + this.Morada + "</Morada>";
            }

            if (ListaBook != null)
            {
                ret += "<ListaBook>";
                foreach (Book liv in this.ListaBook)
                {
                    ret += liv.ToString();
                }
                ret += "</ListaBook>";
            }

            ret += "</ns0:EncomendaDist>";
            return ret;
        }
    }

    public class Book
    {
        private string _ISBN = null;
        private string _titulo = null;
        private string _disponibilidade = null;
        private string _fornecedor = null;
        private double _precoVenda = 0;
        private int _quantidade = 0;

        public Book()
        {            
        }

        public Book(XmlNode xmlNo)
        {
            //vamos colocar as propriedades dos livros
            foreach (XmlNode elementoBook in xmlNo)
            {
                if (elementoBook == null)
                {
                    continue; //apenas por precaucao
                }
                else if (elementoBook.Name.Equals("ISBN"))
                {
                    this.ISBN = elementoBook.InnerText;
                }
                else if (elementoBook.Name.Equals("Titulo"))
                {
                    this.Titulo = elementoBook.InnerText;
                }
                else if (elementoBook.Name.Equals("Disponibilidade"))
                {
                    this.Disponibilidade = elementoBook.InnerText;
                }
                else if (elementoBook.Name.Equals("Fornecedor"))
                {
                    this.Fornecedor = elementoBook.InnerText;
                }
                else if (elementoBook.Name.Equals("Quantidade"))
                {
                    try
                    {
                        this.Quantidade = int.Parse(elementoBook.InnerText);
                    }
                    catch { this.Quantidade = 0; }//pelo sim pelo nao.... vamos por o preco a 0 se bem que deviamo-nos queixar
                }
                else if (elementoBook.Name.Equals("PrecoVenda"))
                {
                    try
                    {
                        this.PrecoVenda = double.Parse(elementoBook.InnerText);
                    }
                    catch { this.PrecoVenda = 0; }//pelo sim pelo nao.... vamos por o preco a 0 se bem que deviamo-nos queixar
                }
            }
        }

        public string ISBN
        {
            get { return _ISBN; }
            set { _ISBN = value; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string Disponibilidade
        {
            get { return _disponibilidade; }
            set { _disponibilidade = value; }
        }

        public string Fornecedor
        {
            get { return _fornecedor; }
            set { _fornecedor = value; }
        }

        public double PrecoVenda
        {
            get { return _precoVenda; }
            set { _precoVenda = value; }
        }

        public int Quantidade
        {
            get { return _quantidade; }
            set { _quantidade = value; }
        }

        public override string ToString()
        {
            string ret = "<BookDist>";

            if (this.ISBN != null)
            {
                ret += "<ISBN>" + this.ISBN + "</ISBN>";
            }
            if (this.Titulo != null)
            {
                ret += "<Titulo>" + this.Titulo + "</Titulo>";
            }
            if (this.Disponibilidade != null)
            {
                ret += "<Disponibilidade>" + this.Disponibilidade + "</Disponibilidade>";
            }
            if (this.Fornecedor != null)
            {
                ret += "<Fornecedor>" + this.Fornecedor + "</Fornecedor>";
            }
            if (this.PrecoVenda != null)
            {
                ret += "<Fornecedor>" + this.PrecoVenda + "</Fornecedor>";
            }
            if (this.Quantidade != null)
            {
                ret += "<Quantidade>" + this.Quantidade + "</Quantidade>";
            }
            ret += "</BookDist>";

            return ret;
        }
    }

