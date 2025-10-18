using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var mongoDb = builder.AddMongoDB("db")
    .AddDatabase("car-rental");
var api = builder.AddProject<Projects.CarRentalService_WebApplication>("api")
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

builder.Build().Run();
