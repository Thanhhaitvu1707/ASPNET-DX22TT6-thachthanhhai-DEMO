using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace KhmerFestivalWeb.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null || Session["AdminID"] != null) // Nếu đã đăng nhập, chuyển về trang chính
            {
                Response.Redirect("Default.aspx");
            }
            txtUsername.Attributes.Add("required", "required");
            txtPassword.Attributes.Add("required", "required");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

      

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string hashedPassword = HashPassword(txtPassword.Text); // Hash mật khẩu nhập vào

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString))
            {
                conn.Open();

                // Kiểm tra trong bảng Users trước
                string userQuery = "SELECT UserID, Username FROM Users WHERE Username = @Username AND PasswordHash = @Password";
                using (SqlCommand cmd = new SqlCommand(userQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Nếu tìm thấy user
                        {
                            Session["UserID"] = reader["UserID"].ToString();
                            Session["Username"] = reader["Username"].ToString(); // Lưu Username vào Session
                            Session["Role"] = "User";
                            reader.Close();
                            Response.Redirect("/Default.aspx");
                            return;
                        }
                    }
                }

                // Kiểm tra trong bảng Admins
                string adminQuery = "SELECT AdminID, Username FROM Admins WHERE Username = @Username AND PasswordHash = @Password";
                using (SqlCommand cmd = new SqlCommand(adminQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Nếu tìm thấy admin
                        {
                            Session["AdminID"] = reader["AdminID"].ToString();
                            Session["Username"] = reader["Username"].ToString(); // Lưu Username vào Session
                            Session["Role"] = "Admin";
                            reader.Close();
                            Response.Redirect("~/Admin/AdminDashboard.aspx");
                            return;
                        }
                    }
                }


                // Nếu không tìm thấy trong cả hai bảng, báo lỗi
                lblMessage.Text = "<span style='color: red;'>Tên đăng nhập hoặc mật khẩu không đúng!</span>";
            }
        }
    }
}
