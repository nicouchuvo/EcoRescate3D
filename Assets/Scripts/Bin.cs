using UnityEngine;

public class Bin : MonoBehaviour, IInteractuable
{
    public bool aceptaReciclaje;

    public void Interactuar()
    {
        if (PlayerTrash.instance.inventario.Count == 0)
        {
            Debug.Log("No tienes basura");
            return;
        }

        int correctos = 0;
        int incorrectos = 0;

        foreach (Basura b in PlayerTrash.instance.inventario)
        {
            if (b.esReciclable == aceptaReciclaje)
                correctos++;
            else
                incorrectos++;
        }

        int puntos = (correctos * 10) - (incorrectos * 5);

        GameManager.instancia.SumarPunto(puntos);

        Debug.Log("Correctos: " + correctos + " Incorrectos: " + incorrectos);

        PlayerTrash.instance.SoltarBasura();
    }
}