using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Data;
using StockManagementSystem.Data.Models;
using StockManagementSystem.Data.ViewModels;

namespace StockManagementSystem.Controllers
{
    [Authorize]
    public class BatchesController : Controller
    {
        private readonly IDataManager dataManager;

        public BatchesController(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        // GET: Batches
        public async Task<IActionResult> Index()
        {
            return View(dataManager.GetBatches());
        }

        // GET: Batches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BatchViewModel batch = dataManager.GetBatch(id);
            
            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }

        // GET: Batches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Batches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BatchViewModel batch)
        {
            if (ModelState.IsValid)
            {
                await dataManager.NewBatch(batch);
                return RedirectToAction(nameof(Index));
            }
            return View(batch);
        }

        // GET: Batches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BatchViewModel batch = dataManager.GetBatch(id);
            if (batch == null)
            {
                return NotFound();
            }
            return View(batch);
        }

        // POST: Batches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BatchViewModel batch)
        {
            if(batch == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await dataManager.EditBatch(batch);
                return RedirectToAction(nameof(Index));
            }
            return View(batch);
        }

        // GET: Batches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BatchViewModel batch = dataManager.GetBatch(id);

            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }

        // POST: Batches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await dataManager.DeleteBatch(id);

            return RedirectToAction(nameof(Index));
        }


    }
}
