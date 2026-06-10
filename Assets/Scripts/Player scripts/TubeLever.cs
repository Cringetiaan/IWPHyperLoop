using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TubeLever : MonoBehaviour
{
    [SerializeField]
    GameObject text;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject holoTube;

    [SerializeField]
    GameObject digiTube;
    Material digiMat;
    float amount = 1f;
    bool transition = false;
    //bool glassFade = false;

    //[SerializeField]
    //Rotate rotate;

    [SerializeField]
    Material glassMat;

    //[SerializeField]
    //GameObject bridge;

    bool contact;
    bool activated;

    PlayerInput playerInput;
    InputAction Interact;

    //Dialogue window
    [SerializeField]
    GameObject FacilityDiaolouge;

    [SerializeField]
    GameObject PlayerCamera;
    [SerializeField]
    GameObject CutsceneCamera;
    [SerializeField]
    GameObject Dialogue;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        Interact = playerInput.actions["Interact"];

        digiMat = digiTube.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (contact && !activated)
        {
            if (Interact.triggered)
            {
                player.GetComponent<EssentialMovement>().DeShittifyDash = true;
                //FacilityDiaolouge.SetActive(true);

                //bridge.transform.rotation = Quaternion.Euler(0, 0, 0);

                //rotate.rotationSpeed = 0f;

                activated = true;

                transition = true;

                PlayerCamera.SetActive(false);
                CutsceneCamera.SetActive(true);

                Dialogue.SetActive(true);
               
            }
        }

        if (transition)
        {
            digiTube.SetActive(true);

            amount -= 0.005f;
            digiMat.SetFloat("_Amount", amount);

            if ((digiMat.GetFloat("_Amount")) <= 0)
            {
                transition = false;
                holoTube.SetActive(false);
                digiTube.GetComponent<MeshRenderer>().material = glassMat;
                //glassFade = true;
            }
        }

        //if (glassFade)
        //{
        //    if (glassMat.color.a > 25)
        //    {
        //        glassMat.color.a -= 1;
        //    }
        //}
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
