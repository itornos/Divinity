using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mueltoCaidaMision : MonoBehaviour
{
    [SerializeField] Mision1 mision;
    [SerializeField] Transform moverPj;

    private void Start()
    {
        mision = GameObject.Find("Msion1Controlador").GetComponent<Mision1>();
    }

    private void Update()
    {
        if (mision != null) return;

        mision = GameObject.Find("Msion1Controlador").GetComponent<Mision1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = moverPj.position;
            Destroy(GameObject.Find("Mision1_Bunker_Zona1(Clone)"));
            mision.volverPuntoAnterior(3);
        }
    }
}
