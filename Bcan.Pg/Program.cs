using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


public static class ThreadInformation
{
    public static void ShowThreadInfo(string taskName, bool detailed = false)
    {
        Thread thrd = Thread.CurrentThread;
        var msg = 
                string.Format("{0} thread information\n", taskName) +
                string.Format("   Thread ID: {0}\n", thrd.ManagedThreadId) +
                (detailed 
                    ? ( 
                        string.Format("   Background: {0}\n", thrd.IsBackground) +
                        string.Format("   Thread Pool: {0}\n", thrd.IsThreadPoolThread)) 
                    : string.Empty);
        
        Console.WriteLine(msg);
    }
}

public class MyService
{
    private string _result = "Not started";

    public void StartBackgroundWork()
    {
        ThreadInformation.ShowThreadInfo("StartBackgroundWork");
        _ = BackgroundTaskAsyncOnDiff3();
    }


    /// <summary>
    /// Runs the background the task within the same thread of the caller. 
    /// </summary>
    /// <returns></returns>
    private async Task BackgroundTaskAsync()
    {
        ThreadInformation.ShowThreadInfo("BackgroundTaskAsync");
        // Simulate I/O or CPU-bound work
        await Task.Delay(3000);
        _result = "Work completed at " + DateTime.Now;
    }

    /// <summary>
    /// Runs the background task within a different thread picked from ThreadPool  
    /// </summary>
    /// <returns></returns>
    private async Task BackgroundTaskAsyncOnDiff1()
    {
        await Task.Run(async delegate 
        {
            ThreadInformation.ShowThreadInfo("BackgroundTaskAsyncOnDiff");
            await Task.Delay(3000);
            _result = "Work completed at " + DateTime.Now;
        });
    }

    private async Task BackgroundTaskAsyncOnDiff2()
    {
        await Task.Run(async () => 
        {
            ThreadInformation.ShowThreadInfo("BackgroundTaskAsyncOnDiff");
            await Task.Delay(3000);
            _result = "Work completed at " + DateTime.Now;
        });
    }

    public async Task BackgroundTaskAsyncOnDiff3()
    {
        try
        {
            await Task.Run(() => 
            {
                ThreadInformation.ShowThreadInfo("BackgroundTaskAsyncOnDiff2");
                Thread.Sleep(3000);
                _result = $"Work completed at {DateTime.Now:T} on Thread ID: {Thread.CurrentThread.ManagedThreadId}";
            });

            // _result = $"Work completed at {DateTime.Now:T} on Thread ID: {Thread.CurrentThread.ManagedThreadId}";
        }
        catch (Exception ex)
        {
            _result = $"Failed: {ex.Message}";
        }
    }

    public string Result => _result;
}

public class Example
{
    public static async Task RunAsync()
    {
        Console.WriteLine("RunAsync Entry");
        await Task.Run(() => 
        {
            Console.WriteLine("{0} thread ID: {1}", "Background", Thread.CurrentThread.ManagedThreadId);
            for(int i = 0; i < 4; ++i)
            {
                Console.WriteLine("...");
                Thread.Sleep(1000);
            }
        });
        Console.WriteLine("RunAsync Exit");

    }

    public static void CallAsyncFromSync()
    {
        ThreadInformation.ShowThreadInfo("CallAsyncFromSync");
        RunAsync();
        Console.WriteLine("Sync function exit");
    }

    public static void Main()
    {
        // ThreadInformation.ShowThreadInfo("Main");
        // CallAsyncFromSync();
        // for (int i = 0; i < 7; i++)
        // {
        //     Thread.Sleep(1000);
        // }

        // var t = Task.Run(async delegate 
        // {
        //     await Task.Delay(6000);
        // });
        // t.Wait();

        var service = new MyService();
        _ = service.BackgroundTaskAsyncOnDiff3();

        ThreadInformation.ShowThreadInfo("Main");
        // Poll every second for 7 seconds
        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine($"[{DateTime.Now:T}] Result: {service.Result}");
            Thread.Sleep(1000);
        }

        Console.WriteLine("Done polling.");
    }

    private static void PrepareDish(uint id, int milisecs)
    {
        Console.WriteLine($"Chef {id} is preparing Dish {id}");
        Thread.Sleep(milisecs);
        Console.WriteLine($"Dish {id} is ready.");
    }

    private static async Task WhenAllExample()
    {
        Task myTask1 = Task.Run(() =>
        {
            PrepareDish(1, 10000);

            // Console.WriteLine("Chef 1 is preparing Dish 1");
            // await Task.Delay(10000); // Chef 1 takes 10 seconds to prepare Dish 1
            // Console.WriteLine("Dish 1 is ready!");
        });

        Task myTask2 = Task.Run(() =>
        {
            PrepareDish(2, 1000);

            // Console.WriteLine("Chef 2 is preparing Dish 2");
            // await Task.Delay(1000); // Chef 2 takes 1 second to prepare Dish 2
            // Console.WriteLine("Dish 2 is ready!");
        });

        Task myTask3 = Task.Run(() =>
        {
            PrepareDish(3, 1000);

            // Console.WriteLine("Chef 3 is preparing Dish 3");
            // await Task.Delay(1000); // Chef 3 takes 1 second to prepare Dish 3
            // Console.WriteLine("Dish 3 is ready!");
        });

        await Task.WhenAll(myTask1, myTask2, myTask3);
        Console.WriteLine("Manager: All dishes are ready! Task completed!");
    }
}

//https://learn.microsoft.com/en-us/training/modules/implement-asynchronous-tasks/3-implement-asynchronous-file-input-output