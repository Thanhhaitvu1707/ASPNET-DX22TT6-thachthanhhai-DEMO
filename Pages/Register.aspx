<%@ Page Title="Đăng Kí" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="KhmerFestivalWeb.Pages.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center mt-5">
            <div class="col-md-6">
                <div class="card shadow-lg p-4">
                    <h3 class="text-center text-primary">Đăng Ký</h3>

                    <div class="mb-3">
                        <label class="form-label">Tên đăng nhập</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" ></asp:TextBox>
                           <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                           ControlToValidate="txtUsername" 
                           ErrorMessage="Tên đăng nhập không được để trống" 
                           CssClass="text-danger" 
                            Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Mật khẩu</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
  ErrorMessage="Vui lòng nhập mật khẩu" CssClass="text-danger" Display="Dynamic" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Email</label>
                       <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
    ErrorMessage="Vui lòng nhập email" CssClass="text-danger" Display="Dynamic" />


                    </div>

                    <div class="mb-3">
                        <label class="form-label">Số điện thoại</label>
                      <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
   <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Số điện thoại không hợp lệ" CssClass="text-danger" Display="Dynamic" ValidationExpression="^0\d{9,10}$" />

                    </div>

                    <div class="mb-3">
                        <label class="form-label">Họ và Tên</label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:RegularExpressionValidator ErrorMessage="Vui lòng điền đầy đủ thông tin" ControlToValidate="txtFullName" runat="server" />
                        </div>

                    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-primary w-100" Text="Đăng ký" OnClick="btnRegister_Click" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-3 d-block"></asp:Label>

                    <div class="text-center mt-3">
                     <a href="Login.aspx" class="btn btn-outline-success">Đăng nhập ngay</a>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
