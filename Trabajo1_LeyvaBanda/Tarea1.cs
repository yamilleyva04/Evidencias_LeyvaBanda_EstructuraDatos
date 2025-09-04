using System;
using System.Collections.Generic;

class Program {
    static void Main() {
        // Arreglo inicial
        string[] nombres = { "Ana", "Luis", "Carlos", "María" };
        nombres[1] = "Pedro";

        foreach (string n in nombres) {
            Console.WriteLine(n);
        }

        // Lista dinámica
        Console.Write("¿Cuántos nombres quieres agregar?: ");
        int tamaño = int.Parse(Console.ReadLine());
        List<string> arr = new List<string>();

        for (int i = 0; i < tamaño; i++) {
            Console.Write($"Ingrese el nombre {i+1}: ");
            string valor = Console.ReadLine();
            arr.Add(valor);
        }

        Console.WriteLine("Lista final: " + string.Join(", ", arr));
    }
}
