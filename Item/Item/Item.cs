using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] protected string nombre;
    [SerializeField] protected string tipo;
    [SerializeField] protected string equipo;

    [SerializeField] protected int materialPrincipalHash;
    [SerializeField] protected int materialSecundarioHash;
    public Material materialPrincipal;
    public Material materialSecundario;

    public Item() {
        tipo = "";
        equipo = "";
        materialSecundarioHash = 0;
        materialPrincipalHash = 0;
    }

    public Item(Item i) {
        nombre = i.nombre;
        tipo = i.tipo;
        equipo = i.equipo;
        materialPrincipal = i.materialPrincipal;
        materialSecundario = i.materialSecundario;
        materialPrincipalHash = i.materialPrincipalHash;
        materialSecundarioHash = i.materialSecundarioHash;
    }

    public void setTipo(string tipo) {
        this.tipo = tipo;
    }

    public string getTipo()
    {
        return tipo;
    }
    public void setEquipo(string equipo)
    {
        this.equipo = equipo;
    }

    public string getEquipo()
    {
        return equipo;
    }

    public void setMaterialPrincipal(Material nuevoMaterial)
    {
        materialPrincipal = nuevoMaterial;
        setMaterialPrincipalHash(materialPrincipal.GetHashCode());
    }

    public Material getMaterialPrincipal()
    {
        return materialPrincipal;
    }

    public void setMaterialSecundario(Material nuevoMaterial)
    {
        materialSecundario = nuevoMaterial;
        setMaterialSecundarioHash(materialSecundario.GetHashCode());
    }

    public Material getMaterialSecundario()
    {
        return materialSecundario;
    }

    public void setMaterialPrincipalHash(int nuevoMaterial)
    {
        materialPrincipalHash = nuevoMaterial;
    }

    public int getMaterialPrincipalHash()
    {
        return materialPrincipalHash;
    }

    public void setMaterialSecundarioHash(int nuevoMaterial)
    {
        materialSecundarioHash = nuevoMaterial;
    }

    public int getMaterialSecundarioHash()
    {
        return materialSecundarioHash;
    }

    public void setNombre(string nombre)
    {
        this.nombre = nombre;
    }

    public string getNombre()
    {
        return nombre;
    }
}
