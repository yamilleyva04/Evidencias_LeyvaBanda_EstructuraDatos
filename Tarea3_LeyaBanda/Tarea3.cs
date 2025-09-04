using System;
using System.Collections.Generic;

class Program {
    static void Main() {
        List<int> arr = new List<int> { 10, 20, 30, 40, 50 };

        Console.WriteLine("Arreglo inicial: " + string.Join(", ", arr));
        for (int i = 0; i < arr.Count; i++) {
            Console.WriteLine($"arr[{i}] = {arr[i]}");
        }

        Console.Write("Ingrese la posición donde quiere insertar: ");
        int pos = int.Parse(Console.ReadLine());
        Console.Write("Ingrese el valor a insertar: ");
        int val = int.Parse(Console.ReadLine());
        arr.Insert(pos, val);

        Console.WriteLine("Arreglo después de insertar: " + string.Join(", ", arr));

        Console.Write("Ingrese el valor a buscar: ");
        int buscar = int.Parse(Console.ReadLine());
        int encontrado = -1;

        for (int i = 0; i < arr.Count; i++) {
            if (arr[i] == buscar) {
                encontrado = i;
                break;
            }
        }

        if (encontrado != -1)
            Console.WriteLine($"Elemento {buscar} encontrado en índice {encontrado}");
        else
            Console.WriteLine("Elemento no encontrado");
    }
}
