using UnityEngine;

public class InteraccionJugador : MonoBehaviour
{
    public float distancia = 4f;
    public LayerMask capaInteractuable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, distancia);

            foreach (Collider col in hits)
            {
                if (((1 << col.gameObject.layer) & capaInteractuable) != 0)
                {
                    IInteractuable obj = col.GetComponent<IInteractuable>();

                    if (obj != null)
                    {
                        obj.Interactuar();
                        return;
                    }
                }
            }
        }
    }
}