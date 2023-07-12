using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class HomeWork_JobParallelFor : MonoBehaviour
{
    NativeArray<Vector3> _positions;
    NativeArray<Vector3> _velocities;
    NativeArray<Vector3> _finalPosition;

    [SerializeField] private int _numberOfElements;
    [SerializeField] private float _startDistance;
    [SerializeField] private float _startVelocity;



    void Start()
    {
        _positions = new NativeArray<Vector3>(_numberOfElements, Allocator.Persistent);
        _velocities = new NativeArray<Vector3>(_numberOfElements, Allocator.Persistent);
        _finalPosition = new NativeArray<Vector3>(_numberOfElements, Allocator.Persistent);


        for (int i = 0; i < _numberOfElements; i++)
        {
            _positions[i] = Random.insideUnitSphere * Random.Range(0, _startDistance);
            _velocities[i] = Random.insideUnitSphere * Random.Range(0, _startVelocity);
        }

        StartJob();
        ShowResult();
    }

    private void ShowResult()
    {
        for (int i = 0; i < _finalPosition.Length; i++) Debug.Log($"Next position for element {i + 1} is {_finalPosition[i]}");
    }

    private void StartJob()
    {
        MoveJob moveJob = new MoveJob()
        {
            Positions = _positions,
            Velocities = _velocities,
            FinalPositions = _finalPosition,
            DeltaTime = Time.deltaTime
        };

        JobHandle moveHandle = moveJob.Schedule(_numberOfElements, 0);
        moveHandle.Complete();
    }

    private void OnDestroy()
    {
        _positions.Dispose();
        _velocities.Dispose();
        _finalPosition.Dispose();
    }

    public struct MoveJob : IJobParallelFor
    {
        public NativeArray<Vector3> Positions;
        public NativeArray<Vector3> Velocities;
        public NativeArray<Vector3> FinalPositions;
        [ReadOnly] public float DeltaTime;

        public void Execute(int index)
        {
            FinalPositions[index] = Positions[index] + Velocities[index] * DeltaTime;
        }
    }
}
