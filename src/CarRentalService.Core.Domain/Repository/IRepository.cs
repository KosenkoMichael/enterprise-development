namespace CarRentalService.Core.Domain.Repository;

/// <summary>
/// Defines a generic repository pattern for data access operations.
/// </summary>
/// <typeparam name="T">The type of entity managed by the repository.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Creates a new entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to be created.</param>
    /// <returns>
    /// The unique identifier (<see cref="Guid"/>) assigned to the created entity.
    /// </returns>
    public Task<Guid> CreateAsync(T entity);

    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> containing all entities of type <typeparamref name="T"/>.
    /// </returns>
    public Task<List<T>> ReadAllAsync();

    /// <summary>
    /// Retrieves a single entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>
    /// The entity with the specified <paramref name="id"/>, or <see langword="null"/> if not found.
    /// </returns>
    public Task<T?> ReadAsync(Guid id);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to update.</param>
    /// <param name="entity">The updated entity data.</param>
    /// <returns>
    /// The updated entity, or <see langword="null"/> if the entity was not found.
    /// </returns>
    public Task<T?> UpdateAsync(Guid id, T entity);

    /// <summary>
    /// Deletes an entity from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>
    /// <see langword="true"/> if the entity was successfully deleted; otherwise, <see langword="false"/>.
    /// </returns>
    public Task<bool> DeleteAsync(Guid id);
}