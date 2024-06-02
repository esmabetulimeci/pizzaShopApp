using Application.Common.Interfaces.Redis;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Infrastructure.Repositories.Redis
{
    public class RedisDbContext : IRedisDbContext
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _database;

        public RedisDbContext()
        {
            _connection = ConnectionMultiplexer.Connect("localhost");
            _database = _connection.GetDatabase();
        }


        public async Task<T> Get<T>(string key)
        {
            var value = await _database.StringGetAsync(key);

            if (value.IsNullOrEmpty)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(value);
        }


        public async Task Add<T>(string key, T value, int time = 1)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(value, options);

            await _database.StringSetAsync(key, json, TimeSpan.FromHours(time));
        }

        public async Task Delete(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task AddString(string key, string value, TimeSpan time)
        {
            await _database.StringSetAsync(key, value, time);
        }

        public async Task<bool> KeyExist(string key)
        {
            return await _database.KeyExistsAsync(key);
        }
    }
}
