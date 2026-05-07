using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEMPLeverScript : MonoBehaviour
{
    public GameObject text;
    Collider CubeCollider;

    [SerializeField]
    GameObject player;

    PlayerInput playerInput;
    InputAction Interact;

    //Dialogue window
    [SerializeField]
    GameObject FacilityDiaolouge;


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
                FacilityDiaolouge.SetActive(true);


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
