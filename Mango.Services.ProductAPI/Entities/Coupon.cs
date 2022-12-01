using System.ComponentModel.DataAnnotations;

namespace Mango.Services.API.Entities
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public double Amount { get; set; }

    }
}
