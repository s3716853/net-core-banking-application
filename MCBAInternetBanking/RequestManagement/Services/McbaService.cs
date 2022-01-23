using MCBABackend.Contexts;
using Microsoft.Extensions.Logging;

namespace MCBABackend.Services
{
    abstract public class McbaService
    {
        protected readonly ILogger<McbaService> _logger;
        protected readonly McbaContext _context;

        protected McbaService(ILogger<McbaService> logger)
        {
            _logger = logger;
            // _repository = repository;
        }
    }
}
