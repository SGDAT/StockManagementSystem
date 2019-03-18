using System;
using System.Collections.Generic;
using System.Text;

namespace StockManagementSystem.Data.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<StockViewModel> stockViewModels { get; set; }
    }
}
