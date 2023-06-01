using System;
using UnityEngine;

[Obsolete("Sustituida por Interaccion")]
public class RecogerItemSuelo : MonoBehaviour
{
    public CountNewItems newItem;
    public InventoryController inventory;

    private void Start()
    {
        inventory = GameObject.Find("Canvas").gameObject.GetComponent<InventoryController>();
        newItem = GameObject.Find("PanelNewItems").gameObject.GetComponent<CountNewItems>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            string tipo = other.gameObject.GetComponent<Renderer>().material.name;
            Destroy(other.gameObject);

            /*int equipo = Random.Range(0, 4);
            switch (equipo)
            {
                case 0:
                    newItem.addItem(tipo, "casco");
                    return;
                case 1:
                    newItem.addItem(tipo, "pecho");
                    return;
                case 2:
                    newItem.addItem(tipo, "pierna");
                    return;
                case 3:
                    newItem.addItem(tipo, "brazo");
                    return;
            }*/

        }

        if (other.tag == "save")
        {
            inventory.save();
        }
    }
}
