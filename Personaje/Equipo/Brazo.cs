using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazo : MonoBehaviour, Equipo
{
    public Renderer materiales;
    ArmaduraController itemEqupado;

    // Start is called before the first frame update
    void Start()
    {
        materiales = gameObject.GetComponent<Renderer>();
    }

    public void onChangeItem()
    {
            Material[] nuevosMateriales = new Material[materiales.materials.Length];
            nuevosMateriales[0] = itemEqupado.getMaterialPrincipal();
            nuevosMateriales[1] = itemEqupado.getMaterialSecundario();
            materiales.materials = nuevosMateriales;

    }

    public bool inicializado()
    {
        Debug.Log(materiales.materials[0]);
        if (materiales.materials[0] != null) {
            return true;
        }

        return false;
    }

    public void setItem(ArmaduraController item)
    {
        itemEqupado = item;
    }
}
