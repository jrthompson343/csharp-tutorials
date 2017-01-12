using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoAsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            //var obj = new AnyOleObject();
            //var result = obj.Init();
            //Console.WriteLine("End Program");

            //var AsyncExample = new AsyncHttpExample("http://127.0.0.1:9090");
            var AsyncExample = new BetterAsyncExample("http://127.0.0.1:9090");
            List<int> ids = new List<int>
            {
                1000,
                12000,
                78,
                3000,
                500,
                31000
            };

            var task = AsyncExample.GetDataByIds(ids);
            Console.WriteLine("Blocking Main thread from quitting...");
            task.Wait();
            Console.WriteLine("Task is now done, calling Readline to keep console open");
            Console.ReadLine();
        }
    }

    public class BetterAsyncExample
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public BetterAsyncExample(string url)
        {
            _url = url;
            _client = new HttpClient();
            _client.Timeout = new TimeSpan(0, 0, 0, 30);
        }

        public async Task<bool> GetDataByIds(IList<int> ids)
        {
            IList<Task> tasks = new List<Task>();
            foreach (var resourceId in ids)
            {
                var url = resourceId < 100 ? "http://127.0.0.1:9091" : _url + "/" + resourceId;
                Console.WriteLine("Requesting: " + url);
                var requestTask = _client.GetAsync(url).ContinueWith(t => HandleResponse(t, resourceId));
                tasks.Add(requestTask);
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch
            {
                for (var i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].IsFaulted || tasks[i].IsCanceled)
                    {
                        Console.WriteLine("Task failed: " + i);
                    }
                }
            }
            Console.WriteLine("All tasks have completed");
            _client.Dispose();
            return true;
        }

        public Task<string> HandleResponse(Task<HttpResponseMessage> task, int id)
        {
            task.Result.EnsureSuccessStatusCode();
            var result = task.Result.Content.ReadAsStringAsync();
            result.ContinueWith(t => Console.WriteLine("Server Responded with: " + t.Result));
            return result;
        }
    }
    public class AsyncHttpExample
    {
        private readonly string _url;

        public AsyncHttpExample(string url)
        {
            _url = url;
            
        }

        public async Task<bool> GetDataByIds(IList<int> ids)
        {
            List<Task> resultTasks = new List<Task>();
            foreach (var id in ids)
            {
                var task = SendId(id);
                if (!task.IsFaulted)
                {
                    var continuation = task.ContinueWith(t => Console.WriteLine(t.Result + " Completed"));
                    if (!continuation.IsFaulted)
                    {
                        resultTasks.Add(continuation);
                    }
                }
                else
                {
                    if (task != null)
                    {
                        foreach (var exception in task.Exception.Flatten().InnerExceptions)
                        {
                            Console.WriteLine("Task " + ids + " threw an error: " + exception.Message);
                        }
                    }
                }
            }

            await Task.WhenAll(resultTasks);
            Console.WriteLine("All tasks' continuations have run");
            return true;
        }

        public async Task<string> SendId(int id)
        {
            var url = _url + "/" + id;
            if (id < 100)
            {
                //bad url
                url = "http://127.0.0.1:9091";
            }

            Console.WriteLine("Sending request: " + url);

            using (var client = new HttpClient())
            {
                using (var r = await client.GetAsync(new Uri(url)))
                {
                    var result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }

    }
    public class AnyOleObject
    {
        public async Task<bool> Init()
        {
            var series = Enumerable.Range(1, 5).ToList();
            var tasks = new List<Task<Tuple<int, bool>>>();
            foreach (var i in series)
            {
                Console.WriteLine("Starting Process {0}", i);
                var task = DoWorkAsync(i);
                if (!task.IsFaulted)
                {
                    tasks.Add(task);
                }
                else
                {
                    foreach (var exception in task.Exception.Flatten().InnerExceptions)
                    {
                        Console.WriteLine("Task " + i + " threw an error: " + exception.Message);
                    }
                }

            }
            foreach (var task in await Task.WhenAll(tasks))
            {
                if (task.Item2)
                {
                    Console.WriteLine("Ending Process {0}", task.Item1);
                }
            }
            return true;
        }

        public async Task<Tuple<int, bool>> DoWorkAsync(int i)
        {
            Console.WriteLine("working..{0}", i);
            if (i == 3)
            {
                throw new Exception("I just blue myself");
            }
            await Task.Delay(1000);
            return Tuple.Create(i, true);
        }
    }
}
