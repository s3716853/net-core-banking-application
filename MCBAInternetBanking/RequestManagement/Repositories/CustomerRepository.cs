using MCBABackend.Contexts;
using MCBABackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBABackend.Repositories;

public class CustomerRepository : DataRepository<Customer, string>
{
    public CustomerRepository(McbaContext context) : base(context)
    {
    }

    public override async Task<List<Customer>> GetAll()
    {
        return await _context.Customer.
            Include(customer => customer.Login).
            Include(customer => customer.Accounts).ToListAsync();
    }

    public override async Task<Customer?> Get(string id)
    {
        return await _context.Customer.
            Include(customer => customer.Login).
            Include(customer => customer.Accounts).
            FirstOrDefaultAsync(customer => customer.CustomerID == id);
    }

    public string Add(Customer customer, Login login)
    {
        // Ensuring the newly added user is the one recieving this login
        if (customer.CustomerID == login.CustomerID)
        {
            _context.Customer.Add(customer);
            _context.Login.Add(login);
            _context.SaveChanges();
        }

        return customer.CustomerID;
    }

    public string Update(Customer customer)
    {
        _context.Customer.Update(customer);
        _context.SaveChanges();
        return customer.CustomerID;
    }

    public string Delete(string id)
    {
        Customer? customer = _context.Customer.Find(id);

        if(customer != null) _context.Customer.Remove(customer);

        return id;
    }
}