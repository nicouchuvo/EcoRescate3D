using UnityEngine;

public class Energia : MonoBehaviour, IInteractuable
{
    public bool reparado = false;
    public Light luz;

    void Start()
    {
        // 🔥 Si no asignaste la luz, la busca automáticamente
        if (luz == null)
        {
            luz = GetComponentInChildren<Light>();
        }

        // 🔥 Asegura que inicie encendida
        if (luz != null)
        {
            luz.enabled = true;
        }
    }

    public void Interactuar()
    {
        Debug.Log("INTERACTUANDO CON ENERGIA");

        if (reparado) return;

        reparado = true;

        // 🔥 Apaga la luz
        if (luz != null)
        {
            luz.enabled = false;
        }
        else
        {
            Debug.LogWarning("No se encontró la luz en el objeto");
        }

        GameManager.instancia.SumarPunto();
    }
}