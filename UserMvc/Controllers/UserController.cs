using DAL;
using DAL.Model;
using DAL.ViewModel;
using Microsoft.Ajax.Utilities;
using System;
using System.Web;
using System.Web.Mvc;

namespace UserMvc.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult LogInUser()
        {
            if (Request.Cookies["user_password"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction(controllerName: "Home", actionName: "Index");
            }
        }
        [HttpPost]
        public ActionResult LogInUser(UserVM user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            string role = RepositoryFactory.GetRepository().GetUserRole(user.UserName, user.Password);
            if (!role.ToLower().Equals("user") || role.IsNullOrWhiteSpace())
            {
                ViewBag.IsValid = false;
                return View(user);
            }
            else
            {
                User u = RepositoryFactory.GetRepository().GetUser(user.UserName, user.Password);

                ViewBag.IsValid = true;
                HttpContext.Session["user"] = u;
                HttpCookie cookie_username = new HttpCookie("user_username")
                {
                    Value = user.UserName,
                    Expires = DateTime.Now.AddDays(1)
                };
                HttpCookie cookie_id = new HttpCookie("user_id")
                {
                    Value = u.Id.ToString(),
                    Expires = DateTime.Now.AddDays(1)
                };
                HttpCookie cookie_password = new HttpCookie("user_password")
                {
                    Value = user.Password,
                    Expires = DateTime.Now.AddDays(1)
                };
                HttpCookie cookie_email = new HttpCookie("user_email")
                {
                    Value = u.Email,
                    Expires = DateTime.Now.AddDays(1)
                };
                HttpCookie cookie_address = new HttpCookie("user_address")
                {
                    Value = u.Address,
                    Expires = DateTime.Now.AddDays(1)
                };
                HttpCookie cookie_phone = new HttpCookie("user_phone")
                {
                    Value = u.PhoneNumber,
                    Expires = DateTime.Now.AddDays(1)
                };

                Response.Cookies.Add(cookie_username);
                Response.Cookies.Add(cookie_id);
                Response.Cookies.Add(cookie_password);
                Response.Cookies.Add(cookie_email);
                Response.Cookies.Add(cookie_address);
                Response.Cookies.Add(cookie_phone);

                return RedirectToAction(controllerName: "Home", actionName: "Index");
            }
        }
        public ActionResult RegistrateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegistrateUser(User user)
        {
            if (!ModelState.IsValid)
            {

                return View(user);
            }
            user.Role = Role.User;

            RepositoryFactory.GetRepository().CreateUser(user);
            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }
        public ActionResult Check()
        {
            if (Request.Cookies["user_username"] != null)
            {
                HttpCookie nameCookie = Request.Cookies["user_password"];
                HttpCookie passCookie = Request.Cookies["user_username"];
                HttpCookie phoneCookie = Request.Cookies["user_phone"];
                HttpCookie idCookie = Request.Cookies["user_id"];
                HttpCookie addressCookie = Request.Cookies["user_address"];
                HttpCookie emailCookie = Request.Cookies["user_email"];
                nameCookie.Expires = DateTime.Now.AddDays(-1);
                passCookie.Expires = DateTime.Now.AddDays(-1);
                phoneCookie.Expires = DateTime.Now.AddDays(-1);
                idCookie.Expires = DateTime.Now.AddDays(-1);
                emailCookie.Expires = DateTime.Now.AddDays(-1);
                addressCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(nameCookie);
                Response.Cookies.Add(passCookie);
                Response.Cookies.Add(idCookie);
                Response.Cookies.Add(phoneCookie);
                Response.Cookies.Add(addressCookie);
                Response.Cookies.Add(emailCookie);

            }

            return RedirectToAction(actionName: "LogInUser", controllerName: "User");
        }
    }
}