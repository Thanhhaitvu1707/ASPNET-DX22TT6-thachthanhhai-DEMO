<%@ Page Title="Đặt Tour" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TourBooking.aspx.cs" Inherits="KhmerFestivalWeb.Pages.TourBooking" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Đặt Tour Tham Quan</h2>

    <div class="mb-3">
        <label>Chọn Tour:</label>
        <asp:DropDownList ID="ddlTours" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>

    <div class="mb-3">
        <label>Họ và Tên:</label>
        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label>Email:</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label>Số điện thoại:</label>
        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <asp:Button ID="btnBook" runat="server" Text="Đặt Tour" CssClass="btn btn-primary" OnClick="btnBook_Click" />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
</asp:Content>
