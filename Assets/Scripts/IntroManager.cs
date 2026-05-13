using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.Space)
        )
        {
            Debug.Log(
                "ESPACIO PRESIONADO"
            );

            SceneManager.LoadScene(
                "Juego"
            );
        }
    }
}