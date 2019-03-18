using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Data;

namespace StockManagementSystem.Controllers
{
    [Authorize]
    public class StocksController : Controller
    {
        private readonly IDataManager dataManager;

        public StocksController(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return View(dataManager.GetStocks());
        }
    }
}