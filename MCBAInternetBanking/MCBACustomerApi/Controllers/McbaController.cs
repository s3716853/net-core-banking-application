using MCBABackend.Models;
using MCBABackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MCBACustomerApi.Controllers;

/// <summary>
/// Creates a controller with _repo and _logger as well as a constructor to set these. _repo is a repository, _logger the logger 
/// </summary>
/// <typeparam name="TModelEntity">The model which this controller will be handling</typeparam>
/// <typeparam name="TModelRepository">The repository that will need to be accessed</typeparam>
/// <typeparam name="TModelEntityKeyType">The type of the model's primary key</typeparam>
public abstract class McbaController<TModelEntity, TModelRepository, TModelEntityKeyType> : ControllerBase 
    where TModelEntity : class
    where TModelRepository : DataRepository<TModelEntity, TModelEntityKeyType>
{
    protected readonly TModelRepository _repo;
    protected readonly ILogger<TModelEntity> _logger;

    protected McbaController(TModelRepository repo, ILogger<TModelEntity> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet]
    public Task<List<TModelEntity>> GetAllAsync()
    {
        return _repo.GetAll();
    }

    [HttpGet]
    [Route("{key}")]
    public Task<TModelEntity?> GetAsync(TModelEntityKeyType key)
    {
        return _repo.Get(key);
    }
}
