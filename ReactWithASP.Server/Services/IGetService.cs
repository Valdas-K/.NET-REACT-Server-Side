namespace ReactWithASP.Server.Services;
public interface IGetService<T>
{
    Task<List<T>> GetAll();
    Task<T> Get(int id);
}