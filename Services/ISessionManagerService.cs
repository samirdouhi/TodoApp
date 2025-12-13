namespace TodoApp.Services
{
    public interface ISessionManagerService
    {
        public void Add(string key, object obj, HttpContext context);
        public T Get<T>(string key, HttpContext context);


    }
}
