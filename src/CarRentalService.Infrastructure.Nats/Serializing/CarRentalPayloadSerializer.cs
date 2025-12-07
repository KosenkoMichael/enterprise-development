using NATS.Client.Core;
using System.Buffers;
using System.Text.Json;

namespace CarRentalService.Infrastructure.Nats.Serializing;

internal class CarRentalPayloadSerializer<T> : INatsSerialize<T>
{
    public void Serialize(IBufferWriter<byte> buffer, T value) =>
        buffer.Write(JsonSerializer.SerializeToUtf8Bytes(value));
}
