using UnityEngine;
using UnityEngine.InputSystem;

public class RemoveLaterMagBlock : MonoBehaviour
{
    [SerializeField]
    Collider CubeCollider;
    [SerializeField]
    GameObject MagBlock;

    [SerializeField]
    PlayerInput playerInput;
    InputAction interactAction;
    private void Start()
    {
       
        interactAction = playerInput.actions["Interact"];
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactAction.triggered)
        {
            if (MagBlock.GetComponent<MagneticBlocks>().IsPlusPolarity)
            {

                MagBlock.GetComponent<MagneticBlocks>().IsPlusPolarity = false;
            }
            else if (!MagBlock.GetComponent<MagneticBlocks>().IsPlusPolarity)
            {
                MagBlock.GetComponent<MagneticBlocks>().IsPlusPolarity = true;
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (interactAction.triggered)
            //{
            //    if (MagBlock.GetComponent<MagneticBlocks>().IsPlusPolarity)
            //    {

            //        MagBlock.GetComponent<MagneticBlocks>().IsPlusPolarity = false;
            //    }
            //    else if (!MagBlock.GetComponent<MagneticBlocks>().IsPlusPolarity)
            //    {
            //        MagBlock.GetComponent<MagneticBlocks>().IsPlusPolarity = true;
            //    }

            //}
        }
    }
}
