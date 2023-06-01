using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

[System.Serializable]
public class InventoryController : MonoBehaviour
{
    [SerializeField] List<ArmaduraController> cascos       =  new List<ArmaduraController>();
    [SerializeField] List<ArmaduraController> brazos       =  new List<ArmaduraController>();
    [SerializeField] List<ArmaduraController> pechos       =  new List<ArmaduraController>();
    [SerializeField] List<ArmaduraController> piernas      =  new List<ArmaduraController>();
    [SerializeField] List<ArmaController> principal    =  new List<ArmaController>();
    [SerializeField] List<ArmaController> secundaria   =  new List<ArmaController>();
    [SerializeField] List<ArmaController> pesada       =  new List<ArmaController>();
    [SerializeField] List<ItemController> postMaster   =  new List<ItemController>(30);

    [SerializeField] Inventory inventario;
    [SerializeField] Casco PJcasco;
    [SerializeField] Pecho PJpecho;
    [SerializeField] Brazo PJbrazo;
    [SerializeField] Pierna PJpierna;
    [SerializeField] Principal PJArmaPrincipal;

    [SerializeField] bool borrarItem;
    [SerializeField] float currentiempoBorrar;
    [SerializeField] float tiempoBorrado;
    [SerializeField] public GameObject currentItemBorrar;
    [SerializeField] int checkHash;

    [SerializeField] Movement_PJ_Last statMovimiento;
    [SerializeField] HealthPj statsVida;
    [SerializeField] Shoot disparo;

    [SerializeField] GameObject inpectorItem;
    [SerializeField] public List<Sprite> listaTipos;

    private void Start()
    {
        currentItemBorrar = null;
        borrarItem = false;
        tiempoBorrado = 1f;
        currentiempoBorrar = tiempoBorrado;
        checkHash = 0;
    }

    //UPDATE PARA BORRAR ITEM (HAY QUE MANTENER EL BOTON PULSADO Y QUE EL ITEM SEA SIEMPRE EL MISMO)
    private void Update()
    {
        if (borrarItem)
        {
            if (checkHash == 0 || currentItemBorrar.GetHashCode() == checkHash)
            {

                currentiempoBorrar -= Time.deltaTime;

                if (currentiempoBorrar <= 0f)
                {
                    currentiempoBorrar = tiempoBorrado;

                    if (currentItemBorrar.name == "Equipado")
                    {
                        checkHash = 0;
                        return;
                    }

                    int pos = int.Parse(currentItemBorrar.name);
                    string lista = currentItemBorrar.transform.parent.transform.parent.name;
                    letMeLeaveGameObject(lista, pos);
                }

                checkHash = currentItemBorrar.GetHashCode();
                return;

            }else{
                currentiempoBorrar = tiempoBorrado;
                checkHash = 0;
            }
        }
    }

    //AL CARGAR ESCENA GET MODELO DE LA NUEVA ESCENA
    public void getModelo()
    {
        GameObject pj = GameObject.FindGameObjectWithTag("Player");
        PJcasco = pj.transform.GetChild(0).gameObject.GetComponent<Casco>();
        PJpecho = pj.transform.GetChild(1).gameObject.GetComponent<Pecho>();
        PJbrazo = pj.transform.GetChild(2).gameObject.GetComponent<Brazo>();
        PJpierna = pj.transform.GetChild(4).gameObject.GetComponent<Pierna>();
        PJArmaPrincipal = GameObject.Find("Arma").GetComponent<Principal>();

        PJcasco.setItem(cascos[9]);
        PJpecho.setItem(pechos[9]);
        PJbrazo.setItem(brazos[9]);
        PJpierna.setItem(piernas[9]);
        PJArmaPrincipal.setItem(principal[9]);
    }

    public void getStats() {
        GameObject pj = GameObject.FindGameObjectWithTag("Player");
        statMovimiento = pj.GetComponent<Movement_PJ_Last>();
        statsVida = pj.GetComponent<HealthPj>();
        disparo = pj.GetComponent<Shoot>();

        aplicarStats();
    }

