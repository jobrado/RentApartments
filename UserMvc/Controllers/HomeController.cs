using CaptchaMvc.HtmlHelpers;
using DAL;
using DAL.Model;
using DAL.ViewModel;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace UserMvc.Controllers
{
    public class HomeController : Controller
    {

        private static IEnumerable<Apartment> allapartments = RepositoryFactory.GetRepository().GetApartments(null, null, null);
        private static IEnumerable<Apartment> apartments;

       
        public ActionResult Index(string sortOrder)
        {
            SetCookie("sort", sortOrder);
            apartments = GetListOfApartments();
            ViewBag.PriceSortParm = string.IsNullOrEmpty(sortOrder) ? "price_desc" : "price_asc";
            ViewBag.IdSortParm = string.IsNullOrEmpty(sortOrder) ? "id_asc" : "";
            if (Request.Cookies["sortOrder"] != null)
            {
                switch (Request.Cookies["sortOrder"].Value)
                {
                    case "price_desc":
                        apartments = apartments.OrderByDescending(a => a.Price);
                        break;
                    case "price_asc":
                        apartments = apartments.OrderBy(a => a.Price);
                        break;

                    case "id_asc":
                        apartments = apartments.OrderBy(a => a.IDApartment);
                        break;

                    default:
                        apartments = apartments.OrderBy(s => s.IDApartment);
                        break;
                }

            }

            if (!string.IsNullOrEmpty(sortOrder) && Request.Cookies["sortOrder"] == null)
            {
                switch (sortOrder)
                {
                    case "price_desc":
                        apartments = apartments.OrderByDescending(a => a.Price);
                        break;
                    case "price_asc":
                        apartments = apartments.OrderBy(a => a.Price);
                        break;

                    case "id_asc":
                        apartments = apartments.OrderBy(a => a.IDApartment);
                        break;

                    default:
                        apartments = apartments.OrderBy(s => s.IDApartment);
                        break;
                }

            }
            return View(apartments);
        }
     
          [HttpPost]
        public ActionResult Index( string searchString, string searchAdults, string searchChildren, string searchRooms)
        {
          
            SetCookie("city", searchString);
            SetCookie("adults", searchAdults);
            SetCookie("children", searchChildren);
            SetCookie("rooms", searchRooms);

           


            if (Request.Cookies["searchString"] != null)
            {
                apartments = apartments.Where(a => a.CityId.Name.Contains(Request.Cookies["searchString"].Value));
            }
            if (Request.Cookies["searchRooms"] != null)
            {
                apartments = apartments.Where(a => a.TotalRooms.ToString().Contains(Request.Cookies["searchRooms"].Value));
            }
            if (Request.Cookies["searchChildren"] != null)
            {
                apartments = apartments.Where(a => a.MaxChildren.ToString().Contains(searchChildren));
            }
            if (Request.Cookies["searchAdults"] != null)
            {
                apartments = apartments.Where(a => a.MaxAdults.ToString().Contains(Request.Cookies["searchAdults"].Value));
            }

            if (!string.IsNullOrEmpty(searchString) && Request.Cookies["searchString"] == null)
            {
                apartments = apartments.Where(a => a.CityId.Name.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(searchRooms) && Request.Cookies["searchRooms"] == null)
            {
                apartments = apartments.Where(a => a.TotalRooms.ToString().Contains(searchRooms));
            }
            if (!string.IsNullOrEmpty(searchChildren) && Request.Cookies["searchChildren"] == null)
            {
                apartments = apartments.Where(a => a.MaxChildren.ToString().Contains(searchChildren));
            }
            if (!string.IsNullOrEmpty(searchAdults) && Request.Cookies["searchAdults"] == null)
            {
                apartments = apartments.Where(a => a.MaxAdults.ToString().Contains(searchAdults));
            }
            return View(apartments);
        }
        private void SetCookie(string name, string value)
        {
            if (Request.Cookies[name] == null)
            {
                HttpCookie cookie_ = new HttpCookie(name)
                {
                    Value = value,
                    Expires = DateTime.Now.AddDays(1)
                };
                Response.Cookies.Add(cookie_);
                Response.SetCookie(cookie_);
            }

        }

        public IEnumerable<Apartment> GetListOfApartments()
        {
            IEnumerable<Apartment> apartments = allapartments.ToList().FindAll(a => (int)a.StatusId == 3);
            foreach (var item in apartments)
            {
                item.Pictures = RepositoryFactory.GetRepository().GetPictures(item.IDApartment);
                item.Tag = RepositoryFactory.GetRepository().GetApartmantTags(item.IDApartment);
            }
            return apartments;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your provider for best Croatian apartments. Rent now! Enjoy tomorrow!";

            return View();
        }

        public ActionResult Details(int? id)
        {
            return CommonAction(id);
        }

        private ActionResult CommonAction(int? id)
        {

            var star = new Stars
            {
                Question = "Ocijenite apartman"
            };

            var detailsVM = new DAL.ViewModel.DetailsViewModel
            {
                apartment = RepositoryFactory.GetRepository().GetApartments(null, null, null)
                   .FirstOrDefault(a => a.IDApartment == id),
                user = Session["user"] as User,
                Stars = star,
                Review = new ApartmentReview()
            };
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            int nonNullId = id ?? -1;
            IList<Tag> tags = new List<Tag>();
            tags = RepositoryFactory.GetRepository().GetApartmantTags(nonNullId);
            if (tags != null)
            {
                detailsVM.apartment.Tag = tags;
            }
            

                List<Pictures> pics = RepositoryFactory.GetRepository().GetPictures(nonNullId);
            detailsVM.apartment.Pictures = pics;
            Console.Write(detailsVM.apartment.Pictures.Count);
            if (detailsVM.apartment == null)
            {
                return HttpNotFound();
            }
            return View(detailsVM);
        }

        public ActionResult FillContactForm()
        {
            var detailsVM = new DAL.ViewModel.DetailsViewModel
            {
                user = RepositoryFactory.GetRepository().GetUser(Request.Cookies["user_username"]?.Value, Request.Cookies["user_password"]?.Value)
            };
            return View(detailsVM);
        }
        public ActionResult FillContactFormForNonReg()
        {
            DetailsViewModel detailsVM = new DetailsViewModel();
            return View(detailsVM);
        }
        [HttpPost]
        public ActionResult FillContactForm(User user, Apartment apartment, string children, string datestart, string dateend)
        {

            SendMail(user, apartment, children, datestart, dateend);

            return Json(new { success = true });

        }

        private void SendMail(User user, Apartment apartment, string children, string datestart, string dateend)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Reservation request", "jobrado@racunarstvo.hr"));

                    message.To.Add(new MailboxAddress("Reservation request", "jobrado@racunarstvo.hr"));

                    message.Subject = "Reservation request ";
                    message.Body = new TextPart("plain")
                    {

                        Text = user.UserName + " wants to reserve apartmant " + apartment.Name + "! Email: " + user.Email + "! Address: " + user.Address + ", Phone number: " + user.PhoneNumber + "! Number of children: " + children + "! date start: " + datestart + " till " + dateend
                    };
                    smtp.Connect("smtp.office365.com", 587, false);
                    smtp.Authenticate("jobrado@racunarstvo.hr", "Vanja1989");

                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult FillContactFormForNonReg(User user, Apartment apartment, string children, string datestart, string dateend)
        {
            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                ViewBag.ErrMessage = "Error: captcha is not valid.";
                return Json(new { success = false });
            }
            else
            {
                SendMail(user, apartment, children, datestart, dateend);

                return Json(new { success = true });
            }
        }
        [HttpPost]
        public ActionResult ChangeLanguage(string language)
        {

            HttpCookie cookie = new HttpCookie("culture");
            cookie.Value = language;
            cookie.Expires = DateTime.Now.AddYears(1);

            Response.Cookies.Add(cookie);
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult UpdateRating(int rating, int apartmentid, int userid, string reviewText)
        {
            ApartmentReview ar = new ApartmentReview
            {
                UserId = userid,
                Stars = rating,
                ApartmentId = apartmentid,
                Details = reviewText
            };
            RepositoryFactory.GetRepository().CreateReview(ar);

            return Json(new { success = true });
        }

     
        
    }
}