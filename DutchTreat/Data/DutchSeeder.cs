﻿using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;

        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting)
        {
            this._ctx = ctx;
            this._hosting = hosting;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Products.Any())
            {
                var file = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(file);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                _ctx.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity=5,
                            UnitPrice= products.First().Price
                        }
                    }
                };


                _ctx.Orders.Add(order);

                _ctx.SaveChanges();
            }
        }
    }
}
