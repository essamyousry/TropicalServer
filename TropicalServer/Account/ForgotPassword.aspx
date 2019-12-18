<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TropicalServer.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="TropicalServer.Account.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../AppThemes/TropicalStyles/PasswordReset.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="container" action="../Account/Login.aspx">
        <div id="BodyDetail">
            <h1 id="Loginlbl">Passsord Reset</h1>
            <div id="loginBox">
                <div id="errorBox">
                    <asp:Label ID="errorlbl" runat="server" CssClass="errorCredentials" Text="Passwords Don't Match" Visible="False"></asp:Label>
                </div>
                <div id="emailConfirm" runat="server">
                    <asp:Label ID="emaillbl" Text="Email:" runat="server"></asp:Label>
                    <asp:TextBox ID="email" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="errorlbl" ControlToValidate="email" errormessage="Password is required" runat="server"></asp:RequiredFieldValidator>
                    <br /><br /><br /><br />
                </div>
                <div id="passwordConfirm" runat="server">
                    <asp:Label ID="useridlbl" Text="Password:" runat="server"></asp:Label>
                    <asp:TextBox ID="password1" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredfield1" CssClass="errorlbl" ControlToValidate="password1" errormessage="Password is required" runat="server"></asp:RequiredFieldValidator>
                    <br />

                    <asp:Label ID="passwordlbl" Text="Confirm Password:" runat="server"></asp:Label>
                    <asp:TextBox ID="password2" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredfield2" CssClass="errorlbl" ControlToValidate="password2" errormessage="Confirm password is required" runat="server"></asp:RequiredFieldValidator>
                    <br />
                </div>

                <asp:Button ID="submitButton" runat="server" Text="Get Reset Link" OnClick="submitButton_Click"/>
            </div>
        </div>
     </form>
</asp:Content>
