using Mango.Web.Model;
using System;

namespace Mango.Web.Models
{
    public class CartDetailModel
    {
        public ProductModel Product { get; set; }

        public int Quantity { get; set; }

        public string UserId { get; set; }
    }
}