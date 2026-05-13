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

            // Raycast SOLO a objetos interactuables
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

                // Buscar interfaz
                IInteractuable obj =
                    hit.collider
                    .GetComponent<IInteractuable>();

                // Buscar en padre si no existe
                if (obj == null)
                {
                    obj =
                        hit.collider
                        .GetComponentInParent<IInteractuable>();
                }

                // Interactuar
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

    // Gizmo para debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            transform.position,
            distancia
        );
    }
}