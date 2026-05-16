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

        if (slider == null || fill == null)
            return;


        if (slider.value > 60)
        {
            fill.color = Color.green;

            fill.transform.localScale =
                Vector3.one;
        }


        else if (slider.value > 30)
        {
            fill.color = Color.yellow;

            fill.transform.localScale =
                Vector3.one;
        }


        else
        {
            fill.color = Color.red;


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