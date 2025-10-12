using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.Infrastructure.InMemory.Seeders;

namespace CarRentalService.Infrastructure.InMemory.Repositories;
/// <summary>
/// In memory implementation of the customer repository
/// </summary>
public class InMemoryCustomerRepository: IRepository<Customer>
{
    private readonly List<Customer> _customers = new();
    public InMemoryCustomerRepository(CustomerSeeder? seeder)
    {
        if (seeder == null) return;
        _customers = seeder.GetItems();
    }
    /// <inheritdoc/>
    public Guid Create(Customer entity)
    {
        _customers.Add(entity);
        return entity.Id;
    }
    /// <inheritdoc/>
    public bool Delete(Guid id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return false;
        }
        _customers.Remove(customer);
        return true;
    }
    /// <inheritdoc/>
    public List<Customer> Read() =>
        [.. _customers];
    /// <inheritdoc/>
    public Customer? Read(Guid id) =>
        _customers.FirstOrDefault(c => c.Id == id);
    /// <inheritdoc/>
    public Customer? Update(Guid id, Customer entity)
    {
        var index = _customers.FindIndex(c => c.Id == id);
        if (index == -1)
        {
            return null;
        }
        entity.Id = id;
        _customers[index] = entity;
        return entity;
    }
}
