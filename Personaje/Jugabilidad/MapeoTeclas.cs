using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapeoTeclas : MonoBehaviour
{
    PlayerController inputSystem;
    Animator anim;

    Camera_Pj_Last mira;
    Shoot armapimum;
    Movement_PJ_Last movimiento;
    Recoil disparitos;

    GameObject inventarioCanvas;
    InventoryController inventario;
    Interaccion interaccion;

    GameObject menu;
    
    public bool inventarioBool;

    private void Start()
    {
        inventario = GetComponent<InventoryController>();
        inventarioCanvas = gameObject;

        menu = GameObject.Find("Menu");
        menu.SetActive(false);
    }

    public void nuevaScene() {
        GameObject pj = GameObject.FindGameObjectWithTag("Player");
        armapimum = pj.GetComponent<Shoot>();
        mira = pj.GetComponent<Camera_Pj_Last>();
        movimiento = pj.GetComponent<Movement_PJ_Last>();
        disparitos = pj.GetComponent<Recoil>();
        anim = pj.GetComponent<Animator>();
        interaccion = pj.GetComponent<Interaccion>();
    }

    private void Awake()
    {
        inputSystem = new PlayerController();

        //INVENTORY
        inputSystem.Move.Inventario.started += ctx => abrirCerrarInventario();
        inputSystem.Move.Borrar.performed += ctx => borrando(true);
        inputSystem.Move.Borrar.canceled += ctx => borrando(false);

        //MOVE
        inputSystem.Move.Derecha.performed += ctx => movimiento.right(1);
        inputSystem.Move.Derecha.canceled += ctx => movimiento.right(0);

        inputSystem.Move.Izquierda.performed += ctx => movimiento.left(-1);
        inputSystem.Move.Izquierda.canceled += ctx => movimiento.left(0);

        inputSystem.Move.Adelante.performed += ctx => movimiento.up(1);
        inputSystem.Move.Adelante.canceled += ctx => movimiento.up(0);

        inputSystem.Move.Atras.performed += ctx => movimiento.down(-1);
        inputSystem.Move.Atras.canceled += ctx => movimiento.down(0);

        inputSystem.Move.Agachar.started += ctx => movimiento.crouched();
        inputSystem.Move.Saltar.started += ctx => movimiento.jump();
        inputSystem.Move.Sprint.started += ctx => movimiento.run();

        //GUNPLAY
        inputSystem.Move.Apuntar.performed += ctx => disparitos.apuntar(true);
        inputSystem.Move.Apuntar.canceled += ctx => disparitos.apuntar(false);

        inputSystem.Move.Disparar.started += ctx => shoot(true);
        inputSystem.Move.Disparar.canceled += ctx => shoot(false);

        inputSystem.Move.Recarga.performed += ctx => reload();

        //OTROS
        inputSystem.Move.Menu.started += ctx => mostrarMenu();

        inputSystem.Move.Interaccion.started += ctx => interaccion.setInteracion();
        inputSystem.Move.Interaccion.canceled += ctx => interaccion.setInteracion();
    }

    private void shoot(bool v)
    {
        if (v && movimiento.running())
        {
            movimiento.run();
        }
        armapimum.Disparando = v;
    }

        private void reload()
    {
        if (armapimum.TamannaCargador != armapimum.Balas && !anim.GetBool("recarga"))
        {
            anim.SetBool("recarga", true);
            armapimum.reload();
        }
    }

    private void abrirCerrarInventario()
    {
        if (menu.activeSelf) {
            return;
        }
        inventarioBool = anim.GetBool("Inventario");
        if (inventarioBool) {
            inventarioBool = false;
            mira.cambiarEstado(true);
            movimiento.cambiarEstado(true);
            anim.SetBool("Inventario", false);
        }
        else
        {
            inventarioBool = true;
            mira.cambiarEstado(false);
            movimiento.cambiarEstado(false);
            anim.SetBool("Inventario", true);
            anim.SetInteger("movimiento", 0);
            anim.SetInteger("moveBrazo", 0);
        }
        mostrarIventario();
    }

    private void borrando(bool v)
    {
        if (inventarioBool) {
            inventario.SetBorrarItem(v);
            inventario.SetCurrentiempoBorrar(1f);
        }
    }

    public void mostrarMenu() {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            if (!inventarioBool)
            {
                mira.cambiarEstado(true);
                movimiento.cambiarEstado(true);
            }
        }
        else {
            menu.SetActive(true);
            if (!inventarioBool)
            {
                mira.cambiarEstado(false);
                movimiento.cambiarEstado(false);
            }
        }
    }

    public void mostrarIventario() {
        inventarioCanvas.transform.GetChild(1).gameObject.SetActive(inventarioBool);
        inventarioCanvas.transform.GetChild(2).gameObject.SetActive(inventarioBool);
        inventarioCanvas.transform.GetChild(3).gameObject.SetActive(inventarioBool);
        inventarioCanvas.transform.GetChild(4).gameObject.SetActive(inventarioBool);
        inventarioCanvas.transform.GetChild(5).gameObject.SetActive(inventarioBool);
        inventarioCanvas.transform.GetChild(6).gameObject.SetActive(inventarioBool);
        inventarioCanvas.transform.GetChild(7).gameObject.SetActive(inventarioBool);
    }

    private void OnEnable()
    {
        inputSystem.Move.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Move.Disable();
    }
}
