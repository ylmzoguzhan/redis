using StackExchange.Redis;

namespace Redis.Sentinel.Services
{
    public class RedisService
    {
        static ConfigurationOptions sentinelOptions => new()
        {
            EndPoints =
            {
                {"localhost",6383 },
                {"localhost",6384 },
                {"localhost",6385 }
            },
            CommandMap = CommandMap.Sentinel,
            AbortOnConnectFail = false
        };
        static ConfigurationOptions masterOptions => new()
        {
            AbortOnConnectFail = false
        };
        static public IDatabase RedisMasterDB()
        {
            ConnectionMultiplexer sentinelConnection = ConnectionMultiplexer.SentinelConnect(sentinelOptions);
            System.Net.EndPoint masterEndPoint = null;
            foreach (var item in sentinelConnection.GetEndPoints())
            {
                var server = sentinelConnection.GetServer(item);
                if (!server.IsConnected)
                    continue;
                masterEndPoint = server.SentinelGetMasterAddressByName("mymaster");
                break;
            }
            var localMasterIP = masterEndPoint.ToString() switch
            {
                "172.18.0.2:6379" => "localhost:6379",
                "172.18.0.3:6379" => "localhost:6380",
                "172.18.0.4:6379" => "localhost:6381",
                "172.18.0.5:6379" => "localhost:6382"
            };
            ConnectionMultiplexer masterConnection = ConnectionMultiplexer.Connect(localMasterIP);
            var db = masterConnection.GetDatabase();
            return db;
        }
    }
}
