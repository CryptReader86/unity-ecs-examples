using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;

public class SheepParallelSpawner : MonoBehaviour
{
    private struct SheepMoveJob : IJobParallelForTransform
    {
        private const float ZLimit = 50.0f;

        private const float SheepSpeed = 1.0f;
        private static readonly Vector3 SheepForward = Vector3.forward;

        public void Execute(int index, TransformAccess transform)
        {
            transform.position += SheepSpeed * Time.deltaTime * SheepForward;

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

    private GameObject[] _sheeps;

    void Start()
    {
        _sheeps = new GameObject[_sheepsToSpawn];

        for (var i = 0; i < _sheepsToSpawn; i++)
        {
            var sheepPosition = new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));

            _sheeps[i] = Instantiate(_sheepPrefab, sheepPosition, Quaternion.identity);
        }
    }

    /*
    private void Update()
    {
        foreach(var sheep in _sheeps)
        {
            var sheepTransform = sheep.transform;

            sheepTransform.Translate(_sheepSpeed * Time.deltaTime * transform.forward);

            var position = sheepTransform.position;
            if (position.z >= _zLimit)
            {
                position.z = -_zLimit;
                sheepTransform.position = position;
            }
        }
    }
    */
}
