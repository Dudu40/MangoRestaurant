using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<T> SendRequest<T>(RequestModel Request);
        void Dispose();

    }
}
