using System.Collections.Generic;
using Unity.FPS.AI;
using UnityEngine;

public class SpawnPuerta : MonoBehaviour
{
    [SerializeField] Transform spawn;
    [SerializeField] GameObject enemigo;
    [SerializeField] PatrolPath ruta;
    [SerializeField] int numEntidadesMax;
    [SerializeField] List<GameObject> currentEntidades;
    [SerializeField] float tiempoSpawnDelay;
    [SerializeField] float currentTime;
    [SerializeField] bool stopSpawn;
    [SerializeField] bool stopSpawnWhenMaxEntidades;

    public void NuevoEnemigo() {
        
        Debug.Log("Aparece un enemigo por la puerta " + gameObject.name);
        GameObject nuevo = Instantiate(enemigo, spawn.position, new Quaternion(0, 0, 0, 0));
        ruta.addEnemyPatrol(nuevo.GetComponent<EnemyController>());
        nuevo.GetComponent<EnemyController>().pruebaOtravez();
        currentEntidades.Add(nuevo);
    }

    private void Start()
    {
        currentTime = tiempoSpawnDelay;
    }

    private void Update()
    {
        if (stopSpawn) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0) {
            currentTime = tiempoSpawnDelay;
            if (!checkEnemigos()) return;

            NuevoEnemigo();  
        }
    }

    private bool checkEnemigos() {
        if (numEntidadesMax > currentEntidades.Count) return true;

        foreach (GameObject current in currentEntidades) {
            if (current == null) {
                currentEntidades.Remove(current);
                return true;
            }
        }

        if (stopSpawnWhenMaxEntidades) {
            stopSpawn = true;
        }
        return false;
    }

    public bool StopSpawn { get => stopSpawn; set => stopSpawn = value; }
    public bool StopSpawnWhenMaxEntidades { get => stopSpawnWhenMaxEntidades; set => stopSpawnWhenMaxEntidades = value; }
    public float TiempoSpawnDelay { get => tiempoSpawnDelay; set => tiempoSpawnDelay = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }
    public int NumEntidadesMax { get => numEntidadesMax; set => numEntidadesMax = value; }
    public PatrolPath Ruta { get => ruta; set => ruta = value; }
    public GameObject Enemigo { get => enemigo; set => enemigo = value; }
    public List<GameObject> CurrentEntidades { get => currentEntidades; set => currentEntidades = value; }

    public void SetEnemigo(GameObject nuevoEnemigo) { 
        enemigo = nuevoEnemigo;
    }

    public int CountCurrentEntidades() {
        int num = 0;
        foreach (GameObject entidad in currentEntidades) {
            if (entidad == null)
            {
                currentEntidades.Remove(entidad);
            }
            else {
                num++;
            }
        }

        Debug.Log(gameObject.name + ": Quedan " + num + " Enenmigos");

        return num;
    }
}
