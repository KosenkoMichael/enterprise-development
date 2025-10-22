using CarRentalService.Core.Domain.DataSeed;

namespace CarRentalService.WebApplication;
/// <summary>
/// Seeder for populating the MongoDB database with initial test data.
/// </summary>

public class DbSeedService(DbSeeder seeder) : IHostedService
{
    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await seeder.Seed(cancellationToken);
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken) =>
        Task.CompletedTask;
}
