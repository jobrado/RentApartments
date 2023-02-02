
using DAL;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace AdminWebForms
{
    public partial class Update : System.Web.UI.Page
    {
        private static IList<Tag> tags = new List<Tag>();
        private static IList<ApartmentPicture> acs = new List<ApartmentPicture>();
        private static ApartmentPicture apartmentPicture = new ApartmentPicture();
        private static int idApt;
       private static Apartment apt = new Apartment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            idApt = int.Parse(Request.QueryString["id"]);
            
            if (!IsPostBack)
            {
                LoadData();
                LoadDdl();
               
            }
        }

        private void LoadData()
        {
            apt = RepositoryFactory.GetRepository().GetApartment(idApt);
            tbName.Text = apt.Name;
            tbAddress.Text = apt.Address;
            tbPrice.Text = apt.Price.ToString();
            tbAdults.Text = apt.MaxAdults.ToString();
            tbChildren.Text = apt.MaxChildren.ToString();
            tbTotalRooms.Text = apt.TotalRooms.ToString();
            tbBeachDistance.Text = apt.BeachDistance.ToString();

            apt.Pictures = RepositoryFactory.GetRepository().GetPictures(idApt);
            for (int i = 0; i < apt.Pictures.Count; i++)
            {
                apt.Pictures[i].Path =
                Path.Combine(_picPath, apt.Pictures[i].Path);
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

            repApartmentPictures.DataSource = apt.Pictures;
            repApartmentPictures.DataBind();
        }
        protected FileUpload GetFileUpload1()
        {
            return uplImages;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var files = SaveUploadedImagesToDisk();
            int owner = int.Parse(ddlOwner.SelectedValue);
            int cityID = int.Parse(ddlCity.SelectedValue);
            var apartmentPictures = new List<Pictures> ();
            apartmentPictures =
            files.Select(x => new Pictures
            {
               Path = x,
               Name = Path.GetFileNameWithoutExtension(x),
               IsRepresentative = false
            }).ToList();
          var   apartmentPictures1 = GetRepeaterPictures();


           
            Apartment a = RepositoryFactory.GetRepository().GetApartment(int.Parse(Request.QueryString["id"]));
            
            a.IDApartment = idApt;
            a.Name = tbName.Text;
            a.CreatedAt = DateTime.Now;
            a.OwnerId = RepositoryFactory.GetRepository().GetApartmentOwners().ToList().Find(x => x.Id == owner);
            a.StatusId = (Apartment.Status)Enum.Parse(typeof(Apartment.Status), ddlStatus.SelectedValue);
            a.CityId = RepositoryFactory.GetRepository().GetCities().ToList().Find(x => x.Id == cityID);
            a.Address = tbAddress.Text;
            a.Price = int.Parse(tbPrice.Text);
            a.MaxAdults = int.Parse(tbAdults.Text);
            a.MaxChildren = int.Parse(tbChildren.Text);
            a.TotalRooms = int.Parse(tbTotalRooms.Text);
            a.BeachDistance = int.Parse(tbBeachDistance.Text);
           
            foreach (ListItem item in lbTags.Items)
            {
                if (item.Selected)
                {
                    var id = item.Value;
                    Tag tag = RepositoryFactory.GetRepository().GetTags().ToList().Find(x => x.Id == int.Parse(id));
                    tags.Add(tag);
                    a.Tag = tags;
                }
            }
            if (apartmentPictures.Any() == true)
            {
                a.Pictures = apartmentPictures;
            }
            if (apartmentPictures1.Any() == true)
                a.Pictures = apartmentPictures1;

      
            RepositoryFactory.GetRepository().UpdateApartment(a);
            Response.Redirect($"Update.aspx?{Request.QueryString}");
        }
        private readonly string _picPath = "/Content/Pictures/";
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
                pic.Path = (item.FindControl("imgApartmentPicture") as Image).ImageUrl.Substring(18);
                
                pic.IsRepresentative = (item.FindControl("cbIsRepresentative") as CheckBox).Checked;
                pic.DoDelete = (item.FindControl("cbDelete") as CheckBox).Checked;
                pictures.Add(pic);
            }
            return pictures;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (uplImages.HasFile)
            {
                string fileName = Path.GetFileName(uplImages.PostedFile.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/") + fileName);
                string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(fileName));
                apartmentPicture = new ApartmentPicture(path, encodedStr, fileName, true);
            }
        }
    }
}