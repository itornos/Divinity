using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZonaSeguras : MonoBehaviour
{

    public Texture2D cursor;
    GameObject pj;
    Casco casco;
    Pecho pecho;
    Brazo brazo;
    Pierna pierna;
    Principal principal;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursor, new Vector2(0, 0), CursorMode.Auto);
    }

    void Update()
    {
        GameObject canvas = GameObject.Find("Canvas");
        canvas.GetComponent<KeyController>().InvetarioBool = true;
        canvas.GetComponent<KeyController>().mostrarIventario();
        
        pj = GameObject.FindGameObjectWithTag("Player");
        casco = pj.transform.GetChild(0).gameObject.GetComponent<Casco>();
        pecho = pj.transform.GetChild(1).gameObject.GetComponent<Pecho>();
        brazo = pj.transform.GetChild(2).gameObject.GetComponent<Brazo>();
        pierna = pj.transform.GetChild(4).gameObject.GetComponent<Pierna>();
        principal = GameObject.Find("Arma").GetComponent<Principal>();

        canvas.GetComponent<InventoryController>().getModelo();
        if (casco.materiales.materials[0].name == "Default (Instance)" ||
            pecho.materiales.materials[0].name == "Default (Instance)" ||
            pierna.materiales.materials[0].name == "Default (Instance)" ||
            brazo.materiales.materials[0].name == "Default (Instance)" ||
            principal.parteUno.materials[0].name == "Default (Instance)" ||
            casco.materiales.materials[0].name == "Lit (Instance)" ||
            pecho.materiales.materials[0].name == "Lit (Instance)" ||
            pierna.materiales.materials[0].name == "Lit (Instance)" ||
            brazo.materiales.materials[0].name == "Lit (Instance)" ||
            principal.parteUno.materials[0].name == "Lit (Instance)" ||
            casco.materiales.materials[0].name == "Lit (Instance)" ||
            pecho.materiales.materials[0].name == "Lit (Instance)" ||
            pierna.materiales.materials[0].name == "Lit (Instance)" ||
            brazo.materiales.materials[0].name == "Lit (Instance)" ||
            principal.parteUno.materials[0].name == "Lit (Instance)")
        {
            casco.onChangeItem();
            pecho.onChangeItem();
            pierna.onChangeItem();
            brazo.onChangeItem();
            principal.onChangeItem();
        }
        else { 
            Destroy( gameObject );
        }
    }
}
