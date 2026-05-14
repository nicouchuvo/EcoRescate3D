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
        // INVENTARIO VACIO
        if (PlayerTrash.instance.inventario.Count <= 0)
        {
            Debug.Log("Inventario vacio");
            return;
        }

        // NUEVA LISTA
        List<Basura> basuraNueva =
            new List<Basura>();

        // RECORRER INVENTARIO
        foreach (Basura b in PlayerTrash.instance.inventario)
        {
            // SEGURIDAD
            if (b == null)
                continue;

            // BASURA CORRECTA
            if (b.esReciclable == aceptaReciclaje)
            {
                // SUMAR AMBIENTE
                GameManager.instance
                    .SumarAmbiente(
                        puntosCorrectos
                    );

                // CONTADORES
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

                // VERIFICAR OBJETIVO
                GameManager.instance
                    .VerificarObjetivo();

                Debug.Log(
                    "Basura depositada correctamente"
                );

                // MENSAJE EDUCATIVO
                if (
                    MensajesEducativos.instance != null
                )
                {
                    MensajesEducativos.instance
                        .MensajeReciclaje();
                }

                // SONIDO
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

                // ELIMINAR BASURA
                Destroy(
                    b.gameObject
                );
            }

            // BASURA INCORRECTA
            else
            {
                // RESTAR AMBIENTE
                GameManager.instance
                    .RestarAmbiente(
                        castigoIncorrecto
                    );

                Debug.Log(
                    "Basura incorrecta"
                );

                // MENSAJE EDUCATIVO
                if (
                    MensajesEducativos.instance != null
                )
                {
                    MensajesEducativos.instance
                        .MensajeError();
                }

                // CONSERVAR EN INVENTARIO
                basuraNueva.Add(b);
            }
        }

        // LIMPIAR INVENTARIO
        PlayerTrash.instance
            .inventario
            .Clear();

        // DEVOLVER SOLO BASURA INCORRECTA
        foreach (Basura b in basuraNueva)
        {
            PlayerTrash.instance
                .inventario
                .Add(b);
        }

        // ACTUALIZAR UI
        PlayerTrash.instance
            .ActualizarUI();

        // DEBUG FINAL
        Debug.Log(
            "Inventario actual: "
            +
            PlayerTrash.instance
                .inventario.Count
        );
    }
}