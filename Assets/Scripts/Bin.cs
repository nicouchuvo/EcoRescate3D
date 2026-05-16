using UnityEngine;
using System.Collections.Generic;

public class Bin : MonoBehaviour, IInteractuable
{
    [Header("Configuracion")]
    public bool aceptaReciclaje;

    [Header("Puntos")]
    public float puntosCorrectos = 10f;
    public float castigoIncorrecto = 5f;

    public void Interactuar()
    {

        if (PlayerTrash.instance.inventario.Count <= 0)
        {
            Debug.Log("Inventario vacio");
            return;
        }


        List<Basura> basuraNueva =
            new List<Basura>();


        foreach (Basura b in PlayerTrash.instance.inventario)
        {

            if (b == null)
                continue;


            if (b.esReciclable == aceptaReciclaje)
            {

                GameManager.instance
                    .SumarAmbiente(
                        puntosCorrectos
                    );


                if (b.esReciclable)
                {
                    GameManager.instance
                        .reciclablesDepositados++;
                }
                else
                {
                    GameManager.instance
                        .desechosDepositados++;
                }


                GameManager.instance
                    .VerificarObjetivo();

                Debug.Log(
                    "Basura depositada correctamente"
                );


                if (
                    MensajesEducativos.instance != null
                )
                {
                    MensajesEducativos.instance
                        .MensajeReciclaje();
                }


                if (
                    AudioManager.instance != null
                    &&
                    AudioManager.instance.depositarBasura != null
                )
                {
                    AudioManager.instance
                        .ReproducirSonido(
                            AudioManager.instance
                                .depositarBasura
                        );
                }


                Destroy(
                    b.gameObject
                );
            }


            else
            {

                GameManager.instance
                    .RestarAmbiente(
                        castigoIncorrecto
                    );

                Debug.Log(
                    "Basura incorrecta"
                );


                if (
                    MensajesEducativos.instance != null
                )
                {
                    MensajesEducativos.instance
                        .MensajeError();
                }


                basuraNueva.Add(b);
            }
        }


        PlayerTrash.instance
            .inventario
            .Clear();


        foreach (Basura b in basuraNueva)
        {
            PlayerTrash.instance
                .inventario
                .Add(b);
        }


        PlayerTrash.instance
            .ActualizarUI();


        Debug.Log(
            "Inventario actual: "
            +
            PlayerTrash.instance
                .inventario.Count
        );
    }
}