namespace ReactWithASP.Server.Services;
public class GetProgrammeService(AppDbContext context) : IGetService<ProgrammeDto>
{
    public async Task<List<ProgrammeDto>> GetAll()
    {
        var programmes = await context.Programmes.ToListAsync();
        List<ProgrammeDto> results = [];
        foreach (var programme in programmes)
        {
            results.Add(MapDto(programme));
        }
        return results;
    }

    public async Task<ProgrammeDto> Get(int id)
    {
        var programme = await context.Programmes.FirstOrDefaultAsync(i => i.Id == id);
        return MapDto(programme);
    }

    private ProgrammeDto MapDto(Programme programme)
        => new ProgrammeDto(programme.Id, programme.Title, programme.Description);
}