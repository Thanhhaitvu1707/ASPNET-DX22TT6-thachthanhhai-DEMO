using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace KhmerFestivalWeb.Pages
{
    public partial class TourBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTourList();
            }
        }

        private void LoadTourList()
        {
            string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT DISTINCT TourID, TourName FROM Tours ORDER BY TourName"; // Thêm DISTINCT
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                ddlTours.DataSource = reader;
                ddlTours.DataTextField = "TourName"; // Hiển thị tên tour
                ddlTours.DataValueField = "TourID"; // Lưu ID của tour
                ddlTours.DataBind();

                reader.Close();
            }

            // Thêm mục chọn mặc định
            ddlTours.Items.Insert(0, new ListItem("-- Chọn Tour --", "0"));
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblMessage.Text = "Bạn cần đăng nhập để đặt tour!";
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);
            int tourId = Convert.ToInt32(ddlTours.SelectedValue);
            string notes = txtNotes.Text.Trim();
            int quantity = int.Parse(txtQuantity.Text);



            string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Kiểm tra số lượng chỗ trống
                string checkSlotsQuery = "SELECT AvailableSlots FROM Tours WHERE TourID = @TourID";
                SqlCommand checkSlotsCmd = new SqlCommand(checkSlotsQuery, conn);
                checkSlotsCmd.Parameters.AddWithValue("@TourID", tourId);
                int availableSlots = Convert.ToInt32(checkSlotsCmd.ExecuteScalar());

                if (availableSlots <= 0)
                {
                    lblMessage.Text = "Tour này đã hết chỗ!";
                    return;
                }

                // Chèn dữ liệu vào bảng Bookings (Thêm cột Notes)
                string insertQuery = "INSERT INTO Bookings (UserID, TourID, Quantity, BookingDate, Status, Notes) VALUES (@UserID, @TourID, @Quantity, GETDATE(), 'Pending', @Notes)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@UserID", userId);
                insertCmd.Parameters.AddWithValue("@TourID", tourId);
                insertCmd.Parameters.AddWithValue("@Quantity", quantity);
                insertCmd.Parameters.AddWithValue("@Notes", notes);
                insertCmd.ExecuteNonQuery();

                // Cập nhật số lượng chỗ trống
                string updateQuery = "UPDATE Tours SET AvailableSlots = AvailableSlots - 1 WHERE TourID = @TourID";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@TourID", tourId);
                updateCmd.ExecuteNonQuery();

                lblMessage.Text = "Đặt tour thành công!";
            }
        }
        protected void ddlTours_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tourID = Convert.ToInt32(ddlTours.SelectedValue);

            string query = "SELECT Price, AvailableSlots FROM Tours WHERE TourID = @TourID";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TourID", tourID);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblPrice.Text = reader["Price"].ToString() + " VND";  // Hiển thị giá tiền
                        lblAvailableSlots.Text = reader["AvailableSlots"].ToString(); // Hiển thị số chỗ còn lại
                    }
                    reader.Close();
                }
            }
        }


    }
}
