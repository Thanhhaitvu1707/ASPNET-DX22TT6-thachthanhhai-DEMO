using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace KhmerFestivalWeb.Pages
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string fullName = txtFullName.Text.Trim();

            // 1️⃣ Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(fullName))
            {
                ShowMessage("Vui lòng điền đầy đủ thông tin!", false);
                return;
            }

            // 2️⃣ Kiểm tra trùng Username hoặc Email
            if (CheckUserExists(username, email))
            {
                ShowMessage("Tên đăng nhập hoặc Email đã tồn tại!", false);
                return;
            }

            // 3️⃣ Mã hóa mật khẩu
            string passwordHash = HashPassword(password);

            // 4️⃣ Lưu vào Database
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO Users (Username, PasswordHash, FullName, Email, Phone) VALUES (@Username, @PasswordHash, @FullName, @Email, @Phone)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            ShowMessage("Đăng ký thành công! Bạn có thể <a href='Login.aspx'>đăng nhập ngay</a>.", true);
                        }
                        else
                        {
                            ShowMessage("Có lỗi xảy ra, vui lòng thử lại!", false);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ShowMessage("Lỗi hệ thống: " + ex.Message, false); // Chỉ hiển thị khi debug
            }
        }

        // ⚡ Kiểm tra User tồn tại
        private bool CheckUserExists(string username, string email)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // 🔐 Hàm Hash mật khẩu
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes); // 🔥 Base64 để đồng bộ với đăng nhập
            }
        }

        // 🖥️ Hiển thị thông báo lên giao diện
        private void ShowMessage(string message, bool isSuccess)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = isSuccess ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            lblMessage.CssClass = isSuccess ? "text-success" : "text-danger";
        }
    }
}
