namespace ReactWithASP.Server.Services.GroupServices;

public class DeleteGroupService(AppDbContext context) : IDeleteService<GroupDto>
{
    public async Task Delete(int Id, GroupDto dto)
    {
        var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == Id);
        if (group != null)
        {
            //group.SetValues(dto.Title);
            context.Groups.Remove(group);
            await context.SaveChangesAsync();
        }
    }
}