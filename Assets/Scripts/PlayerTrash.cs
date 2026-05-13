using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerTrash : MonoBehaviour
{
    public static PlayerTrash instance;

    [Header("Inventario")]
    public List<Basura> inventario =
        new List<Basura>();

    public int capacidad = 5;

    [Header("UI")]
    public TextMeshProUGUI textoBasura;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ActualizarUI();
    }

    // TOMAR BASURA
    public bool TomarBasura(Basura basura)
    {
        if (basura == null)
            return false;

        // INVENTARIO LLENO
        if (inventario.Count >= capacidad)
        {
            Debug.Log("Inventario lleno");

            return false;
        }

        // EVITA DUPLICADOS
        if (inventario.Contains(basura))
        {
            return false;
        }

        inventario.Add(basura);

        Debug.Log(
            "Basura guardada: " +
            inventario.Count
        );

        ActualizarUI();

        return true;
    }

    // ELIMINAR BASURA ESPECÍFICA
    public void EliminarBasura(Basura basura)
    {
        if (inventario.Contains(basura))
        {
            inventario.Remove(basura);

            Debug.Log(
                "Basura eliminada: " +
                inventario.Count
            );

            ActualizarUI();
        }
    }

    // LIMPIAR TODO
    public void LimpiarInventario()
    {
        inventario.Clear();

        ActualizarUI();
    }

    // UI
    public void ActualizarUI()
    {
        if (textoBasura == null)
            return;

        textoBasura.text =
            "INVENTARIO: " +
            inventario.Count +
            "/" +
            capacidad;
    }
}