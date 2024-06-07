// See https://aka.ms/new-console-template for more information

using StackExchange.Redis;

var redis = ConnectionMultiplexer.Connect("1.1.1.1");

// Redis üzerinde bir lock anahtarı oluştur
var lockKey = "test_key";

// Lock'ı elde et
var db = redis.GetDatabase();
var lockValue = Guid.NewGuid().ToString();
var acquiredLock = db.LockTake(lockKey, lockValue, TimeSpan.FromSeconds(10));

if (acquiredLock)
{
    Console.WriteLine("Lock acquired. Do something...");

    // İşlemi gerçekleştir (örneğin, başka bir servise istek yap)

    // Lock'ı serbest bırak
    db.LockRelease(lockKey, lockValue);
    Console.WriteLine("Lock released.");
}
else
{
    Console.WriteLine("Failed to acquire lock. Another process may be holding the lock.");
}