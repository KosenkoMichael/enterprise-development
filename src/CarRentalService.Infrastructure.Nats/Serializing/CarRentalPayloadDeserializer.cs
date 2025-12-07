using CarRentalService.Core.Contracts.Dto;
using NATS.Client.Core;
using System.Buffers;
using System.Text.Json;

namespace CarRentalService.Infrastructure.Nats.Serializing;
internal class CarRentalPayloadDeserializer<T> : INatsDeserialize<T>
{
    public T? Deserialize(in ReadOnlySequence<byte> buffer) =>
        JsonSerializer.Deserialize<T>(buffer.ToArray());
}
