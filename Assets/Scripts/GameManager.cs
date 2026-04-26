using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Puntos")]
    public int puntos = 0;
    public Text textoPuntos;

    [Header("Barra Ambiental")]
    public Slider barraAmbiental;
    public float valorActual = 0f;
    public float valorMax = 100f;

    [Header("Victoria")]
    public GameObject mensajeVictoria;

    void Awake()
    {
        instancia = this;
    }

    public void SumarPunto(float cantidad)
    {
        puntos++;
        textoPuntos.text = "Puntos: " + puntos;

        valorActual += cantidad;
        barraAmbiental.value = valorActual;

        if (valorActual >= valorMax)
        {
            valorActual = valorMax;
            mensajeVictoria.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}