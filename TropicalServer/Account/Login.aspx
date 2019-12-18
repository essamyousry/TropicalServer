<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TropicalServer.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TropicalServer.UI.Login" %>
<%@ Register Src="~/UserControls/WebUserControl.ascx" TagPrefix="uc1" TagName="LoginControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../AppThemes/TropicalStyles/Login.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:LoginControl ID="LoginControl" runat="server"/>
</asp:Content>

