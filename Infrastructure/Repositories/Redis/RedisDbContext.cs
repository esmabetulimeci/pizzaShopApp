using Application.Common.Interfaces.Redis;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Redis
{
    public class RedisDbContext : IRedisDbContext
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly PizzaShopAppDbContext _dbcontext;

        public RedisDbContext(PizzaShopAppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _connection = ConnectionMultiplexer.Connect("localhost");
        }


        public async Task Add<T>(string key, T value, int time = 1)
        {
            var db = _connection.GetDatabase();
            var json = JsonSerializer.Serialize(value);
            await db.StringSetAsync(key, json, TimeSpan.FromMinutes(time));


        }

        public async Task AddString(string key, string value, TimeSpan time)
        {
            var db = _connection.GetDatabase();
            await db.StringSetAsync(key, value, time);

        }

        public async Task Delete(string key)
        {
            var db = _connection.GetDatabase();
            await db.KeyDeleteAsync(key);

        }

        public async Task<T> Get<T>(string key)
        {
            var db = _connection.GetDatabase();
            var value = await db.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(value);

        }

        public async Task<bool> KeyExist(string key)
        {
            var db = _connection.GetDatabase();
            return await db.KeyExistsAsync(key);

        }
    }
}
