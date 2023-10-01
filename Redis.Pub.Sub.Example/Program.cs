using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:2023");
ISubscriber subscriber = redis.GetSubscriber();

while (true)
{
    Console.Write("Mesaj :");
    string message = Console.ReadLine();
    subscriber.Publish("mychannel",message);
}