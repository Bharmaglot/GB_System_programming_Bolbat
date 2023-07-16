using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class HomeWork_Jobs : MonoBehaviour
{ 
    void Start()
    {
        NativeArray<int> intArray = new NativeArray<int>(new int[] { 1, 5, 11, 4, 54 }, Allocator.Persistent);
        MyJob myJob = new MyJob()
        {
            intArr = intArray,
        };

        StartJob(myJob);
        ShowResult(intArray);
        intArray.Dispose();
    }

    private void StartJob(MyJob myJob)
    {
        JobHandle jobHandle = myJob.Schedule();
        jobHandle.Complete();
    }

    public void ShowResult(NativeArray<int> arr)
    {
        foreach(int elementofArray in arr) Debug.Log($"Element is {elementofArray}");
    }

    public struct MyJob : IJob
    {
        public NativeArray<int> intArr;

        public void Execute()
        {
            for (int i = 0; i < intArr.Length; i++)
            {
                if (intArr[i] > 10) intArr[i] = 0;
            }
        }
    }
}