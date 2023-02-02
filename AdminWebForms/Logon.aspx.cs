using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminWebForms
{
    public partial class Logon : System.Web.UI.Page
    {

        protected override void OnPreInit(EventArgs e)
        {

            if (Request.Cookies["AuthCookie"] != null)
            {
                Response.Redirect("Default.aspx");
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [Obsolete]
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (FormsAuthentication.Authenticate(txtUsername.Text, txtPassword.Text))
            {
                HttpCookie AuthCookie;
                FormsAuthentication.SetAuthCookie(txtUsername.Text, false);

                AuthCookie = FormsAuthentication.GetAuthCookie(txtUsername.Text, true);
                AuthCookie.Name = "AuthCookie";
                if (cbRemember.Checked)
                {
                    AuthCookie.Expires = DateTime.Now.AddDays(7);
                }

                Response.Cookies.Add(AuthCookie);

                Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, true));
            }
            else
            {
                lblMsg.Text = "Wrong password or username!";
            }
        }
    }
}