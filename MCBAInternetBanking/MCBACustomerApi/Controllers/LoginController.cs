using MCBABackend.Models;
using MCBACustomerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MCBACustomerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : McbaController<Login, LoginRepository, string>
{
    public LoginController(LoginRepository repo, ILogger<Login> logger) : base(repo, logger)
    {
    }
}
