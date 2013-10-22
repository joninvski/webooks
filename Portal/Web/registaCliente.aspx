<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="registaCliente.aspx.cs" Inherits="Web_registaCliente" Title="WEBooks [Registo de utilizadores]" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentHolder" runat="server">
    <h1>Registar Novo Cliente</h1>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContentHolder" Runat="Server">
    <div id="ErroRegisto" runat="server" ></div>
    <table>
        <tr>
            <td colspan="2"><h1>Dados Utilizador</h1></td>
            <td>  </td>
        </tr>
        <tr>
            <td><h2>Nome</h2></td>
            <td> <asp:TextBox id="tbNome" runat="server"/> </td>
            <td><asp:RequiredFieldValidator ControlToValidate="tbNome" ID="NomeErro" runat="server" ErrorMessage="� necess�rio preencher o Nome"/></td>
        </tr>
        <tr>
            <td><h2>User Name (email)</h2></td>
            <td> <asp:TextBox ID="tbUserName" runat="server"/> </td>
            <td><asp:RequiredFieldValidator ControlToValidate="tbUserName" ID="RequiredFieldValidator1" runat="server" ErrorMessage="� necess�rio preencher o User Name"/></td>
        </tr>
        <tr>
            <td><h2>Password</h2></td>
            <td> <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" /> </td>
            <td><asp:RequiredFieldValidator ControlToValidate="tbPassword" ID="PasswordErro" runat="server" ErrorMessage="� necess�rio preencher a Password"/></td>
        </tr>
        <tr>
            <td><h2>Reescreva a Password</h2></td>
            <td> <asp:TextBox ID="tbPasswordValidacao" runat="server" TextMode="Password" /> </td>
            <td>
                <asp:CompareValidator ControlToCompare="tbPassword" ControlToValidate="tbPasswordValidacao" ID="PasswordComparacao" runat="server" ErrorMessage="As passwords n�o correspondem"></asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan="2"><h1>Dados Pagamento</h1></td>
            <td>  </td>
        </tr>
        <tr>
            <td><h2>Telefone</h2></td>
            <td> <asp:TextBox ID="tbTelefone" runat="server" MaxLength="9"/> </td>
            <td><asp:RequiredFieldValidator ControlToValidate="tbTelefone" ID="TelefoneErro" runat="server" ErrorMessage="� necess�rio preencher o Telefone"/></td>
        </tr>
        <tr>
            <td><h2>N�mero Cart�o Cr�dito</h2></td>
            <td> <asp:TextBox ID="tbNrCartaoCredito" runat="server" /> </td>
            <td><asp:RequiredFieldValidator ControlToValidate="tbNrCartaoCredito" ID="NrCartaoCreditoErro" runat="server" ErrorMessage="� necess�rio preencher o N�mero Cart�o Cr�dito"/></td>
        </tr>
        <tr>
            <td> <h2>Data Validade (DD-MM-AAAA) </h2> </td>
            <td> <asp:TextBox ID="tbDataValidade" runat="server" MaxLength="10" /> </td>
            <td> <asp:RequiredFieldValidator ControlToValidate="tbDataValidade" ID="DataValidadeErro" runat="server" ErrorMessage="� necess�rio preencher a Data Validade"/></td>
        </tr>
        <tr>
            <td colspan="2"><h1>Dados Envio</h1></td>
            <td>  </td>
        </tr>
        <tr>
            <td><h2>Numero</h2></td>
            <td> <asp:TextBox ID="tbNumero" runat="server" /> </td>
            <td><asp:RequiredFieldValidator ControlToValidate="tbNumero" ID="NumeroErro" runat="server" ErrorMessage="� necess�rio preencher o Numero"/></td>
        </tr>

        <tr>
            <td><h2>Rua</h2></td>
            <td> <asp:TextBox ID="tbRua" runat="server" /> </td>
            <td><asp:RequiredFieldValidator ControlToValidate="tbRua" ID="RuaErro" runat="server" ErrorMessage="� necess�rio preencher a Rua"/></td>
        </tr>
        <tr>
            <td><h2>Cidade</h2></td>
            <td> <asp:TextBox ID="tbCidade" runat="server" /> </td>
            <td><asp:RequiredFieldValidator ControlToValidate="tbCidade" ID="CidadeErro" runat="server" ErrorMessage="� necess�rio preencher a Cidade"/></td>
        </tr>
        <tr>
            <td><h2>Estado</h2></td>
            <td> <asp:TextBox ID="tbEstado" runat="server" /> </td>
            <td><asp:RequiredFieldValidator ControlToValidate="tbEstado" ID="EstadoErro" runat="server" ErrorMessage="� necess�rio preencher o Estado"/></td>
        </tr>
        <tr>
            <td><h2>Zip Code</h2></td>
            <td> <asp:TextBox ID="tbZipCode" runat="server" /> </td>
            <td> </td>
        </tr>
        <tr>
            <td><h2>Pa�s</h2></td>
            <td> <asp:TextBox ID="tbPais" runat="server" /> </td>
            <td><asp:RequiredFieldValidator ControlToValidate="tbPais" ID="PaisErro" runat="server" ErrorMessage="� necess�rio preencher o Pa�s"/></td>
        </tr>
        <tr>
            <td> <asp:Button id="btRegistar" Text="Resgistar Novo Cliente" OnCommand="RegistaCliente" runat="server"/> </td>
            <td> <input id="btReset" type="Reset" value="Limpar Todos Os Campos" /></td>
        </tr>
    </table>
</asp:Content>

