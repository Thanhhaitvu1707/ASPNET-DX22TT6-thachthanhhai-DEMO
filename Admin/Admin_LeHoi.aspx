<%@ Page Title="Quản Lý Lễ Hội" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin_LeHoi.aspx.cs" Inherits="KhmerFestivalWeb.Admin.Admin_LeHoi" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="admin-container">
        <h2>Quản Lý Lễ Hội</h2>
        <asp:GridView ID="gvLeHoi" runat="server" CssClass="admin-table" AutoGenerateColumns="False" DataKeyNames="FestivalID"
            OnRowEditing="gvLeHoi_RowEditing" OnRowCancelingEdit="gvLeHoi_RowCancelingEdit"
            OnRowUpdating="gvLeHoi_RowUpdating" OnRowDeleting="gvLeHoi_RowDeleting">
            <Columns>
                <asp:BoundField DataField="FestivalID" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField DataField="FestivalName" HeaderText="Tên Lễ Hội" />
                <asp:BoundField DataField="Location" HeaderText="Địa Điểm" />
                <asp:BoundField DataField="Date" HeaderText="Ngày Tổ Chức" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:CommandField ShowEditButton="true" HeaderText="Sửa" />
                <asp:CommandField ShowDeleteButton="true" HeaderText="Xóa" />
            </Columns>
        </asp:GridView>

        <h3>Thêm Lễ Hội Mới</h3>
        <div>
            <label>Tên Lễ Hội:</label>
            <asp:TextBox ID="txtTenLeHoi" runat="server" CssClass="form-control"></asp:TextBox>
            <label>Địa Điểm:</label>
            <asp:TextBox ID="txtDiaDiem" runat="server" CssClass="form-control"></asp:TextBox>
            <label>Ngày Tổ Chức:</label>
            <asp:TextBox ID="txtNgayToChuc" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            <asp:Button ID="btnThemLeHoi" runat="server" Text="Thêm Lễ Hội" CssClass="btn btn-success mt-2" OnClick="btnThemLeHoi_Click" />
        </div>
    </div>
</asp:Content>
