using UnityEngine;
using UnityEngine.InputSystem;

public class EssentialMovement : MonoBehaviour
{

    //Essentials
    [SerializeField]
    Rigidbody rb;

    Vector2 MoveInput;
    Vector3 MoveDir;
    Vector3 JumpDir;

    [SerializeField]
    InputActionReference Move;
    InputActionReference Jump;

    //Movement Options
    [Header("Movement Options")]
    [SerializeField]
    float MoveSpeed;
    [SerializeField]
    bool IsGrounded;
    [SerializeField]
    int JumpCount;


   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        JumpDir = new Vector3(0, 10, 0);
    }
    // Update is called once per frame
    void Update()
    {
        MoveInput = Move.action.ReadValue<Vector2>();

    }

    void FixedUpdate()
    {
        if(Jump.action.triggered && IsGrounded)
        {
            rb.AddForce(JumpDir, ForceMode.Impulse);
        }
        MoveDir = new Vector3(MoveInput.x, 0, MoveInput.y);
        rb.AddForce(MoveDir * MoveSpeed, ForceMode.Acceleration);
    }


   
}

