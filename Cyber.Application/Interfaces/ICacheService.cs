namespace Cyber.Application.Interfaces;

public interface ICacheService
{
    public void Set(string key, object value, TimeSpan expiration);
    public T? Get<T>(string key);
    public void Remove(string key);
}