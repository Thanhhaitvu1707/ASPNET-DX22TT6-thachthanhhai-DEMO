using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace KhmerFestivalWeb.Admin
{
    public partial class Admin_LeHoi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeHoi();
            }
        }

        private void LoadLeHoi()
        {
            string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Festivals ORDER BY Date ASC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLeHoi.DataSource = dt;
                gvLeHoi.DataBind();
            }
        }

        protected void btnThemLeHoi_Click(object sender, EventArgs e)
        {
            string tenLeHoi = txtTenLeHoi.Text;
            string diaDiem = txtDiaDiem.Text;
            string ngayToChuc = txtNgayToChuc.Text;

            string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Festivals (FestivalName, Location, Date) VALUES (@TenLeHoi, @DiaDiem, @NgayToChuc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenLeHoi", tenLeHoi);
                cmd.Parameters.AddWithValue("@DiaDiem", diaDiem);
                cmd.Parameters.AddWithValue("@NgayToChuc", ngayToChuc);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                LoadLeHoi();
            }
        }

        protected void gvLeHoi_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLeHoi.EditIndex = e.NewEditIndex;
            LoadLeHoi();
        }

        protected void gvLeHoi_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvLeHoi.EditIndex = -1;
            LoadLeHoi();
        }

        protected void gvLeHoi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvLeHoi.DataKeys[e.RowIndex].Value);
            string tenLeHoi = (gvLeHoi.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
            string diaDiem = (gvLeHoi.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text;
            string ngayToChuc = (gvLeHoi.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox).Text;

            string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Festivals SET FestivalName=@TenLeHoi, Location=@DiaDiem, Date=@NgayToChuc WHERE FestivalID=@ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@TenLeHoi", tenLeHoi);
                cmd.Parameters.AddWithValue("@DiaDiem", diaDiem);
                cmd.Parameters.AddWithValue("@NgayToChuc", ngayToChuc);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                gvLeHoi.EditIndex = -1;
                LoadLeHoi();
            }
        }

        protected void gvLeHoi_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvLeHoi.DataKeys[e.RowIndex].Value);
            string connStr = ConfigurationManager.ConnectionStrings["KhmerFestivalDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Xóa tất cả Tour liên quan đến Festival trước
                string deleteToursQuery = "DELETE FROM Tours WHERE FestivalID = @ID";
                SqlCommand deleteToursCmd = new SqlCommand(deleteToursQuery, conn);
                deleteToursCmd.Parameters.AddWithValue("@ID", id);
                deleteToursCmd.ExecuteNonQuery();

                // Sau đó mới xóa Festival
                string deleteFestivalQuery = "DELETE FROM Festivals WHERE FestivalID = @ID";
                SqlCommand deleteFestivalCmd = new SqlCommand(deleteFestivalQuery, conn);
                deleteFestivalCmd.Parameters.AddWithValue("@ID", id);
                deleteFestivalCmd.ExecuteNonQuery();

                conn.Close();
                LoadLeHoi(); // Cập nhật lại danh sách
            }
        }
    }
}
