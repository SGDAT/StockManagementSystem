using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Data.Models;
using StockManagementSystem.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Data
{
    public interface IDataManager
    {
        IEnumerable<BatchViewModel> GetBatches();
        BatchViewModel GetBatch(int? batchId);
        Task NewBatch(BatchViewModel batch);
        Task EditBatch(BatchViewModel batch);
        Task DeleteBatch(int? batchId);

        IEnumerable<StockViewModel> GetStocks();
    }

    public class DataManager : IDataManager
    {
        private readonly ApplicationDbContext applicationDbContext;

        public DataManager(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IEnumerable<BatchViewModel> GetBatches()
        {
            IEnumerable<BatchViewModel> results = applicationDbContext.Batches.Select(b => new BatchViewModel
            {
                Id = b.Id,
                Fruit = b.Fruit,
                Quantity = b.Quantity,
                Variety = b.Variety,
            });

            return results;
        }

        public BatchViewModel GetBatch(int? batchId)
        {
            Batch batch = applicationDbContext.Batches.FirstOrDefault(b => b.Id == batchId);
            BatchViewModel result = new BatchViewModel
            {
                Id = batch.Id,
                Fruit = batch.Fruit,
                Quantity = batch.Quantity,
                Variety = batch.Variety,
            };

            return result;
        }

        public async Task NewBatch(BatchViewModel batch)
        {
            Batch newBatch = new Batch
            {
                Fruit = batch.Fruit,
                Quantity = batch.Quantity,
                Variety = batch.Variety
            };
            applicationDbContext.Batches.Add(newBatch);

            await applicationDbContext.SaveChangesAsync();

            await UpdateStocks(batch);
        }

        private async Task UpdateStocks(BatchViewModel batch)
        {
            var existingBatches = applicationDbContext.Batches.Where(b => b.Fruit == batch.Fruit && b.Variety == batch.Variety);
            Stock stock = applicationDbContext.Stocks.FirstOrDefault(s => s.Fruit == batch.Fruit && s.Variety == batch.Variety);
            if (stock == null)
            {
                stock = new Stock
                {
                    Fruit = batch.Fruit,
                    Variety = batch.Variety,
                    Quantity = existingBatches.AsEnumerable().Sum(b => b.Quantity)
            };
                applicationDbContext.Stocks.Add(stock);
            }
            else
                stock.Quantity = existingBatches.AsEnumerable().Sum(b => b.Quantity);

            await applicationDbContext.SaveChangesAsync();
        }

        public async Task EditBatch(BatchViewModel batch)
        {
            Batch existingBatch = applicationDbContext.Batches.FirstOrDefault(b => b.Id == batch.Id);
            existingBatch.Fruit = batch.Fruit;
            existingBatch.Quantity = batch.Quantity;
            existingBatch.Variety = batch.Variety;
            try
            {
                await applicationDbContext.SaveChangesAsync();

            }
            catch(DbUpdateConcurrencyException)
            {
                if (!BatchExists(batch.Id))
                {
                    throw new Exception("Id Exists");
                }
                else
                {
                    throw;
                }
            }

            await UpdateStocks(batch);
        }

        public async Task DeleteBatch(int? batchId)
        {
            Batch existingBatch = applicationDbContext.Batches.FirstOrDefault(b => b.Id == batchId);
            applicationDbContext.Batches.Remove(existingBatch);

            await applicationDbContext.SaveChangesAsync();
        }

        private bool BatchExists(int id)
        {
            return applicationDbContext.Batches.Any(e => e.Id == id);
        }

        public IEnumerable<StockViewModel> GetStocks()
        {
            IEnumerable<StockViewModel> results = applicationDbContext.Stocks.Select(b => new StockViewModel
            {
                Id = b.Id,
                Fruit = b.Fruit,
                Quantity = b.Quantity,
                Variety = b.Variety,
            });

            return results;
        }
    }
}
