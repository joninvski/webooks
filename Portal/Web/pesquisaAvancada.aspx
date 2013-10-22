<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pesquisaAvancada.aspx.cs" Inherits="Web_procura" Title="WEBooks [Pesquisa de Livros]" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentHolder" runat="server">
    <h1>Pesquisa Avançada de Livros</h1>
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContentHolder" Runat="Server">
   
    
    <center>
    <h3>Preencha os campos desejados:</h3>
        <h4>Keyword: <asp:TextBox ID="KeywordBox" runat="server" OnTextChanged="Pesquisa" />
            Autor:&nbsp<asp:TextBox ID="AutorBox" runat="server" OnTextChanged="Pesquisa" />
            Título:&nbsp<asp:TextBox ID="TituloBox" runat="server" OnTextChanged="Pesquisa" />
            ISBN:&nbsp<asp:TextBox ID="IsbnBox" runat="server" OnTextChanged="Pesquisa" />
            <asp:Button ID="PesquisarButton" runat="server" OnClick="Pesquisa" Text="Pesquisar"/>
        </h4>
        <div id="Erros" runat="server" />
        <center >
        
<% if (this.Session["Livro"] == null)
   { %>

        <asp:GridView ID="PesquisaGrid" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="OperacaoLivro" AutoGenerateColumns="False" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:ImageField DataImageUrlField="ImageUrl" HeaderText="Image"/>
                <asp:BoundField DataField ="ISBN" HeaderText="ISBN" />
                <asp:BoundField DataField="Titulo" HeaderText="Titulo" />
                <asp:BoundField DataField ="Autores" HeaderText="Autores"  />
                <asp:BoundField DataField ="Editora" HeaderText="Editora"  />
                <asp:BoundField DataField ="PrecoVenda" HeaderText="Preço de Venda"  />
                <asp:ButtonField CommandName="Detalhes" ButtonType="Button" Text="Detalhes" />
                <asp:ButtonField CommandName="AddCart" ButtonType="Button" Text="Adicionar Carrinho" />
            </Columns>
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
       <asp:button ID="idAnteriorPagina" runat="server" OnClick="PaginaAnterior" Text="Pagina Anterior"/>       
       <asp:Label ID="paginaActual" runat="server" Text=""></asp:Label>
       <asp:button ID="idProximaPagina" runat="server" OnClick="PaginaSeguinte" Text="Pagina Seguinte"/>
        </center>
<%}
  else
  { %>

<asp:Table ID="TabelaDadosLivro" runat="server">
        <asp:TableRow runat="server">
            <asp:TableCell ColumnSpan="3" runat="server"><h1>Dados Livro</h1></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server" RowSpan="9" VerticalAlign="Top"> 
                <asp:HyperLink ID="imgLivro" runat="server" /> </asp:TableCell>
                
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"> <h2>ISBN</h2> </asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server">
                <asp:Label ID="lbISBN" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Titlulo</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbTitulo" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Categoria</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbCategoria" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Autore(s)</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbAutores" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Editora</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbEditora" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Ano Edicao</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbAnoEdicao" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Preco Venda</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbPrecoVenda" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Tempo Entrega</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbTempoEntrega" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Nome Fornecedor</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbNomeFornecedore" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
     <asp:Button ID="btVoltaLivros" runat="server" Text="<< Voltar Atrás" OnClick="VoltaLivros"/>
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="btAddCart" runat="server" Text="Adiciona Carrinho" OnClick="BotaoAddCart"/>

<%} %>
    </center>  
</asp:Content>

