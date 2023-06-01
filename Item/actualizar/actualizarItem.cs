using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class actualizarItem : MonoBehaviour
{
    public GameObject tipo;
    public GameObject fondo;
    public ListSpritesItems sprites;
    public ItemController item;

    private void Start()
    {
        fondo = transform.parent.parent.gameObject;
        tipo = transform.parent.gameObject;
        item = fondo.GetComponent<ItemController>();
        sprites = GameObject.Find("ListSpritesItems").GetComponent<ListSpritesItems>();
        setSkinItem();
        inicializarEstiloItem();
    }

    public void setSkinItem() {
        string nombreEquipo = item.getNombre();
        string tipoCalidad = item.getTipo();


        foreach (Sprite currentItem in sprites.listaTipos)
        {

            if ((currentItem.name + " (Instance)") == tipoCalidad)
            {
                tipo.gameObject.GetComponent<Image>().sprite = currentItem;
                tipo.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
        }

        foreach (Sprite currentItem in sprites.listaEquipos)
        {
            Debug.Log(currentItem.name + "    " + nombreEquipo);
            if (currentItem.name == nombreEquipo)
            {
                gameObject.GetComponent<Image>().sprite = currentItem;
                gameObject.GetComponent<Image>().color = new Color(255,255,255,255);
                fondo.gameObject.GetComponent<Image>().sprite = sprites.fondos[1];
                return;
            }
        }

        tipo.gameObject.GetComponent<Image>().sprite = null;
        gameObject.GetComponent<Image>().sprite = null;
        fondo.gameObject.GetComponent<Image>().sprite = sprites.fondos[0];
    }

    public void inicializarEstiloItem()
    {
        
        foreach (Material currentItem in sprites.materiales) {
            if (currentItem.GetHashCode() == item.getMaterialPrincipalHash()) {
                item.setMaterialPrincipal(currentItem);
            } else if (currentItem.GetHashCode() == item.getMaterialSecundarioHash()) {
                item.setMaterialSecundario(currentItem);
            }
        }

        if (item.getMaterialPrincipalHash() == 0)
        {
            item.setMaterialPrincipal(sprites.materiales[Random.Range(0, sprites.materiales.Count)]);
            item.setMaterialSecundario(sprites.materiales[Random.Range(0, sprites.materiales.Count)]);
        }
    }

    public string getNameFondo() {
        return fondo.name;
    }

    public void nuevoJugador()
    {
        fondo = transform.parent.parent.gameObject;
        tipo = transform.parent.gameObject;
        item = fondo.GetComponent<ItemController>();
        item.setMaterialPrincipal(sprites.materiales[12]);
        item.setMaterialSecundario(sprites.materiales[12]);
    }
}
