using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCam : MonoBehaviour
{
    public GameObject Jugador;
    public Vector3 offset;
    void Start()
    {
        offset = transform.position - Jugador.transform.position;
    }

    
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = Jugador.transform.position + offset;
    }
}
