using ReactWithASP.Server.Services;

namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubjectsController(
    IGetService<SubjectDto> getSubjectService, 
    ISaveService<SubjectDto> saveSubjectService,
    IDeleteService<SubjectDto> deleteSubjectService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getSubjectService.GetAll();
        return Ok(results);
    }

    [HttpPut("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put(int id, SubjectDto dto)
    {
        await saveSubjectService.Update(id, dto);
        return Ok();
    }

    [HttpPost("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post(SubjectDto dto)
    {
        await saveSubjectService.Store(dto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int id, SubjectDto dto)
    {
        await deleteSubjectService.Delete(id, dto);
        return Ok();
    }
}