
using MongoDB.Driver;

namespace Los_Angeles_Life.Handlers;

public class DatabaseHandler
{
    private readonly IMongoDatabase _database;

    public DatabaseHandler()
    {
        string connectionString = "mongodb://localhost:27017";
        string databaseName = "altv";
        
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    private IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    public void Insert<T>(string collectionName, T document)
    {
        var collection = GetCollection<T>(collectionName);
        collection.InsertOne(document);
    }

    public void Update<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update)
    {
        var collection = GetCollection<T>(collectionName);
        collection.UpdateOne(filter, update);
    }

    public void Delete<T>(string collectionName, FilterDefinition<T> filter)
    {
        var collection = GetCollection<T>(collectionName);
        collection.DeleteOne(filter);
    }

    public List<T> Find<T>(string collectionName, FilterDefinition<T> filter)
    {
        var collection = GetCollection<T>(collectionName);
        return collection.Find(filter).ToList();
    }
}