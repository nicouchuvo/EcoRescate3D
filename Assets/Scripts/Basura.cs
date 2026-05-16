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

        if (recogida)
            return;


        bool guardada =
            PlayerTrash.instance.TomarBasura(this);


        if (!guardada)
        {
            return;
        }

        recogida = true;

        Debug.Log("Recogiste basura");


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


        Renderer[] renderers =
            GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }


        Collider[] colliders =
            GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            c.enabled = false;
        }
    }
}