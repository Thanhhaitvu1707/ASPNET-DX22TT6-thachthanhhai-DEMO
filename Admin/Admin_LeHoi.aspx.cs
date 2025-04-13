using KhmerFestivalWeb.Pages;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;

namespace KhmerFestivalWeb.Admin
{
    public partial class Admin_LeHoi : System.Web.UI.Page
    {
        // Chuỗi kết nối tới database KhmerFestivalDB
        string connString = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Nếu lần đầu load trang
            {
                LoadFestivals(); // Tải danh sách lễ hội
            }
        }

        /// <summary>
        /// Hàm tải danh sách lễ hội từ cơ sở dữ liệu
        /// </summary>
        private void LoadFestivals()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Festivals", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gán dữ liệu vào GridView
                    gvFestivals.DataSource = dt;
                    gvFestivals.DataBind();
                }
                lblError.Text = ""; // Xóa thông báo lỗi nếu load thành công
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi khi tải danh sách lễ hội: " + ex.Message;
            }
        }

        /// <summary>
        /// Xử lý khi nhấn nút "Thêm Lễ Hội"
        /// </summary>
        protected void btnAddFestival_Click(object sender, EventArgs e)
        {
            try
            {
                string festivalName = txtFestivalName.Text;
                string location = txtLocation.Text;
                string date = txtDate.Text;
                string imagePath = "";

                // Nếu có file ảnh được upload
                if (fileUploadNewImage.HasFile)
                {
                    string fileName = Path.GetFileName(fileUploadNewImage.FileName);
                    imagePath = "images/" + fileName;
                    fileUploadNewImage.SaveAs(Server.MapPath("~/" + imagePath)); // Lưu ảnh lên server
                }

                // Thêm lễ hội mới vào database
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "INSERT INTO Festivals (FestivalName, Location, Date, ImagePath) VALUES (@name, @location, @date, @image)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", festivalName);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@image", imagePath);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadFestivals(); // Tải lại danh sách
                lblError.Text = "Thêm lễ hội thành công!";
                lblError.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi khi thêm lễ hội: " + ex.Message;
            }
        }

        /// <summary>
        /// Sự kiện nhấn nút "Edit" trên GridView
        /// </summary>
        protected void gvFestivals_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFestivals.EditIndex = e.NewEditIndex; // Chuyển dòng được chọn thành editable
            LoadFestivals(); // Load lại dữ liệu
        }

        /// <summary>
        /// Sự kiện nhấn "Cập nhật" khi chỉnh sửa lễ hội
        /// </summary>
        protected void gvFestivals_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // Lấy dòng hiện tại
                GridViewRow row = gvFestivals.Rows[e.RowIndex];
                int id = Convert.ToInt32(gvFestivals.DataKeys[e.RowIndex].Value);
                string festivalName = ((TextBox)row.Cells[1].Controls[0]).Text;
                string location = ((TextBox)row.Cells[2].Controls[0]).Text;
                string dateText = ((TextBox)row.Cells[3].Controls[0]).Text;
                FileUpload fileUpload = (FileUpload)row.FindControl("fileUploadImage");
                HiddenField hfImagePath = (HiddenField)row.FindControl("hfImagePath");

                // Kiểm tra và chuyển định dạng ngày tháng
                DateTime parsedDate;
                if (!DateTime.TryParseExact(dateText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    lblError.Text = "Lỗi: Ngày tháng không đúng định dạng (dd/MM/yyyy)";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Lấy đường dẫn ảnh (nếu có upload mới)
                string imagePath = hfImagePath.Value;
                if (fileUpload.HasFile)
                {
                    string fileName = Path.GetFileName(fileUpload.FileName);
                    imagePath = "Images/" + fileName;
                    fileUpload.SaveAs(Server.MapPath("~/" + imagePath));
                }

                // Cập nhật lễ hội vào database
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "UPDATE Festivals SET FestivalName=@name, Location=@location, Date=@date, ImagePath=@image WHERE FestivalID=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", festivalName);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@date", parsedDate.ToString("yyyy-MM-dd")); // Định dạng ngày cho SQL Server
                    cmd.Parameters.AddWithValue("@image", imagePath);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvFestivals.EditIndex = -1; // Thoát chế độ chỉnh sửa
                LoadFestivals();
                lblError.Text = "Cập nhật lễ hội thành công!";
                lblError.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi khi cập nhật lễ hội: " + ex.Message;
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Sự kiện hủy chỉnh sửa trên GridView
        /// </summary>
        protected void gvFestivals_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFestivals.EditIndex = -1; // Thoát khỏi chế độ chỉnh sửa
            LoadFestivals();
        }

        /// <summary>
        /// Sự kiện xóa lễ hội khỏi cơ sở dữ liệu
        /// </summary>
        protected void gvFestivals_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvFestivals.DataKeys[e.RowIndex].Value);
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "DELETE FROM Festivals WHERE FestivalID=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadFestivals();
                lblError.Text = "Xóa lễ hội thành công!";
                lblError.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi khi xóa lễ hội: " + ex.Message;
            }
        }
    }
}
