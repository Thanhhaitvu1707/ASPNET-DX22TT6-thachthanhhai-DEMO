using System;
using System.Configuration;
using System.Data.SqlClient;
using WebGrease.Activities;

namespace KhmerFestivalWeb.Pages
{
    public partial class ChiTietLeHoi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFestivalDetails();
            }
        }

        private void LoadFestivalDetails()
        {
            string festivalId = Request.QueryString["ID"];
            if (string.IsNullOrEmpty(festivalId))
            {
                lblError.Text = "Không tìm thấy lễ hội.";
                return;
            }


            string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT FestivalName, Description, Location, Date, ImagePath FROM Festivals WHERE FestivalID = @FestivalID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FestivalID", festivalId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblFestivalName.Text = reader["FestivalName"].ToString();
                    lblLocation.Text = reader["Location"].ToString();
                    lblDate.Text = Convert.ToDateTime(reader["Date"]).ToString("dd/MM/yyyy");
                    lblDescription.Text = reader["Description"].ToString();
                    imgFestival.ImageUrl = reader["ImagePath"].ToString();
                }
                else
                {
                    Response.Redirect("Festivals.aspx");
                }
                conn.Close();
            }
        }
    }
}
