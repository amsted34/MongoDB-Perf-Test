using System;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading; 



namespace MongoDBPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("MS3");
            bool isMongoLive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            Console.WriteLine(isMongoLive);

            var collec = database.GetCollection<BsonDocument>("datas");
            long count = 0;

            Stopwatch st1 = new Stopwatch();
            st1.Start();
            long previousTime = st1.ElapsedMilliseconds;
            for (long i = 0; i < 5000000; i++)
            {
                var doc = new BsonDocument
            {
                { "camera", "1" },
                { "data", "1254874458774587456985" },
                { "count", i.ToString() },
                { "elapsed", st1.ElapsedMilliseconds - previousTime },
                { "date", DateTime.Now}

            };
                collec.InsertOne(doc);
                
                previousTime = st1.ElapsedMilliseconds;
                Console.WriteLine("count " + count++);
                Thread.Sleep(1);

            }
            Console.WriteLine(st1.ElapsedMilliseconds.ToString());
            Console.WriteLine(DateTime.Now);
            Console.ReadKey();
        }
    }
}
