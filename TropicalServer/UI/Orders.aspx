<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TropicalServer.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="TropicalServer.UI.Orders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../AppThemes/TropicalStyles/Orders.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="CriteriaBar">
            <div class="Criteria">
                <label class="label">Order Date: </label>
                <asp:DropDownList ID="criteria1" CssClass="Input" runat="server" OnSelectedIndexChanged="OrderDate_SelectedIndexChanged" AutoPostBack="True">
                     <asp:ListItem Value="Today" Selected="true"> Today </asp:ListItem>
                     <asp:ListItem Value="Last 7 Days"> Last 7 Days </asp:ListItem>
                     <asp:ListItem Value="Last 1 Month"> Last 1 Month </asp:ListItem>
                     <asp:ListItem Value="Last 6 Months"> Last 6 Months </asp:ListItem>
                </asp:DropDownList>
            </div>
        <div class="Criteria">
            <label class="label">CustomerID: </label>     
            <asp:TextBox ID="customerid" OnTextChanged="DisplaySelectedCustomerID" CssClass="Input" runat="server" AutoPostBack="true" ></asp:TextBox>
            <div class="row">  
                <div id="ajaxcontroltoolkitplaceholder">  
                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="autoComplete1" TargetControlID="customerid" ServiceMethod="GetByCustomerID" MinimumPrefixLength="3" ShowOnlyCurrentWordInCompletionListItem="true"></ajaxToolkit:AutoCompleteExtender>
                </div>  
            </div>        
        </div>

        <div class="Criteria">
            <label class="label">Customer Name: </label>
            <asp:TextBox ID="customername" OnTextChanged="DisplaySelectedCustomerName" CssClass="Input" runat="server" AutoPostBack="true"></asp:TextBox>
            <div class="row">  
                <div id="ajaxcontroltoolkitplaceholder2">  
                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="AutoCompleteExtender2" TargetControlID="customername" ServiceMethod="GetByCustomerName" MinimumPrefixLength="3" ShowOnlyCurrentWordInCompletionListItem="true"></ajaxToolkit:AutoCompleteExtender>
                </div>  
            </div>
        </div>
        <div class="Criteria">
            <label class="label">Sales Manager: </label>
            <asp:DropDownList ID="criteria2" CssClass="Input" runat="server"></asp:DropDownList>
        </div>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="grid" autogeneratecolumns="false" CssClass="gvOrders" OnRowCancelingEdit="CancelEdit" OnSelectedIndexChanging="ViewDetails" OnRowUpdating="UpdateEdit" OnRowEditing="EditDetails" OnRowDeleting="DeleteDetails" runat="server" AlternatingRowStyle-CssClass="AltRow">
                <Columns>
                      <asp:BoundField DataField="OrderTrackingNumber" headertext="Tracking #"/>
                      <asp:BoundField DataField="OrderDate" headertext="Order Date"/>
                      <asp:BoundField DataField="OrderCustomerNumber" headertext="Customer ID"/>
                      <asp:BoundField DataField="CustName" headertext="Customer Name"/>
                      <asp:BoundField DataField="CustOfficeAddress1" headertext="Address"/>
                      <asp:BoundField DataField="OrderRouteNumber" headertext="Route #"/>
                      <asp:CommandField HeaderText="Available Actions" ShowEditButton="true" ShowDeleteButton="true" ShowSelectButton="true" SelectText="View"/> 
                </Columns>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="customerid" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="customername" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
    
          
</asp:Content>
