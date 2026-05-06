using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerTrash : MonoBehaviour
{
    public static PlayerTrash instance;

    public List<Basura> inventario = new List<Basura>();
    public int capacidad = 5;

    public TextMeshProUGUI textoBasura;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ActualizarUI();
    }

    // 🔥 TOMAR BASURA
    public void TomarBasura(Basura basura)
    {
        if (basura == null) return;

        if (inventario.Count >= capacidad)
        {
            Debug.Log("Inventario lleno");
            return;
        }

        if (inventario.Contains(basura))
        {
            Debug.Log("Esa basura ya está en el inventario");
            return;
        }

        inventario.Add(basura);

        Debug.Log("Recogiste basura. Total: " + inventario.Count);

        ActualizarUI(); // 🔥 actualizar aquí
    }

    // 🔥 SOLTAR TODA LA BASURA
    public void SoltarBasura()
    {
        if (inventario.Count == 0)
        {
            Debug.Log("No tienes basura");
            return;
        }

        inventario.Clear();

        Debug.Log("Vaciaste el inventario");

        ActualizarUI(); // 🔥 actualizar aquí
    }

    // 🔥 CONSULTA
    public bool TieneBasura()
    {
        return inventario.Count > 0;
    }

    // 🔥 UI
    void ActualizarUI()
    {
        if (textoBasura == null) return;

        textoBasura.text = "Llevas: " + inventario.Count + "/" + capacidad;

        if (inventario.Count > 0)
        {
            textoBasura.text += "\nVe a una caneca";
        }
    }
}