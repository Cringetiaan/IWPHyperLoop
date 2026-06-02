using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 0.5f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
