using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

//CLASE PARA TRASLADAR OBJETOS ENTRE ESCENAS (Pj: El inventario no debe cargarse en cada escena, se carga en el inicio del juego y se mantiene)
public class CambioEscena : MonoBehaviour
{

    [SerializeField] bool cambio;

    [SerializeField] List<GameObject> objetoSalvado;

    private void Start()
    {
        objetoSalvado = new List<GameObject>();
        cambio = false;
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cambio = false;
        int sceneCount = SceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name != "DontDestroyOnLoad") {
                foreach (GameObject j in objetoSalvado)
                {
                    SceneManager.MoveGameObjectToScene(j, SceneManager.GetSceneAt(i));
                }
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneAt(i));
            };
        }
    }

    public void addDontDestroyGameObject(GameObject obj) {
        objetoSalvado.Add(obj);
    }

    public void quitarLista(GameObject obj)
    {
        if (objetoSalvado.Contains(obj)) {
            objetoSalvado.Remove(obj);
        }
    }

    public void cargaNuevaEscena() {
        cambio = true;

        try
        {
            foreach (GameObject i in objetoSalvado)
            {
                if (i == null)
                {
                    objetoSalvado.Remove(i);
                }
                else
                {
                    DontDestroyOnLoad(i);
                }
            }
        }catch(Exception) { }
        DontDestroyOnLoad(gameObject);
    }

    public bool Cambio { get => cambio; set => cambio = value; }
}
