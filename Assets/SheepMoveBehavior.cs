using UnityEngine;

public class SheepMoveBehavior : MonoBehaviour
{
    [SerializeField]
    private float _sheepSpeed;

    void Update()
    {
        transform.Translate(_sheepSpeed * Time.deltaTime * transform.forward);
    }
}
