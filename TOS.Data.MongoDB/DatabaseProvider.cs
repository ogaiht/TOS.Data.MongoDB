using MongoDB.Driver;

namespace TOS.Data.MongoDB
{
    public class DatabaseProvider : IDatabaseProvider
    {
        public DatabaseProvider(IMongoDatabase database, ICollectionNameProvider collectionNameProvider)
        {
            Database = database;
            CollectionNameProvider = collectionNameProvider;
        }

        public IMongoDatabase Database { get; }

        public ICollectionNameProvider CollectionNameProvider { get; }        
    }
}
