<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="registoClienteSucesso.aspx.cs" Inherits="Web_registoClienteSucesso" Title="WEBooks [Registo de utilizadores]" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentHolder" runat="server">

    <h1>Registo de Cliente com Sucesso</h1>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContentHolder" Runat="Server">

    <div id="registoSucesso" runat="server" >&nbsp;</div>

    <h1>Registado com Sucesso</h1>
    <br />

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

    </asp:Table>   

</asp:Content>

