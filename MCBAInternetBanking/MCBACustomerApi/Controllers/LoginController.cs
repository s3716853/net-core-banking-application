using MCBABackend.Models;
using MCBABackend.Repositories;
using MCBACustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using SimpleHashing;

namespace MCBACustomerApi.Controllers;

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

    [HttpPut]
    [Route("Update")]
    public async Task Update([FromBody] ControllerInputs.LoginUpdateInput loginUpdateInput)
    {
        Login? login = await GetByCustomerId(loginUpdateInput.customerID);
        if (login != null && PBKDF2.Verify(login.PasswordHash, loginUpdateInput.passwordOld))
        {
            login.PasswordHash = PBKDF2.Hash(loginUpdateInput.passwordNew);
            await _repo.Update(login);
        }
    }
}
