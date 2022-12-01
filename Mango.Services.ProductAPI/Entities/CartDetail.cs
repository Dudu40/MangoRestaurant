using Mango.Services.API.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.API.Entities
{
    public class CartDetail
    {
        [Key]
        public int Id { get; set; }
        public int HeaderId { get; set; }
        [ForeignKey("HeaderId")]
        public virtual CartHeader Header { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product {get;set;}
        public int Quantity { get; set; }
    }
}
