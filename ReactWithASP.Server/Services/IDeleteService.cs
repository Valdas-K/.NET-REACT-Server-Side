namespace ReactWithASP.Server.Services;
public interface IDeleteService<T>
{
    Task Delete(int Id, T dto);
}