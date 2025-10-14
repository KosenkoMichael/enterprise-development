namespace CarRentalService.Core.Domain.Repository;

/// <summary>
/// Defines a generic repository interface for basic CRUD operations on entities.
/// </summary>
/// <typeparam name="T">The type of entity the repository manages.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Creates a new entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The unique identifier of the newly created entity.</returns>
    public Task<Guid> CreateAsync(T entity);

    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public Task<List<T>> ReadAllAsync();

    /// <summary>
    /// Retrieves a specific entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public Task<T?> ReadAsync(Guid id);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to update.</param>
    /// <param name="entity">The updated entity data.</param>
    /// <returns>The updated entity if successful; otherwise, null.</returns>
    public Task<T?> UpdateAsync(Guid id, T entity);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
