using UnityEngine;

public class Basura : MonoBehaviour, IInteractuable
{
    public bool esReciclable;

    public void Interactuar()
    {
        Debug.Log("Recogiste basura");

        PlayerTrash.instance.TomarBasura(this);

        gameObject.SetActive(false);
    }
}