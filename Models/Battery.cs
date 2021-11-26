using System;
using System.Text.Json.Serialization;

namespace RocketElevatorsRESTAPI.Models
{
    public class Battery
    {
        public int id { get; set; }
        public string entity_type { get; set; }
        public string status { get; set; }
        public DateTime date_of_commissioning { get; set; }
        public DateTime date_of_last_inspection { get; set; }
        public string certificate_of_operations { get; set; }
        public string information { get; set; }
        public string notes { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        [JsonIgnore]
        public int building_id { get; set; }
    }
} 

// dotnet aspnet-codegenerator controller -name QuotesController -async -api -m quote -dc TodoContext -outDir Controllers  