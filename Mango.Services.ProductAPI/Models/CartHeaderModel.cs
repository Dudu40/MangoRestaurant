using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.API.Models
{
    public class CartHeaderModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }

    }
}
