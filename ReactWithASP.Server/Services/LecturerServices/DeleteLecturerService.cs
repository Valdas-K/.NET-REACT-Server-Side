namespace ReactWithASP.Server.Services.LecturerServices;

public class DeleteLecturerService(AppDbContext context) : IDeleteService<LecturerDto>
{
    public async Task Delete(int Id, LecturerDto dto)
    {
        var lecturer = await context.Lecturers.FirstOrDefaultAsync(i => i.Id == Id);
        if (lecturer != null)
        {
            //lecturer.SetValues(dto.FirstName, dto.LastName, dto.Email, dto.Qualification);
            context.Lecturers.Remove(lecturer);
            await context.SaveChangesAsync();
        }
    }
}