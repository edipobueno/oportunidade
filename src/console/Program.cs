using console.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HttpClient web = new HttpClient())
            {
                Task<HttpResponseMessage> taskFeed = web.GetAsync("https://localhost:44306/api/feeds");

                taskFeed.ContinueWith(WriteFeeds);
                
                taskFeed.Wait();

                Task<HttpResponseMessage> taskWord = web.GetAsync("https://localhost:44306/api/words/top/10");

                taskWord.ContinueWith(WriteTop10);

                taskWord.Wait();
            }

            Console.ReadLine();
        }

        private static object WriteFeeds(Task<HttpResponseMessage> task)
        {
            Console.WriteLine(new string('-', 200));

            FeedModel[] resultFeed = JsonConvert.DeserializeObject<FeedModel[]>(task.Result.Content.ReadAsStringAsync().Result);

            Console.WriteLine("Aqui estão os títulos e a quantidade de palavras:");
            foreach (FeedModel feed in resultFeed)
            {
                Console.WriteLine($"{feed.title} - {feed.wordsCount}");
            }

            return null;
        }

        private static object WriteTop10(Task<HttpResponseMessage> task)
        {
            Console.WriteLine(new string('-', 200));

            WordModel[] resultWord = JsonConvert.DeserializeObject<WordModel[]>(task.Result.Content.ReadAsStringAsync().Result);

            Console.WriteLine("Aqui estão as palavras e a quantidade que elas apareceram:");
            foreach (WordModel word in resultWord)
            {
                Console.WriteLine($"{word.word} - {word.quantity}");
            }

            return null;
        }
    }
}
