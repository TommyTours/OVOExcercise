using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace OVOExcercise
{

    [DataContract]
    public class AllCustomers
    {
        [DataMember]
        public List<CustomerAttributes> customers { get; set; }
    }
}