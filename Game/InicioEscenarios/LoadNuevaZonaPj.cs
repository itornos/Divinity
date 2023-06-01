using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

//SCRIPT PARA SITUAR EN LA NUEVA ESCENA AL MODELO JUGADOR (UTILIZAR SOLO CUANDO SE PASA DE UNA ESCENA A OTRA EL MODELO DEL PJ)
public class LoadNuevaZonaPj : MonoBehaviour
{
    [SerializeField] GameObject pj;

    private void Update()
    {
        pj = GameObject.FindGameObjectWithTag("Player");
        if (pj.GetComponent<Actor>().nuevaEscena()) {
            Destroy(gameObject);
        };
    }
}
