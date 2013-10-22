<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MostraClientes.aspx.cs" Inherits="Web_pesquisaHistorico" Title="WEBooks [Pesquisa Historico]" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentHolder" runat="server">
    <h1>Mostra Clientes Registados </h1>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContentHolder" Runat="Server">
    <div id="DivUserInexistente" runat="server" />

<% if (this.Session["Cliente"] == null)
   {%>

    <asp:DataGrid ID="ListaClientes" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnItemCommand="GerirClientes">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            
            <Columns>
                <asp:BoundColumn DataField="IdCliente" HeaderText="ID do Cliente"/>
                <asp:BoundColumn DataField="Nome" HeaderText="Nome Cliente"/>
                <asp:BoundColumn DataField="Username" HeaderText="Username"/>
                <asp:BoundColumn DataField="Estado" HeaderText="Estado"/>
                
                <asp:TemplateColumn HeaderText="Detalhes">
                    <ItemTemplate>                    
                        <asp:LinkButton ID="btDetalhes" Text="Detahes" CommandName="Detalhes" ForeColor="Blue" runat="server"> </asp:LinkButton>
                    </ItemTemplate>                
                </asp:TemplateColumn >
                <asp:TemplateColumn HeaderText="Apagar">
                    <ItemTemplate>                    
                        <asp:LinkButton ID="btApagar" Text="Apagar" CommandName="Apagar" ForeColor="Blue" runat="server"> </asp:LinkButton>
                    </ItemTemplate>                
                </asp:TemplateColumn>
                
            </Columns>
            
            
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           
            
    </asp:DataGrid>
    
    <% }
       else
       { %>
    
    <asp:Table ID="TabelaDadosCliente" runat="server">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2"><h1>Dados Utilizador</h1></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"> <h2>Nome</h2> </asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"><asp:Label ID="lbNome" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>User Name (email)</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbUsername" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2"><h1>Dados Pagamento</h1></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>Telefone</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbTelefone" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>Número Cartão Crédito</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbNrCartaoCredito" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>Data Validade (DD-MM-AAAA)</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbDataValidade" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2"><h1>Dados Envio</h1></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>Numero</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbNumero" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>Rua</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbRua" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>Cidade</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbCidade" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>Estado</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbEstado" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>Zip Code</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbZipCode" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>País</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbPais" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2"><h1>Detalhes de Gestão</h1></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1"><h2>Estado</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1"> 
                <asp:Label ID="lbEstadoCliente" runat="server" Text=""></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>   
    </asp:Table>
    
    <asp:Button ID="btVoltaClientes" runat="server" Text="<< Voltar Atrás" OnClick="VoltaClientes"/>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btApagaCliente" runat="server" Text="Apagar Cliente" OnClick="BotaoApagaCliente"/>
    
    <% } %>              
</asp:Content>