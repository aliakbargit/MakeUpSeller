using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeUpSeller.Models
{
    public class IndexPageViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public IEnumerable<Completed> Competeds { get; set; }

        public double Price => OrderItems.Sum(o=>o.Price);
        public double Total => Competeds.Sum(c => c.Price);

    }
}
