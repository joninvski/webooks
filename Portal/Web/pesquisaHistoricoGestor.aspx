<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pesquisaHistoricoGestor.aspx.cs" Inherits="Web_pesquisaHistorico" Title="WEBooks [Pesquisa Historico]" %>
<%@ Import Namespace="WEBooksBD" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentHolder" runat="server">
    <h1>Pesquisa Historico Encomendas </h1>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContentHolder" Runat="Server">
    <div id="DivUserInexistente" runat="server" />    
        
        
    <asp:Label ID="lbInsiraLogin" runat="server" Text="Insira a palavra:" Font-Size="Larger" Font-Bold="true"></asp:Label> <asp:TextBox ID="tbUserName" runat="server" />
    <asp:Button ID="btPesquisaHistorico" runat="server" Text="Procurar" OnClick="PesquisaHistoricoEncomendas" /> 
    <br />
    <asp:RadioButton ID="rbCliente" runat="server"  Text="Cliente"  Visible="false" Checked="true" GroupName="pesquisaEncomendas" Font-Size="Large" />
    <asp:RadioButton ID="rbLivro" runat="server"  Text="Livro" Visible="false"   GroupName="pesquisaEncomendas" Font-Size="Large" />
    <asp:RadioButton ID="rbTodas" runat="server"  Text="Todas" Visible="false"   GroupName="pesquisaEncomendas" Font-Size="Large" />
    
    <% if (this.Session["mostraEncomendas"] != null ){ %>
    
    <% if (this.Session["Encomenda"] == null)
       { %>
       <asp:GridView ID="HistoricoEncomendas" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowCommand="OperacaoEncomenda">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                
                <Columns>
                    <asp:BoundField DataField="IdEncomenda" HeaderText="Numero Encomenda"/>
                    <asp:BoundField DataField="Cliente" HeaderText="Nome Comprador"/>
                    <asp:BoundField DataField="DataCriacao" HeaderText="Data do CheckOut"/>
                    <asp:BoundField DataField="Estado" HeaderText="Estado"/>
                    <asp:BoundField DataField="ValorTotal" HeaderText="Valor Total"/>
                    <asp:ButtonField CommandName="MostaDestalhes" ButtonType="Button" Text="Destalhes" />
                    <asp:ButtonField CommandName="Cancelar" ButtonType="Button" Text="Cancelar Encomenda" />
                </Columns>
                
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        
        
            <%} else { %>
         
        <asp:Table ID="tabelaEncomenda" runat="server" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" CellSpacing="1" BorderColor="black">
            <asp:TableRow runat="server" BorderWidth="2px">
                <asp:TableCell runat="server" BorderWidth="2px"> <asp:Label ID="Label1" runat="server" Text="Numero Encomenda" Font-Bold="true"></asp:Label> </asp:TableCell>
                <asp:TableCell ID="TableCell7" runat="server" BorderWidth="1"> <asp:Label ID="lbidEncomenda" runat="server"></asp:Label> </asp:TableCell>
                <asp:TableCell ID="TableCell3" runat="server" Width="60%" > </asp:TableCell>
             </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server" BorderWidth="2px">   
                <asp:TableCell runat="server" BorderWidth="2px"><asp:Label ID="Label2" runat="server" Text="Nome Comprador" Font-Bold="true"></asp:Label></asp:TableCell>
                <asp:TableCell ID="TableCell8" runat="server" BorderWidth="1"><asp:Label ID="lbNomeComprador" runat="server"></asp:Label></asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server" Width="60%" > </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow4" runat="server" BorderWidth="2px">    
                <asp:TableCell runat="server" BorderWidth="2px"><asp:Label ID="Label3" runat="server" Text="Data do CheckOut" Font-Bold="true"></asp:Label></asp:TableCell>
                <asp:TableCell ID="TableCell9" runat="server" BorderWidth="1"><asp:Label ID="lbDataCriacao" runat="server"></asp:Label></asp:TableCell>
               <asp:TableCell ID="TableCell5" runat="server" Width="60%" > </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow5" runat="server" BorderWidth="2px">    
                <asp:TableCell runat="server" BorderWidth="2px"><asp:Label ID="Label4" runat="server" Text="Estado" Font-Bold="true"></asp:Label></asp:TableCell>
                <asp:TableCell ID="TableCell10" runat="server" BorderWidth="1"><asp:Label ID="lbEstado" runat="server"></asp:Label>
                
                    <asp:GridView ID="TabelaEstados" runat="server" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>                                            
                            <asp:BoundField DataField="contador" HeaderText="Ordem" />
                            <asp:BoundField DataField="nomeEstado" HeaderText="Estado" />
                            <asp:BoundField DataField="dataAlteracao" HeaderText="Data Alteracao" />
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>                 
                
                
                </asp:TableCell>
                <asp:TableCell ID="TableCell6" runat="server" Width="60%" > </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow6" runat="server" BorderWidth="2px">    
                <asp:TableCell runat="server" BorderWidth="2px"><asp:Label ID="Label5" runat="server" Text="Valor Total" Font-Bold="true"></asp:Label></asp:TableCell>
                <asp:TableCell ID="TableCell11" runat="server" BorderWidth="1"><asp:Label ID="lbValorTotal" runat="server"></asp:Label></asp:TableCell>
                <asp:TableCell ID="TableCell12" runat="server" Width="60%" > </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow ID="TableRow1" runat="server" BorderWidth="2px">
                <asp:TableCell ID="TableCell1" runat="server" BorderWidth="2" ColumnSpan="3"><asp:Label ID="Label6" runat="server" Text="Livros da Encomenda" Font-Bold="true"></asp:Label></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow ID="TableRow2" runat="server" BorderWidth="2px">
                <asp:TableCell ID="TableCell13" runat="server" BorderWidth="2" ColumnSpan="3">
                
                    <asp:GridView ID="LivroTabela" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
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
                        
                </asp:TableCell>
            </asp:TableRow>
            
        </asp:Table>
        <asp:Button ID="Button1" runat="server" Text="<< Voltar Atrás" OnClick="VoltaEncomendas"/>        
           
        <%} %>  
     <%} %>  
                  
</asp:Content>