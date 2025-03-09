<%@ Page Title="Festivals" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Festivals.aspx.cs" Inherits="KhmerFestivalWeb.Pages.Festivals" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Danh Sách Lễ Hội</h2>

    <div class="mb-3">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Nhập tên lễ hội..." />
        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary mt-2" OnClick="btnSearch_Click" />
    </div>

    <asp:GridView ID="gvFestivals" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="true" PageSize="5"
        OnPageIndexChanging="gvFestivals_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="FestivalName" HeaderText="Tên Lễ Hội" />
            <asp:BoundField DataField="Location" HeaderText="Địa Điểm" />
            <asp:BoundField DataField="Date" HeaderText="Ngày Tổ Chức" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:TemplateField HeaderText="Chi Tiết">
              <ItemTemplate>
                <asp:HyperLink runat="server" 
                     NavigateUrl='<%# "ChiTietLeHoi.aspx?ID=" + Eval("FestivalID") %>' 
                      Text="Xem Thêm" CssClass="btn btn-primary btn-sm"></asp:HyperLink>

            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
