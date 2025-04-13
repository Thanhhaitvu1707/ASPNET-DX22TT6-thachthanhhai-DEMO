<%@ Page Title="Chi Tiết Lễ Hội" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChiTietLeHoi.aspx.cs" Inherits="KhmerFestivalWeb.Pages.ChiTietLeHoi" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/ChiTietLeHoi.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Chi Tiết Lễ Hội</h2>

    <div class="festival-detail">
        <asp:Image ID="imgFestival" runat="server" CssClass="festival-image" />
        <h3><asp:Label ID="lblFestivalName" runat="server" /></h3>
        <p><strong>Địa điểm:</strong> <asp:Label ID="lblLocation" runat="server" /></p>
        <p><strong>Ngày tổ chức:</strong> <asp:Label ID="lblDate" runat="server" /></p>
        <p><strong>Mô tả:</strong></p>
        <p><asp:Label ID="lblDescription" runat="server" /></p>
    </div>

        <hr />
        <div class="festival-content">
            <asp:Literal ID="litContent" runat="server" Mode="PassThrough" />
        </div>
    <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
    <a href="Festivals.aspx" class="btn btn-secondary">Quay lại danh sách</a>
</asp:Content>
