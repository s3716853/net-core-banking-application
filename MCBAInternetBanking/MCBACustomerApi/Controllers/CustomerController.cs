using MCBACommon.Controllers;
using MCBACommon.Models;
using MCBACommon.Repositories;
using MCBACustomerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCBACustomerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : McbaController<Customer, CustomerRepository, string>
{
    public CustomerController(CustomerRepository repo, ILogger<Customer> logger) : base(repo, logger)
    {
    }

    [HttpPut]
    [Route("Update")]
    public async Task Update([FromBody] ControllerInputs.CustomerUpdateInput customerUpdateInput)
    {
        Customer? customer = await _repo.Get(customerUpdateInput.CustomerID);
        if (customer != null)
        {
            customer.Name = customerUpdateInput.Name;
            customer.TFN = customerUpdateInput.TFN;
            customer.Address = customerUpdateInput.Address;
            customer.Suburb = customerUpdateInput.Suburb;
            customer.State = customerUpdateInput.State;
            customer.PostCode = customerUpdateInput.PostCode;
            customer.Mobile = customerUpdateInput.Mobile;

            await _repo.Update(customer);
        }
    }
}
