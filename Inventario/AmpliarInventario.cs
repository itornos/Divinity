using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpliarInventario : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void abrirInvetario() {
        anim.SetBool("abrir", true);
    }

    public void cerrarInventario() {
        anim.SetBool("abrir", false);
    }

    public void abrirInvetarioIzq()
    {
        anim.SetBool("abrirIzq", true);
    }

    public void cerrarInventarioIzq()
    {
        anim.SetBool("abrirIzq", false);
    }
}
