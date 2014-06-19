using System.Collections.Generic;
using SportShop.Domain.Entities;

namespace SportShop.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PaginingInfo PaginingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}