    public void aplicarStats()
    {
        statMovimiento.reset();
        statMovimiento.setStatVelocity(cascos[9].getItem().getMovilidad() / 100f);
        statMovimiento.setStatVelocity(pechos[9].getItem().getMovilidad() / 100f);
        statMovimiento.setStatVelocity(brazos[9].getItem().getMovilidad() / 100f);
        statMovimiento.setStatVelocity(piernas[9].getItem().getMovilidad() / 100f);

        statsVida.reset();
        statsVida.setStatDureza(cascos[9].getItem().getDureza() / 100f);
        statsVida.setStatDureza(pechos[9].getItem().getDureza() / 100f);
        statsVida.setStatDureza(brazos[9].getItem().getDureza() / 100f);
        statsVida.setStatDureza(piernas[9].getItem().getDureza() / 100f);

        statsVida.setStatRecuperacion(cascos[9].getItem().getRegeneracion() / 100f);
        statsVida.setStatRecuperacion(pechos[9].getItem().getRegeneracion() / 100f);
        statsVida.setStatRecuperacion(brazos[9].getItem().getRegeneracion() / 100f);
        statsVida.setStatRecuperacion(piernas[9].getItem().getRegeneracion() / 100f);

        disparo.reset();
        disparo.setCadencia(principal[9].getItem().Cadencia);
        disparo.setDanno(principal[9].getItem().Danno);
        disparo.setRango(principal[9].getItem().Alcance);
        disparo.setTamnnoCargador(principal[9].getItem().Cargador);
        disparo.setVelocidadRecarga(principal[9].getItem().VelocidadRecarga);
    }

    //AÑADIR ITEM AL INVENTARIO
    public void addGameObjectPleaseArmadura(Armadura item)
    {
        Debug.Log("Buscando Lista: " + item.getEquipo());
        switch (item.getEquipo())
        {
            case "Casco":
                cascos = annadirGameObjectArmadura(cascos, item);
                return;
            case "Brazo":
                brazos = annadirGameObjectArmadura(brazos, item);
                return;
            case "Pecho":
                pechos = annadirGameObjectArmadura(pechos, item);
                return;
            case "Pierna":
                piernas = annadirGameObjectArmadura(piernas, item);
                return;
        }
    }

    public void addGameObjectPleaseArma(Arma item)
    {
        switch (item.getEquipo())
        {
            case "Arma Principal Automatica":
                principal = annadirGameObjectArma(principal, item);
                return;
                /*case "secundaria":
                    secundaria = annadirGameObjectArma(secundaria, item);
                    return;
                case "pesada":
                    pesada = annadirGameObjectArma(pesada, item);
                    return;*/
        }
    }

    private List<ArmaController> annadirGameObjectArma(List<ArmaController> inventario, Arma nuevo)
    {
        for (int i = 0; i < inventario.Count; i++)
        {
            if (inventario[i].gameObject.GetComponent<ArmaController>().getTipo() == "" || inventario[i].gameObject.GetComponent<ArmaController>().getTipo() == null)
            {
                inventario[i].GetComponent<ArmaController>().inicializa(nuevo, true);
                return inventario;
            }
        }

        return inventario;
    }

    private List<ArmaduraController> annadirGameObjectArmadura(List<ArmaduraController> inventario, Armadura nuevo)
    {
        Debug.Log("Buscando Hueco Lista");
        for (int i = 0; i < inventario.Count; i++)
        {
            if (inventario[i].gameObject.GetComponent<ArmaduraController>().getTipo() == "" || inventario[i].gameObject.GetComponent<ArmaduraController>().getTipo() == null)
            {
                inventario[i].GetComponent<ArmaduraController>().inicializa(nuevo, true);
                return inventario;
            }
        }

        return inventario;
    }

    //CAMBIAR ITEM DE EQUIPADO A LA POSICION X Y VICEVERSA
    public void swapGameObject(string lista, string pos) {
        switch (lista)
        {
            case "InventarioCascos":
                cascos = cambioGameObjectArmadura(cascos, pos);
                PJcasco.onChangeItem();
                aplicarStats();
                return;
            case "InventarioBrazos":
                brazos = cambioGameObjectArmadura(brazos, pos);
                PJbrazo.onChangeItem();
                aplicarStats();
                return;
            case "InventarioPechos":
                pechos = cambioGameObjectArmadura(pechos, pos);
                PJpecho.onChangeItem();
                aplicarStats();
                return;
            case "InventarioPiernas":
                piernas = cambioGameObjectArmadura(piernas, pos);
                PJpierna.onChangeItem();
                aplicarStats();
                return;
            case "InventarioPrincipal":
                principal = cambioGameObjectArma(principal, pos);
                PJArmaPrincipal.onChangeItem();
                aplicarStats();
                return;
           /* case "InventarioSecundario":
                secundaria = cambioGameObject(secundaria, pos);
                return;
            case "InventarioPesada":
                pesada = cambioGameObject(pesada, pos);
                return;*/
        }
    }

