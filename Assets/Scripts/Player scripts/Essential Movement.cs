using JetBrains.Annotations;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class EssentialMovement : MonoBehaviour
{

    //Essentials
    [SerializeField]
    Rigidbody rb;

    Vector3 MoveInput;
    Vector3 DesiredMoveDir;
    

    
    PlayerInput playerInput;
    InputAction MoveAction;
    InputAction JumpAction;
    InputAction DashAction;

    //Movement Options
    [Header("Movement Options")]
    [SerializeField]
    float MoveSpeed;
    [SerializeField]
    float MaxMoveSpeed;
    //[SerializeField]
    //int JumpCount;
    [SerializeField]
    float JumpForce;
    [SerializeField]
    float DashForce;

    //Do not touch ever
    public bool DeShittifyDash = false;
    public bool dashReset = true;


    [SerializeField]
    CinemachineCamera Cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assigning the player input and actions
        playerInput = GetComponent<PlayerInput>();
        MoveAction = playerInput.actions["Move"];
        JumpAction = playerInput.actions["Jump"];
        DashAction = playerInput.actions["Dash"];
    }
    // Update is called once per frame
    void Update()
    {
       
        MovePlayer();
        if(rb.linearVelocity.magnitude > MaxMoveSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * MaxMoveSpeed;
        }

    }

    //Ground check
    private bool GetIsGrounded()
    {
        return (Physics.Raycast(transform.position, Vector3.down, 1.1f, LayerMask.GetMask("Ground")));
    }

    void FixedUpdate()
    {
       
  
        
    }

    void MovePlayer()
    {
        MoveInput = MoveAction.ReadValue<Vector3>();
        Vector3 CamF = Cam.transform.forward;
        Vector3 CamR = Cam.transform.right;

        DesiredMoveDir = (CamF * MoveInput.z + CamR * MoveInput.x).normalized;
        if (GetIsGrounded())
        {
            rb.AddForce(DesiredMoveDir * MoveSpeed, ForceMode.Acceleration);
        } else {
            rb.AddForce(DesiredMoveDir * MoveSpeed * 0.5f, ForceMode.Acceleration);
        }

        if (JumpAction.triggered && GetIsGrounded())
        {
            rb.AddForce(0, JumpForce, 0, ForceMode.Impulse);
            dashReset = true;
        }

        if (DashAction.triggered && dashReset == true)
        {
            dashReset = false;
            //normal dash
            if (DeShittifyDash)
            {
                rb.AddForce(DesiredMoveDir * DashForce, ForceMode.Impulse);
            }

            //garbo dash
            else
            {
                rb.AddForce(DesiredMoveDir * DashForce * 0.4f, ForceMode.Impulse);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }
}