using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KhmerFestivalWeb.Admin
{
    public partial class Admin_Bookings : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBookings();
            }
        }

        private void LoadBookings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT B.BookingID, U.Username, T.TourName, B.BookingDate, B.Status " +
                               "FROM Bookings B " +
                               "JOIN Users U ON B.UserID = U.UserID " +
                               "JOIN Tours T ON B.TourID = T.TourID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvBookings.DataSource = dt;
                        gvBookings.DataBind();
                    }
                }
            }
        }

        protected void gvBookings_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBookings.EditIndex = e.NewEditIndex;
            LoadBookings();
        }

        protected void gvBookings_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBookings.EditIndex = -1;
            LoadBookings();
        }

        protected void gvBookings_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int bookingID = Convert.ToInt32(gvBookings.DataKeys[e.RowIndex].Value);
            DropDownList ddlStatus = (DropDownList)gvBookings.Rows[e.RowIndex].FindControl("ddlStatus");
            string newStatus = ddlStatus.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Bookings SET Status = @Status WHERE BookingID = @BookingID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@BookingID", bookingID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            gvBookings.EditIndex = -1;
            LoadBookings();
        }

        protected void gvBookings_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookingID = Convert.ToInt32(gvBookings.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Bookings WHERE BookingID = @BookingID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookingID", bookingID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            LoadBookings();
        }
    }
}