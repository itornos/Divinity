using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaNuevaFaseMision : MonoBehaviour
{
    [SerializeField] Mision1 mision;

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
        mision.CompleteFase();
        Destroy(gameObject);
    }
}
