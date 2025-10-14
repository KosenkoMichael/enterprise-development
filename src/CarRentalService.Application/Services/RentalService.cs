using CarRentalService.Application.Dto;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.Infrastructure.Repositories;

namespace CarRentalService.Application.Services;
public class RentalService(
    IRepository<Rental> rentalRepository,
    IRepository<Customer> customerRepository,
    IRepository<Vehicle> vehicleRepository
    )
{
    private static Rental MapDto(RentalDto entity)
    {
        return new Rental
        {
            VehicleId = entity.VehicleId,
            CustomerId = entity.CustomerId,
            RentStartTime = entity.RentStartTime,
            RentalDurationHours = entity.RentalDurationHours
        };
    }

    public async Task<Guid> CreateRentalAsync(RentalDto entity)
    {
        if (await customerRepository.ReadAsync(entity.CustomerId ) is null)
        {
            throw new Exception($"Customer with Id = {entity.CustomerId} does not found");
        }

        if (await vehicleRepository.ReadAsync(entity.VehicleId) is null)
        {
            throw new Exception($"Vehicle with Id = {entity.VehicleId} does not found");
        }

        return await rentalRepository.CreateAsync(MapDto(entity));
    }

    public async Task<List<Rental>> GetRentalsAsync() =>
        await rentalRepository.ReadAllAsync();

    public async Task<Rental?> GetRentalAsync(Guid id) =>
        await rentalRepository.ReadAsync(id);

    public async Task<Rental?> UpdateRentalAsync(Guid id, RentalDto entity) =>
        await rentalRepository.UpdateAsync(id, MapDto(entity));

    public async Task<bool> DeleteRentalAsync(Guid id) =>
        await rentalRepository.DeleteAsync(id);
}
