using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KhmerFestivalWeb.Admin
{
    public partial class Admin_Users : System.Web.UI.Page
    {
        // Chuỗi kết nối đến database KhmerFestivalDB
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Chỉ thực hiện khi lần đầu load trang
            {
                LoadUsers(); // Tải danh sách người dùng
            }
        }

        /// <summary>
        /// Hàm tải danh sách người dùng từ database
        /// </summary>
        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Câu truy vấn lấy thông tin User
                string query = "SELECT UserID, Username, FullName, Email, Phone, Role FROM Users";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Đổ dữ liệu vào GridView
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        /// <summary>
        /// Sự kiện khi nhấn nút "Edit" trong GridView
        /// </summary>
        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex; // Bật chế độ chỉnh sửa dòng
            LoadUsers(); // Load lại danh sách
        }

        /// <summary>
        /// Sự kiện khi nhấn "Cancel" khi chỉnh sửa
        /// </summary>
        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1; // Thoát chế độ chỉnh sửa
            LoadUsers();
        }

        /// <summary>
        /// Sự kiện cập nhật người dùng sau khi chỉnh sửa
        /// </summary>
        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Lấy ID người dùng từ DataKey
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

            // Lấy các giá trị mới từ ô TextBox
            string fullName = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string email = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string phone = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string role = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Cập nhật thông tin người dùng
                string query = "UPDATE Users SET FullName=@FullName, Email=@Email, Phone=@Phone, Role=@Role WHERE UserID=@UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                cmd.ExecuteNonQuery(); // Thực thi lệnh UPDATE
                conn.Close();
            }

            gvUsers.EditIndex = -1; // Thoát chế độ chỉnh sửa
            LoadUsers(); // Load lại danh sách
        }

        /// <summary>
        /// Sự kiện xóa người dùng
        /// </summary>
        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Lấy ID người dùng cần xóa
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Xóa người dùng từ bảng Users
                string query = "DELETE FROM Users WHERE UserID=@UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                cmd.ExecuteNonQuery(); // Thực thi lệnh DELETE
                conn.Close();
            }

            LoadUsers(); // Load lại danh sách
        }

        /// <summary>
        /// Sự kiện thêm người dùng mới
        /// </summary>
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các TextBox
            string username = txtUsername.Text;
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string role = ddlRole.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Thêm user mới vào database
                string query = "INSERT INTO Users (Username, FullName, Email, Phone, Role) VALUES (@Username, @FullName, @Email, @Phone, @Role)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Role", role);

                conn.Open();
                cmd.ExecuteNonQuery(); // Thực thi lệnh INSERT
                conn.Close();
            }

            // Xóa dữ liệu sau khi thêm
            txtUsername.Text = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            ddlRole.SelectedIndex = 0;

            LoadUsers(); // Load lại danh sách
        }
    }
}
