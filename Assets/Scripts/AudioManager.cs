using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Basura")]
    public AudioClip recogerBasura;
    public AudioClip depositarBasura;

    [Header("Zona Agua")]
    public AudioClip repararAgua;

    [Header("Zona Energia")]
    public AudioClip apagarEnergia;

    [Header("Finales")]
    public AudioClip victoria;
    public AudioClip gameOver;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // =========================
    // REPRODUCIR SONIDO
    // =========================
    public void ReproducirSonido(
        AudioClip clip
    )
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}