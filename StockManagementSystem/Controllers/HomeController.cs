using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Data;
using StockManagementSystem.Data.ViewModels;
using StockManagementSystem.Models;

namespace StockManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataManager dataManager;

        public HomeController(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                stockViewModels = dataManager.GetStocks().OrderBy(s=>s.Fruit)
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
