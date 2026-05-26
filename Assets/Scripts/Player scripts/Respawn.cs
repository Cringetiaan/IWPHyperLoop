using UnityEngine;
using UnityEngine.InputSystem;

public class Respawn : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction ResetAction;

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

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        ResetAction = playerInput.actions["Reset"];
    }

    void Update()
    {
        if (ResetAction.triggered)
        {
            this.transform.position = checkpoint.transform.position;
        }
    }
}