namespace ReactWithASP.Server.Services.LecturerServices;

public class SaveLecturerService(AppDbContext context) : ISaveService<LecturerDto>
{
    public async Task Store(LecturerDto dto)
    {
        var lecturer = new Lecturer(dto.FirstName, dto.LastName, dto.Email, dto.Qualification);
        context.Lecturers.Add(lecturer);
        await context.SaveChangesAsync();
    }

    public async Task Update(int Id, LecturerDto dto)
    {
        var lecturer = await context.Lecturers.FirstOrDefaultAsync(i => i.Id == Id);
        if (lecturer != null)
        {
            lecturer.SetValues(dto.FirstName, dto.LastName, dto.Email, dto.Qualification);
            context.Lecturers.Update(lecturer);
            await context.SaveChangesAsync();
        }
    }
}