using UnityEngine;

public class SheepMoveBehavior : MonoBehaviour
{
    [SerializeField]
    private float _sheepSpeed;

    [SerializeField]
    private float _zLimit;

    void Update()
    {
        transform.Translate(_sheepSpeed * Time.deltaTime * transform.forward);

        var position = transform.position;
        if (position.z >= _zLimit)
        {
            position.z = -_zLimit;
            transform.position = position;
        }
    }
}
