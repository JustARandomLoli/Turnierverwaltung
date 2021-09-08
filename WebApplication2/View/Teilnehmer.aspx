<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teilnehmer.aspx.cs" Inherits="WebApplication2.View.Teilnehmer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Teilnehmer</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous" />

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.min.js" integrity="sha384-Atwg2Pkwv9vp0ygtn1JAojH0nYbwNJLPhwyoVbhoPwBhjQPR5VtM2+xf0Uwh9KtT" crossorigin="anonymous"></script>
    
</head>
<body>
    <h1><asp:Label runat="server" ID="title"></asp:Label></h1>

    <form runat="server">

        <asp:BulletedList runat="server" ID="dataList"></asp:BulletedList>

        <div>
            <asp:CheckBoxList ID="TeilnehmerTypenList" runat="server"></asp:CheckBoxList>
            <asp:PlaceHolder ID="edit" runat="server"></asp:PlaceHolder>
        </div>
        
        <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="Edit" />
        <asp:Button ID="DeleteBtn" runat="server" OnClick="DeleteBtn_Click" Text="Delete" />
    </form>
    
    <script>

        for (let el of document.querySelectorAll('input[name^="TeilnehmerTypenList"]')) {
            el.onchange = function () {
                for (let el2 of document.querySelectorAll('*[id^="' + el.value + '"]')) {
                    el2.style.display = el.checked ? "block" : "none";
                }
            }
            el.onchange();
        }

    </script>

</body>
</html>
