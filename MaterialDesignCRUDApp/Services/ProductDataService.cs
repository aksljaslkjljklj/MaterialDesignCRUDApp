using MaterialDesignCRUDApp.Models;
using MaterialDesignCRUDApp.SQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignCRUDApp.Services
{
    public class ProductDataService: GenericDataService<Product>
    {
        public override async Task<IEnumerable<Product>> GetListAsync()
        {
            using (var ctx = new SqlDataContext())
            {
                return await ctx.Products.Include(c => c.Category).ToListAsync();
            }
        }
    }
}
