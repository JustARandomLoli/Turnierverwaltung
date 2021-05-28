<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mannschaftsverwaltung.aspx.cs" Inherits="WebApplication2.View.Mannschaftsverwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Mannschaftsverwaltung</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous" />
</head>
<body>
    <div class="container">
        <h1>Mannschaftsverwaltung</h1>

        <form id="form1" runat="server">
            <div>
                <asp:Button runat="server" OnClick="Back_Click" Text="Zurück" CssClass="float-end" />
                <label><b>Mannschaft hinzufügen</b></label><br />
                
			    <label for="txtName">Name: </label><asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />

			    <asp:Button ID="Button1" runat="server" Text="OK" OnClick="Button1_Click" /><br /><br />

			    <label><b>Mannschaft verwalten</b></label><br />
                <asp:DropDownList AutoPostBack="True" OnSelectedIndexChanged="listMannschaften_SelectedIndexChanged" id="listMannschaften" runat="server">
                </asp:DropDownList>
                <label>+</label>
                <asp:DropDownList id="listTeilnehmer" runat="server">
                </asp:DropDownList>
                

			    <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOk_Click" />
            </div>
        </form><br/><br/>
        <asp:Table CssClass="table table-striped" ID="tableListe" runat="server">
            <asp:TableHeaderRow ID="headerRow">
                <asp:TableHeaderCell>#</asp:TableHeaderCell>
                <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nachname</asp:TableHeaderCell>
                <asp:TableHeaderCell>Vorname</asp:TableHeaderCell>
                <asp:TableHeaderCell>Tore</asp:TableHeaderCell>
                <asp:TableHeaderCell>Punkte</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>

    <script>

    </script>
    <style>
        label {
            padding-right: 1rem;
            margin-bottom: 5px;
        }
    </style>
</body>
</html>
