
using System;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Armadura : Item
{
    [SerializeField] int Dureza;
    [SerializeField] int Regeneracion;
    [SerializeField] int Movilidad;
    [SerializeField] int total;

    public Armadura(string nombre, string equipo, string tipo) {
        this.nombre = nombre;
        this.equipo = equipo;
        this.tipo = tipo;
        Debug.Log(nombre);
        totalRandomize(tipo);
    }

    public Armadura(Item i) : base(i){}

    public Armadura() : base() {
        Dureza = 0;
        Regeneracion = 0;
        Movilidad = 0;
        total = 0;
    }

    private void totalRandomize(string tipo)
    {
        System.Random random = new System.Random();
        switch (tipo) {
            case "Comun (Instance)":
                total = random.Next(6, 16);
                statsRandomize();
                return;
            case "Peculiar (Instance)":
                total = random.Next(16, 30);
                statsRandomize();
                return;
            case "Raro (Instance)":
                total = random.Next(31, 46);
                statsRandomize();
                return;
            case "Unico (Instance)":
                total = random.Next(46, 61);
                statsRandomize();
                return;
            case "Legendario (Instance)":
                total = random.Next(61, 76);
                statsRandomize();
                return;
        }
        return;
    }

    private void statsRandomize() {
        int valor;
        int totalLocal = total;
        System.Random random = new System.Random();

        valor = random.Next(2, totalLocal - 4);
        totalLocal -= valor;
        Movilidad = valor;

        valor = random.Next(2, totalLocal - 2);
        totalLocal -= valor;
        Regeneracion = valor;

        Dureza = totalLocal;

        Debug.Log(total + "   Dureza" + Dureza + "    Movilidad" + Movilidad + "    Regeneracion" + Regeneracion);
    }

    public int getDureza() {
        return Dureza;
    }
    public int getMovilidad()
    {
        return Movilidad;
    }
    public int getRegeneracion()
    {
        return Regeneracion;
    }

    public int getTotal() {
        return total;
    }
}
