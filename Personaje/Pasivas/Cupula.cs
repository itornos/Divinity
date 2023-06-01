using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupula : MonoBehaviour
{
    [SerializeField] CharacterController colliderToIgnore;
    [SerializeField] float duracion;

    private void Start()
    {
        duracion = 10f;
        colliderToIgnore = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        Collider collider = GetComponent<Collider>();

        if (collider && colliderToIgnore)
        {
            Physics.IgnoreCollision(collider, colliderToIgnore);
        }
    }

    private void Update()
    {
        duracion -= Time.deltaTime;
        if(duracion <= 0)Destroy(gameObject);
    }
}
