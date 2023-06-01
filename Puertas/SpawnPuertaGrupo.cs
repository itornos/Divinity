using System.Collections.Generic;
using Unity.FPS.AI;
using UnityEngine;

[System.Serializable]
public class GameObjectArrayWrapper
{
    public GameObjectArrayWrapper() {
        array = new List<GameObject>();
    }

    public List<GameObject> array;
    public List<GameObject> Array { get => array; set => array = value; }
}

public class SpawnPuertaGrupo : MonoBehaviour
{
    [SerializeField] Transform spawn;
    [SerializeField] GameObject enemigo;
    [SerializeField] PatrolPath ruta;
    [SerializeField] float tiempoSpawnDelay;
    [SerializeField] float currentTime;
    [SerializeField] int numGruposMax;
    [SerializeField] int currentGrupos;
    [SerializeField] int NumIntegrantesPorGrupo;
    [SerializeField] public List<GameObjectArrayWrapper> grupos;

    public void NuevoEnemigo()
    {

        Debug.Log("Aparece un enemigo por la puerta " + gameObject.name);
        GameObject nuevo = Instantiate(enemigo, spawn.position, new Quaternion(0, 0, 0, 0));
        ruta.addEnemyPatrol(nuevo.GetComponent<EnemyController>());
        nuevo.GetComponent<EnemyController>().pruebaOtravez();
        grupos[currentGrupos].Array.Add(nuevo);
    }

    private void Start()
    {
        currentTime = tiempoSpawnDelay;
        grupos= new List<GameObjectArrayWrapper>();
        currentGrupos = 0;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = tiempoSpawnDelay;

            if (!checkGrupos())return;

            grupos.Add(new GameObjectArrayWrapper());
            Debug.Log("Nuevo Grupo de enemigos (Grupo " + currentGrupos + ")"); 

            for (int i = 0; i < NumIntegrantesPorGrupo; i++)
            {
                NuevoEnemigo();
                Debug.Log("Enemigo " + i + " se ha unido al grupo");
            }

            currentGrupos++;
        }
    }

    private bool checkGrupos() {
        int contMueltos;

        if (grupos.Count < numGruposMax) {
            return true;
        }

        foreach (GameObjectArrayWrapper grupo in grupos)
        {
            contMueltos = 0;
            foreach (GameObject integrante in grupo.Array)
            {
                if(integrante == null)contMueltos++;
            }
            if (contMueltos == NumIntegrantesPorGrupo) {
                grupos.Remove(grupo);
                currentGrupos--;
                return true; 
            }
        }

        return false;
    }
}
