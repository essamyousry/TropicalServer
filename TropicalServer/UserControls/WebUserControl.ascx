<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl.ascx.cs" Inherits="TropicalServer.UserControls.WebUserControl" %>
    <link rel="stylesheet" href="../AppThemes/TropicalStyles/Login.css"/>
    <form id="container" action="../UI/Products.aspx">
        <div id="BodyDetail">
            <h1 id="Loginlbl">Mobile Customer Order Tracking</h1>
            <div id="loginBox">
                <div id="errorBox">
                    <asp:Label ID="errorlbl" runat="server" CssClass="errorCredentials" Text="Username/Password incorrect" Visible="False"></asp:Label>
                </div>
                <asp:Label ID="useridlbl" Text="Login:" runat="server"></asp:Label>
                <asp:TextBox ID="useridtextbox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredfield1" CssClass="errorlbl" ControlToValidate="useridtextbox" errormessage="Userid is required" runat="server"></asp:RequiredFieldValidator>
                <br />

                <asp:Label ID="passwordlbl" Text="Password:" runat="server"></asp:Label>
                <asp:TextBox ID="passwordtextbox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredfield2" CssClass="errorlbl" ControlToValidate="passwordtextbox" errormessage="Password is required" runat="server"></asp:RequiredFieldValidator>
                <br />
                
                <asp:CheckBox ID="rememberCheckbox" Text="Remember my ID" runat="server" Checked="false"/>
                <asp:Button ID="loginButton" runat="server" Text="Log-In" />
            </div>
            <div id="forgot">
                <asp:LinkButton ID="forgotUsername" runat="server" Text="Forgot Username"></asp:LinkButton>
                <asp:LinkButton ID="forgotPassword" runat="server" Text="Forgot Password" OnClick="forgotPassword_Click" CausesValidation="false"></asp:LinkButton>
            </div>
        </div>
    </form>