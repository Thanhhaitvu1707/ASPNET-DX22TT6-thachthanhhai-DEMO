using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
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
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Tours (TourName, Description, Price, AvailableSlots, StartDate, EndDate) VALUES (@TourName, @Description, @Price, @AvailableSlots, @StartDate, @EndDate)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TourName", txtTourName.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                cmd.Parameters.AddWithValue("@AvailableSlots", Convert.ToInt32(txtAvailableSlots.Text));
                cmd.Parameters.AddWithValue("@StartDate", txtStartDate.Text);
                cmd.Parameters.AddWithValue("@EndDate", txtEndDate.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                LoadTours();
            }
        }
        protected void gvTours_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTours.EditIndex = e.NewEditIndex;
            LoadTours();
        }
        protected void gvTours_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvTours.Rows[e.RowIndex];
            int TourID = Convert.ToInt32(gvTours.DataKeys[e.RowIndex].Value);

            string TourName = (row.Cells[1].Controls[0] as TextBox).Text;
            string Description = (row.Cells[2].Controls[0] as TextBox).Text;
            decimal Price = Convert.ToDecimal((row.Cells[3].Controls[0] as TextBox).Text);
            int AvailableSlots = Convert.ToInt32((row.Cells[4].Controls[0] as TextBox).Text);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Tours SET TourName=@TourName, Description=@Description, Price=@Price, AvailableSlots=@AvailableSlots WHERE TourID=@TourID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TourID", TourID);
                cmd.Parameters.AddWithValue("@TourName", TourName);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@AvailableSlots", AvailableSlots);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            gvTours.EditIndex = -1;
            LoadTours();
        }
        protected void gvTours_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int tourID = Convert.ToInt32(gvTours.DataKeys[e.RowIndex].Value); // Lấy TourID

            string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Tours WHERE TourID = @TourID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TourID", tourID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadTours(); // Tải lại danh sách sau khi xóa
        }
        protected void gvTours_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTours.EditIndex = -1;
            LoadTours(); // Hàm này dùng để load lại dữ liệu
        }

    }


}