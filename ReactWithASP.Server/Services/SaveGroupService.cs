namespace ReactWithASP.Server.Services;

public class SaveGroupService(AppDbContext context) : ISaveService<GroupDto>
{
    public async Task Store(GroupDto dto)
    {
        var group = new Group(dto.Title);
        context.Groups.Add(group);
        await context.SaveChangesAsync();
    }

    public async Task Update(int Id, GroupDto dto)
    {
        var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == Id);
        if (group != null)
        {
            group.SetValues(dto.Title);
            context.Groups.Update(group);
            await context.SaveChangesAsync();
        }
    }
    public async Task Delete(int Id, GroupDto dto)
    {
        var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == Id);
        if (group != null)
        {
            group.SetValues(dto.Title);
            context.Groups.Remove(group);
            await context.SaveChangesAsync();
        }
    }
}