<%@ Page Title="Quản lý User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin_Users.aspx.cs" Inherits="KhmerFestivalWeb.Admin.Admin_Users" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Quản lý User</h2>

    <asp:GridView ID="gvUsers" runat="server" CssClass="table table-bordered" 
        AutoGenerateColumns="False" DataKeyNames="UserID"
        OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating" 
        OnRowCancelingEdit="gvUsers_RowCancelingEdit" OnRowDeleting="gvUsers_RowDeleting">
        
        <Columns>
            <asp:BoundField DataField="UserID" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="Username" HeaderText="Tài khoản" ReadOnly="True" />
            <asp:BoundField DataField="FullName" HeaderText="Họ tên" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Phone" HeaderText="Số điện thoại" />
            <asp:BoundField DataField="Role" HeaderText="Vai trò" />

            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

    <h3>Thêm User</h3>
    <asp:TextBox ID="txtUsername" runat="server" Placeholder="Tài khoản"></asp:TextBox>
    <asp:TextBox ID="txtFullName" runat="server" Placeholder="Họ tên"></asp:TextBox>
    <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email"></asp:TextBox>
    <asp:TextBox ID="txtPhone" runat="server" Placeholder="Số điện thoại"></asp:TextBox>
    <asp:DropDownList ID="ddlRole" runat="server">
        <asp:ListItem Text="User" Value="User"></asp:ListItem>
        <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="btnAddUser" runat="server" Text="Thêm User" OnClick="btnAddUser_Click" />
</asp:Content>
