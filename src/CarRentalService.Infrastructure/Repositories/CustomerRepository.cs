using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CarRentalService.Infrastructure.Repositories;
/// <summary>
/// Repository implementation for managing <see cref="Customer"/> entities using MongoDB.
/// </summary>
public class CustomerRepository(CarRentalDbContext dbContext) : IRepository<Customer>
{
    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(Customer entity)
    {
        dbContext.Customers.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await dbContext.Customers.Where(x => x.Id == id).ExecuteDeleteAsync();
        return result > 0;
    }
    /// <inheritdoc/>
    public async Task<List<Customer>> ReadAllAsync() =>
        await dbContext.Customers.ToListAsync();
    /// <inheritdoc/>
    public async Task<Customer?> ReadAsync(Guid id) =>
        await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
    /// <inheritdoc/>
    public async Task<Customer?> UpdateAsync(Guid id, Customer entity)
    {
        var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

        if (customer == null) return null;

        entity.Id = customer.Id;

        dbContext.Customers.Update(entity);

        await dbContext.SaveChangesAsync();
        return customer;
    }
}
