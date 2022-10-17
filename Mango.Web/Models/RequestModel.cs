using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Models
{
    //this class is to store informations of the request
    public class RequestModel
    {
        public Utils.ApiType ApiType { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
