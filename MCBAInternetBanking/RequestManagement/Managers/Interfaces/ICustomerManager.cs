using MCBABackend.Models;

namespace MCBABackend.Managers.Interfaces;

public interface ICustomerManager
{
    public void Insert(Customer customer);
    public List<Customer> RetrieveCustomers();
}