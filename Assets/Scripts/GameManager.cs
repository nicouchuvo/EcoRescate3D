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

    // =========================
    // UI ZONA 1
    // =========================
    [Header("UI Zona 1")]

    public GameObject panelObjetivosBasura;

    public TMP_Text textoReciclables;
    public TMP_Text textoDesechos;

    // =========================
    // UI ZONA 2
    // =========================
    [Header("UI Zona 2")]

    public GameObject panelObjetivosZona2;

    public TMP_Text textoFugas;
    public TMP_Text textoLuces;

    // =========================
    // PANEL DESBLOQUEO
    // =========================
    [Header("Panel Zona 2")]

    public GameObject panelZona2;

    // =========================
    // BARRA AMBIENTAL
    // =========================
    [Header("Barra Ambiental")]

    public Slider barraAmbiental;

    public float valorActual = 50f;
    public float valorMax = 100f;

    // =========================
    // OBJETIVOS ZONA 1
    // =========================
    [Header("Objetivos Basura")]

    public int reciclablesDepositados = 0;
    public int desechosDepositados = 0;

    public int objetivoReciclables = 10;
    public int objetivoDesechos = 10;

    // =========================
    // OBJETIVOS ZONA 2
    // =========================
    [Header("Zona 2")]

    public int fugasReparadas = 0;
    public int fugasObjetivo = 5;

    public int lucesApagadas = 0;
    public int lucesObjetivo = 5;

    // =========================
    // REJA
    // =========================
    [Header("Puerta")]

    public GameObject rejaZona2;

    // =========================
    // CONTROL
    // =========================
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

        // PANEL ZONA 2 OCULTO
        if (panelZona2 != null)
        {
            panelZona2.SetActive(false);
        }

        // UI ZONA 2 OCULTA
        if (panelObjetivosZona2 != null)
        {
            panelObjetivosZona2.SetActive(false);
        }

        // UI ZONA 1 ACTIVA
        if (panelObjetivosBasura != null)
        {
            panelObjetivosBasura.SetActive(true);
        }
    }

    void Update()
    {
        // EVITAR ERRORES
        if (juegoTerminado)
            return;

        // =========================
        // EL AMBIENTE BAJA SOLO
        // =========================
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

        // =========================
        // GAME OVER
        // =========================
        if (valorActual <= 1f)
        {
            juegoTerminado = true;

            Debug.Log("GAME OVER");

            // SONIDO
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

        // =========================
        // YOU WIN
        // =========================
        if (
            fugasReparadas >= fugasObjetivo
            &&
            lucesApagadas >= lucesObjetivo
        )
        {
            juegoTerminado = true;

            Debug.Log("YOU WIN");

            // SONIDO
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

    // =========================
    // SUMAR AMBIENTE
    // =========================
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

    // =========================
    // RESTAR AMBIENTE
    // =========================
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

        // GAME OVER
        if (valorActual <= 1f)
        {
            juegoTerminado = true;

            Debug.Log("GAME OVER");

            // SONIDO
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

    // =========================
    // DESBLOQUEAR ZONA 2
    // =========================
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

            // SONIDO
            if (AudioManager.instance != null)
            {
                AudioManager.instance
                    .ReproducirSonido(
                        AudioManager.instance
                            .victoria
                    );
            }

            // ABRIR REJA
            if (rejaZona2 != null)
            {
                rejaZona2.SetActive(false);
            }

            // OCULTAR OBJETIVOS BASURA
            if (panelObjetivosBasura != null)
            {
                panelObjetivosBasura.SetActive(false);
            }

            // MOSTRAR OBJETIVOS ZONA 2
            if (panelObjetivosZona2 != null)
            {
                panelObjetivosZona2.SetActive(true);
            }

            // PANEL INFORMATIVO
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

    // =========================
    // OCULTAR PANEL
    // =========================
    void OcultarPanelZona2()
    {
        if (panelZona2 != null)
        {
            panelZona2.SetActive(false);
        }
    }

    // =========================
    // ACTUALIZAR UI
    // =========================
    void ActualizarUI()
    {
        // BARRA
        if (barraAmbiental != null)
        {
            barraAmbiental.value =
                valorActual;
        }

        // TEXTO AMBIENTAL
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

        // RECICLABLES
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

        // DESECHOS
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

        // FUGAS
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

        // LUCES
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