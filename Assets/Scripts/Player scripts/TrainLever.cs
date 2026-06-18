using UnityEngine;
using UnityEngine.InputSystem;

public class TrainLever : MonoBehaviour
{
    [SerializeField]
    GameObject text;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject holoTrain1;

    [SerializeField]
    GameObject holoTrain2;

    [SerializeField]
    GameObject holoTrain3;

    [SerializeField]
    GameObject holoTrain4;

    [SerializeField]
    GameObject digiTrain1;
    Material digiMat1;

    [SerializeField]
    GameObject digiTrain2;
    Material digiMat2;

    [SerializeField]
    GameObject digiTrain3;
    Material digiMat3;

    [SerializeField]
    GameObject digiTrain4;
    Material digiMat4;
    float amount = 1f;
    bool transition = false;

    bool contact;
    bool activated;

    [SerializeField]
    GameObject PlayerCamera;
    [SerializeField]
    GameObject CutsceneCamera;
    [SerializeField]
    GameObject Dialogue;

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

        digiMat1 = digiTrain1.GetComponent<MeshRenderer>().material;
        digiMat2 = digiTrain2.GetComponent<MeshRenderer>().material;
        digiMat3 = digiTrain3.GetComponent<MeshRenderer>().material;
        digiMat4 = digiTrain4.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (contact && !activated)
        {
            if (Interact.triggered)
            {
                text.SetActive(false);

                //FacilityDiaolouge.SetActive(true);

                activated = true;

                transition = true;

                PlayerCamera.SetActive(false);
                CutsceneCamera.SetActive(true);

                Dialogue.SetActive(true);
            }
        }

        if (transition)
        {
            digiTrain1.SetActive(true);
            digiTrain2.SetActive(true);
            digiTrain3.SetActive(true);
            digiTrain4.SetActive(true);

            amount -= 0.001f;
            digiMat1.SetFloat("_Amount", amount);
            digiMat2.SetFloat("_Amount", amount);
            digiMat3.SetFloat("_Amount", amount);
            digiMat4.SetFloat("_Amount", amount);

            if (((digiMat1.GetFloat("_Amount")) <= 0) && ((digiMat2.GetFloat("_Amount")) <= 0) && ((digiMat3.GetFloat("_Amount")) <= 0) && ((digiMat4.GetFloat("_Amount")) <= 0))
            {
                transition = false;
                holoTrain1.SetActive(false);
                holoTrain2.SetActive(false);
                holoTrain3.SetActive(false);
                holoTrain4.SetActive(false);
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