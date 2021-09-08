<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ranking.aspx.cs" Inherits="WebApplication2.View.Ranking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Ranking</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.min.js" integrity="sha384-Atwg2Pkwv9vp0ygtn1JAojH0nYbwNJLPhwyoVbhoPwBhjQPR5VtM2+xf0Uwh9KtT" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Ranking</h1>
            <asp:Table CssClass="table table-striped" ID="tableListe" runat="server">
                <asp:TableHeaderRow ID="headerRow">
                    <asp:TableHeaderCell>Turnier</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Mannschaft</asp:TableHeaderCell>
                    <asp:TableHeaderCell>PD</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Punkte</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
