using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEMPLeverScript : MonoBehaviour
{
    public GameObject text;
    Collider CubeCollider;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject bridge;

    PlayerInput playerInput;
    InputAction Interact;


    [SerializeField]
    GameObject killPlat;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CubeCollider = GetComponent<Collider>();
        playerInput = player.GetComponent<PlayerInput>();
        Interact = playerInput.actions["Interact"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Interact.triggered)
            {
                player.GetComponent<EssentialMovement>().DeShittifyDash = true;

                bridge.transform.rotation = Quaternion.Euler(0, 0, 0);

                killPlat.SetActive(false);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(false);
        }
    }
}
