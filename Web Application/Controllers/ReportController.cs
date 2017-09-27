using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web_Application.Helpers;

namespace Web_Application.Controllers
{
    public class ReportController : Controller
    {
        private HttpClient client = new HttpClient();
        private HttpConfig conf = new HttpConfig();

        [Authorize]
        public ActionResult Index()
        {
            Dictionary<string, int> categoriesAmount = new Dictionary<string, int>();
            conf.SetClientGETSettings(client);

            HttpResponseMessage response = client.GetAsync("api/Report").Result;
            if (response.IsSuccessStatusCode)
            {
                categoriesAmount = response.Content.ReadAsAsync<Dictionary<string, int>>().Result;
            }

            return View(categoriesAmount);
        }
    }
}