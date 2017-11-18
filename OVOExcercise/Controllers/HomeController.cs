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

        List<CustomerAttributes> customerList;

        // GET: Home
        public ActionResult Index()
        {
            //using the WebClient to get the JSON data
            var content = syncClient.DownloadString(url + "customers");

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AllCustomers));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                var customerData = (AllCustomers)serializer.ReadObject(ms);
                customerList = customerData.customers;
            }

            HomeViewModel vm = new HomeViewModel() { Customers = customerList };
         
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel model)
        {
            var content = syncClient.DownloadString(url + "customer?id=" + model.Id);

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Customer));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                var customerData = (Customer)serializer.ReadObject(ms);
                customerList = new List<CustomerAttributes>();
                customerList.Add(customerData.customer);
            }
            

            return View(new HomeViewModel() { Customers = customerList });
        }
    }
}