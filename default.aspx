<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="LastCallServer._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Login() {
            window.open("Login.html", "_blank", "toolbar=yes,scrollbars=no,resizable=yes,top=500,left=500,width=800,height=400");
        }
        function Register() {
            window.open("Registration.aspx", "_blank", "toolbar=yes,scrollbars=no,resizable=yes,top=500,left=500,width=800,height=600");
        }
        function NoAction() {
            alert("No action defined");
        }
        function Offers() {
            window.open("MealOffers.html", "_blank", "toolbar=yes,scrollbars=no,resizable=yes,top=500,left=500,width=800,height=600");
        }
    </script>
</head>
<body>
    <!--<iframe src="https://giphy.com/embed/7p3e2WCM0VEnm" width="480" height="480" frameBorder="0" class="giphy-embed" allowFullScreen></iframe>-->
    <form id="form1" action="">
        <div>
            You have arrived at the LastCall development website.
            <br /><br />
            <button onclick="Login()" style="width: 150px">Sign In</button>
            <br /><br />
            <button onclick="Register()" style="width: 150px">Sign Up</button>
            <br /><br />
            <button onclick="NoAction()" style="width: 150px">Learn More</button>
            <br /><br />
            <button onclick="Offers()" style="width: 150px">Meal Offers</button>
            <br /><br />
        </div>
    </form>
</body>
</html>
