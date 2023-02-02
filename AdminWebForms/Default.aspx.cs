using DAL;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminWebForms
{
    public partial class Default : System.Web.UI.Page
    {
        private List<Apartment> listOfApartmants = new List<Apartment>();
        static int tagID;

        protected void Page_Load(object sender, EventArgs e)
        {
            listOfApartmants = RepositoryFactory.GetRepository().GetApartments(null, null, null).OrderBy(x => x.IDApartment).ToList();


            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            foreach (var item in listOfApartmants)
            {
                item.Ar = RepositoryFactory.GetRepository().GetApartmentReservation(item.IDApartment);
            }
            rptTags.DataSource = listOfApartmants;
            rptTags.DataBind();
        }

        protected void lbStatus_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;

            tagID = int.Parse(linkButton.CommandArgument);
            Console.Write(tagID);

            lblStatus.Visible = true;
            ddlStatus.Visible = true;
            lbluser.Visible = true;
            tbUser.Visible = true;
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;

            Apartment a = RepositoryFactory.GetRepository().GetApartment(tagID);

            a.Picture = RepositoryFactory.GetRepository().GetApartmentPictures(tagID);
            a.Tag = RepositoryFactory.GetRepository().GetApartmantTags(tagID);
            a.StatusId = (Apartment.Status)Enum.Parse(typeof(Apartment.Status), ddl.SelectedValue);

            if (tbUser.Text.Trim() != "" && int.TryParse(tbUser.Text.Trim(), out _))
            {
                int idUser = int.Parse(tbUser.Text);

                if (ddlStatus.SelectedIndex == 1 || ddlStatus.SelectedIndex == 3)
                {
                    User user = RepositoryFactory.GetRepository().GetUserByID(idUser);
                    ApartmentReservation ar = new ApartmentReservation
                    {
                        UserId = user,
                        ApartmentId = a,
                        UserAddress = user.Address,
                        UserEmail = user.Email,
                        UserName = user.UserName,
                        UserPhone = user.PhoneNumber
                    };
                    RepositoryFactory.GetRepository().CreateApartmentReservation(ar);
                    RepositoryFactory.GetRepository().UpdateApartment(a);
                }
            }
            else if (ddlStatus.SelectedIndex == 1 || ddlStatus.SelectedIndex == 3 && tbUser.Text.Trim() == "")
            {
                lblError.Visible = true;

            }

            if (ddlStatus.SelectedIndex == 2)
            {
                RepositoryFactory.GetRepository().DeleteApartmentReservation(a.IDApartment);
                RepositoryFactory.GetRepository().UpdateApartment(a);
            }

            Response.Redirect(Request.Url.AbsoluteUri);

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int id = int.Parse(button.CommandArgument);
            RepositoryFactory.GetRepository().DeleteApartment(id);
            Response.Redirect(Request.RawUrl);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForm1.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int id = int.Parse(button.CommandArgument);
            Response.Redirect("~/Update.aspx?id=" + id);
        }
    }
}