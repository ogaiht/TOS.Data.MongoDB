using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Security.Authentication;
using TOS.Common.Configuration;
using TOS.Common.Text.Semantics;
using TOS.Data.MongoDB.Configuration;

namespace TOS.Data.MongoDB
{
    public static class MongoConfigurationExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            IDatabaseSettings databaseSettings = configuration.GetConfig<DatabaseSettings>();
            return serviceCollection
                .AddSingleton(databaseSettings)
                .AddScoped<IMongoDatabase>(factory =>
                {
                    IDatabaseSettings settings = factory.GetService<IDatabaseSettings>();
                    MongoClientSettings mongoClientSettings;
                    if (settings.UseCosmos)
                    {
                        mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(settings.ConnectionString));
                        mongoClientSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    }
                    else
                    {
                        mongoClientSettings = MongoClientSettings.FromConnectionString(MongoConnectionStringBuilder.Build(settings));
                    }
                    IMongoClient mongoClient = new MongoClient(mongoClientSettings);
                    IMongoDatabase mongoDatabase = mongoClient.GetDatabase(settings.Database);
                    return mongoDatabase;
                })
                .AddSingleton<ICollectionNameProvider, CollectionNameProvider>()
                .AddScoped<IDatabaseProvider, DatabaseProvider>()
                .AddScoped<IMongoCollectionProvider, MongoCollectionProvider>()
                .AddSemantics();
        }
    }
}
