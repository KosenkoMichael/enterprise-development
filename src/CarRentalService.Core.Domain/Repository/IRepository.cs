namespace CarRentalService.Core.Domain.Repository;
/// <summary>
/// Interface for a generic repository pattern
/// </summary>
/// <typeparam name="T">
/// The type of the entity managed by the repository
/// </typeparam>
public interface IRepository<T> where T: class
{
    /// <summary>
    /// Creates a new entity in the repository
    /// </summary>
    /// <param name="entity">The entity to be created</param>
    /// <returns>
    /// The unique identifier (<see cref="Guid"/>) assigned to the created entity
    /// </returns>
    public Guid Create(T entity);
    /// <summary>
    /// Retrieves all entities from the repository
    /// </summary>
    /// <returns>
    /// A list of all entities of type <typeparamref name="T"/>.
    /// </returns>
    public List<T> Read();
    /// <summary>
    /// Retrieves a single entity by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve</param>
    /// <returns>
    /// The entity with the specified <paramref name="id"/>, or <c>null</c> if not found.
    /// </returns>
    public T? Read(Guid id);
    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="id">The unique identifier of the entity to update</param>
    /// <param name="entity">The updated entity data</param>
    /// <returns>
    /// The updated entity, or <c>null</c> if the entity was not found
    /// </returns>
    public T? Update(Guid id, T entity);
    /// <summary>
    /// Deletes an entity from the repository by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <returns>
    /// <c>true</c> if the entity was successfully deleted; otherwise, <c>false</c>
    /// </returns>
    public bool Delete(Guid id);
}