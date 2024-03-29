﻿using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using SportSrore.Models;
using SportStore.Models;

namespace SportSrore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
            
        
    }
}
