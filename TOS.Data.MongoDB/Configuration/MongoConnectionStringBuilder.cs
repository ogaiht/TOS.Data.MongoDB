namespace TOS.Data.MongoDB.Configuration
{
    public class MongoConnectionStringBuilder
    {
        public static string Build(IDatabaseSettings databaseSettings)
        {
            return $@"mongodb://{databaseSettings.User}:{databaseSettings.Password}@{databaseSettings.Host}:{databaseSettings.Port}";
        }
    }
}
