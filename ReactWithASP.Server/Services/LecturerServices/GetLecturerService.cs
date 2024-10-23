namespace ReactWithASP.Server.Services.LecturerServices;
public class GetLecturerService(AppDbContext context) : IGetService<LecturerDto>
{
    public async Task<List<LecturerDto>> GetAll()
    {
        var lecturers = await context.Lecturers.ToListAsync();
        List<LecturerDto> results = [];
        foreach (var lecturer in lecturers)
        {
            results.Add(MapDto(lecturer));
        }
        return results;
    }

    public async Task<LecturerDto> Get(int id)
    {
        var lecturer = await context.Lecturers.FirstOrDefaultAsync(i => i.Id == id);
        return MapDto(lecturer);
    }

    private LecturerDto MapDto(Lecturer lecturer)
        => new(lecturer.Id, lecturer.FirstName, lecturer.LastName, lecturer.Email, lecturer.Qualification);
}