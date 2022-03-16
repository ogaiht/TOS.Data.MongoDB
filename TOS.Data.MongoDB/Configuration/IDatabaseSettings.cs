namespace TOS.Data.MongoDB.Configuration
{
    public interface IDatabaseSettings
    {
        string Database { get; }
        string Host { get; }
        int Port { get; }
        string User { get; }
        string Password { get; }
        string ConnectionString { get; }
        bool UseCosmos { get; }
    }
}
