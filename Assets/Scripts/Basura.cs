using UnityEngine;

public class Basura : MonoBehaviour, IInteractuable
{
    public void Interactuar()
    {
        Debug.Log("Recogiendo basura");

        GameManager.instancia.SumarPunto(10f);

        Destroy(gameObject);
    }
}