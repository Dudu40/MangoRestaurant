namespace Mango.Web.Models
{
    public class ShoppingCartModel
    {
        public CartHeaderModel cartHeader { get; set; }
        
        public List<CartDetailModel> cartDetails { get; set; }

        public double TotalPrice { get; set; }

        public double CouponAmount { get; set; }
    }
}
