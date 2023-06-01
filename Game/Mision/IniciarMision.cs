using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarMision : MonoBehaviour
{
    [SerializeField] GameObject mision1;
    [SerializeField] GameObject currenteMision;

    public void nuevaMision() {
        if (currenteMision == null)
        {
            currenteMision = Instantiate(mision1);
        }
        else {
            Destroy(GameObject.Find("Msion1Controlador"));
            Destroy(currenteMision);
            currenteMision = Instantiate(mision1);
        }

    }
}
