<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Turnierverwaltung.aspx.cs" Inherits="WebApplication2.View.Turnierverwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Turnierverwaltung</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous" />

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.min.js" integrity="sha384-Atwg2Pkwv9vp0ygtn1JAojH0nYbwNJLPhwyoVbhoPwBhjQPR5VtM2+xf0Uwh9KtT" crossorigin="anonymous"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <asp:Button runat="server" OnClick="Back_Click" Text="Zurück" CssClass="float-end" />
                <h1>Turnierverwaltung</h1>

            <asp:Label runat="server" ID="labb"></asp:Label>
        </div>

        <div class="container">
            <label>Turnier erstellen</label>
            <label for="name">Name: </label>
            <asp:TextBox ID="name" runat="server" CssClass="mb-3" ></asp:TextBox><br />
            <label for="spielart">Spielart: </label>
            <asp:DropDownList ID="spielart" runat="server" CssClass="mb-3"></asp:DropDownList><br />
            <asp:Button Text="Turnier erstellen" runat="server" OnClick="Unnamed_Click" />
        </div>

        <div class="container">
            <asp:DropDownList CssClass="my-3" ID="turniere" AutoPostBack="true" runat="server" OnSelectedIndexChanged="turniere_SelectedIndexChanged"></asp:DropDownList><br />

            <label>Spiel hinzufügen</label><br />
            <label for="mannschaft1">Mannschaft1: </label>
            <asp:DropDownList ID="mannschaft1" runat="server" CssClass="mb-3"></asp:DropDownList>
            <label> Punktestand: </label><asp:TextBox ID="punke1" runat="server"></asp:TextBox>
                <br />
            <label for="mannschaft2">Mannschaft2: </label>
            <asp:DropDownList ID="mannschaft2" runat="server" CssClass="mb-3"></asp:DropDownList>
           <label> Punktestand: </label><asp:TextBox ID="punkte2" runat="server"></asp:TextBox>
<br />
            <asp:Button Text="Spiel hinzufügen" ID="addspiel" runat="server" OnClick="addspiel_Click" />
        </div>

        <div class="container">

            <asp:Table runat="server" ID="spiele" CssClass="table table-striped mt-3">
                <asp:TableHeaderRow>
                    <asp:TableCell>Spiel</asp:TableCell>
                    <asp:TableCell>Punkte</asp:TableCell>
                </asp:TableHeaderRow>
            </asp:Table>

        </div>

    </form>

    <style>
        .table, .table * {
            text-align: center;
        }
    </style>
</body>
</html>
