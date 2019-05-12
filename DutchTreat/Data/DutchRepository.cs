using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            this._ctx = ctx;
            this._logger = logger;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                      .Include(o => o.Items)
                      .ThenInclude(o => o.Product)
                      .ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _ctx.Products
                       .OrderBy(p => p.Title)
                       .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetAllProductsByCategory(string category)
        {
            try
            {
                return _ctx.Products
                .Where(p => p.Category == category)
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Get All Products By Category: {ex}");
                return null;
            }
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders
                     .Include(o => o.Items)
                     .ThenInclude(o => o.Product)
                     .Where(o => o.Id == id)
                     .FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
