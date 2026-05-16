using UnityEngine;
using TMPro;

public class MensajesEducativos : MonoBehaviour
{
    public static MensajesEducativos instance;

    public GameObject panel;
    public TMP_Text texto;

    [Header("Mensajes")]
    [TextArea]
    public string[] mensajesReciclaje;

    [TextArea]
    public string[] mensajesError;

    [TextArea]
    public string[] mensajesAgua;

    [TextArea]
    public string[] mensajesEnergia;

    void Awake()
    {
        instance = this;
    }


    public void MostrarMensaje(
        string mensaje
    )
    {
        panel.SetActive(true);

        texto.text = mensaje;

        CancelInvoke();

        Invoke(
            "Ocultar",
            4f
        );
    }

    void Ocultar()
    {
        panel.SetActive(false);
    }


    public void MensajeReciclaje()
    {
        MostrarMensaje(
            mensajesReciclaje[
                Random.Range(
                    0,
                    mensajesReciclaje.Length
                )
            ]
        );
    }


    public void MensajeError()
    {
        MostrarMensaje(
            mensajesError[
                Random.Range(
                    0,
                    mensajesError.Length
                )
            ]
        );
    }


    public void MensajeAgua()
    {
        MostrarMensaje(
            mensajesAgua[
                Random.Range(
                    0,
                    mensajesAgua.Length
                )
            ]
        );
    }


    public void MensajeEnergia()
    {
        MostrarMensaje(
            mensajesEnergia[
                Random.Range(
                    0,
                    mensajesEnergia.Length
                )
            ]
        );
    }
}