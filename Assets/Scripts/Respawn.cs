using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Abyss"))
        {
            this.transform.position = checkpoint.transform.position;
        }

        if (other.CompareTag("Checkpoint"))
        {
            checkpoint = other.gameObject;
        }
    }
}