namespace ReactWithASP.Server.Controllers.Admin;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = UserRoles.Admin)]
public class DashboardController() : ControllerBase
{
    [HttpGet]
    public IActionResult Show() => Ok(new { text = "You logged to dashboard!" });

}
