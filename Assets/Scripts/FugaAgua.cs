using UnityEngine;

public class FugaAgua : MonoBehaviour, IInteractuable
{
    public bool reparada = false;
    public GameObject aguaVisual;

    void Start()
    {
        // Asegura que el agua esté activa al iniciar
        if (aguaVisual != null)
            aguaVisual.SetActive(true);
    }

    public void Interactuar()
    {
        if (reparada) return;

        reparada = true;

        // Apaga el agua cuando la arreglas
        if (aguaVisual != null)
            aguaVisual.SetActive(false);

        GameManager.instancia.SumarPunto();

        Debug.Log("Fuga reparada");
    }
}