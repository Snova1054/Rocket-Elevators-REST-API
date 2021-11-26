using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketElevatorsRESTAPI.Models
{
    public class Address
    {
        public int id { get; set; }
        public string address { get; set; }
        public string entity_type { get; set; }
        public string status { get; set; }
        public string date_of_commissioning { get; set; }
        public string date_of_last_inspection { get; set; }
        public string certificate_of_operations { get; set; }
        public string information { get; set; }
        public string notes { get; set; }
       
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public int building_id { get; set; }
        public int customer_id { get; set; }
        public int address_id { get; set; }
    }
}