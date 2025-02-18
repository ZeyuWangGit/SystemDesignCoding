using System.Security.Cryptography;
using System.Text;
using StackExchange.Redis;

namespace Coding.ShortUrlService;

public class HashCollisionShortenerService(IDatabase database)
{
    private readonly IDatabase _database = database;
    private const string BloomFilterKey = "short_url_bloom";
    private const string UrlDatabaseKey = "short_url_map";

    private static string GenerateShortUrl(string longUrl, int attempts = 0)
    {
        using var sha1 = SHA1.Create();
        var saltedUrl = attempts > 0 ? $"{longUrl}-salt{attempts}" : longUrl;
        var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(saltedUrl));
        var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        return hashString.Substring(0, 7);
    }
    
    public async Task<string> ShortenUrlAsync(string longUrl)
    {
        var attempt = 0;
        string shortUrl;

        while (true)
        {
            shortUrl = GenerateShortUrl(longUrl, attempt);

            // 先用 Bloom Filter 快速检查是否可能已存在
            var mightExist = (bool)await _database.ExecuteAsync("BF.EXISTS", BloomFilterKey, shortUrl);
            if (!mightExist)
                break;

            // 确保 Redis 里也没有存储（防止 Bloom Filter 误判）
            if (!await _database.HashExistsAsync(UrlDatabaseKey, shortUrl))
                break;

            attempt++; // 发生哈希碰撞，尝试新的 short URL
        }

        // 存入 Redis
        await _database.HashSetAsync(UrlDatabaseKey, shortUrl, longUrl);
        await _database.ExecuteAsync("BF.ADD", BloomFilterKey, shortUrl); // 加入 Bloom Filter

        return shortUrl;
    }
}