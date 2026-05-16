using UnityEngine;

public class Energia : MonoBehaviour, IInteractuable
{
    [Header("Estado")]
    public bool reparado = false;

    [Header("Luz del sistema")]
    public Light luz;

    [Header("Puntos ambientales")]
    public float puntosAmbientales = 20f;

    void Start()
    {

        if (luz == null)
        {
            luz =
                GetComponentInChildren<Light>();
        }


        if (luz != null)
        {
            luz.enabled = true;
        }
    }

    public void Interactuar()
    {
        Debug.Log(
            "INTERACTUANDO CON ENERGIA"
        );


        if (reparado)
            return;

        reparado = true;


        if (luz != null)
        {
            luz.enabled = false;
        }
        else
        {
            Debug.LogWarning(
                "No se encontro la luz"
            );
        }


        if (
            AudioManager.instance != null
            &&
            AudioManager.instance.apagarEnergia != null
        )
        {
            AudioManager.instance
                .ReproducirSonido(
                    AudioManager.instance
                        .apagarEnergia
                );
        }


        if (
            MensajesEducativos.instance != null
        )
        {
            MensajesEducativos.instance
                .MensajeEnergia();
        }


        GameManager.instance
            .SumarAmbiente(
                puntosAmbientales
            );


        GameManager.instance
            .lucesApagadas++;

        Debug.Log(
            "Luces reparadas: "
            +
            GameManager.instance
                .lucesApagadas
        );
    }
}