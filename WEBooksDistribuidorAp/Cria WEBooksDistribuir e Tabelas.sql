CREATE DATABASE WEBooksDistribuidor
GO

USE WEBooksDistribuidor

CREATE TABLE Encomenda(
	idEncomenda nchar(100) NOT NULL,
	valorTotal varchar(max),
	estado varchar(max),
	nomeCliente varchar(max),
	numero varchar(max),
	address varchar(max),	
	city varchar(max),
	state varchar(max),
	ZIPcode varchar(max),
	country varchar(max),
	tempoEspera int,
 PRIMARY KEY (idEncomenda)
)


CREATE TABLE Livro(
	idLivro nchar(100) NOT NULL,
	idEncomenda nchar(100) NOT NULL,
	ISBN varchar(max) NOT NULL,
	titulo varchar(max),
	precoVenda varchar(max),
	tempoEntrega varchar(max),
	nomeFornecedor varchar(max) NOT NULL,
	quantidade int,
 PRIMARY KEY (idLivro, idEncomenda),
 FOREIGN KEY (idEncomenda) REFERENCES Encomenda (idEncomenda)
)

