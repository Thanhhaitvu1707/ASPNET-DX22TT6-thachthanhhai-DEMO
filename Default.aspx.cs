using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace KhmerFestivalWeb
{
    public partial class Default : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTours();
                LoadFestivals();
            }
        }

     
        private void LoadTours()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TourID, TourName, Price, StartDate, EndDate, TourImage FROM Tours";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rptTourList.DataSource = dt;
                    rptTourList.DataBind();
                }
            }
        }

        private void LoadFestivals()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT FestivalID, FestivalName, Content FROM Festivals";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rptFestivals.DataSource = dt;
                    rptFestivals.DataBind();
                }
            }
        }

    }
}
