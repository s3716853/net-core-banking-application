using MCBACommon.Controllers;
using MCBACommon.Models;
using MCBACommon.Repositories;
using Microsoft.AspNetCore.Mvc;
using SimpleHashing;

namespace MCBAAdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : McbaController<Login, LoginRepository, string>
{
    public LoginController(LoginRepository repo, ILogger<Login> logger) : base(repo, logger)
    {
    }

    [HttpGet]
    [Route("Customer/{id}")]
    public Task<Login?> GetByCustomerId(string id)
    {
        return _repo.GetByCustomerId(id);
    }
}
