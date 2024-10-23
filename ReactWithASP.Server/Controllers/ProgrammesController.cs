using ReactWithASP.Server.Services;

namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProgrammesController(
    IGetService<ProgrammeDto> getProgrammeService,
    ISaveService<ProgrammeDto> saveProgrammeService,
    IDeleteService<ProgrammeDto> deleteProgrammeService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getProgrammeService.GetAll();
        return Ok(results);
    }

    [HttpPut("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put(int id, ProgrammeDto dto)
    {
        await saveProgrammeService.Update(id, dto);
        return Ok();
    }

    [HttpPost("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post(ProgrammeDto dto)
    {
        await saveProgrammeService.Store(dto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int id, ProgrammeDto dto)
    {
        await deleteProgrammeService.Delete(id, dto);
        return Ok();
    }
}