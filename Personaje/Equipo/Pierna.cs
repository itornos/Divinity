using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierna : MonoBehaviour, Equipo
{
    public Renderer materiales;
    ArmaduraController itemEqupado;
    public Renderer detallesPierna;

    // Start is called before the first frame update
    void Start()
    {
        materiales =  GetComponent<Renderer>();
        detallesPierna = GameObject.Find("Detalles").GetComponent<Renderer>();
    }

    public void onChangeItem() {
            Material[] nuevosMaterialesPierna = new Material[materiales.materials.Length];
            nuevosMaterialesPierna[0] = materiales.materials[0];
            nuevosMaterialesPierna[1] = itemEqupado.getMaterialPrincipal();
            nuevosMaterialesPierna[2] = itemEqupado.getMaterialSecundario();
            materiales.materials = nuevosMaterialesPierna;

            Material[] nuevosMaterialesPiernaDetalles = new Material[detallesPierna.materials.Length];
            nuevosMaterialesPiernaDetalles[0] = itemEqupado.getMaterialPrincipal();
            nuevosMaterialesPiernaDetalles[1] = itemEqupado.getMaterialSecundario();
            detallesPierna.materials = nuevosMaterialesPiernaDetalles;

    }

    public bool inicializado()
    {
        Debug.Log(materiales.materials[1]);
        if (materiales.materials[1] != null)
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
