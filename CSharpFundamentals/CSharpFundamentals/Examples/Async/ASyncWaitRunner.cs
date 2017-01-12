using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpFundamentals.Examples.Async
{
    public class ASyncWaitRunner : IExampleRunner
    {
        public void RunExample()
        {
            Console.WriteLine("\r\n============== Blocking Application ===============\r\n");
            BlockedApplication app1 = new BlockedApplication();
            app1.PerformTasks();
            Console.WriteLine("\r\n============== Parallel Task Application ===============\r\n");
            ParallelTaskApplication app2 = new ParallelTaskApplication();
            app2.PerformTasks();
            Console.WriteLine("\r\n============== Async Await Application ===============\r\n");
            SyncAwaitApplication app3 = new SyncAwaitApplication();
            app3.PerformTasks();
        }
    }

    public class BlockedApplication
    {
        public void PerformTasks()
        {
            try
            {
                Console.WriteLine("BlockedApplication: Beginning Long Work");
                Console.WriteLine(LongTask());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("BlockedApplication: Beginning other Work");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Other Work");
            }
            
        }

        public string LongTask()
        {
            Thread.Sleep(3000);
            Random random = new Random(Math.Abs(DateTime.Now.GetHashCode()));
            if (random.Next(100) < 50)
            {
                return "BlockedApplication: Long Work Complete";
            }
            else
            {
                throw new Exception("BlockedApplication: LongTask Failed");
            }
            
        }
    }

    public class ParallelTaskApplication
    {
        public void PerformTasks()
        {
            Console.WriteLine("ParallelTaskApplication: Beginning Long Work");
            LongTask();
            Console.WriteLine("ParallelTaskApplication: Beginning Other Work");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("New Other Work");
            }
        }

        public Task LongTask()
        {
            var task = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Random random = new Random(Math.Abs(DateTime.Now.GetHashCode()));
                if (random.Next(100) < 50)
                {
                    return "ParallelTaskApplication: Long Work Complete";
                }
                else
                {
                    //This exception will be swallowed.  What you'll see is the applicaiton will not print anything 50% of the time.
                    throw new Exception("ParallelTaskApplication: LongTask Failed");
                }
            });

            task.ContinueWith((t) =>
            {
                //if this was a GUI app we would have to use Dispatch.Invoke since the UI would be on a different thread.
                if (t.IsFaulted)
                {
                    Console.WriteLine(t.Exception);
                }
                else
                {
                    Console.WriteLine(t.Result);
                }
            });
            return task;
        }
    }

    public class SyncAwaitApplication
    {
        public void PerformTasks()
        {
            Console.WriteLine("SyncAwaitApplication: Beginning Long Work");
            LongTaskAsync();
            Console.WriteLine("SyncAwaitApplication: Beginning Other Work");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("New Other Work");
            }
        }

        public async void LongTaskAsync()
        {
            try
            {
                var task = await Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    Random random = new Random(Math.Abs(DateTime.Now.GetHashCode()));
                    if (random.Next(100) < 50)
                    {
                        return "SyncAwaitApplication: Long Work Complete";
                    }
                    else
                    {
                        //This exception will be swallowed.  What you'll see is the applicaiton will not print anything 50% of the time.
                        throw new Exception("SyncAwaitApplication: LongTask Failed");
                    }
                });

               Console.WriteLine(task);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }



}
