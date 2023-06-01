

using UnityEngine;

[System.Serializable]
public class Arma : Item
{
    [SerializeField] int danno;
    [SerializeField] int estabilidad;
    [SerializeField] int alcance;
    [SerializeField] int velocidadRecarga;
    [SerializeField] int cargador;
    [SerializeField] int cadencia;//1:360 / 2:540 / 3:720 / 4:900

    public Arma(string nombre, string equipo, string tipo)
    {
        this.nombre = nombre;
        this.equipo = equipo;
        this.tipo = tipo;
        totalRandomize(tipo);
    }

    public Arma(Item i) : base(i) { }

    public Arma() { }

    private void totalRandomize(string tipo)
    {
        System.Random random = new System.Random();
        switch (tipo)
        {
            case "Comun (Instance)":
                estabilidad = random.Next(0, 6);
                alcance = random.Next(0, 10);
                velocidadRecarga = random.Next(0, 2);
                cargador = random.Next(0, 6);
                danno = random.Next(0, 3);
                cadencia= random.Next(1, 5);
                dannoFinal();
                return;
            case "Peculiar (Instance)":
                estabilidad = random.Next(5, 11);
                alcance = random.Next(10, 15);
                velocidadRecarga = random.Next(1, 3);
                cargador = random.Next(5, 11);
                danno = random.Next(2, 5);
                cadencia = random.Next(1, 5);
                dannoFinal();
                return;
            case "Raro (Instance)":
                estabilidad = random.Next(10, 16);
                alcance = random.Next(15, 21);
                velocidadRecarga = random.Next(2, 5);
                cargador = random.Next(10, 16);
                danno = random.Next(3, 6);
                cadencia = random.Next(1, 5);
                dannoFinal();
                return;
            case "Unico (Instance)":
                estabilidad = random.Next(15, 20);
                alcance = random.Next(20, 26);
                velocidadRecarga = random.Next(4, 7);
                cargador = random.Next(15, 21);
                danno = random.Next(4, 7);
                cadencia = random.Next(1, 5);
                dannoFinal();
                return;
            case "Legendario (Instance)":
                estabilidad = random.Next(20, 31);
                alcance = random.Next(25, 41);
                velocidadRecarga = random.Next(5, 11);
                cargador = random.Next(20, 26);
                danno = random.Next(5, 8);
                cadencia = random.Next(1, 5);
                dannoFinal();
                return;
        }
        return;
    }

    private void dannoFinal()
    {
        System.Random random = new System.Random();
        switch (cadencia) {
            case 1 :
                alcance += 70;
                velocidadRecarga += 10;
                cadencia = 360;
                cargador += 20;
                danno += 24;
                return;
            case 2:
                alcance += 50;
                velocidadRecarga += 10;
                cadencia = 540;
                danno += 16;
                cargador += 25;
                return;
            case 3:
                alcance += 35;
                velocidadRecarga += 10;
                cadencia = 720;
                danno += 11;
                cargador += 30;
                return;
            case 4:
                alcance += 20;
                velocidadRecarga += 10;
                cadencia = 900;
                cargador += 35;
                danno += 8;
                return;
        }
    }

    public int Danno { get => danno; set => danno = value; }
    public int Estabilidad { get => estabilidad; set => estabilidad = value; }
    public int Alcance { get => alcance; set => alcance = value; }
    public int VelocidadRecarga { get => velocidadRecarga; set => velocidadRecarga = value; }
    public int Cargador { get => cargador; set => cargador = value; }
    public int Cadencia { get => cadencia; set => cadencia = value; }
}
