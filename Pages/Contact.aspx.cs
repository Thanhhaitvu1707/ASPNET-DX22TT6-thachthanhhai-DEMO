using System;
using System.Net.Mail;

namespace KhmerFestivalWeb.Pages
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
            {
                lblMessage.Text = "Please fill in all fields.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                // Gửi email (giả lập)
                string toEmail = "support@khmerfestival.com"; // Email nhận
                string subject = "New Contact Form Message";
                string body = $"Name: {name}\nEmail: {email}\nMessage:\n{message}";

                MailMessage mail = new MailMessage();
                mail.To.Add(toEmail);
                mail.From = new MailAddress(email);
                mail.Subject = subject;
                mail.Body = body;

                SmtpClient smtp = new SmtpClient("smtp.yourserver.com"); // Cấu hình SMTP của bạn
                smtp.Send(mail);

                lblMessage.Text = "Your message has been sent!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Failed to send message. Please try again later.";
                lblMessage.ForeColor = System.Drawing.Color.Red;

                // Ghi log lỗi ra console hoặc file log
                Console.WriteLine(ex.Message);
                // Hoặc nếu bạn dùng logging framework:
                // Logger.LogError(ex.ToString());
            }

        }
    }
}
