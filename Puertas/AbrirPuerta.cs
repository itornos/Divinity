using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AbrirPuerta : MonoBehaviour
{

    [SerializeField] GameObject puerta1, puerta2, puerta3, puerta4;
    [SerializeField] Vector3 original1, original2, original3, original4;
    [SerializeField] Transform target1, target2, target3, target4;
    [SerializeField] bool abrirPuerta;
    [SerializeField] const float velocidad = 10;
    [SerializeField] float currentVelocidad;

    private void Start()
    {
        original1 = new Vector3(puerta1.transform.position.x, puerta1.transform.position.y, puerta1.transform.position.z);
        original2 = new Vector3(puerta2.transform.position.x, puerta2.transform.position.y, puerta2.transform.position.z);
        original3 = new Vector3(puerta3.transform.position.x, puerta3.transform.position.y, puerta3.transform.position.z);
        original4 = new Vector3(puerta4.transform.position.x, puerta4.transform.position.y, puerta4.transform.position.z);
    }

    private void Update()
    {
        

        if (abrirPuerta) {
            currentVelocidad = Time.deltaTime * velocidad;
            puerta1.transform.position = Vector3.Lerp(puerta1.transform.position, target1.position, currentVelocidad);
            puerta2.transform.position = Vector3.Lerp(puerta2.transform.position, target2.position, currentVelocidad);
            puerta3.transform.position = Vector3.Lerp(puerta3.transform.position, target3.position, currentVelocidad);
            puerta4.transform.position = Vector3.Lerp(puerta4.transform.position, target4.position, currentVelocidad);
        } else if ( puerta1.transform.position != original1 || 
                    puerta2.transform.position != original2 || 
                    puerta3.transform.position != original3 || 
                    puerta4.transform.position != original4) {
            currentVelocidad = Time.deltaTime * velocidad;
            puerta1.transform.position = Vector3.Lerp(puerta1.transform.position, original1, currentVelocidad);
            puerta2.transform.position = Vector3.Lerp(puerta2.transform.position, original2, currentVelocidad);
            puerta3.transform.position = Vector3.Lerp(puerta3.transform.position, original3, currentVelocidad);
            puerta4.transform.position = Vector3.Lerp(puerta4.transform.position, original4, currentVelocidad);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        abrirPuerta = true;
    }

    private void OnTriggerExit(Collider other)
    {
        abrirPuerta = false;
    }
}
