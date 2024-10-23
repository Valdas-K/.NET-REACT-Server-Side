using ReactWithASP.Server.Services.StudentServices;

namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route ("api/[controller]")]
[Authorize]
public class StudentsController (
    IGetService<StudentDto> getStudentService, 
    ISaveService<StudentDto> saveStudentService,
    IDeleteService<StudentDto> deleteStudentService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getStudentService.GetAll();
        return Ok(results);
    }

    [HttpPut("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put(int id, StudentDto dto)
    {
        await saveStudentService.Update(id, dto);
        return Ok();
    }

    [HttpPost("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post(StudentDto dto)
    {
        await saveStudentService.Store(dto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int id, StudentDto dto)
    {
        await deleteStudentService.Delete(id, dto);
        return Ok();
    }
}