namespace ReactWithASP.Server.Services;
public class GetSubjectService(AppDbContext context) : IGetService<SubjectDto>
{
    public async Task<List<SubjectDto>> GetAll()
    {
        var subjects = await context.Subjects.ToListAsync();
        List<SubjectDto> results = [];
        foreach (var subject in subjects)
        {
            results.Add(MapDto(subject));
        }
        return results;
    }

    public async Task<SubjectDto> Get(int id)
    {
        var subject = await context.Subjects.FirstOrDefaultAsync(i => i.Id == id);
        return MapDto(subject);
    }

    private SubjectDto MapDto(Subject subject)
        => new SubjectDto(subject.Id, subject.Title, subject.Description);
}