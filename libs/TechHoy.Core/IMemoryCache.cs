namespace TechHoy.Core
{
    public interface IMemoryCache
    {
        Task PutAsync(string address, string value);
        Task<string> GetAsync(string address);
    }
}
