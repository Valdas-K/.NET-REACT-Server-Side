namespace ReactWithASP.Server.Services.SubjectServices;

public class DeleteSubjectService(AppDbContext context) : IDeleteService<SubjectDto>
{
    public async Task Delete(int Id, SubjectDto dto)
    {
        var subject = await context.Subjects.FirstOrDefaultAsync(i => i.Id == Id);
        if (subject != null)
        {
            subject.SetValues(dto.Title, dto.Description);
            context.Subjects.Remove(subject);
            await context.SaveChangesAsync();
        }
    }
}