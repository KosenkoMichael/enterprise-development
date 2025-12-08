var builder = DistributedApplication.CreateBuilder(args);

var batchSize = builder.AddParameter("GeneratorBatchSize");
var payloadLimit = builder.AddParameter("GeneratorPayloadLimit");
var waitTime = builder.AddParameter("GeneratorWaitTime");

var mongoDb = builder.AddMongoDB("db")
    .AddDatabase("car-rental");

var api = builder.AddProject<Projects.CarRentalService_WebApplication>("api")
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

var natsUserName = builder.AddParameter("NatsLogin");
var natsPassword = builder.AddParameter("NatsPassword");
var nats = builder.AddNats("car-rental-nats", userName: natsUserName, password: natsPassword, port: 4222)
    .WithJetStream()
    .WithArgs("-m", "8222")
    .WithHttpEndpoint(port: 8222, targetPort: 8222);

builder.AddContainer("car-rental-nui", "ghcr.io/nats-nui/nui")
    .WithReference(nats)
    .WaitFor(nats)
    .WithHttpEndpoint(port: 31311, targetPort: 31311);

var natsStream = builder.AddParameter("NatsStream");
var natsSubject = builder.AddParameter("NatsSubject");
builder.AddProject<Projects.CarRentalService_Producer_Web>("car-rental-producer-web")
    .WithReference(nats)
    .WaitFor(nats)
    .WithEnvironment("Generator:BatchSize", batchSize)
    .WithEnvironment("Generator:PayloadLimit", payloadLimit)
    .WithEnvironment("Generator:WaitTime", waitTime)
    .WithEnvironment("Nats:StreamName", natsStream)
    .WithEnvironment("Nats:SubjectName", natsSubject);

api.WithEnvironment("Nats:SubjectName", natsSubject)
    .WithEnvironment("Nats:StreamName", natsStream)
    .WithReference(nats)
    .WaitFor(nats);

builder.Build().Run();
