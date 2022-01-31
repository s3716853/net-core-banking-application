using MCBACommon.Repositories;
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
    public async Task<List<TModelEntity>> GetAllAsync()
    {
        return await _repo.GetAll();
    }

    [HttpGet]
    [Route("{key}")]
    public async Task<TModelEntity?> GetAsync(TModelEntityKeyType key)
    {
        return await _repo.Get(key);
    }

    [HttpPost]
    public async Task<StatusCodeResult> Add(TModelEntity entity)
    {
        await _repo.Add(entity);
        return StatusCode(200);
    }

    [HttpPut]
    public async Task<StatusCodeResult> Update(TModelEntity entity)
    {
        await _repo.Update(entity);
        return StatusCode(200);
    }

    [HttpDelete]
    public async Task<StatusCodeResult> Delete(TModelEntityKeyType key)
    {
        await _repo.Delete(key);
        return StatusCode(200);
    }
}
