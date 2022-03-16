namespace TOS.Data.MongoDB
{
    public interface ICollectionNameProvider
    {
        string GetCollectionName<TModel>();
    }
}