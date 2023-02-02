using DAL;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DAL.Model.Apartment;

namespace AdminWebForms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static IList<Tag> tags = new List<Tag>();
        private static IList<ApartmentPicture> acs = new List<ApartmentPicture>();
        private static ApartmentPicture apartmentPicture = new ApartmentPicture();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDdl();
            }
        }

        private void LoadDdl()
        {
            ddlCity.DataSource = RepositoryFactory.GetRepository().GetCities();
            ddlCity.DataValueField = "Id";
            ddlCity.DataTextField = "Name";
            ddlCity.DataBind();
            ddlOwner.DataSource = RepositoryFactory.GetRepository().GetApartmentOwners();
            ddlOwner.DataValueField = "Id";
            ddlOwner.DataTextField = "Name";
            ddlOwner.DataBind();
            ddlStatus.Items.Add(new ListItem(Apartment.Status.Zauzeto.ToString()));
            ddlStatus.Items.Add(new ListItem(Apartment.Status.Rezervirano.ToString()));
            ddlStatus.Items.Add(new ListItem(Apartment.Status.Slobodno.ToString()));
            lbTags.DataSource = RepositoryFactory.GetRepository().GetTags();
            lbTags.DataTextField = "Name";
            lbTags.DataValueField = "Id";
            lbTags.DataBind();

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            int owner = int.Parse(ddlOwner.SelectedValue);
            int cityID = int.Parse(ddlCity.SelectedValue);
            foreach (ListItem item in lbTags.Items)
            {
                if (item.Selected)
                {
                    var id = item.Value;
                    Tag tag = RepositoryFactory.GetRepository().GetTags().ToList().Find(x => x.Id == int.Parse(id));
                    tags.Add(tag);
                }
            }
            var files = SaveUploadedImagesToDisk();
            var apartmentPictures =
            files.Select(x => new Pictures
            {
              Path = x,
              Name = Path.GetFileNameWithoutExtension(x),
              IsRepresentative = false
             }).ToList();

            Apartment apartment = new Apartment
            {
                Name = tbName.Text,
                CreatedAt = DateTime.Now,
                OwnerId = RepositoryFactory.GetRepository().GetApartmentOwners().ToList().Find(x => x.Id == owner),

                StatusId = (Apartment.Status)Enum.Parse(typeof(Apartment.Status), ddlStatus.SelectedValue),
                CityId = RepositoryFactory.GetRepository().GetCities().ToList().Find(x => x.Id == cityID),
                Address = tbAddress.Text,
                Price = int.Parse(tbPrice.Text),
                MaxAdults = int.Parse(tbAdults.Text),
                MaxChildren = int.Parse(tbChildren.Text),
                TotalRooms = int.Parse(tbTotalRooms.Text),
                BeachDistance = int.Parse(tbBeachDistance.Text),
                Tag = tags,
                Pictures = GetRepeaterPictures()

            };
            apartment.Pictures = apartmentPictures;
            RepositoryFactory.GetRepository().CreateApartment(apartment);

            Response.Redirect("~/Default.aspx");
        }

          protected FileUpload GetFileUpload1()
            {
              return uplImages;
          }

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            var files = SaveUploadedImagesToDisk();
            var apartmentPictures =
             files.Select(x => new Pictures
            {
              Path = x,
              Name = Path.GetFileNameWithoutExtension(x),
              IsRepresentative = false
                 }).ToList();
            var isNewApartment = (Request.QueryString["id"] == null);

        
        }
        private readonly string _picPath =  "/Content/Pictures/";
            private List<string> SaveUploadedImagesToDisk()
        {
            var files = new List<string>();
            if (uplImages.HasFiles)
            {
                var uplImagesRoot = Server.MapPath(_picPath);
                if (!Directory.Exists(uplImagesRoot))
                    Directory.CreateDirectory(uplImagesRoot);
                foreach (HttpPostedFile uploadedFile in uplImages.PostedFiles)
                {
                    var uplImagePath = Path.Combine(uplImagesRoot, uploadedFile.FileName);
                    uploadedFile.SaveAs(uplImagePath);
                    files.Add(uploadedFile.FileName);
                }
            }
            return files;
        }
        private List<Pictures> GetRepeaterPictures()
        {
            var repApartmentPicturesItems = repApartmentPictures.Items;
            var pictures = new List<Pictures>();
            foreach (RepeaterItem item in repApartmentPicturesItems)
            {
                var pic = new Pictures();
                pic.Id = int.Parse((item.FindControl("hidApartmentPictureId") as HiddenField).Value);
                pic.Name = (item.FindControl("txtApartmentPicture") as TextBox).Text;
                pic.Path = (item.FindControl("imgApartmentPicture") as Image).ImageUrl;
                pic.IsRepresentative = (item.FindControl("cbIsRepresentative") as CheckBox).Checked;
                pic.DoDelete = (item.FindControl("cbDelete") as CheckBox).Checked;
                pictures.Add(pic);
            }
            return pictures;
        }
    }
}