using ReactWithASP.Server.Services;

namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LecturersController(IGetService<LecturerDto> getLecturerService, ISaveService<LecturerDto> saveLecturerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getLecturerService.GetAll();
        return Ok(results);
    }

    [HttpPut("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put(int id, LecturerDto dto)
    {
        await saveLecturerService.Update(id, dto);
        return Ok();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post(LecturerDto dto)
    {
        await saveLecturerService.Store(dto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int id, LecturerDto dto)
    {
        await saveLecturerService.Delete(id, dto);
        return Ok();
    }
}