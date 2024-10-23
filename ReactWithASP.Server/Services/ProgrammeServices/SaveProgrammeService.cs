namespace ReactWithASP.Server.Services.ProgrammeServices;

public class SaveProgrammeService(AppDbContext context) : ISaveService<ProgrammeDto>
{
    public async Task Store(ProgrammeDto dto)
    {
        var programme = new Programme(dto.Title, dto.Description);
        context.Programmes.Add(programme);
        await context.SaveChangesAsync();
    }

    public async Task Update(int Id, ProgrammeDto dto)
    {
        var programme = await context.Programmes.FirstOrDefaultAsync(i => i.Id == Id);
        if (programme != null)
        {
            programme.SetValues(dto.Title, dto.Description);
            context.Programmes.Update(programme);
            await context.SaveChangesAsync();
        }
    }
}