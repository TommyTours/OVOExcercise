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

            //Creating a serializer to convert the data from into a usable format.
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AllCustomers));
            //Using the serializer to convert the JSON data and create a list of customers that can be used to populate the view.
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
            //Clearing the model state so that when the view is returned the searchbox will be empty.
            ModelState.Clear();
            //using the WebClient to get the JSON data
            var content = syncClient.DownloadString(url + "customer?id=" + model.Id);

            //Creating a serializer to convert the data from into a usable format.
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Customer));
            //Using the serializer to convert the JSON data and create a list of customers that can be used to populate the view.
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                var customerData = (Customer)serializer.ReadObject(ms);
                //Checking that the submitted value has returned a valid customer, otherwise an error is added to the model state which is the displayed on the view.
                if (customerData.customer.id == "")
                {
                    ModelState.AddModelError("Customer Not Found", "Customer ID: " + model.Id + " not found.");
                }
                customerList = new List<CustomerAttributes>();
                customerList.Add(customerData.customer);
            }

            return View(new HomeViewModel() { Customers = customerList });
        }
    }
}