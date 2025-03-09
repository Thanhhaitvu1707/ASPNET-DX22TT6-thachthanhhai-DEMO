<%@ Page Title="Quản lý Đặt Tour" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin_Bookings.aspx.cs" Inherits="KhmerFestivalWeb.Admin.Admin_Bookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Quản lý Đặt Tour</h2>
    
    <asp:GridView ID="gvBookings" runat="server" AutoGenerateColumns="False"
        DataKeyNames="BookingID" OnRowEditing="gvBookings_RowEditing"
        OnRowCancelingEdit="gvBookings_RowCancelingEdit"
        OnRowUpdating="gvBookings_RowUpdating"
        OnRowDeleting="gvBookings_RowDeleting">
        <Columns>
            <asp:BoundField DataField="BookingID" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="Username" HeaderText="Khách hàng" />
            <asp:BoundField DataField="TourName" HeaderText="Tour" />
            <asp:BoundField DataField="BookingDate" HeaderText="Ngày đặt" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:TemplateField HeaderText="Trạng thái">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                        <asp:ListItem Text="Confirmed" Value="Confirmed"></asp:ListItem>
                        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>
