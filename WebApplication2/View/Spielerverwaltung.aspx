<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Spielerverwaltung.aspx.cs" Inherits="WebApplication2.View.Spielerverwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Spielerverwaltung</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous" />
</head>
<body>
    <div class="container">
        <h1>Spielerverwaltung</h1>

        <form id="form1" runat="server">
            <div>
                <asp:Button runat="server" OnClick="Back_Click" Text="Zurück" CssClass="float-end" />
			    <label><b>Spieler hinzufügen</b></label><br />
                <label for="typeFootball">Fußballspieler</label><asp:CheckBox ID="typeFootball" runat="server" Value="Fussballspieler" /><br />
                <label for="typeVolleyball">Volleyballspieler</label><asp:CheckBox ID="typeVolleyball" runat="server" Value="Volleyballspieler" /><br />

			    <label for="txtNachname">Nachname: </label><asp:TextBox ID="txtNachname" runat="server"></asp:TextBox><br />
                <label for="txtVorname">Vorname: </label><asp:TextBox ID="txtVorname" runat="server"></asp:TextBox><br />

                <div id="divFootball">
                    <label for="numTore">Tore: </label><asp:TextBox ID="numTore" type="number" runat="server"></asp:TextBox>
                </div>

                <div id="divVolleyball">
                    <label for="numPunkte">Punkte: </label><asp:TextBox ID="numPunkte" type="number" runat="server"></asp:TextBox>
                </div>

			    <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnbestaetigen_onclick" />
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
        const tf = document.getElementById("typeFootball");
        const tv = document.getElementById("typeVolleyball");

        const football = document.getElementById("divFootball");
        const volleyball = document.getElementById("divVolleyball");

        tf.onchange = () => {
            football.style.display = tf.checked ? "block" : "none";
        };
        tf.onchange();

        tv.onchange = () => {
            volleyball.style.display = tv.checked ? "block" : "none";
        };
        tv.onchange();
    </script>
    <style>
        label {
            padding-right: 1rem;
            margin-bottom: 5px;
        }
    </style>
</body>
</html>
