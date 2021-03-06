using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ControlJgdr : MonoBehaviour
{
    public float gravedad = 20.0f;
    public float AlturaDeSalto = 2f;

    Rigidbody r;
    bool TocandoElPiso = false;
    Vector3 defaultScale;
    bool Agacharse = false;     

    void Start()
    {
        r = GetComponent<Rigidbody>();
        r.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        r.freezeRotation = true;
        r.useGravity = false;

        defaultScale = transform.localScale;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && TocandoElPiso)
        {
            r.velocity = new Vector3(r.velocity.x, CalcularVelSaltoVert(), r.velocity.z);            
        }

        Agacharse = Input.GetKey(KeyCode.S);
        if (Agacharse)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(defaultScale.x, defaultScale.y * 0.4f, defaultScale.z), Time.deltaTime * 7);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            //defaultScale = transform.localScale; // ACA QUERIA HACER QUE LA BOLA RETOMARA SU APARIENCIA REDONDEADA, PERO NO FUNCIONABA
            
        }
        
    }

    void FixedUpdate()
    {
        r.AddForce(new Vector3(0, -gravedad * r.mass, 0));

        TocandoElPiso = false;
    }

    void OnCollisionStay()
    {
        TocandoElPiso = true;
    }

    float CalcularVelSaltoVert()
    {
        return Mathf.Sqrt(2 * AlturaDeSalto * gravedad);
    }

    void OnCollisionEnter(Collision collision)
    {                
        if (collision.gameObject.CompareTag("Obstaculo"))
        {                      
            //CrearTerreno.Texto // NO SE POR QUÉ NO DETECTA EL ATRIBUTO DEL SCRIPT "CrearTerreno"
                        
            Time.timeScale = 0.0f;
            //OnGUI();                   
        }
    }
    /*void OnGUI()
    {
        
        GUI.color = Color.red;
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Perdiste!");
    }*/
}
