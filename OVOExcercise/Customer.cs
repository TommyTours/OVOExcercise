using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace OVOExcercise
{
    public class CustomerAttributes
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string title { get; set; }
    }

    [DataContract]
    public class Customer
    {
        [DataMember]
        public CustomerAttributes customer { get; set; }
    }
}