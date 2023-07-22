
using MrUnrealData.Context;
using MrUnrealData.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MrUnreal.Controllers
{

    public class AdminController : Controller
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ChangeCreation(Creation product)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = $"https://localhost:44320/api/Creation/PutCreation/{product.Id}";

                    string json = JsonConvert.SerializeObject(product);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("CreationPanel", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("EditCreation", "Admin");
                    }
                }
            }
            else
            {
                return View(product);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditCreation(int id)
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
                    return RedirectToAction("Home", "Home");
                }
            }
        }

        [Authorize]
        public async Task<ActionResult> CreationPanel()
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
                    return RedirectToAction("AdminPage", "Account");
                }
            }
        }

        [Authorize]
        public ActionResult AddCreation()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddCreation(Creation creation, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid && imageFile != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(imageFile.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imageFile.ContentLength);
                }

                creation.Image = imageData;

                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://localhost:44320/api/Creation/PostCreation";

                    string json = JsonConvert.SerializeObject(creation);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("CreationPanel", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("AddCreation", "Admin");
                    }
                }
            }
            else
            {
                return View(creation);
            }
        }

        [Authorize]
        public async Task<ActionResult> DeleteCreation(int? id)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"https://localhost:44320/api/Creation/DeleteCreation/{id}";

                HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("CreationPanel", "Admin");
                }
                else
                {
                    return View();
                }
            }
        }
    }
}