using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//CLASE LOAD MUNDO
public class Mundo : MonoBehaviour
{
    [SerializeField] Texture2D cursor;
    [SerializeField] BoxCollider bunker;
    [SerializeField] GameObject pj;
    [SerializeField] Casco casco;
    [SerializeField] Pecho pecho;
    [SerializeField] Brazo brazo;
    [SerializeField] Pierna pierna;
    [SerializeField] Principal arma;

    void Update()
    {
        GameObject canvas = GameObject.Find("Canvas");
        canvas.GetComponent<KeyController>().nuevaScene();
        pj = GameObject.FindGameObjectWithTag("Player");
        casco = pj.transform.GetChild(0).gameObject.GetComponent<Casco>();
        pecho = pj.transform.GetChild(1).gameObject.GetComponent<Pecho>();
        brazo = pj.transform.GetChild(2).gameObject.GetComponent<Brazo>();
        pierna = pj.transform.GetChild(4).gameObject.GetComponent<Pierna>();
        arma = GameObject.Find("Arma").GetComponent<Principal>();

        canvas.GetComponent<InventoryController>().getModelo();
        canvas.GetComponent<InventoryController>().getStats();

        canvas.transform.GetChild(10).gameObject.SetActive(true);
        pj.GetComponent<HealthPj>().SliderVida = canvas.transform.GetChild(10).GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>();
        pj.GetComponent<Shoot>().TextAmmo = canvas.transform.GetChild(10).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        pj.GetComponent<Shoot>().iniciaTexto();
        if (casco.materiales.materials[0].name == "Default (Instance)" ||
            pecho.materiales.materials[0].name == "Default (Instance)" ||
            pierna.materiales.materials[0].name == "Default (Instance)" ||
            brazo.materiales.materials[0].name == "Default (Instance)")
        {
            casco.onChangeItem();
            pecho.onChangeItem();
            pierna.onChangeItem();
            brazo.onChangeItem();
            arma.onChangeItem();
        }

        canvas.GetComponent<KeyController>().InvetarioBool = false;
        canvas.GetComponent<KeyController>().mostrarIventario();
        pj.GetComponent<Actor>().nuevaEscena();
        bunker.enabled = false;

        canvas.transform.GetChild(10).GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().SetText("Mundo Tras La Muralla");
        canvas.transform.GetChild(10).GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().SetText("Exploracion libre");

        Destroy(gameObject);
    }
}
