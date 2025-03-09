<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="KhmerFestivalWeb.Admin.AdminDashboard" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../Content/admin.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="admin-container">
        <h2>Quản Trị Website</h2>
        <div class="admin-menu">
            <asp:LinkButton ID="btnLeHoi" runat="server" CssClass="btn btn-primary" OnClick="btnLeHoi_Click">Quản lý Lễ Hội</asp:LinkButton>
            <asp:LinkButton ID="btnTour" runat="server" CssClass="btn btn-success" OnClick="btnTour_Click">Quản lý Tour</asp:LinkButton>
            <asp:LinkButton ID="btnUsers" runat="server" CssClass="btn btn-warning" OnClick="btnUsers_Click">Quản lý Người Dùng</asp:LinkButton>
            <asp:LinkButton ID="btnBookings" runat="server" CssClass="btn btn-danger" OnClick="btnBookings_Click">Quản lý Đơn Đặt Tour</asp:LinkButton>


        </div>
    </div>
</asp:Content>
