namespace CarRentalService.Core.Contracts.Dto;

/// <summary>
/// Model generation collection response
/// </summary>
/// <param name="ModelGenerations">Colection of model generations</param>
public sealed record ModelGenerationCollectionResponse(IEnumerable<ModelGenerationDto> ModelGenerations);
