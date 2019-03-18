using System;
using System.Collections.Generic;
using System.Text;

namespace StockManagementSystem.Data.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public string Fruit { get; set; }
        public string Variety { get; set; }
        public int Quantity { get; set; }

    }
}
