using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TOS.Common.MongoDB;
using TOS.Data.Repositories;

namespace TOS.Data.MongoDB.Repositories
{
    public abstract class Repository<TModel> : IRepository<TModel, Guid>
        where TModel : IDocumentModel
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly string _collectionName;

        protected Repository(IDatabaseProvider databaseProvider)
        {
            _mongoDatabase = databaseProvider.Database;
            _collectionName = databaseProvider.CollectionNameProvider.GetCollectionName<TModel>();
        }

        protected IMongoCollection<TModel> Collection => _mongoDatabase
                .GetCollection<TModel>(_collectionName);

        public virtual Guid Add(TModel model)
        {
            Collection.InsertOne(model);
            return model.Id;
        }

        public virtual async Task<Guid> AddAsync(TModel model)
        {
            await Collection.InsertOneAsync(model);
            return model.Id;
        }

        public virtual void AddRange(IEnumerable<TModel> models)
        {
            Collection.InsertMany(models);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TModel> models)
        {
            await Collection.InsertManyAsync(models);
        }

        public virtual bool Delete(Guid id)
        {
            return Collection.DeleteOne(m => m.Id == id).DeletedCount == 1;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            return (await Collection.DeleteOneAsync(m => m.Id == id)).DeletedCount == 1;
        }

        public virtual TModel GetById(Guid id)
        {
            return Collection.Find(m => m.Id == id).FirstOrDefault();
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id)
        {
            return await Collection.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        public virtual void Update(TModel model)
        {
            Collection.ReplaceOne(m => m.Id == model.Id, model);
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            await Collection.ReplaceOneAsync(m => m.Id == model.Id, model);
        }
    }
}
