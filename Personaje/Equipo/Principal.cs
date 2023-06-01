using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Principal : MonoBehaviour, Equipo
{
    public Renderer parteUno;
    ArmaController itemEqupado;
    public Renderer parteDos;

    // Start is called before the first frame update
    void Start()
    {
        try {
            parteUno = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>();
            parteDos = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Renderer>();
        }
        catch(System.Exception) {
            parteUno = transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
            parteDos = transform.GetChild(0).GetChild(1).GetComponent<Renderer>();
        }
        
    }

    public void onChangeItem()
    {
        Material[] nuevoParte = new Material[parteUno.materials.Length];
        for (int i = 0; i< nuevoParte.Length; i++) {
            nuevoParte[i] = itemEqupado.getMaterialPrincipal();
        }
        parteUno.materials = nuevoParte;

        nuevoParte = new Material[parteDos.materials.Length];
        for (int i = 0; i < nuevoParte.Length; i++)
        {
            nuevoParte[i] = itemEqupado.getMaterialSecundario();
        }
        parteDos.materials = nuevoParte;

    }

    public bool inicializado()
    {
        Debug.Log(parteUno.materials[1]);
        if (parteUno.materials[1] != null)
        {
            return true;
        }

        return false;
    }

    public void setItem(ArmaController item)
    {
        itemEqupado = item;
    }
}