    private List<ArmaduraController> cambioGameObjectArmadura(List<ArmaduraController> inventario, string stringPos)
    {
        int pos;
        int.TryParse(stringPos, out pos);

        if (inventario[pos].getItem().getTipo() == "" || inventario[pos].getItem().getTipo() == null || pos == 9)
        {
            return inventario;
        }

        Debug.Log(inventario[pos].getItem().getDureza());
        Debug.Log(inventario[9].getItem().getDureza());

        Armadura equipado = inventario[9].getItem();
        Armadura desequipado = inventario[pos].getItem();

        inventario[9].inicializa(desequipado, false);
        inventario[pos].inicializa(equipado, false);

        if (inventario[9].getMaterialPrincipalHash() == 0 || inventario[9].getMaterialSecundarioHash() == 0)
        {
            inventario[9].material();
        }

        inventario[9].animacionCambio();
        inventario[pos].animacionCambio();

        return inventario;
    }

    private List<ArmaController> cambioGameObjectArma(List<ArmaController> inventario, string stringPos)
    {
        int pos;
        int.TryParse(stringPos, out pos);

        if (inventario[pos].getItem().getTipo() == "" || inventario[pos].getItem().getTipo() == null || pos == 9)
        {
            return inventario;
        }

        Arma equipado = inventario[9].getItem();
        Arma desequipado = inventario[pos].getItem();

        inventario[9].inicializa(desequipado, false);
        inventario[pos].inicializa(equipado, false);

        if (inventario[9].getMaterialPrincipalHash() == 0 || inventario[9].getMaterialSecundarioHash() == 0)
        {
            inventario[9].material();
        }

        inventario[9].animacionCambio();
        inventario[pos].animacionCambio();

        return inventario;
    }

    //BORRAR ITEM DE LA POS X, SI ESTA EQUIPADO NO SE PUEDE BORRAR
    public void letMeLeaveGameObject(string pieza, int pos) {
        switch (pieza) {
            case "InventarioCascos":
                cascos = borrarGameObjectArmadura(cascos, pos);
                return;
            case "InventarioBrazos":
                brazos = borrarGameObjectArmadura(brazos, pos);
                return;
            case "InventarioPechos":
                pechos = borrarGameObjectArmadura(pechos, pos);
                return;
            case "InventarioPiernas":
                piernas = borrarGameObjectArmadura(piernas, pos);
                return;
            case "InventarioPrincipal":
                principal = borrarGameObjectArma(principal, pos);
                return;
            case "InventarioSecundario":
                secundaria = borrarGameObjectArma(secundaria, pos);
                return;
            case "InventarioPesada":
                pesada = borrarGameObjectArma(pesada, pos);
                return;
        }
    }

    private List<ArmaController> borrarGameObjectArma(List<ArmaController> inventario, int pos)
    {
        inventario[pos].borrarContenido();
        return inventario;
    }

    private List<ArmaduraController> borrarGameObjectArmadura(List<ArmaduraController> inventario, int pos)
    {
        inventario[pos].borrarContenido();
        return inventario;
    }

    //AL INICIAR JUEGO RECOGER LOS GAMEOBJECTS E INTENTAR LEER DE JSON
    public void vincularInventarioCanvas()
    {
        piernas = setGameObjectsInContextArmadura(piernas, 1, gameObject);
        pechos = setGameObjectsInContextArmadura(pechos, 2, gameObject);
        brazos = setGameObjectsInContextArmadura(brazos, 3, gameObject);
        cascos = setGameObjectsInContextArmadura(cascos, 4, gameObject);
        //pesada = setGameObjectsInContextArma(pesada, 5, gameObject);
        //secundaria = setGameObjectsInContextArma(secundaria, 6, gameObject);
        principal = setGameObjectsInContextArma(principal, 7, gameObject);
        load();
    }

    //GUARDAR TODOS LOS ESPACIOS DE INVENTARIO
    public List<ArmaController> setGameObjectsInContextArma(List<ArmaController> lista, int pos, GameObject canvas)
    {
        GameObject listado = canvas.transform.GetChild(pos).GetChild(1).gameObject;
        for (int i = 0; i < 9; i++)
        {
            lista.Add(listado.transform.GetChild(i).gameObject.GetComponent<ArmaController>());
        }
        lista.Add(canvas.transform.GetChild(pos).GetChild(0).gameObject.GetComponent<ArmaController>());
        canvas.transform.GetChild(pos).gameObject.SetActive(false);
        return lista;
    }

