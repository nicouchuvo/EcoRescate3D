
using UnityEngine;

public class Basura : MonoBehaviour, IInteractuable
{
    public string tipo;

    public void Interactuar()
    {
        Debug.Log("Recogiste: " + tipo);
        GameManager.instancia.SumarPunto();
        Destroy(gameObject);
    }
}