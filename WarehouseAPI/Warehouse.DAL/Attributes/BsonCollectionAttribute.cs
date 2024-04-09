namespace Warehouse.DAL.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class BsonCollectionAttribute : Attribute
{
    private readonly string _collectionName;

    public BsonCollectionAttribute(string collectionName)
    {
        _collectionName = collectionName;
    }
    public string CollectionName => _collectionName;
}
