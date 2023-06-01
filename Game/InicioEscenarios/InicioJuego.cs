using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InicioJuego : MonoBehaviour
{
    CambioEscena inicio;
    bool vinculo;

    private void Start()
    {
        inicio = GameObject.Find("ControladorEscena").GetComponent<CambioEscena>();
        vinculo = false;
    }


    void Update()
    {
        if (!inicio.Cambio && !vinculo)
        {
            vinculo = true;
            GameObject s = GameObject.Find("Canvas");
            inicio.addDontDestroyGameObject(s);
            inicio.cargaNuevaEscena();
            s.GetComponent<InventoryController>().vincularInventarioCanvas();
        }
    }

        public void procedimientoCambioEscena()
    {
        StartCoroutine(LoadMundoLibre("ZonaSegura"));
    }

    private IEnumerator LoadMundoLibre(string scene)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(scene);
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {
            if (operacion.progress >= 0.9f)
            {
                operacion.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
