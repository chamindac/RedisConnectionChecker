using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RedisConnectivity.Controllers
{
    [Route("api")]
    [ApiController]
    public class RedisConnectivityController : ControllerBase
    {
        [HttpGet("redisserverstatus")]
        public string GetRedisServerStatus(string redisConnectionString)
        {
            try
            {
                IConnectionMultiplexer connection = ConnectionMultiplexer.Connect($"{redisConnectionString}, allowAdmin=true");

                // Server is frequently where more "admin" type operations are
                var server = connection.GetServer(connection.GetEndPoints().First());
                return $"Connection string: {redisConnectionString} \r\n {server.InfoRaw()}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("redisdatabasestatus")]
        public string GetPublicRedisDbStatus(string redisConnectionString)
        {
            try
            {
                IConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisConnectionString);

                bool connectionState = connection.GetDatabase().IsConnected("fake-key");
                return $"Connection string: {redisConnectionString} \r\n Redis database connecton status: {connectionState}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}