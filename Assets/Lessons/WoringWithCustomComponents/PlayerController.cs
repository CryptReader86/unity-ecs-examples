using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _rotationSpeed = 100.0f;


    void Update()
    {
        var translation = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
        var rotation = Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}
