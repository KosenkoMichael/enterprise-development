using NATS.Client.Core;
using System.Buffers;
using System.Text.Json;

namespace CarRentalService.Infrastructure.Nats.Serializing;

/// <summary>
/// Json deserializer
/// </summary>
/// <typeparam name="T">param</typeparam>
internal class CarRentalPayloadDeserializer<T> : INatsDeserialize<T>
{
    public T? Deserialize(in ReadOnlySequence<byte> buffer) =>
        JsonSerializer.Deserialize<T>(buffer.ToArray());
}
