using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casco : MonoBehaviour, Equipo
{
    public Renderer materiales;
    public ArmaduraController itemEqupado;

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
        if (materiales.materials[0] != null)
        {
            return true;
        }

        return false;
    }

    public void setItem(ArmaduraController item)
    {
        itemEqupado = item;
    }
}
