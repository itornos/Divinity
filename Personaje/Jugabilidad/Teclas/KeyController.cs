using UnityEngine;
using UnityEngine.InputSystem;

public class KeyController : MonoBehaviour
{
    [SerializeField] PlayerInput playerActionMap;
    [SerializeField] Animator anim;

    [SerializeField] Camera_Pj_Last mira;
    [SerializeField] Shoot armapimum;
    [SerializeField] Movement_PJ_Last movimiento;
    [SerializeField] Recoil disparitos;

    [SerializeField] GameObject inventarioCanvas;
    [SerializeField] InventoryController inventario;
    [SerializeField] Interaccion interaccion;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject destinos; 
    [SerializeField] GameObject situacionMision;

    [SerializeField] bool inventarioBool;

    [SerializeField] bool isDead;

    private void Start()
    {
        inventario = GetComponent<InventoryController>();
        inventarioCanvas = gameObject;
    }

    private void Update()
    {
        if (anim == null) return;

        if (anim.runtimeAnimatorController.name != "Personaje")
        {
            isDead = true;
            movimiento.enabled = false;
        }
        else if (isDead)
        {
            isDead = false;
            inventario.aplicarStats();
            movimiento.enabled = true;
        }
    }

    private void Awake()
    {
        //INVENTORY
        playerActionMap.actions["Inventario"].started += ctx => abrirCerrarInventario();
        playerActionMap.actions["Borrar"].performed += ctx => borrando(true);
        playerActionMap.actions["Borrar"].canceled += ctx => borrando(false);

        //MOVE
        playerActionMap.actions["Derecha"].performed += ctx => movimiento.right(1);
        playerActionMap.actions["Derecha"].canceled += ctx => movimiento.right(0);

        playerActionMap.actions["Izquierda"].performed += ctx => movimiento.left(-1);
        playerActionMap.actions["Izquierda"].canceled += ctx => movimiento.left(0);

        playerActionMap.actions["Adelante"].performed += ctx => movimiento.up(1);
        playerActionMap.actions["Adelante"].canceled += ctx => movimiento.up(0);

        playerActionMap.actions["Atras"].performed += ctx => movimiento.down(-1);
        playerActionMap.actions["Atras"].canceled += ctx => movimiento.down(0);

        playerActionMap.actions["Agachar"].started += ctx => movimiento.crouched();
        playerActionMap.actions["Saltar"].started += ctx => movimiento.jump();
        playerActionMap.actions["Sprint"].started += ctx => movimiento.run();

        //GUNPLAY
        playerActionMap.actions["Apuntar"].performed += ctx => apuntar(true);
        playerActionMap.actions["Apuntar"].canceled += ctx => apuntar(false);

        playerActionMap.actions["Disparar"].started += ctx => shoot(true);
        playerActionMap.actions["Disparar"].canceled += ctx => shoot(false);

        playerActionMap.actions["Recarga"].performed += ctx => reload();

        //OTROS
        playerActionMap.actions["Menu"].started += ctx => mostrarMenu();

        playerActionMap.actions["Interaccion"].started += ctx => interaccion.setInteracion();
        playerActionMap.actions["Interaccion"].canceled += ctx => interaccion.setInteracion();

        playerActionMap.actions["Destinos"].started += ctx => destinosAction();

        playerActionMap.actions["SituacionActual"].started += ctx => situacionActual();
    }

    public void nuevaScene()
    {
        GameObject pj = GameObject.FindGameObjectWithTag("Player");
        armapimum = pj.GetComponent<Shoot>();
        mira = pj.GetComponent<Camera_Pj_Last>();
        movimiento = pj.GetComponent<Movement_PJ_Last>();
        disparitos = pj.GetComponent<Recoil>();
        anim = pj.GetComponent<Animator>();
        interaccion = pj.GetComponent<Interaccion>();
    }

    private void apuntar(bool v)
    {
        if (v && movimiento.running())
        {
            movimiento.run();
        }
        if (!inventarioBool)
        {
            disparitos.apuntar(v);
        }
    }

    private void situacionActual()
    {
        if (situacionMision.activeSelf)
        {
            situacionMision.GetComponent<Animator>().SetBool("accion", false);
        }
        else {
            situacionMision.SetActive(true);
        }
    }

    private void shoot(bool v)
    {
        if (v && movimiento.running())
        {
            movimiento.run();
        }
        if (!inventarioBool || menu.activeSelf)
        {
            armapimum.Disparando = v;
        }
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
        if (menu.activeSelf || isDead) {
            return;
        }
        inventarioBool = anim.GetBool("Inventario");
        if (inventarioBool) {
            inventario.setCurrentItem(null);
            inventarioBool = false;
            mira.cambiarEstado(true);
            movimiento.cambiarEstado(true);
            anim.SetBool("Inventario", false);
        }
        else
        {
            inventario.setCurrentItem(null);
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

    private void destinosAction()
    {
        if (destinos.activeSelf)
        {
            destinos.SetActive(false);
            if (!inventarioBool)
            {
                mira.cambiarEstado(true);
                movimiento.cambiarEstado(true);
            }
        }
        else
        {
            destinos.SetActive(true);
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

    public void resetInputs()
    {
        foreach (InputActionMap map in playerActionMap.actions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
        PlayerPrefs.DeleteKey("rebinds");
    }

    public bool InvetarioBool { get => inventarioBool; set => inventarioBool = value; }
}
