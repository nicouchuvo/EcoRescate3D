using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 4f;
    public float runSpeed = 7f;
    public float crouchSpeed = 2f;
    public float rotationSpeed = 200f;
    public float smoothSpeed = 10f;

    [Header("Salto")]
    public float jumpForce = 2.5f;
    public float gravity = -25f;

    [Header("Referencias")]
    public Animator anim;

    private CharacterController controller;
    private float verticalVelocity;
    private float smoothVelocity;
    private bool isCrouching;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");   // RAW = sin retraso
        float horizontal = Input.GetAxisRaw("Horizontal");

        bool shift = Input.GetKey(KeyCode.LeftShift);
        bool ctrl = Input.GetKey(KeyCode.LeftControl);

        isCrouching = ctrl;

        // CORRECCIÓN CLAVE (esto arregla tu bug de quedarse quieto)
        bool isMoving = Mathf.Abs(vertical) > 0.1f;
        bool run = shift && isMoving && !isCrouching;

        float targetSpeed = 0f;

        if (isCrouching)
            targetSpeed = crouchSpeed * vertical;
        else if (run)
            targetSpeed = runSpeed * vertical;
        else
            targetSpeed = speed * vertical;

        smoothVelocity = Mathf.Lerp(smoothVelocity, targetSpeed, Time.deltaTime * smoothSpeed);

        // ROTACIÓN
        transform.Rotate(Vector3.up * horizontal * rotationSpeed * Time.deltaTime);

        // SUELO
        if (controller.isGrounded)
        {
            if (verticalVelocity < -2f)
                verticalVelocity = -2f;

            // SALTO
            if (Input.GetKeyDown(KeyCode.Space) && !isCrouching)
            {
                verticalVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }

        // GRAVEDAD
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = transform.forward * smoothVelocity;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);

        // ANIMACIONES (REHECHAS)
        anim.SetFloat("yVelocity", verticalVelocity);
        anim.SetBool("crouch", isCrouching);

        if (!controller.isGrounded)
        {
            anim.SetInteger("states", 3); // Jump
        }
        else
        {
            if (isCrouching)
            {
                if (isMoving)
                    anim.SetInteger("states", 4); // Crouch walk
                else
                    anim.SetInteger("states", 0); // Idle crouch
            }
            else
            {
                if (!isMoving)
                    anim.SetInteger("states", 0); // Idle
                else if (run)
                    anim.SetInteger("states", 2); // Run
                else
                    anim.SetInteger("states", 1); // Walk
            }
        }
    }
}