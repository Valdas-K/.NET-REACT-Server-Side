namespace ReactWithASP.Server.Services.ProgrammeServices;

public class DeleteProgrammeService(AppDbContext context) : IDeleteService<ProgrammeDto>
{
    public async Task Delete(int Id, ProgrammeDto dto)
    {
        var programme = await context.Programmes.FirstOrDefaultAsync(i => i.Id == Id);
        if (programme != null)
        {
            //programme.SetValues(dto.Title, dto.Description);
            context.Programmes.Remove(programme);
            await context.SaveChangesAsync();
        }
    }
}