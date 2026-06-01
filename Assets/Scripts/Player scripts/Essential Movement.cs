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
    InputAction InteractAction;

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


    bool moveGrounded = false;
    bool jumpTrigger = false;


    //Do not touch ever
    public bool DeShittifyDash = false;
    bool dashReset = true;


    [SerializeField]
    CinemachineCamera Cam;

    [SerializeField]
    GameObject model;

    //Level2 mechanic + visul
    public Globalvariables PolarityVar;
    public ParticleSystem PlusPolarityParticles;
    public ParticleSystem MinusPolarityParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assigning the player input and actions
        playerInput = GetComponent<PlayerInput>();
        MoveAction = playerInput.actions["Move"];
        JumpAction = playerInput.actions["Jump"];
        DashAction = playerInput.actions["Dash"];
        InteractAction = playerInput.actions["Interact"];

    }
    // Update is called once per frame
    void Update()
    {

        ReadInput();
        
    }

    void FixedUpdate()
    {
        MovePlayer();
        if (rb.linearVelocity.magnitude > MaxMoveSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * MaxMoveSpeed;
        }

        if (DesiredMoveDir.magnitude != 0f)
        {
            model.transform.rotation = Quaternion.LookRotation(new Vector3(DesiredMoveDir.x, 0, DesiredMoveDir.z));
        }


    }

    //Ground check
    private bool GetIsGrounded()
    {
        return (Physics.Raycast(transform.position, Vector3.down, 1.1f, LayerMask.GetMask("Ground")));
    }

    void ReadInput()
    {
        MoveInput = MoveAction.ReadValue<Vector3>();
        Vector3 CamF = Cam.transform.forward;
        Vector3 CamR = Cam.transform.right;

        CamF.y = 0;
        CamR.y = 0;

        DesiredMoveDir = (CamF * MoveInput.z + CamR * MoveInput.x).normalized;
        if (GetIsGrounded())
        {
            moveGrounded = true;
        } else {
            moveGrounded = false;
        }

        if (JumpAction.triggered && GetIsGrounded())
        {
            jumpTrigger = true;
        }

        if (DashAction.triggered && dashReset == true)
        {
            dashReset = false;
            //normal dash
            if (DeShittifyDash)
            {
                rb.AddForce(DesiredMoveDir * DashForce, ForceMode.VelocityChange);

                rb.useGravity = false;
                Invoke(nameof(ResetGrav), 0.25f);
            }

            //garbo dash
            else
            {
                rb.AddForce(DesiredMoveDir * DashForce * 0.4f, ForceMode.VelocityChange);

                rb.useGravity = false;
                Invoke(nameof(ResetGrav), 0.15f);
            }
        }

        if (InteractAction.triggered)
        {
            if(PolarityVar.PlusPolarity)
            {
                PolarityVar.PlusPolarity = false;
                MinusPolarityParticles.Play();
            }
            else
            {
                PolarityVar.PlusPolarity = true;
                PlusPolarityParticles.Play();
            }
           
        }
    }

    void MovePlayer() 
    {
        if (moveGrounded == true) 
        {
            rb.AddForce(DesiredMoveDir * MoveSpeed, ForceMode.Acceleration);
        } else
        {
            rb.AddForce(DesiredMoveDir * MoveSpeed * 0.5f, ForceMode.Acceleration);
        }

        if (jumpTrigger == true)
        {
            jumpTrigger = false;
            rb.AddForce(0, JumpForce, 0, ForceMode.VelocityChange);
            dashReset = true;
        }
    }

    void ResetGrav()
    {
        rb.useGravity = true;
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