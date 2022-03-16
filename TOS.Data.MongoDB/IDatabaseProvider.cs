using MongoDB.Driver;

namespace TOS.Data.MongoDB
{
    public interface IDatabaseProvider
    {
        IMongoDatabase Database { get; }
        ICollectionNameProvider CollectionNameProvider { get; }
    }
}
