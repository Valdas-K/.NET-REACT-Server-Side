namespace ReactWithASP.Server.Services;
public interface ISaveStudentService
{
    Task Store(StudentDto dto);
    Task Update(int Id, StudentDto dto);
}