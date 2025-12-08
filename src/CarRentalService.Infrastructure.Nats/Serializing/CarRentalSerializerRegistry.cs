using NATS.Client.Core;

namespace CarRentalService.Infrastructure.Nats.Serializing;

public class CarRentalSerializerRegistry : INatsSerializerRegistry
{
    public INatsSerialize<T> GetSerializer<T>() => new CarRentalPayloadSerializer<T>();

    public INatsDeserialize<T> GetDeserializer<T>() => new CarRentalPayloadDeserializer<T>();
}