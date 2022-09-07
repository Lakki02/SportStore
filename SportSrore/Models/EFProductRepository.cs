﻿using System.Collections.Generic;
using System.Linq;

namespace SportSrore.Models
{
    public class EFProductRepository :IProductRepository
    {
        private ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}
