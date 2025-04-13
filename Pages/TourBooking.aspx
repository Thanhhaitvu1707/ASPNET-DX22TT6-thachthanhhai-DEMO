<%@ Page Title="Đặt Tour" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TourBooking.aspx.cs" Inherits="KhmerFestivalWeb.Pages.TourBooking" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center fw-bold mt-3">Đặt Tour Tham Quan</h2>

    <div class="mb-3">
        <label>Chọn Tour:</label>
        <asp:DropDownList ID="ddlTours" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTours_SelectedIndexChanged"></asp:DropDownList>
    </div>

    <div class="mb-3">
        <label>Giá tiền:</label>
        <asp:Label ID="lblPrice" runat="server" CssClass="text-success fw-bold"></asp:Label>
    </div>

    <div class="mb-3">
        <label>Số chỗ còn lại:</label>
        <asp:Label ID="lblAvailableSlots" runat="server" CssClass="text-danger fw-bold"></asp:Label>
    </div>

    <div class="mb-3">
        <label>Số lượng chỗ muốn đặt:</label>
        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" TextMode="Number" Min="1"></asp:TextBox>
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

    <div class="mb-3">
        <label>Ghi chú:</label>
        <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
    </div>

    <asp:Button ID="btnBook" runat="server" Text="Đặt Tour" CssClass="btn btn-primary mt-3" OnClick="btnBook_Click" />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" CssClass="d-block mt-2"></asp:Label>
</asp:Content>
