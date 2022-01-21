using MCBABackend.Models;
using MCBACustomerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.OpenApi.Any;

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
    public IEnumerable<TModelEntity> Get()
    {
        _logger.LogInformation($"{typeof(TModelEntity)} Controller Get All");
        return _repo.GetAll();
    }

    [HttpGet("{key}")]
    public TModelEntity? Get(TModelEntityKeyType key)
    {
        _logger.LogInformation($"{typeof(TModelEntity)} Controller Get KEY={key}");
        return _repo.Get(key);
    }
}
