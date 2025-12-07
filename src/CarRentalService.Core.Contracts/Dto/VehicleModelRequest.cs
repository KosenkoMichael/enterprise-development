using CarRentalService.Core.Domain.Models;
using DriveType = CarRentalService.Core.Domain.Models.DriveType;

namespace CarRentalService.Core.Contracts.Dto;

/// <summary>
/// Vehicle model request
/// </summary>
/// <param name="Name">Name</param>
/// <param name="DriveType">Drive type</param>
/// <param name="SeatCount">Seat count</param>
/// <param name="BodyType">Body type</param>
/// <param name="Class">Class</param>
public sealed record VehicleModelRequest(string Name, DriveType DriveType, int SeatCount, BodyType BodyType, VehicleClass Class);

