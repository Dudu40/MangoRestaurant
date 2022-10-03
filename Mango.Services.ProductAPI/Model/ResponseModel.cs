namespace Mango.Services.ProductAPI.Model
{
    public class ResponseModel
    {
        public bool isSucces { get; set; } = true;
        public object result { get; set; }
        public string DisplayMessage { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
