using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KhmerFestivalWeb.Admin
{
	public partial class AdminDashboard : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
        protected void btnLeHoi_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Admin_LeHoi.aspx");
        }

        protected void btnTour_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Admin_Tour.aspx");
        }

        protected void btnUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Admin_Users.aspx");
        }

        protected void btnBookings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Admin_Bookings.aspx");
        }

    }
}