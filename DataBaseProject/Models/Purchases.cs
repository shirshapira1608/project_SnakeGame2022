using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class Purchases
    {
        public int UserId { get; set; }
        public string Product { get; set; }
        public int ProductSerialNumber { get; set; }
        public Purchases(int UserId, string product, int ProductSerialNamber)
        {
            this.UserId = UserId;
            this.Product = product;
            this.ProductSerialNumber = ProductSerialNamber;
        }
    }
}
