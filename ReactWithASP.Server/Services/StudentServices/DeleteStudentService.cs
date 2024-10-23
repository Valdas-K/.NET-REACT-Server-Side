namespace ReactWithASP.Server.Services.StudentServices;

public class DeleteStudentService(AppDbContext context) : IDeleteService<StudentDto>
{
    public async Task Delete(int Id, StudentDto dto)
    {
        var student = await context.Students.FirstOrDefaultAsync(i => i.Id == Id);
        if (student != null)
        {
            context.Students.Remove(student);
            await context.SaveChangesAsync();
        }
    }
}