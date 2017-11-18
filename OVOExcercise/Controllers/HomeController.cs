using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace OVOExcercise.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getCustomers()
        {
            var url = "https://sheltered-depths-66346.herokuapp.com/customers";

            var syncClient = new WebClient();
            var content = syncClient.DownloadString(url);

            return View();
        }
    }
}