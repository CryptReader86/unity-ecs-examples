/*
using UnityEngine;
using Unity.Mathematics;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform[] _waypoints;

    private float3[] _waypointsArray;

    public static GameDataManager instance;

    public Transform Player => _player;

    public float3[] WaypointsArray => _waypointsArray;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        _waypointsArray = new float3[_waypoints.Length];
        for (var i = 0; i < _waypoints.Length; i++)
        {
            _waypointsArray[i] = _waypoints[i].position;
        }
    }
}
*/