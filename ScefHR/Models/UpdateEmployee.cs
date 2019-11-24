using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Models
{
    public class UpdateEmployee
    {
        [JsonProperty("firstName")]
        public string Firstname { get; set; }
        [JsonProperty("lastName")]
        public string Lastname { get; set; }
        [JsonProperty("branch")]
        public string Branch { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("nationality")]
        public string Nationality { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("birthdate")]
        public string Birthdate { get; set; }
        [JsonProperty("position")]
        public string Position { get; set; }
        [JsonProperty("salary")]
        public int Salary { get; set; }
        [JsonProperty("hireDate")]
        public string HireDate { get; set; }
    }
}
