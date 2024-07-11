using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductAPI.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public string description { get; set; }
    }
}