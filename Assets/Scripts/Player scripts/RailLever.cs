using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RailLever : MonoBehaviour
{
    [SerializeField]
    GameObject text;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject holoRail1;

    [SerializeField]
    GameObject holoRail2;

    [SerializeField]
    GameObject digiRail1;
    Material digiMat1;

    [SerializeField]
    GameObject digiRail2;
    Material digiMat2;
    float amount = 1f;
    bool transition = false;

    bool contact;


    PlayerInput playerInput;
    InputAction Interact;

    //Dialogue window
    //[SerializeField]
    //GameObject FacilityDiaolouge;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        Interact = playerInput.actions["Interact"];

        digiMat1 = digiRail1.GetComponent<MeshRenderer>().material;
        digiMat2 = digiRail2.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (contact)
        {
            if (Interact.triggered)
            {
                //FacilityDiaolouge.SetActive(true);

                transition = true;
            }
        }

        if (transition)
        {
            digiRail1.SetActive(true);
            digiRail2.SetActive(true);

            amount -= 0.005f;
            digiMat1.SetFloat("_Amount", amount);
            digiMat2.SetFloat("_Amount", amount);

            if (((digiMat1.GetFloat("_Amount")) <= 0) && ((digiMat2.GetFloat("_Amount")) <= 0))
            {
                transition = false;
                holoRail1.SetActive(false);
                holoRail2.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(true);

            contact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(false);

            contact = false;
        }
    }
}
