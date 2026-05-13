using UnityEngine;
using UnityEngine.UI;

public class BarraColor : MonoBehaviour
{
    [Header("Referencias")]
    public Slider slider;
    public Image fill;

    [Header("Animacion")]
    public bool animarPeligro = true;

    void Update()
    {
        // Evita errores
        if (slider == null || fill == null)
            return;

        // =========================
        // VERDE
        // =========================
        if (slider.value > 60)
        {
            fill.color = Color.green;

            fill.transform.localScale =
                Vector3.one;
        }

        // =========================
        // AMARILLO
        // =========================
        else if (slider.value > 30)
        {
            fill.color = Color.yellow;

            fill.transform.localScale =
                Vector3.one;
        }

        // =========================
        // ROJO
        // =========================
        else
        {
            fill.color = Color.red;

            // EFECTO DE PELIGRO
            if (animarPeligro)
            {
                float escala =
                    1f +
                    Mathf.Sin(Time.time * 8f)
                    * 0.05f;

                fill.transform.localScale =
                    new Vector3(
                        escala,
                        escala,
                        1f
                    );
            }
        }
    }
}   