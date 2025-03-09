<%@ Page Title="Đăng Nhập" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KhmerFestivalWeb.Pages.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center mt-5">
            <div class="col-md-5">
                <div class="card shadow-lg p-4">
                    <h3 class="text-center text-primary">Đăng Nhập</h3>

                    
                        <div class="mb-3">
                            <label class="form-label">Tên đăng nhập</label>
                         <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                                ControlToValidate="txtUsername" 
                                ErrorMessage="Tên đăng nhập không được để trống" 
                                CssClass="text-danger" 
                                 Display="Dynamic">
                         </asp:RequiredFieldValidator>

                        </div>

                        <div class="mb-3">
                            <label class="form-label">Mật khẩu</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                             ErrorMessage="Vui lòng nhập mật khẩu" CssClass="text-danger" Display="Dynamic" />

                        </div>

                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary w-100" Text="Đăng nhập" OnClick="btnLogin_Click" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>

                        <div class="text-center mt-3">
                            <a href="Register.aspx" class="btn btn-outline-secondary">Chưa có tài khoản? Đăng ký</a>
                        </div>
                   
                </div>
            </div>
        </div>
    </div>
</asp:Content>
