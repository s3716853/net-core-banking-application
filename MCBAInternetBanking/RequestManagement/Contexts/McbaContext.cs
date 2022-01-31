using MCBABackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBABackend.Contexts;

public class McbaContext : DbContext
{
    public McbaContext(DbContextOptions<McbaContext> options) : base(options)
    { }

    public DbSet<Account> Account { get; set; }

    public DbSet<Customer> Customer { get; set; }

    public DbSet<Login> Login { get; set; }

    public DbSet<Transaction> Transaction { get; set; }

    public DbSet<BillPay> BillPay { get; set; }

    public DbSet<Payee> Payee { get; set; }
}