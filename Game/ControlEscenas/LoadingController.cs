using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{

    [SerializeField] CambioEscena controladorEscenas;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject destinos;
    [SerializeField] GameObject load;

    [SerializeField] InventoryController inventoryController;

    [SerializeField] Animator cambioEscena;

    public void cerrarJuego() {
        Application.Quit();
    }

    public void mundoLibre() {
        GameObject misionActiva = GameObject.FindGameObjectWithTag("mision");
        GameObject PlayerFPS = GameObject.Find("PlayerFPS");
        if (misionActiva != null) Destroy(misionActiva);
        if (PlayerFPS != null) controladorEscenas.quitarLista(PlayerFPS);

        procedimientoCambioEscena("MainScene");
    }

    public void zonaSegura()
    {
        GameObject misionActiva = GameObject.FindGameObjectWithTag("mision");
        GameObject PlayerFPS = GameObject.Find("PlayerFPS");
        if (misionActiva != null)Destroy(misionActiva);
        if (PlayerFPS != null) controladorEscenas.quitarLista(PlayerFPS);

        procedimientoCambioEscena("ZonaSegura");
    }
    public void bunker()
    {
        procedimientoCambioEscena("bunker");
    }

    void procedimientoCambioEscena(string nombreEscena) {
        inventoryController.save();
        menu.SetActive(false); 
        destinos.SetActive(false);
        load.SetActive(true);
        controladorEscenas.cargaNuevaEscena();
        StartCoroutine(LoadMundoLibre(nombreEscena));
    }

    private IEnumerator LoadMundoLibre(string scene) {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(scene);
        operacion.allowSceneActivation = false;

        while (!operacion.isDone) {
            if (operacion.progress >= 0.9f) {
                operacion.allowSceneActivation = true;
                cambioEscena.SetBool("Carga", false);
            }

            yield return null;
        }
    }
}
