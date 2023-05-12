using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;

public class SheepParallelSpawner : MonoBehaviour
{
    private struct SheepMoveJob : IJobParallelForTransform
    {
        private const float ZLimit = 50.0f;

        private const float SheepSpeed = 0.1f;
        private static readonly Vector3 SheepForward = Vector3.forward;

        public void Execute(int index, TransformAccess transform)
        {
            transform.position += SheepSpeed * SheepForward;

            var position = transform.position;
            if (position.z >= ZLimit)
            {
                position.z = -ZLimit;
                transform.position = position;
            }
        }
    }

    [SerializeField]
    private GameObject _sheepPrefab;

    [SerializeField]
    private int _sheepsToSpawn;

    private SheepMoveJob _sheepMoveJob;
    private JobHandle _sheepMoveJobHandle;
    private TransformAccessArray _sheepTransforms;

    void Start()
    {
        var sheepTransforms = new Transform[_sheepsToSpawn];

        for (var i = 0; i < _sheepsToSpawn; i++)
        {
            var sheepPosition = new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));

            sheepTransforms[i] = Instantiate(_sheepPrefab, sheepPosition, Quaternion.identity).transform;
        }

        _sheepTransforms = new TransformAccessArray(sheepTransforms);
    }

    private void Update()
    {
        _sheepMoveJob = new SheepMoveJob { };
        _sheepMoveJobHandle = _sheepMoveJob.Schedule(_sheepTransforms);
    }

    private void LateUpdate()
    {
        _sheepMoveJobHandle.Complete();
    }

    private void OnDestroy()
    {
        _sheepTransforms.Dispose();
    }
}
