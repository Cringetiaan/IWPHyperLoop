using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 0.5f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
