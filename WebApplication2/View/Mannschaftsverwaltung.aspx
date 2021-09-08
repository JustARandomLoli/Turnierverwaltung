<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mannschaftsverwaltung.aspx.cs" Inherits="WebApplication2.View.Mannschaftsverwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Mannschaftsverwaltung</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous" />

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.min.js" integrity="sha384-Atwg2Pkwv9vp0ygtn1JAojH0nYbwNJLPhwyoVbhoPwBhjQPR5VtM2+xf0Uwh9KtT" crossorigin="anonymous"></script>
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
            </div><br/><br/>
        <asp:Table CssClass="table table-striped" ID="tableListe" runat="server">
            <asp:TableHeaderRow ID="headerRow">
                <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nachname</asp:TableHeaderCell>
                <asp:TableHeaderCell>Vorname</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        </form>
    </div>


    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-body">
              <iframe id="modalFrame"></iframe>
          </div>
        </div>
      </div>
    </div>


    <script>

        function OpenModal(sid) {
            document.getElementById("modalFrame").src = "/View/Teilnehmer.aspx?sid=" + sid;

            document.getElementById("modalButton").click();
        }
    </script>
    <style>
        label {
            padding-right: 1rem;
            margin-bottom: 5px;
        }
    </style>
</body>
</html>
