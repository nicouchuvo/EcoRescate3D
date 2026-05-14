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
        // BUSCAR LUZ AUTOMATICAMENTE
        if (luz == null)
        {
            luz =
                GetComponentInChildren<Light>();
        }

        // ASEGURAR QUE INICIE ENCENDIDA
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

        // EVITAR REPETIR
        if (reparado)
            return;

        reparado = true;

        // APAGAR LUZ
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

        // SONIDO
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

        // MENSAJE EDUCATIVO
        if (
            MensajesEducativos.instance != null
        )
        {
            MensajesEducativos.instance
                .MensajeEnergia();
        }

        // SUMAR AMBIENTE
        GameManager.instance
            .SumarAmbiente(
                puntosAmbientales
            );

        // CONTADOR DE LUCES
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