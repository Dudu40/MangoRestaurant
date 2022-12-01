using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.API.Models
{
    public class ShoppingCartModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("HeaderId")]
        public int HeaderId { get; set; }
        public virtual CartHeaderModel CartHeader { get; set; }

        public virtual List<CartDetailModel> CartDetails { get; set; }
    }
}
