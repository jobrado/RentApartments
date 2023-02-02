using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminWebForms
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("yyyy");
        }

        protected void btnLogo_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void SignOut_Click(object sender, EventArgs e)
        {

            if (Request.Cookies["AuthCookie"] != null)
            {
                Response.Cookies["AuthCookie"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Redirect("~/Logon.aspx");

        }
    }
}