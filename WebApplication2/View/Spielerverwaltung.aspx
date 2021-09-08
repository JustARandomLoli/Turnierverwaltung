<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Spielerverwaltung.aspx.cs" Inherits="WebApplication2.View.Spielerverwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Spielerverwaltung</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous" />

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.min.js" integrity="sha384-Atwg2Pkwv9vp0ygtn1JAojH0nYbwNJLPhwyoVbhoPwBhjQPR5VtM2+xf0Uwh9KtT" crossorigin="anonymous"></script>
</head>
<body>
    <div class="container">
        <h1>Teilnehmerverwaltung</h1>

        <form id="form1" runat="server">
            <div>
                <asp:Button runat="server" OnClick="Back_Click" Text="Zurück" CssClass="float-end" />
			    <label><b>Spieler hinzufügen</b></label><br />
                <asp:CheckBoxList ID="TeilnehmerTypenList" runat="server"></asp:CheckBoxList>

			    <label for="txtNachname">Nachname: </label><asp:TextBox ID="txtNachname" runat="server"></asp:TextBox><br />
                <label for="txtVorname">Vorname: </label><asp:TextBox ID="txtVorname" runat="server"></asp:TextBox><br />

                <asp:PlaceHolder runat="server" ID="placeholderEigenschaften"></asp:PlaceHolder>

			    <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnbestaetigen_onclick" />
            </div>
        </form><br/><br/>


        <asp:Table CssClass="table table-striped" ID="tableListe" runat="server">
            <asp:TableHeaderRow ID="headerRow">
                <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nachname</asp:TableHeaderCell>
                <asp:TableHeaderCell>Vorname</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>

    <!-- Button trigger modal -->
    <button id="modalButton" type="button" class="btn btn-primary d-none" data-bs-toggle="modal" data-bs-target="#exampleModal">
      Launch demo modal
    </button>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-body">
              <iframe id="modalFrame" style="height: 70vh;"></iframe>
          </div>
        </div>
      </div>
    </div>


    <script>

        function OpenModal(sid) {
            document.getElementById("modalFrame").src = "/View/Teilnehmer.aspx?sid=" + sid;

            document.getElementById("modalButton").click();
        }

        for (let el of document.querySelectorAll('input[name^="TeilnehmerTypenList"]')) {
            el.onchange = function () {
                for (let el2 of document.querySelectorAll('*[id^="' + el.value + '"]')) {
                    el2.style.display = el.checked ? "block" : "none";
                }
            }
            el.onchange();
        }

    </script>
    <style>
        label {
            padding-right: 1rem;
            margin-bottom: 5px;
        }

        iframe { width: 100%;
        }
    </style>
</body>
</html>
