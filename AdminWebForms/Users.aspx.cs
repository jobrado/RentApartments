using DAL.Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminWebForms
{
    public partial class Users : System.Web.UI.Page
    {
        private List<User> listOfUsers = new List<User>();

        protected void Page_Load(object sender, EventArgs e)
        {

            listOfUsers = RepositoryFactory.GetRepository().GetAllUsers().OrderBy(x => x.Id).ToList();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptTags.DataSource = listOfUsers;
            rptTags.DataBind();
        }


    }
}