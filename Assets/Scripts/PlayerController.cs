using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 4f;
    public float runSpeed = 6f;
    public float crouchSpeed = 2f;
    public float rotationSpeed = 120f;
    public float smoothSpeed = 8f;

    [Header("Salto")]
    public float jumpForce = 1.0f;   
    public float gravity = -30f;       

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
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        bool shift = Input.GetKey(KeyCode.LeftShift);
        bool ctrl = Input.GetKey(KeyCode.LeftControl);

        isCrouching = ctrl;

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

        // 🔥 ROTACIÓN MÁS CONTROLADA
        transform.Rotate(Vector3.up * horizontal * rotationSpeed * Time.deltaTime);

        // 🔥 SUELO ESTABLE (IMPORTANTE)
        if (controller.isGrounded)
        {
            if (verticalVelocity < -2f)
                verticalVelocity = -2f;

            // SALTO MEJORADO
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

        // 🔥 ANIMACIONES MÁS ESTABLES
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
                    anim.SetInteger("states", 4);
                else
                    anim.SetInteger("states", 0);
            }
            else
            {
                if (!isMoving)
                    anim.SetInteger("states", 0);
                else if (run)
                    anim.SetInteger("states", 2);
                else
                    anim.SetInteger("states", 1);
            }
        }
    }
}