using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverMenu : MonoBehaviour
{
    public void Volver()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            "MenuPrincipal"
        );
    }
}