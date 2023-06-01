using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaHack : MonoBehaviour
{
    [SerializeField] Mision1 mision;
    [SerializeField] bool dentro;
    [SerializeField] int velocidad;
    [SerializeField] SphereCollider thisCollider;

    private void Start()
    {
        dentro = false;
        mision = GameObject.Find("Msion1Controlador").GetComponent<Mision1>();
    }

    private void Update()
    {
        if (mision == null) {
            mision = GameObject.Find("Msion1Controlador").GetComponent<Mision1>();
            return;
        }

        mision.Dentro = dentro;
        if (dentro && thisCollider.enabled)
        {
            mision.CurrentHack += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")dentro = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") dentro = false;
    }
}
