namespace TechnicalTestOpea.Adapters.Infraestructure.NoSQL.Repositories
{
    public class NoSqlRepository<T> //: INoSqlRepository<T> where T //: BaseEntityBson
    {
        //private readonly IMongoCollection<T> _collection;

        //public NoSqlRepository(IMongoCollection<T> collection)
        //{
        //    _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        //}

        //public NoSqlRepository(IMongoClient Client, IOptions<NoSqlSettings> Settings)
        //{
        //    var datagase = Client.GetDatabase(Settings.Value.DatabaseName);
        //    var collectionName = typeof(T).Name.ToLowerInvariant() + "s";
        //    _collection = datagase.GetCollection<T>(collectionName);
        //}

        //public async Task<Guid> CreateAsync(T entitty)
        //{
        //    try
        //    {
        //        await _collection.InsertOneAsync(entitty);
        //        return Guid.Parse(entitty.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public async Task<IEnumerable<T>> GetAllAsync() => (IEnumerable<T>)_collection.FindAsync(Builders<T>.Filter.Empty);

        //public async Task<T?> GetByIdAsync(Guid id)
        //{
        //    var filter = Builders<T>.Filter.Eq("Id", id);
        //    var cursor = await _collection.FindAsync(filter);
        //    return await cursor.FirstOrDefaultAsync();
        //}

        //public async Task<PagedResponse<TProjected>> GetPagedAsync<TProjected>(Expression<Func<T, bool>> filter, Expression<Func<T, TProjected>> projection, PagedRequest request) where TProjected : class
        //{
        //    try
        //    {
        //        var query = _collection.Find(filter);

        //        var totalItems = await _collection.CountDocumentsAsync(filter);

        //        var items = await query
        //            .Skip((request.Page - 1) + request.PageSize)
        //            .Limit(request.PageSize)
        //            .Project(projection)
        //            .ToListAsync();

        //        return new PagedResponse<TProjected>(items, (int)totalItems, request.Page, request.PageSize);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public async Task RemoveAsync(Guid id)
        //{
        //    try
        //    {
        //        var filter = Builders<T>.Filter.Eq("Id", id);
        //        await _collection.DeleteOneAsync(filter);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public async Task UpdateAsync(T entitty)
        //{
        //    try
        //    {
        //        var filter = Builders<T>.Filter.Eq(x => x.Id, entitty.Id);
        //        await _collection.ReplaceOneAsync(filter, entitty);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
