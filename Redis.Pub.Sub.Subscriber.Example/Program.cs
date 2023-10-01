using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:2023");
ISubscriber subscriber = redis.GetSubscriber();

subscriber.Subscribe("mychannel", (channel, value) =>
{
    Console.WriteLine(value);
});
Console.Read();