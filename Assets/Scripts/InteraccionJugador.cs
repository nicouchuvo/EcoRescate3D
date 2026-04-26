using UnityEngine;

public class InteraccionJugador : MonoBehaviour
{
    [Header("Interacción")]
    public float distancia = 5f; // 🔥 un poco más amplio
    public LayerMask capaInteractuable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 🔥 Detecta todo alrededor del jugador
            Collider[] hits = Physics.OverlapSphere(transform.position + Vector3.up, distancia);

            foreach (Collider col in hits)
            {
                // 🔥 Verifica que esté en la capa correcta
                if (((1 << col.gameObject.layer) & capaInteractuable) != 0)
                {
                    // 🔥 Busca el script en el objeto o en el padre
                    IInteractuable obj = col.GetComponent<IInteractuable>();

                    if (obj == null)
                        obj = col.GetComponentInParent<IInteractuable>();

                    if (obj != null)
                    {
                        Debug.Log("Interactuando con: " + col.name);

                        obj.Interactuar();
                        return;
                    }
                }
            }
        }
    }

    // 🔥 Solo para debug (ver el área de interacción)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, distancia);
    }
}