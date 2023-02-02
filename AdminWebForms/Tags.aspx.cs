using DAL;
using DAL.Model;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace AdminWebForms
{
    public partial class Tags : System.Web.UI.Page
    {
        
        private List<Tag> listOfTags = new List<Tag>();
   
        protected void Page_Load(object sender, EventArgs e)
        {
            listOfTags = RepositoryFactory.GetRepository().GetTags().OrderBy(x => x.Id).ToList();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptTags.DataSource = listOfTags;
            rptTags.DataBind();

        }
   //     TextBox name;
  //      TextBox nameEng;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Tag tag = new Tag();
            tag.Name = tbName.Text;
            tag.NameEng = tbName.Text;
            RepositoryFactory.GetRepository().CreateTag(tag);
            Response.Redirect(Request.RawUrl);
            /*  name = new TextBox();
             Label lblName = new Label
             {
                 Text = "Name: "
             };

             nameEng = new TextBox();
             Label lblNameEng = new Label();
             lblNameEng.Text = "Name in english: ";
             Panel1.Controls.Add(lblName);
             Panel1.Controls.Add(name);
             Panel1.Controls.Add(new LiteralControl("<br>"));
             Panel1.Controls.Add(lblNameEng);
             Panel1.Controls.Add(nameEng);
             Panel1.Controls.Add(new LiteralControl("<br>"));
             Button btn = new Button
             {
                 ID = "btnNewAdd",
                 Text = "Add"

             };
             Panel1.DefaultButton = "btnNewAdd";
             btn.Click += new EventHandler(btnAddNewTag_Click);
             Panel1.Controls.Add(btn);*/


        }
        protected void btnAddNewTag_Click(object sender, EventArgs e)
        {
   //         tag.Name = name.Text;
   //         tag.NameEng = nameEng.Text;
    //        Button button = sender as Button;
   //         Label label = new Label { Text = "Added" };
   //         Panel1.Controls.Add(label);
   //         RepositoryFactory.GetRepository().CreateTag(tag);

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int id = int.Parse(button.CommandArgument);
            RepositoryFactory.GetRepository().DeleteTag(id);
            Response.Redirect(Request.RawUrl);

        }
    }
    }
