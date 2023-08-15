using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.Entities
{
    public class City
    {
        public int city_id { get; set; }
        public int countory_id { get; set; }
        public string city_code { get; set; }
        public string city_name { get; set; }
    }
}