// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;
//...
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("172.22.144.1:6379");
IDatabase db = redis.GetDatabase();
Console.WriteLine(db.Ping()); // prints bar