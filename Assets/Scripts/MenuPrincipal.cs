using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject panelControles;
    public GameObject panelCreditos;

    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void AbrirControles()
    {
        panelControles.SetActive(true);
    }

    public void CerrarControles()
    {
        panelControles.SetActive(false);
    }

    public void AbrirCreditos()
    {
        panelCreditos.SetActive(true);
    }

    public void CerrarCreditos()
    {
        panelCreditos.SetActive(false);
    }

    public void Salir()
    {
        Application.Quit();
    }
}