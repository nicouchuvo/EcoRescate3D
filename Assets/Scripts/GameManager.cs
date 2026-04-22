using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public int puntos = 0;
    public TMP_Text textoPuntos;

    void Awake()
    {
        instancia = this;
    }

    public void SumarPunto()
    {
        puntos++;
        textoPuntos.text = "Puntos: " + puntos;
    }
}