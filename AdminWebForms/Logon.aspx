<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="AdminWebForms.Logon" UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="StyleSheet/StyleSheetCss.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="form-login">
                <h4>Log In</h4>
                <div class="tb-input">
                    <asp:TextBox type="text" ID="txtUsername" class="form-control input-sm chat-input" placeholder="username" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="  * obavezno polje" ForeColor="Red" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>

                </div>
                <div class="tb-input">
                    <asp:TextBox runat="server" type="password" ID="txtPassword" class="form-control input-sm chat-input" placeholder="password" />
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="  * obavezno polje" ForeColor="Red" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                </div>
                <div class="wrapper">
                    <div class="cb">
                        <asp:CheckBox Text="Remember me" ID="cbRemember" runat="server" AutoPostBack="false" />
                    </div>
                    <br />
                    <asp:Button ID="btnLogin" Text="Log in" runat="server" class="btn btn-primary" OnClick="btnLogin_Click" />
                    <br />
                    <asp:Label Text="" runat="server" ID="lblMsg" ForeColor="Red" />

                </div>
            </div>
        </div>
    </form>
</body>
</html>
