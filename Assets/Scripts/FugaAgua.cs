using UnityEngine;

public class FugaAgua : MonoBehaviour, IInteractuable
{
    [Header("Estado")]
    public bool reparada = false;

    [Header("Visual del agua")]
    public GameObject aguaVisual;

    [Header("Puntos ambientales")]
    public float puntosAmbientales = 15f;

    void Start()
    {
        // ASEGURAR QUE EL AGUA INICIE ACTIVA
        if (aguaVisual != null)
        {
            aguaVisual.SetActive(true);
        }
    }

    public void Interactuar()
    {
        // EVITAR REPETIR
        if (reparada)
            return;

        reparada = true;

        // APAGAR AGUA
        if (aguaVisual != null)
        {
            aguaVisual.SetActive(false);
        }

        // SONIDO
        if (
            AudioManager.instance != null
            &&
            AudioManager.instance.repararAgua != null
        )
        {
            AudioManager.instance
                .ReproducirSonido(
                    AudioManager.instance
                        .repararAgua
                );
        }

        // SUMAR AMBIENTE
        GameManager.instance
            .SumarAmbiente(
                puntosAmbientales
            );

        // CONTADOR DE FUGAS
        GameManager.instance
            .fugasReparadas++;

        Debug.Log(
            "Fugas reparadas: "
            +
            GameManager.instance
                .fugasReparadas
        );
    }
}