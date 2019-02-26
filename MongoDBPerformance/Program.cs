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
            WriteConcern WCvalue = new WriteConcern(0,null,false,false);  
            var client = new MongoClient("mongodb://127.0.0.1:27017").WithWriteConcern(WCvalue);

            var database = client.GetDatabase("MS3");
            bool isMongoLive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            Console.WriteLine(isMongoLive);

            var collec = database.GetCollection<BsonDocument>("datas");
            long count = 0;
           
            Stopwatch st1 = new Stopwatch();
            Stopwatch st2 = new Stopwatch();
            st2.Start(); 
            for (long i = 0; i < 5000000; i++)
            {
                st1.Restart();
               
                var doc = new BsonDocument
            {
                { "camera", "1" },
                { "data", "1254874458774587456985" },
                { "count", i.ToString() },
                { "elapsed", st1.Elapsed.TotalMilliseconds},
                { "date", DateTime.Now}
            };

                collec.InsertOneAsync(doc);

               
                Console.WriteLine("count " + count++ + " " + st1.Elapsed.TotalMilliseconds);
               

                //Thread.Sleep(1);

            }
            Console.WriteLine(st2.Elapsed.TotalSeconds.ToString() + " Seconds");
            Console.WriteLine(st2.Elapsed.TotalMinutes.ToString() + " minutes");
            Console.WriteLine(st2.Elapsed.TotalHours.ToString() + " Hours");
            Console.WriteLine(DateTime.Now);
            Console.ReadKey();
        }
    }
}
