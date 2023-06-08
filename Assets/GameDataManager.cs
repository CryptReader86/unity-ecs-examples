using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] private Transform _player;

    public static GameDataManager instance;

    public Transform Player => _player;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }
}
