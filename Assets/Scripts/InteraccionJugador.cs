using UnityEngine;

public class InteraccionJugador : MonoBehaviour
{
    [Header("Interaccion")]
    public float distancia = 6f;

    [Header("Camara")]
    public Camera camaraJugador;

    [Header("Layers")]
    public LayerMask capaInteractuable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray =
                camaraJugador.ScreenPointToRay(
                    new Vector3(
                        Screen.width / 2,
                        Screen.height / 2
                    )
                );

            RaycastHit hit;


            if (
                Physics.Raycast(
                    ray,
                    out hit,
                    distancia,
                    capaInteractuable
                )
            )
            {
                Debug.Log(
                    "Golpeando: " +
                    hit.collider.name
                );

                Debug.Log(
                    "Layer detectada: " +
                    LayerMask.LayerToName(
                        hit.collider.gameObject.layer
                    )
                );


                IInteractuable obj =
                    hit.collider
                    .GetComponent<IInteractuable>();


                if (obj == null)
                {
                    obj =
                        hit.collider
                        .GetComponentInParent<IInteractuable>();
                }


                if (obj != null)
                {
                    Debug.Log(
                        "Interactuando con: " +
                        hit.collider.name
                    );

                    obj.Interactuar();
                }
                else
                {
                    Debug.LogWarning(
                        "El objeto no tiene IInteractuable"
                    );
                }
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            transform.position,
            distancia
        );
    }
}