<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="carrinhoCompras.aspx.cs" Inherits="Web_checkOut" Title="Carrinho Compras" %>
<%@ Import Namespace="WEBooksBD" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentHolder" Runat="Server">
    <h1>Mostra Carrinho Compras</h1>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContentHolder" Runat="Server">
    <%if (this.Session["carrinhoVazio"] != "" && this.Session["utilizador"] != null)
      { %>
    <table>
        <tr>
            <td>
                <asp:GridView ID="CarrinhoGrid" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnSelectedIndexChanged="CarrinhoGrid_SelectedIndexChanged">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:ImageField DataImageUrlField="ImageUrl" HeaderText="Image"/>
                        <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                        <asp:BoundField DataField="Titulo" HeaderText="Titulo" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="Autores" HeaderText="Autores" />
                        <asp:BoundField DataField="Editora" HeaderText="Editora" />
                        <asp:BoundField DataField="AnoEdicao" HeaderText="Ano Edicao" />
                        <asp:BoundField DataField="PrecoVenda" HeaderText="Preço Venda" />
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Remover Livro" />
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>           
        </tr>
        <tr>
         <td>
            <asp:Button ID="CheckOut" runat="server" Text="CheckOut" OnCommand="FazerCheckOut" OnClick="FazerCheckOut"/>
        </td>
        </tr>
    </table>
    <%}
      else if (this.Session["utilizador"] == null)
      { %>   
      <center><div id="ErroCarrinhoLogado" runat="server"></div></center>
    <%} 
      else if (this.Session["carrinhoVazio"] == "")
      { %>   
      <center><div id="ErroCarrinho" runat="server"></div></center>
    <%} %>
</asp:Content>

