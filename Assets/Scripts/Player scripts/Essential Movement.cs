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

    //Mika - Coyote Time Variables
    float coyoteTime = 0.2f;
    float coyoteTimeCounter = 0.2f;
    InputAction SwitchPolarityAction;

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
    bool dashTrigger = false;


    //Do not touch ever
    public bool DeShittifyDash = false;
    bool dashReset = true;
    public Animator BigBotAnimator;

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

        //Mika
        SwitchPolarityAction = playerInput.actions["SwitchPolarity"];

    }
    // Update is called once per frame
    void Update()
    {
        ReadInput();

        CoyoteTime();
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

        //if(DesiredMoveDir.magnitude > 0f)
        //{
        //    BigBotAnimator.SetBool("isWalking", true);
        //} else
        //{
        //    BigBotAnimator.SetBool("isWalking", false);
        //}   

        if (GetIsGrounded())
        {
            moveGrounded = true;
            //BigBotAnimator.SetBool("isJumping", false);
            //BigBotAnimator.SetBool("isDashing", false);
            //BigBotAnimator.SetBool("isFalling", false);
        } else {
            moveGrounded = false;
            //BigBotAnimator.SetBool("isFalling", true);
        }

        if ((JumpAction.triggered) && (coyoteTimeCounter > 0f) && (jumpTrigger == false))
        {
            jumpTrigger = true;
            //BigBotAnimator.SetBool("isJumping", true);
        }

        if (DashAction.triggered && dashReset == true)
        {
            dashReset = false;
            //BigBotAnimator.SetBool("isDashing", true);
            
            dashTrigger = true;
        }

        if (SwitchPolarityAction.triggered)
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

    //Mika - Coyote Time
    void CoyoteTime()
    {
        if (GetIsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    void MovePlayer() 
    {
        if (DesiredMoveDir.magnitude > 0f)
        {
            BigBotAnimator.SetBool("isWalking", true);
        }
        else
        {
            BigBotAnimator.SetBool("isWalking", false);
        }

        if (moveGrounded == true) 
        {
            BigBotAnimator.SetBool("isJumping", false);
            BigBotAnimator.SetBool("isDashing", false);
            BigBotAnimator.SetBool("isFalling", false);

            rb.AddForce(DesiredMoveDir * MoveSpeed, ForceMode.Acceleration);
        } else
        {
            BigBotAnimator.SetBool("isFalling", true);

            rb.AddForce(DesiredMoveDir * MoveSpeed * 0.5f, ForceMode.Acceleration);
        }

        if (jumpTrigger == true)
        {
            BigBotAnimator.SetBool("isJumping", true);

            coyoteTimeCounter = 0;
            jumpTrigger = false;
            rb.AddForce(0, JumpForce, 0, ForceMode.VelocityChange);
            dashReset = true;
        }

        if (dashTrigger == true)
        {
            BigBotAnimator.SetBool("isDashing", true);

            dashTrigger = false;

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
    }

    //Mika
    void ResetGrav()
    {
        rb.useGravity = true;
    }

    //Mika
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    //Mika
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }
}