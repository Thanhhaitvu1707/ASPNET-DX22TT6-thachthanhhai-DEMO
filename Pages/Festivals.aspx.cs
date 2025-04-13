using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace KhmerFestivalWeb.Pages
{
    public partial class Festivals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFestivals();
            }
        }

        private void LoadFestivals(string search = "")
        {
            string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT FestivalID, FestivalName, Location, Date FROM Festivals WHERE FestivalName LIKE @Search ORDER BY Date ASC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Search", "%" + search + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvFestivals.DataSource = dt;
                gvFestivals.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadFestivals(txtSearch.Text);
        }

        protected void gvFestivals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFestivals.PageIndex = e.NewPageIndex;
            LoadFestivals(txtSearch.Text);
        }
    }
}
