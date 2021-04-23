using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenrcnPlatfrm : MonoBehaviour
{
    public Transform PuntoDePartida;
    public Transform PuntoDeFin;

    public GameObject[] obstaculos; // Array que tiene los diferentes tipos de obstaculos para generarlos al azar
    
    public void ActivarObstAlAzar ()
    {
        DesactivarTodosLosObst();

        System.Random azar = new System.Random();
        int NumAlAzar = azar.Next(0, obstaculos.Length);
        obstaculos[NumAlAzar].SetActive(true);
    }

    public void DesactivarTodosLosObst ()
    {
        for (int i=0; i< obstaculos.Length; i++)
        {
            obstaculos[i].SetActive(false);
        }
    }
}
