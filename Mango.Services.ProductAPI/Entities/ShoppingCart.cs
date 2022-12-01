using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.API.Entities
{
    public class ShoppingCart
    {
        [Key]
        public CartHeader CartHeader { get; set; }
        public virtual List<CartDetail> CartDetails { get; set; }
    }
}
