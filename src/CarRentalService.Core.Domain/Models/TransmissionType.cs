namespace CarRentalService.Core.Domain.Models;

/// <summary>
/// Transmission types for vehicles
/// </summary>
public enum TransmissionType
{
    /// <summary>
    /// Automatic transmission
    /// </summary>
    Automatic,

    /// <summary>
    /// Manual transmission
    /// </summary>
    Manual,

    /// <summary>
    /// Continuously variable transmission
    /// </summary>
    CVT,

    /// <summary>
    /// Dual-clutch transmission
    /// </summary>
    DCT,

    /// <summary>
    /// Semi-automatic transmission
    /// </summary>
    SemiAutomatic
}