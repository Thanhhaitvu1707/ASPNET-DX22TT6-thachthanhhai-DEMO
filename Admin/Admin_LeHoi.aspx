<%@ Page Title="Quản lý Lễ Hội" MasterPageFile="~/Admin/Admin.Master" Language="C#" AutoEventWireup="true" CodeBehind="Admin_LeHoi.aspx.cs" Inherits="KhmerFestivalWeb.Admin.Admin_LeHoi" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" runat="server">
    <div class="container mt-4">

 
        <h2 class="text-center text-primary">Quản Lý Lễ Hội</h2>

      
        <div class="card">
            <div class="card-header bg-primary text-white">Danh sách Lễ Hội</div>
            <div class="card-body">

               
                <asp:GridView ID="gvFestivals" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    DataKeyNames="FestivalID" 
                    OnRowEditing="gvFestivals_RowEditing" 
                    OnRowDeleting="gvFestivals_RowDeleting"
                    OnRowUpdating="gvFestivals_RowUpdating" 
                    OnRowCancelingEdit="gvFestivals_RowCancelingEdit">
                    
                    <Columns>
                 
                        <asp:BoundField DataField="FestivalID" HeaderText="ID" ReadOnly="True" />

                      
                        <asp:BoundField DataField="FestivalName" HeaderText="Tên Lễ Hội" />

                        <asp:BoundField DataField="Location" HeaderText="Địa Điểm" />

                 
                        <asp:BoundField DataField="Date" HeaderText="Ngày Tổ Chức" DataFormatString="{0:yyyy-MM-dd}" />

          
                        <asp:TemplateField HeaderText="Hình ảnh">
                            <ItemTemplate>
                          
                                <asp:Image ID="imgFestival" runat="server" ImageUrl='<%# Eval("ImagePath") %>' Width="80px" />
                               
                                <asp:Label ID="lblImagePath" runat="server" Text='<%# Eval("ImagePath") %>' CssClass="d-block small text-muted"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                
                                <asp:FileUpload ID="fileUploadImage" runat="server" CssClass="form-control" />
                               
                                <asp:HiddenField ID="hfImagePath" runat="server" Value='<%# Eval("ImagePath") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                       
                        <asp:CommandField ShowEditButton="True" EditText="Edit" ControlStyle-CssClass="btn btn-warning btn-sm" />
                        <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>

        <!-- Khung thêm lễ hội mới -->
        <div class="card mt-4">
            <div class="card-header bg-success text-white">Thêm Lễ Hội Mới</div>
            <div class="card-body">

                <!-- TextBox nhập tên lễ hội mới -->
                <asp:TextBox ID="txtFestivalName" runat="server" CssClass="form-control mb-2" placeholder="Tên Lễ Hội"></asp:TextBox>

                <!-- TextBox nhập địa điểm lễ hội mới -->
                <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control mb-2" placeholder="Địa Điểm"></asp:TextBox>

                <!-- TextBox chọn ngày tổ chức lễ hội (dạng Date) -->
                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control mb-2" TextMode="Date"></asp:TextBox>

                <!-- FileUpload chọn hình ảnh lễ hội mới -->
                <asp:FileUpload ID="fileUploadNewImage" runat="server" CssClass="form-control mb-2" />

                <!-- Nút bấm để thêm lễ hội mới -->
                <asp:Button ID="btnAddFestival" runat="server" CssClass="btn btn-success" Text="Thêm Lễ Hội" OnClick="btnAddFestival_Click" />
            </div>
        </div>

    </div>

    <!-- Label hiển thị lỗi (nếu có) -->
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

    </form>
</asp:Content>
