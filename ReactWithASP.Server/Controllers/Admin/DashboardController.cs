namespace ReactWithASP.Server.Controllers.Admin;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = UserRoles.Admin)]
public class DashboardController(UserManager<IdentityUser> userManager) : ControllerBase
{
    //[HttpGet]
    //public IActionResult Show() => Ok(new { text = "You logged to dashboard!" });


    [HttpGet]
    public async Task<List<IdentityUser>> GetAllUsers()
    {
        var users = userManager.Users.ToListAsync();
        List<IdentityUser> results = [];
        foreach (var user in await users)
        {
            results.Add(user);
        }
        return results;
    }
}