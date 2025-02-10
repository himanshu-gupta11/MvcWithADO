using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMVC_ADO.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }
            public int QTY { get; set; }
        public string Remarks { get; set; }
    }
}