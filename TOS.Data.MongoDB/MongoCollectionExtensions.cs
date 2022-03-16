using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TOS.Common.DataModel;

namespace TOS.Data.MongoDB
{
    public static class MongoCollectionExtensions
    {
        public static async Task<IPagedResult<TDocument>> FindPagedResultAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, int offset = -1, int limit = -1)
        {
            return await collection.Find(filter).ToPagedResultAsync(offset, limit);
        }

        public static async Task<IPagedResult<TDocument>> FindPagedResultAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, int offset = -1, int limit = -1)
        {
            return await collection.Find(filter).ToPagedResultAsync(offset, limit);
        }

        public static async Task<IPagedResult<TDocument>> ToPagedResultAsync<TDocument>(this IFindFluent<TDocument, TDocument> findFluent, int offset = -1, int limit = -1)
        {
            long total = await findFluent.CountDocumentsAsync();
            if (offset > -1)
            {
                findFluent = findFluent.Skip(offset);
            }
            if (limit > -1)
            {
                findFluent = findFluent.Limit(limit);
            }
            List<TDocument> items = await findFluent.ToListAsync();
            return new PagedResult<TDocument>(items, total, offset < 0 ? 0 : offset, limit < 0 ? 0 : limit);
        }
    }
}
