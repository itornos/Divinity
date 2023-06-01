using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmaduraController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Armadura item;

    actualizarArmadura skin;
    Animator AnimatorItem;

    InventoryController inventario;

    bool itemNuevo;

    private void Start()
    {
        inventario = GameObject.Find("Canvas").gameObject.GetComponent<InventoryController>();
        skin = transform.GetChild(0).GetChild(0).gameObject.GetComponent<actualizarArmadura>();
        AnimatorItem = GetComponent<Animator>();
        if (gameObject.name == "Equipado")
        {
            AnimatorItem.SetBool("Nuevo", false);
        }
    }

    //ANIMACION OBJETO NUEVO AL MOSTRAR INVENTARIO
    private void OnEnable()
    {
        try { AnimatorItem.SetBool("Nuevo", itemNuevo); } catch (Exception) { }
    }

    //MOSTRAR OBJETO NUEVO POR PANTALLA Y GUARDAR EN COPIA NO DESTRUIBLE PARA MANDAR A INVENTARIO
    public void inicializa(string tipoCalidad, string nombreEquipamiento, string equipo)
    {
        item = new Armadura(nombreEquipamiento, equipo, tipoCalidad);
    }

    //ITEM INVENTARIO
    public void inicializa(Armadura item, bool nuevo)
    {
        this.item = item;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);

        if (gameObject.name == "Equipado")
        {
            itemNuevo = false;
        }
        else {
            itemNuevo = nuevo;
        }

        skin.setSkinItem();
    }

    //ITEM INVENTARIO
    public void inicializa(Armadura item)
    {
        this.item = item; 
    }

    //ITEM INVENTARIO DESDE JSON
    public void loadJson(Armadura item)
    {
        this.item = item;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    //SI NO EXISTE EL ARCHIVO JSON ES UN JUGADOR NUEVO, NEWBIE ITEMS :)
    public void nuevoJugador(string nombre, string equipo)
    {
        item = new Armadura(nombre, equipo, "Comun (Instance)");
        
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    //USAR CLICK DERECHO PARA CAMBIAR ITEMS INVENTARIO
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventario.swapGameObject(transform.parent.transform.parent.name, gameObject.name);
        }
    }

    //SABER QUE ITEM ESTA SOBRE EL RATON PARA BORRARLO (HAY QUE MANTENER EL BOTON DE BORRAR, SI EL ITEM CAMBIA SE CANCELA EL BORRADO)
    public void OnPointerEnter(PointerEventData eventData) {
        nuevo();
        inventario.setCurrentItem(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventario.setCurrentItem(null);
    }

    //CUANDO EL ITEM SEA NUEVO CAMBIAR SU ESTADO AL PASAR EL RATON SOBRE EL PANEL DEL ITEM
    public void nuevo() {
        itemNuevo = false;
        AnimatorItem.SetBool("Nuevo", false);
    }

    //RESET DE VALORES AL BORRAR ITEM
    public void borrarContenido()
    {
        item = new Armadura();
        skin.setSkinItem();

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }


    //SI NO TIENE MATERIAL POR POSIBLE FALLO
    public void material()
    {
        skin.inicializarEstiloItem();
    }

    //SI NO TIENE LOS PANELES ACTUALIZADOS POR POSIBLE FALLO
    public void actualizarSkin() {
        skin.setSkinItem();
    }

    //DESTRUIR ITEM TEMPORAL QUE SE MUESTRA AL RECOGER UN ITEM DEL SUELO
    public void destrulle()
    {
        Destroy(gameObject);
    }

    public void animacionCambio()
    {
        AnimatorItem.SetBool("Cambio", true);
    }

    public void dejarAnimacionCambio()
    {
        AnimatorItem.SetBool("Cambio", false);
    }

    public string getTipo()
    {
        return item.getTipo();
    }

    public string getEquipo()
    {
        return item.getEquipo();
    }

    public Material getMaterialPrincipal() {
        return item.getMaterialPrincipal();
    }

    public Material getMaterialSecundario()
    {
        return item.getMaterialSecundario();
    }

    public void setMaterialPrincipal(Material nuevoMaterial) {
        item.setMaterialPrincipal(nuevoMaterial);
    }

    public void setMaterialSecundario(Material nuevoMaterial)
    {
        item.setMaterialSecundario(nuevoMaterial);
    }

    public Armadura getItem() {
        return item;
    }

    public int getMaterialSecundarioHash()
    {
        return item.getMaterialSecundarioHash();
    }

    public int getMaterialPrincipalHash()
    {
        return item.getMaterialPrincipalHash();
    }

    public string getNombre()
    {
        return item.getNombre();
    }
}
