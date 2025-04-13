<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KhmerFestivalWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Liên kết file CSS -->
    <link rel="stylesheet" href="Content/Default.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Slider hình ảnh -->
    <div class="slider-container">
        <div class="slider">
            <img src="Images/ok_om_bok.png" class="active" alt="Lễ hội Ok Om Bok">
            <img src="Images/chol_chnam_thmay.png" alt="Lễ hội Chol Chnam Thmay">
            <img src="Images/sendolta.png" alt="Lễ hội Đôn Ta">
            <img src="Images/kathina2.png" alt="Lễ hội Chol Chnam Thmay">
            <img src="Images/damtu.png" alt="Lễ Tu trưởng thành">
            <img src="Images/kathina1.png" alt="Lễ hội Kathina">
        </div>
        <div class="overlay-text">
            <h1>Khám Phá Lễ Hội Khmer</h1>
            <br />
              <h2>Văn Hóa & Truyền Thống Khmer</h2>
                 <p>Trải nghiệm những lễ hội đặc sắc của người Khmer, mang đậm bản sắc dân tộc.</p>
        </div>
        <!-- Vòng tròn chỉ số -->
        <div class="slider-indicators"></div>
    </div>

    <!-- Phần giới thiệu lễ hội -->
    <section class="about">
        <div class="about-overlay">
            <h2>Văn Hóa & Truyền Thống Khmer</h2>
            <p>Trải nghiệm những lễ hội đặc sắc của người Khmer, mang đậm bản sắc dân tộc.</p>
        </div>
    </section>

  
    <!-- Danh sách các tour -->
    <section class="tour-list">
    <h2>Danh sách các Tour</h2>
    <div class="tour-container">
        <asp:Repeater ID="rptTourList" runat="server">
            <ItemTemplate>
                <div class="tour-item">
                    <img src='<%# Eval("TourImage") %>' alt='<%# Eval("TourName") %>' />
                    <h3><%# Eval("TourName") %></h3>
                    <p>Giá: <%# Eval("Price", "{0:N0}") %> VNĐ</p>
                    <p>Thời gian: <%# Eval("StartDate", "{0:dd/MM/yyyy}") %> - <%# Eval("EndDate", "{0:dd/MM/yyyy}") %></p>
                    <a href="/Pages/TourBooking.aspx?TourID=<%# Eval("TourID") %>" class="btnbook">Đặt Ngay</a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </section>
    <section class="festival-list">
    <h2>Bài viết lễ hội Khmer</h2>
<asp:Repeater ID="rptFestivals" runat="server">
    <ItemTemplate>
        <div class="festival-item">
            <h3><a href='/Pages/ChiTietLeHoi.aspx?ID=<%# Eval("FestivalID") %>'><%# Eval("FestivalName") %></a></h3>
            <p><%# Eval("Content").ToString().Substring(0, Math.Min(150, Eval("Content").ToString().Length)) %>...</p>
        </div>
    </ItemTemplate>
</asp:Repeater>
        <</section>



    <script src="Scripts/default.js"></script>
</asp:Content>