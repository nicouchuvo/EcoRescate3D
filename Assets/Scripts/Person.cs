using UnityEngine;

public class player : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2f;
    public float speedrun = 5f;
    public float girar = 200f;

    [Header("Salto")]
    public float fuerzaSalto = 5f;
    public LayerMask capaSuelo;

    [Header("Referencias")]
    public Animator anim;

    private Rigidbody rb;
    private bool saltando = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool enSuelo = EnSuelo();

        ManejarSalto(enSuelo);

        if (saltando) return;

        ManejarMovimiento();
    }

    private bool EnSuelo()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 1.5f, capaSuelo);
    }

    private void ManejarSalto(bool enSuelo)
    {
        if (saltando && enSuelo)
        {
            saltando = false;
            anim.SetInteger("states", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && enSuelo && !saltando)
        {
            saltando = true;
            anim.SetInteger("states", 3);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            return;
        }

        if (saltando && !enSuelo)
        {
            anim.SetInteger("states", 3);
            return;
        }
    }

    private void ManejarMovimiento()
    {
        bool adelante = Input.GetKey(KeyCode.W);
        bool atras = Input.GetKey(KeyCode.S);
        bool izquierda = Input.GetKey(KeyCode.A);
        bool derecha = Input.GetKey(KeyCode.D);
        bool shift = Input.GetKey(KeyCode.LeftShift);

        float move = adelante ? 1f : (atras ? -1f : 0f);
        float rotation = derecha ? 1f : (izquierda ? -1f : 0f);

        // MOVIMIENTO
        if (move != 0)
        {
            float vel = (shift && adelante) ? speedrun : speed;
            Vector3 dir = transform.forward * move * vel;
            transform.Translate(dir * Time.deltaTime, Space.World);
        }

        // ROTACIÓN
        if (rotation != 0)
            transform.Rotate(Vector3.up * rotation * girar * Time.deltaTime);

        // ANIMACIÓN
        int estado = move == 0 ? 0 : (shift && adelante ? 2 : 1);
        anim.SetInteger("states", estado);
    }
}