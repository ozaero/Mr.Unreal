using MrUnrealData.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MrUnreal.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://localhost:44320/api/User/GetAllUsers";

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    IEnumerable<User> userList = JsonConvert.DeserializeObject<IEnumerable<User>>(json);

                    var users = userList.ToList();

                    var login = users.FirstOrDefault(x => x.Name == user.Name && x.Password == user.Password);

                    if (login != null)
                    {
                        FormsAuthentication.SetAuthCookie(login.Name, false);
                        Session["UserName"] = login.Name;

                        return RedirectToAction("AdminPage", "Account");
                    }
                    else
                    {
                        return View(user);
                    }
                }
                else
                {
                    return View(user);
                }
            }
        }


        [Authorize]
        public ActionResult AdminPage()
        {
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Home", "Home");
        }
    }
}