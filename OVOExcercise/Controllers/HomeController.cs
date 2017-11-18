using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Web.Helpers;
using OVOExcercise.Models;

namespace OVOExcercise.Controllers
{
    public class HomeController : Controller
    {
        static string url = "https://sheltered-depths-66346.herokuapp.com/";

        static WebClient syncClient = new WebClient();
        // GET: Home
        public ActionResult Index()
        {
            //using the WebClient to get the JSON data
            var content = syncClient.DownloadString(url + "customers");

            List<Customer> customerList;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Customers));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                var customerData = (Customers)serializer.ReadObject(ms);
                customerList = customerData.customers;
            }

            HomeViewModel vm = new HomeViewModel(customerList);
         
            return View(vm);
        }
    }
}