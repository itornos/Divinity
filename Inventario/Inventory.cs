using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    [SerializeField] List<Armadura> cascos;
    [SerializeField] List<Armadura> brazos;
    [SerializeField] List<Armadura> pechos;
    [SerializeField] List<Armadura> piernas;
    [SerializeField] List<Arma> principal;
    [SerializeField] List<Arma> secundaria;
    [SerializeField] List<Arma> pesada;
    [SerializeField] List<Item> postMaster;

    public Inventory()
    {
        Cascos = new List<Armadura>();
        Brazos = new List<Armadura>();
        Pechos = new List<Armadura>();
        Piernas = new List<Armadura>();
        Principal = new List<Arma>();
        Secundaria = new List<Arma>();
        Pesada = new List<Arma>();
        PostMaster = new List<Item>(30);
    }

    public List<Armadura> Cascos { get => cascos; set => cascos = value; }
    public List<Armadura> Brazos { get => brazos; set => brazos = value; }
    public List<Armadura> Pechos { get => pechos; set => pechos = value; }
    public List<Armadura> Piernas { get => piernas; set => piernas = value; }
    public List<Arma> Principal { get => principal; set => principal = value; }
    public List<Arma> Secundaria { get => secundaria; set => secundaria = value; }
    public List<Arma> Pesada { get => pesada; set => pesada = value; }
    public List<Item> PostMaster { get => postMaster; set => postMaster = value; }
}
