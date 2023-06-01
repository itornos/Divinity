using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadActive : MonoBehaviour
{
    public void Ocultar() {
        gameObject.SetActive(false);
    }

    public void Mostrar() { 
        gameObject.SetActive(true);
    }
}
