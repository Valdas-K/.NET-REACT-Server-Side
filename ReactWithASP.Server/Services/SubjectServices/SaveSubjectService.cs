namespace ReactWithASP.Server.Services.SubjectServices;

public class SaveSubjectService(AppDbContext context) : ISaveService<SubjectDto>
{
    public async Task Store(SubjectDto dto)
    {
        var subject = new Subject(dto.Title, dto.Description);
        context.Subjects.Add(subject);
        await context.SaveChangesAsync();
    }

    public async Task Update(int Id, SubjectDto dto)
    {
        var subject = await context.Subjects.FirstOrDefaultAsync(i => i.Id == Id);
        if (subject != null)
        {
            subject.SetValues(dto.Title, dto.Description);
            context.Subjects.Update(subject);
            await context.SaveChangesAsync();
        }
    }
}