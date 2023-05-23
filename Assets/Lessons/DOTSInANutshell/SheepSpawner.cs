using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _sheepPrefab;

    [SerializeField]
    private int _sheepsToSpawn;

    void Start()
    {
        for (var i = 0; i < _sheepsToSpawn; i++)
        {
            var sheepPosition = new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));

            Instantiate(_sheepPrefab, sheepPosition, Quaternion.identity);
        }
    }
}
