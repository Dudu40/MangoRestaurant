using Mango.Web.Model;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseModel responseModel { get; set; }
        public IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            responseModel = new ResponseModel();
            _httpClient = httpClient;
        }

        public async Task<T> SendRequest<T>(RequestModel apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("MangoAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }

                HttpResponseMessage apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case Utils.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Utils.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Utils.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;

            }
            catch (Exception e)
            {
                var dto = new ResponseModel
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
