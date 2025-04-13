using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.IO;

namespace KhmerFestivalWeb.Admin
{
    public partial class Admin_Tour : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTours();
            }
        }

        void LoadTours()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Tours";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvTours.DataSource = dt;
                gvTours.DataBind();
            }
        }

        protected void btnAddTour_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu trước khi thêm
            if (string.IsNullOrWhiteSpace(txtTourName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtAvailableSlots.Text) || string.IsNullOrWhiteSpace(txtStartDate.Text) ||
                string.IsNullOrWhiteSpace(txtEndDate.Text))
            {
                lblMessage.Text = "⚠ Vui lòng điền đầy đủ thông tin!";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO Tours (TourName, Description, Price, AvailableSlots, StartDate, EndDate) VALUES (@TourName, @Description, @Price, @AvailableSlots, @StartDate, @EndDate)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TourName", txtTourName.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@AvailableSlots", Convert.ToInt32(txtAvailableSlots.Text));
                    cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text));
                    cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(txtEndDate.Text));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = "✅ Tour đã được thêm thành công!";
                LoadTours();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ Lỗi: " + ex.Message;
            }
        }

        protected void gvTours_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTours.EditIndex = e.NewEditIndex;
            LoadTours();
        }

        protected void gvTours_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = gvTours.Rows[e.RowIndex];
                int TourID = Convert.ToInt32(gvTours.DataKeys[e.RowIndex].Value);

                string TourName = (row.Cells[1].Controls[0] as TextBox).Text;
                string Description = (row.Cells[2].Controls[0] as TextBox).Text;
                decimal Price = Convert.ToDecimal((row.Cells[3].Controls[0] as TextBox).Text);
                int AvailableSlots = Convert.ToInt32((row.Cells[4].Controls[0] as TextBox).Text);
                DateTime StartDate = Convert.ToDateTime((row.Cells[5].Controls[0] as TextBox).Text);
                DateTime EndDate = Convert.ToDateTime((row.Cells[6].Controls[0] as TextBox).Text);
              

                FileUpload fileUpload = (FileUpload)row.FindControl("fileUploadTourImage");
                HiddenField hfTourImage = (HiddenField)row.FindControl("hfTourImage");

                // Xử lý đường dẫn ảnh
                string tourImage = hfTourImage.Value; // Giữ nguyên ảnh cũ nếu không upload ảnh mới
                if (fileUpload.HasFile)
                {
                    string fileName = Path.GetFileName(fileUpload.FileName);
                    string savePath = Server.MapPath("~/Images/" + fileName);

                    // Kiểm tra nếu file trùng thì thêm số vào tên file
                    int count = 1;
                    string fileExt = Path.GetExtension(fileName);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                    while (File.Exists(savePath))
                    {
                        fileName = $"{fileNameWithoutExt}_{count}{fileExt}";
                        savePath = Server.MapPath("~/images/" + fileName);
                        count++;
                    }

                    fileUpload.SaveAs(savePath);
                    tourImage = "Images/" + fileName; // Cập nhật đường dẫn ảnh mới
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "UPDATE Tours SET TourName=@TourName, Description=@Description, Price=@Price, AvailableSlots=@AvailableSlots, StartDate=@StartDate, EndDate=@EndDate, TourImage=@TourImage WHERE TourID=@TourID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TourID", TourID);
                    cmd.Parameters.AddWithValue("@TourName", TourName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@AvailableSlots", AvailableSlots);
                    cmd.Parameters.AddWithValue("@StartDate", StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", EndDate);
                    cmd.Parameters.AddWithValue("@TourImage", tourImage);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvTours.EditIndex = -1;
                LoadTours();
                lblMessage.Text = "✅ Cập nhật tour thành công!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ Lỗi khi cập nhật tour: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }


        protected void gvTours_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int tourID = Convert.ToInt32(gvTours.DataKeys[e.RowIndex].Value); // Lấy TourID

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 1️⃣ Kiểm tra số lượng booking liên quan
                string checkQuery = "SELECT COUNT(*) FROM Bookings WHERE TourID = @TourID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@TourID", tourID);
                int bookingCount = (int)checkCmd.ExecuteScalar();

                if (bookingCount > 0)
                {
                    lblMessage.Text = "⚠ Tour này có khách đặt chỗ. Hãy hủy booking trước khi xóa!";
                    return;
                }

                // 2️⃣ Xóa tour nếu không có booking
                string deleteQuery = "DELETE FROM Tours WHERE TourID = @TourID";
                SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                cmd.Parameters.AddWithValue("@TourID", tourID);
                cmd.ExecuteNonQuery();

                lblMessage.Text = "✅ Tour đã được xóa thành công!";
            }

            LoadTours(); // Tải lại danh sách sau khi xóa
        }
        protected void gvTours_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTours.EditIndex = -1;
            LoadTours(); // Hàm này cần có để tải lại danh sách tour
        }

    }
}
