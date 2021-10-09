using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyCashFlow
{
    [DataContract]
    public class Finance
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}