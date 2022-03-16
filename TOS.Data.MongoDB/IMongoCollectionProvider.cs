using MongoDB.Driver;

namespace TOS.Data.MongoDB
{
    public interface IMongoCollectionProvider
    {
        IMongoCollection<TDocument> GetCollection<TDocument>();
    }
}
