<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="LastCallServer.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var prefs = "";
        function preference(o, i) {
            var s = document.getElementById("foodprefs");
            s.value = s.value.replace(i + ";", "");
            if (o.checked)
                s.value += i + ";";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="foodprefs" ClientIDMode="Static" runat="server" Value="" />
    <div>
        <asp:Label ID="ErrorMessage" runat="server" Text="" ForeColor="Red"/><br />
        Email address: <asp:TextBox ID="email" runat="server"></asp:TextBox><br /><br />
        Friendly name: <asp:TextBox ID="friendlyname" runat="server"></asp:TextBox><br /><br />
        Password: <asp:TextBox ID="password" runat="server"></asp:TextBox><br /><br />
        Confirm password: <asp:TextBox ID="confirmpassword" runat="server"></asp:TextBox><br /><br />
        Telephone: <asp:TextBox ID="telephone" runat="server"></asp:TextBox><br /><br />
        Address: <asp:TextBox ID="address" runat="server"></asp:TextBox><br /><br />
        Notify me by email: <asp:CheckBox ID="emailoffers" runat="server" Checked="false" /><br /><br />
        Notify me by text message: <asp:CheckBox ID="textoffers" runat="server" Checked="false" /><br /><br />
        Add me to your mailing list: <asp:CheckBox ID="mailinglist" runat="server" Checked="false" /><br /><br />
        Food preferences:<br /><br />
        <asp:PlaceHolder ID="preferences" runat="server" /><br /><br />
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
    </div>
    </form>
</body>
</html>
