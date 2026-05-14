using UnityEngine;

public class EnemigoDrone : MonoBehaviour
{
    public float velocidad = 5f;
    public float daño = 5f;

    [Header("Objetivo")]
    public Transform jugador;

    void Update()
    {
        if (jugador == null)
            return;

        Vector3 objetivo = jugador.position;

        objetivo.y = transform.position.y;

        transform.LookAt(jugador);

        transform.position =
            Vector3.MoveTowards(
                transform.position,
                objetivo,
                velocidad * Time.deltaTime
            );
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance
                .RestarAmbiente(
                    daño * Time.deltaTime
                );
        }
    }
}