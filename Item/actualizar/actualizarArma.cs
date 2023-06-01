using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class actualizarArma : MonoBehaviour
{
    public GameObject tipo;
    public GameObject fondo;
    public ListSpritesItems sprites;
    public ArmaController item;

    private void Start()
    {
        fondo = transform.parent.parent.gameObject;
        tipo = transform.parent.gameObject;
        item = fondo.GetComponent<ArmaController>();
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
                Debug.Log("Shader Encontrado");
                item.setMaterialPrincipal(currentItem);
            } else if (currentItem.GetHashCode() == item.getMaterialSecundarioHash()) {
                Debug.Log("Shader Encontrado");
                item.setMaterialSecundario(currentItem);
            }
        }

        if (item.getMaterialPrincipalHash() == 0)
        {
            Debug.Log("Shader Nuevo");
            item.setMaterialPrincipal(sprites.materiales[Random.Range(0, sprites.materiales.Count)]);
            item.setMaterialSecundario(sprites.materiales[Random.Range(0, sprites.materiales.Count)]);
            Debug.Log(item.getMaterialPrincipal());
            Debug.Log(item.getNombre());
        }
    }

    public string getNameFondo() {
        return fondo.name;
    }
}
