using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelControles;
    public GameObject panelCreditos;

    // =========================
    // JUGAR
    // =========================
    public void Jugar()
    {
        SceneManager.LoadScene(
            "Intro"
        );
    }

    // =========================
    // CONTROLES
    // =========================
    public void AbrirControles()
    {
        if (panelControles != null)
        {
            panelControles.SetActive(true);
        }
    }

    public void CerrarControles()
    {
        if (panelControles != null)
        {
            panelControles.SetActive(false);
        }
    }

    // =========================
    // CREDITOS
    // =========================
    public void AbrirCreditos()
    {
        if (panelCreditos != null)
        {
            panelCreditos.SetActive(true);
        }
    }

    public void CerrarCreditos()
    {
        if (panelCreditos != null)
        {
            panelCreditos.SetActive(false);
        }
    }

    // =========================
    // SALIR
    // =========================
    public void Salir()
    {
        Application.Quit();
    }
}