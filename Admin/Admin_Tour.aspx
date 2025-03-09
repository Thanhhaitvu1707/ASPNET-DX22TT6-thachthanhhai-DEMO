<%@ Page Title="Quản Lý Tour" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin_Tour.aspx.cs" Inherits="KhmerFestivalWeb.Admin.Admin_Tour" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Quản lý Tour</h2>
    
    <!-- Bảng hiển thị danh sách Tour -->
    <asp:GridView ID="gvTours" runat="server" AutoGenerateColumns="False" 
        CssClass="table table-bordered" DataKeyNames="TourID" 
        OnRowEditing="gvTours_RowEditing"
        OnRowCancelingEdit="gvTours_RowCancelingEdit"
        OnRowUpdating="gvTours_RowUpdating"
        OnRowDeleting="gvTours_RowDeleting">
        
        <Columns>
            <asp:BoundField DataField="TourID" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="TourName" HeaderText="Tên Tour" />
            <asp:BoundField DataField="Description" HeaderText="Mô tả" />
            <asp:BoundField DataField="Price" HeaderText="Giá" DataFormatString="{0:C}" />
            <asp:BoundField DataField="AvailableSlots" HeaderText="Số chỗ còn" />
            <asp:BoundField DataField="StartDate" HeaderText="Ngày bắt đầu" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="EndDate" HeaderText="Ngày kết thúc" DataFormatString="{0:dd/MM/yyyy}" />

            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Xóa" />
   
        </Columns>
    </asp:GridView>

    <h3>Thêm Tour Mới</h3>
    <table class="table">
        <tr>
            <td>Tên Tour:</td>
            <td><asp:TextBox ID="txtTourName" runat="server" CssClass="form-control" /></td>
        </tr>
        <tr>
            <td>Mô tả:</td>
            <td><asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" /></td>
        </tr>
        <tr>
            <td>Giá:</td>
            <td><asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" /></td>
        </tr>
        <tr>
            <td>Số chỗ còn:</td>
            <td><asp:TextBox ID="txtAvailableSlots" runat="server" CssClass="form-control" /></td>
        </tr>
        <tr>
            <td>Ngày bắt đầu:</td>
            <td><asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date" /></td>
        </tr>
        <tr>
            <td>Ngày kết thúc:</td>
            <td><asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date" /></td>
        </tr>
    </table>
    
    <asp:Button ID="btnAddTour" runat="server" Text="Thêm Tour" CssClass="btn btn-primary" OnClick="btnAddTour_Click" />
</asp:Content>
