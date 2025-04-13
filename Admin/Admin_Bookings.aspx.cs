using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace KhmerFestivalWeb.Admin
{
    public partial class Admin_Bookings : System.Web.UI.Page
    {
        // Chuỗi kết nối đến database, lấy từ Web.config
        string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Chỉ tải dữ liệu 1 lần, không tải lại khi postback
            {
                LoadBookings();
            }
        }

        /// <summary>
        /// Hàm tải danh sách đặt chỗ từ database, có thể lọc theo trạng thái
        /// </summary>
        /// <param name="statusFilter">Trạng thái cần lọc: All, Pending, Confirmed, Canceled</param>
        private void LoadBookings(string statusFilter = "All")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Câu truy vấn lấy danh sách booking, join bảng Users và Tours
                string query = @"
                SELECT b.BookingID, u.FullName, t.TourName, b.BookingDate, b.Status, b.PaymentStatus, b.Quantity
                FROM Bookings b
                INNER JOIN Users u ON b.UserID = u.UserID
                INNER JOIN Tours t ON b.TourID = t.TourID";

                // Nếu có chọn lọc trạng thái, thêm điều kiện WHERE
                if (statusFilter != "All")
                {
                    query += " WHERE b.Status = @StatusFilter";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (statusFilter != "All")
                    {
                        cmd.Parameters.AddWithValue("@StatusFilter", statusFilter);
                    }

                    conn.Open();
                    // Đổ dữ liệu từ SQL ra DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind dữ liệu lên GridView
                    gvBookings.DataSource = dt;
                    gvBookings.DataBind();
                }
            }
        }

        /// <summary>
        /// Sự kiện khi chọn lọc trạng thái trong DropDownList
        /// </summary>
        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBookings(ddlFilter.SelectedValue); // Load danh sách theo trạng thái đã chọn
        }

        /// <summary>
        /// Sự kiện khi nhấn nút "Sửa" trên GridView
        /// </summary>
        protected void gvBookings_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBookings.EditIndex = e.NewEditIndex; // Bật chế độ chỉnh sửa dòng được chọn
            LoadBookings(ddlFilter.SelectedValue); // Tải lại danh sách
        }

        /// <summary>
        /// Sự kiện khi nhấn "Hủy" không sửa dòng nữa
        /// </summary>
        protected void gvBookings_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBookings.EditIndex = -1; // Hủy chế độ chỉnh sửa
            LoadBookings(ddlFilter.SelectedValue); // Tải lại danh sách
        }

        /// <summary>
        /// Sự kiện khi nhấn "Cập nhật" dòng trong GridView
        /// </summary>
        protected void gvBookings_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Lấy dòng cần cập nhật
            GridViewRow row = gvBookings.Rows[e.RowIndex];
            int bookingID = Convert.ToInt32(gvBookings.DataKeys[e.RowIndex].Value);

            // Tìm DropDownList trạng thái mới và trạng thái thanh toán mới
            DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;
            DropDownList ddlPayment = row.FindControl("ddlPayment") as DropDownList;

            if (ddlStatus == null || ddlPayment == null)
            {
                return; // Nếu không tìm thấy control, thoát
            }

            string newStatus = ddlStatus.SelectedValue;
            string newPaymentStatus = ddlPayment.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Cập nhật trạng thái mới vào database
                string query = "UPDATE Bookings SET Status = @Status, PaymentStatus = @PaymentStatus WHERE BookingID = @BookingID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@PaymentStatus", newPaymentStatus);
                    cmd.Parameters.AddWithValue("@BookingID", bookingID);

                    conn.Open();
                    cmd.ExecuteNonQuery(); // Thực thi câu lệnh UPDATE
                }
            }

            gvBookings.EditIndex = -1; // Thoát chế độ chỉnh sửa
            LoadBookings(ddlFilter.SelectedValue); // Tải lại danh sách
        }

        /// <summary>
        /// Sự kiện khi nhấn "Xóa" đơn đặt chỗ trong GridView
        /// </summary>
        protected void gvBookings_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookingID = Convert.ToInt32(gvBookings.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Xóa đơn đặt chỗ theo ID
                string query = "DELETE FROM Bookings WHERE BookingID = @BookingID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookingID", bookingID);

                    conn.Open();
                    cmd.ExecuteNonQuery(); // Thực thi câu lệnh DELETE
                }
            }

            LoadBookings(ddlFilter.SelectedValue); // Tải lại danh sách
        }
    }
}
