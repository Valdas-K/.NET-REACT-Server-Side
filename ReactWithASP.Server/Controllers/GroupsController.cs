namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GroupsController(IGetService<GroupDto> getGroupService, ISaveService<GroupDto> saveGroupService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getGroupService.GetAll();
        return Ok(results);
    }

    [HttpPut("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put(int id, GroupDto dto)
    {
        await saveGroupService.Update(id, dto);
        return Ok();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post(GroupDto dto)
    {
        await saveGroupService.Store(dto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int id, GroupDto dto)
    {
        await saveGroupService.Delete(id, dto);
        return Ok();
    }
}