using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVOExcercise.Models
{
    public class HomeViewModel
    {
        public List<Customer> Customers { get; private set; }
        public string Id { get; set; }

        public HomeViewModel(List<Customer> customers)
        {
            Customers = customers;
        }


    }
}