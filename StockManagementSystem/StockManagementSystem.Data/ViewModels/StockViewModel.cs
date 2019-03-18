using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StockManagementSystem.Data.ViewModels
{
    public class StockViewModel
    {
        [Display(Name = "StockId")]
        public int Id { get; set; }
        public string Fruit { get; set; }
        public string Variety { get; set; }
        public int Quantity { get; set; }

    }
}
