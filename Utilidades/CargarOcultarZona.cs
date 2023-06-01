using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarOcultarZona : MonoBehaviour
{
    [SerializeField] GameObject zona;
    [SerializeField] bool cargar;
    [SerializeField] bool ocultar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (cargar) {
                zona.SetActive(true);
            } else if (ocultar) {
                zona.SetActive(false);
            }
        }
    }
}
