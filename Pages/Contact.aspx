<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="KhmerFestivalWeb.Pages.Contact" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/contact.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Contact Us</h2>
    <p>If you have any questions, feel free to reach out to us!</p>

    <div class="row">
        <div class="col-md-6">
            <h4>Contact Information</h4>
            <p><strong>Email:</strong> support@khmerfestival.com</p>
            <p><strong>Phone:</strong> +855 1234 5678</p>
            <p><strong>Address:</strong> Phnom Penh, Cambodia</p>
        </div>

        <div class="col-md-6">
            <h4>Send us a message</h4>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
            
            <div class="mb-3">
                <label>Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label>Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label>Message</label>
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5"></asp:TextBox>
            </div>

            <asp:Button ID="btnSubmit" runat="server" Text="Send Message" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </div>
</asp:Content>
