using System;

class Carro {
    public string Marca;
    public string Modelo;
    public int Año;
}

class Program {
    static void Main() {
        Carro c = new Carro();
        c.Marca = "Nissan";
        c.Modelo = "Versa";
        c.Año = 2020;

        Console.WriteLine($"Carro: {c.Marca} {c.Modelo} ({c.Año})");
    }
}
