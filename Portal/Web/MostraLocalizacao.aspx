<%@ Page language="c#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="MostraLocalizacao" CodeFile="MostraLocalizacao.aspx.cs" Title="WEBooks [Localizacao de Clientes]" %>
<%@ Register TagPrefix="wcp" Namespace="WCPierce.Web.UI.WebControls" Assembly="WCPierce.Web" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentHolder" runat="server">

        <script type="text/javascript">
          function GMarker_Click()
          {
            var html = "<b>" + this.id + "</b>";
            this.openInfoWindowHtml(html);
          }
        </script>
  

    <h1>Localizacao dos Clientes</h1>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContentHolder" Runat="Server">
   
        <div id="DivUserInexistente" runat="server"></div>   
   
        <wcp:GMap runat="server" id="gMap" Width="1000px" Height="600px" />

</asp:Content>