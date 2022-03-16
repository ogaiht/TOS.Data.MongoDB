using MongoDB.Driver;

namespace TOS.Data.MongoDB
{
    public class MongoCollectionProvider : IMongoCollectionProvider
    {
        private readonly IDatabaseProvider _databaseProvider;

        public MongoCollectionProvider(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            string collectionName = _databaseProvider.CollectionNameProvider.GetCollectionName<TDocument>();
            return _databaseProvider.Database.GetCollection<TDocument>(collectionName);
        }
    }
}
