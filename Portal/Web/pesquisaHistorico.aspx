<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pesquisaHistorico.aspx.cs" Inherits="Web_pesquisaHistorico" Title="WEBooks [Pesquisa Historico]" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentHolder" runat="server">
    <h1>Pesquisa Historico</h1>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContentHolder" Runat="Server">
    <h1>Insira o login do utilizador: <asp:TextBox ID="TBUserName" runat="server"/></h1>
    <asp:Button ID="BTPesquisaHistorico" runat="server" Text="Pesquisa Utilizador" OnClick="PesquisaHistorico" />
    
    <br />
    <div id="DivUserInexistente" runat="server" />
    
    <asp:GridView ID="HistoricoGrid" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:ImageField DataImageUrlField="ImageUrl" HeaderText="Image"/>
                <asp:BoundField DataField ="ISBN" HeaderText="ISBN" />
                <asp:BoundField DataField="Titulo" HeaderText="Titulo" />
                <asp:BoundField DataField ="Categoria" HeaderText="Categoria" />
                <asp:BoundField DataField ="Autores" HeaderText="Autores"  />
                <asp:BoundField DataField ="Editora" HeaderText="Editora"  />
                <asp:BoundField DataField ="AnoEdicao" HeaderText="Ano de Edição"  />
                <asp:BoundField DataField ="PrecoVenda" HeaderText="Preço de Venda"  />
            </Columns>
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
</asp:Content>