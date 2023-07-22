
using MrUnrealData.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MrUnreal.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public async Task<ActionResult> Gallery()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://localhost:44320/api/Creation/GetAllCreations";

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    IEnumerable<Creation> creation = JsonConvert.DeserializeObject<IEnumerable<Creation>>(json);
                    return View(creation);
                }
                else
                {
                    return RedirectToAction("Home", "Home");
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> CreationPage(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"https://localhost:44320/api/Creation/GetCreation/{id}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Creation creation = JsonConvert.DeserializeObject<Creation>(json);
                    return View(creation);
                }
                else
                {
                    return RedirectToAction("Gallery", "Store");
                }
            }
        }

        public PartialViewResult LittleMenu()
        {
            return PartialView();
        }
    }
}