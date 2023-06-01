using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class CountNewItems : MonoBehaviour
{
    [SerializeField] InventoryController inventario;
    [SerializeField] List<GameObject> items;
    [SerializeField] int sizeListItems;

    [SerializeField] GameObject ArmaPrefab;
    [SerializeField] GameObject ArmaduraPrefab;

    [SerializeField] const float timeout = 0.5f;
    [SerializeField] float time;

    [SerializeField] AudioSource malo;
    [SerializeField] AudioSource bueno;
    [SerializeField] AudioSource super;


    private void Start()
    {
        items = new List<GameObject>();
        inventario = transform.parent.GetComponent<InventoryController>();
        time = timeout;
        sizeListItems = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (sizeListItems == 0) return;

        time -= Time.deltaTime;

        if (time > 0) return;

        updateItems();
        time = timeout;
    }

    public void addItem(string tipo, string nombreEquipo, string equipo) {
        ajusteLista();
        sizeListItems++;
        Debug.Log("Asignacion Valores Item " + tipo + "   " + nombreEquipo + "    " + equipo);
        
        if (equipo.StartsWith("Arma"))
        {
            GameObject instance = Instantiate(ArmaPrefab, gameObject.transform);
            instance.gameObject.GetComponent<ArmaController>().inicializa(tipo, nombreEquipo, equipo);

            items.Add(instance);
            inventario.addGameObjectPleaseArma(new Arma(nombreEquipo, equipo, tipo));
        }
        else {
            GameObject instance = Instantiate(ArmaduraPrefab, gameObject.transform);
            instance.gameObject.GetComponent<ArmaduraController>().inicializa(tipo, nombreEquipo, equipo);
            
            items.Add(instance);
            inventario.addGameObjectPleaseArmadura(new Armadura(nombreEquipo, equipo, tipo));
        }
       
    }

    public void ajusteLista() {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items.RemoveAt(i);
                for (int j = i + 1; j < sizeListItems; j++)
                {
                    items[j - 1] = items[j];
                }
                i--;
                continue;
            }
        }
    }

    void updateItems() {
        ajusteLista();
        sizeListItems--;

        for (int i = 0; i < items.Count; i++)
        {
            int pos = items[i].gameObject.GetComponent<Animator>().GetInteger("NuevaPos");
            if (pos == 0)
            {
                items[i].gameObject.GetComponent<Animator>().SetInteger("NuevaPos", 1);
                string tipo;
                try {
                    tipo = items[i].gameObject.GetComponent<ArmaduraController>().getItem().getTipo();
                }
                catch(Exception) {
                    tipo = items[i].gameObject.GetComponent<ArmaController>().getItem().getTipo();
                }
                
                if (tipo == "Legendario (Instance)")
                {
                    super.Play();
                }
                else if (tipo == "Unico (Instance)")
                {
                    bueno.Play();
                }
                else {
                    malo.Play();
                }
                break;
            }

            pos++;
            items[i].gameObject.GetComponent<Animator>().SetInteger("NuevaPos", pos);
        }
    }
}
