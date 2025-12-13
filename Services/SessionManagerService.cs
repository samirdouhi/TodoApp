using System.Collections.Generic;
using System.Text.Json;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class SessionManagerService : ISessionManagerService
    {
        public void Add(string key, object obj,HttpContext context )
        {
            String Todos = JsonSerializer.Serialize(obj);
            context.Session.SetString(key, Todos);
        }
        public T Get<T>(string key,  HttpContext context)
        {
         
           return JsonSerializer.Deserialize<T>(context.Session.GetString(key)??"[]");
        }

     
    }
}
