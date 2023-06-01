using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using Unity.FPS.AI;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class Mision1 : MonoBehaviour//SI HUBIERA MAS MISIONES HACER CLASE PADRE O INTERFAZ PARA TODAS LAS MISIONES
{
    [SerializeField] CambioEscena pipeEscenas;
    [SerializeField] GameObject informacion;

    [SerializeField] List<string> fases;
    [SerializeField] TextMeshProUGUI infoMision;

    [SerializeField] List<string> objetivos;
    [SerializeField] TextMeshProUGUI estadoMision;

    [SerializeField] int currentFase;

    //FASE 0
    [SerializeField] List<GameObject> torretas;
    [SerializeField] int numTorretas;

    //FASE 1
    [SerializeField] BoxCollider panelControl;

    //FASE 2
    [SerializeField] BoxCollider puertaBunker;
    [SerializeField] BoxCollider cargaBunker;

    //FASE 3 (Vincular Objetos Nueva Escena Bunker para el resto de fases)
    [SerializeField] SpawnPuerta puerta1;
    [SerializeField] SpawnPuerta puerta2;
    [SerializeField] SpawnPuerta puerta3;
    [SerializeField] SpawnPuerta puerta4;
    [SerializeField] SpawnPuerta puerta5;
    [SerializeField] SpawnPuerta puerta6;
    [SerializeField] List<GameObject> torretasZona1;
    [SerializeField] GameObject Zona1Bunker;

    //FASE 6 HACK PANEL PUERTA FINAL
    [SerializeField] SphereCollider zonaHack;
    [SerializeField] int tiempoHack;
    [SerializeField] float currentHack;
    [SerializeField] bool dentro;
    [SerializeField] PatrolPath ruta1;
    [SerializeField] PatrolPath ruta2;
    [SerializeField] BoxCollider puertaBoss;
    [SerializeField] GameObject enemigo;

    //Fase Final
    [SerializeField] bool final;

    void Start()
    {
        final = false;
        pipeEscenas = GameObject.FindGameObjectWithTag("Pipe").GetComponent<CambioEscena>();
        transform.SetParent(null);
        transform.SetAsFirstSibling();
        pipeEscenas.addDontDestroyGameObject(gameObject);
        DontDestroyOnLoad(gameObject);
        gameObject.transform.position = new Vector3(-13.6672993f, 4.87262964f, -58.0251007f);
        currentFase = 0;
        currentHack = 0;
        numTorretas = torretas.Count;
        puertaBunker = GameObject.Find("Bunker").transform.GetChild(0).GetComponent<BoxCollider>();
        cargaBunker = GameObject.Find("Bunker").transform.GetChild(1).GetComponent<BoxCollider>();
        GameObject canvas = GameObject.Find("Canvas");
        informacion = canvas.transform.GetChild(10).GetChild(1).gameObject;
        infoMision = informacion.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        estadoMision = informacion.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        infoMision.SetText(fases[0]);
        estadoMision.SetText(objetivos[0]);
    }

    private void Update()
    {
        Debug.Log("Fase " + currentFase);
        switch (currentFase) {
            case 0:
                DestrulleTorretas();
                Debug.Log("Torretas");
                return;
            case 1:
                Debug.Log("Panel");
                //No hay logica de actualizacion (Destruir Panel)
                return;
            case 2:
                entraBunker();
                Debug.Log("Cambio Escena");
                return;
            case 3:
                checkCambioEscenaBunker();
                Debug.Log("Vinculando Escena");
                return;
            case 4:
                Debug.Log("Entra en el bunker");
                return;
            case 5:
                enemigoEleminados();
                Debug.Log("Contando Enemigos");
                return;
            case 6:
                hackPanel();
                return;
            case 7:
                return;
            case 8:
                zonafinalPorfin();
                return;
        }
    }

    private void entraBunker()
    {
        if (SceneManager.GetActiveScene().name == "bunker")
        {
            CompleteFase();
        }
    }

    public void CompleteFase() {
        currentFase++;
        infoMision.SetText(fases[currentFase]);
        estadoMision.SetText(objetivos[currentFase]);
        infoMision.gameObject.SetActive(false);
        estadoMision.gameObject.SetActive(false);
        infoMision.gameObject.SetActive(true);
        estadoMision.gameObject.SetActive(true);
        informacion.SetActive(false);
        informacion.SetActive(true);

        switch (currentFase)
        {
            case 1:
                IncializarPaneldeControl();
                return;
            case 2:
                IncializarPuertaBunker();
                return;
            case 3:
                return;
            case 4:
                return;
            case 5:
                return;
            case 6:
                inciaHack();
                return;
            case 7:
                paseZonaBoss();
                return;
        }
    }

    public void DestrulleTorretas() {
        foreach (GameObject torreta in torretas)
        {
            if (torreta == null) torretas.Remove(torreta);
        }

        int currentTorretas = torretas.Count;

        if (currentTorretas == 0)
        {
            CompleteFase();
        }
        else {
            estadoMision.SetText(objetivos[currentFase] + (numTorretas - currentTorretas) + "/" + numTorretas);
        }
    }

    public void IncializarPaneldeControl() {
        panelControl.enabled = true;
    }

    public void IncializarPuertaBunker()
    {
        puertaBunker.enabled = true;
        cargaBunker.enabled = true;
    }
   
    public void checkCambioEscenaBunker()
    {
        Debug.Log("Comprobando Enemigos");
        if (GameObject.Find("Mision1_Bunker_Zona1(Clone)") != null) {
            Destroy(GameObject.Find("Mision1_Bunker_Zona1(Clone)"));
        }
        
        puertaBoss = GameObject.Find("PuertaBoss").GetComponent<BoxCollider>();
        puertaBoss.enabled = false;

        puerta1 = GameObject.Find("PuertaSpawn0").GetComponent<SpawnPuerta>();
        puerta2 = GameObject.Find("PuertaSpawn1").GetComponent<SpawnPuerta>();
        puerta3 = GameObject.Find("PuertaSpawn2").GetComponent<SpawnPuerta>();
        puerta4 = GameObject.Find("PuertaSpawn3").GetComponent<SpawnPuerta>();
        puerta5 = GameObject.Find("PuertaSpawn4").GetComponent<SpawnPuerta>();
        puerta6 = GameObject.Find("PuertaSpawn5").GetComponent<SpawnPuerta>();
        if (puerta6 == null) {
            puerta6 = GameObject.Find("PuertaSpawn5").GetComponent<SpawnPuerta>();
        }

        puerta1.StopSpawnWhenMaxEntidades = true;
        puerta2.StopSpawnWhenMaxEntidades = true;
        puerta3.StopSpawnWhenMaxEntidades = true;
        puerta4.StopSpawnWhenMaxEntidades = true;
        puerta5.StopSpawnWhenMaxEntidades = true;
        puerta6.StopSpawnWhenMaxEntidades = true; 
        puerta6.StopSpawnWhenMaxEntidades = true;

        puerta1.NumEntidadesMax = 2;
        puerta2.NumEntidadesMax = 2;
        puerta3.NumEntidadesMax = 2;
        puerta4.NumEntidadesMax = 2;
        puerta5.NumEntidadesMax = 2;
        puerta6.NumEntidadesMax = 2;

        puerta1.TiempoSpawnDelay = 3f;
        puerta2.TiempoSpawnDelay = 3f;
        puerta3.TiempoSpawnDelay = 3f;
        puerta4.TiempoSpawnDelay = 3f;
        puerta5.TiempoSpawnDelay = 3f;
        puerta6.TiempoSpawnDelay = 3f;

        puerta1.CurrentTime = 1f;
        puerta2.CurrentTime = 1f;
        puerta3.CurrentTime = 1f;
        puerta4.CurrentTime = 1f;
        puerta5.CurrentTime = 1f;
        puerta6.CurrentTime = 1f;

        GameObject zona1 = Instantiate(Zona1Bunker);
        for (int i = 0; i < 6; i++)
        {
            torretasZona1.Add(zona1.transform.GetChild(i).gameObject);
        }

        zonaHack = zona1.transform.GetChild(10).GetChild(0).GetComponent<SphereCollider>();
        zonaHack.enabled= false;

        ruta1 = zona1.transform.GetChild(6).GetComponent<PatrolPath>();
        ruta2 = zona1.transform.GetChild(7).GetComponent<PatrolPath>();

        CompleteFase();
    }

    public void enemigoEleminados() {
        foreach (GameObject torreta in torretasZona1) {
            if(torreta == null)torretasZona1.Remove(torreta);
        }
        if (torretasZona1.Count == 0 &&
            puerta1.CountCurrentEntidades() == 0 &&
            puerta2.CountCurrentEntidades() == 0 &&
            puerta3.CountCurrentEntidades() == 0 &&
            puerta4.CountCurrentEntidades() == 0 &&
            puerta5.CountCurrentEntidades() == 0 &&
            puerta6.CountCurrentEntidades() == 0)
        {
            CompleteFase();
        }
    }

    public int faseActual() {
        return currentFase;
    }
    public void inciaHack()
    {
        zonaHack.enabled = true;

        puerta1.Ruta = ruta1;
        puerta5.Ruta = ruta2; 
        puerta1.StopSpawnWhenMaxEntidades = false; 
        puerta5.StopSpawnWhenMaxEntidades = false;
        puerta1.TiempoSpawnDelay = 3f;
        puerta5.TiempoSpawnDelay = 3f; 
        puerta1.CurrentTime = 0f;
        puerta5.CurrentTime = 0f;
        puerta1.StopSpawn = false;
        puerta5.StopSpawn = false;

        puerta1.SetEnemigo(enemigo);
        puerta2.SetEnemigo(enemigo); 
        puerta4.SetEnemigo(enemigo); 
        puerta5.SetEnemigo(enemigo);
    }
    
    public void hackPanel() {
        if (currentHack < 1f) {
            return;
        }

        if (dentro)
        {
            float porcentajeHack = (currentHack / tiempoHack) * 100;
            if(porcentajeHack >100)porcentajeHack=100;
            estadoMision.SetText("Hackeando: " + porcentajeHack.ToString("F2") + "%");

            if (porcentajeHack >= 69f && porcentajeHack <= 71f) {
                puerta2.Ruta = ruta1;
                puerta4.Ruta = ruta2;
                puerta2.StopSpawnWhenMaxEntidades = false;
                puerta4.StopSpawnWhenMaxEntidades = false;
                puerta2.TiempoSpawnDelay = 7f;
                puerta4.TiempoSpawnDelay = 7f;
                puerta2.StopSpawn = false;
                puerta4.StopSpawn = false;
            }

            if (porcentajeHack == 100) { 
                puerta1.enabled = false;
                puerta2.enabled = false;
                puerta3.enabled = false;
                puerta4.enabled = false;
                puerta5.enabled = false;
                CompleteFase(); 
            }
        }
        else {
            estadoMision.SetText("Debes estar cerca del panel");
        }
    }

    public void paseZonaBoss() {
        puertaBoss.enabled = true;
    }

    public void zonafinalPorfin()
    {
        if (final)
        {
            CompleteFase(); 
            Destroy(gameObject);
        }
    }

    public void volverPuntoAnterior(int fase)
    {
        currentFase = fase - 1;
        CompleteFase();
        
    }

    public bool Dentro { get => dentro; set => dentro = value; }
    public float CurrentHack { get => currentHack; set => currentHack = value; }

    public void TerminaMision() {
        final = true;
    }
}
