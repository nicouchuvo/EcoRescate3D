using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // =========================
    // UI GENERAL
    // =========================
    [Header("UI General")]

    public TMP_Text textoAmbiental;


    [Header("UI Zona 1")]

    public GameObject panelObjetivosBasura;

    public TMP_Text textoReciclables;
    public TMP_Text textoDesechos;


    [Header("UI Zona 2")]

    public GameObject panelObjetivosZona2;

    public TMP_Text textoFugas;
    public TMP_Text textoLuces;


    [Header("Panel Zona 2")]

    public GameObject panelZona2;


    [Header("Barra Ambiental")]

    public Slider barraAmbiental;

    public float valorActual = 50f;
    public float valorMax = 100f;



    [Header("Objetivos Basura")]

    public int reciclablesDepositados = 0;
    public int desechosDepositados = 0;

    public int objetivoReciclables = 10;
    public int objetivoDesechos = 10;


    [Header("Zona 2")]

    public int fugasReparadas = 0;
    public int fugasObjetivo = 5;

    public int lucesApagadas = 0;
    public int lucesObjetivo = 5;


    [Header("Puerta")]

    public GameObject rejaZona2;


    bool zona2Desbloqueada = false;

    bool juegoTerminado = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        ActualizarUI();


        if (panelZona2 != null)
        {
            panelZona2.SetActive(false);
        }


        if (panelObjetivosZona2 != null)
        {
            panelObjetivosZona2.SetActive(false);
        }


        if (panelObjetivosBasura != null)
        {
            panelObjetivosBasura.SetActive(true);
        }
    }

    void Update()
    {

        if (juegoTerminado)
            return;


        if (valorActual > 0)
        {
            valorActual -= Time.deltaTime * 0.7f;
        }

        valorActual =
            Mathf.Clamp(
                valorActual,
                0,
                valorMax
            );

        ActualizarUI();


        if (valorActual <= 1f)
        {
            juegoTerminado = true;

            Debug.Log("GAME OVER");


            if (AudioManager.instance != null)
            {
                AudioManager.instance
                    .ReproducirSonido(
                        AudioManager.instance
                            .gameOver
                    );
            }

            Time.timeScale = 1f;

            SceneManager.LoadScene(
                "GameOver"
            );
        }


        if (
            fugasReparadas >= fugasObjetivo
            &&
            lucesApagadas >= lucesObjetivo
        )
        {
            juegoTerminado = true;

            Debug.Log("YOU WIN");


            if (AudioManager.instance != null)
            {
                AudioManager.instance
                    .ReproducirSonido(
                        AudioManager.instance
                            .victoria
                    );
            }

            Time.timeScale = 1f;

            SceneManager.LoadScene(
                "YouWin"
            );
        }
    }


    public void SumarAmbiente(
        float cantidad
    )
    {
        valorActual += cantidad;

        valorActual =
            Mathf.Clamp(
                valorActual,
                0,
                valorMax
            );

        ActualizarUI();
    }


    public void RestarAmbiente(
        float cantidad
    )
    {
        valorActual -= cantidad;

        valorActual =
            Mathf.Clamp(
                valorActual,
                0,
                valorMax
            );

        ActualizarUI();


        if (valorActual <= 1f)
        {
            juegoTerminado = true;

            Debug.Log("GAME OVER");


            if (AudioManager.instance != null)
            {
                AudioManager.instance
                    .ReproducirSonido(
                        AudioManager.instance
                            .gameOver
                    );
            }

            Time.timeScale = 1f;

            SceneManager.LoadScene(
                "GameOver"
            );
        }
    }


    public void VerificarObjetivo()
    {
        if (
            reciclablesDepositados
            >=
            objetivoReciclables

            &&

            desechosDepositados
            >=
            objetivoDesechos
        )
        {
            if (zona2Desbloqueada)
                return;

            zona2Desbloqueada = true;

            Debug.Log(
                "Zona 2 desbloqueada"
            );


            if (AudioManager.instance != null)
            {
                AudioManager.instance
                    .ReproducirSonido(
                        AudioManager.instance
                            .victoria
                    );
            }


            if (rejaZona2 != null)
            {
                rejaZona2.SetActive(false);
            }


            if (panelObjetivosBasura != null)
            {
                panelObjetivosBasura.SetActive(false);
            }


            if (panelObjetivosZona2 != null)
            {
                panelObjetivosZona2.SetActive(true);
            }


            if (panelZona2 != null)
            {
                panelZona2.SetActive(true);

                Invoke(
                    "OcultarPanelZona2",
                    5f
                );
            }
        }
    }


    void OcultarPanelZona2()
    {
        if (panelZona2 != null)
        {
            panelZona2.SetActive(false);
        }
    }


    void ActualizarUI()
    {

        if (barraAmbiental != null)
        {
            barraAmbiental.value =
                valorActual;
        }


        if (textoAmbiental != null)
        {
            textoAmbiental.text =
                "🌍 AMBIENTE: "
                +
                Mathf.RoundToInt(
                    valorActual
                )
                +
                "%";
        }


        if (textoReciclables != null)
        {
            textoReciclables.text =
                "♻ RECICLABLES: "
                +
                reciclablesDepositados
                +
                " / "
                +
                objetivoReciclables;

            if (
                reciclablesDepositados
                >=
                objetivoReciclables
            )
            {
                textoReciclables.color =
                    Color.green;
            }
        }


        if (textoDesechos != null)
        {
            textoDesechos.text =
                "☣ DESECHOS: "
                +
                desechosDepositados
                +
                " / "
                +
                objetivoDesechos;

            if (
                desechosDepositados
                >=
                objetivoDesechos
            )
            {
                textoDesechos.color =
                    Color.green;
            }
        }


        if (textoFugas != null)
        {
            textoFugas.text =
                "💧 FUGAS: "
                +
                fugasReparadas
                +
                " / "
                +
                fugasObjetivo;

            if (
                fugasReparadas
                >=
                fugasObjetivo
            )
            {
                textoFugas.color =
                    Color.green;
            }
        }


        if (textoLuces != null)
        {
            textoLuces.text =
                "💡 ENERGÍA: "
                +
                lucesApagadas
                +
                " / "
                +
                lucesObjetivo;

            if (
                lucesApagadas
                >=
                lucesObjetivo
            )
            {
                textoLuces.color =
                    Color.green;
            }
        }
    }
}