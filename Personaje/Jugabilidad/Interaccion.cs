using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaccion : MonoBehaviour
{
    [SerializeField] GameObject interactuar;
    [SerializeField] TextMeshProUGUI textoTecla;
    [SerializeField] TextMeshProUGUI textoMensaje;
    [SerializeField] InputActionReference tecla;//0 mision, 1 Comerciante

    [SerializeField] GameObject imagenCadera;
    [SerializeField] Quaternion posicionOriginalImagenCadera;

    [SerializeField] bool interactua;
    [SerializeField] bool interaccionActivada;
    [SerializeField] int tipoInteraccion;//0 mision, 1 Comerciante
    [SerializeField] const float tiempoInteraccion = 0.65f;
    [SerializeField] float currentTime;

    [SerializeField] GameObject objetoInteraccionado;

    [SerializeField] CountNewItems newItem;
    [SerializeField] InventoryController inventory;
    [SerializeField] LoadingController loadScene;
    [SerializeField] CambioEscena ControladorEscena;

    private void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");

        interactuar = canvas.transform.GetChild(9).gameObject; 
        textoTecla = interactuar.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        textoMensaje = interactuar.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        inventory = canvas.gameObject.GetComponent<InventoryController>();
        newItem = canvas.gameObject.transform.GetChild(0).GetComponent<CountNewItems>();

        posicionOriginalImagenCadera = new Quaternion(imagenCadera.transform.rotation.x, imagenCadera.transform.rotation.y, imagenCadera.transform.rotation.z, imagenCadera.transform.rotation.w);

        interaccionActivada = false;
        interactua = false;
        interactuar.SetActive(false);
    }

    private void Update()
    {
        if (interactua && !interaccionActivada)
        {
            currentTime -= Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(0f, 0f, -10f) * imagenCadera.transform.rotation;
            imagenCadera.transform.rotation = Quaternion.Lerp(imagenCadera.transform.rotation, rotation, 30 * Time.deltaTime);

            if (currentTime <= 0)
            {

                if (tipoInteraccion == 0)
                {
                    objetoInteraccionado.GetComponent<IniciarMision>().nuevaMision();
                    interaccionActivada = true;
                }
                else if (tipoInteraccion == 1)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                }
            }
        }
        else {
            imagenCadera.transform.rotation = Quaternion.Lerp(imagenCadera.transform.rotation, 
                                                              posicionOriginalImagenCadera,
                                                              5 * Time.deltaTime);
        }
    }

    public void setInteracion() {
        if (!interactuar.activeSelf) return;

        if (interactua)
        {
            interactua = false;
            interaccionActivada = false;
        }
        else {
            interactua = true;
        }
        Debug.Log(interactua);
        currentTime = tiempoInteraccion;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "interaccion")
        {
            interactuar.SetActive(true); 
            textoMensaje.SetText("Interactuar");
            InputBinding binding = tecla.action.bindings[0];
            textoTecla.SetText(binding.path.Substring(binding.path.LastIndexOf('/') + 1).ToUpper());
            objetoInteraccionado = other.gameObject;

        } else if (other.tag == "interaccionMision") {
            interactuar.SetActive(true);
            textoMensaje.SetText("Iniciar Mision");
            InputBinding binding = tecla.action.bindings[0];
            textoTecla.SetText(binding.path.Substring(binding.path.LastIndexOf('/') + 1).ToUpper());
            objetoInteraccionado = other.gameObject;

        }
        else if (other.tag == "Item")
        {
            string tipo = other.gameObject.GetComponent<Renderer>().material.name;
            Destroy(other.gameObject);

            int equipo = Random.Range(0, 5);
            switch (equipo)
            {
                case 0:
                    newItem.addItem(tipo, "Casco basico", "Casco");
                    return;
                case 1:
                    newItem.addItem(tipo, "Pechera basica", "Pecho");
                    return;
                case 2:
                    newItem.addItem(tipo, "Pierna basica", "Pierna");
                    return;
                case 3:
                    newItem.addItem(tipo, "Brazo basico", "Brazo");
                    return;
                case 4:
                    newItem.addItem(tipo, "Ak-47 Compacta", "Arma Principal Automatica");
                    return;
            }

        }
        else if (other.tag == "save")
        {
            inventory.save();
        }
        else if (other.tag == "Bunker") {
            loadScene = GameObject.Find("Canvas").GetComponent<LoadingController>();
            ControladorEscena = GameObject.Find("ControladorEscena").GetComponent<CambioEscena>();
            ControladorEscena.addDontDestroyGameObject(gameObject);
            loadScene.bunker();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "interaccion" || other.tag == "interaccionMision")
        {
            interactuar.SetActive(false);
        }
    }

    public void resetStats() {
        inventory.aplicarStats();
    }
}
