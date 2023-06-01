using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mision1Boss : MonoBehaviour
{
    [SerializeField] List<SpawnPuerta> puertas;
    [SerializeField] Mision1 mision;
    [SerializeField] GameObject recompensa;

    private void Start()
    {
        mision = GameObject.Find("Msion1Controlador").GetComponent<Mision1>();
    }

    private void Update()
    {
        if (mision != null) return;

        mision = GameObject.Find("Msion1Controlador").GetComponent<Mision1>();

    }

    private void OnDestroy()
    {
        foreach (SpawnPuerta spawn in puertas) {
            spawn.enabled = false; 
        }
        recompensa.SetActive(true);
        mision.TerminaMision();
    }
}
