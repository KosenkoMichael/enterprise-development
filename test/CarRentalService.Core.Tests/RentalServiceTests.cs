using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Core.Tests;
public class RentalServiceTests
{
    private (List<VehicleModel>, List<ModelGeneration>, List<Vehicle>, List<Customer>, List<Rental>) GenerateTestData()
    {
        var random = new Random();
        var now = DateTime.Now;
        var vehicleModels = new List<VehicleModel>
{
    new() { Id = Guid.NewGuid(), Name = "Camry", DriveType = "FWD", SeatCount = 5, BodyType = "Sedan", Class = "Business" },
    new() { Id = Guid.NewGuid(), Name = "RAV4", DriveType = "AWD", SeatCount = 5, BodyType = "SUV", Class = "Compact" },
    new() { Id = Guid.NewGuid(), Name = "Corolla", DriveType = "FWD", SeatCount = 5, BodyType = "Sedan", Class = "Economy" },
    new() { Id = Guid.NewGuid(), Name = "Civic", DriveType = "FWD", SeatCount = 5, BodyType = "Sedan", Class = "Economy" },
    new() { Id = Guid.NewGuid(), Name = "CR-V", DriveType = "AWD", SeatCount = 5, BodyType = "SUV", Class = "Compact" }
};
        var modelGenerations = new List<ModelGeneration>();
        foreach (var model in vehicleModels)
        {
            for (var i = 0; i < 3; i++)
            {
                modelGenerations.Add(new ModelGeneration
                {
                    Id = Guid.NewGuid(),
                    Year = 2018 + i,
                    EngineVolume = 1.6 + (i * 0.4),
                    TransmissionType = i % 2 == 0 ? "Automatic" : "Manual",
                    Model = model,
                    RentalPricePerHour = 40m + (i * 10)
                });
            }
        }
        var vehicles = new List<Vehicle>();
        var colors = new[] { "Black", "White", "Silver", "Red", "Blue" };
        foreach (var generation in modelGenerations)
        {
            for (var i = 0; i < 2; i++)
            {
                vehicles.Add(new Vehicle
                {
                    Id = Guid.NewGuid(),
                    LicensePlate = $"{colors[i][0]}{100 + vehicles.Count:000}{colors[i][0]}RUS",
                    Color = colors[vehicles.Count % colors.Length],
                    Generation = generation
                });
            }
        }
        var customers = new List<Customer>();
        var firstNames = new[] { "Иван", "Петр", "Мария", "Анна", "Сергей", "Ольга" };
        var lastNames = new[] { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов" };

        for (var i = 0; i < 10; i++)
        {
            customers.Add(new Customer
            {
                Id = Guid.NewGuid(),
                DriverLicenseNumber = $"77AB{100000 + i}",
                FullName = $"{lastNames[i % lastNames.Length]} {firstNames[i % firstNames.Length]}",
                DateOfBirth = new DateTime(1985, 1, 1).AddDays(i * 300)
            });
        }
        var rentals = new List<Rental>();
        var currentlyRentedVehicles = vehicles.Take(3).ToList();
        foreach (var vehicle in currentlyRentedVehicles)
        {
            rentals.Add(new Rental
            {
                Id = Guid.NewGuid(),
                RentedVehicle = vehicle,
                Customer = customers[random.Next(customers.Count)],
                RentStartTime = now.AddHours(-2),
                RentalDurationHours = 8
            });
        }
        var popularVehicle = vehicles[5];
        for (var i = 0; i < 5; i++)
        {
            rentals.Add(new Rental
            {
                Id = Guid.NewGuid(),
                RentedVehicle = popularVehicle,
                Customer = customers[random.Next(customers.Count)],
                RentStartTime = now.AddDays(-i * 10),
                RentalDurationHours = random.Next(1, 48)
            });
        }
        for (var i = 0; i < 15; i++)
        {
            var vehicle = vehicles[random.Next(vehicles.Count)];
            if (currentlyRentedVehicles.Contains(vehicle)) continue;

            rentals.Add(new Rental
            {
                Id = Guid.NewGuid(),
                RentedVehicle = vehicle,
                Customer = customers[random.Next(customers.Count)],
                RentStartTime = now.AddDays(-random.Next(1, 100)),
                RentalDurationHours = random.Next(1, 72)
            });
        }

        return (vehicleModels, modelGenerations, vehicles, customers, rentals);
    }
    [Fact]
    public void GetCustomersRentingModel_OrderedByName()
    {
        var (_, _, _, customers, rentals) = GenerateTestData();
        var targetModel = "Camry";
        var result = rentals
            .Where(r => r.RentedVehicle.Generation.Model.Name == targetModel)
            .Select(r => r.Customer)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        foreach (var customer in result)
        {
            var hasRentedTargetModel = rentals.Any(r =>
                r.Customer.Id == customer.Id &&
                r.RentedVehicle.Generation.Model.Name == targetModel);
            Assert.True(hasRentedTargetModel, $"Клиент {customer.FullName} не арендовал {targetModel}");
        }
        var sorted = result.OrderBy(c => c.FullName).ToList();
        Assert.Equal(sorted, result);
    }
    [Fact]
    public void GetVehiclesCurrentlyRented()
    {
        var (_, _, vehicles, _, rentals) = GenerateTestData();
        var now = DateTime.Now;
        var result = rentals
            .Where(r => r.RentStartTime <= now &&
                       r.RentStartTime.AddHours(r.RentalDurationHours) > now)
            .Select(r => r.RentedVehicle)
            .Distinct()
            .ToList();

        Assert.NotNull(result);
        Assert.NotEmpty(result);

        foreach (var vehicle in result)
        {
            var isCurrentlyRented = rentals.Any(r =>
                r.RentedVehicle.Id == vehicle.Id &&
                r.RentStartTime <= now &&
                r.RentStartTime.AddHours(r.RentalDurationHours) > now);

            Assert.True(isCurrentlyRented, $"Автомобиль {vehicle.LicensePlate} не в аренде");
        }
    }
    [Fact]
    public void GetTop5MostFrequentlyRentedVehicles()
    {
        var (_, _, vehicles, _, rentals) = GenerateTestData();
        var result = rentals
            .GroupBy(r => r.RentedVehicle)
            .Select(g => new { Vehicle = g.Key, RentalCount = g.Count() })
            .OrderByDescending(x => x.RentalCount)
            .Take(5)
            .ToList();
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.Count <= 5);
        for (var i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].RentalCount >= result[i + 1].RentalCount,
                $"Неправильная сортировка: {result[i].RentalCount} < {result[i + 1].RentalCount}");
        }
        Assert.True(result.First().RentalCount >= 5);
    }
    [Fact]
    public void GetRentalCountPerVehicle()
    {
        var (_, _, vehicles, _, rentals) = GenerateTestData();
        var result = vehicles
            .Select(vehicle => new
            {
                Vehicle = vehicle,
                RentalCount = rentals.Count(r => r.RentedVehicle.Id == vehicle.Id)
            })
            .ToList();
        Assert.NotNull(result);
        Assert.Equal(vehicles.Count, result.Count);
        foreach (var item in result)
        {
            var actualCount = rentals.Count(r => r.RentedVehicle.Id == item.Vehicle.Id);
            Assert.Equal(actualCount, item.RentalCount);
        }
        var distinctCounts = result.Select(x => x.RentalCount).Distinct().Count();
        Assert.True(distinctCounts >= 2, "Должны быть автомобили с разной популярностью");
    }
    [Fact]
    public void GetTop5CustomersByRentalCost()
    {
        var (_, _, _, customers, rentals) = GenerateTestData();
        var result = rentals
            .GroupBy(r => r.Customer)
            .Select(g => new
            {
                Customer = g.Key,
                TotalCost = g.Sum(r => (decimal)r.RentalDurationHours * r.RentedVehicle.Generation.RentalPricePerHour)
            })
            .OrderByDescending(x => x.TotalCost)
            .Take(5)
            .ToList();
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
        for (var i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].TotalCost >= result[i + 1].TotalCost,
                $"Неправильная сортировка: {result[i].TotalCost} < {result[i + 1].TotalCost}");
        }
        foreach (var item in result)
        {
            var actualCost = rentals
                .Where(r => r.Customer.Id == item.Customer.Id)
                .Sum(r => (decimal)r.RentalDurationHours * r.RentedVehicle.Generation.RentalPricePerHour);

            Assert.Equal(actualCost, item.TotalCost);
        }
    }
}