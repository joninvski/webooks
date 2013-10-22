<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MostraDadosGestao.aspx.cs" Inherits="Web_procura" Title="WEBooks [Dados Gestao]" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentHolder" runat="server">
    <h1>Dados de Gestao</h1>
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContentHolder" Runat="Server">
   
    
    <center>
        <div id="ErrosDadosGestao" runat="server" />
        

<asp:Table ID="TabelaDadosGestao" runat="server" Visible="false">
        <asp:TableRow runat="server">
            <asp:TableCell ColumnSpan="3" runat="server"><h1>Dados sobre Livros</h1></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server" > <h2> Numero de Livros Comprados </h2> </asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server">
                <asp:Label ID="lbLivrosComprados" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Numero de Livros Pesquisados</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbLivrosPesquisados" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Numero de Livros Comprados Com Desconto</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbLivrosCompradosComDesconto" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow runat="server">
            <asp:TableCell HorizontalAlign="Left" BorderWidth="1px" runat="server"><h2>Numero de Livros Comprados Sem Desconto</h2></asp:TableCell>
            <asp:TableCell Font-Size="Medium" HorizontalAlign="Left" BorderWidth="1px" runat="server"> 
                <asp:Label ID="lbLivrosCompradosSemDesconto" runat="server"></asp:Label>&nbsp;</asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
    </center>  
</asp:Content>

