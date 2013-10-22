CREATE DATABASE WEBooksBD
GO

USE WEBooksBD

CREATE TABLE Utilizador
(
	idUtilizador nchar(100) NOT NULL,
	nome varchar(max) NULL,
	username nchar(100) NOT NULL UNIQUE,
	password varchar(max) NOT NULL,
	PRIMARY KEY (idUtilizador)
)

CREATE TABLE Cliente(
	idCliente nchar(100) NOT NULL,
	telefone varchar(max) ,
	numero varchar(max),
	address varchar(max),
	city varchar(max),
	state varchar(max),
	ZIPcode varchar(max),
	country varchar(max),
	latitude varchar(max),
	longitude varchar(max),
	numCartaoCredito varchar(max),
	dataValidadeCartaoCredito varchar(max),
	estado varchar(max),
 PRIMARY KEY (idCliente),
 FOREIGN KEY (idCliente) REFERENCES Utilizador (idUtilizador)
)

CREATE TABLE Gestor(
	idGestor nchar(100) NOT NULL,
 PRIMARY KEY (idGestor),
 FOREIGN KEY (idGestor) REFERENCES Utilizador (idUtilizador)
)

CREATE TABLE Livro(
	idLivro nchar(100) NOT NULL,
	ISBN varchar(max) NOT NULL,
	titulo varchar(max),
	categoria varchar(max),
	autores varchar(max),
	editora varchar(max),
	anoEdicao varchar(max),
	precoVenda varchar(max),
	precoSemDesconto varchar(max),
	urlImagem varchar(max),
	tempoEntrega varchar(max),
	desconto bit,
	nomeFornecedor nchar(50) NOT NULL,
	numPesquisas int,
 PRIMARY KEY (idLivro)
)

CREATE TABLE Carrinho(
	idCarrinho nchar(100) NOT NULL,
	idCliente nchar(100) NOT NULL,
	idLivro nchar(100) NOT NULL,
	quantidade int,
 PRIMARY KEY (idCarrinho, idCliente, idLivro),
 FOREIGN KEY (idCliente) REFERENCES Cliente (idCliente),
 FOREIGN KEY (idLivro) REFERENCES Livro (idLivro)
)

CREATE TABLE Encomenda(
	idEncomenda nchar(100) NOT NULL,
	idCliente nchar(100) NOT NULL,
	valorTotal varchar(max),
	dataCriacao varchar(max),
 PRIMARY KEY (idEncomenda),
 FOREIGN KEY (idCliente) REFERENCES Cliente (idCliente)
)

CREATE TABLE LinhaEncomenda(
	idLinhaEncomenda nchar(100) NOT NULL,
	idEncomenda nchar(100) NOT NULL,
	idLivro nchar(100) NOT NULL,
	quantidade int,
	precoVenda varchar(max),
	comDesconto bit,
 PRIMARY KEY (idLinhaEncomenda, idEncomenda, idLivro),
FOREIGN KEY (idEncomenda) REFERENCES Encomenda (idEncomenda),
FOREIGN KEY (idLivro) REFERENCES Livro (idLivro)
)

CREATE TABLE PesquisasHistorico(
	idPesquisasHistorico nchar(100) NOT NULL,
	idCliente nchar(100) NOT NULL,
	idLivro nchar(100) NOT NULL,
	numero int,
 PRIMARY KEY (idPesquisasHistorico, idCliente, idLivro),
FOREIGN KEY (idCliente) REFERENCES Cliente (idCliente),
FOREIGN KEY (idLivro) REFERENCES Livro (idLivro)
)

CREATE TABLE EstadoHistorico(
	idEstado nchar(100) NOT NULL,
	idEncomenda nchar(100) NOT NULL,
	nomeEstado varchar(max),
	contador int,
	dataAlteracao varchar(max),
  PRIMARY KEY (idEstado, idEncomenda),
FOREIGN KEY (idEncomenda) REFERENCES Encomenda (idEncomenda)
)

CREATE TABLE TopSellers(
	idTopSellers nchar(100) NOT NULL,
	tipoCapa varchar(max),
	titulo varchar(max),
	autores varchar(max),
  PRIMARY KEY (idTopSellers)
)