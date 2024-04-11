using CGBC_Travel_Group_90.Services;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace GBC_Travel_Group_90.Services
{

    //Implement the ISessionService interface in a class named SessionService. This class uses
    //IHttpContextAccessor to interact with the HTTP context and manage session data
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T GetSessionData<T>(string key)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var value = session.GetString(key);
            return value ==null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public void SetSessionData<T>(string key, T value)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString(key, JsonSerializer.Serialize(value));
        }
    }
}
