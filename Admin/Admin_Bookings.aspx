<%@ Page Title="Quản lý Đặt Chỗ" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Admin_Bookings.aspx.cs" Inherits="KhmerFestivalWeb.Admin.Admin_Bookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" runat="server">
    <h2 class="title">Quản lý Đặt Chỗ</h2>

 
    <div class="filter-section">
        <label for="ddlFilter">Lọc theo trạng thái:</label>
        
        <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
            <asp:ListItem Value="All">Tất cả</asp:ListItem>
            <asp:ListItem Value="Pending">Chờ xử lý</asp:ListItem>
            <asp:ListItem Value="Confirmed">Đã xác nhận</asp:ListItem>
            <asp:ListItem Value="Canceled">Đã hủy</asp:ListItem>
        </asp:DropDownList>
    </div>

   
    <asp:GridView ID="gvBookings" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
        DataKeyNames="BookingID" 
        OnRowEditing="gvBookings_RowEditing" 
        OnRowCancelingEdit="gvBookings_RowCancelingEdit" 
        OnRowUpdating="gvBookings_RowUpdating"
        OnRowDeleting="gvBookings_RowDeleting">
        
        <Columns>
 
            <asp:BoundField DataField="BookingID" HeaderText="Mã Đặt Chỗ" ReadOnly="true" />
            
     
            <asp:BoundField DataField="FullName" HeaderText="Khách Hàng" />
            
         
            <asp:BoundField DataField="TourName" HeaderText="Tour" />
            
         
            <asp:BoundField DataField="BookingDate" HeaderText="Ngày Đặt" DataFormatString="{0:dd/MM/yyyy}" />
            
          
            <asp:BoundField DataField="Quantity" HeaderText="Số Lượng" /> 

       
            <asp:TemplateField HeaderText="Trạng Thái">
                <ItemTemplate>
              
                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="Pending">Chờ xử lý</asp:ListItem>
                        <asp:ListItem Value="Confirmed">Đã xác nhận</asp:ListItem>
                        <asp:ListItem Value="Canceled">Đã hủy</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Thanh Toán">
                <ItemTemplate>
                 
                    <asp:Label ID="lblPaymentStatus" runat="server" Text='<%# Eval("PaymentStatus") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                 
                    <asp:DropDownList ID="ddlPayment" runat="server">
                        <asp:ListItem Value="Pending">Chưa thanh toán</asp:ListItem>
                        <asp:ListItem Value="Completed">Đã thanh toán</asp:ListItem>
                        <asp:ListItem Value="Failed">Thanh toán thất bại</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

          
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" ButtonType="Button" />
        </Columns>
    </asp:GridView>
    </form>
</asp:Content>
