using UnityEngine;

public class Basura : MonoBehaviour, IInteractuable
{
    [Header("Configuracion")]
    public bool esReciclable;

    [Header("Puntos ambientales")]
    public float puntosAmbientales = 5f;

    private bool recogida = false;

    public void Interactuar()
    {
        // EVITA RECOGER DOS VECES
        if (recogida)
            return;

        // INTENTAR GUARDAR EN INVENTARIO
        bool guardada =
            PlayerTrash.instance.TomarBasura(this);

        // SI NO PUDO GUARDARSE
        if (!guardada)
        {
            return;
        }

        recogida = true;

        Debug.Log("Recogiste basura");

        // SONIDO
        if (
            AudioManager.instance != null
            &&
            AudioManager.instance.recogerBasura != null
        )
        {
            AudioManager.instance
                .ReproducirSonido(
                    AudioManager.instance
                        .recogerBasura
                );
        }

        // OCULTAR RENDERERS
        Renderer[] renderers =
            GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }

        // DESACTIVAR COLLIDERS
        Collider[] colliders =
            GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            c.enabled = false;
        }
    }
}