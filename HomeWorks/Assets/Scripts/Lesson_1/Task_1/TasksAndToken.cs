using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TasksAndToken : MonoBehaviour
{
    void Start()
    {
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken cancelToken = cancelTokenSource.Token;

        TasksAsync(cancelToken);

        cancelTokenSource.Cancel();
        cancelTokenSource.Dispose();
    }

    async void TasksAsync(CancellationToken cancelToken)
    {
        await Task.WhenAll(TaskOneAsync(cancelToken), TaskTwoAsync(cancelToken));
    }

    async Task TaskOneAsync(CancellationToken cancelToken)
    {
        if (cancelToken.IsCancellationRequested)
        {
            Debug.Log("Operation interrupted by token.");
            return;
        }
        await Task.Delay(1000);
        Debug.Log("Task finished after one second delay.");
    }

    async Task TaskTwoAsync(CancellationToken cancelToken)
    {
        int frames = 60;
        while (frames > 0)
        {
            if (cancelToken.IsCancellationRequested)
            {
                Debug.Log("Operation interrupted by token.");
                return;
            }
            frames = frames - 1;
            await Task.Yield();
        }
        Debug.Log("Task finishe after 60 frames.");
    }
}
