<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gateway.aspx.cs" Inherits="WebApplication2.View.View2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gateway</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
                    <div>
            Welcome
            <asp:LoginName ID="LoginName" runat="server" Font-Bold="true" />
          
            <br />
            <asp:LoginStatus ID="LoginStatus" runat="server" />
        </div>

            <asp:Button runat="server" OnClick="Spielerverwaltung_Click" Text="Teilnehmerverwaltung" />
            <asp:Button runat="server" OnClick="Mannschaftsverwaltung_Click" Text="Mannschaftsverwaltung" />
            <asp:Button runat="server" OnClick="Turnierverwaltung_Click" ID="Turnierverwaltung" Text="Turnierverwaltung" />
            <asp:Button runat="server" OnClick="Ranking_Click" ID="Ranking" Text="Ranking" />
        </div>
    </form>

    <style>
        .container {
            padding-top: 5rem;
        }

        input[type="submit"] {
            width: 49%;
            margin: 0;
            padding: 0;
            display: inline;
            border: none;
            height: 200px;
            margin-bottom: 5px;
        }
    </style>
</body>
</html>