    public List<ArmaduraController> setGameObjectsInContextArmadura(List<ArmaduraController> lista, int pos, GameObject canvas)
    {
        GameObject listado = canvas.transform.GetChild(pos).GetChild(1).gameObject;
        for (int i = 0; i < 9; i++)
        {
            lista.Add(listado.transform.GetChild(i).gameObject.GetComponent<ArmaduraController>());
        }
        lista.Add(canvas.transform.GetChild(pos).GetChild(0).gameObject.GetComponent<ArmaduraController>());
        canvas.transform.GetChild(pos).gameObject.SetActive(false);
        return lista;
    }

    public void load() {
        try{
            string json = File.ReadAllText("datos_jugador.json");
            inventario = JsonUtility.FromJson<Inventory>(json);
        }
        catch(Exception) {
            nuevoJugador();
            return;
        }
        

        for (int i = 0; i < 10; i++)
        {
            if (inventario.Piernas[i].getTipo() != "")
            {
                piernas[i].loadJson(inventario.Piernas[i]);

            }
            
        }
        for (int i = 0; i < 10; i++)
        {
            if (inventario.Pechos[i].getTipo() != "")
            {
                pechos[i].loadJson(inventario.Pechos[i]);

            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (inventario.Brazos[i].getTipo() != "")
            {
                brazos[i].loadJson(inventario.Brazos[i]);

            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (inventario.Cascos[i].getTipo() != "")
            {
                cascos[i].loadJson(inventario.Cascos[i]);

            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (inventario.Principal[i].getTipo() != "")
            {
                principal[i].loadJson(inventario.Principal[i]);

            }
        }/*
        for (int i = 0; i < 10; i++)
        {
            if (inventario.Secundaria[i].getTipo() != "")
            {
                secundaria[i].loadJson(inventario.Secundaria[i]);

            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (inventario.Pesada[i].getTipo() != "")
            {
                pesada[i].loadJson(inventario.Pesada[i]);

            }
        }*/
    }

    public void save()
    {
        inventario = new Inventory();

        foreach (ArmaduraController i in piernas)
        {
            if (i.getItem().getNombre() != "")
            {
                inventario.Piernas.Add(i.getItem());
            }
            else {
                inventario.Piernas.Add(new Armadura());
            }
        }
        foreach (ArmaduraController i in cascos)
        {
            if (i.getItem().getNombre() != "") {
                inventario.Cascos.Add(i.getItem());
            }
            else
            {
                inventario.Cascos.Add(new Armadura());
            }
        }
        foreach (ArmaduraController i in brazos)
        {
            if (i.getItem().getNombre() != "")
            {
                inventario.Brazos.Add(i.getItem());
            }
            else
            {
                inventario.Brazos.Add(new Armadura());
            }
        }
        foreach (ArmaduraController i in pechos)
        {
            if (i.getItem().getNombre() != "")
            {
                inventario.Pechos.Add(i.getItem());
            }
            else
            {
                inventario.Pechos.Add(new Armadura());
            }
        }
        foreach (ArmaController i in principal)
        {
            if (i.getItem().getNombre() != "")
            {
                Debug.Log("asdcdsasdc");
                inventario.Principal.Add(i.getItem());
            }
            else
            {
                inventario.Principal.Add(new Arma());
            }
        }
        /*foreach (ItemController i in secundaria)
        {
            inventario.Secundaria.Add(i.getItem());
        }
        foreach (ItemController i in pesada)
        {
            inventario.Pesada.Add(i.getItem());
        }*/

        File.WriteAllText("datos_jugador.json", JsonUtility.ToJson(inventario));
    }

    private void nuevoJugador()
    {
        piernas[9].nuevoJugador("Pierna basica", "Pierna"); 
        brazos[9].nuevoJugador("Brazo basico", "Brazo");
        pechos[9].nuevoJugador("Pechera basica", "Pecho");
        cascos[9].nuevoJugador("Casco basico", "Casco");
        /*pesada[9].nuevoJugador("");
        secundaria[9].nuevoJugador("");*/
        principal[9].nuevoJugador("Ak-47 Compacta", "Arma automatica");
    }

    public void setCurrentItem(GameObject current) {
        currentItemBorrar = current;
        Item thisItem = null;
        try {thisItem = currentItemBorrar.GetComponent<ArmaduraController>().getItem();}
        catch(Exception) {
            try{thisItem = currentItemBorrar.GetComponent<ArmaController>().getItem();}catch (Exception){ }
        }
        
        //PODRIA CREAR UNA CLASE PARA ESTO PERO SOCARRAO... COMPLETA Y ABSOLUTAMENTE
        if (thisItem == null) {
            inpectorItem.SetActive(false);
            return;
        } else if(thisItem.getNombre() == "")
        {
            inpectorItem.SetActive(false);
            return;
        }

        Debug.Log("Pasa");
        inpectorItem.SetActive(true);

        string tipoCalidad = thisItem.getTipo();

        if(tipoCalidad.Contains("Comun")) inpectorItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = listaTipos[0];
        else if (tipoCalidad.Contains("Peculiar")) inpectorItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = listaTipos[1];
        else if (tipoCalidad.Contains("Raro")) inpectorItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = listaTipos[2];
        else if (tipoCalidad.Contains("Unico")) inpectorItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = listaTipos[3];
        else inpectorItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = listaTipos[4];

        string nombre = thisItem.getNombre();
        string equipo = thisItem.getEquipo();

        inpectorItem.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(nombre);
        inpectorItem.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(equipo);

        if (thisItem is Armadura)
        {
            int dureza = ((Armadura)thisItem).getDureza();
            int reg = ((Armadura)thisItem).getRegeneracion();
            int move = ((Armadura)thisItem).getMovilidad();
            int total = ((Armadura)thisItem).getTotal();

            inpectorItem.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Dureza");
            inpectorItem.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Slider>().value = dureza;
            inpectorItem.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(dureza+"");

            inpectorItem.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Regeneracion");
            inpectorItem.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Slider>().value = reg;
            inpectorItem.transform.GetChild(1).GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(reg + "");

            inpectorItem.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Movilidad");
            inpectorItem.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Slider>().value = move;
            inpectorItem.transform.GetChild(1).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(move + "");

            inpectorItem.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Total");
            inpectorItem.transform.GetChild(1).GetChild(3).GetChild(1).gameObject.SetActive(false);
            inpectorItem.transform.GetChild(1).GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(total + "");

            inpectorItem.transform.GetChild(1).GetChild(4).gameObject.SetActive(false);
            inpectorItem.transform.GetChild(1).GetChild(5).gameObject.SetActive(false);
        }
        else {
            int danno = ((Arma)thisItem).Danno;
            int estabilidad = ((Arma)thisItem).Estabilidad;
            int alcance = ((Arma)thisItem).Alcance;
            int velRecarga = ((Arma)thisItem).VelocidadRecarga;
            int cargador = ((Arma)thisItem).Cargador;
            int cadencia = ((Arma)thisItem).Cadencia;

            inpectorItem.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Daño");
            inpectorItem.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Slider>().value = danno;
            inpectorItem.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(danno + "");

            inpectorItem.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Alcance");
            inpectorItem.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Slider>().value = alcance;
            inpectorItem.transform.GetChild(1).GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(alcance + "");

            inpectorItem.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Estabilidad");
            inpectorItem.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Slider>().value = estabilidad;
            inpectorItem.transform.GetChild(1).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(estabilidad + "");

            inpectorItem.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Recarga");
            inpectorItem.transform.GetChild(1).GetChild(3).GetChild(1).gameObject.SetActive(true);
            inpectorItem.transform.GetChild(1).GetChild(3).GetChild(1).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Slider>().value = velRecarga;
            inpectorItem.transform.GetChild(1).GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(velRecarga + "");

            inpectorItem.transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
            inpectorItem.transform.GetChild(1).GetChild(5).gameObject.SetActive(true);

            inpectorItem.transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Cargador");
            inpectorItem.transform.GetChild(1).GetChild(4).GetChild(1).gameObject.SetActive(false);
            inpectorItem.transform.GetChild(1).GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(cargador + "");

            inpectorItem.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Cadencia");
            inpectorItem.transform.GetChild(1).GetChild(5).GetChild(1).gameObject.SetActive(false);
            inpectorItem.transform.GetChild(1).GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().SetText(cadencia + "");
        }
    }

    internal void SetBorrarItem(bool v)
    {
        borrarItem = v;
    }

    internal void SetCurrentiempoBorrar(float v)
    {
        currentiempoBorrar = v;
    }
}
