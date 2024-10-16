namespace ReactWithASP.Server.Services;
public interface ISaveService<T>
{
    Task Store(T dto);
    Task Update(int Id, T dto);
    Task Delete(int Id, T dto);
}