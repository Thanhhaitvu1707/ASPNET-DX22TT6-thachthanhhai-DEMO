<%@ Page Title="Quản lý Tour" MasterPageFile="~/Admin/Admin.Master" Language="C#" AutoEventWireup="true" CodeBehind="Admin_Tour.aspx.cs" Inherits="KhmerFestivalWeb.Admin.Admin_Tour" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    <form id="form1" runat="server">
    

    <h2>Quản lý Tour</h2>
    
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
            
        
            <asp:BoundField DataField="Price" HeaderText="Giá" DataFormatString="{0:N0} đ" HtmlEncode="false" />
      
            <asp:BoundField DataField="AvailableSlots" HeaderText="Số chỗ còn" />
            
        
            <asp:BoundField DataField="StartDate" HeaderText="Ngày bắt đầu" DataFormatString="{0:dd/MM/yyyy}" />
         
            <asp:BoundField DataField="EndDate" HeaderText="Ngày kết thúc" DataFormatString="{0:dd/MM/yyyy}" />
        
            <asp:TemplateField HeaderText="Hình Ảnh">
                <ItemTemplate>
              
                    <asp:Image ID="imgTour" runat="server" ImageUrl='<%# Eval("TourImage") %>' Width="80px" />
             
                    <asp:Label ID="lblTourImagePath" runat="server" Text='<%# Eval("TourImage") %>' CssClass="d-block small text-muted"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
              
                    <asp:FileUpload ID="fileUploadTourImage" runat="server" CssClass="form-control" />
                  
                    <asp:HiddenField ID="hfTourImage" runat="server" Value='<%# Eval("TourImage") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

        
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />

        
            <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Xóa" />
        </Columns>

    </asp:GridView>

 
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>

 
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

    </form>
</asp:Content>
